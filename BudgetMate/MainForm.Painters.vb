Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

' All the custom drawing for the dashboard cards.
Partial Public Class MainForm

    Private Shared Function WithAlpha(c As Color, alpha As Integer) As Color
        Return Color.FromArgb(alpha, c.R, c.G, c.B)
    End Function

    ' Small round icon + title (+ optional subtitle), like the panel headings in the web version.
    Private Shared Sub DrawHeading(g As Graphics, x As Integer, y As Integer, glyph As String, tint As Color, title As String, subtitle As String)
        Dim iconRect As New Rectangle(x, y, 34, 34)
        FillRound(g, iconRect, 11, WithAlpha(tint, 40))
        TxtIn(g, glyph, Sym(12.0F), tint, iconRect, TextFormatFlags.HorizontalCenter)
        If String.IsNullOrEmpty(subtitle) Then
            Txt(g, title, Fnt(True, 11.5F, True), ColInk, x + 46, y + 7)
        Else
            Txt(g, title, Fnt(True, 11.5F, True), ColInk, x + 46, y - 1)
            Txt(g, subtitle, Fnt(False, 8.5F), ColMuted, x + 46, y + 20)
        End If
    End Sub

    ' Centered empty-state block.
    Private Shared Sub DrawEmpty(g As Graphics, w As Integer, topY As Integer, glyph As String, title As String, message As String)
        Dim circle As New Rectangle((w - 52) \ 2, topY, 52, 52)
        FillRound(g, circle, 26, WithAlpha(ColGreen, 36))
        TxtIn(g, glyph, Sym(16.0F), ColGreen, circle, TextFormatFlags.HorizontalCenter)
        TxtIn(g, title, Fnt(True, 11.5F, True), ColInk, New Rectangle(0, topY + 62, w, 24), TextFormatFlags.HorizontalCenter)
        TxtIn(g, message, Fnt(False, 9.5F), ColMuted, New Rectangle(20, topY + 88, w - 40, 22), TextFormatFlags.HorizontalCenter)
    End Sub

    ' ======================================================================
    Private Sub PaintLoading(g As Graphics, s As Surface)
        Dim mark As New Rectangle((s.Width - 56) \ 2, s.Height \ 2 - 60, 56, 56)
        Using path As GraphicsPath = RoundedPath(mark, 18)
            Using br As New LinearGradientBrush(mark, Color.FromArgb(40, 222, 168), Color.FromArgb(7, 153, 111), 45.0F)
                g.FillPath(br, path)
            End Using
        End Using
        TxtIn(g, Peso, Fnt(True, 20.0F, True), Color.FromArgb(5, 40, 33), mark, TextFormatFlags.HorizontalCenter)
        TxtIn(g, "Loading your data...", Fnt(False, 11.0F), ColMuted, New Rectangle(0, mark.Bottom + 14, s.Width, 26), TextFormatFlags.HorizontalCenter)
    End Sub

    Private Sub PaintToast(g As Graphics, s As Surface)
        FillDot(g, 18, (s.Height - 10) \ 2, 10, ColGreen)
        TxtIn(g, _toastText, Fnt(False, 10.0F), ColInk, New Rectangle(38, 0, s.Width - 52, s.Height), TextFormatFlags.Left)
    End Sub

    Private Sub PaintHeader(g As Graphics, s As Surface)
        Dim h As Integer = s.Height
        Dim mark As New Rectangle(36, (h - 44) \ 2, 44, 44)
        Using path As GraphicsPath = RoundedPath(mark, 15)
            Using br As New LinearGradientBrush(mark, Color.FromArgb(40, 222, 168), Color.FromArgb(7, 153, 111), 45.0F)
                g.FillPath(br, path)
            End Using
        End Using
        TxtIn(g, Peso, Fnt(True, 16.0F, True), Color.FromArgb(5, 40, 33), mark, TextFormatFlags.HorizontalCenter)
        Txt(g, "BudgetMate", Fnt(True, 14.0F, True), ColInk, 92, mark.Y + 3)
        Txt(g, "Money, made clearer.", Fnt(False, 8.5F), ColMuted, 93, mark.Y + 26)

        Dim syncFont As Font = Fnt(False, 9.0F)
        Dim syncText As String = "Local mode"
        Dim sx As Integer = _syncRight - Measure(syncText, syncFont)
        FillDot(g, sx - 16, h \ 2 - 4, 8, ColMuted)
        Txt(g, syncText, syncFont, ColMuted, sx, h \ 2 - syncFont.Height \ 2)

        Using pen As New Pen(ColLineSoft)
            g.DrawLine(pen, 0, h - 1, s.Width, h - 1)
        End Using
    End Sub

    Private Sub PaintWelcome(g As Graphics, s As Surface)
        Txt(g, "YOUR FINANCIAL OVERVIEW", Fnt(False, 8.0F, True), ColGreen, 2, 8)
        Dim headFont As Font = Fnt(True, 22.0F, True)
        Dim first As String = "Make every peso"
        Txt(g, first, headFont, ColInk, 0, 28)
        Txt(g, "count.", headFont, ColGreen, Measure(first, headFont) + 9, 28)
        Txt(g, "A clearer picture of your money, one transaction at a time.", Fnt(False, 10.0F), ColMuted, 2, 74)

        Dim labelX As Integer = s.Width - 42 - 8 - 170
        TxtIn(g, "Viewing month", Fnt(False, 8.5F), ColMuted, New Rectangle(labelX, 24, 170, 20), TextFormatFlags.HorizontalCenter)
        TxtIn(g, MonthTitle(_month), Fnt(True, 12.0F, True), ColInk, New Rectangle(labelX, 48, 170, 42), TextFormatFlags.HorizontalCenter)
    End Sub

    Private Sub PaintStat(g As Graphics, s As Surface, index As Integer)
        Dim label As String = ""
        Dim value As String = ""
        Dim foot As String = ""
        Dim glyph As String = ""
        Dim valueColor As Color = ColInk
        Dim footColor As Color = ColMuted
        Dim tint As Color = ColGreen
        Dim showBar As Boolean = False

        Select Case index
            Case 0
                label = "TOTAL INCOME"
                value = Money(_sum.Income)
                glyph = Glyphs.TrendUp
                tint = ColGreen
                If _sum.IncomeChange.HasValue Then
                    Dim up As Boolean = _sum.IncomeChange.Value >= 0
                    foot = If(up, Glyphs.ArrowUp, Glyphs.ArrowDown) & " " & Math.Abs(_sum.IncomeChange.Value).ToString() & "% vs last month"
                    footColor = If(up, ColGreen, ColRed)
                Else
                    foot = "Income this month"
                End If
            Case 1
                label = "TOTAL EXPENSES"
                value = Money(_sum.Expenses)
                glyph = Glyphs.TrendDown
                tint = ColRed
                If _sum.ExpenseChange.HasValue Then
                    Dim up As Boolean = _sum.ExpenseChange.Value > 0
                    foot = If(_sum.ExpenseChange.Value >= 0, Glyphs.ArrowUp, Glyphs.ArrowDown) & " " & Math.Abs(_sum.ExpenseChange.Value).ToString() & "% vs last month"
                    footColor = If(up, ColRed, ColGreen)
                Else
                    foot = "Spending this month"
                End If
            Case 2
                label = "NET BALANCE"
                value = Money(_sum.Balance)
                glyph = Glyphs.Diamond
                tint = ColBlue
                If _sum.Balance < 0D Then valueColor = ColRed
                foot = "Income minus expenses"
            Case Else
                label = "SAVINGS RATE"
                value = _sum.SavingsRate.ToString() & "%"
                glyph = Glyphs.Target
                tint = ColGold
                showBar = True
                foot = "Of monthly income retained"
        End Select

        Txt(g, label, Fnt(False, 8.0F, True), ColMuted, 22, 22)
        Dim iconRect As New Rectangle(s.Width - 22 - 36, 16, 36, 36)
        FillRound(g, iconRect, 12, WithAlpha(tint, 38))
        TxtIn(g, glyph, Sym(13.0F), tint, iconRect, TextFormatFlags.HorizontalCenter)
        Txt(g, value, Fnt(True, 18.0F, True), valueColor, 22, 50)

        If showBar Then
            ProgressBar(g, New Rectangle(22, 94, s.Width - 44, 6), Math.Max(0, _sum.SavingsRate), ColGold, ColLineSoft)
            Txt(g, foot, Fnt(False, 8.5F), footColor, 22, 106)
        Else
            Txt(g, foot, Fnt(False, 8.5F), footColor, 22, 100)
        End If
    End Sub

    ' ======================================================================
    Private Sub PaintCashflow(g As Graphics, s As Surface)
        Dim w As Integer = s.Width
        Dim h As Integer = s.Height
        DrawHeading(g, 24, 22, Glyphs.Bolt, ColGreen, "Cash flow history", "")
        TxtRight(g, "Income vs expenses", Fnt(False, 8.5F), ColMuted, w - 24, 32)

        If _days.Count = 0 Then
            DrawEmpty(g, w, 100, Glyphs.Sparkle, "Your money story starts here", "Add a transaction to see your cash flow.")
            Return
        End If

        Dim maxValue As Decimal = 1D
        For Each d In _days
            maxValue = Math.Max(maxValue, Math.Max(d.Income, d.Expense))
        Next

        Dim plot As New Rectangle(30, 84, w - 60, h - 84 - 72)
        Using pen As New Pen(ColLineSoft)
            For i As Integer = 0 To 4
                Dim gy As Integer = plot.Top + CInt(plot.Height * i / 4.0)
                g.DrawLine(pen, plot.Left, gy, plot.Right, gy)
            Next
        End Using

        Dim slot As Double = plot.Width / _days.Count
        Dim barW As Integer = CInt(Math.Min(24.0, slot * 0.26))
        For i As Integer = 0 To _days.Count - 1
            Dim d As DayTotal = _days(i)
            Dim cx As Integer = plot.Left + CInt(slot * (i + 0.5))
            Dim incH As Integer = CInt(plot.Height * CDbl(d.Income / maxValue))
            Dim expH As Integer = CInt(plot.Height * CDbl(d.Expense / maxValue))
            If d.Income > 0D Then
                incH = Math.Max(incH, 4)
                FillRound(g, New Rectangle(cx - barW - 2, plot.Bottom - incH, barW, incH), 4, ColGreen)
            End If
            If d.Expense > 0D Then
                expH = Math.Max(expH, 4)
                FillRound(g, New Rectangle(cx + 2, plot.Bottom - expH, barW, expH), 4, ColRed)
            End If
            TxtIn(g, d.DayNumber.ToString(), Fnt(False, 8.0F), ColMuted, New Rectangle(cx - 20, plot.Bottom + 6, 40, 18), TextFormatFlags.HorizontalCenter)
        Next

        Dim ly As Integer = h - 34
        FillRound(g, New Rectangle(24, ly + 3, 10, 10), 3, ColGreen)
        Txt(g, "Income", Fnt(False, 8.5F), ColMuted, 40, ly)
        FillRound(g, New Rectangle(104, ly + 3, 10, 10), 3, ColRed)
        Txt(g, "Expenses", Fnt(False, 8.5F), ColMuted, 120, ly)
    End Sub

    Private Sub PaintDonut(g As Graphics, s As Surface)
        Dim w As Integer = s.Width
        DrawHeading(g, 24, 22, Glyphs.DonutIcon, ColPurple, "Expense breakdown", "")

        Dim size As Integer = 170
        Dim ring As Integer = 24
        Dim ox As Integer = 24
        Dim oy As Integer = 80
        Dim arcRect As New Rectangle(ox + ring \ 2, oy + ring \ 2, size - ring, size - ring)

        Dim total As Decimal = 0D
        For Each c In Catalog.ExpenseCategories
            total += _sum.ByCategory(c.Name)
        Next

        g.SmoothingMode = SmoothingMode.AntiAlias
        Using track As New Pen(ColLine, ring)
            g.DrawEllipse(track, arcRect)
        End Using
        If total > 0D Then
            Dim start As Single = -90.0F
            For Each c In Catalog.ExpenseCategories
                Dim v As Decimal = _sum.ByCategory(c.Name)
                If v <= 0D Then Continue For
                Dim sweep As Single = CSng(CDbl(v / total) * 360.0)
                Using pen As New Pen(c.Tint, ring)
                    g.DrawArc(pen, arcRect, start, Math.Max(sweep - 1.2F, 0.6F))
                End Using
                start += sweep
            Next
        End If

        Dim hole As New Rectangle(ox + ring, oy + ring, size - 2 * ring, size - 2 * ring)
        TxtIn(g, "Total spent", Fnt(False, 8.0F), ColMuted, New Rectangle(hole.X, hole.Y + hole.Height \ 2 - 22, hole.Width, 18), TextFormatFlags.HorizontalCenter)
        TxtIn(g, Money(_sum.Expenses), Fnt(True, 10.5F, True), ColInk, New Rectangle(hole.X - 4, hole.Y + hole.Height \ 2 - 4, hole.Width + 8, 24), TextFormatFlags.HorizontalCenter)

        Dim legendX As Integer = ox + size + 30
        Dim legendW As Integer = w - 24 - legendX
        If legendW < 120 Then Return
        Dim rowY As Integer = 76
        For Each c In Catalog.ExpenseCategories
            Dim v As Decimal = _sum.ByCategory(c.Name)
            Dim pct As Integer = If(total > 0D, CInt(Math.Round(v / total * 100D, MidpointRounding.AwayFromZero)), 0)
            FillRound(g, New Rectangle(legendX, rowY + 6, 9, 9), 3, c.Tint)
            TxtIn(g, c.Name, Fnt(False, 9.0F), ColInk, New Rectangle(legendX + 18, rowY, legendW - 18 - 44, 22), TextFormatFlags.Left)
            TxtRight(g, pct.ToString() & "%", Fnt(False, 9.0F, True), ColMuted, legendX + legendW, rowY + 3)
            rowY += 25
        Next
    End Sub

    ' ======================================================================
    Private Sub PaintLimits(g As Graphics, s As Surface)
        DrawHeading(g, 24, 22, Glyphs.Target, ColCoral, "Monthly budget limits", "Keep your spending intentional.")

        Dim cols As Integer = 3
        Dim gap As Integer = 12
        Dim itemW As Integer = (s.Width - 48 - gap * (cols - 1)) \ cols
        Dim itemH As Integer = 86
        For i As Integer = 0 To Catalog.ExpenseCategories.Length - 1
            Dim c As CategoryInfo = Catalog.ExpenseCategories(i)
            Dim r As New Rectangle(24 + (i Mod cols) * (itemW + gap), 80 + (i \ cols) * (itemH + gap), itemW, itemH)
            FillRound(g, r, 14, ColSoft)
            StrokeRound(g, r, 14, ColLineSoft)

            Dim spent As Decimal = _sum.ByCategory(c.Name)
            Dim limit As Decimal = 0D
            _data.Limits.TryGetValue(c.Name, limit)
            Dim pct As Double = If(limit > 0D, CDbl(spent / limit) * 100.0, If(spent > 0D, 100.0, 0.0))

            FillRound(g, New Rectangle(r.X + 14, r.Y + 17, 8, 8), 3, c.Tint)
            Txt(g, c.Name, Fnt(False, 9.5F, True), ColInk, r.X + 30, r.Y + 11)

            Dim limitText As String = "/ " & Money(limit)
            TxtRight(g, limitText, Fnt(False, 8.5F), ColMuted, r.Right - 14, r.Y + 13)
            TxtRight(g, Money(spent), Fnt(False, 9.5F, True), ColInk, r.Right - 14 - Measure(limitText, Fnt(False, 8.5F)) - 6, r.Y + 11)

            Dim barColor As Color = If(pct >= 100.0, ColRed, If(pct >= 80.0, ColGold, ColGreen))
            ProgressBar(g, New Rectangle(r.X + 14, r.Y + 42, r.Width - 28, 6), pct, barColor, ColLine)

            Txt(g, CInt(Math.Round(pct)).ToString() & "% used", Fnt(False, 8.0F), ColMuted, r.X + 14, r.Y + 58)
            If pct >= 100.0 Then
                TxtRight(g, "Over limit", Fnt(False, 8.0F, True), ColRed, r.Right - 14, r.Y + 58)
            ElseIf pct >= 80.0 Then
                TxtRight(g, "Near limit", Fnt(False, 8.0F, True), ColGold, r.Right - 14, r.Y + 58)
            Else
                TxtRight(g, Money(Math.Max(limit - spent, 0D)) & " left", Fnt(False, 8.0F), ColMuted, r.Right - 14, r.Y + 58)
            End If
        Next
    End Sub

    ' ======================================================================
    Private Sub PaintWallets(g As Graphics, s As Surface)
        DrawHeading(g, 24, 22, Glyphs.Diamond, ColGreen, "Wallets & accounts", "")
        Txt(g, "Manual balances for Philippine banks, cash, and e-wallets.", Fnt(False, 8.5F), ColMuted, 24, 62)

        Dim gap As Integer = 11
        Dim cardW As Integer = (s.Width - 48 - gap) \ 2
        Dim cardH As Integer = 112
        Dim count As Integer = _data.Wallets.Count
        For i As Integer = 0 To count - 1
            Dim wallet As Wallet = _data.Wallets(i)
            Dim r As New Rectangle(24 + (i Mod 2) * (cardW + gap), 96 + (i \ 2) * (cardH + gap), cardW, cardH)
            FillRound(g, r, 14, ColSoft)
            StrokeRound(g, r, 14, ColLine)

            Dim tint As Color = wallet.AsColor
            Dim mark As New Rectangle(r.X + 14, r.Y + 12, 32, 32)
            FillRound(g, mark, 10, WithAlpha(tint, 45))
            TxtIn(g, Glyphs.Diamond, Sym(12.0F), tint, mark, TextFormatFlags.HorizontalCenter)

            Dim removeRect As New Rectangle(r.Right - 36, r.Y + 8, 28, 28)
            TxtIn(g, Glyphs.Times, Fnt(False, 13.0F), ColMuted, removeRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(removeRect, "wallet-del:" & wallet.Id)

            Txt(g, wallet.Provider & " " & Glyphs.Middot & " " & Catalog.KindName(wallet.Kind), Fnt(False, 8.0F), ColMuted, r.X + 14, r.Y + 50)
            Txt(g, Money(WalletBalance(wallet)), Fnt(True, 12.0F, True), ColInk, r.X + 14, r.Y + 65)
            TxtIn(g, wallet.Name, Fnt(False, 8.5F), ColMuted, New Rectangle(r.X + 14, r.Y + 88, r.Width - 28, 18), TextFormatFlags.Left)
        Next

        If _data.Transfers.Count > 0 Then
            Dim rows As Integer = (count + 1) \ 2
            Dim y As Integer = 96 + rows * (cardH + gap) + 8
            Txt(g, "Recent transfers", Fnt(False, 9.0F, True), ColInk, 24, y)
            y += 26
            For Each t In _data.Transfers.AsEnumerable().Reverse().Take(3)
                Dim label As String = WalletName(t.FromId) & "  " & Glyphs.ArrowRight & "  " & WalletName(t.ToId)
                TxtIn(g, label, Fnt(False, 9.0F), ColMuted, New Rectangle(24, y, s.Width - 48 - 110, 22), TextFormatFlags.Left)
                TxtRight(g, Money(t.Amount), Fnt(False, 9.0F, True), ColInk, s.Width - 24, y + 3)
                y += 28
            Next
        End If
    End Sub

    Private Sub PaintGoals(g As Graphics, s As Surface)
        DrawHeading(g, 24, 22, Glyphs.Target, ColCoral, "Savings goals", "")
        Txt(g, "Give your savings a purpose.", Fnt(False, 8.5F), ColMuted, 24, 62)

        If _data.Goals.Count = 0 Then
            DrawEmpty(g, s.Width, 110, Glyphs.Sparkle, "Big goals start small.", "Create a savings goal and track your progress.")
            Dim linkRect As New Rectangle((s.Width - 220) \ 2, 110 + 120, 220, 24)
            TxtIn(g, "Create your first goal " & Glyphs.ArrowRight, Fnt(False, 9.5F, True), ColGreen, linkRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(linkRect, "goal-new")
            Return
        End If

        Dim y As Integer = 96
        For Each goal In _data.Goals
            Dim r As New Rectangle(24, y, s.Width - 48, 118)
            FillRound(g, r, 14, ColSoft)
            StrokeRound(g, r, 14, ColLineSoft)

            TxtIn(g, goal.Name, Fnt(False, 10.0F, True), ColInk, New Rectangle(r.X + 16, r.Y + 10, r.Width - 70, 22), TextFormatFlags.Left)
            Dim deadlineText As String = If(IsValidDate(goal.Deadline), "Target " & Glyphs.Middot & " " & DateTitle(ParseDate(goal.Deadline, Date.Today)), "No deadline set")
            Txt(g, deadlineText, Fnt(False, 8.0F), ColMuted, r.X + 16, r.Y + 32)

            Dim removeRect As New Rectangle(r.Right - 38, r.Y + 8, 28, 28)
            TxtIn(g, Glyphs.Times, Fnt(False, 13.0F), ColMuted, removeRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(removeRect, "goal-del:" & goal.Id)

            Dim pct As Double = If(goal.Target > 0D, CDbl(goal.Saved / goal.Target) * 100.0, 0.0)
            Dim savedText As String = Money(goal.Saved)
            Txt(g, savedText, Fnt(True, 11.0F, True), ColInk, r.X + 16, r.Y + 54)
            Txt(g, "of " & Money(goal.Target), Fnt(False, 8.5F), ColMuted, r.X + 16 + Measure(savedText, Fnt(True, 11.0F, True)) + 8, r.Y + 58)
            TxtRight(g, CInt(Math.Min(100.0, Math.Round(pct))).ToString() & "%", Fnt(True, 10.0F, True), ColGreen, r.Right - 16, r.Y + 55)
            ProgressBar(g, New Rectangle(r.X + 16, r.Y + 82, r.Width - 32, 6), pct, ColGreen, ColLine)

            Dim addRect As New Rectangle(r.X + 12, r.Y + 92, 130, 22)
            TxtIn(g, "+ Add savings", Fnt(False, 9.0F, True), ColGreen, New Rectangle(r.X + 16, r.Y + 92, 130, 22), TextFormatFlags.Left)
            s.AddRegion(addRect, "goal-add:" & goal.Id)
            y += 118 + 10
        Next
    End Sub

    Private Sub PaintRecurring(g As Graphics, s As Surface)
        DrawHeading(g, 24, 22, Glyphs.Cycle, ColBlue, "Recurring transactions", "")
        TxtRight(g, _data.RecurringRules.Where(Function(r) r.Active).Count().ToString() & " active", Fnt(False, 8.5F), ColMuted, s.Width - 24, 32)
        Txt(g, "Salary, rent, subscriptions and bills that repeat every month.", Fnt(False, 8.5F), ColMuted, 24, 62)

        If _data.RecurringRules.Count = 0 Then
            TxtIn(g, "Nothing repeats yet. Tick ""Repeat this transaction every month"" when adding one.", Fnt(False, 9.5F), ColMuted, New Rectangle(24, 100, s.Width - 48, 30), TextFormatFlags.Left)
            Return
        End If

        Dim y As Integer = 96
        For Each rule In _data.RecurringRules
            Dim isIncome As Boolean = (rule.Type = "income")
            Dim tint As Color = If(isIncome, ColGreen, ColRed)
            Dim mark As New Rectangle(24, y + 11, 34, 34)
            FillRound(g, mark, 10, WithAlpha(tint, 40))
            TxtIn(g, If(isIncome, "IN", "EX"), Fnt(False, 7.5F, True), tint, mark, TextFormatFlags.HorizontalCenter)

            Dim rightX As Integer = s.Width - 24
            Dim removeRect As New Rectangle(rightX - 28, y + 14, 28, 28)
            TxtIn(g, Glyphs.Times, Fnt(False, 13.0F), ColMuted, removeRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(removeRect, "rec-del:" & rule.Id)

            Dim toggleRect As New Rectangle(removeRect.Left - 8 - 84, y + 14, 84, 28)
            FillRound(g, toggleRect, 9, ColRaised)
            StrokeRound(g, toggleRect, 9, ColLineSoft)
            TxtIn(g, If(rule.Active, "Pause", "Resume"), Fnt(False, 8.5F, True), ColInk, toggleRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(toggleRect, "rec-toggle:" & rule.Id)

            Dim amountText As String = If(isIncome, "+", Glyphs.MinusSign) & Money(rule.Amount)
            TxtRight(g, amountText, Fnt(False, 10.0F, True), tint, toggleRect.Left - 18, y + 18)

            Dim textW As Integer = toggleRect.Left - 18 - Measure(amountText, Fnt(False, 10.0F, True)) - 70
            TxtIn(g, rule.Title, Fnt(False, 10.0F, True), If(rule.Active, ColInk, ColMuted), New Rectangle(70, y + 8, Math.Max(textW, 80), 22), TextFormatFlags.Left)
            Txt(g, rule.Category & " " & Glyphs.Middot & " day " & rule.Day.ToString() & " " & Glyphs.Middot & " " & WalletName(rule.WalletId) & If(rule.Active, "", "  (paused)"), Fnt(False, 8.0F), ColMuted, 70, y + 30)

            Using pen As New Pen(ColLineSoft)
                g.DrawLine(pen, 24, y + 61, s.Width - 24, y + 61)
            End Using
            y += 62
        Next
    End Sub

    ' ======================================================================
    Private Sub PaintTxHeading(g As Graphics, s As Surface)
        DrawHeading(g, 24, 22, Glyphs.TableIcon, ColBlue, "Transaction history", "Every peso, accounted for.")
        TxtRight(g, _filtered.Count.ToString() & " of " & _monthTx.Count.ToString() & " records", Fnt(False, 8.5F), ColMuted, s.Width - 24, 32)
    End Sub

    Private Sub PaintTxTable(g As Graphics, s As Surface)
        Dim w As Integer = s.Width
        If _filtered.Count = 0 Then
            If _monthTx.Count = 0 Then
                DrawEmpty(g, w, 60, Peso, "A fresh start", "Add your first transaction to see this month take shape.")
                Dim addRect As New Rectangle((w - 220) \ 2, 60 + 124, 220, 40)
                FillRound(g, addRect, 12, ColGreenDeep)
                TxtIn(g, "+ Add your first transaction", Fnt(False, 9.5F, True), Color.White, addRect, TextFormatFlags.HorizontalCenter)
                s.AddRegion(addRect, "tx-add")
            Else
                DrawEmpty(g, w, 60, Glyphs.Sparkle, "No matching transactions", "Try a different search or reset the filters.")
            End If
            Return
        End If

        Dim usable As Integer = w - 48 - 150
        Dim xTitle As Integer = 24
        Dim xCat As Integer = 24 + CInt(usable * 0.36)
        Dim xPay As Integer = xCat + CInt(usable * 0.17)
        Dim xDate As Integer = xPay + CInt(usable * 0.21)
        Dim xAmountRight As Integer = xDate + CInt(usable * 0.12) + CInt(usable * 0.14)
        Dim xActions As Integer = w - 24 - 140

        Dim headFont As Font = Fnt(False, 8.0F, True)
        Txt(g, "TRANSACTION", headFont, ColMuted, xTitle, 14)
        Txt(g, "CATEGORY", headFont, ColMuted, xCat, 14)
        Txt(g, "PAYMENT", headFont, ColMuted, xPay, 14)
        Txt(g, "DATE", headFont, ColMuted, xDate, 14)
        TxtRight(g, "AMOUNT", headFont, ColMuted, xAmountRight, 14)
        Using pen As New Pen(ColLineSoft)
            g.DrawLine(pen, 0, 39, w, 39)
        End Using

        Dim y As Integer = 40
        For Each t In _filtered
            Dim isIncome As Boolean = t.IsIncome
            Dim tint As Color = If(isIncome, ColGreen, ColRed)

            Dim mark As New Rectangle(xTitle, y + 16, 32, 32)
            FillRound(g, mark, 10, WithAlpha(tint, 40))
            TxtIn(g, If(isIncome, "IN", "EX"), Fnt(False, 7.5F, True), tint, mark, TextFormatFlags.HorizontalCenter)

            Dim titleW As Integer = Math.Max(xCat - xTitle - 44 - 12, 60)
            TxtIn(g, t.Title, Fnt(False, 10.0F, True), ColInk, New Rectangle(xTitle + 44, y + 12, titleW, 22), TextFormatFlags.Left)
            Dim sub1 As String = If(t.Notes.Length > 0, t.Notes, WalletName(t.WalletId))
            TxtIn(g, sub1, Fnt(False, 8.0F), ColMuted, New Rectangle(xTitle + 44, y + 33, titleW, 18), TextFormatFlags.Left)

            Dim pillFont As Font = Fnt(False, 8.5F)
            Dim pillW As Integer = Math.Min(Measure(t.Category, pillFont) + 32, Math.Max(xPay - xCat - 10, 40))
            Dim pill As New Rectangle(xCat, y + 19, pillW, 26)
            FillRound(g, pill, 8, ColRaised)
            Dim dotColor As Color = If(isIncome, ColGreen, Catalog.TintOf(t.Category))
            FillDot(g, pill.X + 10, pill.Y + 9, 8, dotColor)
            TxtIn(g, t.Category, pillFont, ColInk, New Rectangle(pill.X + 24, pill.Y, pill.Width - 28, pill.Height), TextFormatFlags.Left)

            TxtIn(g, t.Payment, Fnt(False, 9.0F), ColInk, New Rectangle(xPay, y + 12, Math.Max(xDate - xPay - 10, 40), 22), TextFormatFlags.Left)
            TxtIn(g, WalletName(t.WalletId), Fnt(False, 8.0F), ColMuted, New Rectangle(xPay, y + 33, Math.Max(xDate - xPay - 10, 40), 18), TextFormatFlags.Left)

            TxtIn(g, DateTitle(t.AsDate), Fnt(False, 9.0F), ColMuted, New Rectangle(xDate, y + 20, Math.Max(xAmountRight - xDate - 110, 60), 24), TextFormatFlags.Left)

            Dim amountText As String = If(isIncome, "+", Glyphs.MinusSign) & Money(t.Amount)
            TxtRight(g, amountText, Fnt(False, 10.0F, True), tint, xAmountRight, y + 22)

            Dim editRect As New Rectangle(xActions, y + 18, 64, 28)
            FillRound(g, editRect, 9, ColRaised)
            StrokeRound(g, editRect, 9, ColLineSoft)
            TxtIn(g, "Edit", Fnt(False, 8.5F, True), ColInk, editRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(editRect, "tx-edit:" & t.Id)

            Dim delRect As New Rectangle(xActions + 72, y + 18, 68, 28)
            FillRound(g, delRect, 9, ColRaised)
            StrokeRound(g, delRect, 9, ColLineSoft)
            TxtIn(g, "Delete", Fnt(False, 8.5F, True), ColRed, delRect, TextFormatFlags.HorizontalCenter)
            s.AddRegion(delRect, "tx-del:" & t.Id)

            Using pen As New Pen(ColLineSoft)
                g.DrawLine(pen, 0, y + 63, w, y + 63)
            End Using
            y += 64
        Next
    End Sub

    Private Sub PaintFooter(g As Graphics, s As Surface)
        Using pen As New Pen(ColLineSoft)
            g.DrawLine(pen, 0, 8, s.Width, 8)
        End Using
        Txt(g, "Made for mindful money moves.", Fnt(False, 8.5F), ColMuted, 0, 26)
        TxtRight(g, "All amounts are in Philippine pesos (" & Peso & ").", Fnt(False, 8.5F), ColMuted, s.Width, 26)
    End Sub

End Class
