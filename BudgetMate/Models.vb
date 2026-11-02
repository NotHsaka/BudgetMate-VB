Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Text.Json.Serialization

' Property names match the web version's JSON, so backups can be moved between the two.

Public Module Ids

    Public Function NewId() As String
        Return Guid.NewGuid().ToString("N")
    End Function

    Public Function DateText(d As Date) As String
        Return d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
    End Function

    Public Function ParseDate(text As String, fallback As Date) As Date
        Dim d As Date
        If Date.TryParseExact(If(text, ""), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then Return d
        Return fallback
    End Function

    Public Function IsValidDate(text As String) As Boolean
        Dim d As Date
        Return Date.TryParseExact(If(text, ""), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, d)
    End Function

End Module

Public Class Txn
    Public Property Id As String = NewId()
    Public Property Type As String = "expense"
    Public Property Title As String = ""
    Public Property Amount As Decimal = 0D
    Public Property [Date] As String = DateText(Date.Today)
    Public Property Category As String = "Food"
    Public Property Payment As String = "GCash"
    Public Property WalletId As String = "cash"
    Public Property Notes As String = ""
    Public Property RecurringRuleId As String = Nothing

    <JsonIgnore>
    Public ReadOnly Property AsDate As Date
        Get
            Return ParseDate([Date], Date.Today)
        End Get
    End Property

    <JsonIgnore>
    Public ReadOnly Property YearMonth As String
        Get
            If [Date] IsNot Nothing AndAlso [Date].Length >= 7 Then Return [Date].Substring(0, 7)
            Return ""
        End Get
    End Property

    <JsonIgnore>
    Public ReadOnly Property IsIncome As Boolean
        Get
            Return Type = "income"
        End Get
    End Property
End Class

Public Class Wallet
    Public Property Id As String = NewId()
    Public Property Name As String = ""
    Public Property Provider As String = "Cash"
    Public Property Kind As String = "cash"
    Public Property OpeningBalance As Decimal = 0D
    <JsonPropertyName("color")>
    Public Property Hex As String = "#16bd8b"

    <JsonIgnore>
    Public ReadOnly Property AsColor As Color
        Get
            Try
                Return ColorTranslator.FromHtml(Hex)
            Catch ex As Exception
                Return Color.FromArgb(22, 189, 139)
            End Try
        End Get
    End Property
End Class

Public Class Goal
    Public Property Id As String = NewId()
    Public Property Name As String = ""
    Public Property Target As Decimal = 0D
    Public Property Saved As Decimal = 0D
    Public Property Deadline As String = ""
End Class

Public Class Transfer
    Public Property Id As String = NewId()
    Public Property FromId As String = ""
    Public Property ToId As String = ""
    Public Property Amount As Decimal = 0D
    Public Property [Date] As String = DateText(Date.Today)
    Public Property Note As String = ""
End Class

Public Class RecurringRule
    Public Property Id As String = NewId()
    Public Property Title As String = ""
    Public Property Type As String = "expense"
    Public Property Amount As Decimal = 0D
    Public Property Category As String = "Food"
    Public Property Payment As String = "GCash"
    Public Property WalletId As String = "cash"
    Public Property Notes As String = ""
    Public Property Day As Integer = 1
    Public Property StartDate As String = DateText(Date.Today)
    Public Property Active As Boolean = True
End Class

Public Class AppData
    Public Property Transactions As List(Of Txn) = New List(Of Txn)()
    Public Property Limits As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)()
    Public Property Goals As List(Of Goal) = New List(Of Goal)()
    Public Property RecurringRules As List(Of RecurringRule) = New List(Of RecurringRule)()
    Public Property Wallets As List(Of Wallet) = New List(Of Wallet)()
    Public Property Transfers As List(Of Transfer) = New List(Of Transfer)()
    Public Property DarkTheme As Boolean = True

    Public Shared Function CreateDefault() As AppData
        Dim d As New AppData()
        For Each c In Catalog.ExpenseCategories
            d.Limits(c.Name) = c.DefaultLimit
        Next
        d.Wallets.Add(NewWallet("cash", "Everyday cash", "Cash", "cash", "#16bd8b"))
        d.Wallets.Add(NewWallet("gcash", "GCash wallet", "GCash", "ewallet", "#4d91ff"))
        d.Wallets.Add(NewWallet("maya", "Maya wallet", "Maya", "ewallet", "#15b8ce"))
        d.Wallets.Add(NewWallet("bank", "Main bank", "BDO", "bank", "#9365ff"))
        Return d
    End Function

    Private Shared Function NewWallet(id As String, name As String, provider As String, kind As String, hex As String) As Wallet
        Dim w As New Wallet()
        w.Id = id
        w.Name = name
        w.Provider = provider
        w.Kind = kind
        w.Hex = hex
        Return w
    End Function
End Class

Public Class CategoryInfo
    Public ReadOnly Name As String
    Public ReadOnly Tint As Color
    Public ReadOnly DefaultLimit As Decimal

    Public Sub New(categoryName As String, tint As Color, defaultLimit As Decimal)
        Me.Name = categoryName
        Me.Tint = tint
        Me.DefaultLimit = defaultLimit
    End Sub
End Class

Public Module Catalog

    Public ReadOnly ExpenseCategories As CategoryInfo() = {
        New CategoryInfo("Housing", Color.FromArgb(77, 145, 255), 12000D),
        New CategoryInfo("Food", Color.FromArgb(255, 91, 117), 5000D),
        New CategoryInfo("Transportation", Color.FromArgb(255, 177, 27), 2500D),
        New CategoryInfo("Utilities", Color.FromArgb(21, 184, 206), 3000D),
        New CategoryInfo("Entertainment", Color.FromArgb(147, 101, 255), 2000D),
        New CategoryInfo("Health", Color.FromArgb(32, 189, 139), 1500D),
        New CategoryInfo("Shopping", Color.FromArgb(240, 120, 69), 2000D),
        New CategoryInfo("Miscellaneous", Color.FromArgb(132, 146, 166), 1500D)
    }

    Public ReadOnly IncomeCategories As String() = {"Salary", "Freelance", "Investment", "Allowance", "Gift", "Other income"}
    Public ReadOnly PaymentMethods As String() = {"Cash", "GCash", "Maya", "Bank Transfer", "Card / Debit", "Credit Card", "QR Ph", "Other"}
    Public ReadOnly Providers As String() = {"Cash", "GCash", "Maya", "GoTyme Bank", "BDO", "BPI", "UnionBank", "Landbank", "Metrobank", "CIMB", "Coins.ph", "Other"}

    Public Function ExpenseNames() As String()
        Dim names(ExpenseCategories.Length - 1) As String
        For i As Integer = 0 To ExpenseCategories.Length - 1
            names(i) = ExpenseCategories(i).Name
        Next
        Return names
    End Function

    Public Function TintOf(categoryName As String) As Color
        For Each c In ExpenseCategories
            If c.Name = categoryName Then Return c.Tint
        Next
        Return Color.FromArgb(32, 189, 139)
    End Function

    Public Function KindName(kind As String) As String
        Select Case kind
            Case "bank"
                Return "Bank account"
            Case "ewallet"
                Return "E-wallet"
            Case "cash"
                Return "Cash"
            Case Else
                Return "Other"
        End Select
    End Function

End Module
