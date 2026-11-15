Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

' =====================================================================
' The "Data" dropdown in the top bar.
'   * Export transactions (CSV)  - opens in Excel / Google Sheets
'   * Export backup (JSON)       - everything: wallets, goals, limits, recurring...
'   * Import transactions (CSV)  - ADDS to what you already have (duplicates are skipped)
'   * Restore backup (JSON)      - REPLACES everything with the backup
'   * Reset all data             - back to a fresh start
' The box always shows "Data"; picking an item runs it and the box goes back to "Data".
' =====================================================================
Partial Class MainForm

    Private Const DataMenuHeader As Integer = 0
    Private Const DataExportCsv As Integer = 1
    Private Const DataExportJson As Integer = 2
    Private Const DataImportCsv As Integer = 3
    Private Const DataRestoreJson As Integer = 4
    Private Const DataResetAll As Integer = 5

    Private Sub InitDataMenu()
        DataDropdown.DropDownStyle = ComboBoxStyle.DropDownList
        DataDropdown.Items.Clear()
        DataDropdown.Items.AddRange(New Object() {
            "Data",
            "Export transactions (CSV)",
            "Export backup (JSON)",
            "Import transactions (CSV)",
            "Restore backup (JSON)",
            "Reset all data"})
        DataDropdown.DropDownWidth = CInt(280 * _scaleK)
        DataDropdown.SelectedIndex = DataMenuHeader
        AddHandler DataDropdown.SelectionChangeCommitted, AddressOf DataDropdown_Commit
    End Sub

    Private Sub DataDropdown_Commit(sender As Object, e As EventArgs)
        Dim choice As Integer = DataDropdown.SelectedIndex
        DataDropdown.SelectedIndex = DataMenuHeader
        If _budget Is Nothing OrElse choice <= DataMenuHeader Then Return

        ' Let the dropdown finish closing before a dialog opens.
        BeginInvoke(New Action(Sub() RunDataAction(choice)))
    End Sub

    Private Sub RunDataAction(choice As Integer)
        Select Case choice
            Case DataExportCsv
                ExportCsv()
            Case DataExportJson
                ExportJson()
            Case DataImportCsv
                ImportCsv()
            Case DataRestoreJson
                RestoreJson()
            Case DataResetAll
                ResetEverything()
        End Select
    End Sub

    ' ------------------------------------------------------------------
    ' Export
    ' ------------------------------------------------------------------
    Private Sub ExportCsv()
        Dim path As String = AskSavePath("Export transactions", "CSV file (*.csv)|*.csv", "csv", "transactions")
        If path Is Nothing Then Return

        Dim sb As New StringBuilder()
        sb.Append("Date,Type,Title,Amount,Category,Payment,Wallet,Notes").Append(vbCrLf)
        For Each t As Txn In _budget.Data.Transactions.OrderBy(Function(x) x.Date).ThenBy(Function(x) x.Title)
            sb.Append(CsvCell(t.Date)).Append(","c)
            sb.Append(CsvCell(t.Type)).Append(","c)
            sb.Append(CsvCell(t.Title)).Append(","c)
            sb.Append(t.Amount.ToString("0.00", CultureInfo.InvariantCulture)).Append(","c)
            sb.Append(CsvCell(t.Category)).Append(","c)
            sb.Append(CsvCell(t.Payment)).Append(","c)
            sb.Append(CsvCell(_budget.WalletName(t.WalletId))).Append(","c)
            sb.Append(CsvCell(t.Notes)).Append(vbCrLf)
        Next

        If WriteTextFile(path, sb.ToString()) Then
            Info("Exported " & _budget.Data.Transactions.Count.ToString() & " transactions.")
        End If
    End Sub

    Private Sub ExportJson()
        Dim path As String = AskSavePath("Export backup", "JSON backup (*.json)|*.json", "json", "backup")
        If path Is Nothing Then Return
        If WriteTextFile(path, DataStore.ToJson(_budget.Data)) Then
            Info("Backup saved. Use ""Restore backup (JSON)"" to load it again.")
        End If
    End Sub

    Private Function AskSavePath(title As String, filter As String, ext As String, baseName As String) As String
        Using dlg As New SaveFileDialog()
            dlg.Title = title
            dlg.Filter = filter
            dlg.DefaultExt = ext
            dlg.AddExtension = True
            dlg.OverwritePrompt = True
            dlg.FileName = "pesomint-" & baseName & "-" & DateText(Date.Today)
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return Nothing
            Return dlg.FileName
        End Using
    End Function

    Private Function WriteTextFile(path As String, text As String) As Boolean
        Try
            File.WriteAllText(path, text, New UTF8Encoding(True))
            Return True
        Catch ex As IOException
            Warn("Couldn't write the file: " & ex.Message)
        Catch ex As UnauthorizedAccessException
            Warn("Windows doesn't allow saving there. Pick another folder.")
        End Try
        Return False
    End Function

    Private Shared Function CsvCell(value As String) As String
        Dim s As String = If(value, "")
        If s.IndexOfAny(New Char() {","c, """"c, ControlChars.Cr, ControlChars.Lf}) >= 0 Then
            Return """" & s.Replace("""", """""") & """"
        End If
        Return s
    End Function

    ' ------------------------------------------------------------------
    ' Import
    ' ------------------------------------------------------------------
    Private Function AskOpenPath(title As String, filter As String) As String
        Using dlg As New OpenFileDialog()
            dlg.Title = title
            dlg.Filter = filter
            dlg.CheckFileExists = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return Nothing
            Return dlg.FileName
        End Using
    End Function

    Private Sub ImportCsv()
        Dim path As String = AskOpenPath("Import transactions", "CSV file (*.csv)|*.csv")
        If path Is Nothing Then Return

        Dim text As String
        Try
            text = File.ReadAllText(path)
        Catch ex As IOException
            Warn("Couldn't read the file: " & ex.Message)
            Return
        Catch ex As UnauthorizedAccessException
            Warn("Windows doesn't allow opening that file.")
            Return
        End Try

        Dim rows As List(Of List(Of String)) = ParseCsv(text)
        If rows.Count < 2 Then
            Warn("That file has no transactions in it.")
            Return
        End If

        ' Find the columns by their header names so the order doesn't matter.
        Dim col As New Dictionary(Of String, Integer)()
        For i As Integer = 0 To rows(0).Count - 1
            col(rows(0)(i).Trim().ToLowerInvariant()) = i
        Next
        If Not (col.ContainsKey("date") AndAlso col.ContainsKey("type") AndAlso col.ContainsKey("amount")) Then
            Warn("The first row must have at least these columns: Date, Type, Amount." & vbCrLf & vbCrLf &
                 "Tip: export a CSV first and use it as a template.")
            Return
        End If

        Dim cellOf As Func(Of List(Of String), String, String) =
            Function(r As List(Of String), name As String) As String
                Dim idx As Integer
                If col.TryGetValue(name, idx) AndAlso idx < r.Count Then Return r(idx).Trim()
                Return ""
            End Function

        Dim wallets As List(Of Wallet) = _budget.Data.Wallets
        Dim defaultWallet As String = wallets(0).Id
        If wallets.Any(Function(w) w.Id = "cash") Then defaultWallet = "cash"

        Dim existing As New HashSet(Of String)(_budget.Data.Transactions.Select(Function(t) TxnKey(t.Date, t.Type, t.Amount, t.Title)))
        Dim added As New List(Of Txn)()
        Dim skippedBad As Integer = 0
        Dim skippedDup As Integer = 0
        Dim latest As Date = Date.MinValue

        For r As Integer = 1 To rows.Count - 1
            Dim row As List(Of String) = rows(r)
            If row.All(Function(c) String.IsNullOrWhiteSpace(c)) Then Continue For

            Dim kind As String = cellOf(row, "type").ToLowerInvariant()
            Dim amount As Decimal
            Dim amountText As String = cellOf(row, "amount").Replace(Peso, "").Replace(",", "").Replace(" ", "")
            Dim whenText As String = cellOf(row, "date")
            Dim whenDate As Date

            If kind <> "income" AndAlso kind <> "expense" Then
                skippedBad += 1
                Continue For
            End If
            If Not Decimal.TryParse(amountText, NumberStyles.Number, CultureInfo.InvariantCulture, amount) OrElse amount <= 0D Then
                skippedBad += 1
                Continue For
            End If
            If IsValidDate(whenText) Then
                whenDate = ParseDate(whenText, Date.Today)
            ElseIf Not Date.TryParse(whenText, CultureInfo.InvariantCulture, DateTimeStyles.None, whenDate) Then
                skippedBad += 1
                Continue For
            End If

            Dim t As New Txn()
            t.Type = kind
            t.Title = cellOf(row, "title")
            t.Amount = amount
            t.Date = DateText(whenDate)
            t.Category = cellOf(row, "category")
            t.Payment = cellOf(row, "payment")
            t.Notes = cellOf(row, "notes")
            t.WalletId = defaultWallet

            Dim walletText As String = cellOf(row, "wallet")
            If walletText.Length > 0 Then
                Dim match As Wallet = wallets.FirstOrDefault(Function(w) String.Equals(w.Name, walletText, StringComparison.OrdinalIgnoreCase) OrElse
                                                                         String.Equals(w.Id, walletText, StringComparison.OrdinalIgnoreCase))
                If match IsNot Nothing Then t.WalletId = match.Id
            End If

            Dim key As String = TxnKey(t.Date, t.Type, t.Amount, t.Title)
            If Not existing.Add(key) Then
                skippedDup += 1
                Continue For
            End If

            added.Add(t)
            If whenDate > latest Then latest = whenDate
        Next

        If added.Count = 0 Then
            Warn("Nothing new to import." & SkipNote(skippedBad, skippedDup))
            Return
        End If

        _budget.Data.Transactions.AddRange(added)
        _budget.ReplaceAll(DataStore.Clean(_budget.Data))
        _budget.GoToMonth(latest)
        Info("Imported " & added.Count.ToString() & " transactions." & SkipNote(skippedBad, skippedDup))
    End Sub

    Private Shared Function TxnKey(dateText As String, kind As String, amount As Decimal, title As String) As String
        Return dateText & "|" & kind & "|" & amount.ToString("0.00", CultureInfo.InvariantCulture) & "|" & If(title, "").Trim().ToLowerInvariant()
    End Function

    Private Shared Function SkipNote(bad As Integer, dup As Integer) As String
        Dim s As String = ""
        If dup > 0 Then s &= vbCrLf & dup.ToString() & " already existed and were skipped."
        If bad > 0 Then s &= vbCrLf & bad.ToString() & " rows had a missing or invalid date, type or amount and were skipped."
        Return s
    End Function

    ' Small CSV reader: handles quoted cells, commas and line breaks inside quotes, and a leading BOM.
    Private Shared Function ParseCsv(text As String) As List(Of List(Of String))
        Dim rows As New List(Of List(Of String))()
        Dim row As New List(Of String)()
        Dim cell As New StringBuilder()
        Dim inQuotes As Boolean = False
        Dim i As Integer = 0
        If text.Length > 0 AndAlso text(0) = ChrW(&HFEFF) Then i = 1

        While i < text.Length
            Dim ch As Char = text(i)
            If inQuotes Then
                If ch = """"c Then
                    If i + 1 < text.Length AndAlso text(i + 1) = """"c Then
                        cell.Append(""""c)
                        i += 1
                    Else
                        inQuotes = False
                    End If
                Else
                    cell.Append(ch)
                End If
            ElseIf ch = """"c Then
                inQuotes = True
            ElseIf ch = ","c Then
                row.Add(cell.ToString())
                cell.Clear()
            ElseIf ch = ControlChars.Lf Then
                row.Add(cell.ToString())
                cell.Clear()
                rows.Add(row)
                row = New List(Of String)()
            ElseIf ch <> ControlChars.Cr Then
                cell.Append(ch)
            End If
            i += 1
        End While

        If cell.Length > 0 OrElse row.Count > 0 Then
            row.Add(cell.ToString())
            rows.Add(row)
        End If
        Return rows
    End Function

    ' ------------------------------------------------------------------
    ' Restore / reset
    ' ------------------------------------------------------------------
    Private Sub RestoreJson()
        Dim path As String = AskOpenPath("Restore backup", "JSON backup (*.json)|*.json")
        If path Is Nothing Then Return

        Dim restored As AppData
        Try
            restored = DataStore.FromJson(File.ReadAllText(path))
        Catch ex As IOException
            Warn("Couldn't read the file: " & ex.Message)
            Return
        Catch ex As UnauthorizedAccessException
            Warn("Windows doesn't allow opening that file.")
            Return
        Catch ex As System.Text.Json.JsonException
            Warn("That file isn't a valid backup.")
            Return
        End Try

        Dim answer As DialogResult = MessageBox.Show(Me,
            "This backup has " & restored.Transactions.Count.ToString() & " transactions, " &
            restored.Wallets.Count.ToString() & " wallets and " & restored.Goals.Count.ToString() & " goals." & vbCrLf & vbCrLf &
            "Restoring REPLACES everything you have now. Continue?",
            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If answer <> DialogResult.Yes Then Return

        _budget.ReplaceAll(restored)
        Info("Backup restored.")
    End Sub

    Private Sub ResetEverything()
        Dim answer As DialogResult = MessageBox.Show(Me,
            "This permanently deletes all your transactions, goals, recurring items and custom wallets, and puts the budget limits back to their defaults." & vbCrLf & vbCrLf &
            "Tip: use ""Export backup (JSON)"" first if you might want them back." & vbCrLf & vbCrLf &
            "Reset everything?",
            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If answer <> DialogResult.Yes Then Return

        _budget.ResetAll()
        Info("All data was reset.")
    End Sub

    ' ------------------------------------------------------------------
    Private Sub Info(message As String)
        MessageBox.Show(Me, message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Warn(message As String)
        MessageBox.Show(Me, message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

End Class
