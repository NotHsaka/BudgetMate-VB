Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

' Base for all pop-up forms: themed background, heading, error line, Cancel / Save buttons.
<DesignerCategory("")>
Public MustInherit Class DialogBase
    Inherits Form

    Protected Const Pad As Integer = 28

    Private ReadOnly _eyebrow As String
    Private ReadOnly _heading As String
    Private ReadOnly _errorLabel As New Label()

    Protected Sub New(eyebrow As String, heading As String, width As Integer)
        _eyebrow = eyebrow
        _heading = heading
        Text = heading
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        BackColor = ColSurface
        ForeColor = ColInk
        Font = Fnt(False, 10.0F)
        DoubleBuffered = True
        ClientSize = New Size(width, 400)
    End Sub

    Protected ReadOnly Property ContentTop As Integer
        Get
            Return 88
        End Get
    End Property

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Win.ApplyTitleBar(Me)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Txt(e.Graphics, _eyebrow, Fnt(False, 8.0F, True), ColGreen, Pad, 22)
        Txt(e.Graphics, _heading, Fnt(True, 15.0F, True), ColInk, Pad, 40)
    End Sub

    ' Adds a caption and a control. Returns the Y position for the next row.
    Protected Function AddField(caption As String, ctl As Control, x As Integer, y As Integer, w As Integer, h As Integer) As Integer
        Dim lbl As New Label()
        lbl.Text = caption
        lbl.AutoSize = True
        lbl.Font = Fnt(False, 8.5F, True)
        lbl.ForeColor = ColMuted
        lbl.BackColor = ColSurface
        lbl.Location = New Point(x, y)
        ctl.SetBounds(x, y + 22, w, h)
        Controls.Add(lbl)
        Controls.Add(ctl)
        Return y + 22 + h + 12
    End Function

    ' Adds the error line and the Cancel / Save buttons, then sizes the form.
    Protected Sub FinishLayout(y As Integer, saveText As String)
        _errorLabel.AutoSize = False
        _errorLabel.SetBounds(Pad, y, ClientSize.Width - 2 * Pad, 22)
        _errorLabel.Font = Fnt(False, 9.0F)
        _errorLabel.ForeColor = ColRed
        _errorLabel.BackColor = ColSurface
        Controls.Add(_errorLabel)

        Dim buttonY As Integer = y + 28
        Dim save As RoundButton = UI.MakeButton(saveText, BtnKind.Primary, 190, 42)
        save.Location = New Point(ClientSize.Width - Pad - save.Width, buttonY)
        AddHandler save.Click, AddressOf OnSaveClicked
        Controls.Add(save)

        Dim cancel As RoundButton = UI.MakeButton("Cancel", BtnKind.Secondary, 110, 42)
        cancel.Location = New Point(save.Left - 10 - cancel.Width, buttonY)
        AddHandler cancel.Click, Sub() Close()
        Controls.Add(cancel)
        CancelButton = cancel

        ClientSize = New Size(ClientSize.Width, buttonY + 42 + Pad)
    End Sub

    Private Sub OnSaveClicked(sender As Object, e As EventArgs)
        _errorLabel.Text = ""
        If TryAccept() Then
            DialogResult = DialogResult.OK
        End If
    End Sub

    Protected Sub Fail(message As String)
        _errorLabel.Text = message
    End Sub

    ' Return True if the input is valid and has been stored in the dialog's result properties.
    Protected MustOverride Function TryAccept() As Boolean

    Protected Shared Function TryAmount(text As String, allowZero As Boolean, ByRef value As Decimal) As Boolean
        Dim t As String = If(text, "").Replace(",", "").Replace(Peso, "").Trim()
        If Not Regex.IsMatch(t, "^[0-9]+(\.[0-9]{1,2})?$") Then Return False
        Dim parsed As Decimal
        If Not Decimal.TryParse(t, NumberStyles.Number, CultureInfo.InvariantCulture, parsed) Then Return False
        If parsed > 999999999D Then Return False
        If parsed < 0D OrElse (parsed = 0D AndAlso Not allowZero) Then Return False
        value = parsed
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class TransactionDialog
    Inherits DialogBase

    Private ReadOnly _data As AppData
    Private ReadOnly _existing As Txn
    Private _kind As String

    Private ReadOnly _btnExpense As RoundButton
    Private ReadOnly _btnIncome As RoundButton
    Private ReadOnly _txtTitle As RoundedTextBox
    Private ReadOnly _txtAmount As RoundedTextBox
    Private ReadOnly _dtpDate As DateField
    Private ReadOnly _pickCategory As PickerField
    Private ReadOnly _pickPayment As PickerField
    Private ReadOnly _pickWallet As PickerField
    Private ReadOnly _txtNotes As RoundedTextBox
    Private ReadOnly _chkRepeat As CheckBox

    Public Result As Txn
    Public RepeatMonthly As Boolean = False

    Public Sub New(data As AppData, existing As Txn, kind As String)
        MyBase.New("BUDGETMATE WORKSPACE", If(existing Is Nothing, "Add transaction", "Edit transaction"), 580)
        _data = data
        _existing = existing
        _kind = If(existing IsNot Nothing, existing.Type, kind)

        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        _btnExpense = UI.MakeButton(Glyphs.TrendDown & " Expense", BtnKind.Secondary, half, 40)
        _btnExpense.Location = New Point(Pad, y)
        AddHandler _btnExpense.Click, Sub() SetKind("expense", True)
        Controls.Add(_btnExpense)
        _btnIncome = UI.MakeButton(Glyphs.TrendUp & " Income", BtnKind.Secondary, half, 40)
        _btnIncome.Location = New Point(Pad + half + 16, y)
        AddHandler _btnIncome.Click, Sub() SetKind("income", True)
        Controls.Add(_btnIncome)
        y += 40 + 14

        _txtTitle = UI.MakeInput("e.g. Groceries or monthly salary", 90)
        y = AddField("Title / name *", _txtTitle, Pad, y, innerW, 40)

        _txtAmount = UI.MakeInput("0.00", 14)
        _dtpDate = UI.MakeDate(Date.Today, False)
        AddField("Amount (" & Peso & ") *", _txtAmount, Pad, y, half, 40)
        y = AddField("Date *", _dtpDate, Pad + half + 16, y, half, 40)

        _pickCategory = UI.MakePicker(Catalog.ExpenseNames(), "Food")
        _pickPayment = UI.MakePicker(Catalog.PaymentMethods, "GCash")
        AddField("Category *", _pickCategory, Pad, y, half, 40)
        y = AddField("Payment method *", _pickPayment, Pad + half + 16, y, half, 40)

        _pickWallet = UI.MakePicker(data.Wallets.Select(Function(w) w.Name).ToArray(), data.Wallets(0).Name)
        y = AddField("Wallet / account *", _pickWallet, Pad, y, innerW, 40)

        _txtNotes = UI.MakeInput("Optional note", 200)
        y = AddField("Notes (optional)", _txtNotes, Pad, y, innerW, 40)

        _chkRepeat = New CheckBox()
        _chkRepeat.Text = "Repeat this transaction every month"
        _chkRepeat.AutoSize = True
        _chkRepeat.Font = Fnt(False, 9.5F)
        _chkRepeat.ForeColor = ColInk
        _chkRepeat.BackColor = ColSurface
        _chkRepeat.Location = New Point(Pad, y)
        If existing Is Nothing Then
            Controls.Add(_chkRepeat)
            y += 32
        End If

        FinishLayout(y, If(existing Is Nothing, "Add transaction", "Save changes"))

        If existing IsNot Nothing Then
            _txtTitle.Value = existing.Title
            _txtAmount.Value = existing.Amount.ToString("0.00", CultureInfo.InvariantCulture)
            _dtpDate.Value = existing.AsDate
            SetKind(existing.Type, False)
            _pickCategory.Value = existing.Category
            _pickPayment.Value = existing.Payment
            Dim w As Wallet = data.Wallets.FirstOrDefault(Function(x) x.Id = existing.WalletId)
            If w IsNot Nothing Then _pickWallet.Value = w.Name
            _txtNotes.Value = existing.Notes
        Else
            SetKind(_kind, True)
        End If
    End Sub

    Private Sub SetKind(kind As String, resetCategory As Boolean)
        _kind = kind
        UI.StyleButton(_btnExpense, If(kind = "expense", BtnKind.Primary, BtnKind.Secondary))
        UI.StyleButton(_btnIncome, If(kind = "income", BtnKind.Primary, BtnKind.Secondary))
        _btnExpense.Invalidate()
        _btnIncome.Invalidate()
        Dim items As String() = If(kind = "income", Catalog.IncomeCategories, Catalog.ExpenseNames())
        _pickCategory.SetItems(items, If(resetCategory, "", _pickCategory.Value))
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        If _txtTitle.Value.Trim().Length = 0 Then
            _txtTitle.HasError = True
            Fail("Please enter a title.")
            Return False
        End If
        _txtTitle.HasError = False

        Dim amount As Decimal
        If Not TryAmount(_txtAmount.Value, False, amount) Then
            _txtAmount.HasError = True
            Fail("Enter an amount above " & Peso & "0 with up to 2 decimals.")
            Return False
        End If
        _txtAmount.HasError = False

        If Not _dtpDate.Value.HasValue Then
            Fail("Pick a date.")
            Return False
        End If

        Dim wallet As Wallet = _data.Wallets.FirstOrDefault(Function(x) x.Name = _pickWallet.Value)
        If wallet Is Nothing Then
            Fail("Pick a wallet or account.")
            Return False
        End If

        Dim t As New Txn()
        If _existing IsNot Nothing Then t.Id = _existing.Id
        t.Type = _kind
        t.Title = _txtTitle.Value.Trim()
        t.Amount = amount
        t.Date = DateText(_dtpDate.Value.Value)
        t.Category = _pickCategory.Value
        t.Payment = _pickPayment.Value
        t.WalletId = wallet.Id
        t.Notes = _txtNotes.Value.Trim()
        If _existing IsNot Nothing Then t.RecurringRuleId = _existing.RecurringRuleId
        Result = t
        RepeatMonthly = (_existing Is Nothing AndAlso _chkRepeat.Checked)
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class GoalDialog
    Inherits DialogBase

    Private ReadOnly _txtName As RoundedTextBox
    Private ReadOnly _txtTarget As RoundedTextBox
    Private ReadOnly _txtSaved As RoundedTextBox
    Private ReadOnly _dtpDeadline As DateField
    Public Result As Goal

    Public Sub New()
        MyBase.New("MAKE IT HAPPEN", "Create a savings goal", 540)
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        _txtName = UI.MakeInput("e.g. Emergency fund", 80)
        y = AddField("Goal name *", _txtName, Pad, y, innerW, 40)

        _txtTarget = UI.MakeInput("0.00", 14)
        _txtSaved = UI.MakeInput("0.00", 14)
        AddField("Target amount (" & Peso & ") *", _txtTarget, Pad, y, half, 40)
        y = AddField("Already saved (" & Peso & ")", _txtSaved, Pad + half + 16, y, half, 40)
        _txtSaved.Value = "0"

        _dtpDeadline = UI.MakeDate(Nothing, True)
        y = AddField("Target date (optional)", _dtpDeadline, Pad, y, half, 40)

        FinishLayout(y, "Create goal")
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        If _txtName.Value.Trim().Length = 0 Then
            _txtName.HasError = True
            Fail("Enter a goal name.")
            Return False
        End If
        _txtName.HasError = False

        Dim target As Decimal
        If Not TryAmount(_txtTarget.Value, False, target) Then
            _txtTarget.HasError = True
            Fail("Enter a target above " & Peso & "0.")
            Return False
        End If
        _txtTarget.HasError = False

        Dim saved As Decimal = 0D
        If _txtSaved.Value.Trim().Length > 0 AndAlso Not TryAmount(_txtSaved.Value, True, saved) Then
            _txtSaved.HasError = True
            Fail("Enter a valid saved amount (0 or more).")
            Return False
        End If
        _txtSaved.HasError = False

        Dim g As New Goal()
        g.Name = _txtName.Value.Trim()
        g.Target = target
        g.Saved = saved
        g.Deadline = If(_dtpDeadline.Value.HasValue, DateText(_dtpDeadline.Value.Value), "")
        Result = g
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class WalletDialog
    Inherits DialogBase

    Private ReadOnly _data As AppData
    Private ReadOnly _txtName As RoundedTextBox
    Private ReadOnly _pickProvider As PickerField
    Private ReadOnly _pickKind As PickerField
    Private ReadOnly _txtBalance As RoundedTextBox
    Public Result As Wallet

    Private Shared ReadOnly KindLabels As String() = {"E-wallet", "Bank account", "Cash", "Other"}
    Private Shared ReadOnly KindValues As String() = {"ewallet", "bank", "cash", "other"}

    Public Sub New(data As AppData)
        MyBase.New("YOUR MONEY, ORGANIZED", "Add wallet or account", 540)
        _data = data
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        _txtName = UI.MakeInput("e.g. BPI savings", 60)
        y = AddField("Account nickname *", _txtName, Pad, y, innerW, 40)

        _pickProvider = UI.MakePicker(Catalog.Providers, "GCash")
        _pickKind = UI.MakePicker(KindLabels, "E-wallet")
        AddField("Provider", _pickProvider, Pad, y, half, 40)
        y = AddField("Account type", _pickKind, Pad + half + 16, y, half, 40)

        _txtBalance = UI.MakeInput("0.00", 14)
        y = AddField("Starting balance (" & Peso & ")", _txtBalance, Pad, y, half, 40)
        _txtBalance.Value = "0"

        FinishLayout(y, "Add account")
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        Dim name As String = _txtName.Value.Trim()
        If name.Length = 0 Then
            _txtName.HasError = True
            Fail("Enter an account nickname.")
            Return False
        End If
        If _data.Wallets.Any(Function(existing) String.Equals(existing.Name, name, StringComparison.OrdinalIgnoreCase)) Then
            _txtName.HasError = True
            Fail("You already have an account with that name.")
            Return False
        End If
        _txtName.HasError = False

        Dim balance As Decimal = 0D
        If _txtBalance.Value.Trim().Length > 0 AndAlso Not TryAmount(_txtBalance.Value, True, balance) Then
            _txtBalance.HasError = True
            Fail("Enter a valid starting balance (0 or more).")
            Return False
        End If

        Dim palette As String() = {"#4d91ff", "#16bd8b", "#9365ff", "#ffb11b", "#15b8ce", "#ff5b75", "#f07845"}
        Dim w As New Wallet()
        w.Name = name
        w.Provider = _pickProvider.Value
        w.Kind = KindValues(Array.IndexOf(KindLabels, _pickKind.Value))
        w.OpeningBalance = balance
        w.Hex = palette(_data.Wallets.Count Mod palette.Length)
        Result = w
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class TransferDialog
    Inherits DialogBase

    Private ReadOnly _data As AppData
    Private ReadOnly _balanceOf As Func(Of Wallet, Decimal)
    Private ReadOnly _pickFrom As PickerField
    Private ReadOnly _pickTo As PickerField
    Private ReadOnly _txtAmount As RoundedTextBox
    Private ReadOnly _dtpDate As DateField
    Private ReadOnly _txtNote As RoundedTextBox
    Public Result As Transfer

    Public Sub New(data As AppData, balanceOf As Func(Of Wallet, Decimal))
        MyBase.New("MOVE MONEY, NOT SPENDING", "Transfer between accounts", 540)
        _data = data
        _balanceOf = balanceOf
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        Dim names As String() = data.Wallets.Select(Function(w) w.Name).ToArray()
        _pickFrom = UI.MakePicker(names, names(0))
        _pickTo = UI.MakePicker(names, If(names.Length > 1, names(1), names(0)))
        AddField("From account", _pickFrom, Pad, y, half, 40)
        y = AddField("To account", _pickTo, Pad + half + 16, y, half, 40)

        _txtAmount = UI.MakeInput("0.00", 14)
        _dtpDate = UI.MakeDate(Date.Today, False)
        AddField("Amount (" & Peso & ") *", _txtAmount, Pad, y, half, 40)
        y = AddField("Date", _dtpDate, Pad + half + 16, y, half, 40)

        _txtNote = UI.MakeInput("e.g. Cash top-up to GCash", 120)
        y = AddField("Note (optional)", _txtNote, Pad, y, innerW, 40)

        FinishLayout(y, "Record transfer")
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        Dim fromW As Wallet = _data.Wallets.FirstOrDefault(Function(w) w.Name = _pickFrom.Value)
        Dim toW As Wallet = _data.Wallets.FirstOrDefault(Function(w) w.Name = _pickTo.Value)
        If fromW Is Nothing OrElse toW Is Nothing OrElse fromW.Id = toW.Id Then
            Fail("Choose two different accounts.")
            Return False
        End If

        Dim amount As Decimal
        If Not TryAmount(_txtAmount.Value, False, amount) Then
            _txtAmount.HasError = True
            Fail("Enter an amount above " & Peso & "0 with up to 2 decimals.")
            Return False
        End If
        _txtAmount.HasError = False

        If amount > _balanceOf(fromW) Then
            _txtAmount.HasError = True
            Fail("The amount is more than the tracked balance of " & fromW.Name & ".")
            Return False
        End If

        Dim t As New Transfer()
        t.FromId = fromW.Id
        t.ToId = toW.Id
        t.Amount = amount
        t.Date = DateText(_dtpDate.Value.Value)
        t.Note = _txtNote.Value.Trim()
        Result = t
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class LimitsDialog
    Inherits DialogBase

    Private ReadOnly _inputs As New Dictionary(Of String, RoundedTextBox)()
    Public Result As Dictionary(Of String, Decimal)

    Public Sub New(data As AppData, monthLabel As String)
        MyBase.New(monthLabel.ToUpperInvariant(), "Manage category limits", 600)
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        Dim intro As New Label()
        intro.Text = "Set a monthly cap for each category. The bars update as you add transactions."
        intro.AutoSize = False
        intro.SetBounds(Pad, y - 8, innerW, 36)
        intro.Font = Fnt(False, 9.5F)
        intro.ForeColor = ColMuted
        intro.BackColor = ColSurface
        Controls.Add(intro)
        y += 36

        For i As Integer = 0 To Catalog.ExpenseCategories.Length - 1 Step 2
            For col As Integer = 0 To 1
                Dim idx As Integer = i + col
                If idx >= Catalog.ExpenseCategories.Length Then Exit For
                Dim name As String = Catalog.ExpenseCategories(idx).Name
                Dim box As RoundedTextBox = UI.MakeInput("0.00", 14)
                Dim current As Decimal = 0D
                data.Limits.TryGetValue(name, current)
                box.Value = current.ToString("0.##", CultureInfo.InvariantCulture)
                _inputs(name) = box
                Dim x As Integer = Pad + col * (half + 16)
                If col = 1 Then
                    AddField(name & " limit (" & Peso & ")", box, x, y, half, 40)
                Else
                    AddField(name & " limit (" & Peso & ")", box, x, y, half, 40)
                End If
            Next
            y += 22 + 40 + 12
        Next

        FinishLayout(y, "Save limits")
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        Dim limits As New Dictionary(Of String, Decimal)()
        For Each pair In _inputs
            Dim v As Decimal
            If Not TryAmount(pair.Value.Value, True, v) Then
                pair.Value.HasError = True
                Fail("Enter a valid limit for " & pair.Key & ".")
                Return False
            End If
            pair.Value.HasError = False
            limits(pair.Key) = v
        Next
        Result = limits
        Return True
    End Function

End Class

' ---------------------------------------------------------------------------
Public Class AmountDialog
    Inherits DialogBase

    Private ReadOnly _txtAmount As RoundedTextBox
    Public Amount As Decimal

    Public Sub New(eyebrow As String, heading As String, caption As String, saveText As String)
        MyBase.New(eyebrow, heading, 460)
        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim y As Integer = ContentTop
        _txtAmount = UI.MakeInput("0.00", 14)
        y = AddField(caption, _txtAmount, Pad, y, innerW, 40)
        FinishLayout(y, saveText)
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        Dim v As Decimal
        If Not TryAmount(_txtAmount.Value, False, v) Then
            _txtAmount.HasError = True
            Fail("Enter an amount above " & Peso & "0 with up to 2 decimals.")
            Return False
        End If
        Amount = v
        Return True
    End Function

End Class
