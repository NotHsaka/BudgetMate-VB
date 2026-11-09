Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

' ---------------------------------------------------------------------------
' Drawing helpers
' ---------------------------------------------------------------------------
Public Module Gfx

    Public Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = Math.Min(Math.Max(radius * 2, 1), Math.Max(Math.Min(r.Width, r.Height), 1))
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Public Sub FillRound(g As Graphics, r As Rectangle, radius As Integer, c As Color)
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As GraphicsPath = RoundedPath(r, radius)
            Using b As New SolidBrush(c)
                g.FillPath(b, p)
            End Using
        End Using
    End Sub

    Public Sub StrokeRound(g As Graphics, r As Rectangle, radius As Integer, c As Color, Optional penWidth As Single = 1.0F)
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using p As GraphicsPath = RoundedPath(r, radius)
            Using pen As New Pen(c, penWidth)
                g.DrawPath(pen, p)
            End Using
        End Using
    End Sub

    Public Sub FillDot(g As Graphics, x As Integer, y As Integer, size As Integer, c As Color)
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using b As New SolidBrush(c)
            g.FillEllipse(b, x, y, size, size)
        End Using
    End Sub

    Public Function Mix(a As Color, b As Color, t As Single) As Color
        Dim ar As Integer = a.R
        Dim ag As Integer = a.G
        Dim ab As Integer = a.B
        Dim br As Integer = b.R
        Dim bg As Integer = b.G
        Dim bb As Integer = b.B
        Return Color.FromArgb(ar + CInt((br - ar) * t), ag + CInt((bg - ag) * t), ab + CInt((bb - ab) * t))
    End Function

    Public Function IsDarkColor(c As Color) As Boolean
        Return (CInt(c.R) + CInt(c.G) + CInt(c.B)) \ 3 < 110
    End Function

    Public Function Measure(text As String, f As Font) As Integer
        If String.IsNullOrEmpty(text) Then Return 0
        Return TextRenderer.MeasureText(text, f, New Size(Integer.MaxValue, Integer.MaxValue), TextFormatFlags.NoPadding).Width
    End Function

    ' Draws text with its top-left corner at x, y.
    Public Sub Txt(g As Graphics, text As String, f As Font, c As Color, x As Integer, y As Integer)
        If String.IsNullOrEmpty(text) Then Return
        TextRenderer.DrawText(g, text, f, New Point(x, y), c, TextFormatFlags.NoPadding)
    End Sub

    ' Draws text inside a rectangle, vertically centered, with ellipsis when too long.
    Public Sub TxtIn(g As Graphics, text As String, f As Font, c As Color, r As Rectangle, align As TextFormatFlags)
        If String.IsNullOrEmpty(text) Then Return
        TextRenderer.DrawText(g, text, f, r, c, align Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis)
    End Sub

    ' Draws text whose right edge is at rightX.
    Public Sub TxtRight(g As Graphics, text As String, f As Font, c As Color, rightX As Integer, y As Integer)
        Txt(g, text, f, c, rightX - Measure(text, f), y)
    End Sub

    Public Sub ProgressBar(g As Graphics, r As Rectangle, percent As Double, fill As Color, track As Color)
        FillRound(g, r, r.Height \ 2, track)
        If percent <= 0 Then Return
        Dim w As Integer = Math.Max(r.Height, CInt(r.Width * Math.Min(percent, 100.0) / 100.0))
        FillRound(g, New Rectangle(r.X, r.Y, w, r.Height), r.Height \ 2, fill)
    End Sub

End Module

