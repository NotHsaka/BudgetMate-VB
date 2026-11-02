Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.Json
Imports System.Windows.Forms

' The main window: header, scrolling dashboard, and all the actions.
' Painting of the cards lives in MainForm.Painters.vb.
<DesignerCategory("")>
Partial Public Class MainForm
    Inherits Form

    Private Class Summary
        Public Income As Decimal
        Public Expenses As Decimal
        Public Balance As Decimal
        Public SavingsRate As Integer
        Public ByCategory As New Dictionary(Of String, Decimal)()
        Public IncomeChange As Integer?
        Public ExpenseChange As Integer?
    End Class

    Private Class DayTotal
        Public DayNumber As Integer
        Public Income As Decimal
        Public Expense As Decimal
    End Class

    ' ---------- State ----------
    Private _data As AppData = AppData.CreateDefault()
    Private _month As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
    Private _searchText As String = ""
    Private _typeFilter As String = "All types"
    Private _catFilter As String = "All categories"
    Private _payFilter As String = "All payment methods"
    Private _toastText As String = ""
    Private _syncRight As Integer = 0

    Private _sum As New Summary()
    Private _monthTx As New List(Of Txn)()
    Private _filtered As New List(Of Txn)()
    Private _days As New List(Of DayTotal)()

    ' ---------- Controls ----------
    Private ReadOnly _loadTimer As New System.Windows.Forms.Timer()
    Private ReadOnly _toastTimer As New System.Windows.Forms.Timer()
    Private _loading As Surface
    Private _root As TableLayoutPanel
    Private _header As Surface
    Private _page As Panel
    Private _content As Panel
    Private _toast As Surface

    Private _btnAdd As RoundButton
    Private _btnData As RoundButton
    Private _btnTheme As RoundButton

    Private _welcome As Surface
    Private _btnPrev As RoundButton
    Private _btnNext As RoundButton
    Private ReadOnly _stats(3) As Surface
    Private _cash As Surface
    Private _donut As Surface
    Private _limits As Surface
    Private _btnManageLimits As RoundButton
    Private _wallets As Surface
    Private _btnTransfer As RoundButton
    Private _btnAddAccount As RoundButton
    Private _goals As Surface
    Private _btnNewGoal As RoundButton
    Private _recurring As Surface
    Private _txPanel As Surface
    Private _txTable As Surface
    Private _search As RoundedTextBox
    Private _typePick As PickerField
    Private _catPick As PickerField
    Private _payPick As PickerField
    Private _btnReset As RoundButton
    Private _footer As Surface

    Public Sub New()
        Text = "BudgetMate - Money, made clearer."
        BackColor = ColBg
        ForeColor = ColInk
        DoubleBuffered = True
        StartPosition = FormStartPosition.CenterScreen

        Dim area As Rectangle = Screen.PrimaryScreen.WorkingArea
        ClientSize = New Size(Math.Min(1360, area.Width - 40), Math.Min(880, area.Height - 80))
        MinimumSize = New Size(Math.Min(1060, area.Width - 40), Math.Min(660, area.Height - 80))

        _loading = NewSurface(0, False, AddressOf PaintLoading)
        _loading.BackColor = ColBg
        _loading.Dock = DockStyle.Fill
        Controls.Add(_loading)

        _loadTimer.Interval = 450
        AddHandler _loadTimer.Tick, AddressOf OnLoadTick
        _toastTimer.Interval = 3200
        AddHandler _toastTimer.Tick, AddressOf OnToastTick
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Win.ApplyTitleBar(Me)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        _loadTimer.Start()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If _toast IsNot Nothing AndAlso _toast.Visible Then
            _toast.Location = New Point((ClientSize.Width - _toast.Width) \ 2, ClientSize.Height - 78)
        End If
    End Sub

    Private Sub OnLoadTick(sender As Object, e As EventArgs)
        _loadTimer.Stop()
        Dim notice As String = ""
        _data = DataStore.Load(notice)
        Theme.Apply(_data.DarkTheme)
        If ApplyRecurring() Then Persist()
        BuildUi()
        _loading.Visible = False
        Win.ApplyTitleBar(Me)
        RefreshData()
        If notice.Length > 0 Then ShowToast(notice)
    End Sub

    ' ======================================================================
    ' Building the screen
    ' ======================================================================
    Private Function NewSurface(radius As Integer, bordered As Boolean, painter As Action(Of Graphics, Surface)) As Surface
        Dim s As New Surface()
        s.Radius = radius
        s.Bordered = bordered
        s.Painter = painter
        AddHandler s.RegionClicked, AddressOf OnRegionClicked
        Return s
    End Function

    Private Sub OnRegionClicked(key As String)
        BeginInvoke(New MethodInvoker(Sub() HandleRegion(key)))
    End Sub

    Private Sub BuildUi()
        SuspendLayout()
        If _root IsNot Nothing Then
            Controls.Remove(_root)
            _root.Dispose()
        End If
        If _toast IsNot Nothing Then
            Controls.Remove(_toast)
            _toast.Dispose()
        End If

        BackColor = ColBg
        ForeColor = ColInk

        _root = New TableLayoutPanel()
        _root.Dock = DockStyle.Fill
        _root.BackColor = ColBg
        _root.ColumnCount = 1
        _root.RowCount = 2
        _root.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        _root.RowStyles.Add(New RowStyle(SizeType.Absolute, 78.0F))
        _root.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        BuildHeader()
        BuildPage()
        _root.Controls.Add(_header, 0, 0)
        _root.Controls.Add(_page, 0, 1)
        Controls.Add(_root)

        _toast = NewSurface(14, True, AddressOf PaintToast)
        _toast.BackColor = ColRaised
        _toast.Visible = False
        Controls.Add(_toast)
        _toast.BringToFront()

        ResumeLayout(True)
        LayoutHeader()
        LayoutAll()
    End Sub

    Private Sub BuildHeader()
        _header = NewSurface(0, False, AddressOf PaintHeader)
        _header.BackColor = ColBg
        _header.Dock = DockStyle.Fill
        _header.Margin = New Padding(0)

        _btnAdd = UI.MakeButton("+ Add transaction", BtnKind.Primary, 178, 42)
        AddHandler _btnAdd.Click, Sub() OpenAdd("expense")

        _btnData = UI.MakeButton("Data", BtnKind.Secondary, 104, 42)
        _btnData.LeftAligned = True
        _btnData.ShowChevron = True
        AddHandler _btnData.Click, Sub() ShowDataMenu()

        _btnTheme = UI.MakeButton(If(IsDark, Glyphs.Sun, Glyphs.Moon), BtnKind.IconOnly, 42, 42)
        _btnTheme.Font = Sym(12.0F)
        AddHandler _btnTheme.Click, Sub() ToggleTheme()

        _header.Controls.Add(_btnAdd)
        _header.Controls.Add(_btnData)
        _header.Controls.Add(_btnTheme)
        AddHandler _header.Resize, Sub() LayoutHeader()
    End Sub

    Private Sub LayoutHeader()
        If _header Is Nothing OrElse _btnAdd Is Nothing Then Return
        Dim y As Integer = (_header.Height - 42) \ 2
        Dim x As Integer = _header.Width - 36 - _btnAdd.Width
        _btnAdd.Location = New Point(x, y)
        x -= 10 + _btnData.Width
        _btnData.Location = New Point(x, y)
        x -= 10 + _btnTheme.Width
        _btnTheme.Location = New Point(x, y)
        _syncRight = x - 20
        _header.Invalidate()
    End Sub

    Private Sub BuildPage()
        _page = New Panel()
        _page.Dock = DockStyle.Fill
        _page.Margin = New Padding(0)
        _page.AutoScroll = True
        _page.BackColor = ColBg
        _content = New Panel()
        _content.BackColor = ColBg
        _content.Location = New Point(0, 0)
        _page.Controls.Add(_content)
        AddHandler _page.ClientSizeChanged, Sub() LayoutAll()

        ' Welcome row
        _welcome = NewSurface(0, False, AddressOf PaintWelcome)
        _welcome.BackColor = ColBg
        _btnPrev = UI.MakeButton(Char.ConvertFromUtf32(&H2039), BtnKind.Secondary, 42, 42)
        _btnNext = UI.MakeButton(Char.ConvertFromUtf32(&H203A), BtnKind.Secondary, 42, 42)
        _btnPrev.Font = Fnt(True, 13.0F, True)
        _btnNext.Font = Fnt(True, 13.0F, True)
        AddHandler _btnPrev.Click, Sub() ChangeMonth(-1)
        AddHandler _btnNext.Click, Sub() ChangeMonth(1)
        _welcome.Controls.Add(_btnPrev)
        _welcome.Controls.Add(_btnNext)
        _content.Controls.Add(_welcome)

        ' Four stat cards
        For i As Integer = 0 To 3
            Dim index As Integer = i
            _stats(i) = NewSurface(18, True, Sub(g As Graphics, s As Surface) PaintStat(g, s, index))
            _content.Controls.Add(_stats(i))
        Next

        ' Charts
        _cash = NewSurface(18, True, AddressOf PaintCashflow)
        _donut = NewSurface(18, True, AddressOf PaintDonut)
        _content.Controls.Add(_cash)
        _content.Controls.Add(_donut)

        ' Limits
        _limits = NewSurface(18, True, AddressOf PaintLimits)
        _btnManageLimits = UI.MakeButton("Manage limits", BtnKind.Secondary, 140, 36)
        AddHandler _btnManageLimits.Click, Sub() OpenLimits()
        _limits.Controls.Add(_btnManageLimits)
        _content.Controls.Add(_limits)

        ' Wallets and goals
        _wallets = NewSurface(18, True, AddressOf PaintWallets)
        _btnTransfer = UI.MakeButton(Glyphs.Swap & "  Transfer", BtnKind.Secondary, 120, 36)
        _btnAddAccount = UI.MakeButton("+ Add account", BtnKind.Secondary, 130, 36)
        AddHandler _btnTransfer.Click, Sub() OpenTransfer()
        AddHandler _btnAddAccount.Click, Sub() OpenAddWallet()
        _wallets.Controls.Add(_btnTransfer)
        _wallets.Controls.Add(_btnAddAccount)
        _content.Controls.Add(_wallets)

        _goals = NewSurface(18, True, AddressOf PaintGoals)
        _btnNewGoal = UI.MakeButton("+ New goal", BtnKind.Secondary, 120, 36)
        AddHandler _btnNewGoal.Click, Sub() OpenNewGoal()
        _goals.Controls.Add(_btnNewGoal)
        _content.Controls.Add(_goals)

        ' Recurring
        _recurring = NewSurface(18, True, AddressOf PaintRecurring)
        _content.Controls.Add(_recurring)

        ' Transaction history (filters + table)
        _txPanel = NewSurface(18, True, AddressOf PaintTxHeading)
        _search = UI.MakeInput("Search transactions...", 80)
        _search.Value = _searchText
        _typePick = UI.MakePicker(New String() {"All types", "Income only", "Expenses only"}, _typeFilter)
        _catPick = UI.MakePicker(CategoryFilterItems(), _catFilter)
        _payPick = UI.MakePicker(PaymentFilterItems(), _payFilter)
        _btnReset = UI.MakeButton("Reset filters", BtnKind.Secondary, 130, 40)
        AddHandler _search.Inner.TextChanged, Sub() OnFilterChanged()
        AddHandler _typePick.ValueChanged, Sub() OnFilterChanged()
        AddHandler _catPick.ValueChanged, Sub() OnFilterChanged()
        AddHandler _payPick.ValueChanged, Sub() OnFilterChanged()
        AddHandler _btnReset.Click, Sub() ResetFilters()
        _txPanel.Controls.Add(_search)
        _txPanel.Controls.Add(_typePick)
        _txPanel.Controls.Add(_catPick)
        _txPanel.Controls.Add(_payPick)
        _txPanel.Controls.Add(_btnReset)

        _txTable = NewSurface(0, False, AddressOf PaintTxTable)
        _txPanel.Controls.Add(_txTable)
        _content.Controls.Add(_txPanel)

        _footer = NewSurface(0, False, AddressOf PaintFooter)
        _footer.BackColor = ColBg
        _content.Controls.Add(_footer)
    End Sub

    Private Shared Function CategoryFilterItems() As String()
        Return (New String() {"All categories"}).Concat(Catalog.ExpenseNames()).Concat(Catalog.IncomeCategories).ToArray()
    End Function

    Private Shared Function PaymentFilterItems() As String()
        Return (New String() {"All payment methods"}).Concat(Catalog.PaymentMethods).ToArray()
    End Function

    ' ======================================================================
    ' Layout
    ' ======================================================================
    Private Sub LayoutAll()
        If _content Is Nothing OrElse _page Is Nothing OrElse _footer Is Nothing Then Return

        Dim pageW As Integer = Math.Max(_page.ClientSize.Width, 1040)
        Dim cw As Integer = Math.Min(pageW - 72, 1320)
        Dim x0 As Integer = (pageW - cw) \ 2
        Dim y As Integer = 26

        ' Welcome row
        _welcome.SetBounds(x0, y, cw, 104)
        _btnNext.Location = New Point(cw - 42, 48)
        _btnPrev.Location = New Point(cw - 42 - 8 - 170 - 8 - 42, 48)
        y += 104 + 16

        ' Stat cards
        Dim gap As Integer = 15
        Dim sw As Integer = (cw - 3 * gap) \ 4
        For i As Integer = 0 To 3
            _stats(i).SetBounds(x0 + i * (sw + gap), y, sw, 132)
        Next
        y += 132 + 20

        ' Charts
        Dim leftW As Integer = CInt((cw - 18) * 1.55 / 2.55)
        _cash.SetBounds(x0, y, leftW, 330)
        _donut.SetBounds(x0 + leftW + 18, y, cw - 18 - leftW, 330)
        y += 330 + 20

        ' Budget limits (3 columns)
        Dim limitRows As Integer = (Catalog.ExpenseCategories.Length + 2) \ 3
        Dim limitsH As Integer = 80 + limitRows * 86 + (limitRows - 1) * 12 + 24
        _limits.SetBounds(x0, y, cw, limitsH)
        _btnManageLimits.Location = New Point(cw - 24 - _btnManageLimits.Width, 22)
        y += limitsH + 20

        ' Wallets + goals
        Dim walletRows As Integer = (_data.Wallets.Count + 1) \ 2
        Dim walletsH As Integer = 96 + walletRows * 112 + Math.Max(walletRows - 1, 0) * 11 + 24
        If _data.Transfers.Count > 0 Then walletsH += 20 + 26 + Math.Min(3, _data.Transfers.Count) * 28
        Dim goalsH As Integer
        If _data.Goals.Count = 0 Then
            goalsH = 96 + 200 + 24
        Else
            goalsH = 96 + _data.Goals.Count * 118 + (_data.Goals.Count - 1) * 10 + 24
        End If
        Dim featureH As Integer = Math.Max(Math.Max(walletsH, goalsH), 380)
        Dim wW As Integer = CInt((cw - 18) * 1.1 / 2.0)
        _wallets.SetBounds(x0, y, wW, featureH)
        _goals.SetBounds(x0 + wW + 18, y, cw - 18 - wW, featureH)
        _btnAddAccount.Location = New Point(wW - 24 - _btnAddAccount.Width, 22)
        _btnTransfer.Location = New Point(_btnAddAccount.Left - 8 - _btnTransfer.Width, 22)
        _btnNewGoal.Location = New Point(_goals.Width - 24 - _btnNewGoal.Width, 22)
        y += featureH + 20

        ' Recurring
        Dim recurringH As Integer = 96 + If(_data.RecurringRules.Count = 0, 80, _data.RecurringRules.Count * 62) + 20
        _recurring.SetBounds(x0, y, cw, recurringH)
        y += recurringH + 20

        ' Transaction history
        Dim tableH As Integer = If(_filtered.Count = 0, 300, 40 + _filtered.Count * 64)
        Dim txH As Integer = 132 + tableH + 20
        _txPanel.SetBounds(x0, y, cw, txH)
        LayoutFilters(cw)
        _txTable.SetBounds(1, 132, cw - 2, tableH)
        y += txH + 12

        ' Footer
        _footer.SetBounds(x0, y, cw, 60)
        y += 60 + 10

        _content.Size = New Size(pageW, y)
    End Sub

    Private Sub LayoutFilters(panelWidth As Integer)
        Dim usable As Integer = panelWidth - 48
        Dim resetW As Integer = 130
        Dim gaps As Integer = 4 * 10
        Dim rest As Integer = usable - resetW - gaps
        Dim searchW As Integer = CInt(rest * 0.32)
        Dim typeW As Integer = CInt(rest * 0.19)
        Dim catW As Integer = CInt(rest * 0.24)
        Dim payW As Integer = rest - searchW - typeW - catW
        Dim x As Integer = 24
        Dim y As Integer = 78
        _search.SetBounds(x, y, searchW, 40)
        x += searchW + 10
        _typePick.SetBounds(x, y, typeW, 40)
        x += typeW + 10
        _catPick.SetBounds(x, y, catW, 40)
        x += catW + 10
        _payPick.SetBounds(x, y, payW, 40)
        x += payW + 10
        _btnReset.SetBounds(x, y, resetW, 40)
    End Sub

    ' ======================================================================
    ' Data -> screen
    ' ======================================================================
    Private Sub ComputeSummary()
        Dim key As String = MonthKey(_month)
        Dim prevKey As String = MonthKey(_month.AddMonths(-1))
        _monthTx = _data.Transactions.Where(Function(t) t.YearMonth = key).ToList()
        Dim prevTx As List(Of Txn) = _data.Transactions.Where(Function(t) t.YearMonth = prevKey).ToList()

        Dim s As New Summary()
        s.Income = _monthTx.Where(Function(t) t.Type = "income").Sum(Function(t) t.Amount)
        s.Expenses = _monthTx.Where(Function(t) t.Type = "expense").Sum(Function(t) t.Amount)
        s.Balance = s.Income - s.Expenses
        Dim priorIncome As Decimal = prevTx.Where(Function(t) t.Type = "income").Sum(Function(t) t.Amount)
        Dim priorExpenses As Decimal = prevTx.Where(Function(t) t.Type = "expense").Sum(Function(t) t.Amount)

        For Each c In Catalog.ExpenseCategories
            s.ByCategory(c.Name) = 0D
        Next
        For Each t In _monthTx
            If t.Type <> "expense" Then Continue For
            Dim bucket As String = If(s.ByCategory.ContainsKey(t.Category), t.Category, "Miscellaneous")
            s.ByCategory(bucket) += t.Amount
        Next

        If s.Income > 0D Then
            s.SavingsRate = CInt(Math.Round((s.Income - s.Expenses) / s.Income * 100D, MidpointRounding.AwayFromZero))
        End If
        If priorExpenses > 0D Then s.ExpenseChange = CInt(Math.Round((s.Expenses - priorExpenses) / priorExpenses * 100D, MidpointRounding.AwayFromZero))
        If priorIncome > 0D Then s.IncomeChange = CInt(Math.Round((s.Income - priorIncome) / priorIncome * 100D, MidpointRounding.AwayFromZero))
        _sum = s

        _days = New List(Of DayTotal)()
        Dim lastDay As Integer = Date.DaysInMonth(_month.Year, _month.Month)
        For d As Integer = 1 To lastDay
            Dim dayText As String = key & "-" & d.ToString("00", CultureInfo.InvariantCulture)
            Dim inc As Decimal = _monthTx.Where(Function(t) t.Date = dayText AndAlso t.Type = "income").Sum(Function(t) t.Amount)
            Dim exp As Decimal = _monthTx.Where(Function(t) t.Date = dayText AndAlso t.Type = "expense").Sum(Function(t) t.Amount)
            If inc > 0D OrElse exp > 0D Then
                Dim item As New DayTotal()
                item.DayNumber = d
                item.Income = inc
                item.Expense = exp
                _days.Add(item)
            End If
        Next
        If _days.Count > 9 Then _days = _days.Skip(_days.Count - 9).ToList()
    End Sub

    Private Sub UpdateFiltered()
        Dim q As String = _searchText.Trim().ToLowerInvariant()
        _filtered = _monthTx _
            .Where(Function(t) _typeFilter = "All types" OrElse (_typeFilter = "Income only" AndAlso t.Type = "income") OrElse (_typeFilter = "Expenses only" AndAlso t.Type = "expense")) _
            .Where(Function(t) _catFilter = "All categories" OrElse t.Category = _catFilter) _
            .Where(Function(t) _payFilter = "All payment methods" OrElse t.Payment = _payFilter) _
            .Where(Function(t) q.Length = 0 OrElse (t.Title & " " & t.Notes & " " & t.Category & " " & t.Payment & " " & WalletName(t.WalletId)).ToLowerInvariant().Contains(q)) _
            .OrderByDescending(Function(t) t.Date) _
            .ThenByDescending(Function(t) t.Id) _
            .ToList()
    End Sub

    Private Sub RefreshData()
        If _content Is Nothing Then Return
        ComputeSummary()
        UpdateFiltered()
        LayoutAll()
        For Each c As Control In _content.Controls
            c.Invalidate(True)
        Next
    End Sub

    Private Function WalletName(id As String) As String
        Dim w As Wallet = _data.Wallets.FirstOrDefault(Function(x) x.Id = id)
        If w Is Nothing Then Return ""
        Return w.Name
    End Function

    Private Function WalletBalance(w As Wallet) As Decimal
        Dim fromTx As Decimal = _data.Transactions.Where(Function(t) t.WalletId = w.Id).Sum(Function(t) If(t.Type = "income", t.Amount, -t.Amount))
        Dim fromTransfers As Decimal = _data.Transfers.Sum(Function(t) If(t.ToId = w.Id, t.Amount, If(t.FromId = w.Id, -t.Amount, 0D)))
        Return w.OpeningBalance + fromTx + fromTransfers
    End Function

    Private Sub Persist()
        If Not DataStore.Save(_data) Then ShowToast("Couldn't save your data to this computer.")
    End Sub

    ' ======================================================================
    ' Filters
    ' ======================================================================
    Private Sub OnFilterChanged()
        _searchText = _search.Value
        _typeFilter = _typePick.Value
        _catFilter = _catPick.Value
        _payFilter = _payPick.Value
        UpdateFiltered()
        LayoutAll()
        _txPanel.Invalidate(True)
    End Sub

    Private Sub ResetFilters()
        _search.Value = ""
        _typePick.Value = "All types"
        _catPick.Value = "All categories"
        _payPick.Value = "All payment methods"
        OnFilterChanged()
    End Sub

    ' ======================================================================
    ' Clicks on painted buttons
    ' ======================================================================
    Private Sub HandleRegion(key As String)
        Dim sep As Integer = key.IndexOf(":"c)
        Dim cmd As String = If(sep >= 0, key.Substring(0, sep), key)
        Dim id As String = If(sep >= 0, key.Substring(sep + 1), "")
        Select Case cmd
            Case "tx-add"
                OpenAdd("expense")
            Case "tx-edit"
                EditTransaction(id)
            Case "tx-del"
                DeleteTransaction(id)
            Case "wallet-del"
                RemoveWallet(id)
            Case "goal-add"
                AddGoalSavings(id)
            Case "goal-del"
                DeleteGoal(id)
            Case "goal-new"
                OpenNewGoal()
            Case "rec-toggle"
                ToggleRecurring(id)
            Case "rec-del"
                DeleteRecurring(id)
        End Select
    End Sub

    Private Sub ChangeMonth(delta As Integer)
        _month = _month.AddMonths(delta)
        RefreshData()
    End Sub

    ' ======================================================================
    ' Transactions
    ' ======================================================================
    Private Sub OpenAdd(kind As String)
        Using dlg As New TransactionDialog(_data, Nothing, kind)
            If dlg.ShowDialog(Me) = DialogResult.OK Then CommitTransaction(dlg.Result, dlg.RepeatMonthly, False)
        End Using
    End Sub

    Private Sub EditTransaction(id As String)
        Dim t As Txn = _data.Transactions.FirstOrDefault(Function(x) x.Id = id)
        If t Is Nothing Then Return
        Using dlg As New TransactionDialog(_data, t, t.Type)
            If dlg.ShowDialog(Me) = DialogResult.OK Then CommitTransaction(dlg.Result, False, True)
        End Using
    End Sub

    Private Sub CommitTransaction(result As Txn, repeat As Boolean, isEdit As Boolean)
        If isEdit Then
            Dim idx As Integer = _data.Transactions.FindIndex(Function(x) x.Id = result.Id)
            If idx >= 0 Then _data.Transactions(idx) = result
        Else
            If repeat Then
                Dim rule As New RecurringRule()
                rule.Id = "repeat-" & result.Id
                rule.Title = result.Title
                rule.Type = result.Type
                rule.Amount = result.Amount
                rule.Category = result.Category
                rule.Payment = result.Payment
                rule.WalletId = result.WalletId
                rule.Notes = result.Notes
                rule.Day = result.AsDate.Day
                rule.StartDate = result.Date
                rule.Active = True
                _data.RecurringRules.Add(rule)
                result.RecurringRuleId = rule.Id
            End If
            _data.Transactions.Add(result)
        End If
        _month = New Date(result.AsDate.Year, result.AsDate.Month, 1)
        Persist()
        RefreshData()
        If isEdit Then
            ShowToast("Transaction updated.")
        ElseIf repeat Then
            ShowToast("Transaction saved with monthly repeat.")
        Else
            ShowToast("Transaction added.")
        End If
    End Sub

    Private Sub DeleteTransaction(id As String)
        Dim t As Txn = _data.Transactions.FirstOrDefault(Function(x) x.Id = id)
        If t Is Nothing Then Return
        Dim answer As DialogResult = MessageBox.Show(Me, "Delete """ & t.Title & """?", "Delete transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer <> DialogResult.Yes Then Return
        _data.Transactions.RemoveAll(Function(x) x.Id = id)
        Persist()
        RefreshData()
        ShowToast("Transaction deleted.")
    End Sub

    ' Creates this month's entry for every active monthly rule that is due. Returns True if anything was added.
    Private Function ApplyRecurring() As Boolean
        Dim today As Date = Date.Today
        Dim ym As String = MonthKey(today)
        Dim added As Boolean = False
        For Each rule As RecurringRule In _data.RecurringRules.ToList()
            Dim current As RecurringRule = rule
            If Not current.Active Then Continue For
            If String.CompareOrdinal(current.StartDate, DateText(today)) > 0 Then Continue For
            Dim dueDay As Integer = Math.Min(current.Day, Date.DaysInMonth(today.Year, today.Month))
            Dim due As Date = New Date(today.Year, today.Month, dueDay)
            If due > today Then Continue For
            If _data.Transactions.Any(Function(t) t.RecurringRuleId = current.Id AndAlso t.YearMonth = ym) Then Continue For

            Dim item As New Txn()
            item.Type = current.Type
            item.Title = current.Title
            item.Amount = current.Amount
            item.Date = DateText(due)
            item.Category = current.Category
            item.Payment = current.Payment
            item.WalletId = current.WalletId
            item.Notes = current.Notes
            item.RecurringRuleId = current.Id
            _data.Transactions.Add(item)
            added = True
        Next
        Return added
    End Function

    Private Sub ToggleRecurring(id As String)
        Dim rule As RecurringRule = _data.RecurringRules.FirstOrDefault(Function(x) x.Id = id)
        If rule Is Nothing Then Return
        rule.Active = Not rule.Active
        If rule.Active Then ApplyRecurring()
        Persist()
        RefreshData()
        ShowToast(If(rule.Active, "Recurring transaction resumed.", "Recurring transaction paused."))
    End Sub

    Private Sub DeleteRecurring(id As String)
        Dim rule As RecurringRule = _data.RecurringRules.FirstOrDefault(Function(x) x.Id = id)
        If rule Is Nothing Then Return
        Dim answer As DialogResult = MessageBox.Show(Me, "Stop repeating """ & rule.Title & """ each month? Past transactions are kept.", "Remove recurring transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer <> DialogResult.Yes Then Return
        _data.RecurringRules.RemoveAll(Function(x) x.Id = id)
        Persist()
        RefreshData()
    End Sub

    ' ======================================================================
    ' Limits, wallets, goals
    ' ======================================================================
    Private Sub OpenLimits()
        Using dlg As New LimitsDialog(_data, MonthTitle(_month))
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                _data.Limits = dlg.Result
                Persist()
                RefreshData()
                ShowToast("Monthly category limits saved.")
            End If
        End Using
    End Sub

    Private Sub OpenAddWallet()
        Using dlg As New WalletDialog(_data)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                _data.Wallets.Add(dlg.Result)
                Persist()
                RefreshData()
                ShowToast("Account added.")
            End If
        End Using
    End Sub

    Private Sub OpenTransfer()
        If _data.Wallets.Count < 2 Then
            ShowToast("Add a second account before recording a transfer.")
            Return
        End If
        Using dlg As New TransferDialog(_data, New Func(Of Wallet, Decimal)(AddressOf WalletBalance))
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                _data.Transfers.Add(dlg.Result)
                Persist()
                RefreshData()
                ShowToast("Transfer recorded without affecting income or expenses.")
            End If
        End Using
    End Sub

    Private Sub RemoveWallet(id As String)
        Dim w As Wallet = _data.Wallets.FirstOrDefault(Function(x) x.Id = id)
        If w Is Nothing Then Return
        If w.Id = "cash" Then
            ShowToast("The default cash account cannot be removed.")
            Return
        End If
        If _data.Transactions.Any(Function(t) t.WalletId = id) OrElse _data.Transfers.Any(Function(t) t.FromId = id OrElse t.ToId = id) Then
            ShowToast("This account has activity and cannot be removed yet.")
            Return
        End If
        Dim answer As DialogResult = MessageBox.Show(Me, "Remove " & w.Name & "?", "Remove account", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer <> DialogResult.Yes Then Return
        _data.Wallets.RemoveAll(Function(x) x.Id = id)
        Persist()
        RefreshData()
    End Sub

    Private Sub OpenNewGoal()
        Using dlg As New GoalDialog()
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                _data.Goals.Add(dlg.Result)
                Persist()
                RefreshData()
                ShowToast("Savings goal created.")
            End If
        End Using
    End Sub

    Private Sub AddGoalSavings(id As String)
        Dim g As Goal = _data.Goals.FirstOrDefault(Function(x) x.Id = id)
        If g Is Nothing Then Return
        Using dlg As New AmountDialog("SAVINGS GOAL", "Add savings", "How much to add to " & g.Name & "? (" & Peso & ")", "Add savings")
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                g.Saved += dlg.Amount
                Persist()
                RefreshData()
            End If
        End Using
    End Sub

    Private Sub DeleteGoal(id As String)
        Dim g As Goal = _data.Goals.FirstOrDefault(Function(x) x.Id = id)
        If g Is Nothing Then Return
        Dim answer As DialogResult = MessageBox.Show(Me, "Delete " & g.Name & "?", "Delete goal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer <> DialogResult.Yes Then Return
        _data.Goals.RemoveAll(Function(x) x.Id = id)
        Persist()
        RefreshData()
    End Sub

    ' ======================================================================
    ' Theme, toast
    ' ======================================================================
    Private Sub ToggleTheme()
        _data.DarkTheme = Not _data.DarkTheme
        Theme.Apply(_data.DarkTheme)
        Persist()
        BuildUi()
        Win.ApplyTitleBar(Me)
        RefreshData()
    End Sub

    Private Sub ShowToast(message As String)
        If _toast Is Nothing Then Return
        _toastText = message
        Dim w As Integer = Math.Min(Measure(message, Fnt(False, 10.0F)) + 48, ClientSize.Width - 40)
        _toast.SetBounds((ClientSize.Width - w) \ 2, ClientSize.Height - 78, w, 46)
        _toast.Visible = True
        _toast.BringToFront()
        _toast.Invalidate()
        _toastTimer.Stop()
        _toastTimer.Start()
    End Sub

    Private Sub OnToastTick(sender As Object, e As EventArgs)
        _toastTimer.Stop()
        If _toast IsNot Nothing Then _toast.Visible = False
    End Sub

    ' ======================================================================
    ' Data menu: export / import / reset
    ' ======================================================================
    Private Sub ShowDataMenu()
        Dim m As ContextMenuStrip = UI.MakeMenu()
        AddMenuItem(m, "Export CSV", AddressOf ExportCsv, False)
        AddMenuItem(m, "Export JSON backup", AddressOf ExportJson, False)
        AddMenuItem(m, "Import JSON / CSV", AddressOf ImportFile, False)
        m.Items.Add(New ToolStripSeparator())
        AddMenuItem(m, "Reset data", AddressOf ResetAll, True)
        m.MinimumSize = New Size(_btnData.Width + 60, 0)
        m.Show(_btnData, New Point(0, _btnData.Height + 4))
    End Sub

    Private Sub AddMenuItem(m As ContextMenuStrip, text As String, handler As Action, danger As Boolean)
        Dim mi As New ToolStripMenuItem(text)
        mi.ForeColor = If(danger, ColRed, ColInk)
        mi.Padding = New Padding(4, 6, 4, 6)
        AddHandler mi.Click, Sub(sender As Object, e As EventArgs) handler()
        m.Items.Add(mi)
    End Sub

    Private Shared Function CsvCell(value As String) As String
        Return """" & If(value, "").Replace("""", """""") & """"
    End Function

    Private Sub ExportCsv()
        Using sfd As New SaveFileDialog()
            sfd.Filter = "CSV file (*.csv)|*.csv"
            sfd.FileName = "budgetmate-transactions-" & DateText(Date.Today) & ".csv"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim sb As New StringBuilder()
                sb.AppendLine("Type,Title,Amount (PHP),Date,Category,Payment Method,Wallet,Notes")
                For Each t In _data.Transactions
                    sb.AppendLine(String.Join(",", New String() {
                        CsvCell(t.Type), CsvCell(t.Title), CsvCell(t.Amount.ToString("0.00", CultureInfo.InvariantCulture)),
                        CsvCell(t.Date), CsvCell(t.Category), CsvCell(t.Payment), CsvCell(WalletName(t.WalletId)), CsvCell(t.Notes)}))
                Next
                File.WriteAllText(sfd.FileName, sb.ToString(), New UTF8Encoding(True))
                ShowToast("CSV exported.")
            Catch ex As Exception
                ShowToast("Couldn't write that file: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ExportJson()
        Using sfd As New SaveFileDialog()
            sfd.Filter = "JSON backup (*.json)|*.json"
            sfd.FileName = "budgetmate-backup-" & DateText(Date.Today) & ".json"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                File.WriteAllText(sfd.FileName, DataStore.ToJson(_data), New UTF8Encoding(False))
                ShowToast("JSON backup exported.")
            Catch ex As Exception
                ShowToast("Couldn't write that file: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Shared Function ParseCsvLine(line As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim sb As New StringBuilder()
        Dim quoted As Boolean = False
        Dim i As Integer = 0
        While i < line.Length
            Dim ch As Char = line(i)
            If ch = """"c AndAlso quoted AndAlso i + 1 < line.Length AndAlso line(i + 1) = """"c Then
                sb.Append(""""c)
                i += 1
            ElseIf ch = """"c Then
                quoted = Not quoted
            ElseIf ch = ","c AndAlso Not quoted Then
                result.Add(sb.ToString())
                sb.Clear()
            Else
                sb.Append(ch)
            End If
            i += 1
        End While
        result.Add(sb.ToString())
        Return result
    End Function

    Private Function ParseCsvTransactions(raw As String) As List(Of Txn)
        Dim lines As String() = raw.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        If lines.Length = 0 Then Throw New ArgumentException("Empty CSV")
        Dim headers As List(Of String) = ParseCsvLine(lines(0)).Select(Function(h) h.Trim().TrimStart(Convert.ToChar(&HFEFF)).ToLowerInvariant()).ToList()
        Dim titleIdx As Integer = headers.FindIndex(Function(h) h = "title" OrElse h = "transaction" OrElse h = "name")
        Dim amountIdx As Integer = headers.FindIndex(Function(h) h.StartsWith("amount"))
        Dim dateIdx As Integer = headers.FindIndex(Function(h) h = "date")
        If titleIdx < 0 OrElse amountIdx < 0 OrElse dateIdx < 0 Then Throw New ArgumentException("Invalid CSV headers")
        Dim typeIdx As Integer = headers.IndexOf("type")
        Dim catIdx As Integer = headers.IndexOf("category")
        Dim payIdx As Integer = headers.FindIndex(Function(h) h = "payment method" OrElse h = "payment")
        Dim walletIdx As Integer = headers.IndexOf("wallet")
        Dim notesIdx As Integer = headers.FindIndex(Function(h) h = "notes" OrElse h = "note")

        Dim list As New List(Of Txn)()
        For n As Integer = 1 To lines.Length - 1
            Dim r As List(Of String) = ParseCsvLine(lines(n))
            If r.Count <= Math.Max(Math.Max(titleIdx, amountIdx), dateIdx) Then Continue For
            Dim amountText As String = r(amountIdx).Replace(Peso, "").Replace("$", "").Replace(",", "").Replace(" ", "")
            Dim amount As Decimal
            If Not Decimal.TryParse(amountText, NumberStyles.Number, CultureInfo.InvariantCulture, amount) OrElse amount <= 0D Then Continue For
            Dim parsedDate As Date
            If Not Date.TryParse(r(dateIdx), CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then Continue For

            Dim t As New Txn()
            t.Type = If(typeIdx >= 0 AndAlso typeIdx < r.Count, r(typeIdx).Trim().ToLowerInvariant(), "expense")
            If t.Type <> "income" Then t.Type = "expense"
            t.Title = r(titleIdx).Trim()
            t.Amount = amount
            t.Date = DateText(parsedDate)
            t.Category = If(catIdx >= 0 AndAlso catIdx < r.Count AndAlso r(catIdx).Trim().Length > 0, r(catIdx).Trim(), "Miscellaneous")
            t.Payment = If(payIdx >= 0 AndAlso payIdx < r.Count AndAlso r(payIdx).Trim().Length > 0, r(payIdx).Trim(), "Cash")
            Dim walletText As String = If(walletIdx >= 0 AndAlso walletIdx < r.Count, r(walletIdx).Trim(), "")
            Dim wallet As Wallet = _data.Wallets.FirstOrDefault(Function(x) x.Name = walletText)
            t.WalletId = If(wallet IsNot Nothing, wallet.Id, _data.Wallets(0).Id)
            t.Notes = If(notesIdx >= 0 AndAlso notesIdx < r.Count, r(notesIdx).Trim(), "")
            If t.Title.Length > 0 Then list.Add(t)
        Next
        Return list
    End Function

    Private Sub ImportFile()
        Using ofd As New OpenFileDialog()
            ofd.Filter = "BudgetMate backup or CSV (*.json;*.csv)|*.json;*.csv"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim raw As String = File.ReadAllText(ofd.FileName)
                Dim incoming As New List(Of Txn)()
                Dim backup As AppData = Nothing
                If ofd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) Then
                    incoming = ParseCsvTransactions(raw)
                Else
                    backup = DataStore.FromJson(raw)
                    incoming = backup.Transactions
                End If

                Dim existing As New HashSet(Of String)(_data.Transactions.Select(Function(t) ImportKey(t)))
                Dim added As Integer = 0
                For Each t In incoming
                    If existing.Add(ImportKey(t)) Then
                        t.Id = NewId()
                        _data.Transactions.Add(t)
                        added += 1
                    End If
                Next

                If backup IsNot Nothing Then
                    For Each pair In backup.Limits
                        _data.Limits(pair.Key) = pair.Value
                    Next
                    If backup.Goals.Count > 0 Then _data.Goals = backup.Goals
                    If backup.RecurringRules.Count > 0 Then _data.RecurringRules = backup.RecurringRules
                    If backup.Transfers.Count > 0 Then _data.Transfers = backup.Transfers
                    If backup.Wallets.Count > 0 Then _data.Wallets = backup.Wallets
                End If

                Persist()
                RefreshData()
                ShowToast("Imported " & added.ToString() & " new transaction" & If(added = 1, "", "s") & "; duplicates were skipped.")
            Catch ex As Exception
                ShowToast("That file isn't a valid BudgetMate JSON backup or supported CSV.")
            End Try
        End Using
    End Sub

    Private Shared Function ImportKey(t As Txn) As String
        Return t.Type & "|" & t.Title.ToLowerInvariant() & "|" & t.Amount.ToString("F2", CultureInfo.InvariantCulture) & "|" & t.Date
    End Function

    Private Sub ResetAll()
        Dim answer As DialogResult = MessageBox.Show(Me, "Reset all BudgetMate data? Export a backup first if you need one.", "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer <> DialogResult.Yes Then Return
        Dim dark As Boolean = _data.DarkTheme
        _data = AppData.CreateDefault()
        _data.DarkTheme = dark
        Persist()
        RefreshData()
        ShowToast("BudgetMate data reset.")
    End Sub

End Class
