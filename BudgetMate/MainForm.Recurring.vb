Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Partial Class MainForm

    Private _recurringHost As FlowLayoutPanel
    Private _recurringAddButton As RoundButton
    Private _recurringTimer As System.Windows.Forms.Timer
    Private _recurringBuilt As Boolean = False

    Private Sub RefreshRecurring()
        If _budget Is Nothing Then Return
        EnsureRecurringControls()

        Dim rules = _budget.Data.RecurringRules.OrderBy(Function(r) Not r.Active).
                    ThenBy(Function(r) r.Day).ThenBy(Function(r) r.Title).ToList()
        Dim activeCount As Integer = rules.Where(Function(r) r.Active).Count()
        ActiveCounter.Text = activeCount.ToString() & " active"
        EmptyRecTransDesc.Visible = (rules.Count = 0)
        _recurringHost.Visible = (rules.Count > 0)
        _recurringHost.SuspendLayout()
        _recurringHost.Controls.Clear()

        For Each item As RecurringRule In rules
            Dim rule As RecurringRule = item
            _recurringHost.Controls.Add(BuildRecurringRow(rule))
        Next

        _recurringHost.ResumeLayout(True)
    End Sub

    Private Sub EnsureRecurringControls()
        If _recurringBuilt Then Return
        _recurringBuilt = True

        ' Keep monthly entries current even if BudgetMate stays open across a due date.
        _recurringTimer = New System.Windows.Forms.Timer()
        _recurringTimer.Interval = 60000
        AddHandler _recurringTimer.Tick, AddressOf RecurringTimer_Tick
        _recurringTimer.Start()
        AddHandler Me.FormClosed, AddressOf StopRecurringTimer

        _recurringAddButton = UI.MakeButton("+ Add recurring", BtnKind.Primary, 150, 36)
        _recurringAddButton.Location = New Point(RecTransPnl.Width - 174, 14)
        AddHandler _recurringAddButton.Click, AddressOf AddRecurringClicked
        RecTransPnl.Controls.Add(_recurringAddButton)
        _recurringAddButton.BringToFront()

        ActiveCounter.Left = RecTransPnl.Width - 270
        ActiveCounter.Top = 23

        _recurringHost = New FlowLayoutPanel()
        _recurringHost.Location = New Point(20, 88)
        _recurringHost.Size = New Size(RecTransPnl.Width - 40, 54)
        _recurringHost.FlowDirection = FlowDirection.TopDown
        _recurringHost.WrapContents = False
        _recurringHost.AutoScroll = True
        _recurringHost.BorderStyle = BorderStyle.None
        _recurringHost.BackColor = RecTransPnl.BackColor
        _recurringHost.Padding = New Padding(0)
        _recurringHost.Margin = New Padding(0)
        RecTransPnl.Controls.Add(_recurringHost)
        _recurringHost.BringToFront()
        _recurringAddButton.BringToFront()
    End Sub

    Private Function BuildRecurringRow(rule As RecurringRule) As Control
        Dim row As New Panel()
        row.Size = New Size(Math.Max(400, _recurringHost.ClientSize.Width - 8), 42)
        row.Margin = New Padding(0, 0, 0, 5)
        row.BackColor = ColSoft

        Dim title As New Label()
        title.AutoEllipsis = True
        title.Text = rule.Title
        title.Font = Fnt(False, 9.5F, True)
        title.ForeColor = If(rule.Active, ColInk, ColMuted)
        title.Location = New Point(12, 4)
        title.Size = New Size(250, 18)
        title.BackColor = row.BackColor
        row.Controls.Add(title)

        Dim details As New Label()
        Dim wallet As Wallet = _budget.Data.Wallets.FirstOrDefault(Function(w) w.Id = rule.WalletId)
        Dim walletName As String = If(wallet Is Nothing, "Account unavailable", wallet.Name)
        details.Text = If(rule.Type = "income", "Income", "Expense") & "  ·  " & rule.Category & "  ·  " & walletName & "  ·  Day " & rule.Day.ToString()
        details.Font = Fnt(False, 8.0F)
        details.ForeColor = ColMuted
        details.Location = New Point(12, 22)
        details.Size = New Size(610, 16)
        details.BackColor = row.BackColor
        row.Controls.Add(details)

        Dim amount As New Label()
        amount.Text = Fmt.Money(rule.Amount) & " / month"
        amount.Font = Fnt(False, 9.0F, True)
        amount.ForeColor = If(rule.Type = "income", ColGreen, ColRed)
        amount.TextAlign = ContentAlignment.MiddleRight
        amount.Location = New Point(650, 7)
        amount.Size = New Size(190, 26)
        amount.BackColor = row.BackColor
        row.Controls.Add(amount)

        Dim edit As RoundButton = UI.MakeButton("Edit", BtnKind.Secondary, 72, 30)
        edit.Location = New Point(row.Width - 300, 6)
        AddHandler edit.Click, Sub() EditRecurring(rule.Id)
        row.Controls.Add(edit)

        Dim toggle As RoundButton = UI.MakeButton(If(rule.Active, "Pause", "Resume"), BtnKind.Secondary, 82, 30)
        toggle.Location = New Point(row.Width - 218, 6)
        AddHandler toggle.Click, Sub() ToggleRecurring(rule.Id, Not rule.Active)
        row.Controls.Add(toggle)

        Dim delete As RoundButton = UI.MakeButton("Delete", BtnKind.Danger, 78, 30)
        delete.Location = New Point(row.Width - 130, 6)
        AddHandler delete.Click, Sub() DeleteRecurring(rule.Id, rule.Title)
        row.Controls.Add(delete)

        Return row
    End Function

    Private Sub RecurringTimer_Tick(sender As Object, e As EventArgs)
        If _budget Is Nothing Then Return
        If _budget.ApplyRecurring() > 0 Then RefreshAll()
    End Sub

    Private Sub StopRecurringTimer(sender As Object, e As FormClosedEventArgs)
        If _recurringTimer IsNot Nothing Then
            _recurringTimer.Stop()
            _recurringTimer.Dispose()
            _recurringTimer = Nothing
        End If
    End Sub

    Private Sub AddRecurringClicked(sender As Object, e As EventArgs)
        Using dlg As New RecurringRuleDialog(_budget.Data, Nothing)
            If dlg.ShowDialog(Me) = DialogResult.OK Then _budget.AddRecurringRule(dlg.Result)
        End Using
    End Sub

    Private Sub EditRecurring(id As String)
        Dim rule As RecurringRule = _budget.Data.RecurringRules.FirstOrDefault(Function(r) r.Id = id)
        If rule Is Nothing Then Return
        Using dlg As New RecurringRuleDialog(_budget.Data, rule)
            If dlg.ShowDialog(Me) = DialogResult.OK Then _budget.UpdateRecurringRule(dlg.Result)
        End Using
    End Sub

    Private Sub ToggleRecurring(id As String, active As Boolean)
        If active Then
            ' Resume from today rather than generating transactions for every month it was paused.
            Dim rule As RecurringRule = _budget.Data.RecurringRules.FirstOrDefault(Function(r) r.Id = id)
            If rule IsNot Nothing Then rule.StartDate = DateText(Date.Today)
        End If
        _budget.SetRuleActive(id, active)
    End Sub

    Private Sub DeleteRecurring(id As String, title As String)
        Dim answer As DialogResult = MessageBox.Show(Me,
            "Delete the monthly rule for '" & title & "'? Existing transactions will remain in your history.",
            "Delete recurring rule", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer = DialogResult.Yes Then _budget.DeleteRule(id)
    End Sub

End Class
