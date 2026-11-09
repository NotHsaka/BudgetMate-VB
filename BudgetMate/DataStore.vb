Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.Json

' Saves and loads everything as JSON in %AppData%\BudgetMate\budgetmate-data.json.
Public NotInheritable Class DataStore

    Private Sub New()
    End Sub

    Private Shared ReadOnly FolderPath As String =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BudgetMate")
    Private Shared ReadOnly FilePath As String = Path.Combine(FolderPath, "budgetmate-data.json")
    Private Shared ReadOnly JsonOptions As New JsonSerializerOptions With {
        .WriteIndented = True,
        .PropertyNameCaseInsensitive = True,
        .PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }

    Public Shared Function ToJson(data As AppData) As String
        Return JsonSerializer.Serialize(data, JsonOptions)
    End Function

    ' Throws JsonException if the text is not valid backup data.
    Public Shared Function FromJson(text As String) As AppData
        Dim data As AppData = JsonSerializer.Deserialize(Of AppData)(text, JsonOptions)
        If data Is Nothing Then Throw New JsonException("The file was empty.")
        Return Clean(data)
    End Function

    Public Shared Function Load(ByRef notice As String) As AppData
        notice = ""
        If Not File.Exists(FilePath) Then Return AppData.CreateDefault()

        Try
            Return FromJson(File.ReadAllText(FilePath))
        Catch ex As JsonException
            BackupBrokenFile()
            notice = "Your saved data was unreadable, so BudgetMate started fresh. The old file was kept as budgetmate-data.broken.json."
            Return AppData.CreateDefault()
        Catch ex As IOException
            notice = "BudgetMate couldn't read your saved data, so it started fresh."
            Return AppData.CreateDefault()
        Catch ex As UnauthorizedAccessException
            notice = "BudgetMate isn't allowed to read your saved data, so it started fresh."
            Return AppData.CreateDefault()
        End Try
    End Function

    Public Shared Function Save(data As AppData) As Boolean
        Try
            Directory.CreateDirectory(FolderPath)
            Dim tempPath As String = FilePath & ".tmp"
            File.WriteAllText(tempPath, ToJson(data))
            File.Move(tempPath, FilePath, True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Sub BackupBrokenFile()
        Try
            File.Copy(FilePath, Path.Combine(FolderPath, "budgetmate-data.broken.json"), True)
        Catch ex As Exception
            ' A missing backup should not stop the app from starting.
        End Try
    End Sub

    ' Drops anything invalid so a hand-edited file can't crash the app.
    Public Shared Function Clean(data As AppData) As AppData
        Dim fresh As AppData = AppData.CreateDefault()
        Dim result As New AppData()
        result.DarkTheme = data.DarkTheme

        For Each pair In fresh.Limits
            result.Limits(pair.Key) = pair.Value
        Next
        If data.Limits IsNot Nothing Then
            For Each pair In data.Limits
                If pair.Value >= 0D Then result.Limits(pair.Key) = pair.Value
            Next
        End If

        If data.Wallets IsNot Nothing Then
            For Each w In data.Wallets
                If w Is Nothing OrElse String.IsNullOrWhiteSpace(w.Id) Then Continue For
                w.Name = If(w.Name, "")
                w.Provider = If(w.Provider, "Other")
                w.Kind = If(w.Kind, "other")
                If w.Hex Is Nothing Then w.Hex = "#4d91ff"
                result.Wallets.Add(w)
            Next
        End If
        If result.Wallets.Count = 0 Then result.Wallets = fresh.Wallets

        If data.Transactions IsNot Nothing Then
            For Each t In data.Transactions
                If t Is Nothing OrElse t.Amount <= 0D Then Continue For
                t.Type = If(t.Type, "").ToLowerInvariant()
                If t.Type <> "income" AndAlso t.Type <> "expense" Then Continue For
                If Not IsValidDate(t.Date) Then Continue For
                If String.IsNullOrWhiteSpace(t.Id) Then t.Id = NewId()
                t.Title = If(t.Title, "")
                t.Category = If(String.IsNullOrWhiteSpace(t.Category), "Miscellaneous", t.Category)
                t.Payment = If(String.IsNullOrWhiteSpace(t.Payment), "Cash", t.Payment)
                t.WalletId = If(String.IsNullOrWhiteSpace(t.WalletId), result.Wallets(0).Id, t.WalletId)
                t.Notes = If(t.Notes, "")
                result.Transactions.Add(t)
            Next
        End If

        If data.Goals IsNot Nothing Then
            For Each g In data.Goals
                If g Is Nothing OrElse g.Target <= 0D Then Continue For
                If String.IsNullOrWhiteSpace(g.Id) Then g.Id = NewId()
                g.Name = If(g.Name, "")
                g.Saved = Math.Max(0D, g.Saved)
                g.Deadline = If(IsValidDate(g.Deadline), g.Deadline, "")
                result.Goals.Add(g)
            Next
        End If

        If data.GoalActivities IsNot Nothing Then
            For Each activity In data.GoalActivities
                If activity Is Nothing OrElse activity.Amount <= 0D Then Continue For
                If String.IsNullOrWhiteSpace(activity.GoalId) OrElse Not result.Goals.Any(Function(g) g.Id = activity.GoalId) Then Continue For
                activity.Type = If(activity.Type, "contribution").ToLowerInvariant()
                If activity.Type <> "contribution" AndAlso activity.Type <> "withdrawal" Then Continue For
                If Not IsValidDate(activity.Date) Then activity.Date = DateText(Date.Today)
                If String.IsNullOrWhiteSpace(activity.Id) Then activity.Id = NewId()
                result.GoalActivities.Add(activity)
            Next
        End If

        If data.RecurringRules IsNot Nothing Then
            For Each r In data.RecurringRules
                If r Is Nothing OrElse r.Amount <= 0D Then Continue For
                If String.IsNullOrWhiteSpace(r.Id) Then r.Id = NewId()
                r.Title = If(r.Title, "")
                r.Day = Math.Min(Math.Max(r.Day, 1), 31)
                If Not IsValidDate(r.StartDate) Then r.StartDate = DateText(Date.Today)
                result.RecurringRules.Add(r)
            Next
        End If

        If data.Transfers IsNot Nothing Then
            For Each t In data.Transfers
                If t Is Nothing OrElse t.Amount <= 0D Then Continue For
                If String.IsNullOrWhiteSpace(t.Id) Then t.Id = NewId()
                t.Note = If(t.Note, "")
                result.Transfers.Add(t)
            Next
        End If

        Return result
    End Function

End Class
