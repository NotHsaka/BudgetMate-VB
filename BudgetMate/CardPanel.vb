Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

' A Panel that draws itself as a rounded, bordered card (like the cards in the code-generated version).
Public Class CardPanel
    Inherits Panel

    Private _radius As Integer = 18

    <Category("Appearance"), DefaultValue(18)>
    Public Property Radius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            _radius = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        MyBase.BackColor = Theme.ColSurface
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        ' Fill the corners with whatever is behind the card, then draw the card itself.
        g.Clear(If(Parent IsNot Nothing, Parent.BackColor, SystemColors.Control))
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim r As New Rectangle(0, 0, Width - 1, Height - 1)
        Using path As GraphicsPath = RoundedRect(r, _radius)
            Using fill As New SolidBrush(BackColor)
                g.FillPath(fill, path)
            End Using
            Using border As New Pen(Theme.ColLine)
                g.DrawPath(border, path)
            End Using
        End Using
    End Sub

    Private Shared Function RoundedRect(r As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = Math.Min(radius * 2, Math.Min(r.Width, r.Height))
        If d <= 0 Then
            path.AddRectangle(r)
            Return path
        End If
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

End Class
