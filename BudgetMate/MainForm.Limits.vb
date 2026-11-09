Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Windows.Forms

' =====================================================================
' PART 7 - Monthly budget limits card:
'   * one mini card per expense category (Housing ... Miscellaneous)
'   * "spent / limit", a progress bar, "NN% used" and "amount left"
'   * the bar is the category colour, turns gold at 80% and red once over the limit
'   * "Manage Limits" opens the dialog; saving updates the bars straight away
' Data comes from Budget.SpentByCategory and Budget.LimitFor; RefreshAll calls RefreshLimits.
' A limit of 0 means "no limit" for that category.
' =====================================================================
Partial Class MainForm

    Private Class LimitRow
        Public Category As CategoryInfo
        Public Dot As Panel
        Public NameLabel As Label
        Public Spent As Label
        Public Slash As Label
        Public LimitLabel As Label
        Public Bar As Panel
        Public Used As Label
        Public Remaining As Label
        Public RightEdge As Integer       ' labels stay right-aligned to the end of the bar
        Public Fraction As Double = 0.0   ' 0..1 share of the limit used (drives the bar)
        Public Fill As Color = Color.Gray
    End Class

    Private _limitRows As New List(Of LimitRow)()
    Private _limitsReady As Boolean = False

    ' ------------------------------------------------------------------
    ' One-time setup
    ' ------------------------------------------------------------------
    Private Sub InitLimits()
        If _limitsReady Then Return

        ' Every array is in the same order as Catalog.ExpenseCategories
        ' (Housing, Food, Transportation, Utilities, Entertainment, Health, Shopping, Miscellaneous).
        Dim dots() As Panel = {MBLColorHousing, MBLColorFood, MBLColorTranspo, MBLColorUtil,
                               MBLColorEnt, MBLColorHealth, MBLColorShop, MBLColorMisc}
        Dim names() As Label = {MBLHousing, MBLFood, MBLTransportation, MBLUtilities,
                                MBLEntertainment, MBLHealth, MBLShopping, MBLMiscellaneous}
        Dim spents() As Label = {SpentAmtHousing, SpentAmtFood, SpentAmtTranspo, SpentAmtUtilities,
                                 SpentAmtEntertainment, SpentAmtHealth, SpentAmtShopping, SpentAmtMiscellaneous}
        Dim slashes() As Label = {slash1, slash2, slash3, slash4, slash5, slash6, slash7, slash8}
        Dim limits() As Label = {LimitHousing, LimitFood, LimitTransportation, LimitUtilities,
                                 LimitEnt, LimitHealth, LimitShopping, LimitMisc}
        Dim bars() As Panel = {ProgressHousing, ProgressFood, ProgressTranspo, ProgressUtil,
                               ProgressEnt, ProgressHealth, ProgressShop, ProgressMisc}
        Dim useds() As Label = {UsedHousing, UsedFood, UsedTranspo, UsedUtil,
                                UsedEnt, UsedHealth, UsedShop, UsedMisc}
        Dim lefts() As Label = {AmtLeftHousing, AmtLeftFood, AmtLeftTranspo, AmtLeftUtil,
                                AmtLeftEnt, AmtLeftHealth, AmtLeftShop, AmtLeftMisc}

        Dim dbProp As PropertyInfo = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)

        _limitRows.Clear()
        For i As Integer = 0 To Catalog.ExpenseCategories.Length - 1
            Dim row As New LimitRow()
            row.Category = Catalog.ExpenseCategories(i)
            row.Dot = dots(i)
            row.NameLabel = names(i)
            row.Spent = spents(i)
            row.Slash = slashes(i)
            row.LimitLabel = limits(i)
            row.Bar = bars(i)
            row.Used = useds(i)
            row.Remaining = lefts(i)
            row.RightEdge = bars(i).Right

            row.NameLabel.Text = row.Category.Name
            row.Slash.Text = "/"

            ' Round dot in the category colour
            row.Dot.BackColor = row.Category.Tint
            Using path As New GraphicsPath()
                path.AddEllipse(0, 0, row.Dot.Width, row.Dot.Height)
                row.Dot.Region = New Region(path)
            End Using

            ' The bar is drawn by hand (rounded track + rounded fill)
            If dbProp IsNot Nothing Then dbProp.SetValue(row.Bar, CObj(True))
            row.Bar.Tag = row
            AddHandler row.Bar.Paint, AddressOf LimitBar_Paint

            _limitRows.Add(row)
        Next

        AddHandler Button1.Click, AddressOf ManageLimits_Click
        _limitsReady = True
    End Sub

    ' ------------------------------------------------------------------
    ' Recalculate + redraw (called from RefreshAll)
    ' ------------------------------------------------------------------
    Private Sub RefreshLimits()
        InitLimits()

        Dim cats As CategoryInfo() = Catalog.ExpenseCategories
        Dim spent(cats.Length - 1) As Decimal

        ' Spending per category for the viewed month. A category that isn't in the
        ' catalog (e.g. from an imported file) is counted under Miscellaneous.
        For Each pair As KeyValuePair(Of String, Decimal) In _budget.SpentByCategory(_budget.ViewMonth)
            Dim idx As Integer = Array.FindIndex(cats, Function(c) String.Equals(c.Name, pair.Key, StringComparison.OrdinalIgnoreCase))
            If idx < 0 Then idx = cats.Length - 1
            spent(idx) += pair.Value
        Next

        For i As Integer = 0 To _limitRows.Count - 1
            Dim row As LimitRow = _limitRows(i)
            Dim limit As Decimal = _budget.LimitFor(row.Category.Name)
            Dim amount As Decimal = spent(i)

            Dim usedText As String
            Dim leftText As String
            Dim textColor As Color = Theme.ColMuted

            If limit <= 0D Then
                ' No cap set for this category
                row.LimitLabel.Text = "No limit"
                usedText = "No limit set"
                leftText = ""
                row.Fraction = 0.0
                row.Fill = row.Category.Tint
            Else
                Dim ratio As Double = CDbl(amount / limit)
                row.LimitLabel.Text = Money(limit)
                row.Fraction = Math.Min(1.0, ratio)

                Dim pct As Integer = CInt(Math.Floor(Math.Min(ratio * 100.0, 9999.0)))
                usedText = pct.ToString() & "% used"

                If amount > limit Then
                    leftText = Money(amount - limit) & " over"
                    row.Fill = Theme.ColRed
                    textColor = Theme.ColRed
                ElseIf ratio >= 0.8 Then
                    leftText = Money(limit - amount) & " left"
                    row.Fill = Theme.ColGold
                    textColor = Theme.ColGold
                Else
                    leftText = Money(limit - amount) & " left"
                    row.Fill = row.Category.Tint
                End If
            End If

            ' Text + colours (read from Theme every time so light/dark both work)
            row.NameLabel.Font = Theme.Fnt(False, 9.0F, True)
            row.NameLabel.ForeColor = Theme.ColInk
            row.Spent.Font = Theme.Fnt(False, 9.0F, True)
            row.Spent.ForeColor = If(amount > limit AndAlso limit > 0D, Theme.ColRed, Theme.ColInk)
            row.Spent.Text = Money(amount)
            row.Slash.Font = Theme.Fnt(False, 9.0F)
            row.Slash.ForeColor = Theme.ColMuted
            row.LimitLabel.Font = Theme.Fnt(False, 9.0F)
            row.LimitLabel.ForeColor = Theme.ColMuted
            row.Used.Font = Theme.Fnt(False, 8.5F)
            row.Used.ForeColor = textColor
            row.Used.Text = usedText
            row.Remaining.Font = Theme.Fnt(False, 8.5F)
            row.Remaining.ForeColor = textColor
            row.Remaining.Text = leftText

            ' "spent / limit" and "left" sit against the right edge of the bar
            row.LimitLabel.Left = row.RightEdge - row.LimitLabel.GetPreferredSize(Size.Empty).Width
            row.Slash.Left = row.LimitLabel.Left - row.Slash.GetPreferredSize(Size.Empty).Width
            row.Spent.Left = row.Slash.Left - row.Spent.GetPreferredSize(Size.Empty).Width
            row.Remaining.Left = row.RightEdge - row.Remaining.GetPreferredSize(Size.Empty).Width

            row.Bar.Invalidate()
        Next
    End Sub

    ' ------------------------------------------------------------------
    ' The progress bar
    ' ------------------------------------------------------------------
    Private Sub LimitBar_Paint(sender As Object, e As PaintEventArgs)
        Dim bar As Panel = DirectCast(sender, Panel)
        Dim row As LimitRow = TryCast(bar.Tag, LimitRow)
        If row Is Nothing Then Return

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.Clear(If(bar.Parent IsNot Nothing, bar.Parent.BackColor, Theme.ColSoft))

        Dim track As New Rectangle(0, 0, bar.Width - 1, bar.Height - 1)
        Using b As New SolidBrush(Theme.ColLine)
            FillPill(g, b, track)
        End Using

        If row.Fraction > 0.0 Then
            ' Never narrower than the bar is tall, so even a tiny spend shows as a dot
            Dim w As Integer = Math.Max(track.Height, CInt(Math.Round(track.Width * row.Fraction)))
            Using b As New SolidBrush(row.Fill)
                FillPill(g, b, New Rectangle(0, 0, w, track.Height))
            End Using
        End If
    End Sub

    Private Shared Sub FillPill(g As Graphics, brush As Brush, r As Rectangle)
        If r.Width <= 0 OrElse r.Height <= 0 Then Return
        Dim d As Integer = Math.Min(r.Height, r.Width)
        Using path As New GraphicsPath()
            path.AddArc(r.X, r.Y, d, d, 90, 180)
            path.AddArc(r.Right - d, r.Y, d, d, 270, 180)
            path.CloseFigure()
            g.FillPath(brush, path)
        End Using
    End Sub

    ' ------------------------------------------------------------------
    ' Manage Limits button
    ' ------------------------------------------------------------------
    Private Sub ManageLimits_Click(sender As Object, e As EventArgs)
        If _budget Is Nothing Then Return

        Using dlg As New LimitsDialog(_budget.Data, "Budget limits")
            If dlg.ShowDialog(Me) <> DialogResult.OK OrElse dlg.Result Is Nothing Then Return

            ' Saves to disk and raises Changed, which calls RefreshAll -> RefreshLimits
            _budget.SetLimits(dlg.Result)
        End Using
    End Sub

End Class
