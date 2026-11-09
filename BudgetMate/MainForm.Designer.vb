Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        pnlDashboard = New Panel()
        PnlContent = New Panel()
        BottomLeftBGText = New Label()
        BottomRightBGText = New Label()
        TransHistoryPnl = New CardPanel()
        AddTransButton = New Button()
        EmptyTransDesc = New Label()
        EmptyTransactionHeading = New Label()
        FilterReset = New Button()
        PaymentMethodFilter = New ComboBox()
        CategoryFilter = New ComboBox()
        TypeFilter = New ComboBox()
        TransactionSearchBox = New TextBox()
        RecordsCount = New Label()
        TransactionHistoryIcon = New Panel()
        TransactionHistoryHeading = New Label()
        RecTransPnl = New CardPanel()
        ActiveCounter = New Label()
        EmptyRecTransDesc = New Label()
        RecTransDesc = New Label()
        RecTransIcon = New Panel()
        RecTranHeading = New Label()
        ViewMonth = New Label()
        HeadingDescription = New Label()
        MonthNavContainer = New Panel()
        MonthAndYear = New Label()
        NextMonth = New Button()
        PreviousMonth = New Button()
        SavingGoalsPnl = New CardPanel()
        CreateFirstGoal = New Label()
        EmptySavGoalsSubHeading = New Label()
        EmptySavGoalsHeading = New Label()
        EmptySavGoalsIcon = New Panel()
        AddNewGoal = New Button()
        SvGIcon = New Panel()
        SavGoalsHeading = New Label()
        MainHeading = New Label()
        WalletsAndAccsPnl = New CardPanel()
        WalletCard4 = New CardPanel()
        AmtBDO = New Label()
        BDOHeading = New Label()
        WalletCard3 = New CardPanel()
        AmtGCash = New Label()
        GCashHeading = New Label()
        WalletCard2 = New CardPanel()
        AmtMaya = New Label()
        MayaHeading = New Label()
        WalletCard1 = New CardPanel()
        AmtCash = New Label()
        CashHeading = New Label()
        AddAccBtn = New Button()
        TransferBtn = New Button()
        WalletAndAccIcon = New Panel()
        WalletsHeading = New Label()
        SectionLabel = New Label()
        MBLPnl = New CardPanel()
        MBLCardPanel6 = New CardPanel()
        ProgressHealth = New Panel()
        UsedHealth = New Label()
        AmtLeftHealth = New Label()
        slash6 = New Label()
        MBLHealth = New Label()
        LimitHealth = New Label()
        SpentAmtHealth = New Label()
        MBLColorHealth = New Panel()
        MBLCardPanel8 = New CardPanel()
        ProgressMisc = New Panel()
        UsedMisc = New Label()
        AmtLeftMisc = New Label()
        slash8 = New Label()
        LimitMisc = New Label()
        SpentAmtMiscellaneous = New Label()
        MBLMiscellaneous = New Label()
        MBLColorMisc = New Panel()
        MBLCardPanel3 = New CardPanel()
        ProgressTranspo = New Panel()
        UsedTranspo = New Label()
        AmtLeftTranspo = New Label()
        slash3 = New Label()
        LimitTransportation = New Label()
        SpentAmtTranspo = New Label()
        MBLTransportation = New Label()
        MBLColorTranspo = New Panel()
        MBLCardPanel7 = New CardPanel()
        ProgressShop = New Panel()
        UsedShop = New Label()
        AmtLeftShop = New Label()
        slash7 = New Label()
        LimitShopping = New Label()
        SpentAmtShopping = New Label()
        MBLShopping = New Label()
        MBLColorShop = New Panel()
        MBLCardPanel5 = New CardPanel()
        ProgressEnt = New Panel()
        UsedEnt = New Label()
        AmtLeftEnt = New Label()
        slash5 = New Label()
        LimitEnt = New Label()
        SpentAmtEntertainment = New Label()
        MBLEntertainment = New Label()
        MBLColorEnt = New Panel()
        MBLCardPanel4 = New CardPanel()
        ProgressUtil = New Panel()
        UsedUtil = New Label()
        AmtLeftUtil = New Label()
        slash4 = New Label()
        LimitUtilities = New Label()
        SpentAmtUtilities = New Label()
        MBLUtilities = New Label()
        MBLColorUtil = New Panel()
        MBLCardPanel2 = New CardPanel()
        ProgressFood = New Panel()
        UsedFood = New Label()
        AmtLeftFood = New Label()
        slash2 = New Label()
        LimitFood = New Label()
        SpentAmtFood = New Label()
        MBLFood = New Label()
        MBLColorFood = New Panel()
        MBLCardPanel1 = New CardPanel()
        ProgressHousing = New Panel()
        UsedHousing = New Label()
        AmtLeftHousing = New Label()
        slash1 = New Label()
        LimitHousing = New Label()
        SpentAmtHousing = New Label()
        MBLHousing = New Label()
        MBLColorHousing = New Panel()
        Button1 = New Button()
        MBLIcon = New Panel()
        WalAndAcc = New Label()
        TotExpBG = New CardPanel()
        TotExpLbl = New Label()
        ExpAmt = New Label()
        ExpDesc = New Label()
        ExpIcon = New Panel()
        NetBalBG = New CardPanel()
        NetBal = New Label()
        AmtBal = New Label()
        BalDesc = New Label()
        NetBalIcon = New Panel()
        SavRateBG = New CardPanel()
        SavRate = New Label()
        SavRatePerc = New Label()
        SavRateDesc = New Label()
        IconSavRate = New Panel()
        CashFlowBG = New CardPanel()
        ChartArea = New Panel()
        EmptyStateHeadingDesc = New Label()
        EmptyStateHeading = New Label()
        EmptyStateIcon = New Panel()
        IncVSExpDesc = New Label()
        CashFlowHistoryLabel = New Label()
        CashFlowIcon = New Panel()
        CardBG1 = New CardPanel()
        TotalIncome = New Label()
        AmtIncome = New Label()
        IncDesc = New Label()
        IconInc = New Panel()
        ExpBDBG = New CardPanel()
        Perc8 = New Label()
        Perc7 = New Label()
        ExpBDHeading = New Label()
        ExpBdIcon = New Panel()
        DonutChartArea = New Panel()
        DonutChartAmt = New Label()
        DonutChartDesc = New Label()
        ColorLegend1 = New Panel()
        color2 = New Panel()
        color3 = New Panel()
        color4 = New Panel()
        color5 = New Panel()
        color6 = New Panel()
        color7 = New Panel()
        color8 = New Panel()
        Housing = New Label()
        Food = New Label()
        Transportation = New Label()
        Utilities = New Label()
        Entertainment = New Label()
        Shopping = New Label()
        Perc5 = New Label()
        Miscellaneous = New Label()
        Health = New Label()
        Perc1 = New Label()
        Perc2 = New Label()
        Perc3 = New Label()
        Perc6 = New Label()
        Perc4 = New Label()
        pnlHeader = New Panel()
        PictureBox1 = New PictureBox()
        LogoMain = New PictureBox()
        PesoLogo = New Label()
        BudgetMateTitle = New Label()
        Tagline = New Label()
        ToggleTheme = New Button()
        DataDropdown = New ComboBox()
        AddTransaction = New Button()
        pnlDashboard.SuspendLayout()
        PnlContent.SuspendLayout()
        TransHistoryPnl.SuspendLayout()
        RecTransPnl.SuspendLayout()
        MonthNavContainer.SuspendLayout()
        SavingGoalsPnl.SuspendLayout()
        WalletsAndAccsPnl.SuspendLayout()
        WalletCard4.SuspendLayout()
        WalletCard3.SuspendLayout()
        WalletCard2.SuspendLayout()
        WalletCard1.SuspendLayout()
        MBLPnl.SuspendLayout()
        MBLCardPanel6.SuspendLayout()
        MBLCardPanel8.SuspendLayout()
        MBLCardPanel3.SuspendLayout()
        MBLCardPanel7.SuspendLayout()
        MBLCardPanel5.SuspendLayout()
        MBLCardPanel4.SuspendLayout()
        MBLCardPanel2.SuspendLayout()
        MBLCardPanel1.SuspendLayout()
        TotExpBG.SuspendLayout()
        NetBalBG.SuspendLayout()
        SavRateBG.SuspendLayout()
        CashFlowBG.SuspendLayout()
        ChartArea.SuspendLayout()
        CardBG1.SuspendLayout()
        ExpBDBG.SuspendLayout()
        DonutChartArea.SuspendLayout()
        pnlHeader.SuspendLayout()
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LogoMain, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlDashboard
        ' 
        pnlDashboard.Controls.Add(PnlContent)
        pnlDashboard.Controls.Add(pnlHeader)
        pnlDashboard.Dock = DockStyle.Fill
        pnlDashboard.Location = New System.Drawing.Point(0, 0)
        pnlDashboard.Name = "pnlDashboard"
        pnlDashboard.Padding = New Padding(20)
        pnlDashboard.Size = New System.Drawing.Size(1895, 1055)
        pnlDashboard.TabIndex = 0
        ' 
        ' PnlContent
        ' 
        PnlContent.AutoScroll = True
        PnlContent.Controls.Add(BottomLeftBGText)
        PnlContent.Controls.Add(BottomRightBGText)
        PnlContent.Controls.Add(TransHistoryPnl)
        PnlContent.Controls.Add(RecTransPnl)
        PnlContent.Controls.Add(ViewMonth)
        PnlContent.Controls.Add(HeadingDescription)
        PnlContent.Controls.Add(MonthNavContainer)
        PnlContent.Controls.Add(SavingGoalsPnl)
        PnlContent.Controls.Add(MainHeading)
        PnlContent.Controls.Add(WalletsAndAccsPnl)
        PnlContent.Controls.Add(SectionLabel)
        PnlContent.Controls.Add(MBLPnl)
        PnlContent.Controls.Add(TotExpBG)
        PnlContent.Controls.Add(NetBalBG)
        PnlContent.Controls.Add(SavRateBG)
        PnlContent.Controls.Add(CashFlowBG)
        PnlContent.Controls.Add(CardBG1)
        PnlContent.Controls.Add(ExpBDBG)
        PnlContent.Dock = DockStyle.Fill
        PnlContent.Location = New System.Drawing.Point(20, 110)
        PnlContent.Name = "PnlContent"
        PnlContent.Size = New System.Drawing.Size(1855, 925)
        PnlContent.TabIndex = 12
        ' 
        ' BottomLeftBGText
        ' 
        BottomLeftBGText.AutoSize = True
        BottomLeftBGText.ForeColor = Drawing.SystemColors.ButtonFace
        BottomLeftBGText.Location = New System.Drawing.Point(23, 2113)
        BottomLeftBGText.Name = "BottomLeftBGText"
        BottomLeftBGText.Size = New System.Drawing.Size(221, 20)
        BottomLeftBGText.TabIndex = 21
        BottomLeftBGText.Text = "Made for mindful money moves"
        ' 
        ' BottomRightBGText
        ' 
        BottomRightBGText.AutoSize = True
        BottomRightBGText.ForeColor = Drawing.SystemColors.ButtonFace
        BottomRightBGText.Location = New System.Drawing.Point(1189, 2113)
        BottomRightBGText.Name = "BottomRightBGText"
        BottomRightBGText.Size = New System.Drawing.Size(265, 20)
        BottomRightBGText.TabIndex = 20
        BottomRightBGText.Text = "All amounts are in Philippine Pesos (₱)."
        ' 
        ' TransHistoryPnl
        ' 
        TransHistoryPnl.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        TransHistoryPnl.Controls.Add(AddTransButton)
        TransHistoryPnl.Controls.Add(EmptyTransDesc)
        TransHistoryPnl.Controls.Add(EmptyTransactionHeading)
        TransHistoryPnl.Controls.Add(FilterReset)
        TransHistoryPnl.Controls.Add(PaymentMethodFilter)
        TransHistoryPnl.Controls.Add(CategoryFilter)
        TransHistoryPnl.Controls.Add(TypeFilter)
        TransHistoryPnl.Controls.Add(TransactionSearchBox)
        TransHistoryPnl.Controls.Add(RecordsCount)
        TransHistoryPnl.Controls.Add(TransactionHistoryIcon)
        TransHistoryPnl.Controls.Add(TransactionHistoryHeading)
        TransHistoryPnl.Location = New System.Drawing.Point(23, 1624)
        TransHistoryPnl.Name = "TransHistoryPnl"
        TransHistoryPnl.Size = New System.Drawing.Size(1431, 441)
        TransHistoryPnl.TabIndex = 17
        ' 
        ' AddTransButton
        ' 
        AddTransButton.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        AddTransButton.Location = New System.Drawing.Point(616, 275)
        AddTransButton.Name = "AddTransButton"
        AddTransButton.Size = New System.Drawing.Size(227, 59)
        AddTransButton.TabIndex = 22
        AddTransButton.Text = "+ Add your first transaction"
        AddTransButton.UseVisualStyleBackColor = True
        ' 
        ' EmptyTransDesc
        ' 
        EmptyTransDesc.AutoSize = True
        EmptyTransDesc.Font = New System.Drawing.Font("Verdana", 9F)
        EmptyTransDesc.ForeColor = Drawing.SystemColors.ButtonFace
        EmptyTransDesc.Location = New System.Drawing.Point(521, 239)
        EmptyTransDesc.Name = "EmptyTransDesc"
        EmptyTransDesc.Size = New System.Drawing.Size(425, 18)
        EmptyTransDesc.TabIndex = 21
        EmptyTransDesc.Text = "Add your first transaction to see this month take shape."
        ' 
        ' EmptyTransactionHeading
        ' 
        EmptyTransactionHeading.AutoSize = True
        EmptyTransactionHeading.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        EmptyTransactionHeading.ForeColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        EmptyTransactionHeading.Location = New System.Drawing.Point(660, 210)
        EmptyTransactionHeading.Name = "EmptyTransactionHeading"
        EmptyTransactionHeading.Size = New System.Drawing.Size(137, 22)
        EmptyTransactionHeading.TabIndex = 20
        EmptyTransactionHeading.Text = "A fresh start"
        ' 
        ' FilterReset
        ' 
        FilterReset.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        FilterReset.Location = New System.Drawing.Point(1257, 79)
        FilterReset.Name = "FilterReset"
        FilterReset.Size = New System.Drawing.Size(148, 29)
        FilterReset.TabIndex = 19
        FilterReset.Text = "Reset Filters"
        FilterReset.UseVisualStyleBackColor = True
        ' 
        ' PaymentMethodFilter
        ' 
        PaymentMethodFilter.Font = New System.Drawing.Font("Verdana", 10.8F)
        PaymentMethodFilter.FormattingEnabled = True
        PaymentMethodFilter.Location = New System.Drawing.Point(897, 80)
        PaymentMethodFilter.Name = "PaymentMethodFilter"
        PaymentMethodFilter.Size = New System.Drawing.Size(340, 30)
        PaymentMethodFilter.TabIndex = 18
        PaymentMethodFilter.Text = "All Payment Methods"
        ' 
        ' CategoryFilter
        ' 
        CategoryFilter.Font = New System.Drawing.Font("Verdana", 10.8F)
        CategoryFilter.FormattingEnabled = True
        CategoryFilter.Location = New System.Drawing.Point(598, 79)
        CategoryFilter.Name = "CategoryFilter"
        CategoryFilter.Size = New System.Drawing.Size(279, 30)
        CategoryFilter.TabIndex = 17
        CategoryFilter.Text = "All Categories"
        ' 
        ' TypeFilter
        ' 
        TypeFilter.Font = New System.Drawing.Font("Verdana", 10.8F)
        TypeFilter.FormattingEnabled = True
        TypeFilter.Location = New System.Drawing.Point(403, 79)
        TypeFilter.Name = "TypeFilter"
        TypeFilter.Size = New System.Drawing.Size(175, 30)
        TypeFilter.TabIndex = 16
        TypeFilter.Text = "All Types"
        ' 
        ' TransactionSearchBox
        ' 
        TransactionSearchBox.Font = New System.Drawing.Font("Verdana", 10.8F)
        TransactionSearchBox.Location = New System.Drawing.Point(25, 80)
        TransactionSearchBox.Name = "TransactionSearchBox"
        TransactionSearchBox.Size = New System.Drawing.Size(357, 29)
        TransactionSearchBox.TabIndex = 15
        TransactionSearchBox.Text = "Search transactions..."
        ' 
        ' RecordsCount
        ' 
        RecordsCount.AutoSize = True
        RecordsCount.Font = New System.Drawing.Font("Verdana", 9F)
        RecordsCount.ForeColor = Drawing.SystemColors.ButtonFace
        RecordsCount.Location = New System.Drawing.Point(1297, 29)
        RecordsCount.Name = "RecordsCount"
        RecordsCount.Size = New System.Drawing.Size(119, 18)
        RecordsCount.TabIndex = 13
        RecordsCount.Text = "0 of 0 records."
        ' 
        ' TransactionHistoryIcon
        ' 
        TransactionHistoryIcon.Location = New System.Drawing.Point(23, 22)
        TransactionHistoryIcon.Name = "TransactionHistoryIcon"
        TransactionHistoryIcon.Size = New System.Drawing.Size(35, 38)
        TransactionHistoryIcon.TabIndex = 14
        ' 
        ' TransactionHistoryHeading
        ' 
        TransactionHistoryHeading.AutoSize = True
        TransactionHistoryHeading.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TransactionHistoryHeading.ForeColor = Drawing.SystemColors.ButtonFace
        TransactionHistoryHeading.Location = New System.Drawing.Point(75, 27)
        TransactionHistoryHeading.Name = "TransactionHistoryHeading"
        TransactionHistoryHeading.Size = New System.Drawing.Size(233, 25)
        TransactionHistoryHeading.TabIndex = 13
        TransactionHistoryHeading.Text = "Transaction History"
        ' 
        ' RecTransPnl
        ' 
        RecTransPnl.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        RecTransPnl.Controls.Add(ActiveCounter)
        RecTransPnl.Controls.Add(EmptyRecTransDesc)
        RecTransPnl.Controls.Add(RecTransDesc)
        RecTransPnl.Controls.Add(RecTransIcon)
        RecTransPnl.Controls.Add(RecTranHeading)
        RecTransPnl.Location = New System.Drawing.Point(23, 1440)
        RecTransPnl.Name = "RecTransPnl"
        RecTransPnl.Size = New System.Drawing.Size(1431, 155)
        RecTransPnl.TabIndex = 16
        ' 
        ' ActiveCounter
        ' 
        ActiveCounter.AutoSize = True
        ActiveCounter.Font = New System.Drawing.Font("Verdana", 9F)
        ActiveCounter.ForeColor = Drawing.SystemColors.ButtonFace
        ActiveCounter.Location = New System.Drawing.Point(1345, 21)
        ActiveCounter.Name = "ActiveCounter"
        ActiveCounter.Size = New System.Drawing.Size(67, 18)
        ActiveCounter.TabIndex = 12
        ActiveCounter.Text = "0 active"
        ' 
        ' EmptyRecTransDesc
        ' 
        EmptyRecTransDesc.AutoSize = True
        EmptyRecTransDesc.Font = New System.Drawing.Font("Verdana", 9F)
        EmptyRecTransDesc.ForeColor = Drawing.SystemColors.ButtonFace
        EmptyRecTransDesc.Location = New System.Drawing.Point(23, 97)
        EmptyRecTransDesc.Name = "EmptyRecTransDesc"
        EmptyRecTransDesc.Size = New System.Drawing.Size(626, 18)
        EmptyRecTransDesc.TabIndex = 11
        EmptyRecTransDesc.Text = "Nothing repeats yet. Tick ""Repeat this transaction every month."" when adding one."
        ' 
        ' RecTransDesc
        ' 
        RecTransDesc.AutoSize = True
        RecTransDesc.Font = New System.Drawing.Font("Verdana", 9F)
        RecTransDesc.ForeColor = Drawing.SystemColors.ButtonFace
        RecTransDesc.Location = New System.Drawing.Point(23, 65)
        RecTransDesc.Name = "RecTransDesc"
        RecTransDesc.Size = New System.Drawing.Size(461, 18)
        RecTransDesc.TabIndex = 10
        RecTransDesc.Text = "Salary, rent, subscriptions, and bills that repeat every month."
        ' 
        ' RecTransIcon
        ' 
        RecTransIcon.Location = New System.Drawing.Point(23, 14)
        RecTransIcon.Name = "RecTransIcon"
        RecTransIcon.Size = New System.Drawing.Size(35, 38)
        RecTransIcon.TabIndex = 9
        ' 
        ' RecTranHeading
        ' 
        RecTranHeading.AutoSize = True
        RecTranHeading.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        RecTranHeading.ForeColor = Drawing.SystemColors.ButtonFace
        RecTranHeading.Location = New System.Drawing.Point(75, 19)
        RecTranHeading.Name = "RecTranHeading"
        RecTranHeading.Size = New System.Drawing.Size(242, 25)
        RecTranHeading.TabIndex = 8
        RecTranHeading.Text = "Recurring Transactions"
        ' 
        ' ViewMonth
        ' 
        ViewMonth.AutoSize = True
        ViewMonth.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ViewMonth.ForeColor = Drawing.SystemColors.ButtonFace
        ViewMonth.Location = New System.Drawing.Point(1192, 119)
        ViewMonth.Name = "ViewMonth"
        ViewMonth.Size = New System.Drawing.Size(161, 22)
        ViewMonth.TabIndex = 5
        ViewMonth.Text = "Viewing Month"
        ' 
        ' HeadingDescription
        ' 
        HeadingDescription.AutoSize = True
        HeadingDescription.Font = New System.Drawing.Font("Verdana", 9F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        HeadingDescription.ForeColor = Drawing.SystemColors.ButtonFace
        HeadingDescription.Location = New System.Drawing.Point(42, 195)
        HeadingDescription.Name = "HeadingDescription"
        HeadingDescription.Size = New System.Drawing.Size(446, 18)
        HeadingDescription.TabIndex = 3
        HeadingDescription.Text = "A clearer picture of your money, one transaction at a time."
        ' 
        ' MonthNavContainer
        ' 
        MonthNavContainer.Controls.Add(MonthAndYear)
        MonthNavContainer.Controls.Add(NextMonth)
        MonthNavContainer.Controls.Add(PreviousMonth)
        MonthNavContainer.Location = New System.Drawing.Point(1107, 146)
        MonthNavContainer.Name = "MonthNavContainer"
        MonthNavContainer.Size = New System.Drawing.Size(332, 71)
        MonthNavContainer.TabIndex = 4
        ' 
        ' MonthAndYear
        ' 
        MonthAndYear.AutoSize = True
        MonthAndYear.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        MonthAndYear.ForeColor = Drawing.SystemColors.ButtonFace
        MonthAndYear.Location = New System.Drawing.Point(82, 26)
        MonthAndYear.Name = "MonthAndYear"
        MonthAndYear.Size = New System.Drawing.Size(171, 22)
        MonthAndYear.TabIndex = 6
        MonthAndYear.Text = "December 2026"
        MonthAndYear.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' NextMonth
        ' 
        NextMonth.Location = New System.Drawing.Point(266, 11)
        NextMonth.Name = "NextMonth"
        NextMonth.Size = New System.Drawing.Size(51, 50)
        NextMonth.TabIndex = 7
        NextMonth.Text = ">"
        NextMonth.UseVisualStyleBackColor = True
        ' 
        ' PreviousMonth
        ' 
        PreviousMonth.Location = New System.Drawing.Point(14, 11)
        PreviousMonth.Name = "PreviousMonth"
        PreviousMonth.Size = New System.Drawing.Size(51, 50)
        PreviousMonth.TabIndex = 6
        PreviousMonth.Text = "<"
        PreviousMonth.UseVisualStyleBackColor = True
        ' 
        ' SavingGoalsPnl
        ' 
        SavingGoalsPnl.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        SavingGoalsPnl.Controls.Add(CreateFirstGoal)
        SavingGoalsPnl.Controls.Add(EmptySavGoalsSubHeading)
        SavingGoalsPnl.Controls.Add(EmptySavGoalsHeading)
        SavingGoalsPnl.Controls.Add(EmptySavGoalsIcon)
        SavingGoalsPnl.Controls.Add(AddNewGoal)
        SavingGoalsPnl.Controls.Add(SvGIcon)
        SavingGoalsPnl.Controls.Add(SavGoalsHeading)
        SavingGoalsPnl.Location = New System.Drawing.Point(846, 1109)
        SavingGoalsPnl.Name = "SavingGoalsPnl"
        SavingGoalsPnl.Size = New System.Drawing.Size(608, 304)
        SavingGoalsPnl.TabIndex = 14
        ' 
        ' CreateFirstGoal
        ' 
        CreateFirstGoal.AutoSize = True
        CreateFirstGoal.Font = New System.Drawing.Font("Verdana", 10.8F)
        CreateFirstGoal.ForeColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        CreateFirstGoal.Location = New System.Drawing.Point(192, 223)
        CreateFirstGoal.Name = "CreateFirstGoal"
        CreateFirstGoal.Size = New System.Drawing.Size(232, 22)
        CreateFirstGoal.TabIndex = 19
        CreateFirstGoal.Text = "Create your first goal ->"
        ' 
        ' EmptySavGoalsSubHeading
        ' 
        EmptySavGoalsSubHeading.AutoSize = True
        EmptySavGoalsSubHeading.Font = New System.Drawing.Font("Verdana", 9F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        EmptySavGoalsSubHeading.ForeColor = Drawing.SystemColors.ButtonFace
        EmptySavGoalsSubHeading.Location = New System.Drawing.Point(155, 180)
        EmptySavGoalsSubHeading.Name = "EmptySavGoalsSubHeading"
        EmptySavGoalsSubHeading.Size = New System.Drawing.Size(304, 18)
        EmptySavGoalsSubHeading.TabIndex = 18
        EmptySavGoalsSubHeading.Text = "Add a transaction to see your cash flow"
        EmptySavGoalsSubHeading.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' EmptySavGoalsHeading
        ' 
        EmptySavGoalsHeading.AutoSize = True
        EmptySavGoalsHeading.Font = New System.Drawing.Font("Verdana", 10.8F)
        EmptySavGoalsHeading.ForeColor = Drawing.SystemColors.ButtonFace
        EmptySavGoalsHeading.Location = New System.Drawing.Point(168, 156)
        EmptySavGoalsHeading.Name = "EmptySavGoalsHeading"
        EmptySavGoalsHeading.Size = New System.Drawing.Size(274, 22)
        EmptySavGoalsHeading.TabIndex = 17
        EmptySavGoalsHeading.Text = "Your money story starts here"
        ' 
        ' EmptySavGoalsIcon
        ' 
        EmptySavGoalsIcon.Location = New System.Drawing.Point(284, 107)
        EmptySavGoalsIcon.Name = "EmptySavGoalsIcon"
        EmptySavGoalsIcon.Size = New System.Drawing.Size(35, 38)
        EmptySavGoalsIcon.TabIndex = 16
        ' 
        ' AddNewGoal
        ' 
        AddNewGoal.Font = New System.Drawing.Font("Verdana", 10.8F)
        AddNewGoal.Location = New System.Drawing.Point(440, 15)
        AddNewGoal.Name = "AddNewGoal"
        AddNewGoal.Size = New System.Drawing.Size(142, 38)
        AddNewGoal.TabIndex = 12
        AddNewGoal.Text = "+ New Goal"
        AddNewGoal.UseVisualStyleBackColor = True
        ' 
        ' SvGIcon
        ' 
        SvGIcon.Location = New System.Drawing.Point(23, 15)
        SvGIcon.Name = "SvGIcon"
        SvGIcon.Size = New System.Drawing.Size(35, 38)
        SvGIcon.TabIndex = 9
        ' 
        ' SavGoalsHeading
        ' 
        SavGoalsHeading.AutoSize = True
        SavGoalsHeading.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        SavGoalsHeading.ForeColor = Drawing.SystemColors.ButtonFace
        SavGoalsHeading.Location = New System.Drawing.Point(75, 20)
        SavGoalsHeading.Name = "SavGoalsHeading"
        SavGoalsHeading.Size = New System.Drawing.Size(156, 25)
        SavGoalsHeading.TabIndex = 8
        SavGoalsHeading.Text = "Saving Goals"
        ' 
        ' MainHeading
        ' 
        MainHeading.AutoSize = True
        MainHeading.Font = New System.Drawing.Font("Verdana", 19.8000011F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        MainHeading.ForeColor = Drawing.SystemColors.ButtonFace
        MainHeading.Location = New System.Drawing.Point(34, 147)
        MainHeading.Name = "MainHeading"
        MainHeading.Size = New System.Drawing.Size(462, 41)
        MainHeading.TabIndex = 2
        MainHeading.Text = "Make every cent count."
        ' 
        ' WalletsAndAccsPnl
        ' 
        WalletsAndAccsPnl.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        WalletsAndAccsPnl.Controls.Add(WalletCard4)
        WalletsAndAccsPnl.Controls.Add(WalletCard3)
        WalletsAndAccsPnl.Controls.Add(WalletCard2)
        WalletsAndAccsPnl.Controls.Add(WalletCard1)
        WalletsAndAccsPnl.Controls.Add(AddAccBtn)
        WalletsAndAccsPnl.Controls.Add(TransferBtn)
        WalletsAndAccsPnl.Controls.Add(WalletAndAccIcon)
        WalletsAndAccsPnl.Controls.Add(WalletsHeading)
        WalletsAndAccsPnl.Location = New System.Drawing.Point(23, 1110)
        WalletsAndAccsPnl.Name = "WalletsAndAccsPnl"
        WalletsAndAccsPnl.Size = New System.Drawing.Size(805, 302)
        WalletsAndAccsPnl.TabIndex = 13
        ' 
        ' WalletCard4
        ' 
        WalletCard4.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        WalletCard4.Controls.Add(AmtBDO)
        WalletCard4.Controls.Add(BDOHeading)
        WalletCard4.Location = New System.Drawing.Point(410, 181)
        WalletCard4.Name = "WalletCard4"
        WalletCard4.Size = New System.Drawing.Size(369, 103)
        WalletCard4.TabIndex = 11
        ' 
        ' AmtBDO
        ' 
        AmtBDO.AutoSize = True
        AmtBDO.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Bold)
        AmtBDO.ForeColor = Drawing.SystemColors.ButtonFace
        AmtBDO.Location = New System.Drawing.Point(62, 41)
        AmtBDO.Name = "AmtBDO"
        AmtBDO.Size = New System.Drawing.Size(72, 28)
        AmtBDO.TabIndex = 16
        AmtBDO.Text = "₱ 0.00"
        ' 
        ' BDOHeading
        ' 
        BDOHeading.AutoSize = True
        BDOHeading.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        BDOHeading.ForeColor = Drawing.SystemColors.ButtonFace
        BDOHeading.Location = New System.Drawing.Point(62, 21)
        BDOHeading.Name = "BDOHeading"
        BDOHeading.Size = New System.Drawing.Size(152, 20)
        BDOHeading.TabIndex = 14
        BDOHeading.Text = "BDO - Bank Account"
        ' 
        ' WalletCard3
        ' 
        WalletCard3.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        WalletCard3.Controls.Add(AmtGCash)
        WalletCard3.Controls.Add(GCashHeading)
        WalletCard3.Location = New System.Drawing.Point(410, 67)
        WalletCard3.Name = "WalletCard3"
        WalletCard3.Size = New System.Drawing.Size(369, 103)
        WalletCard3.TabIndex = 11
        ' 
        ' AmtGCash
        ' 
        AmtGCash.AutoSize = True
        AmtGCash.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Bold)
        AmtGCash.ForeColor = Drawing.SystemColors.ButtonFace
        AmtGCash.Location = New System.Drawing.Point(62, 43)
        AmtGCash.Name = "AmtGCash"
        AmtGCash.Size = New System.Drawing.Size(72, 28)
        AmtGCash.TabIndex = 14
        AmtGCash.Text = "₱ 0.00"
        ' 
        ' GCashHeading
        ' 
        GCashHeading.AutoSize = True
        GCashHeading.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        GCashHeading.ForeColor = Drawing.SystemColors.ButtonFace
        GCashHeading.Location = New System.Drawing.Point(62, 23)
        GCashHeading.Name = "GCashHeading"
        GCashHeading.Size = New System.Drawing.Size(123, 20)
        GCashHeading.TabIndex = 13
        GCashHeading.Text = "GCash - E-wallet"
        ' 
        ' WalletCard2
        ' 
        WalletCard2.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        WalletCard2.Controls.Add(AmtMaya)
        WalletCard2.Controls.Add(MayaHeading)
        WalletCard2.Location = New System.Drawing.Point(26, 181)
        WalletCard2.Name = "WalletCard2"
        WalletCard2.Size = New System.Drawing.Size(369, 103)
        WalletCard2.TabIndex = 11
        ' 
        ' AmtMaya
        ' 
        AmtMaya.AutoSize = True
        AmtMaya.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Bold)
        AmtMaya.ForeColor = Drawing.SystemColors.ButtonFace
        AmtMaya.Location = New System.Drawing.Point(63, 41)
        AmtMaya.Name = "AmtMaya"
        AmtMaya.Size = New System.Drawing.Size(72, 28)
        AmtMaya.TabIndex = 15
        AmtMaya.Text = "₱ 0.00"
        ' 
        ' MayaHeading
        ' 
        MayaHeading.AutoSize = True
        MayaHeading.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MayaHeading.ForeColor = Drawing.SystemColors.ButtonFace
        MayaHeading.Location = New System.Drawing.Point(63, 21)
        MayaHeading.Name = "MayaHeading"
        MayaHeading.Size = New System.Drawing.Size(117, 20)
        MayaHeading.TabIndex = 13
        MayaHeading.Text = "Maya - E-wallet"
        ' 
        ' WalletCard1
        ' 
        WalletCard1.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        WalletCard1.Controls.Add(AmtCash)
        WalletCard1.Controls.Add(CashHeading)
        WalletCard1.Location = New System.Drawing.Point(26, 67)
        WalletCard1.Name = "WalletCard1"
        WalletCard1.Size = New System.Drawing.Size(369, 103)
        WalletCard1.TabIndex = 10
        ' 
        ' AmtCash
        ' 
        AmtCash.AutoSize = True
        AmtCash.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Bold)
        AmtCash.ForeColor = Drawing.SystemColors.ButtonFace
        AmtCash.Location = New System.Drawing.Point(63, 43)
        AmtCash.Name = "AmtCash"
        AmtCash.Size = New System.Drawing.Size(72, 28)
        AmtCash.TabIndex = 13
        AmtCash.Text = "₱ 0.00"
        ' 
        ' CashHeading
        ' 
        CashHeading.AutoSize = True
        CashHeading.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        CashHeading.ForeColor = Drawing.SystemColors.ButtonFace
        CashHeading.Location = New System.Drawing.Point(63, 23)
        CashHeading.Name = "CashHeading"
        CashHeading.Size = New System.Drawing.Size(42, 20)
        CashHeading.TabIndex = 12
        CashHeading.Text = "Cash"
        ' 
        ' AddAccBtn
        ' 
        AddAccBtn.Font = New System.Drawing.Font("Verdana", 9F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        AddAccBtn.Location = New System.Drawing.Point(637, 14)
        AddAccBtn.Name = "AddAccBtn"
        AddAccBtn.Size = New System.Drawing.Size(142, 38)
        AddAccBtn.TabIndex = 9
        AddAccBtn.Text = "+ Add Account"
        AddAccBtn.UseVisualStyleBackColor = True
        ' 
        ' TransferBtn
        ' 
        TransferBtn.Font = New System.Drawing.Font("Verdana", 10.8F)
        TransferBtn.Location = New System.Drawing.Point(463, 14)
        TransferBtn.Name = "TransferBtn"
        TransferBtn.Size = New System.Drawing.Size(142, 38)
        TransferBtn.TabIndex = 8
        TransferBtn.Text = "<-> Transfer"
        TransferBtn.UseVisualStyleBackColor = True
        ' 
        ' WalletAndAccIcon
        ' 
        WalletAndAccIcon.Location = New System.Drawing.Point(23, 14)
        WalletAndAccIcon.Name = "WalletAndAccIcon"
        WalletAndAccIcon.Size = New System.Drawing.Size(35, 38)
        WalletAndAccIcon.TabIndex = 7
        ' 
        ' WalletsHeading
        ' 
        WalletsHeading.AutoSize = True
        WalletsHeading.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        WalletsHeading.ForeColor = Drawing.SystemColors.ButtonFace
        WalletsHeading.Location = New System.Drawing.Point(75, 19)
        WalletsHeading.Name = "WalletsHeading"
        WalletsHeading.Size = New System.Drawing.Size(254, 25)
        WalletsHeading.TabIndex = 6
        WalletsHeading.Text = "Wallets and Accounts"
        ' 
        ' SectionLabel
        ' 
        SectionLabel.AutoSize = True
        SectionLabel.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        SectionLabel.ForeColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        SectionLabel.Location = New System.Drawing.Point(38, 121)
        SectionLabel.Name = "SectionLabel"
        SectionLabel.Size = New System.Drawing.Size(256, 22)
        SectionLabel.TabIndex = 1
        SectionLabel.Text = "Your Financial Overview"
        ' 
        ' MBLPnl
        ' 
        MBLPnl.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLPnl.Controls.Add(MBLCardPanel6)
        MBLPnl.Controls.Add(MBLCardPanel8)
        MBLPnl.Controls.Add(MBLCardPanel3)
        MBLPnl.Controls.Add(MBLCardPanel7)
        MBLPnl.Controls.Add(MBLCardPanel5)
        MBLPnl.Controls.Add(MBLCardPanel4)
        MBLPnl.Controls.Add(MBLCardPanel2)
        MBLPnl.Controls.Add(MBLCardPanel1)
        MBLPnl.Controls.Add(Button1)
        MBLPnl.Controls.Add(MBLIcon)
        MBLPnl.Controls.Add(WalAndAcc)
        MBLPnl.Location = New System.Drawing.Point(23, 754)
        MBLPnl.Name = "MBLPnl"
        MBLPnl.Size = New System.Drawing.Size(1431, 331)
        MBLPnl.TabIndex = 12
        ' 
        ' MBLCardPanel6
        ' 
        MBLCardPanel6.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel6.Controls.Add(ProgressHealth)
        MBLCardPanel6.Controls.Add(UsedHealth)
        MBLCardPanel6.Controls.Add(AmtLeftHealth)
        MBLCardPanel6.Controls.Add(slash6)
        MBLCardPanel6.Controls.Add(MBLHealth)
        MBLCardPanel6.Controls.Add(LimitHealth)
        MBLCardPanel6.Controls.Add(SpentAmtHealth)
        MBLCardPanel6.Controls.Add(MBLColorHealth)
        MBLCardPanel6.Location = New System.Drawing.Point(508, 232)
        MBLCardPanel6.Name = "MBLCardPanel6"
        MBLCardPanel6.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel6.TabIndex = 12
        ' 
        ' ProgressHealth
        ' 
        ProgressHealth.Location = New System.Drawing.Point(12, 43)
        ProgressHealth.Name = "ProgressHealth"
        ProgressHealth.Size = New System.Drawing.Size(392, 8)
        ProgressHealth.TabIndex = 59
        ' 
        ' UsedHealth
        ' 
        UsedHealth.AutoSize = True
        UsedHealth.ForeColor = Drawing.SystemColors.ButtonFace
        UsedHealth.Location = New System.Drawing.Point(12, 57)
        UsedHealth.Name = "UsedHealth"
        UsedHealth.Size = New System.Drawing.Size(64, 20)
        UsedHealth.TabIndex = 67
        UsedHealth.Text = "0% used"
        ' 
        ' AmtLeftHealth
        ' 
        AmtLeftHealth.AutoSize = True
        AmtLeftHealth.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftHealth.Location = New System.Drawing.Point(294, 57)
        AmtLeftHealth.Name = "AmtLeftHealth"
        AmtLeftHealth.Size = New System.Drawing.Size(102, 20)
        AmtLeftHealth.TabIndex = 63
        AmtLeftHealth.Text = "₱ 2,000.00 left"
        ' 
        ' slash6
        ' 
        slash6.AutoSize = True
        slash6.ForeColor = Drawing.SystemColors.ButtonFace
        slash6.Location = New System.Drawing.Point(311, 10)
        slash6.Name = "slash6"
        slash6.Size = New System.Drawing.Size(15, 20)
        slash6.TabIndex = 64
        slash6.Text = "/"
        ' 
        ' MBLHealth
        ' 
        MBLHealth.AutoSize = True
        MBLHealth.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLHealth.ForeColor = Drawing.SystemColors.ButtonFace
        MBLHealth.Location = New System.Drawing.Point(40, 10)
        MBLHealth.Name = "MBLHealth"
        MBLHealth.Size = New System.Drawing.Size(55, 20)
        MBLHealth.TabIndex = 54
        MBLHealth.Text = "Health"
        ' 
        ' LimitHealth
        ' 
        LimitHealth.AutoSize = True
        LimitHealth.ForeColor = Drawing.SystemColors.ButtonFace
        LimitHealth.Location = New System.Drawing.Point(328, 10)
        LimitHealth.Name = "LimitHealth"
        LimitHealth.Size = New System.Drawing.Size(76, 20)
        LimitHealth.TabIndex = 63
        LimitHealth.Text = "₱ 2,000.00"
        ' 
        ' SpentAmtHealth
        ' 
        SpentAmtHealth.AutoSize = True
        SpentAmtHealth.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtHealth.Location = New System.Drawing.Point(260, 10)
        SpentAmtHealth.Name = "SpentAmtHealth"
        SpentAmtHealth.Size = New System.Drawing.Size(49, 20)
        SpentAmtHealth.TabIndex = 62
        SpentAmtHealth.Text = "₱ 0.00"
        ' 
        ' MBLColorHealth
        ' 
        MBLColorHealth.Location = New System.Drawing.Point(12, 14)
        MBLColorHealth.Name = "MBLColorHealth"
        MBLColorHealth.Size = New System.Drawing.Size(13, 13)
        MBLColorHealth.TabIndex = 54
        ' 
        ' MBLCardPanel8
        ' 
        MBLCardPanel8.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel8.Controls.Add(ProgressMisc)
        MBLCardPanel8.Controls.Add(UsedMisc)
        MBLCardPanel8.Controls.Add(AmtLeftMisc)
        MBLCardPanel8.Controls.Add(slash8)
        MBLCardPanel8.Controls.Add(LimitMisc)
        MBLCardPanel8.Controls.Add(SpentAmtMiscellaneous)
        MBLCardPanel8.Controls.Add(MBLMiscellaneous)
        MBLCardPanel8.Controls.Add(MBLColorMisc)
        MBLCardPanel8.Location = New System.Drawing.Point(985, 146)
        MBLCardPanel8.Name = "MBLCardPanel8"
        MBLCardPanel8.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel8.TabIndex = 11
        ' 
        ' ProgressMisc
        ' 
        ProgressMisc.Location = New System.Drawing.Point(13, 42)
        ProgressMisc.Name = "ProgressMisc"
        ProgressMisc.Size = New System.Drawing.Size(392, 8)
        ProgressMisc.TabIndex = 59
        ' 
        ' UsedMisc
        ' 
        UsedMisc.AutoSize = True
        UsedMisc.ForeColor = Drawing.SystemColors.ButtonFace
        UsedMisc.Location = New System.Drawing.Point(13, 57)
        UsedMisc.Name = "UsedMisc"
        UsedMisc.Size = New System.Drawing.Size(64, 20)
        UsedMisc.TabIndex = 69
        UsedMisc.Text = "0% used"
        ' 
        ' AmtLeftMisc
        ' 
        AmtLeftMisc.AutoSize = True
        AmtLeftMisc.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftMisc.Location = New System.Drawing.Point(293, 57)
        AmtLeftMisc.Name = "AmtLeftMisc"
        AmtLeftMisc.Size = New System.Drawing.Size(102, 20)
        AmtLeftMisc.TabIndex = 67
        AmtLeftMisc.Text = "₱ 1,500.00 left"
        ' 
        ' slash8
        ' 
        slash8.AutoSize = True
        slash8.ForeColor = Drawing.SystemColors.ButtonFace
        slash8.Location = New System.Drawing.Point(309, 6)
        slash8.Name = "slash8"
        slash8.Size = New System.Drawing.Size(15, 20)
        slash8.TabIndex = 63
        slash8.Text = "/"
        ' 
        ' LimitMisc
        ' 
        LimitMisc.AutoSize = True
        LimitMisc.ForeColor = Drawing.SystemColors.ButtonFace
        LimitMisc.Location = New System.Drawing.Point(328, 6)
        LimitMisc.Name = "LimitMisc"
        LimitMisc.Size = New System.Drawing.Size(76, 20)
        LimitMisc.TabIndex = 62
        LimitMisc.Text = "₱ 1,500.00"
        ' 
        ' SpentAmtMiscellaneous
        ' 
        SpentAmtMiscellaneous.AutoSize = True
        SpentAmtMiscellaneous.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtMiscellaneous.Location = New System.Drawing.Point(258, 6)
        SpentAmtMiscellaneous.Name = "SpentAmtMiscellaneous"
        SpentAmtMiscellaneous.Size = New System.Drawing.Size(49, 20)
        SpentAmtMiscellaneous.TabIndex = 61
        SpentAmtMiscellaneous.Text = "₱ 0.00"
        ' 
        ' MBLMiscellaneous
        ' 
        MBLMiscellaneous.AutoSize = True
        MBLMiscellaneous.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLMiscellaneous.ForeColor = Drawing.SystemColors.ButtonFace
        MBLMiscellaneous.Location = New System.Drawing.Point(39, 9)
        MBLMiscellaneous.Name = "MBLMiscellaneous"
        MBLMiscellaneous.Size = New System.Drawing.Size(107, 20)
        MBLMiscellaneous.TabIndex = 57
        MBLMiscellaneous.Text = "Miscellaneous"
        ' 
        ' MBLColorMisc
        ' 
        MBLColorMisc.Location = New System.Drawing.Point(13, 13)
        MBLColorMisc.Name = "MBLColorMisc"
        MBLColorMisc.Size = New System.Drawing.Size(13, 13)
        MBLColorMisc.TabIndex = 51
        ' 
        ' MBLCardPanel3
        ' 
        MBLCardPanel3.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel3.Controls.Add(ProgressTranspo)
        MBLCardPanel3.Controls.Add(UsedTranspo)
        MBLCardPanel3.Controls.Add(AmtLeftTranspo)
        MBLCardPanel3.Controls.Add(slash3)
        MBLCardPanel3.Controls.Add(LimitTransportation)
        MBLCardPanel3.Controls.Add(SpentAmtTranspo)
        MBLCardPanel3.Controls.Add(MBLTransportation)
        MBLCardPanel3.Controls.Add(MBLColorTranspo)
        MBLCardPanel3.Location = New System.Drawing.Point(32, 232)
        MBLCardPanel3.Name = "MBLCardPanel3"
        MBLCardPanel3.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel3.TabIndex = 9
        ' 
        ' ProgressTranspo
        ' 
        ProgressTranspo.Location = New System.Drawing.Point(12, 43)
        ProgressTranspo.Name = "ProgressTranspo"
        ProgressTranspo.Size = New System.Drawing.Size(392, 8)
        ProgressTranspo.TabIndex = 59
        ' 
        ' UsedTranspo
        ' 
        UsedTranspo.AutoSize = True
        UsedTranspo.ForeColor = Drawing.SystemColors.ButtonFace
        UsedTranspo.Location = New System.Drawing.Point(13, 57)
        UsedTranspo.Name = "UsedTranspo"
        UsedTranspo.Size = New System.Drawing.Size(64, 20)
        UsedTranspo.TabIndex = 59
        UsedTranspo.Text = "0% used"
        ' 
        ' AmtLeftTranspo
        ' 
        AmtLeftTranspo.AutoSize = True
        AmtLeftTranspo.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftTranspo.Location = New System.Drawing.Point(294, 57)
        AmtLeftTranspo.Name = "AmtLeftTranspo"
        AmtLeftTranspo.Size = New System.Drawing.Size(102, 20)
        AmtLeftTranspo.TabIndex = 59
        AmtLeftTranspo.Text = "₱ 2,000.00 left"
        ' 
        ' slash3
        ' 
        slash3.AutoSize = True
        slash3.ForeColor = Drawing.SystemColors.ButtonFace
        slash3.Location = New System.Drawing.Point(310, 10)
        slash3.Name = "slash3"
        slash3.Size = New System.Drawing.Size(15, 20)
        slash3.TabIndex = 61
        slash3.Text = "/"
        ' 
        ' LimitTransportation
        ' 
        LimitTransportation.AutoSize = True
        LimitTransportation.ForeColor = Drawing.SystemColors.ButtonFace
        LimitTransportation.Location = New System.Drawing.Point(327, 10)
        LimitTransportation.Name = "LimitTransportation"
        LimitTransportation.Size = New System.Drawing.Size(76, 20)
        LimitTransportation.TabIndex = 60
        LimitTransportation.Text = "₱ 2,000.00"
        ' 
        ' SpentAmtTranspo
        ' 
        SpentAmtTranspo.AutoSize = True
        SpentAmtTranspo.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtTranspo.Location = New System.Drawing.Point(259, 10)
        SpentAmtTranspo.Name = "SpentAmtTranspo"
        SpentAmtTranspo.Size = New System.Drawing.Size(49, 20)
        SpentAmtTranspo.TabIndex = 59
        SpentAmtTranspo.Text = "₱ 0.00"
        ' 
        ' MBLTransportation
        ' 
        MBLTransportation.AutoSize = True
        MBLTransportation.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLTransportation.ForeColor = Drawing.SystemColors.ButtonFace
        MBLTransportation.Location = New System.Drawing.Point(43, 10)
        MBLTransportation.Name = "MBLTransportation"
        MBLTransportation.Size = New System.Drawing.Size(113, 20)
        MBLTransportation.TabIndex = 53
        MBLTransportation.Text = "Transportation"
        ' 
        ' MBLColorTranspo
        ' 
        MBLColorTranspo.Location = New System.Drawing.Point(13, 14)
        MBLColorTranspo.Name = "MBLColorTranspo"
        MBLColorTranspo.Size = New System.Drawing.Size(13, 13)
        MBLColorTranspo.TabIndex = 53
        ' 
        ' MBLCardPanel7
        ' 
        MBLCardPanel7.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel7.Controls.Add(ProgressShop)
        MBLCardPanel7.Controls.Add(UsedShop)
        MBLCardPanel7.Controls.Add(AmtLeftShop)
        MBLCardPanel7.Controls.Add(slash7)
        MBLCardPanel7.Controls.Add(LimitShopping)
        MBLCardPanel7.Controls.Add(SpentAmtShopping)
        MBLCardPanel7.Controls.Add(MBLShopping)
        MBLCardPanel7.Controls.Add(MBLColorShop)
        MBLCardPanel7.Location = New System.Drawing.Point(985, 60)
        MBLCardPanel7.Name = "MBLCardPanel7"
        MBLCardPanel7.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel7.TabIndex = 10
        ' 
        ' ProgressShop
        ' 
        ProgressShop.Location = New System.Drawing.Point(13, 42)
        ProgressShop.Name = "ProgressShop"
        ProgressShop.Size = New System.Drawing.Size(392, 8)
        ProgressShop.TabIndex = 59
        ' 
        ' UsedShop
        ' 
        UsedShop.AutoSize = True
        UsedShop.ForeColor = Drawing.SystemColors.ButtonFace
        UsedShop.Location = New System.Drawing.Point(13, 57)
        UsedShop.Name = "UsedShop"
        UsedShop.Size = New System.Drawing.Size(64, 20)
        UsedShop.TabIndex = 68
        UsedShop.Text = "0% used"
        ' 
        ' AmtLeftShop
        ' 
        AmtLeftShop.AutoSize = True
        AmtLeftShop.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftShop.Location = New System.Drawing.Point(293, 57)
        AmtLeftShop.Name = "AmtLeftShop"
        AmtLeftShop.Size = New System.Drawing.Size(102, 20)
        AmtLeftShop.TabIndex = 66
        AmtLeftShop.Text = "₱ 2,500.00 left"
        ' 
        ' slash7
        ' 
        slash7.AutoSize = True
        slash7.ForeColor = Drawing.SystemColors.ButtonFace
        slash7.Location = New System.Drawing.Point(311, 10)
        slash7.Name = "slash7"
        slash7.Size = New System.Drawing.Size(15, 20)
        slash7.TabIndex = 60
        slash7.Text = "/"
        ' 
        ' LimitShopping
        ' 
        LimitShopping.AutoSize = True
        LimitShopping.ForeColor = Drawing.SystemColors.ButtonFace
        LimitShopping.Location = New System.Drawing.Point(328, 10)
        LimitShopping.Name = "LimitShopping"
        LimitShopping.Size = New System.Drawing.Size(76, 20)
        LimitShopping.TabIndex = 59
        LimitShopping.Text = "₱ 2,500.00"
        ' 
        ' SpentAmtShopping
        ' 
        SpentAmtShopping.AutoSize = True
        SpentAmtShopping.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtShopping.Location = New System.Drawing.Point(260, 10)
        SpentAmtShopping.Name = "SpentAmtShopping"
        SpentAmtShopping.Size = New System.Drawing.Size(49, 20)
        SpentAmtShopping.TabIndex = 58
        SpentAmtShopping.Text = "₱ 0.00"
        ' 
        ' MBLShopping
        ' 
        MBLShopping.AutoSize = True
        MBLShopping.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLShopping.ForeColor = Drawing.SystemColors.ButtonFace
        MBLShopping.Location = New System.Drawing.Point(39, 10)
        MBLShopping.Name = "MBLShopping"
        MBLShopping.Size = New System.Drawing.Size(75, 20)
        MBLShopping.TabIndex = 56
        MBLShopping.Text = "Shopping"
        ' 
        ' MBLColorShop
        ' 
        MBLColorShop.Location = New System.Drawing.Point(13, 14)
        MBLColorShop.Name = "MBLColorShop"
        MBLColorShop.Size = New System.Drawing.Size(13, 13)
        MBLColorShop.TabIndex = 52
        ' 
        ' MBLCardPanel5
        ' 
        MBLCardPanel5.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel5.Controls.Add(ProgressEnt)
        MBLCardPanel5.Controls.Add(UsedEnt)
        MBLCardPanel5.Controls.Add(AmtLeftEnt)
        MBLCardPanel5.Controls.Add(slash5)
        MBLCardPanel5.Controls.Add(LimitEnt)
        MBLCardPanel5.Controls.Add(SpentAmtEntertainment)
        MBLCardPanel5.Controls.Add(MBLEntertainment)
        MBLCardPanel5.Controls.Add(MBLColorEnt)
        MBLCardPanel5.Location = New System.Drawing.Point(508, 146)
        MBLCardPanel5.Name = "MBLCardPanel5"
        MBLCardPanel5.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel5.TabIndex = 11
        ' 
        ' ProgressEnt
        ' 
        ProgressEnt.Location = New System.Drawing.Point(12, 42)
        ProgressEnt.Name = "ProgressEnt"
        ProgressEnt.Size = New System.Drawing.Size(392, 8)
        ProgressEnt.TabIndex = 59
        ' 
        ' UsedEnt
        ' 
        UsedEnt.AutoSize = True
        UsedEnt.ForeColor = Drawing.SystemColors.ButtonFace
        UsedEnt.Location = New System.Drawing.Point(12, 57)
        UsedEnt.Name = "UsedEnt"
        UsedEnt.Size = New System.Drawing.Size(64, 20)
        UsedEnt.TabIndex = 66
        UsedEnt.Text = "0% used"
        ' 
        ' AmtLeftEnt
        ' 
        AmtLeftEnt.AutoSize = True
        AmtLeftEnt.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftEnt.Location = New System.Drawing.Point(294, 57)
        AmtLeftEnt.Name = "AmtLeftEnt"
        AmtLeftEnt.Size = New System.Drawing.Size(102, 20)
        AmtLeftEnt.TabIndex = 62
        AmtLeftEnt.Text = "₱ 2,000.00 left"
        ' 
        ' slash5
        ' 
        slash5.AutoSize = True
        slash5.ForeColor = Drawing.SystemColors.ButtonFace
        slash5.Location = New System.Drawing.Point(311, 9)
        slash5.Name = "slash5"
        slash5.Size = New System.Drawing.Size(15, 20)
        slash5.TabIndex = 61
        slash5.Text = "/"
        ' 
        ' LimitEnt
        ' 
        LimitEnt.AutoSize = True
        LimitEnt.ForeColor = Drawing.SystemColors.ButtonFace
        LimitEnt.Location = New System.Drawing.Point(328, 9)
        LimitEnt.Name = "LimitEnt"
        LimitEnt.Size = New System.Drawing.Size(76, 20)
        LimitEnt.TabIndex = 60
        LimitEnt.Text = "₱ 2,000.00"
        ' 
        ' SpentAmtEntertainment
        ' 
        SpentAmtEntertainment.AutoSize = True
        SpentAmtEntertainment.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtEntertainment.Location = New System.Drawing.Point(260, 9)
        SpentAmtEntertainment.Name = "SpentAmtEntertainment"
        SpentAmtEntertainment.Size = New System.Drawing.Size(49, 20)
        SpentAmtEntertainment.TabIndex = 59
        SpentAmtEntertainment.Text = "₱ 0.00"
        ' 
        ' MBLEntertainment
        ' 
        MBLEntertainment.AutoSize = True
        MBLEntertainment.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLEntertainment.ForeColor = Drawing.SystemColors.ButtonFace
        MBLEntertainment.Location = New System.Drawing.Point(40, 9)
        MBLEntertainment.Name = "MBLEntertainment"
        MBLEntertainment.Size = New System.Drawing.Size(110, 20)
        MBLEntertainment.TabIndex = 56
        MBLEntertainment.Text = "Entertainment"
        ' 
        ' MBLColorEnt
        ' 
        MBLColorEnt.Location = New System.Drawing.Point(12, 13)
        MBLColorEnt.Name = "MBLColorEnt"
        MBLColorEnt.Size = New System.Drawing.Size(13, 13)
        MBLColorEnt.TabIndex = 54
        ' 
        ' MBLCardPanel4
        ' 
        MBLCardPanel4.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel4.Controls.Add(ProgressUtil)
        MBLCardPanel4.Controls.Add(UsedUtil)
        MBLCardPanel4.Controls.Add(AmtLeftUtil)
        MBLCardPanel4.Controls.Add(slash4)
        MBLCardPanel4.Controls.Add(LimitUtilities)
        MBLCardPanel4.Controls.Add(SpentAmtUtilities)
        MBLCardPanel4.Controls.Add(MBLUtilities)
        MBLCardPanel4.Controls.Add(MBLColorUtil)
        MBLCardPanel4.Location = New System.Drawing.Point(508, 60)
        MBLCardPanel4.Name = "MBLCardPanel4"
        MBLCardPanel4.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel4.TabIndex = 10
        ' 
        ' ProgressUtil
        ' 
        ProgressUtil.Location = New System.Drawing.Point(12, 42)
        ProgressUtil.Name = "ProgressUtil"
        ProgressUtil.Size = New System.Drawing.Size(392, 8)
        ProgressUtil.TabIndex = 59
        ' 
        ' UsedUtil
        ' 
        UsedUtil.AutoSize = True
        UsedUtil.ForeColor = Drawing.SystemColors.ButtonFace
        UsedUtil.Location = New System.Drawing.Point(12, 57)
        UsedUtil.Name = "UsedUtil"
        UsedUtil.Size = New System.Drawing.Size(64, 20)
        UsedUtil.TabIndex = 62
        UsedUtil.Text = "0% used"
        ' 
        ' AmtLeftUtil
        ' 
        AmtLeftUtil.AutoSize = True
        AmtLeftUtil.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftUtil.Location = New System.Drawing.Point(294, 57)
        AmtLeftUtil.Name = "AmtLeftUtil"
        AmtLeftUtil.Size = New System.Drawing.Size(102, 20)
        AmtLeftUtil.TabIndex = 65
        AmtLeftUtil.Text = "₱ 5,000.00 left"
        ' 
        ' slash4
        ' 
        slash4.AutoSize = True
        slash4.ForeColor = Drawing.SystemColors.ButtonFace
        slash4.Location = New System.Drawing.Point(311, 10)
        slash4.Name = "slash4"
        slash4.Size = New System.Drawing.Size(15, 20)
        slash4.TabIndex = 58
        slash4.Text = "/"
        ' 
        ' LimitUtilities
        ' 
        LimitUtilities.AutoSize = True
        LimitUtilities.ForeColor = Drawing.SystemColors.ButtonFace
        LimitUtilities.Location = New System.Drawing.Point(328, 10)
        LimitUtilities.Name = "LimitUtilities"
        LimitUtilities.Size = New System.Drawing.Size(76, 20)
        LimitUtilities.TabIndex = 57
        LimitUtilities.Text = "₱ 5,000.00"
        ' 
        ' SpentAmtUtilities
        ' 
        SpentAmtUtilities.AutoSize = True
        SpentAmtUtilities.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtUtilities.Location = New System.Drawing.Point(260, 10)
        SpentAmtUtilities.Name = "SpentAmtUtilities"
        SpentAmtUtilities.Size = New System.Drawing.Size(49, 20)
        SpentAmtUtilities.TabIndex = 56
        SpentAmtUtilities.Text = "₱ 0.00"
        ' 
        ' MBLUtilities
        ' 
        MBLUtilities.AutoSize = True
        MBLUtilities.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLUtilities.ForeColor = Drawing.SystemColors.ButtonFace
        MBLUtilities.Location = New System.Drawing.Point(40, 10)
        MBLUtilities.Name = "MBLUtilities"
        MBLUtilities.Size = New System.Drawing.Size(63, 20)
        MBLUtilities.TabIndex = 55
        MBLUtilities.Text = "Utilities"
        ' 
        ' MBLColorUtil
        ' 
        MBLColorUtil.Location = New System.Drawing.Point(12, 14)
        MBLColorUtil.Name = "MBLColorUtil"
        MBLColorUtil.Size = New System.Drawing.Size(13, 13)
        MBLColorUtil.TabIndex = 54
        ' 
        ' MBLCardPanel2
        ' 
        MBLCardPanel2.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel2.Controls.Add(ProgressFood)
        MBLCardPanel2.Controls.Add(UsedFood)
        MBLCardPanel2.Controls.Add(AmtLeftFood)
        MBLCardPanel2.Controls.Add(slash2)
        MBLCardPanel2.Controls.Add(LimitFood)
        MBLCardPanel2.Controls.Add(SpentAmtFood)
        MBLCardPanel2.Controls.Add(MBLFood)
        MBLCardPanel2.Controls.Add(MBLColorFood)
        MBLCardPanel2.Location = New System.Drawing.Point(32, 146)
        MBLCardPanel2.Name = "MBLCardPanel2"
        MBLCardPanel2.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel2.TabIndex = 8
        ' 
        ' ProgressFood
        ' 
        ProgressFood.Location = New System.Drawing.Point(12, 42)
        ProgressFood.Name = "ProgressFood"
        ProgressFood.Size = New System.Drawing.Size(392, 8)
        ProgressFood.TabIndex = 59
        ' 
        ' UsedFood
        ' 
        UsedFood.AutoSize = True
        UsedFood.ForeColor = Drawing.SystemColors.ButtonFace
        UsedFood.Location = New System.Drawing.Point(13, 57)
        UsedFood.Name = "UsedFood"
        UsedFood.Size = New System.Drawing.Size(64, 20)
        UsedFood.TabIndex = 58
        UsedFood.Text = "0% used"
        ' 
        ' AmtLeftFood
        ' 
        AmtLeftFood.AutoSize = True
        AmtLeftFood.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftFood.Location = New System.Drawing.Point(294, 57)
        AmtLeftFood.Name = "AmtLeftFood"
        AmtLeftFood.Size = New System.Drawing.Size(102, 20)
        AmtLeftFood.TabIndex = 57
        AmtLeftFood.Text = "₱ 3,000.00 left"
        ' 
        ' slash2
        ' 
        slash2.AutoSize = True
        slash2.ForeColor = Drawing.SystemColors.ButtonFace
        slash2.Location = New System.Drawing.Point(310, 9)
        slash2.Name = "slash2"
        slash2.Size = New System.Drawing.Size(15, 20)
        slash2.TabIndex = 58
        slash2.Text = "/"
        ' 
        ' LimitFood
        ' 
        LimitFood.AutoSize = True
        LimitFood.ForeColor = Drawing.SystemColors.ButtonFace
        LimitFood.Location = New System.Drawing.Point(327, 9)
        LimitFood.Name = "LimitFood"
        LimitFood.Size = New System.Drawing.Size(76, 20)
        LimitFood.TabIndex = 57
        LimitFood.Text = "₱ 3,000.00"
        ' 
        ' SpentAmtFood
        ' 
        SpentAmtFood.AutoSize = True
        SpentAmtFood.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtFood.Location = New System.Drawing.Point(259, 9)
        SpentAmtFood.Name = "SpentAmtFood"
        SpentAmtFood.Size = New System.Drawing.Size(49, 20)
        SpentAmtFood.TabIndex = 56
        SpentAmtFood.Text = "₱ 0.00"
        ' 
        ' MBLFood
        ' 
        MBLFood.AutoSize = True
        MBLFood.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLFood.ForeColor = Drawing.SystemColors.ButtonFace
        MBLFood.Location = New System.Drawing.Point(43, 9)
        MBLFood.Name = "MBLFood"
        MBLFood.Size = New System.Drawing.Size(44, 20)
        MBLFood.TabIndex = 58
        MBLFood.Text = "Food"
        ' 
        ' MBLColorFood
        ' 
        MBLColorFood.Location = New System.Drawing.Point(13, 13)
        MBLColorFood.Name = "MBLColorFood"
        MBLColorFood.Size = New System.Drawing.Size(13, 13)
        MBLColorFood.TabIndex = 53
        ' 
        ' MBLCardPanel1
        ' 
        MBLCardPanel1.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        MBLCardPanel1.Controls.Add(ProgressHousing)
        MBLCardPanel1.Controls.Add(UsedHousing)
        MBLCardPanel1.Controls.Add(AmtLeftHousing)
        MBLCardPanel1.Controls.Add(slash1)
        MBLCardPanel1.Controls.Add(LimitHousing)
        MBLCardPanel1.Controls.Add(SpentAmtHousing)
        MBLCardPanel1.Controls.Add(MBLHousing)
        MBLCardPanel1.Controls.Add(MBLColorHousing)
        MBLCardPanel1.Location = New System.Drawing.Point(32, 60)
        MBLCardPanel1.Name = "MBLCardPanel1"
        MBLCardPanel1.Size = New System.Drawing.Size(415, 80)
        MBLCardPanel1.TabIndex = 7
        ' 
        ' ProgressHousing
        ' 
        ProgressHousing.Location = New System.Drawing.Point(12, 42)
        ProgressHousing.Name = "ProgressHousing"
        ProgressHousing.Size = New System.Drawing.Size(392, 8)
        ProgressHousing.TabIndex = 58
        ' 
        ' UsedHousing
        ' 
        UsedHousing.AutoSize = True
        UsedHousing.ForeColor = Drawing.SystemColors.ButtonFace
        UsedHousing.Location = New System.Drawing.Point(13, 57)
        UsedHousing.Name = "UsedHousing"
        UsedHousing.Size = New System.Drawing.Size(64, 20)
        UsedHousing.TabIndex = 57
        UsedHousing.Text = "0% used"
        ' 
        ' AmtLeftHousing
        ' 
        AmtLeftHousing.AutoSize = True
        AmtLeftHousing.ForeColor = Drawing.SystemColors.ButtonFace
        AmtLeftHousing.Location = New System.Drawing.Point(294, 57)
        AmtLeftHousing.Name = "AmtLeftHousing"
        AmtLeftHousing.Size = New System.Drawing.Size(110, 20)
        AmtLeftHousing.TabIndex = 56
        AmtLeftHousing.Text = "₱ 12,000.00 left"
        ' 
        ' slash1
        ' 
        slash1.AutoSize = True
        slash1.ForeColor = Drawing.SystemColors.ButtonFace
        slash1.Location = New System.Drawing.Point(310, 10)
        slash1.Name = "slash1"
        slash1.Size = New System.Drawing.Size(15, 20)
        slash1.TabIndex = 55
        slash1.Text = "/"
        ' 
        ' LimitHousing
        ' 
        LimitHousing.AutoSize = True
        LimitHousing.ForeColor = Drawing.SystemColors.ButtonFace
        LimitHousing.Location = New System.Drawing.Point(327, 10)
        LimitHousing.Name = "LimitHousing"
        LimitHousing.Size = New System.Drawing.Size(84, 20)
        LimitHousing.TabIndex = 54
        LimitHousing.Text = "₱ 12,000.00"
        ' 
        ' SpentAmtHousing
        ' 
        SpentAmtHousing.AutoSize = True
        SpentAmtHousing.ForeColor = Drawing.SystemColors.ButtonFace
        SpentAmtHousing.Location = New System.Drawing.Point(259, 10)
        SpentAmtHousing.Name = "SpentAmtHousing"
        SpentAmtHousing.Size = New System.Drawing.Size(49, 20)
        SpentAmtHousing.TabIndex = 53
        SpentAmtHousing.Text = "₱ 0.00"
        ' 
        ' MBLHousing
        ' 
        MBLHousing.AutoSize = True
        MBLHousing.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
        MBLHousing.ForeColor = Drawing.SystemColors.ButtonFace
        MBLHousing.Location = New System.Drawing.Point(43, 11)
        MBLHousing.Name = "MBLHousing"
        MBLHousing.Size = New System.Drawing.Size(67, 20)
        MBLHousing.TabIndex = 13
        MBLHousing.Text = "Housing"
        ' 
        ' MBLColorHousing
        ' 
        MBLColorHousing.Location = New System.Drawing.Point(13, 14)
        MBLColorHousing.Name = "MBLColorHousing"
        MBLColorHousing.Size = New System.Drawing.Size(13, 13)
        MBLColorHousing.TabIndex = 52
        ' 
        ' Button1
        ' 
        Button1.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        Button1.Location = New System.Drawing.Point(1219, 16)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(186, 38)
        Button1.TabIndex = 6
        Button1.Text = "Manage Limits"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' MBLIcon
        ' 
        MBLIcon.Location = New System.Drawing.Point(23, 13)
        MBLIcon.Name = "MBLIcon"
        MBLIcon.Size = New System.Drawing.Size(35, 38)
        MBLIcon.TabIndex = 5
        ' 
        ' WalAndAcc
        ' 
        WalAndAcc.AutoSize = True
        WalAndAcc.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        WalAndAcc.ForeColor = Drawing.SystemColors.ButtonFace
        WalAndAcc.Location = New System.Drawing.Point(75, 18)
        WalAndAcc.Name = "WalAndAcc"
        WalAndAcc.Size = New System.Drawing.Size(250, 25)
        WalAndAcc.TabIndex = 0
        WalAndAcc.Text = "Montly Budget Limits"
        ' 
        ' TotExpBG
        ' 
        TotExpBG.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        TotExpBG.Controls.Add(TotExpLbl)
        TotExpBG.Controls.Add(ExpAmt)
        TotExpBG.Controls.Add(ExpDesc)
        TotExpBG.Controls.Add(ExpIcon)
        TotExpBG.Location = New System.Drawing.Point(388, 237)
        TotExpBG.Name = "TotExpBG"
        TotExpBG.Size = New System.Drawing.Size(345, 150)
        TotExpBG.TabIndex = 9
        ' 
        ' TotExpLbl
        ' 
        TotExpLbl.AutoSize = True
        TotExpLbl.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TotExpLbl.ForeColor = Drawing.SystemColors.ButtonFace
        TotExpLbl.Location = New System.Drawing.Point(20, 20)
        TotExpLbl.Name = "TotExpLbl"
        TotExpLbl.Size = New System.Drawing.Size(181, 25)
        TotExpLbl.TabIndex = 0
        TotExpLbl.Text = "Total Expenses"
        ' 
        ' ExpAmt
        ' 
        ExpAmt.AutoSize = True
        ExpAmt.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        ExpAmt.ForeColor = Drawing.SystemColors.ButtonFace
        ExpAmt.Location = New System.Drawing.Point(20, 53)
        ExpAmt.Name = "ExpAmt"
        ExpAmt.Size = New System.Drawing.Size(61, 28)
        ExpAmt.TabIndex = 1
        ExpAmt.Text = "₱0.00"
        ' 
        ' ExpDesc
        ' 
        ExpDesc.AutoSize = True
        ExpDesc.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        ExpDesc.ForeColor = Drawing.SystemColors.ButtonFace
        ExpDesc.Location = New System.Drawing.Point(22, 105)
        ExpDesc.Name = "ExpDesc"
        ExpDesc.Size = New System.Drawing.Size(255, 25)
        ExpDesc.TabIndex = 2
        ExpDesc.Text = "Income minus Expenses"
        ' 
        ' ExpIcon
        ' 
        ExpIcon.Location = New System.Drawing.Point(295, 14)
        ExpIcon.Name = "ExpIcon"
        ExpIcon.Size = New System.Drawing.Size(35, 38)
        ExpIcon.TabIndex = 3
        ' 
        ' NetBalBG
        ' 
        NetBalBG.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        NetBalBG.Controls.Add(NetBal)
        NetBalBG.Controls.Add(AmtBal)
        NetBalBG.Controls.Add(BalDesc)
        NetBalBG.Controls.Add(NetBalIcon)
        NetBalBG.Location = New System.Drawing.Point(752, 237)
        NetBalBG.Name = "NetBalBG"
        NetBalBG.Size = New System.Drawing.Size(345, 150)
        NetBalBG.TabIndex = 8
        ' 
        ' NetBal
        ' 
        NetBal.AutoSize = True
        NetBal.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        NetBal.ForeColor = Drawing.SystemColors.ButtonFace
        NetBal.Location = New System.Drawing.Point(20, 20)
        NetBal.Name = "NetBal"
        NetBal.Size = New System.Drawing.Size(145, 25)
        NetBal.TabIndex = 0
        NetBal.Text = "Net Balance"
        ' 
        ' AmtBal
        ' 
        AmtBal.AutoSize = True
        AmtBal.Font = New System.Drawing.Font("Segoe UI", 12F)
        AmtBal.ForeColor = Drawing.SystemColors.ButtonFace
        AmtBal.Location = New System.Drawing.Point(20, 53)
        AmtBal.Name = "AmtBal"
        AmtBal.Size = New System.Drawing.Size(61, 28)
        AmtBal.TabIndex = 1
        AmtBal.Text = "₱0.00"
        ' 
        ' BalDesc
        ' 
        BalDesc.AutoSize = True
        BalDesc.Font = New System.Drawing.Font("Verdana", 12F)
        BalDesc.ForeColor = Drawing.SystemColors.ButtonFace
        BalDesc.Location = New System.Drawing.Point(22, 105)
        BalDesc.Name = "BalDesc"
        BalDesc.Size = New System.Drawing.Size(255, 25)
        BalDesc.TabIndex = 2
        BalDesc.Text = "Income minus Expenses"
        ' 
        ' NetBalIcon
        ' 
        NetBalIcon.Location = New System.Drawing.Point(295, 14)
        NetBalIcon.Name = "NetBalIcon"
        NetBalIcon.Size = New System.Drawing.Size(35, 38)
        NetBalIcon.TabIndex = 3
        ' 
        ' SavRateBG
        ' 
        SavRateBG.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        SavRateBG.Controls.Add(SavRate)
        SavRateBG.Controls.Add(SavRatePerc)
        SavRateBG.Controls.Add(SavRateDesc)
        SavRateBG.Controls.Add(IconSavRate)
        SavRateBG.Location = New System.Drawing.Point(1116, 237)
        SavRateBG.Name = "SavRateBG"
        SavRateBG.Size = New System.Drawing.Size(345, 150)
        SavRateBG.TabIndex = 9
        ' 
        ' SavRate
        ' 
        SavRate.AutoSize = True
        SavRate.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        SavRate.ForeColor = Drawing.SystemColors.ButtonFace
        SavRate.Location = New System.Drawing.Point(20, 20)
        SavRate.Name = "SavRate"
        SavRate.Size = New System.Drawing.Size(157, 25)
        SavRate.TabIndex = 0
        SavRate.Text = "Savings Rate"
        ' 
        ' SavRatePerc
        ' 
        SavRatePerc.AutoSize = True
        SavRatePerc.Font = New System.Drawing.Font("Segoe UI", 12F)
        SavRatePerc.ForeColor = Drawing.SystemColors.ButtonFace
        SavRatePerc.Location = New System.Drawing.Point(20, 53)
        SavRatePerc.Name = "SavRatePerc"
        SavRatePerc.Size = New System.Drawing.Size(39, 28)
        SavRatePerc.TabIndex = 1
        SavRatePerc.Text = "0%"
        ' 
        ' SavRateDesc
        ' 
        SavRateDesc.AutoSize = True
        SavRateDesc.Font = New System.Drawing.Font("Verdana", 12F)
        SavRateDesc.ForeColor = Drawing.SystemColors.ButtonFace
        SavRateDesc.Location = New System.Drawing.Point(22, 105)
        SavRateDesc.Name = "SavRateDesc"
        SavRateDesc.Size = New System.Drawing.Size(272, 25)
        SavRateDesc.TabIndex = 2
        SavRateDesc.Text = "Of monthly income raised"
        ' 
        ' IconSavRate
        ' 
        IconSavRate.Location = New System.Drawing.Point(295, 14)
        IconSavRate.Name = "IconSavRate"
        IconSavRate.Size = New System.Drawing.Size(35, 38)
        IconSavRate.TabIndex = 3
        ' 
        ' CashFlowBG
        ' 
        CashFlowBG.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        CashFlowBG.Controls.Add(ChartArea)
        CashFlowBG.Controls.Add(IncVSExpDesc)
        CashFlowBG.Controls.Add(CashFlowHistoryLabel)
        CashFlowBG.Controls.Add(CashFlowIcon)
        CashFlowBG.Location = New System.Drawing.Point(23, 425)
        CashFlowBG.Name = "CashFlowBG"
        CashFlowBG.Size = New System.Drawing.Size(854, 301)
        CashFlowBG.TabIndex = 10
        ' 
        ' ChartArea
        ' 
        ChartArea.Controls.Add(EmptyStateHeadingDesc)
        ChartArea.Controls.Add(EmptyStateHeading)
        ChartArea.Controls.Add(EmptyStateIcon)
        ChartArea.Location = New System.Drawing.Point(108, 69)
        ChartArea.Name = "ChartArea"
        ChartArea.Size = New System.Drawing.Size(626, 208)
        ChartArea.TabIndex = 13
        ' 
        ' EmptyStateHeadingDesc
        ' 
        EmptyStateHeadingDesc.AutoSize = True
        EmptyStateHeadingDesc.Font = New System.Drawing.Font("Verdana", 9F)
        EmptyStateHeadingDesc.ForeColor = Drawing.SystemColors.ButtonFace
        EmptyStateHeadingDesc.Location = New System.Drawing.Point(165, 147)
        EmptyStateHeadingDesc.Name = "EmptyStateHeadingDesc"
        EmptyStateHeadingDesc.Size = New System.Drawing.Size(304, 18)
        EmptyStateHeadingDesc.TabIndex = 15
        EmptyStateHeadingDesc.Text = "Add a transaction to see your cash flow"
        EmptyStateHeadingDesc.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' EmptyStateHeading
        ' 
        EmptyStateHeading.AutoSize = True
        EmptyStateHeading.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        EmptyStateHeading.ForeColor = Drawing.SystemColors.ButtonFace
        EmptyStateHeading.Location = New System.Drawing.Point(164, 123)
        EmptyStateHeading.Name = "EmptyStateHeading"
        EmptyStateHeading.Size = New System.Drawing.Size(306, 22)
        EmptyStateHeading.TabIndex = 14
        EmptyStateHeading.Text = "Your money story starts here"
        ' 
        ' EmptyStateIcon
        ' 
        EmptyStateIcon.Location = New System.Drawing.Point(279, 43)
        EmptyStateIcon.Name = "EmptyStateIcon"
        EmptyStateIcon.Size = New System.Drawing.Size(67, 67)
        EmptyStateIcon.TabIndex = 5
        ' 
        ' IncVSExpDesc
        ' 
        IncVSExpDesc.AutoSize = True
        IncVSExpDesc.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        IncVSExpDesc.ForeColor = Drawing.SystemColors.ButtonFace
        IncVSExpDesc.Location = New System.Drawing.Point(644, 27)
        IncVSExpDesc.Name = "IncVSExpDesc"
        IncVSExpDesc.Size = New System.Drawing.Size(186, 20)
        IncVSExpDesc.TabIndex = 12
        IncVSExpDesc.Text = "Income vs Expenses"
        ' 
        ' CashFlowHistoryLabel
        ' 
        CashFlowHistoryLabel.AutoSize = True
        CashFlowHistoryLabel.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        CashFlowHistoryLabel.ForeColor = Drawing.SystemColors.ButtonFace
        CashFlowHistoryLabel.Location = New System.Drawing.Point(65, 24)
        CashFlowHistoryLabel.Name = "CashFlowHistoryLabel"
        CashFlowHistoryLabel.Size = New System.Drawing.Size(215, 25)
        CashFlowHistoryLabel.TabIndex = 4
        CashFlowHistoryLabel.Text = "Cash Flow History"
        ' 
        ' CashFlowIcon
        ' 
        CashFlowIcon.Location = New System.Drawing.Point(19, 20)
        CashFlowIcon.Name = "CashFlowIcon"
        CashFlowIcon.Size = New System.Drawing.Size(35, 38)
        CashFlowIcon.TabIndex = 4
        ' 
        ' CardBG1
        ' 
        CardBG1.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        CardBG1.Controls.Add(TotalIncome)
        CardBG1.Controls.Add(AmtIncome)
        CardBG1.Controls.Add(IncDesc)
        CardBG1.Controls.Add(IconInc)
        CardBG1.Location = New System.Drawing.Point(24, 237)
        CardBG1.Name = "CardBG1"
        CardBG1.Size = New System.Drawing.Size(345, 150)
        CardBG1.TabIndex = 6
        ' 
        ' TotalIncome
        ' 
        TotalIncome.AutoSize = True
        TotalIncome.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TotalIncome.ForeColor = Drawing.SystemColors.ButtonFace
        TotalIncome.Location = New System.Drawing.Point(20, 20)
        TotalIncome.Name = "TotalIncome"
        TotalIncome.Size = New System.Drawing.Size(161, 25)
        TotalIncome.TabIndex = 0
        TotalIncome.Text = "Total Income"
        ' 
        ' AmtIncome
        ' 
        AmtIncome.AutoSize = True
        AmtIncome.Font = New System.Drawing.Font("Segoe UI", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        AmtIncome.ForeColor = Drawing.SystemColors.ButtonFace
        AmtIncome.Location = New System.Drawing.Point(20, 53)
        AmtIncome.Name = "AmtIncome"
        AmtIncome.Size = New System.Drawing.Size(61, 28)
        AmtIncome.TabIndex = 1
        AmtIncome.Text = "₱0.00"
        ' 
        ' IncDesc
        ' 
        IncDesc.AutoSize = True
        IncDesc.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        IncDesc.ForeColor = Drawing.SystemColors.ButtonFace
        IncDesc.Location = New System.Drawing.Point(22, 105)
        IncDesc.Name = "IncDesc"
        IncDesc.Size = New System.Drawing.Size(202, 25)
        IncDesc.TabIndex = 2
        IncDesc.Text = "Income this month"
        ' 
        ' IconInc
        ' 
        IconInc.Location = New System.Drawing.Point(295, 14)
        IconInc.Name = "IconInc"
        IconInc.Size = New System.Drawing.Size(35, 38)
        IconInc.TabIndex = 3
        ' 
        ' ExpBDBG
        ' 
        ExpBDBG.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        ExpBDBG.Controls.Add(Perc8)
        ExpBDBG.Controls.Add(Perc7)
        ExpBDBG.Controls.Add(ExpBDHeading)
        ExpBDBG.Controls.Add(ExpBdIcon)
        ExpBDBG.Controls.Add(DonutChartArea)
        ExpBDBG.Controls.Add(ColorLegend1)
        ExpBDBG.Controls.Add(color2)
        ExpBDBG.Controls.Add(color3)
        ExpBDBG.Controls.Add(color4)
        ExpBDBG.Controls.Add(color5)
        ExpBDBG.Controls.Add(color6)
        ExpBDBG.Controls.Add(color7)
        ExpBDBG.Controls.Add(color8)
        ExpBDBG.Controls.Add(Housing)
        ExpBDBG.Controls.Add(Food)
        ExpBDBG.Controls.Add(Transportation)
        ExpBDBG.Controls.Add(Utilities)
        ExpBDBG.Controls.Add(Entertainment)
        ExpBDBG.Controls.Add(Shopping)
        ExpBDBG.Controls.Add(Perc5)
        ExpBDBG.Controls.Add(Miscellaneous)
        ExpBDBG.Controls.Add(Health)
        ExpBDBG.Controls.Add(Perc1)
        ExpBDBG.Controls.Add(Perc2)
        ExpBDBG.Controls.Add(Perc3)
        ExpBDBG.Controls.Add(Perc6)
        ExpBDBG.Controls.Add(Perc4)
        ExpBDBG.Location = New System.Drawing.Point(902, 424)
        ExpBDBG.Name = "ExpBDBG"
        ExpBDBG.Size = New System.Drawing.Size(559, 302)
        ExpBDBG.TabIndex = 11
        ' 
        ' Perc8
        ' 
        Perc8.AutoSize = True
        Perc8.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc8.ForeColor = Drawing.SystemColors.ButtonFace
        Perc8.Location = New System.Drawing.Point(476, 238)
        Perc8.Name = "Perc8"
        Perc8.Size = New System.Drawing.Size(39, 28)
        Perc8.TabIndex = 65
        Perc8.Text = "0%"
        ' 
        ' Perc7
        ' 
        Perc7.AutoSize = True
        Perc7.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc7.ForeColor = Drawing.SystemColors.ButtonFace
        Perc7.Location = New System.Drawing.Point(476, 214)
        Perc7.Name = "Perc7"
        Perc7.Size = New System.Drawing.Size(39, 28)
        Perc7.TabIndex = 64
        Perc7.Text = "0%"
        ' 
        ' ExpBDHeading
        ' 
        ExpBDHeading.AutoSize = True
        ExpBDHeading.Font = New System.Drawing.Font("Verdana", 12F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ExpBDHeading.ForeColor = Drawing.SystemColors.ButtonFace
        ExpBDHeading.Location = New System.Drawing.Point(67, 29)
        ExpBDHeading.Name = "ExpBDHeading"
        ExpBDHeading.Size = New System.Drawing.Size(238, 25)
        ExpBDHeading.TabIndex = 41
        ExpBDHeading.Text = "Expense Breakdown"
        ' 
        ' ExpBdIcon
        ' 
        ExpBdIcon.Location = New System.Drawing.Point(23, 24)
        ExpBdIcon.Name = "ExpBdIcon"
        ExpBdIcon.Size = New System.Drawing.Size(35, 38)
        ExpBdIcon.TabIndex = 40
        ' 
        ' DonutChartArea
        ' 
        DonutChartArea.Controls.Add(DonutChartAmt)
        DonutChartArea.Controls.Add(DonutChartDesc)
        DonutChartArea.Location = New System.Drawing.Point(38, 84)
        DonutChartArea.Name = "DonutChartArea"
        DonutChartArea.Size = New System.Drawing.Size(182, 182)
        DonutChartArea.TabIndex = 42
        ' 
        ' DonutChartAmt
        ' 
        DonutChartAmt.AutoSize = True
        DonutChartAmt.ForeColor = Drawing.SystemColors.ButtonFace
        DonutChartAmt.Location = New System.Drawing.Point(50, 72)
        DonutChartAmt.Name = "DonutChartAmt"
        DonutChartAmt.Size = New System.Drawing.Size(45, 20)
        DonutChartAmt.TabIndex = 17
        DonutChartAmt.Text = "₱0.00"
        DonutChartAmt.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' DonutChartDesc
        ' 
        DonutChartDesc.AutoSize = True
        DonutChartDesc.ForeColor = Drawing.SystemColors.ButtonFace
        DonutChartDesc.Location = New System.Drawing.Point(31, 47)
        DonutChartDesc.Name = "DonutChartDesc"
        DonutChartDesc.Size = New System.Drawing.Size(84, 20)
        DonutChartDesc.TabIndex = 16
        DonutChartDesc.Text = "Total Spent"
        DonutChartDesc.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' ColorLegend1
        ' 
        ColorLegend1.Location = New System.Drawing.Point(257, 84)
        ColorLegend1.Name = "ColorLegend1"
        ColorLegend1.Size = New System.Drawing.Size(13, 13)
        ColorLegend1.TabIndex = 43
        ' 
        ' color2
        ' 
        color2.Location = New System.Drawing.Point(257, 106)
        color2.Name = "color2"
        color2.Size = New System.Drawing.Size(13, 13)
        color2.TabIndex = 44
        ' 
        ' color3
        ' 
        color3.Location = New System.Drawing.Point(257, 129)
        color3.Name = "color3"
        color3.Size = New System.Drawing.Size(13, 13)
        color3.TabIndex = 45
        ' 
        ' color4
        ' 
        color4.Location = New System.Drawing.Point(257, 151)
        color4.Name = "color4"
        color4.Size = New System.Drawing.Size(13, 13)
        color4.TabIndex = 46
        ' 
        ' color5
        ' 
        color5.Location = New System.Drawing.Point(257, 175)
        color5.Name = "color5"
        color5.Size = New System.Drawing.Size(13, 13)
        color5.TabIndex = 47
        ' 
        ' color6
        ' 
        color6.Location = New System.Drawing.Point(257, 199)
        color6.Name = "color6"
        color6.Size = New System.Drawing.Size(13, 13)
        color6.TabIndex = 48
        ' 
        ' color7
        ' 
        color7.Location = New System.Drawing.Point(257, 223)
        color7.Name = "color7"
        color7.Size = New System.Drawing.Size(13, 13)
        color7.TabIndex = 49
        ' 
        ' color8
        ' 
        color8.Location = New System.Drawing.Point(257, 247)
        color8.Name = "color8"
        color8.Size = New System.Drawing.Size(13, 13)
        color8.TabIndex = 50
        ' 
        ' Housing
        ' 
        Housing.AutoSize = True
        Housing.Font = New System.Drawing.Font("Segoe UI", 12F)
        Housing.ForeColor = Drawing.SystemColors.ButtonFace
        Housing.Location = New System.Drawing.Point(274, 74)
        Housing.Name = "Housing"
        Housing.Size = New System.Drawing.Size(85, 28)
        Housing.TabIndex = 51
        Housing.Text = "Housing"
        ' 
        ' Food
        ' 
        Food.AutoSize = True
        Food.Font = New System.Drawing.Font("Segoe UI", 12F)
        Food.ForeColor = Drawing.SystemColors.ButtonFace
        Food.Location = New System.Drawing.Point(274, 96)
        Food.Name = "Food"
        Food.Size = New System.Drawing.Size(58, 28)
        Food.TabIndex = 52
        Food.Text = "Food"
        ' 
        ' Transportation
        ' 
        Transportation.AutoSize = True
        Transportation.Font = New System.Drawing.Font("Segoe UI", 12F)
        Transportation.ForeColor = Drawing.SystemColors.ButtonFace
        Transportation.Location = New System.Drawing.Point(274, 119)
        Transportation.Name = "Transportation"
        Transportation.Size = New System.Drawing.Size(139, 28)
        Transportation.TabIndex = 53
        Transportation.Text = "Transportation"
        ' 
        ' Utilities
        ' 
        Utilities.AutoSize = True
        Utilities.Font = New System.Drawing.Font("Segoe UI", 12F)
        Utilities.ForeColor = Drawing.SystemColors.ButtonFace
        Utilities.Location = New System.Drawing.Point(274, 142)
        Utilities.Name = "Utilities"
        Utilities.Size = New System.Drawing.Size(78, 28)
        Utilities.TabIndex = 54
        Utilities.Text = "Utilities"
        ' 
        ' Entertainment
        ' 
        Entertainment.AutoSize = True
        Entertainment.Font = New System.Drawing.Font("Segoe UI", 12F)
        Entertainment.ForeColor = Drawing.SystemColors.ButtonFace
        Entertainment.Location = New System.Drawing.Point(274, 165)
        Entertainment.Name = "Entertainment"
        Entertainment.Size = New System.Drawing.Size(135, 28)
        Entertainment.TabIndex = 55
        Entertainment.Text = "Entertainment"
        ' 
        ' Shopping
        ' 
        Shopping.AutoSize = True
        Shopping.Font = New System.Drawing.Font("Segoe UI", 12F)
        Shopping.ForeColor = Drawing.SystemColors.ButtonFace
        Shopping.Location = New System.Drawing.Point(274, 214)
        Shopping.Name = "Shopping"
        Shopping.Size = New System.Drawing.Size(98, 28)
        Shopping.TabIndex = 57
        Shopping.Text = "Shopping"
        ' 
        ' Perc5
        ' 
        Perc5.AutoSize = True
        Perc5.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc5.ForeColor = Drawing.SystemColors.ButtonFace
        Perc5.Location = New System.Drawing.Point(476, 165)
        Perc5.Name = "Perc5"
        Perc5.Size = New System.Drawing.Size(39, 28)
        Perc5.TabIndex = 62
        Perc5.Text = "0%"
        ' 
        ' Miscellaneous
        ' 
        Miscellaneous.AutoSize = True
        Miscellaneous.Font = New System.Drawing.Font("Segoe UI", 12F)
        Miscellaneous.ForeColor = Drawing.SystemColors.ButtonFace
        Miscellaneous.Location = New System.Drawing.Point(274, 238)
        Miscellaneous.Name = "Miscellaneous"
        Miscellaneous.Size = New System.Drawing.Size(134, 28)
        Miscellaneous.TabIndex = 58
        Miscellaneous.Text = "Miscellaneous"
        ' 
        ' Health
        ' 
        Health.AutoSize = True
        Health.Font = New System.Drawing.Font("Segoe UI", 12F)
        Health.ForeColor = Drawing.SystemColors.ButtonFace
        Health.Location = New System.Drawing.Point(274, 190)
        Health.Name = "Health"
        Health.Size = New System.Drawing.Size(69, 28)
        Health.TabIndex = 56
        Health.Text = "Health"
        ' 
        ' Perc1
        ' 
        Perc1.AutoSize = True
        Perc1.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc1.ForeColor = Drawing.SystemColors.ButtonFace
        Perc1.Location = New System.Drawing.Point(476, 74)
        Perc1.Name = "Perc1"
        Perc1.Size = New System.Drawing.Size(39, 28)
        Perc1.TabIndex = 39
        Perc1.Text = "0%"
        ' 
        ' Perc2
        ' 
        Perc2.AutoSize = True
        Perc2.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc2.ForeColor = Drawing.SystemColors.ButtonFace
        Perc2.Location = New System.Drawing.Point(476, 96)
        Perc2.Name = "Perc2"
        Perc2.Size = New System.Drawing.Size(39, 28)
        Perc2.TabIndex = 59
        Perc2.Text = "0%"
        ' 
        ' Perc3
        ' 
        Perc3.AutoSize = True
        Perc3.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc3.ForeColor = Drawing.SystemColors.ButtonFace
        Perc3.Location = New System.Drawing.Point(476, 119)
        Perc3.Name = "Perc3"
        Perc3.Size = New System.Drawing.Size(39, 28)
        Perc3.TabIndex = 60
        Perc3.Text = "0%"
        ' 
        ' Perc6
        ' 
        Perc6.AutoSize = True
        Perc6.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc6.ForeColor = Drawing.SystemColors.ButtonFace
        Perc6.Location = New System.Drawing.Point(476, 190)
        Perc6.Name = "Perc6"
        Perc6.Size = New System.Drawing.Size(39, 28)
        Perc6.TabIndex = 63
        Perc6.Text = "0%"
        ' 
        ' Perc4
        ' 
        Perc4.AutoSize = True
        Perc4.Font = New System.Drawing.Font("Segoe UI", 12F)
        Perc4.ForeColor = Drawing.SystemColors.ButtonFace
        Perc4.Location = New System.Drawing.Point(476, 142)
        Perc4.Name = "Perc4"
        Perc4.Size = New System.Drawing.Size(39, 28)
        Perc4.TabIndex = 61
        Perc4.Text = "0%"
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        pnlHeader.Controls.Add(PictureBox1)
        pnlHeader.Controls.Add(LogoMain)
        pnlHeader.Controls.Add(PesoLogo)
        pnlHeader.Controls.Add(BudgetMateTitle)
        pnlHeader.Controls.Add(Tagline)
        pnlHeader.Controls.Add(ToggleTheme)
        pnlHeader.Controls.Add(DataDropdown)
        pnlHeader.Controls.Add(AddTransaction)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New System.Drawing.Point(20, 20)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New System.Drawing.Size(1855, 90)
        pnlHeader.TabIndex = 0
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Drawing.Color.Transparent
        PictureBox1.Image = My.Resources.Resources.PesoMint
        PictureBox1.Location = New System.Drawing.Point(89, -54)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(380, 190)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 20
        PictureBox1.TabStop = False
        ' 
        ' LogoMain
        ' 
        LogoMain.BorderStyle = BorderStyle.FixedSingle
        LogoMain.Image = CType(resources.GetObject("LogoMain.Image"), Drawing.Image)
        LogoMain.Location = New System.Drawing.Point(7, 7)
        LogoMain.Name = "LogoMain"
        LogoMain.Size = New System.Drawing.Size(76, 76)
        LogoMain.SizeMode = PictureBoxSizeMode.StretchImage
        LogoMain.TabIndex = 19
        LogoMain.TabStop = False
        ' 
        ' PesoLogo
        ' 
        PesoLogo.AutoSize = True
        PesoLogo.BackColor = Drawing.Color.FromArgb(CByte(0), CByte(192), CByte(0))
        PesoLogo.Font = New System.Drawing.Font("Segoe UI", 18F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        PesoLogo.ForeColor = Drawing.Color.White
        PesoLogo.Location = New System.Drawing.Point(23, 26)
        PesoLogo.Name = "PesoLogo"
        PesoLogo.Size = New System.Drawing.Size(37, 41)
        PesoLogo.TabIndex = 1
        PesoLogo.Text = "₱"
        ' 
        ' BudgetMateTitle
        ' 
        BudgetMateTitle.AutoSize = True
        BudgetMateTitle.BackColor = Drawing.Color.Transparent
        BudgetMateTitle.Font = New System.Drawing.Font("Copperplate Gothic Light", 24F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        BudgetMateTitle.ForeColor = Drawing.SystemColors.ButtonFace
        BudgetMateTitle.Location = New System.Drawing.Point(98, 15)
        BudgetMateTitle.Name = "BudgetMateTitle"
        BudgetMateTitle.Size = New System.Drawing.Size(224, 43)
        BudgetMateTitle.TabIndex = 1
        BudgetMateTitle.Text = "PesoMint"
        ' 
        ' Tagline
        ' 
        Tagline.AutoSize = True
        Tagline.ForeColor = Drawing.SystemColors.ButtonFace
        Tagline.Location = New System.Drawing.Point(109, 53)
        Tagline.Name = "Tagline"
        Tagline.Size = New System.Drawing.Size(187, 20)
        Tagline.TabIndex = 2
        Tagline.Text = "Your finances, made easier."
        ' 
        ' ToggleTheme
        ' 
        ToggleTheme.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ToggleTheme.BackgroundImageLayout = ImageLayout.None
        ToggleTheme.Image = CType(resources.GetObject("ToggleTheme.Image"), Drawing.Image)
        ToggleTheme.Location = New System.Drawing.Point(1445, 13)
        ToggleTheme.Name = "ToggleTheme"
        ToggleTheme.Size = New System.Drawing.Size(65, 63)
        ToggleTheme.TabIndex = 1
        ToggleTheme.UseVisualStyleBackColor = True
        ' 
        ' DataDropdown
        ' 
        DataDropdown.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        DataDropdown.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        DataDropdown.FormattingEnabled = True
        DataDropdown.Location = New System.Drawing.Point(1526, 30)
        DataDropdown.Name = "DataDropdown"
        DataDropdown.Size = New System.Drawing.Size(130, 30)
        DataDropdown.TabIndex = 1
        DataDropdown.Text = "Data"
        ' 
        ' AddTransaction
        ' 
        AddTransaction.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        AddTransaction.Font = New System.Drawing.Font("Verdana", 9F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        AddTransaction.Location = New System.Drawing.Point(1674, 24)
        AddTransaction.Name = "AddTransaction"
        AddTransaction.Size = New System.Drawing.Size(153, 41)
        AddTransaction.TabIndex = 3
        AddTransaction.Text = "+ Add Transaction"
        AddTransaction.UseVisualStyleBackColor = True
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(120F, 120F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Drawing.Color.FromArgb(CByte(16), CByte(24), CByte(39))
        ClientSize = New System.Drawing.Size(1895, 1055)
        Controls.Add(pnlDashboard)
        MinimumSize = New System.Drawing.Size(1000, 700)
        Name = "MainForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "PesoMint"
        WindowState = FormWindowState.Maximized
        pnlDashboard.ResumeLayout(False)
        PnlContent.ResumeLayout(False)
        PnlContent.PerformLayout()
        TransHistoryPnl.ResumeLayout(False)
        TransHistoryPnl.PerformLayout()
        RecTransPnl.ResumeLayout(False)
        RecTransPnl.PerformLayout()
        MonthNavContainer.ResumeLayout(False)
        MonthNavContainer.PerformLayout()
        SavingGoalsPnl.ResumeLayout(False)
        SavingGoalsPnl.PerformLayout()
        WalletsAndAccsPnl.ResumeLayout(False)
        WalletsAndAccsPnl.PerformLayout()
        WalletCard4.ResumeLayout(False)
        WalletCard4.PerformLayout()
        WalletCard3.ResumeLayout(False)
        WalletCard3.PerformLayout()
        WalletCard2.ResumeLayout(False)
        WalletCard2.PerformLayout()
        WalletCard1.ResumeLayout(False)
        WalletCard1.PerformLayout()
        MBLPnl.ResumeLayout(False)
        MBLPnl.PerformLayout()
        MBLCardPanel6.ResumeLayout(False)
        MBLCardPanel6.PerformLayout()
        MBLCardPanel8.ResumeLayout(False)
        MBLCardPanel8.PerformLayout()
        MBLCardPanel3.ResumeLayout(False)
        MBLCardPanel3.PerformLayout()
        MBLCardPanel7.ResumeLayout(False)
        MBLCardPanel7.PerformLayout()
        MBLCardPanel5.ResumeLayout(False)
        MBLCardPanel5.PerformLayout()
        MBLCardPanel4.ResumeLayout(False)
        MBLCardPanel4.PerformLayout()
        MBLCardPanel2.ResumeLayout(False)
        MBLCardPanel2.PerformLayout()
        MBLCardPanel1.ResumeLayout(False)
        MBLCardPanel1.PerformLayout()
        TotExpBG.ResumeLayout(False)
        TotExpBG.PerformLayout()
        NetBalBG.ResumeLayout(False)
        NetBalBG.PerformLayout()
        SavRateBG.ResumeLayout(False)
        SavRateBG.PerformLayout()
        CashFlowBG.ResumeLayout(False)
        CashFlowBG.PerformLayout()
        ChartArea.ResumeLayout(False)
        ChartArea.PerformLayout()
        CardBG1.ResumeLayout(False)
        CardBG1.PerformLayout()
        ExpBDBG.ResumeLayout(False)
        ExpBDBG.PerformLayout()
        DonutChartArea.ResumeLayout(False)
        DonutChartArea.PerformLayout()
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LogoMain, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlDashboard As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents PesoLogo As System.Windows.Forms.Label
    Friend WithEvents Tagline As System.Windows.Forms.Label
    Friend WithEvents BudgetMateTitle As System.Windows.Forms.Label
    Friend WithEvents DataDropdown As System.Windows.Forms.ComboBox
    Friend WithEvents ToggleTheme As System.Windows.Forms.Button
    Friend WithEvents HeadingDescription As System.Windows.Forms.Label
    Friend WithEvents MainHeading As System.Windows.Forms.Label
    Friend WithEvents SectionLabel As System.Windows.Forms.Label
    Friend WithEvents AddTransaction As System.Windows.Forms.Button
    Friend WithEvents ViewMonth As System.Windows.Forms.Label
    Friend WithEvents MonthNavContainer As System.Windows.Forms.Panel
    Friend WithEvents NextMonth As System.Windows.Forms.Button
    Friend WithEvents PreviousMonth As System.Windows.Forms.Button
    Friend WithEvents MonthAndYear As System.Windows.Forms.Label
    Friend WithEvents CardBG1 As CardPanel
    Friend WithEvents IncDesc As System.Windows.Forms.Label
    Friend WithEvents AmtIncome As System.Windows.Forms.Label
    Friend WithEvents TotalIncome As System.Windows.Forms.Label
    Friend WithEvents NetBalBG As CardPanel
    Friend WithEvents NetBalIcon As System.Windows.Forms.Panel
    Friend WithEvents BalDesc As System.Windows.Forms.Label
    Friend WithEvents AmtBal As System.Windows.Forms.Label
    Friend WithEvents NetBal As System.Windows.Forms.Label
    Friend WithEvents IconInc As System.Windows.Forms.Panel
    Friend WithEvents SavRateBG As CardPanel
    Friend WithEvents IconSavRate As System.Windows.Forms.Panel
    Friend WithEvents SavRateDesc As System.Windows.Forms.Label
    Friend WithEvents SavRatePerc As System.Windows.Forms.Label
    Friend WithEvents SavRate As System.Windows.Forms.Label
    Friend WithEvents CashFlowBG As CardPanel
    Friend WithEvents ExpBDBG As CardPanel
    Friend WithEvents CashFlowIcon As System.Windows.Forms.Panel
    Friend WithEvents CashFlowHistoryLabel As System.Windows.Forms.Label
    Friend WithEvents ChartArea As System.Windows.Forms.Panel
    Friend WithEvents IncVSExpDesc As System.Windows.Forms.Label
    Friend WithEvents EmptyStateHeading As System.Windows.Forms.Label
    Friend WithEvents EmptyStateIcon As System.Windows.Forms.Panel
    Friend WithEvents EmptyStateHeadingDesc As System.Windows.Forms.Label
    Friend WithEvents PnlContent As System.Windows.Forms.Panel
    Friend WithEvents ExpBDHeading As System.Windows.Forms.Label
    Friend WithEvents ExpBdIcon As System.Windows.Forms.Panel
    Friend WithEvents DonutChartArea As System.Windows.Forms.Panel
    Friend WithEvents DonutChartAmt As System.Windows.Forms.Label
    Friend WithEvents DonutChartDesc As System.Windows.Forms.Label
    Friend WithEvents ColorLegend1 As System.Windows.Forms.Panel
    Friend WithEvents color2 As System.Windows.Forms.Panel
    Friend WithEvents color3 As System.Windows.Forms.Panel
    Friend WithEvents color4 As System.Windows.Forms.Panel
    Friend WithEvents color5 As System.Windows.Forms.Panel
    Friend WithEvents color6 As System.Windows.Forms.Panel
    Friend WithEvents color7 As System.Windows.Forms.Panel
    Friend WithEvents color8 As System.Windows.Forms.Panel
    Friend WithEvents Housing As System.Windows.Forms.Label
    Friend WithEvents Food As System.Windows.Forms.Label
    Friend WithEvents Transportation As System.Windows.Forms.Label
    Friend WithEvents Utilities As System.Windows.Forms.Label
    Friend WithEvents Entertainment As System.Windows.Forms.Label
    Friend WithEvents Shopping As System.Windows.Forms.Label
    Friend WithEvents Perc5 As System.Windows.Forms.Label
    Friend WithEvents Miscellaneous As System.Windows.Forms.Label
    Friend WithEvents Health As System.Windows.Forms.Label
    Friend WithEvents Perc1 As System.Windows.Forms.Label
    Friend WithEvents Perc2 As System.Windows.Forms.Label
    Friend WithEvents Perc3 As System.Windows.Forms.Label
    Friend WithEvents Perc6 As System.Windows.Forms.Label
    Friend WithEvents Perc4 As System.Windows.Forms.Label
    Friend WithEvents TotExpBG As CardPanel
    Friend WithEvents TotExpLbl As System.Windows.Forms.Label
    Friend WithEvents ExpAmt As System.Windows.Forms.Label
    Friend WithEvents ExpDesc As System.Windows.Forms.Label
    Friend WithEvents ExpIcon As System.Windows.Forms.Panel
    Friend WithEvents WalletsAndAccsPnl As CardPanel
    Friend WithEvents SavingGoalsPnl As CardPanel
    Friend WithEvents MBLPnl As CardPanel
    Friend WithEvents WalAndAcc As System.Windows.Forms.Label
    Friend WithEvents MBLIcon As System.Windows.Forms.Panel
    Friend WithEvents MBLCardPanel3 As CardPanel
    Friend WithEvents MBLCardPanel2 As CardPanel
    Friend WithEvents MBLCardPanel1 As CardPanel
    Friend WithEvents MBLCardPanel6 As CardPanel
    Friend WithEvents MBLCardPanel8 As CardPanel
    Friend WithEvents MBLCardPanel5 As CardPanel
    Friend WithEvents MBLColorUtil As Panel
    Friend WithEvents MBLCardPanel4 As CardPanel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents MBLColorHealth As System.Windows.Forms.Panel
    Friend WithEvents MBLColorEnt As System.Windows.Forms.Panel
    Friend WithEvents MBLCardPanel7 As CardPanel
    Friend WithEvents MBLColorMisc As System.Windows.Forms.Panel
    Friend WithEvents MBLColorTranspo As System.Windows.Forms.Panel
    Friend WithEvents MBLColorShop As System.Windows.Forms.Panel
    Friend WithEvents MBLColorFood As System.Windows.Forms.Panel
    Friend WithEvents MBLColorHousing As System.Windows.Forms.Panel
    Friend WithEvents MBLHealth As Label
    Friend WithEvents MBLTransportation As Label
    Friend WithEvents MBLEntertainment As Label
    Friend WithEvents MBLUtilities As Label
    Friend WithEvents MBLHousing As Label
    Friend WithEvents MBLMiscellaneous As Label
    Friend WithEvents MBLShopping As Label
    Friend WithEvents MBLFood As Label
    Friend WithEvents SpentAmtHousing As Label
    Friend WithEvents LimitHealth As Label
    Friend WithEvents LimitFood As Label
    Friend WithEvents SpentAmtFood As Label
    Friend WithEvents slash1 As Label
    Friend WithEvents LimitHousing As Label
    Friend WithEvents slash2 As System.Windows.Forms.Label
    Friend WithEvents slash3 As Label
    Friend WithEvents LimitTransportation As Label
    Friend WithEvents SpentAmtTranspo As Label
    Friend WithEvents slash5 As Label
    Friend WithEvents LimitEnt As Label
    Friend WithEvents SpentAmtEntertainment As Label
    Friend WithEvents slash4 As Label
    Friend WithEvents LimitUtilities As Label
    Friend WithEvents SpentAmtUtilities As Label
    Friend WithEvents slash6 As Label
    Friend WithEvents SpentAmtHealth As Label
    Friend WithEvents slash8 As Label
    Friend WithEvents LimitMisc As Label
    Friend WithEvents SpentAmtMiscellaneous As Label
    Friend WithEvents slash7 As Label
    Friend WithEvents LimitShopping As Label
    Friend WithEvents SpentAmtShopping As Label
    Friend WithEvents AmtLeftHousing As Label
    Friend WithEvents Perc8 As Label
    Friend WithEvents Perc7 As Label
    Friend WithEvents AmtLeftFood As Label
    Friend WithEvents AmtLeftHealth As Label
    Friend WithEvents AmtLeftMisc As Label
    Friend WithEvents AmtLeftTranspo As Label
    Friend WithEvents AmtLeftShop As Label
    Friend WithEvents AmtLeftEnt As Label
    Friend WithEvents AmtLeftUtil As Label
    Friend WithEvents UsedHealth As Label
    Friend WithEvents UsedMisc As Label
    Friend WithEvents UsedTranspo As Label
    Friend WithEvents UsedShop As Label
    Friend WithEvents UsedEnt As Label
    Friend WithEvents ProgressUtil As Panel
    Friend WithEvents UsedUtil As Label
    Friend WithEvents UsedFood As Label
    Friend WithEvents ProgressHousing As Panel
    Friend WithEvents UsedHousing As Label
    Friend WithEvents ProgressHealth As Panel
    Friend WithEvents ProgressMisc As Panel
    Friend WithEvents ProgressTranspo As Panel
    Friend WithEvents ProgressShop As Panel
    Friend WithEvents ProgressEnt As Panel
    Friend WithEvents ProgressFood As Panel
    Friend WithEvents WalletAndAccIcon As Panel
    Friend WithEvents WalletsHeading As Label
    Friend WithEvents RecTransPnl As CardPanel
    Friend WithEvents AddAccBtn As Button
    Friend WithEvents TransferBtn As Button
    Friend WithEvents WalletCard4 As CardPanel
    Friend WithEvents WalletCard3 As CardPanel
    Friend WithEvents WalletCard2 As CardPanel
    Friend WithEvents WalletCard1 As CardPanel
    Friend WithEvents CashHeading As Label
    Friend WithEvents SvGIcon As Panel
    Friend WithEvents SavGoalsHeading As Label
    Friend WithEvents AmtBDO As Label
    Friend WithEvents BDOHeading As Label
    Friend WithEvents AmtGCash As Label
    Friend WithEvents GCashHeading As Label
    Friend WithEvents AmtMaya As Label
    Friend WithEvents MayaHeading As Label
    Friend WithEvents AmtCash As Label
    Friend WithEvents AddNewGoal As Button
    Friend WithEvents CreateFirstGoal As Label
    Friend WithEvents EmptySavGoalsSubHeading As Label
    Friend WithEvents EmptySavGoalsHeading As Label
    Friend WithEvents EmptySavGoalsIcon As Panel
    Friend WithEvents TransHistoryPnl As CardPanel
    Friend WithEvents BottomLeftBGText As Label
    Friend WithEvents BottomRightBGText As Label
    Friend WithEvents RecTransIcon As Panel
    Friend WithEvents RecTranHeading As Label
    Friend WithEvents EmptyRecTransDesc As Label
    Friend WithEvents RecTransDesc As Label
    Friend WithEvents ActiveCounter As Label
    Friend WithEvents RecordsCount As Label
    Friend WithEvents TransactionHistoryIcon As Panel
    Friend WithEvents TransactionHistoryHeading As Label
    Friend WithEvents TransactionSearchBox As TextBox
    Friend WithEvents FilterReset As Button
    Friend WithEvents PaymentMethodFilter As ComboBox
    Friend WithEvents CategoryFilter As ComboBox
    Friend WithEvents TypeFilter As ComboBox
    Friend WithEvents AddTransButton As Button
    Friend WithEvents EmptyTransDesc As Label
    Friend WithEvents EmptyTransactionHeading As Label
    Friend WithEvents LogoMain As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
