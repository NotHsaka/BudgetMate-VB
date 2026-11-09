Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms

' =====================================================================
' PART 6 - Cash flow history card:
'   * grouped bars (income = green, expenses = red) for the last 6 months,
'     ending with the month being viewed
'   * grid lines with peso labels on the left, month names underneath
'   * legend (Income / Expenses) along the bottom
'   * hover a month to see its exact income, expenses and net
'   * the "Your money story starts here" message while there is nothing to plot
' Data comes from Budget.CashFlow; RefreshAll calls RefreshCashFlow.
' =====================================================================
Partial Class MainForm

    Private Const CashFlowMonths As Integer = 6

    Private _cashPoints As New List(Of MonthPoint)()
    Private _cashHasData As Boolean = False
    Private _cashReady As Boolean = False
    Private _cashGroups As New List(Of RectangleF)()     ' hover area of each month (filled while painting)
    Private _cashHover As Integer = -1
    Private _cashTip As ToolTip
    Private _cashTipText As String = ""
    Private _cashFont As Font
    Private _cashFontBold As Font

    ' ------------------------------------------------------------------
    ' One-time setup
    ' ------------------------------------------------------------------
    Private Sub InitCashFlow()
        If _cashReady Then Return

        _cashFont = New Font("Segoe UI", 8.5F)
        _cashFontBold = New Font("Segoe UI", 8.5F, FontStyle.Bold)

        ' Panels don't expose DoubleBuffered, so switch it on by reflection (stops flicker)
        GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic) _
            .SetValue(ChartArea, CObj(True))

        _cashTip = New ToolTip()
        AddHandler ChartArea.Paint, AddressOf ChartArea_Paint
        AddHandler ChartArea.MouseMove, AddressOf ChartArea_MouseMove
        AddHandler ChartArea.MouseLeave, AddressOf ChartArea_MouseLeave
        AddHandler ChartArea.Resize, AddressOf CenterCashFlowEmpty

        _cashReady = True
    End Sub

    ' ------------------------------------------------------------------
    ' Recalculate + redraw (called from RefreshAll)
    ' ------------------------------------------------------------------
    Private Sub RefreshCashFlow()
        InitCashFlow()

        _cashPoints = _budget.CashFlow(CashFlowMonths)
        _cashHasData = _cashPoints.Any(Function(p) p.Income > 0D OrElse p.Expenses > 0D)
        _cashHover = -1
        SetCashTip("")

        ' Empty message only while there is nothing to draw
        EmptyStateIcon.Visible = Not _cashHasData
        EmptyStateHeading.Visible = Not _cashHasData
        EmptyStateHeadingDesc.Visible = Not _cashHasData
        CenterCashFlowEmpty(Nothing, EventArgs.Empty)

        ChartArea.Invalidate()
    End Sub

    Private Sub CenterCashFlowEmpty(sender As Object, e As EventArgs)
        Dim cx As Integer = ChartArea.Width \ 2
        EmptyStateIcon.Left = cx - EmptyStateIcon.Width \ 2
        EmptyStateHeading.Left = cx - EmptyStateHeading.GetPreferredSize(Size.Empty).Width \ 2
        EmptyStateHeadingDesc.Left = cx - EmptyStateHeadingDesc.GetPreferredSize(Size.Empty).Width \ 2
    End Sub

    ' ------------------------------------------------------------------
    ' Helpers
    ' ------------------------------------------------------------------
    ' Rounds the top of the scale up to a tidy number so the grid labels read nicely
    ' (e.g. 2,000 -> 2,000; 1,850 -> 2,000; 7,300 -> 8,000). Returns the step between grid lines.
    Private Shared Function NiceStep(maxValue As Decimal, lines As Integer) As Decimal
        If maxValue <= 0D Then Return 1D
        Dim raw As Double = CDbl(maxValue) / lines
        Dim mag As Double = Math.Pow(10.0, Math.Floor(Math.Log10(raw)))
        For Each m As Double In New Double() {1.0, 2.0, 2.5, 5.0, 10.0}
            If m * mag >= raw - 0.0000001 Then Return CDec(m * mag)
        Next
        Return CDec(10.0 * mag)
    End Function

    Private Shared Function AxisText(v As Decimal) As String
        If v >= 1000D Then Return Fmt.Peso & CDbl(v / 1000D).ToString("0.##", Globalization.CultureInfo.InvariantCulture) & "k"
        Return Fmt.Peso & CDbl(v).ToString("0", Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Shared Function BarPath(r As RectangleF, radius As Single) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Single = Math.Min(radius * 2.0F, Math.Min(r.Width, r.Height * 2.0F))
        If d < 1.0F Then
            path.AddRectangle(r)
            Return path
        End If
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddLine(r.Right, r.Bottom, r.X, r.Bottom)
        path.CloseFigure()
        Return path
    End Function

    Private Const TextFlags As TextFormatFlags = TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix

    ' ------------------------------------------------------------------
    ' Drawing
    ' ------------------------------------------------------------------
    Private Sub ChartArea_Paint(sender As Object, e As PaintEventArgs)
        _cashGroups.Clear()
        If Not _cashHasData Then Return

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim k As Single = _scaleK
        Dim w As Single = ChartArea.Width
        Dim h As Single = ChartArea.Height

        ' --- scale (4 grid intervals) ---
        Dim maxValue As Decimal = _cashPoints.Max(Function(p) Math.Max(p.Income, p.Expenses))
        Dim stepValue As Decimal = NiceStep(maxValue, 4)
        Dim topValue As Decimal = stepValue * 4D

        ' --- plot rectangle ---
        Dim textH As Integer = TextRenderer.MeasureText("Ag", _cashFont, New Size(Integer.MaxValue, Integer.MaxValue), TextFlags).Height
        Dim axisW As Integer = 0
        For i As Integer = 0 To 4
            axisW = Math.Max(axisW, TextRenderer.MeasureText(AxisText(stepValue * i), _cashFont, New Size(Integer.MaxValue, Integer.MaxValue), TextFlags).Width)
        Next
        Dim legendH As Single = textH + 6.0F * k
        Dim plot As New RectangleF(axisW + 10.0F * k, textH / 2.0F + 2.0F * k,
                                   w - (axisW + 10.0F * k) - 4.0F * k,
                                   h - legendH - (textH + 8.0F * k) - (textH / 2.0F + 2.0F * k))
        If plot.Width < 20.0F OrElse plot.Height < 20.0F Then Return

        ' --- grid + axis labels ---
        Using grid As New Pen(Theme.ColLineSoft, 1.0F)
            For i As Integer = 0 To 4
                Dim y As Single = plot.Bottom - plot.Height * i / 4.0F
                g.DrawLine(grid, plot.Left, y, plot.Right, y)
                Dim labelRect As New Rectangle(0, CInt(y - textH / 2.0F), CInt(axisW), textH)
                TextRenderer.DrawText(g, AxisText(stepValue * i), _cashFont, labelRect, Theme.ColMuted,
                                      TextFlags Or TextFormatFlags.Right Or TextFormatFlags.VerticalCenter)
            Next
        End Using

        ' --- bars ---
        Dim n As Integer = _cashPoints.Count
        Dim groupW As Single = plot.Width / n
        Dim barW As Single = Math.Min(groupW * 0.30F, 34.0F * k)
        Dim barGap As Single = 4.0F * k
        Dim radius As Single = 4.0F * k

        For i As Integer = 0 To n - 1
            Dim p As MonthPoint = _cashPoints(i)
            Dim gx As Single = plot.Left + groupW * i
            Dim area As New RectangleF(gx, plot.Top, groupW, plot.Height + textH + 8.0F * k)
            _cashGroups.Add(area)

            ' soft highlight behind the month under the mouse
            If i = _cashHover Then
                Using hl As New SolidBrush(Color.FromArgb(40, Theme.ColMuted))
                    g.FillRectangle(hl, gx + 2.0F * k, plot.Top, groupW - 4.0F * k, plot.Height)
                End Using
            End If

            Dim pairW As Single = barW * 2.0F + barGap
            Dim left As Single = gx + (groupW - pairW) / 2.0F
            DrawBar(g, p.Income, topValue, plot, left, barW, radius, Theme.ColGreen)
            DrawBar(g, p.Expenses, topValue, plot, left + barW + barGap, barW, radius, Theme.ColRed)

            ' month name (the month being viewed is emphasised)
            Dim isViewed As Boolean = (p.Month.Year = _budget.ViewMonth.Year AndAlso p.Month.Month = _budget.ViewMonth.Month)
            Dim monthRect As New Rectangle(CInt(gx), CInt(plot.Bottom + 6.0F * k), CInt(groupW), textH)
            TextRenderer.DrawText(g, p.Month.ToString("MMM", Globalization.CultureInfo.CurrentCulture),
                                  If(isViewed, _cashFontBold, _cashFont), monthRect,
                                  If(isViewed, Theme.ColInk, Theme.ColMuted),
                                  TextFlags Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.Top)
        Next

        ' baseline on top of the bars' feet
        Using baseline As New Pen(Theme.ColLine, 1.0F)
            g.DrawLine(baseline, plot.Left, plot.Bottom, plot.Right, plot.Bottom)
        End Using

        ' --- legend (bottom left) ---
        Dim ly As Single = h - legendH + 3.0F * k
        Dim dot As Single = 9.0F * k
        Dim lx As Single = plot.Left
        For Each item As KeyValuePair(Of String, Color) In New KeyValuePair(Of String, Color)() {
                New KeyValuePair(Of String, Color)("Income", Theme.ColGreen),
                New KeyValuePair(Of String, Color)("Expenses", Theme.ColRed)}
            Using b As New SolidBrush(item.Value)
                g.FillEllipse(b, lx, ly + (textH - dot) / 2.0F, dot, dot)
            End Using
            Dim tw As Integer = TextRenderer.MeasureText(item.Key, _cashFont, New Size(Integer.MaxValue, Integer.MaxValue), TextFlags).Width
            TextRenderer.DrawText(g, item.Key, _cashFont, New Point(CInt(lx + dot + 5.0F * k), CInt(ly)), Theme.ColMuted, TextFlags)
            lx += dot + 5.0F * k + tw + 16.0F * k
        Next
    End Sub

    Private Shared Sub DrawBar(g As Graphics, amount As Decimal, topValue As Decimal, plot As RectangleF,
                               left As Single, barW As Single, radius As Single, tint As Color)
        If amount <= 0D OrElse topValue <= 0D Then Return
        Dim barH As Single = Math.Max(3.0F, CSng(amount / topValue) * plot.Height)
        Dim r As New RectangleF(left, plot.Bottom - barH, barW, barH)
        Using path As GraphicsPath = BarPath(r, radius)
            Using fill As New SolidBrush(tint)
                g.FillPath(fill, path)
            End Using
        End Using
    End Sub

    ' ------------------------------------------------------------------
    ' Hover tooltip
    ' ------------------------------------------------------------------
    Private Sub ChartArea_MouseMove(sender As Object, e As MouseEventArgs)
        Dim hit As Integer = -1
        For i As Integer = 0 To _cashGroups.Count - 1
            If _cashGroups(i).Contains(e.Location) Then
                hit = i
                Exit For
            End If
        Next

        If hit <> _cashHover Then
            _cashHover = hit
            ChartArea.Invalidate()
        End If

        If hit < 0 OrElse hit >= _cashPoints.Count Then
            SetCashTip("")
        Else
            Dim p As MonthPoint = _cashPoints(hit)
            SetCashTip(MonthTitle(p.Month) & vbCrLf &
                       "Income: " & Money(p.Income) & vbCrLf &
                       "Expenses: " & Money(p.Expenses) & vbCrLf &
                       "Net: " & Money(p.Income - p.Expenses))
        End If
    End Sub

    Private Sub ChartArea_MouseLeave(sender As Object, e As EventArgs)
        If _cashHover <> -1 Then
            _cashHover = -1
            ChartArea.Invalidate()
        End If
        SetCashTip("")
    End Sub

    Private Sub SetCashTip(text As String)
        If _cashTip Is Nothing OrElse text = _cashTipText Then Return
        _cashTipText = text
        _cashTip.SetToolTip(ChartArea, text)
    End Sub

End Class
