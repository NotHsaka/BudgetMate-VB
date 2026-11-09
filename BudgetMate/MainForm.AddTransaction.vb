Imports System
Imports System.Windows.Forms

' =====================================================================
' PART 3 - adding a transaction.
' Both "+ Add Transaction" buttons (top bar, and the one in the empty
' history card) open the same pop-up. On Save the data is stored by
' Budget.AddTransaction, which saves to disk and redraws the screen.
' =====================================================================
Partial Class MainForm

    Private Sub AddTransaction_Click(sender As Object, e As EventArgs) Handles AddTransaction.Click
        OpenAddTransaction()
    End Sub

    Private Sub AddTransButton_Click(sender As Object, e As EventArgs) Handles AddTransButton.Click
        OpenAddTransaction()
    End Sub

    Private Sub OpenAddTransaction()
        If _budget Is Nothing Then Return

        Using dlg As New TransactionDialog(_budget.Data, Nothing, "expense")
            If dlg.ShowDialog(Me) <> DialogResult.OK OrElse dlg.Result Is Nothing Then Return

            _budget.AddTransaction(dlg.Result, dlg.RepeatMonthly)

            ' If the date is in another month, show that month so the new entry is visible.
            _budget.GoToMonth(dlg.Result.AsDate)
        End Using
    End Sub

End Class
