Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports System.ComponentModel

' Savings goals UI and dialogs. Goal balances are stored separately from transactions,
' so goal contributions do not inflate income or expenses.
Partial Class MainForm
    Private _goalGrid As FlowLayoutPanel
    Private _goalsReady As Boolean = False

    Private Sub InitSavingsGoals()
        If _goalsReady Then Return
        _goalsReady = True

        AddHandler AddNewGoal.Click, AddressOf AddGoal_Click
        AddHandler CreateFirstGoal.Click, AddressOf AddGoal_Click

        _goalGrid = New FlowLayoutPanel With {
            .Name = "SavingsGoalGrid",
            .Location = New Point(18, 64),
            .Size = New Size(SavingGoalsPnl.ClientSize.Width - 36, SavingGoalsPnl.ClientSize.Height - 76),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = True,
            .AutoScroll = True,
            .BackColor = Theme.ColSurface,
            .Padding = New Padding(0),
            .Margin = New Padding(0)
        }
        SavingGoalsPnl.Controls.Add(_goalGrid)
        _goalGrid.BringToFront()
    End Sub

    Private Sub RefreshSavingsGoals()
        If _budget Is Nothing OrElse _goalGrid Is Nothing Then Return

        _goalGrid.SuspendLayout()
        For Each old As Control In _goalGrid.Controls.Cast(Of Control)().ToList()
            old.Dispose()
        Next
        _goalGrid.Controls.Clear()

        Dim hasGoals As Boolean = _budget.Data.Goals.Count > 0
        EmptySavGoalsIcon.Visible = Not hasGoals
        EmptySavGoalsHeading.Visible = Not hasGoals
        EmptySavGoalsSubHeading.Visible = Not hasGoals
        CreateFirstGoal.Visible = Not hasGoals
        _goalGrid.Visible = hasGoals

        For Each goal As Goal In _budget.Data.Goals
            _goalGrid.Controls.Add(BuildGoalCard(goal))
        Next
        _goalGrid.ResumeLayout(True)
    End Sub

    Private Function BuildGoalCard(goal As Goal) As Control
        Dim card As New CardPanel With {
            .Name = "Goal_" & goal.Id,
            .Size = New Size(260, 154),
            .Radius = 12,
            .BackColor = Theme.ColSoft,
            .Margin = New Padding(0, 0, 12, 10),
            .Padding = New Padding(12)
        }

        Dim title As New Label With {
            .Text = goal.Name,
            .Location = New Point(12, 10),
            .Size = New Size(232, 22),
            .Font = Theme.Fnt(True, 9.5F, True),
            .ForeColor = Theme.ColInk,
            .BackColor = Color.Transparent,
            .AutoEllipsis = True
        }
        Dim saved As New Label With {
            .Text = Fmt.Money(goal.Saved) & " saved",
            .Location = New Point(12, 36),
            .Size = New Size(120, 21),
            .Font = Theme.Fnt(True, 10.0F, True),
            .ForeColor = Theme.ColInk,
            .BackColor = Color.Transparent
        }
        Dim target As New Label With {
            .Text = "of " & Fmt.Money(goal.Target),
            .Location = New Point(132, 38),
            .Size = New Size(112, 18),
            .TextAlign = ContentAlignment.MiddleRight,
            .Font = Theme.Fnt(False, 8.0F),
            .ForeColor = Theme.ColMuted,
            .BackColor = Color.Transparent
        }
        Dim pct As Decimal = Math.Min(100D, Math.Max(0D, goal.Saved / goal.Target * 100D))
        Dim percent As New Label With {
            .Text = Math.Round(pct, 0).ToString("0") & "% complete",
            .Location = New Point(12, 62),
            .Size = New Size(232, 17),
            .Font = Theme.Fnt(False, 8.0F, True),
            .ForeColor = Theme.ColMuted,
            .BackColor = Color.Transparent
        }
        Dim track As New Panel With {
            .Location = New Point(12, 83),
            .Size = New Size(232, 8),
            .BackColor = Theme.ColLine,
            .Name = "GoalTrack"
        }
        Dim fillWidth As Integer = CInt(track.Width * CDbl(pct) / 100.0R)
        Dim fill As New Panel With {
            .Location = Point.Empty,
            .Size = New Size(Math.Max(0, Math.Min(track.Width, fillWidth)), track.Height),
            .BackColor = Theme.ColGreen
        }
        track.Controls.Add(fill)

        Dim remaining As Decimal = Math.Max(0D, goal.Target - goal.Saved)
        Dim remainingLabel As New Label With {
            .Text = If(remaining = 0D, "Goal reached!", Fmt.Money(remaining) & " to go"),
            .Location = New Point(12, 96),
            .Size = New Size(124, 17),
            .Font = Theme.Fnt(False, 7.8F),
            .ForeColor = If(remaining = 0D, Theme.ColGreen, Theme.ColMuted),
            .BackColor = Color.Transparent,
            .AutoEllipsis = True
        }
        Dim deadlineText As String = If(Ids.IsValidDate(goal.Deadline), "Due " & Ids.ParseDate(goal.Deadline, Date.Today).ToString("MMM d, yyyy"), "No target date")
        Dim deadlineLabel As New Label With {
            .Text = deadlineText,
            .Location = New Point(138, 96),
            .Size = New Size(106, 17),
            .TextAlign = ContentAlignment.MiddleRight,
            .Font = Theme.Fnt(False, 7.3F),
            .ForeColor = Theme.ColMuted,
            .BackColor = Color.Transparent,
            .AutoEllipsis = True
        }

        Dim addButton As New Button With {
            .Text = "+ Add money",
            .Location = New Point(12, 119),
            .Size = New Size(108, 27),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Theme.ColGreenDeep,
            .ForeColor = Color.White,
            .Font = Theme.Fnt(False, 7.5F, True),
            .Cursor = Cursors.Hand
        }
        addButton.FlatAppearance.BorderSize = 0
        AddHandler addButton.Click, Sub() AdjustGoal_Click(goal.Id, goal.Name, False)

        Dim withdrawButton As New Button With {
            .Text = "Withdraw",
            .Location = New Point(126, 119),
            .Size = New Size(76, 27),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Theme.ColRaised,
            .ForeColor = Theme.ColInk,
            .Font = Theme.Fnt(False, 7.2F, True),
            .Cursor = Cursors.Hand,
            .Enabled = goal.Saved > 0D
        }
        withdrawButton.FlatAppearance.BorderSize = 0
        AddHandler withdrawButton.Click, Sub() AdjustGoal_Click(goal.Id, goal.Name, True)

        Dim menu As New ContextMenuStrip()
        Dim editItem As New ToolStripMenuItem("Edit goal")
        Dim historyItem As New ToolStripMenuItem("View activity")
        Dim deleteItem As New ToolStripMenuItem("Delete goal")
        AddHandler editItem.Click, Sub() EditGoal_Click(goal)
        AddHandler historyItem.Click, Sub() ViewGoalActivity(goal)
        AddHandler deleteItem.Click, Sub() DeleteGoal_Click(goal)
        menu.Items.Add(editItem)
        menu.Items.Add(historyItem)
        menu.Items.Add(deleteItem)
        card.ContextMenuStrip = menu

        card.Controls.AddRange(New Control() {title, saved, target, percent, track, remainingLabel, deadlineLabel, addButton, withdrawButton})
        For Each child As Control In card.Controls
            child.ContextMenuStrip = menu
        Next
        Return card
    End Function

    Private Sub AddGoal_Click(sender As Object, e As EventArgs)
        Using dlg As New GoalDialog()
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Result IsNot Nothing Then
                _budget.AddGoal(dlg.Result)
            End If
        End Using
    End Sub

    Private Sub EditGoal_Click(goal As Goal)
        Using dlg As New EditGoalDialog(goal)
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Result IsNot Nothing Then
                _budget.UpdateGoal(dlg.Result)
            End If
        End Using
    End Sub

    Private Sub AdjustGoal_Click(id As String, goalName As String, isWithdrawal As Boolean)
        Dim goal As Goal = _budget.Data.Goals.FirstOrDefault(Function(g) g.Id = id)
        If goal Is Nothing Then Return
        Using dlg As New GoalAmountDialog(goal, isWithdrawal)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Dim message As String = ""
                If Not _budget.AdjustGoal(id, dlg.Amount, isWithdrawal, message) Then
                    MessageBox.Show(Me, message, "Savings goal", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End Using
    End Sub

    Private Sub ViewGoalActivity(goal As Goal)
        Dim entries = _budget.Data.GoalActivities.
            Where(Function(a) a.GoalId = goal.Id).
            OrderByDescending(Function(a) a.Date).
            ToList()
        If entries.Count = 0 Then
            MessageBox.Show(Me, "No contributions or withdrawals have been recorded for this goal yet.",
                            "Goal activity · " & goal.Name, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim lines As New List(Of String)
        For Each entry In entries
            Dim verb As String = If(entry.Type = "withdrawal", "Withdrawal", "Contribution")
            Dim sign As String = If(entry.Type = "withdrawal", "−", "+")
            lines.Add(Ids.ParseDate(entry.Date, Date.Today).ToString("MMM d, yyyy") & "   " & verb & "   " & sign & Fmt.Money(entry.Amount))
        Next
        MessageBox.Show(Me, String.Join(Environment.NewLine, lines), "Goal activity · " & goal.Name,
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DeleteGoal_Click(goal As Goal)
        If MessageBox.Show(Me, "Delete the savings goal '" & goal.Name & "'? Its contribution history will also be removed.",
                           "Delete savings goal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        _budget.DeleteGoal(goal.Id)
    End Sub
End Class

' Edit an existing goal without allowing its target to be reduced below the saved amount.
<DesignerCategory("")>
Public Class EditGoalDialog
    Inherits DialogBase

    Private ReadOnly _existing As Goal
    Private ReadOnly _txtName As RoundedTextBox
    Private ReadOnly _txtTarget As RoundedTextBox
    Private ReadOnly _dtpDeadline As DateField
    Public Result As Goal

    Public Sub New(existing As Goal)
        MyBase.New("KEEP YOUR PLAN ON TRACK", "Edit savings goal", 540)
        _existing = existing
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim y As Integer = ContentTop

        _txtName = UI.MakeInput("Goal name", 80)
        _txtName.Value = existing.Name
        y = AddField("Goal name *", _txtName, Pad, y, innerW, 40)

        _txtTarget = UI.MakeInput("0.00", 14)
        _txtTarget.Value = existing.Target.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
        y = AddField("Target amount (₱) *", _txtTarget, Pad, y, innerW, 40)

        _dtpDeadline = UI.MakeDate(If(Ids.IsValidDate(existing.Deadline), CType(Ids.ParseDate(existing.Deadline, Date.Today), Date?), Nothing), True)
        y = AddField("Target date (optional)", _dtpDeadline, Pad, y, innerW, 40)
        FinishLayout(y, "Save changes")
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        If String.IsNullOrWhiteSpace(_txtName.Value) Then
            _txtName.HasError = True
            Fail("Enter a goal name.")
            Return False
        End If
        Dim target As Decimal
        If Not TryAmount(_txtTarget.Value, False, target) Then
            _txtTarget.HasError = True
            Fail("Enter a valid target amount above ₱0.")
            Return False
        End If
        If target < _existing.Saved Then
            _txtTarget.HasError = True
            Fail("The target cannot be lower than the amount already saved (" & Fmt.Money(_existing.Saved) & ").")
            Return False
        End If
        Dim updated As New Goal With {
            .Id = _existing.Id,
            .Name = _txtName.Value.Trim(),
            .Target = target,
            .Saved = _existing.Saved,
            .Deadline = If(_dtpDeadline.Value.HasValue, DateText(_dtpDeadline.Value.Value), "")
        }
        Result = updated
        Return True
    End Function
End Class

<DesignerCategory("")>
Public Class GoalAmountDialog
    Inherits DialogBase

    Private ReadOnly _goal As Goal
    Private ReadOnly _isWithdrawal As Boolean
    Private ReadOnly _txtAmount As RoundedTextBox
    Public Amount As Decimal

    Public Sub New(goal As Goal, isWithdrawal As Boolean)
        MyBase.New("SAVINGS GOAL", If(isWithdrawal, "Withdraw from goal", "Add to savings"), 500)
        _goal = goal
        _isWithdrawal = isWithdrawal
        Dim y As Integer = ContentTop
        Dim balanceLabel As New Label With {
            .Text = "Current savings: " & Fmt.Money(goal.Saved) & "    ·    Target: " & Fmt.Money(goal.Target),
            .AutoSize = False,
            .Location = New Point(Pad, y),
            .Size = New Size(ClientSize.Width - 2 * Pad, 24),
            .Font = Theme.Fnt(False, 8.5F, True),
            .ForeColor = Theme.ColMuted,
            .BackColor = Theme.ColSurface
        }
        Controls.Add(balanceLabel)
        y += 38
        _txtAmount = UI.MakeInput("0.00", 14)
        y = AddField("Amount (₱) *", _txtAmount, Pad, y, ClientSize.Width - 2 * Pad, 40)
        FinishLayout(y, If(isWithdrawal, "Withdraw", "Add money"))
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        Dim value As Decimal
        If Not TryAmount(_txtAmount.Value, False, value) Then
            _txtAmount.HasError = True
            Fail("Enter an amount above ₱0 with up to 2 decimals.")
            Return False
        End If
        If _isWithdrawal AndAlso value > _goal.Saved Then
            _txtAmount.HasError = True
            Fail("You can withdraw at most " & Fmt.Money(_goal.Saved) & ".")
            Return False
        End If
        Amount = value
        Return True
    End Function
End Class
