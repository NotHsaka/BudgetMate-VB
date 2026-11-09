Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

' Wallet/account UI and transfer workflows. The Budget class remains the source of truth.
Partial Class MainForm
    Private _walletGrid As FlowLayoutPanel
    Private _walletUiReady As Boolean = False

    Private Sub InitWallets()
        If _walletUiReady Then Return
        _walletUiReady = True
        For Each c As Control In New Control() {WalletCard1, WalletCard2, WalletCard3, WalletCard4}
            c.Visible = False
        Next
        _walletGrid = New FlowLayoutPanel With {
            .Name = "WalletGrid", .Location = New Point(23, 65), .Size = New Size(WalletsAndAccsPnl.ClientSize.Width - 46, 220),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right,
            .FlowDirection = FlowDirection.LeftToRight, .WrapContents = True, .AutoScroll = True,
            .BackColor = Theme.ColSurface, .Padding = New Padding(0), .Margin = New Padding(0)
        }
        WalletsAndAccsPnl.Controls.Add(_walletGrid)
        _walletGrid.BringToFront()
        AddHandler AddAccBtn.Click, AddressOf AddAccount_Click
        AddHandler TransferBtn.Click, AddressOf Transfer_Click
        RefreshWallets()
    End Sub

    Private Sub RefreshWallets()
        If _budget Is Nothing OrElse _walletGrid Is Nothing Then Return
        _walletGrid.SuspendLayout()
        For Each oldCard As Control In _walletGrid.Controls.Cast(Of Control)().ToList()
            oldCard.Dispose()
        Next
        _walletGrid.Controls.Clear()
        For Each w As Wallet In _budget.Data.Wallets
            Dim card As New CardPanel With {
                .Size = New Size(270, 94), .Radius = 12, .BackColor = Theme.ColSoft,
                .Margin = New Padding(0, 0, 12, 10), .Padding = New Padding(12)
            }
            Dim name As New Label With {.Text = w.Name & " · " & Catalog.KindName(w.Kind), .AutoSize = False,
                .Location = New Point(12, 12), .Size = New Size(238, 22), .ForeColor = Theme.ColMuted,
                .Font = Theme.Fnt(False, 8.5F, True), .BackColor = Color.Transparent}
            Dim amount As New Label With {.Text = Fmt.Money(_budget.WalletBalance(w)), .AutoSize = False,
                .Location = New Point(12, 37), .Size = New Size(238, 25), .ForeColor = Theme.ColInk,
                .Font = Theme.Fnt(True, 12.0F, True), .BackColor = Color.Transparent}
            Dim provider As New Label With {.Text = w.Provider, .AutoSize = False,
                .Location = New Point(12, 65), .Size = New Size(238, 18), .ForeColor = Theme.ColMuted,
                .Font = Theme.Fnt(False, 8.0F), .BackColor = Color.Transparent}
            card.Controls.AddRange(New Control() {name, amount, provider})
            Dim menu As New ContextMenuStrip()
            Dim remove As New ToolStripMenuItem("Remove account")
            AddHandler remove.Click, Sub() RemoveWallet(w.Id, w.Name)
            menu.Items.Add(remove)
            card.ContextMenuStrip = menu
            For Each child As Control In card.Controls
                child.ContextMenuStrip = menu
            Next
            _walletGrid.Controls.Add(card)
        Next
        _walletGrid.ResumeLayout()
    End Sub

    Private Sub AddAccount_Click(sender As Object, e As EventArgs)
        Using dlg As New WalletDialog(_budget.Data)
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Result IsNot Nothing Then
                _budget.AddWallet(dlg.Result)
            End If
        End Using
    End Sub

    Private Sub RemoveWallet(id As String, accountName As String)
        Dim reason As String = ""
        If Not _budget.CanDeleteWallet(id, reason) Then
            MessageBox.Show(Me, reason, "Cannot remove account", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If MessageBox.Show(Me, "Remove '" & accountName & "'? This cannot be undone.", "Remove account",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        _budget.DeleteWallet(id)
    End Sub

    Private Sub Transfer_Click(sender As Object, e As EventArgs)
        If _budget.Data.Wallets.Count < 2 Then
            MessageBox.Show(Me, "Add at least two accounts before recording a transfer.", "Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using dlg As New TransferDialog(_budget.Data, Function(w) _budget.WalletBalance(w))
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Result IsNot Nothing Then
                _budget.AddTransfer(dlg.Result)
            End If
        End Using
    End Sub
End Class
