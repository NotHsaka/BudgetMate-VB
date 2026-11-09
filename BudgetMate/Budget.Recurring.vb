Imports System

Partial Public Class Budget

    Public Sub AddRecurringRule(rule As RecurringRule)
        If rule Is Nothing Then Return
        If String.IsNullOrWhiteSpace(rule.Id) Then rule.Id = NewId()
        rule.Title = If(rule.Title, "").Trim()
        rule.Day = Math.Min(Math.Max(rule.Day, 1), 31)
        If Not IsValidDate(rule.StartDate) Then rule.StartDate = DateText(Date.Today)
        rule.Active = True
        Data.RecurringRules.Add(rule)
        ApplyRecurring()
        Commit()
    End Sub

    Public Sub UpdateRecurringRule(updated As RecurringRule)
        If updated Is Nothing Then Return
        Dim idx As Integer = Data.RecurringRules.FindIndex(Function(r) r.Id = updated.Id)
        If idx < 0 Then Return
        updated.Day = Math.Min(Math.Max(updated.Day, 1), 31)
        If Not IsValidDate(updated.StartDate) Then updated.StartDate = DateText(Date.Today)
        Data.RecurringRules(idx) = updated
        ApplyRecurring()
        Commit()
    End Sub

End Class
