Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

' Colors and fonts. Dark is the default, like the web version. Theme.Apply switches palettes.
Public Module Theme

    Public IsDark As Boolean = True

    Public ColBg As Color = Color.FromArgb(13, 20, 37)
    Public ColSurface As Color = Color.FromArgb(27, 39, 59)
    Public ColRaised As Color = Color.FromArgb(32, 46, 67)
    Public ColSoft As Color = Color.FromArgb(23, 35, 56)
    Public ColInk As Color = Color.FromArgb(244, 247, 253)
    Public ColMuted As Color = Color.FromArgb(154, 171, 194)
    Public ColLine As Color = Color.FromArgb(48, 64, 87)
    Public ColLineSoft As Color = Color.FromArgb(45, 58, 78)

    Public ReadOnly ColGreen As Color = Color.FromArgb(22, 189, 139)
    Public ReadOnly ColGreenDeep As Color = Color.FromArgb(7, 153, 111)
    Public ReadOnly ColRed As Color = Color.FromArgb(255, 91, 117)
    Public ReadOnly ColGold As Color = Color.FromArgb(255, 177, 27)
    Public ReadOnly ColBlue As Color = Color.FromArgb(77, 145, 255)
    Public ReadOnly ColPurple As Color = Color.FromArgb(147, 101, 255)
    Public ReadOnly ColCoral As Color = Color.FromArgb(255, 122, 89)

    Public ReadOnly HeadFontName As String = PickFont("Manrope", "Segoe UI")
    Public ReadOnly BodyFontName As String = PickFont("DM Sans", "Segoe UI")

    Private ReadOnly FontCache As New Dictionary(Of String, Font)()

    Private Function PickFont(preferred As String, fallback As String) As String
        Try
            Using family As New FontFamily(preferred)
                Return family.Name
            End Using
        Catch ex As ArgumentException
            Return fallback
        End Try
    End Function

    Public Sub Apply(dark As Boolean)
        IsDark = dark
        If dark Then
            ColBg = Color.FromArgb(13, 20, 37)
            ColSurface = Color.FromArgb(27, 39, 59)
            ColRaised = Color.FromArgb(32, 46, 67)
            ColSoft = Color.FromArgb(23, 35, 56)
            ColInk = Color.FromArgb(244, 247, 253)
            ColMuted = Color.FromArgb(154, 171, 194)
            ColLine = Color.FromArgb(48, 64, 87)
            ColLineSoft = Color.FromArgb(45, 58, 78)
        Else
            ColBg = Color.FromArgb(244, 247, 251)
            ColSurface = Color.FromArgb(255, 255, 255)
            ColRaised = Color.FromArgb(249, 251, 255)
            ColSoft = Color.FromArgb(237, 243, 250)
            ColInk = Color.FromArgb(24, 37, 59)
            ColMuted = Color.FromArgb(108, 123, 145)
            ColLine = Color.FromArgb(220, 228, 239)
            ColLineSoft = Color.FromArgb(232, 236, 242)
        End If
    End Sub

    ' heading = the display font (Manrope if installed). Sizes are in points.
    Public Function Fnt(heading As Boolean, size As Single, Optional bold As Boolean = False, Optional underline As Boolean = False) As Font
        Dim key As String = heading.ToString() & "|" & size.ToString(CultureInfo.InvariantCulture) & "|" & bold.ToString() & "|" & underline.ToString()
        Dim f As Font = Nothing
        If Not FontCache.TryGetValue(key, f) Then
            Dim style As FontStyle = FontStyle.Regular
            If bold Then style = style Or FontStyle.Bold
            If underline Then style = style Or FontStyle.Underline
            f = New Font(If(heading, HeadFontName, BodyFontName), size, style, GraphicsUnit.Point)
            FontCache(key) = f
        End If
        Return f
    End Function

    ' Symbol font for the small icons (arrows, shapes).
    Public Function Sym(size As Single) As Font
        Dim key As String = "sym|" & size.ToString(CultureInfo.InvariantCulture)
        Dim f As Font = Nothing
        If Not FontCache.TryGetValue(key, f) Then
            f = New Font("Segoe UI Symbol", size, FontStyle.Regular, GraphicsUnit.Point)
            FontCache(key) = f
        End If
        Return f
    End Function

End Module

' Dark title bar for the window (Windows 10 20H1 and newer). Safe to ignore if it fails.
Public Module Win

    Private Declare Auto Function DwmSetWindowAttribute Lib "dwmapi.dll" (hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer

    Public Sub ApplyTitleBar(f As Form)
        Try
            If Not f.IsHandleCreated Then Return
            Dim value As Integer = If(Theme.IsDark, 1, 0)
            DwmSetWindowAttribute(f.Handle, 20, value, 4)
            DwmSetWindowAttribute(f.Handle, 19, value, 4)
        Catch ex As Exception
            ' Not critical.
        End Try
    End Sub

End Module

' Text/number formatting.
Public Module Fmt

    Public ReadOnly Peso As String = Char.ConvertFromUtf32(&H20B1)

    Public Function Money(amount As Decimal) As String
        Dim sign As String = If(amount < 0D, "-", "")
        Return sign & Peso & Math.Abs(amount).ToString("N2", CultureInfo.InvariantCulture)
    End Function

    Public Function MonthKey(d As Date) As String
        Return d.ToString("yyyy-MM", CultureInfo.InvariantCulture)
    End Function

    Public Function MonthTitle(d As Date) As String
        Return d.ToString("MMMM yyyy", CultureInfo.CurrentCulture)
    End Function

    Public Function DateTitle(d As Date) As String
        Return d.ToString("MMM d, yyyy", CultureInfo.CurrentCulture)
    End Function

End Module

' Icon characters (drawn with Segoe UI Symbol).
Public Module Glyphs
    Public ReadOnly TrendUp As String = Char.ConvertFromUtf32(&H2197)
    Public ReadOnly TrendDown As String = Char.ConvertFromUtf32(&H2198)
    Public ReadOnly ArrowUp As String = Char.ConvertFromUtf32(&H2191)
    Public ReadOnly ArrowDown As String = Char.ConvertFromUtf32(&H2193)
    Public ReadOnly ArrowRight As String = Char.ConvertFromUtf32(&H2192)
    Public ReadOnly Swap As String = Char.ConvertFromUtf32(&H2194)
    Public ReadOnly Diamond As String = Char.ConvertFromUtf32(&H25C8)
    Public ReadOnly Target As String = Char.ConvertFromUtf32(&H25CE)
    Public ReadOnly DonutIcon As String = Char.ConvertFromUtf32(&H25D4)
    Public ReadOnly Bolt As String = Char.ConvertFromUtf32(&H2301)
    Public ReadOnly TableIcon As String = Char.ConvertFromUtf32(&H25A4)
    Public ReadOnly Cycle As String = Char.ConvertFromUtf32(&H21BB)
    Public ReadOnly Sparkle As String = Char.ConvertFromUtf32(&H2726)
    Public ReadOnly Times As String = Char.ConvertFromUtf32(&HD7)
    Public ReadOnly MinusSign As String = Char.ConvertFromUtf32(&H2212)
    Public ReadOnly Sun As String = Char.ConvertFromUtf32(&H2600)
    Public ReadOnly Moon As String = Char.ConvertFromUtf32(&H263E)
    Public ReadOnly Middot As String = Char.ConvertFromUtf32(&HB7)
    Public ReadOnly Download As String = Char.ConvertFromUtf32(&H21E9)
End Module
