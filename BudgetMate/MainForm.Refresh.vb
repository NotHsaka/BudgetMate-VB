Imports System
Imports System.Windows.Forms

' =====================================================================
' The one place that redraws the whole dashboard from the data.
' Each part of the build adds ONE line to RefreshAll (marked PART n).
' =====================================================================
Partial Class MainForm

    Private Sub RefreshAll()
        If _budget Is Nothing Then Return

        RefreshSummary()          ' PART 2
        RefreshWallets()
        RefreshSavingsGoals()
        ' PART 3 (Add transaction) needs no redraw line: it only saves, and saving calls this.
        RefreshHistory()          ' PART 4
        RefreshBreakdown()        ' PART 5 (expense breakdown donut + legend)
        RefreshCashFlow()         ' PART 6 (cash flow bar chart)
        RefreshLimits()           ' PART 7 (monthly budget limits + Manage Limits)
        ' PART 5 and later add their lines here.

        If _budget.SaveFailed Then
            ' Shown only when Windows refused the save (e.g. disk full or folder locked).
            _budget.SaveFailed = False
            MessageBox.Show(Me, "BudgetMate couldn't save your changes to disk.", "BudgetMate",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        RefreshRecurring()          ' Recurring transactions
    End Sub

End Class
