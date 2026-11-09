Imports System
Imports System.Drawing
Imports System.Windows.Forms

' =====================================================================
' PART 2 - month navigation (< >) and the four summary cards:
' Total income, Total expenses, Net balance, Savings rate.
' =====================================================================
Partial Class MainForm

    Private Sub PreviousMonth_Click(sender As Object, e As EventArgs) Handles PreviousMonth.Click
        _budget.ShiftMonth(-1)
    End Sub

    Private Sub NextMonth_Click(sender As Object, e As EventArgs) Handles NextMonth.Click
        _budget.ShiftMonth(1)
    End Sub

    Private Sub RefreshSummary()
        Dim totals As MonthTotals = _budget.TotalsFor(_budget.ViewMonth)

        MonthAndYear.Text = MonthTitle(_budget.ViewMonth)
        CenterMonthLabel()

        AmtIncome.Text = Money(totals.Income)
        ExpAmt.Text = Money(totals.Expenses)
        AmtBal.Text = Money(totals.Net)
        SavRatePerc.Text = totals.SavingsRate.ToString() & "%"
    End Sub

    ' Keeps "November 2026" centered between the two arrow buttons as the text changes width.
    Private Sub CenterMonthLabel()
        Dim w As Integer = MonthAndYear.GetPreferredSize(Size.Empty).Width
        Dim gapLeft As Integer = PreviousMonth.Right
        Dim gapRight As Integer = NextMonth.Left
        MonthAndYear.Left = gapLeft + (gapRight - gapLeft - w) \ 2
    End Sub

End Class