' ---------------------------------------------------------------------------
' A rounded panel that can paint itself and report clicks on named regions.
' ---------------------------------------------------------------------------
<DesignerCategory("")>
Public Class Surface
    Inherits Panel

    Public Class HitRegion
        Public Rect As Rectangle
        Public Key As String
    End Class

    Public Property Radius As Integer = 18
    Public Property Bordered As Boolean = True
    Public Property Painter As Action(Of Graphics, Surface)
    Public ReadOnly Regions As New List(Of HitRegion)()
    Public Event RegionClicked(key As String)

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        BackColor = ColSurface
    End Sub

    Public Sub AddRegion(rect As Rectangle, key As String)
        Dim r As New HitRegion()
        r.Rect = rect
        r.Key = key
        Regions.Add(r)
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        Dim outer As Color = ColBg
        If Parent IsNot Nothing Then outer = Parent.BackColor
        g.Clear(outer)
        If Radius > 0 Then
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using path As GraphicsPath = RoundedPath(New Rectangle(0, 0, Width - 1, Height - 1), Radius)
                Using fill As New SolidBrush(BackColor)
                    g.FillPath(fill, path)
                End Using
                If Bordered Then
                    Using pen As New Pen(ColLine)
                        g.DrawPath(pen, path)
                    End Using
                End If
            End Using
        Else
            Using fill As New SolidBrush(BackColor)
                g.FillRectangle(fill, 0, 0, Width, Height)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Painter Is Nothing Then Return
        Regions.Clear()
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Try
            Painter.Invoke(e.Graphics, Me)
        Catch ex As Exception
            TextRenderer.DrawText(e.Graphics, "Paint error: " & ex.Message, Fnt(False, 9.0F), New Point(8, 8), Color.Red)
        End Try
    End Sub

    Private Function HitKey(p As Point) As String
        For Each r In Regions
            If r.Rect.Contains(p) Then Return r.Key
        Next
        Return Nothing
    End Function

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If HitKey(e.Location) IsNot Nothing Then
            Cursor = Cursors.Hand
        Else
            Cursor = Cursors.Default
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        Dim k As String = HitKey(e.Location)
        If k IsNot Nothing Then RaiseEvent RegionClicked(k)
    End Sub

End Class

' ---------------------------------------------------------------------------
' Rounded button (also used for dropdown fields and chips)
' ---------------------------------------------------------------------------
<DesignerCategory("")>
Public Class RoundButton
    Inherits Button

    Public Property Radius As Integer = 12
    Public Property BorderColor As Color = Color.Gray
    Public Property LeftAligned As Boolean = False
    Public Property ShowChevron As Boolean = False
    Public Property DotColor As Color = Color.Empty
    Public Property DimText As Boolean = False

    Private _hover As Boolean = False
    Private _down As Boolean = False

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        UseVisualStyleBackColor = False
        Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        _hover = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hover = False
        _down = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
        MyBase.OnMouseDown(mevent)
        _down = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        _down = False
        Invalidate()
    End Sub

    Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
        Dim w As Integer = Measure(Text, Font) + 28
        If Not DotColor.IsEmpty Then w += 18
        If ShowChevron Then w += 16
        Return New Size(w, Math.Max(Font.Height + 14, 30))
    End Function

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        Dim g As Graphics = pevent.Graphics
        Dim outer As Color = SystemColors.Control
        If Parent IsNot Nothing Then outer = Parent.BackColor
        g.Clear(outer)
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim fillColor As Color = BackColor
        If _down Then
            fillColor = If(IsDarkColor(BackColor), Mix(BackColor, Color.White, 0.2F), Mix(BackColor, Color.Black, 0.1F))
        ElseIf _hover Then
            fillColor = If(IsDarkColor(BackColor), Mix(BackColor, Color.White, 0.1F), Mix(BackColor, Color.Black, 0.05F))
        End If

        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim rad As Integer = Math.Min(Radius, Height \ 2)
        Using path As GraphicsPath = RoundedPath(rect, rad)
            Using fill As New SolidBrush(fillColor)
                g.FillPath(fill, path)
            End Using
            Using pen As New Pen(BorderColor)
                g.DrawPath(pen, path)
            End Using
            If Focused AndAlso ShowFocusCues Then
                Using ring As New Pen(ColBlue, 2.0F)
                    g.DrawPath(ring, path)
                End Using
            End If
        End Using

        Dim textColor As Color = If(DimText, ColMuted, ForeColor)
        Dim flags As TextFormatFlags = TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis
        If LeftAligned Then
            Dim x As Integer = 14
            If Not DotColor.IsEmpty Then
                FillDot(g, x, (Height - 9) \ 2, 9, DotColor)
                x += 18
            End If
            Dim rightPad As Integer = If(ShowChevron, 34, 14)
            TextRenderer.DrawText(g, Text, Font, New Rectangle(x, 0, Math.Max(Width - x - rightPad, 10), Height), textColor, flags Or TextFormatFlags.Left)
        Else
            TextRenderer.DrawText(g, Text, Font, New Rectangle(0, 0, Width, Height), textColor, flags Or TextFormatFlags.HorizontalCenter)
        End If

        If ShowChevron Then
            Dim cx As Integer = Width - 22
            Dim cy As Integer = Height \ 2
            Using pen As New Pen(ColMuted, 1.6F)
                g.DrawLines(pen, New Point() {New Point(cx - 4, cy - 2), New Point(cx, cy + 2), New Point(cx + 4, cy - 2)})
            End Using
        End If
    End Sub

End Class

