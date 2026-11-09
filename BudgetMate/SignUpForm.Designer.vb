Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SignUpForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SignUpForm))
        cardSignUp = New CardPanel()
        LogoPBSI = New PictureBox()
        lblLogo = New Label()
        lblTitle = New Label()
        lblSubtitle = New Label()
        lblFullName = New Label()
        txtFullName = New TextBox()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        lblConfirm = New Label()
        txtConfirm = New TextBox()
        chkShowPassword = New CheckBox()
        lblError = New Label()
        btnCreate = New Button()
        linkSkip = New LinkLabel()
        cardSignUp.SuspendLayout()
        CType(LogoPBSI, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cardSignUp
        ' 
        cardSignUp.Anchor = AnchorStyles.None
        cardSignUp.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        cardSignUp.Controls.Add(LogoPBSI)
        cardSignUp.Controls.Add(lblLogo)
        cardSignUp.Controls.Add(lblTitle)
        cardSignUp.Controls.Add(lblSubtitle)
        cardSignUp.Controls.Add(lblFullName)
        cardSignUp.Controls.Add(txtFullName)
        cardSignUp.Controls.Add(lblEmail)
        cardSignUp.Controls.Add(txtEmail)
        cardSignUp.Controls.Add(lblPassword)
        cardSignUp.Controls.Add(txtPassword)
        cardSignUp.Controls.Add(lblConfirm)
        cardSignUp.Controls.Add(txtConfirm)
        cardSignUp.Controls.Add(chkShowPassword)
        cardSignUp.Controls.Add(lblError)
        cardSignUp.Controls.Add(btnCreate)
        cardSignUp.Controls.Add(linkSkip)
        cardSignUp.Location = New System.Drawing.Point(275, 20)
        cardSignUp.Margin = New Padding(4)
        cardSignUp.Name = "cardSignUp"
        cardSignUp.Size = New System.Drawing.Size(575, 835)
        cardSignUp.TabIndex = 0
        ' 
        ' LogoPBSI
        ' 
        LogoPBSI.BorderStyle = BorderStyle.FixedSingle
        LogoPBSI.Image = CType(resources.GetObject("LogoPBSI.Image"), Drawing.Image)
        LogoPBSI.Location = New System.Drawing.Point(24, 23)
        LogoPBSI.Name = "LogoPBSI"
        LogoPBSI.Size = New System.Drawing.Size(76, 76)
        LogoPBSI.SizeMode = PictureBoxSizeMode.StretchImage
        LogoPBSI.TabIndex = 18
        LogoPBSI.TabStop = False
        ' 
        ' lblLogo
        ' 
        lblLogo.BackColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        lblLogo.Font = New System.Drawing.Font("Segoe UI", 16F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        lblLogo.ForeColor = Drawing.Color.FromArgb(CByte(255), CByte(255), CByte(255))
        lblLogo.Image = My.Resources.Resources.Untitled_design__15_
        lblLogo.Location = New System.Drawing.Point(45, 40)
        lblLogo.Margin = New Padding(4, 0, 4, 0)
        lblLogo.Name = "lblLogo"
        lblLogo.Size = New System.Drawing.Size(55, 55)
        lblLogo.TabIndex = 10
        lblLogo.Text = "₱"
        lblLogo.TextAlign = Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New System.Drawing.Font("Verdana", 19.8000011F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        lblTitle.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        lblTitle.Location = New System.Drawing.Point(42, 114)
        lblTitle.Margin = New Padding(4, 0, 4, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New System.Drawing.Size(485, 48)
        lblTitle.TabIndex = 11
        lblTitle.Text = "Create your account"
        lblTitle.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblSubtitle
        ' 
        lblSubtitle.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lblSubtitle.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblSubtitle.Location = New System.Drawing.Point(48, 161)
        lblSubtitle.Margin = New Padding(4, 0, 4, 0)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New System.Drawing.Size(485, 30)
        lblSubtitle.TabIndex = 12
        lblSubtitle.Text = "Start tracking every peso with PesoMint."
        lblSubtitle.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblFullName
        ' 
        lblFullName.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblFullName.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblFullName.Location = New System.Drawing.Point(45, 220)
        lblFullName.Margin = New Padding(4, 0, 4, 0)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New System.Drawing.Size(485, 25)
        lblFullName.TabIndex = 13
        lblFullName.Text = "Full name"
        lblFullName.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtFullName
        ' 
        txtFullName.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtFullName.BorderStyle = BorderStyle.FixedSingle
        txtFullName.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtFullName.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtFullName.Location = New System.Drawing.Point(45, 248)
        txtFullName.Margin = New Padding(4)
        txtFullName.MaxLength = 100
        txtFullName.Name = "txtFullName"
        txtFullName.PlaceholderText = "  Juan Dela Cruz"
        txtFullName.Size = New System.Drawing.Size(484, 29)
        txtFullName.TabIndex = 0
        ' 
        ' lblEmail
        ' 
        lblEmail.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblEmail.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblEmail.Location = New System.Drawing.Point(45, 312)
        lblEmail.Margin = New Padding(4, 0, 4, 0)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New System.Drawing.Size(485, 25)
        lblEmail.TabIndex = 14
        lblEmail.Text = "Email"
        lblEmail.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtEmail
        ' 
        txtEmail.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtEmail.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtEmail.Location = New System.Drawing.Point(45, 340)
        txtEmail.Margin = New Padding(4)
        txtEmail.MaxLength = 190
        txtEmail.Name = "txtEmail"
        txtEmail.PlaceholderText = "  you@example.com"
        txtEmail.Size = New System.Drawing.Size(484, 29)
        txtEmail.TabIndex = 1
        ' 
        ' lblPassword
        ' 
        lblPassword.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblPassword.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblPassword.Location = New System.Drawing.Point(45, 405)
        lblPassword.Margin = New Padding(4, 0, 4, 0)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New System.Drawing.Size(485, 25)
        lblPassword.TabIndex = 15
        lblPassword.Text = "Password"
        lblPassword.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtPassword.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtPassword.Location = New System.Drawing.Point(45, 432)
        txtPassword.Margin = New Padding(4)
        txtPassword.MaxLength = 128
        txtPassword.Name = "txtPassword"
        txtPassword.PlaceholderText = "  At least 8 characters"
        txtPassword.Size = New System.Drawing.Size(484, 29)
        txtPassword.TabIndex = 2
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' lblConfirm
        ' 
        lblConfirm.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblConfirm.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblConfirm.Location = New System.Drawing.Point(45, 498)
        lblConfirm.Margin = New Padding(4, 0, 4, 0)
        lblConfirm.Name = "lblConfirm"
        lblConfirm.Size = New System.Drawing.Size(485, 25)
        lblConfirm.TabIndex = 16
        lblConfirm.Text = "Confirm password"
        lblConfirm.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtConfirm
        ' 
        txtConfirm.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtConfirm.BorderStyle = BorderStyle.FixedSingle
        txtConfirm.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtConfirm.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtConfirm.Location = New System.Drawing.Point(45, 525)
        txtConfirm.Margin = New Padding(4)
        txtConfirm.MaxLength = 128
        txtConfirm.Name = "txtConfirm"
        txtConfirm.PlaceholderText = "  Type it again"
        txtConfirm.Size = New System.Drawing.Size(484, 29)
        txtConfirm.TabIndex = 3
        txtConfirm.UseSystemPasswordChar = True
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.Cursor = Cursors.Hand
        chkShowPassword.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        chkShowPassword.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        chkShowPassword.Location = New System.Drawing.Point(45, 582)
        chkShowPassword.Margin = New Padding(4)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New System.Drawing.Size(174, 24)
        chkShowPassword.TabIndex = 4
        chkShowPassword.Text = "Show passwords"
        chkShowPassword.UseVisualStyleBackColor = True
        ' 
        ' lblError
        ' 
        lblError.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lblError.ForeColor = Drawing.Color.FromArgb(CByte(255), CByte(91), CByte(117))
        lblError.Location = New System.Drawing.Point(45, 622)
        lblError.Margin = New Padding(4, 0, 4, 0)
        lblError.Name = "lblError"
        lblError.Size = New System.Drawing.Size(485, 50)
        lblError.TabIndex = 17
        lblError.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' btnCreate
        ' 
        btnCreate.BackColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        btnCreate.Cursor = Cursors.Hand
        btnCreate.FlatAppearance.BorderSize = 0
        btnCreate.FlatStyle = FlatStyle.Flat
        btnCreate.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        btnCreate.ForeColor = Drawing.Color.FromArgb(CByte(255), CByte(255), CByte(255))
        btnCreate.Location = New System.Drawing.Point(45, 685)
        btnCreate.Margin = New Padding(4)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New System.Drawing.Size(485, 58)
        btnCreate.TabIndex = 5
        btnCreate.Text = "Create account"
        btnCreate.UseVisualStyleBackColor = False
        ' 
        ' linkSkip
        ' 
        linkSkip.ActiveLinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        linkSkip.Cursor = Cursors.Hand
        linkSkip.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        linkSkip.LinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        linkSkip.Location = New System.Drawing.Point(45, 765)
        linkSkip.Margin = New Padding(4, 0, 4, 0)
        linkSkip.Name = "linkSkip"
        linkSkip.Size = New System.Drawing.Size(485, 28)
        linkSkip.TabIndex = 6
        linkSkip.TabStop = True
        linkSkip.Text = "Skip for now"
        linkSkip.TextAlign = Drawing.ContentAlignment.MiddleCenter
        linkSkip.VisitedLinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        ' 
        ' SignUpForm
        ' 
        AcceptButton = btnCreate
        AutoScaleDimensions = New System.Drawing.SizeF(120F, 120F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Drawing.Color.FromArgb(CByte(16), CByte(24), CByte(39))
        ClientSize = New System.Drawing.Size(1125, 875)
        Controls.Add(cardSignUp)
        Margin = New Padding(4)
        MinimumSize = New System.Drawing.Size(670, 898)
        Name = "SignUpForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "BudgetMate - Create account"
        cardSignUp.ResumeLayout(False)
        cardSignUp.PerformLayout()
        CType(LogoPBSI, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardSignUp As CardPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblConfirm As Label
    Friend WithEvents txtConfirm As TextBox
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents lblError As Label
    Friend WithEvents btnCreate As Button
    Friend WithEvents linkSkip As LinkLabel
    Friend WithEvents lblLogo As Label
    Friend WithEvents LogoPBSI As PictureBox

End Class
