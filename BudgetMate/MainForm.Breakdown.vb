Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms

' =====================================================================
' PART 5 - Expense breakdown card:
'   * the donut ring (one arc per spending category, for the month being viewed)
'   * the "Total spent" amount in the middle
'   * the coloured dots and the percentage next to every category name
'   * hover over the ring to see the category and its amount
' Data comes from Budget.SpentByCategory; RefreshAll calls RefreshBreakdown.
' =====================================================================
Partial Class MainForm

    Private Class BreakdownSlice
        Public Name As String = ""
        Public Amount As Decimal = 0D
        Public Tint As Color = Color.Gray
        Public Percent As Integer = 0
        Public Share As Double = 0.0
    End Class

    Private _slices As New List(Of BreakdownSlice)()
    Private _breakdownTotal As Decimal = 0D
    Private _breakdownReady As Boolean = False

    ' Legend controls, sorted top-to-bottom so index i = Catalog.ExpenseCategories(i)
    Private _legendDots() As Panel
    Private _legendNames() As Label
    Private _legendPercs() As Label
    Private _percRight() As Integer

    Private _donutTip As ToolTip
    Private _donutTipText As String = ""

    ' ------------------------------------------------------------------
    ' One-time setup
    ' ------------------------------------------------------------------
    Private Sub InitBreakdown()
        If _breakdownReady Then Return

        _legendDots = New Panel() {ColorLegend1, color2, color3, color4, color5, color6, color7, color8} _
                      .OrderBy(Function(p) p.Top).ToArray()
        _legendNames = New Label() {Housing, Food, Transportation, Utilities, Entertainment, Health, Shopping, Miscellaneous} _
                      .OrderBy(Function(l) l.Top).ToArray()
        _legendPercs = New Label() {Perc1, Perc2, Perc3, Perc4, Perc5, Perc6, Perc7, Perc8} _
                      .OrderBy(Function(l) l.Top).ToArray()

        ReDim _percRight(_legendPercs.Length - 1)
        For i As Integer = 0 To _legendPercs.Length - 1
            _percRight(i) = _legendPercs(i).Right      ' percentages stay right-aligned to this edge
        Next

        ' Legend: round dot in the category colour, name from the catalog
        For i As Integer = 0 To Catalog.ExpenseCategories.Length - 1
            Dim info As CategoryInfo = Catalog.ExpenseCategories(i)
            _legendNames(i).Text = info.Name
            _legendDots(i).BackColor = info.Tint
            Using path As New GraphicsPath()
                path.AddEllipse(0, 0, _legendDots(i).Width, _legendDots(i).Height)
                _legendDots(i).Region = New Region(path)
            End Using
        Next

        ' Centre text
        DonutChartDesc.Font = New Font("Segoe UI", 9.0F)
        DonutChartDesc.ForeColor = Theme.ColMuted
        DonutChartDesc.Text = "Total spent"
        DonutChartAmt.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        DonutChartAmt.ForeColor = Theme.ColInk

        ' Panels don't expose DoubleBuffered, so switch it on by reflection (stops flicker)
        GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic) _
            .SetValue(DonutChartArea, CObj(True))

        _donutTip = New ToolTip()
        AddHandler DonutChartArea.Paint, AddressOf DonutChartArea_Paint
        AddHandler DonutChartArea.MouseMove, AddressOf DonutChartArea_MouseMove
        AddHandler DonutChartArea.MouseLeave, AddressOf DonutChartArea_MouseLeave
        AddHandler DonutChartArea.Resize, AddressOf CenterDonutLabels

        _breakdownReady = True
    End Sub

    ' ------------------------------------------------------------------
    ' Recalculate + redraw (called from RefreshAll)
    ' ------------------------------------------------------------------
    Private Sub RefreshBreakdown()
        InitBreakdown()

        Dim cats As CategoryInfo() = Catalog.ExpenseCategories
        Dim amounts(cats.Length - 1) As Decimal

        ' Spending per category for the viewed month. A category that isn't in the
        ' catalog (e.g. from an imported file) is counted under Miscellaneous.
        For Each pair As KeyValuePair(Of String, Decimal) In _budget.SpentByCategory(_budget.ViewMonth)
            Dim idx As Integer = Array.FindIndex(cats, Function(c) String.Equals(c.Name, pair.Key, StringComparison.OrdinalIgnoreCase))
            If idx < 0 Then idx = cats.Length - 1
            amounts(idx) += pair.Value
        Next

        _breakdownTotal = amounts.Sum()

        ' Whole-number percentages that always add up to exactly 100
        ' (round everything down, then give the leftover points to the biggest remainders).
        Dim pct(cats.Length - 1) As Integer
        If _breakdownTotal > 0D Then
            Dim exact(cats.Length - 1) As Double
            Dim given As Integer = 0
            For i As Integer = 0 To cats.Length - 1
                exact(i) = CDbl(amounts(i) / _breakdownTotal) * 100.0
                pct(i) = CInt(Math.Floor(exact(i)))
                given += pct(i)
            Next
            Dim order As Integer() = Enumerable.Range(0, cats.Length) _
                .Where(Function(i) amounts(i) > 0D) _
                .OrderByDescending(Function(i) exact(i) - Math.Floor(exact(i))).ToArray()
            Dim k As Integer = 0
            While given < 100 AndAlso order.Length > 0
                pct(order(k Mod order.Length)) += 1
                given += 1
                k += 1
            End While
        End If

        ' Slices for the ring (only categories that were actually spent on)
        _slices.Clear()
        For i As Integer = 0 To cats.Length - 1
            If amounts(i) <= 0D Then Continue For
            Dim s As New BreakdownSlice()
            s.Name = cats(i).Name
            s.Amount = amounts(i)
            s.Tint = cats(i).Tint
            s.Percent = pct(i)
            s.Share = CDbl(amounts(i) / _breakdownTotal)
            _slices.Add(s)
        Next

        ' Legend text: right-aligned percentages; unused categories are dimmed
        For i As Integer = 0 To cats.Length - 1
            Dim used As Boolean = amounts(i) > 0D
            Dim text As String
            If Not used Then
                text = "0%"
            ElseIf pct(i) = 0 Then
                text = "<1%"
            Else
                text = pct(i).ToString() & "%"
            End If
            _legendPercs(i).Text = text
            _legendPercs(i).Left = _percRight(i) - _legendPercs(i).GetPreferredSize(Size.Empty).Width
            _legendPercs(i).ForeColor = If(used, Theme.ColInk, Theme.ColMuted)
            _legendNames(i).ForeColor = If(used, Theme.ColInk, Theme.ColMuted)
        Next

        DonutChartAmt.Text = Money(_breakdownTotal)
        CenterDonutLabels(Nothing, EventArgs.Empty)
        DonutChartArea.Invalidate()
    End Sub

    Private Sub CenterDonutLabels(sender As Object, e As EventArgs)
        Dim cx As Integer = DonutChartArea.Width \ 2
        Dim cy As Integer = DonutChartArea.Height \ 2
        Dim hAmt As Integer = DonutChartAmt.GetPreferredSize(Size.Empty).Height
        Dim hDesc As Integer = DonutChartDesc.GetPreferredSize(Size.Empty).Height
        Dim top As Integer = cy - (hDesc + hAmt) \ 2
        DonutChartDesc.Left = cx - DonutChartDesc.GetPreferredSize(Size.Empty).Width \ 2
        DonutChartDesc.Top = top
        DonutChartAmt.Left = cx - DonutChartAmt.GetPreferredSize(Size.Empty).Width \ 2
        DonutChartAmt.Top = top + hDesc
    End Sub

    ' ------------------------------------------------------------------
    ' Drawing
    ' ------------------------------------------------------------------
    ' The ring is a thick circle outline; this gives its bounding box and thickness.
    Private Sub DonutGeometry(ByRef ring As RectangleF, ByRef thickness As Single)
        thickness = Math.Max(10.0F, DonutChartArea.Width * 0.13F)
        Dim diameter As Single = Math.Min(DonutChartArea.Width, DonutChartArea.Height) - thickness - 2.0F
        ring = New RectangleF((DonutChartArea.Width - diameter) / 2.0F,
                              (DonutChartArea.Height - diameter) / 2.0F, diameter, diameter)
    End Sub

    Private Sub DonutChartArea_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim ring As RectangleF
        Dim thickness As Single
        DonutGeometry(ring, thickness)

        ' Nothing spent this month: just an empty grey ring
        If _slices.Count = 0 Then
            Using track As New Pen(Theme.ColLine, thickness)
                g.DrawEllipse(track, ring)
            End Using
            Return
        End If

        ' Only one category: a full circle (arcs can't show a gap here)
        If _slices.Count = 1 Then
            Using pen As New Pen(_slices(0).Tint, thickness)
                g.DrawEllipse(pen, ring)
            End Using
            Return
        End If

        Const gap As Single = 2.0F      ' degrees of space between neighbouring slices
        Dim start As Single = -90.0F    ' 12 o'clock, going clockwise
        For Each s As BreakdownSlice In _slices
            Dim sweep As Single = CSng(s.Share * 360.0)
            Dim shown As Single = If(sweep > gap * 2.0F, sweep - gap, sweep)
            Using pen As New Pen(s.Tint, thickness)
                pen.StartCap = LineCap.Flat
                pen.EndCap = LineCap.Flat
                g.DrawArc(pen, ring, start + (sweep - shown) / 2.0F, shown)
            End Using
            start += sweep
        Next
    End Sub

    ' ------------------------------------------------------------------
    ' Hover tooltip
    ' ------------------------------------------------------------------
    Private Sub DonutChartArea_MouseMove(sender As Object, e As MouseEventArgs)
        SetDonutTip(SliceTextAt(e.Location))
    End Sub

    Private Sub DonutChartArea_MouseLeave(sender As Object, e As EventArgs)
        SetDonutTip("")
    End Sub

    Private Sub SetDonutTip(text As String)
        If text = _donutTipText Then Return
        _donutTipText = text
        _donutTip.SetToolTip(DonutChartArea, text)
    End Sub

    Private Function SliceTextAt(p As Point) As String
        If _slices.Count = 0 Then Return ""

        Dim ring As RectangleF
        Dim thickness As Single
        DonutGeometry(ring, thickness)

        Dim dx As Double = p.X - (ring.X + ring.Width / 2.0)
        Dim dy As Double = p.Y - (ring.Y + ring.Height / 2.0)
        Dim dist As Double = Math.Sqrt(dx * dx + dy * dy)
        Dim mid As Double = ring.Width / 2.0
        If dist < mid - thickness / 2.0 OrElse dist > mid + thickness / 2.0 Then Return ""

        ' Angle measured clockwise from 12 o'clock, 0-360
        Dim angle As Double = (Math.Atan2(dy, dx) * 180.0 / Math.PI + 90.0 + 360.0) Mod 360.0
        Dim running As Double = 0.0
        For Each s As BreakdownSlice In _slices
            running += s.Share * 360.0
            If angle <= running Then
                Return s.Name & ": " & Money(s.Amount) & " (" & If(s.Percent = 0, "<1", s.Percent.ToString()) & "%)"
            End If
        Next
        Dim last As BreakdownSlice = _slices(_slices.Count - 1)
        Return last.Name & ": " & Money(last.Amount)
    End Function

End Class
