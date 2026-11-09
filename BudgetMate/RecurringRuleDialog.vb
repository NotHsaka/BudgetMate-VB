Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

<DesignerCategory("")>
Public Class RecurringRuleDialog
    Inherits DialogBase

    Private ReadOnly _data As AppData
    Private ReadOnly _existing As RecurringRule
    Private ReadOnly _txtTitle As RoundedTextBox
    Private ReadOnly _txtAmount As RoundedTextBox
    Private ReadOnly _pickType As PickerField
    Private ReadOnly _pickCategory As PickerField
    Private ReadOnly _pickPayment As PickerField
    Private ReadOnly _pickWallet As PickerField
    Private ReadOnly _pickDay As PickerField
    Private ReadOnly _dtpStart As DateField
    Private ReadOnly _txtNotes As RoundedTextBox

    Public Result As RecurringRule

    Public Sub New(data As AppData, existing As RecurringRule)
        MyBase.New("MONTHLY SCHEDULE", If(existing Is Nothing, "Add recurring transaction", "Edit recurring transaction"), 580)
        _data = data
        _existing = existing

        Dim innerW As Integer = ClientSize.Width - 2 * Pad
        Dim half As Integer = (innerW - 16) \ 2
        Dim y As Integer = ContentTop

        _txtTitle = UI.MakeInput("e.g. Monthly salary or rent", 90)
        y = AddField("Title / name *", _txtTitle, Pad, y, innerW, 40)

        _pickType = UI.MakePicker(New String() {"Expense", "Income"}, "Expense")
        _txtAmount = UI.MakeInput("0.00", 14)
        AddField("Type *", _pickType, Pad, y, half, 40)
        y = AddField("Amount (" & Peso & ") *", _txtAmount, Pad + half + 16, y, half, 40)

        _pickCategory = UI.MakePicker(Catalog.ExpenseNames(), "Food")
        _pickPayment = UI.MakePicker(Catalog.PaymentMethods, "GCash")
        AddField("Category *", _pickCategory, Pad, y, half, 40)
        y = AddField("Payment method *", _pickPayment, Pad + half + 16, y, half, 40)

        _pickWallet = UI.MakePicker(data.Wallets.Select(Function(w) w.Name).ToArray(), data.Wallets(0).Name)
        AddField("Wallet / account *", _pickWallet, Pad, y, half, 40)
        _pickDay = UI.MakePicker(Enumerable.Range(1, 31).Select(Function(n) n.ToString(CultureInfo.InvariantCulture)).ToArray(), Date.Today.Day.ToString(CultureInfo.InvariantCulture))
        y = AddField("Day of month *", _pickDay, Pad + half + 16, y, half, 40)

        _dtpStart = UI.MakeDate(Date.Today, False)
        y = AddField("Start date *", _dtpStart, Pad, y, half, 40)
        _txtNotes = UI.MakeInput("Optional note", 200)
        y = AddField("Notes (optional)", _txtNotes, Pad + half + 16, y - 74, half, 40)

        AddHandler _pickType.ValueChanged, AddressOf TypeChanged
        FinishLayout(y, If(existing Is Nothing, "Add recurring", "Save changes"))

        If existing IsNot Nothing Then
            _txtTitle.Value = existing.Title
            _txtAmount.Value = existing.Amount.ToString("0.00", CultureInfo.InvariantCulture)
            _pickType.Value = If(existing.Type = "income", "Income", "Expense")
            _pickCategory.SetItems(If(existing.Type = "income", Catalog.IncomeCategories, Catalog.ExpenseNames()), existing.Category)
            _pickPayment.Value = existing.Payment
            Dim wallet As Wallet = data.Wallets.FirstOrDefault(Function(w) w.Id = existing.WalletId)
            If wallet IsNot Nothing Then _pickWallet.Value = wallet.Name
            _pickDay.Value = existing.Day.ToString(CultureInfo.InvariantCulture)
            _dtpStart.Value = ParseDate(existing.StartDate, Date.Today)
            _txtNotes.Value = existing.Notes
        End If
    End Sub

    Private Sub TypeChanged()
        Dim income As Boolean = _pickType.Value = "Income"
        Dim current As String = _pickCategory.Value
        _pickCategory.SetItems(If(income, Catalog.IncomeCategories, Catalog.ExpenseNames()), current)
    End Sub

    Protected Overrides Function TryAccept() As Boolean
        If String.IsNullOrWhiteSpace(_txtTitle.Value) Then
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

        If Not _dtpStart.Value.HasValue Then
            Fail("Choose a start date.")
            Return False
        End If

        Dim wallet As Wallet = _data.Wallets.FirstOrDefault(Function(w) w.Name = _pickWallet.Value)
        If wallet Is Nothing Then
            Fail("Choose a wallet or account.")
            Return False
        End If

        Dim dayValue As Integer
        If Not Integer.TryParse(_pickDay.Value, dayValue) Then dayValue = 1

        Dim rule As New RecurringRule()
        If _existing IsNot Nothing Then
            rule.Id = _existing.Id
            rule.Active = _existing.Active
        End If
        rule.Title = _txtTitle.Value.Trim()
        rule.Type = If(_pickType.Value = "Income", "income", "expense")
        rule.Amount = amount
        rule.Category = _pickCategory.Value
        rule.Payment = _pickPayment.Value
        rule.WalletId = wallet.Id
        rule.Day = Math.Min(Math.Max(dayValue, 1), 31)
        rule.StartDate = DateText(_dtpStart.Value.Value)
        rule.Notes = _txtNotes.Value.Trim()
        Result = rule
        Return True
    End Function
End Class