' ---------------------------------------------------------------------------
' Rounded single-line text box
' ---------------------------------------------------------------------------
<DesignerCategory("")>
Public Class RoundedTextBox
    Inherits Panel

    Public ReadOnly Inner As New TextBox()
    Private _hasError As Boolean = False

    Public Sub New()
        SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        Height = 40
        Inner.BorderStyle = BorderStyle.None
        Inner.Font = Fnt(False, 10.5F)
        Inner.BackColor = ColSoft
        Inner.ForeColor = ColInk
        Controls.Add(Inner)
        AddHandler Inner.GotFocus, Sub() Invalidate()
        AddHandler Inner.LostFocus, Sub() Invalidate()
        PositionInner()
    End Sub

    Public Property Value As String
        Get
            Return Inner.Text
        End Get
        Set(newValue As String)
            Inner.Text = newValue
        End Set
    End Property

    Public Property HasError As Boolean
        Get
            Return _hasError
        End Get
        Set(newValue As Boolean)
            _hasError = newValue
            Invalidate()
        End Set
    End Property

    Private Sub PositionInner()
        Inner.Left = 14
        Inner.Width = Math.Max(Width - 28, 10)
        Inner.Top = Math.Max((Height - Inner.Height) \ 2, 0)
    End Sub

    Protected Overrides Sub OnResize(eventargs As EventArgs)
        MyBase.OnResize(eventargs)
        PositionInner()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Inner.Focus()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        FillRound(g, rect, 12, ColSoft)
        Dim borderColor As Color = ColLine
        Dim w As Single = 1.0F
        If _hasError Then
            borderColor = ColRed
            w = 2.0F
        ElseIf Inner.Focused Then
            borderColor = ColBlue
            w = 2.0F
        End If
        StrokeRound(g, rect, 12, borderColor, w)
    End Sub

End Class

' ---------------------------------------------------------------------------
' Dropdown field: click to choose from a list
' ---------------------------------------------------------------------------
<DesignerCategory("")>
Public Class PickerField
    Inherits RoundButton

    Private _items As String() = New String() {}
    Private _value As String = ""
    Public Event ValueChanged()

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(newValue As String)
            _value = newValue
            Text = newValue
            Invalidate()
        End Set
    End Property

    Public Sub SetItems(items As String(), selected As String)
        _items = items
        If items.Contains(selected) Then
            Value = selected
        ElseIf items.Length > 0 Then
            Value = items(0)
        Else
            Value = ""
        End If
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Dim menu As ContextMenuStrip = UI.MakeMenu()
        For Each entry In _items
            Dim mi As New ToolStripMenuItem(entry)
            mi.Tag = entry
            mi.ForeColor = ColInk
            mi.Font = Fnt(False, 10.0F, entry = _value)
            mi.Padding = New Padding(4, 6, 4, 6)
            AddHandler mi.Click, AddressOf MenuItemClicked
            menu.Items.Add(mi)
        Next
        menu.MinimumSize = New Size(Width, 0)
        menu.Show(Me, New Point(0, Height + 4))
    End Sub

    Private Sub MenuItemClicked(sender As Object, e As EventArgs)
        Dim chosen As String = CStr(DirectCast(sender, ToolStripMenuItem).Tag)
        If chosen <> _value Then
            Value = chosen
            RaiseEvent ValueChanged()
        End If
    End Sub

End Class

' ---------------------------------------------------------------------------
' Date field: click to open a calendar
' ---------------------------------------------------------------------------
<DesignerCategory("")>
Public Class DateField
    Inherits RoundButton

    Private _value As Date? = Date.Today
    Public Property AllowEmpty As Boolean = False
    Public Event ValueChanged()

    Public Sub New()
        UpdateText()
    End Sub

    Public Property Value As Date?
        Get
            Return _value
        End Get
        Set(newValue As Date?)
            _value = newValue
            UpdateText()
        End Set
    End Property

    Private Sub UpdateText()
        If _value.HasValue Then
            Text = _value.Value.ToString("MMM d, yyyy", CultureInfo.CurrentCulture)
            DimText = False
        Else
            Text = "No date"
            DimText = True
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Dim cal As New MonthCalendar()
        cal.MaxSelectionCount = 1
        cal.SetDate(If(_value.HasValue, _value.Value, Date.Today))

        Dim host As New ToolStripControlHost(cal)
        host.Margin = New Padding(0)
        host.Padding = New Padding(0)

        Dim drop As New ToolStripDropDown()
        drop.Padding = New Padding(0)
        drop.Items.Add(host)

        AddHandler cal.DateSelected,
            Sub(s As Object, ev As DateRangeEventArgs)
                Value = ev.Start.Date
                RaiseEvent ValueChanged()
                drop.Close()
            End Sub

        If AllowEmpty Then
            Dim clear As New ToolStripButton("Clear date")
            AddHandler clear.Click,
                Sub(s As Object, ev As EventArgs)
                    Value = Nothing
                    RaiseEvent ValueChanged()
                    drop.Close()
                End Sub
            drop.Items.Add(clear)
        End If

        drop.Show(Me, New Point(0, Height + 4))
    End Sub

