Imports System
Imports System.Collections.Generic
Imports System.Linq

' =====================================================================
' PART 1 - the "brain" of the app. No buttons or drawing in here:
' it holds the data, does the math (totals, balances, limits) and saves.
' Every screen part (summary, history, charts...) just asks this class.
' =====================================================================

Public Class MonthTotals
    Public Income As Decimal = 0D
    Public Expenses As Decimal = 0D

    Public ReadOnly Property Net As Decimal
        Get
            Return Income - Expenses
        End Get
    End Property

    ' Share of income kept, 0-100 (rounded, never below 0).
    Public ReadOnly Property SavingsRate As Integer
        Get
            If Income <= 0D Then Return 0
            Dim rate As Decimal = Net / Income * 100D
            Return CInt(Math.Max(0D, Math.Min(100D, Math.Round(rate, MidpointRounding.AwayFromZero))))
        End Get
    End Property
End Class

Public Class MonthPoint
    Public Month As Date
    Public Income As Decimal = 0D
    Public Expenses As Decimal = 0D
End Class

Public Class Budget

    Public Data As AppData
    Public ViewMonth As Date
    Public LoadNotice As String = ""
    Public SaveFailed As Boolean = False

    ' Raised after anything changes so the screen can redraw itself.
    Public Event Changed()

    Public Sub New()
        Dim notice As String = ""
        Data = DataStore.Load(notice)
        LoadNotice = notice
        Dim today As Date = Date.Today
        ViewMonth = New Date(today.Year, today.Month, 1)
        Theme.Apply(Data.DarkTheme)
        ApplyRecurring()
    End Sub

    ' ------------------------------------------------------------------
    ' Saving
    ' ------------------------------------------------------------------
    Private Sub Persist()
        SaveFailed = Not DataStore.Save(Data)
    End Sub

    Private Sub Commit()
        Persist()
        RaiseEvent Changed()
    End Sub

    ' ------------------------------------------------------------------
    ' Month navigation (does not touch saved data)
    ' ------------------------------------------------------------------
    Public Sub ShiftMonth(delta As Integer)
        ViewMonth = ViewMonth.AddMonths(delta)
        RaiseEvent Changed()
    End Sub

    ' Jumps the view to the month containing d (used after adding a transaction dated in another month).
    Public Sub GoToMonth(d As Date)
        Dim target As New Date(d.Year, d.Month, 1)
        If target = ViewMonth Then Return
        ViewMonth = target
        RaiseEvent Changed()
    End Sub

    ' ------------------------------------------------------------------
    ' Reading / calculating
    ' ------------------------------------------------------------------
    Public Function TotalsFor(month As Date) As MonthTotals
        Dim key As String = MonthKey(month)
        Dim result As New MonthTotals()
        For Each t As Txn In Data.Transactions
            If t.YearMonth <> key Then Continue For
            If t.IsIncome Then
                result.Income += t.Amount
            Else
                result.Expenses += t.Amount
            End If
        Next
        Return result
    End Function

    ' Expense total per category for one month (only categories with spending are present).
    Public Function SpentByCategory(month As Date) As Dictionary(Of String, Decimal)
        Dim key As String = MonthKey(month)
        Dim result As New Dictionary(Of String, Decimal)()
        For Each t As Txn In Data.Transactions
            If t.YearMonth <> key OrElse t.IsIncome Then Continue For
            Dim sofar As Decimal = 0D
            result.TryGetValue(t.Category, sofar)
            result(t.Category) = sofar + t.Amount
        Next
        Return result
    End Function

    Public Function LimitFor(categoryName As String) As Decimal
        Dim v As Decimal = 0D
        Data.Limits.TryGetValue(categoryName, v)
        Return v
    End Function

    ' The last "count" months, ending with the month being viewed (oldest first).
    Public Function CashFlow(count As Integer) As List(Of MonthPoint)
        Dim points As New List(Of MonthPoint)()
        For i As Integer = count - 1 To 0 Step -1
            Dim p As New MonthPoint()
            p.Month = ViewMonth.AddMonths(-i)
            Dim totals As MonthTotals = TotalsFor(p.Month)
            p.Income = totals.Income
            p.Expenses = totals.Expenses
            points.Add(p)
        Next
        Return points
    End Function

    ' Transactions of one month, newest first. Pass "" for any filter you don't want.
    ' type = "income" / "expense" / "";  category and payment match exactly.
    Public Function FilterTxns(month As Date, search As String, type As String,
                               category As String, payment As String) As List(Of Txn)
        Dim key As String = MonthKey(month)
        Dim needle As String = If(search, "").Trim()
        Dim list As New List(Of Txn)()
        For Each t As Txn In Data.Transactions
            If t.YearMonth <> key Then Continue For
            If type.Length > 0 AndAlso t.Type <> type Then Continue For
            If category.Length > 0 AndAlso t.Category <> category Then Continue For
            If payment.Length > 0 AndAlso t.Payment <> payment Then Continue For
            If needle.Length > 0 Then
                Dim hay As String = t.Title & " " & t.Notes & " " & t.Category & " " & t.Payment & " " & WalletName(t.WalletId)
                If hay.IndexOf(needle, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            End If
            list.Add(t)
        Next
        Return list.OrderByDescending(Function(x) x.Date).ToList()
    End Function

    Public Function TxnCountInMonth(month As Date) As Integer
        Dim key As String = MonthKey(month)
        Return System.Linq.Enumerable.Count(Data.Transactions, Function(t) t.YearMonth = key)
    End Function

    ' ------------------------------------------------------------------
    ' Wallets
    ' ------------------------------------------------------------------
    Public Function FindWallet(id As String) As Wallet
        Return Data.Wallets.FirstOrDefault(Function(w) w.Id = id)
    End Function

    Public Function WalletName(id As String) As String
        Dim w As Wallet = FindWallet(id)
        Return If(w Is Nothing, "Unknown account", w.Name)
    End Function

    ' Starting balance + all income - all expenses + transfers in - transfers out (all time).
    Public Function WalletBalance(w As Wallet) As Decimal
        Dim bal As Decimal = w.OpeningBalance
        For Each t As Txn In Data.Transactions
            If t.WalletId <> w.Id Then Continue For
            If t.IsIncome Then
                bal += t.Amount
            Else
                bal -= t.Amount
            End If
        Next
        For Each tr As Transfer In Data.Transfers
            If tr.ToId = w.Id Then bal += tr.Amount
            If tr.FromId = w.Id Then bal -= tr.Amount
        Next
        Return bal
    End Function

    Public Function AddWallet(w As Wallet) As Boolean
        Data.Wallets.Add(w)
        Commit()
        Return True
    End Function

    ' A wallet that has history can't be removed (it would break old records).
    Public Function CanDeleteWallet(id As String, ByRef reason As String) As Boolean
        reason = ""
        If Data.Wallets.Count <= 1 Then
            reason = "You need at least one wallet or account."
            Return False
        End If
        If Data.Transactions.Any(Function(t) t.WalletId = id) OrElse
           Data.Transfers.Any(Function(t) t.FromId = id OrElse t.ToId = id) OrElse
           Data.RecurringRules.Any(Function(r) r.WalletId = id) Then
            reason = "This account has transactions, transfers or repeating items. Delete or move those first."
            Return False
        End If
        Return True
    End Function

    Public Sub DeleteWallet(id As String)
        Data.Wallets.RemoveAll(Function(w) w.Id = id)
        Commit()
    End Sub

    Public Sub AddTransfer(t As Transfer)
        Data.Transfers.Add(t)
        Commit()
    End Sub

    ' ------------------------------------------------------------------
    ' Transactions
    ' ------------------------------------------------------------------
    Public Sub AddTransaction(t As Txn, repeatMonthly As Boolean)
        If repeatMonthly Then
            Dim rule As New RecurringRule()
            rule.Title = t.Title
            rule.Type = t.Type
            rule.Amount = t.Amount
            rule.Category = t.Category
            rule.Payment = t.Payment
            rule.WalletId = t.WalletId
            rule.Notes = t.Notes
            rule.Day = t.AsDate.Day
            rule.StartDate = t.Date
            rule.Active = True
            Data.RecurringRules.Add(rule)
            t.RecurringRuleId = rule.Id
        End If
        Data.Transactions.Add(t)
        Commit()
    End Sub

    Public Sub UpdateTransaction(updated As Txn)
        Dim idx As Integer = Data.Transactions.FindIndex(Function(t) t.Id = updated.Id)
        If idx >= 0 Then Data.Transactions(idx) = updated
        Commit()
    End Sub

    Public Sub DeleteTransaction(id As String)
        Data.Transactions.RemoveAll(Function(t) t.Id = id)
        Commit()
    End Sub

    ' ------------------------------------------------------------------
    ' Budget limits
    ' ------------------------------------------------------------------
    Public Sub SetLimits(limits As Dictionary(Of String, Decimal))
        For Each pair As KeyValuePair(Of String, Decimal) In limits
            Data.Limits(pair.Key) = pair.Value
        Next
        Commit()
    End Sub

    ' ------------------------------------------------------------------
    ' Savings goals
    ' ------------------------------------------------------------------
    Public Sub AddGoal(g As Goal)
        Data.Goals.Add(g)
        Commit()
    End Sub

    Public Sub UpdateGoal(updated As Goal)
        Dim index As Integer = Data.Goals.FindIndex(Function(g) g.Id = updated.Id)
        If index < 0 Then Return
        Data.Goals(index) = updated
        Commit()
    End Sub

    ' Contributions and withdrawals are tracked separately from income/expenses.
    Public Function AdjustGoal(id As String, amount As Decimal, isWithdrawal As Boolean, ByRef reason As String) As Boolean
        reason = ""
        Dim goal As Goal = Data.Goals.FirstOrDefault(Function(g) g.Id = id)
        If goal Is Nothing Then
            reason = "That savings goal no longer exists."
            Return False
        End If
        If amount <= 0D Then
            reason = "Enter an amount greater than zero."
            Return False
        End If
        If isWithdrawal AndAlso amount > goal.Saved Then
            reason = "The withdrawal cannot exceed the amount saved."
            Return False
        End If

        If isWithdrawal Then
            goal.Saved -= amount
        Else
            goal.Saved += amount
        End If
        Data.GoalActivities.Add(New GoalActivity With {
            .GoalId = id,
            .Type = If(isWithdrawal, "withdrawal", "contribution"),
            .Amount = amount,
            .Date = DateText(Date.Today)
        })
        Commit()
        Return True
    End Function

    Public Sub AddToGoal(id As String, amount As Decimal)
        Dim ignored As String = ""
        AdjustGoal(id, amount, False, ignored)
    End Sub

    Public Sub DeleteGoal(id As String)
        Data.Goals.RemoveAll(Function(g) g.Id = id)
        Data.GoalActivities.RemoveAll(Function(a) a.GoalId = id)
        Commit()
    End Sub

    ' ------------------------------------------------------------------
    ' Repeating (monthly) items
    ' ------------------------------------------------------------------
    ' Creates this month's (and any missed earlier months') copy of each active rule
    ' once its day has arrived. Safe to call any time; it never makes duplicates.
    ' Returns how many transactions were created.
    Public Function ApplyRecurring() As Integer
        Dim added As Integer = 0
        Dim today As Date = Date.Today
        Dim lastMonth As New Date(today.Year, today.Month, 1)

        For Each rule As RecurringRule In Data.RecurringRules.ToList()
            If Not rule.Active Then Continue For
            Dim ruleId As String = rule.Id
            Dim start As Date = ParseDate(rule.StartDate, today)
            Dim m As New Date(start.Year, start.Month, 1)

            Do While m <= lastMonth
                Dim day As Integer = Math.Min(rule.Day, Date.DaysInMonth(m.Year, m.Month))
                Dim due As New Date(m.Year, m.Month, day)
                Dim key As String = MonthKey(m)
                If due >= start AndAlso due <= today Then
                    If Not Data.Transactions.Any(Function(t) t.RecurringRuleId = ruleId AndAlso t.YearMonth = key) Then
                        Dim made As New Txn()
                        made.Type = rule.Type
                        made.Title = rule.Title
                        made.Amount = rule.Amount
                        made.Date = DateText(due)
                        made.Category = rule.Category
                        made.Payment = rule.Payment
                        made.WalletId = rule.WalletId
                        made.Notes = rule.Notes
                        made.RecurringRuleId = ruleId
                        Data.Transactions.Add(made)
                        added += 1
                    End If
                End If
                m = m.AddMonths(1)
            Loop
        Next

        If added > 0 Then Persist()
        Return added
    End Function

    Public Sub SetRuleActive(id As String, active As Boolean)
        Dim r As RecurringRule = Data.RecurringRules.FirstOrDefault(Function(x) x.Id = id)
        If r IsNot Nothing Then r.Active = active
        If active Then ApplyRecurring()
        Commit()
    End Sub

    Public Sub DeleteRule(id As String)
        Data.RecurringRules.RemoveAll(Function(r) r.Id = id)
        Commit()
    End Sub

    ' ------------------------------------------------------------------
    ' Theme, import, reset
    ' ------------------------------------------------------------------
    Public Sub SetDarkTheme(dark As Boolean)
        Data.DarkTheme = dark
        Theme.Apply(dark)
        Commit()
    End Sub

    ' Replaces everything with the given data (used by Import and Reset).
    Public Sub ReplaceAll(newData As AppData)
        newData.DarkTheme = Data.DarkTheme
        Data = newData
        Commit()
    End Sub

    Public Sub ResetAll()
        ReplaceAll(AppData.CreateDefault())
    End Sub

End Class
