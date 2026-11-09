Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

' =====================================================================
' PART 4 - Transaction history card:
'   * the table of this month's transactions (newest first)
'   * search box + Type / Category / Payment filters + Reset Filters
'   * "x of y records" counter
'   * Edit and Delete on every row
'   * the empty states ("A fresh start" / "No matching transactions")
' The table is drawn by one Surface (see Controls.vb) placed under the
' filters. The card grows or shrinks with the number of rows, and the
' footer text and scroll area follow it.
' =====================================================================
Partial Class MainForm

    Private _historyList As Surface
    Private _historyRows As New List(Of Txn)()
    Private _historyBaseHeight As Integer    ' card height from the designer (used for the empty states)
    Private _historyFooterGap As Integer     ' space between the card and the footer text
    Private _stageExtra As Integer           ' space under the footer text inside the scrolling stage
    Private _scaleK As Single = 1.0F         ' screen-DPI scale (designer was drawn at 120 dpi)
    Private _filtersLoading As Boolean = False

    ' ------------------------------------------------------------------
    ' One-time setup (called from MainForm_Load, after BuildCenteredLayout)
    ' ------------------------------------------------------------------
    Private Sub InitHistory()
        If _historyList IsNot Nothing Then Return

        _scaleK = CSng(CurrentAutoScaleDimensions.Width / AutoScaleDimensions.Width)
        _historyBaseHeight = TransHistoryPnl.Height
        _historyFooterGap = BottomLeftBGText.Top - TransHistoryPnl.Bottom
        _stageExtra = _stage.Height - BottomLeftBGText.Bottom

        ' --- filters ---
        _filtersLoading = True
        FillCombo(TypeFilter, New String() {"All types", "Income", "Expense"})
        FillCombo(CategoryFilter, New String() {"All categories"}.Concat(Catalog.ExpenseNames()).Concat(Catalog.IncomeCategories).ToArray())
        FillCombo(PaymentMethodFilter, New String() {"All payment methods"}.Concat(Catalog.PaymentMethods).ToArray())
        TransactionSearchBox.Text = ""
        TransactionSearchBox.PlaceholderText = "Search transactions..."
        _filtersLoading = False

        AddHandler TypeFilter.SelectedIndexChanged, AddressOf HistoryFilterChanged
        AddHandler CategoryFilter.SelectedIndexChanged, AddressOf HistoryFilterChanged
        AddHandler PaymentMethodFilter.SelectedIndexChanged, AddressOf HistoryFilterChanged
        AddHandler TransactionSearchBox.TextChanged, AddressOf HistoryFilterChanged
        AddHandler FilterReset.Click, AddressOf FilterReset_Click

        ' --- the table ---
        _historyList = New Surface()
        _historyList.Radius = 0
        _historyList.Bordered = False
        _historyList.BackColor = TransHistoryPnl.BackColor
        _historyList.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Dim sideGap As Integer = TransactionSearchBox.Left
        _historyList.SetBounds(sideGap, TransactionSearchBox.Bottom + CInt(18 * _scaleK),
                               TransHistoryPnl.Width - 2 * sideGap, CInt(100 * _scaleK))
        _historyList.Painter = AddressOf PaintHistory
        AddHandler _historyList.RegionClicked, AddressOf HistoryList_RegionClicked
        TransHistoryPnl.Controls.Add(_historyList)
        _historyList.Visible = False
    End Sub

    Private Sub FillCombo(cb As ComboBox, items As String())
        cb.DropDownStyle = ComboBoxStyle.DropDownList
        cb.Items.Clear()
        For Each item As String In items
            cb.Items.Add(item)
        Next
        cb.SelectedIndex = 0
    End Sub

    ' ------------------------------------------------------------------
    ' Filters
    ' ------------------------------------------------------------------
    Private Sub HistoryFilterChanged(sender As Object, e As EventArgs)
        If _filtersLoading Then Return
        RefreshHistory()
    End Sub

    Private Sub FilterReset_Click(sender As Object, e As EventArgs)
        _filtersLoading = True
        TypeFilter.SelectedIndex = 0
        CategoryFilter.SelectedIndex = 0
        PaymentMethodFilter.SelectedIndex = 0
        TransactionSearchBox.Text = ""
        _filtersLoading = False
        RefreshHistory()
    End Sub

    Private Function TypeFilterValue() As String
        Select Case TypeFilter.SelectedIndex
            Case 1
                Return "income"
            Case 2
                Return "expense"
            Case Else
                Return ""
        End Select
    End Function

    Private Function ComboValue(cb As ComboBox) As String
        If cb.SelectedIndex <= 0 Then Return ""
        Return cb.SelectedItem.ToString()
    End Function

    ' ------------------------------------------------------------------
    ' Redraw (called from RefreshAll, and whenever a filter changes)
    ' ------------------------------------------------------------------
    Private Sub RefreshHistory()
        If _historyList Is Nothing OrElse _budget Is Nothing Then Return

        Dim month As Date = _budget.ViewMonth
        Dim total As Integer = _budget.TxnCountInMonth(month)
        _historyRows = _budget.FilterTxns(month, TransactionSearchBox.Text, TypeFilterValue(),
                                          ComboValue(CategoryFilter), ComboValue(PaymentMethodFilter))

        RecordsCount.Text = _historyRows.Count.ToString() & " of " & total.ToString() & " records"
        RecordsCount.Left = TransHistoryPnl.Width - TransactionSearchBox.Left - RecordsCount.GetPreferredSize(Size.Empty).Width

        Dim isEmpty As Boolean = (_historyRows.Count = 0)
        If total = 0 Then
            EmptyTransactionHeading.Text = "A fresh start"
            EmptyTransDesc.Text = "Add your first transaction to see this month take shape."
        Else
            EmptyTransactionHeading.Text = "No matching transactions"
            EmptyTransDesc.Text = "Try a different search, or press Reset Filters."
        End If
        EmptyTransactionHeading.Visible = isEmpty
        EmptyTransDesc.Visible = isEmpty
        AddTransButton.Visible = (total = 0)
        CenterInHistory(EmptyTransactionHeading)
        CenterInHistory(EmptyTransDesc)
        CenterInHistory(AddTransButton)

        _historyList.BackColor = TransHistoryPnl.BackColor
        _historyList.Visible = Not isEmpty
        If isEmpty Then
            TransHistoryPnl.Height = _historyBaseHeight
        Else
            _historyList.Height = HistoryHeaderHeight() + _historyRows.Count * HistoryRowHeight() + 1
            TransHistoryPnl.Height = _historyList.Bottom + CInt(28 * _scaleK)
        End If
        _historyList.Invalidate()

        ' Footer text and the scrolling area follow the card's new height.
        BottomLeftBGText.Top = TransHistoryPnl.Bottom + _historyFooterGap
        BottomRightBGText.Top = BottomLeftBGText.Top
        _stage.Height = BottomLeftBGText.Bottom + _stageExtra
        _canvas.Height = _stage.Height
    End Sub

    Private Sub CenterInHistory(c As Control)
        c.Left = (TransHistoryPnl.Width - c.GetPreferredSize(Size.Empty).Width) \ 2
    End Sub

    Private Function HistoryRowHeight() As Integer
        Return CInt(58 * _scaleK)
    End Function

    Private Function HistoryHeaderHeight() As Integer
        Return CInt(36 * _scaleK)
    End Function

    ' ------------------------------------------------------------------
    ' Drawing the table
    ' ------------------------------------------------------------------
    Private Sub PaintHistory(g As Graphics, s As Surface)
        Dim k As Single = _scaleK
        Dim w As Integer = s.Width
        Dim headH As Integer = HistoryHeaderHeight()
        Dim rowH As Integer = HistoryRowHeight()

        ' column positions, worked out from the right edge
        Dim btnH As Integer = CInt(30 * k)
        Dim delW As Integer = CInt(70 * k)
        Dim editW As Integer = CInt(58 * k)
        Dim delX As Integer = w - delW
        Dim editX As Integer = delX - CInt(8 * k) - editW
        Dim amountRight As Integer = editX - CInt(24 * k)
        Dim dateX As Integer = amountRight - CInt(260 * k)
        Dim payX As Integer = dateX - CInt(190 * k)
        Dim catX As Integer = payX - CInt(190 * k)
        Dim badge As Integer = CInt(36 * k)
        Dim titleX As Integer = badge + CInt(14 * k)

        Dim headFont As Font = Fnt(False, 8.0F, True)
        Dim headY As Integer = (headH - headFont.Height) \ 2
        Txt(g, "TRANSACTION", headFont, ColMuted, 0, headY)
        Txt(g, "CATEGORY", headFont, ColMuted, catX, headY)
        Txt(g, "PAYMENT", headFont, ColMuted, payX, headY)
        Txt(g, "DATE", headFont, ColMuted, dateX, headY)
        TxtRight(g, "AMOUNT", headFont, ColMuted, amountRight, headY)
        Using pen As New Pen(ColLine)
            g.DrawLine(pen, 0, headH - 1, w, headH - 1)
        End Using

        Dim titleFont As Font = Fnt(False, 10.0F, True)
        Dim subFont As Font = Fnt(False, 8.5F)
        Dim bodyFont As Font = Fnt(False, 9.5F)
        Dim amountFont As Font = Fnt(True, 10.0F, True)
        Dim btnFont As Font = Fnt(False, 8.5F, True)
        Dim left As TextFormatFlags = TextFormatFlags.Left

        For i As Integer = 0 To _historyRows.Count - 1
            Dim t As Txn = _historyRows(i)
            Dim y0 As Integer = headH + i * rowH
            Dim tint As Color = If(t.IsIncome, ColGreen, ColRed)

            ' badge
            Dim badgeRect As New Rectangle(0, y0 + (rowH - badge) \ 2, badge, badge)
            FillRound(g, badgeRect, 10, Color.FromArgb(46, tint))
            TxtIn(g, If(t.IsIncome, "IN", "EX"), Fnt(False, 8.0F, True), tint, badgeRect, TextFormatFlags.HorizontalCenter)

            ' title + wallet
            Dim titleW As Integer = Math.Max(catX - titleX - CInt(16 * k), 20)
            TxtIn(g, t.Title, titleFont, ColInk, New Rectangle(titleX, y0 + CInt(8 * k), titleW, CInt(24 * k)), left)
            TxtIn(g, _budget.WalletName(t.WalletId), subFont, ColMuted, New Rectangle(titleX, y0 + CInt(31 * k), titleW, CInt(20 * k)), left)

            ' category
            Dim catColor As Color = If(t.IsIncome, ColGreen, Catalog.TintOf(t.Category))
            Dim dot As Integer = CInt(9 * k)
            FillDot(g, catX, y0 + (rowH - dot) \ 2, dot, catColor)
            TxtIn(g, t.Category, bodyFont, ColInk, New Rectangle(catX + dot + CInt(8 * k), y0, Math.Max(payX - catX - dot - CInt(24 * k), 20), rowH), left)

            ' payment + wallet
            Dim payW As Integer = Math.Max(dateX - payX - CInt(16 * k), 20)
            TxtIn(g, t.Payment, bodyFont, ColInk, New Rectangle(payX, y0 + CInt(8 * k), payW, CInt(24 * k)), left)
            TxtIn(g, _budget.WalletName(t.WalletId), subFont, ColMuted, New Rectangle(payX, y0 + CInt(31 * k), payW, CInt(20 * k)), left)

            ' date
            TxtIn(g, DateTitle(t.AsDate), bodyFont, ColMuted, New Rectangle(dateX, y0, CInt(150 * k), rowH), left)

            ' amount
            Dim sign As String = If(t.IsIncome, "+", Glyphs.MinusSign)
            TxtIn(g, sign & Money(t.Amount), amountFont, tint,
                  New Rectangle(amountRight - CInt(170 * k), y0, CInt(170 * k), rowH), TextFormatFlags.Right)

            ' Edit / Delete
            Dim btnY As Integer = y0 + (rowH - btnH) \ 2
            Dim editRect As New Rectangle(editX, btnY, editW, btnH)
            Dim delRect As New Rectangle(delX, btnY, delW, btnH)
            FillRound(g, editRect, 8, ColRaised)
            StrokeRound(g, editRect, 8, ColLine)
            TxtIn(g, "Edit", btnFont, ColInk, editRect, TextFormatFlags.HorizontalCenter)
            FillRound(g, delRect, 8, Color.FromArgb(30, ColRed))
            StrokeRound(g, delRect, 8, Color.FromArgb(120, ColRed))
            TxtIn(g, "Delete", btnFont, ColRed, delRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(editRect, "edit:" & t.Id)
            s.AddRegion(delRect, "del:" & t.Id)

            ' row separator
            Using pen As New Pen(ColLineSoft)
                g.DrawLine(pen, 0, y0 + rowH - 1, w, y0 + rowH - 1)
            End Using
        Next
    End Sub

    ' ------------------------------------------------------------------
    ' Edit / Delete
    ' ------------------------------------------------------------------
    Private Sub HistoryList_RegionClicked(key As String)
        Dim sep As Integer = key.IndexOf(":"c)
        If sep < 0 Then Return
        Dim action As String = key.Substring(0, sep)
        Dim id As String = key.Substring(sep + 1)
        Dim t As Txn = _budget.Data.Transactions.FirstOrDefault(Function(x) x.Id = id)
        If t Is Nothing Then Return

        If action = "edit" Then
            EditTransaction(t)
        ElseIf action = "del" Then
            RemoveTransaction(t)
        End If
    End Sub

    Private Sub EditTransaction(t As Txn)
        Using dlg As New TransactionDialog(_budget.Data, t, t.Type)
            If dlg.ShowDialog(Me) <> DialogResult.OK OrElse dlg.Result Is Nothing Then Return
            _budget.UpdateTransaction(dlg.Result)
            _budget.GoToMonth(dlg.Result.AsDate)
        End Using
    End Sub

    Private Sub RemoveTransaction(t As Txn)
        Dim summary As String = """" & t.Title & """ (" & Money(t.Amount) & ")"

        If String.IsNullOrEmpty(t.RecurringRuleId) Then
            If MessageBox.Show(Me, "Delete " & summary & "?", "Delete transaction",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
            _budget.DeleteTransaction(t.Id)
        Else
            ' A repeating item would simply be re-created next time the app starts,
            ' so deleting it also stops the repeat.
            If MessageBox.Show(Me, summary & " repeats every month." & vbCrLf & vbCrLf &
                               "Delete it and stop it from repeating?", "Delete repeating transaction",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
            _budget.DeleteRule(t.RecurringRuleId)
            _budget.DeleteTransaction(t.Id)
        End If
    End Sub

End Class