End Class

' ---------------------------------------------------------------------------
' Factory helpers and the themed popup menu
' ---------------------------------------------------------------------------
Public Enum BtnKind
    Primary
    Secondary
    IconOnly
    Danger
    Field
End Enum

Friend Class MenuColors
    Inherits ProfessionalColorTable

    Public Overrides ReadOnly Property MenuItemSelected As Color
        Get
            Return Mix(ColRaised, ColInk, 0.12F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
        Get
            Return Mix(ColRaised, ColInk, 0.12F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
        Get
            Return Mix(ColRaised, ColInk, 0.12F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
        Get
            Return Mix(ColRaised, ColInk, 0.18F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
        Get
            Return Mix(ColRaised, ColInk, 0.18F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemBorder As Color
        Get
            Return Mix(ColRaised, ColInk, 0.12F)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuBorder As Color
        Get
            Return ColLine
        End Get
    End Property
    Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
        Get
            Return ColRaised
        End Get
    End Property
    Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
        Get
            Return ColRaised
        End Get
    End Property
    Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
        Get
            Return ColRaised
        End Get
    End Property
    Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
        Get
            Return ColRaised
        End Get
    End Property
    Public Overrides ReadOnly Property SeparatorDark As Color
        Get
            Return ColLine
        End Get
    End Property
    Public Overrides ReadOnly Property SeparatorLight As Color
        Get
            Return ColLine
        End Get
    End Property
End Class

Public Module UI

    Public Function MakeMenu() As ContextMenuStrip
        Dim menu As New ContextMenuStrip()
        Dim renderer As New ToolStripProfessionalRenderer(New MenuColors())
        renderer.RoundedEdges = False
        menu.Renderer = renderer
        menu.BackColor = ColRaised
        menu.ForeColor = ColInk
        menu.Font = Fnt(False, 10.0F)
        menu.ShowImageMargin = False
        menu.Padding = New Padding(4)
        Return menu
    End Function

    Public Sub StyleButton(b As RoundButton, kind As BtnKind)
        Select Case kind
            Case BtnKind.Primary
                b.BackColor = ColGreenDeep
                b.ForeColor = Color.White
                b.BorderColor = ColGreenDeep
            Case BtnKind.IconOnly
                b.BackColor = ColSurface
                b.ForeColor = ColGold
                b.BorderColor = ColLine
            Case BtnKind.Danger
                b.BackColor = ColRaised
                b.ForeColor = ColRed
                b.BorderColor = ColLineSoft
            Case BtnKind.Field
                b.BackColor = ColSoft
                b.ForeColor = ColInk
                b.BorderColor = ColLine
            Case Else
                b.BackColor = ColRaised
                b.ForeColor = ColInk
                b.BorderColor = ColLineSoft
        End Select
    End Sub

    Public Function MakeButton(text As String, kind As BtnKind, width As Integer, height As Integer) As RoundButton
        Dim b As New RoundButton()
        b.Text = text
        b.Size = New Size(width, height)
        b.Font = Fnt(False, 10.0F, True)
        StyleButton(b, kind)
        Return b
    End Function

    Public Function MakeInput(placeholder As String, maxLength As Integer) As RoundedTextBox
        Dim t As New RoundedTextBox()
        t.Inner.PlaceholderText = placeholder
        t.Inner.MaxLength = maxLength
        Return t
    End Function

    Public Function MakePicker(items As String(), selected As String) As PickerField
        Dim p As New PickerField()
        p.Font = Fnt(False, 10.5F)
        p.LeftAligned = True
        p.ShowChevron = True
        StyleButton(p, BtnKind.Field)
        p.Height = 40
        p.SetItems(items, selected)
        Return p
    End Function

    Public Function MakeDate(value As Date?, allowEmpty As Boolean) As DateField
        Dim d As New DateField()
        d.Font = Fnt(False, 10.5F)
        d.LeftAligned = True
        d.ShowChevron = True
        d.AllowEmpty = allowEmpty
        StyleButton(d, BtnKind.Field)
        d.Height = 40
        d.Value = value
        Return d
    End Function

End Module
