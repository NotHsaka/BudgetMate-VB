Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class MainForm

    ' _canvas = full-width strip inside PnlContent (the only thing that scrolls)
    ' _stage  = fixed-width block holding every card; it is kept horizontally centered in _canvas
    Private _canvas As Panel
    Private _stage As Panel

    Public Sub New()
        InitializeComponent()
    End Sub

    ' PART 1: the Budget object owns all the data and math; every screen part reads from it.
    Private _budget As Budget

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildCenteredLayout()
        InitWallets()
        InitHistory()             ' PART 4

        _budget = New Budget()
        InitSavingsGoals()
        AddHandler _budget.Changed, AddressOf RefreshAll
        RefreshAll()
        ApplyCurrentTheme()

        If _budget.LoadNotice.Length > 0 Then
            MessageBox.Show(Me, _budget.LoadNotice, "BudgetMate", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ------------------------------------------------------------------
    ' Centering
    ' ------------------------------------------------------------------
    Private Sub BuildCenteredLayout()
        If _stage IsNot Nothing Then Return

        ' Everything in the designer was placed at fixed pixel positions, so we move all of it
        ' into one block, tidy the edges, and then center that block.
        Dim items As New List(Of Control)
        For Each c As Control In PnlContent.Controls
            items.Add(c)
        Next

        ' Scale factor in case the PC runs at a different DPI than the designer (120 dpi)
        Dim k As Single = CSng(CurrentAutoScaleDimensions.Width / AutoScaleDimensions.Width)

        Dim contentLeft As Integer = Integer.MaxValue
        Dim contentTop As Integer = Integer.MaxValue
        For Each c As Control In items
            contentLeft = Math.Min(contentLeft, c.Left)
            contentTop = Math.Min(contentTop, c.Top)
        Next
        ' Right edge of the grid = right edge of the 4th stat card
        Dim contentRight As Integer = SavRateBG.Right

        ' Line up the right edges so every row ends at the same place
        For Each p As Control In New Control() {MBLPnl, RecTransPnl, TransHistoryPnl}
            p.SetBounds(contentLeft, p.Top, contentRight - contentLeft, p.Height)
        Next
        SavingGoalsPnl.Width = contentRight - SavingGoalsPnl.Left
        Dim navShift As Integer = contentRight - MonthNavContainer.Right
        MonthNavContainer.Left += navShift
        ViewMonth.Left += navShift
        BottomRightBGText.Left = contentRight - BottomRightBGText.Width

        ' Pull the content up to a small top margin (it was offset by the old header overlap)
        Dim topMargin As Integer = CInt(26 * k)
        Dim shiftUp As Integer = contentTop - topMargin
        Dim contentBottom As Integer = 0
        For Each c As Control In items
            c.Top -= shiftUp
            contentBottom = Math.Max(contentBottom, c.Bottom)
        Next

        _stage = New Panel()
        _stage.BackColor = PnlContent.BackColor
        _stage.Size = New Size(contentRight + contentLeft, contentBottom + CInt(40 * k))
        For Each c As Control In items
            _stage.Controls.Add(c)
        Next

        _canvas = New Panel()
        _canvas.BackColor = PnlContent.BackColor
        _canvas.Location = Point.Empty
        _canvas.Height = _stage.Height
        _canvas.Controls.Add(_stage)

        PnlContent.SuspendLayout()
        PnlContent.AutoScrollMinSize = Size.Empty
        PnlContent.Controls.Add(_canvas)
        PnlContent.ResumeLayout(True)

        StyleNestedCards()

        AddHandler PnlContent.ClientSizeChanged, AddressOf CenterStage
        CenterStage(Nothing, EventArgs.Empty)
    End Sub

    Private Sub CenterStage(sender As Object, e As EventArgs)
        If _canvas Is Nothing OrElse _stage Is Nothing Then Return
        ' The page is taller than the window, so the vertical scrollbar is nearly always there.
        ' Always leave room for it; otherwise the bar appearing shrinks the panel, which makes
        ' a second (horizontal) scrollbar pop up.
        Dim available As Integer = PnlContent.Width - SystemInformation.VerticalScrollBarWidth
        _canvas.Width = Math.Max(available, _stage.Width)
        _stage.Left = (_canvas.Width - _stage.Width) \ 2
    End Sub

    ' The small cards inside the big ones (budget limits, wallets) get a softer fill and tighter corners
    Private Sub StyleNestedCards()
        Dim nested As CardPanel() = {MBLCardPanel1, MBLCardPanel2, MBLCardPanel3, MBLCardPanel4,
                                     MBLCardPanel5, MBLCardPanel6, MBLCardPanel7, MBLCardPanel8,
                                     WalletCard1, WalletCard2, WalletCard3, WalletCard4}
        For Each card As CardPanel In nested
            card.Radius = 12
            card.BackColor = Theme.ColSoft
        Next
    End Sub

    ' ------------------------------------------------------------------
    ' Existing designer event stubs (unchanged)
    ' ------------------------------------------------------------------
    Private Sub pnlHeader_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles pnlHeader.Paint

    End Sub
    Private Sub Perc2_Click(sender As Object, e As EventArgs) Handles Perc2.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Tagline.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles BalDesc.Click

    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles CashFlowHistoryLabel.Click

    End Sub

    Private Sub ExpCardBg_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub SavRateBG_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles SavRateBG.Paint

    End Sub

    Private Sub Label1_Click_2(sender As Object, e As EventArgs) Handles WalAndAcc.Click

    End Sub

    Private Sub Label1_Click_3(sender As Object, e As EventArgs) Handles WalletsHeading.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles TransHistoryPnl.Paint

    End Sub

    Private Sub Label1_Click_4(sender As Object, e As EventArgs) Handles RecTransDesc.Click

    End Sub
End Class
