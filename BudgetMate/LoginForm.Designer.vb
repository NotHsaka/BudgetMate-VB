Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        cardLogin = New CardPanel()
        LogoPBSI = New PictureBox()
        lblTitle = New Label()
        lblSubtitle = New Label()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        chkShowPassword = New CheckBox()
        lblError = New Label()
        btnSignIn = New Button()
        linkCreate = New LinkLabel()
        linkSkip = New LinkLabel()
        cardLogin.SuspendLayout()
        CType(LogoPBSI, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cardLogin
        ' 
        cardLogin.Anchor = AnchorStyles.None
        cardLogin.BackColor = Drawing.Color.FromArgb(CByte(27), CByte(39), CByte(59))
        cardLogin.Controls.Add(LogoPBSI)
        cardLogin.Controls.Add(lblTitle)
        cardLogin.Controls.Add(lblSubtitle)
        cardLogin.Controls.Add(lblEmail)
        cardLogin.Controls.Add(txtEmail)
        cardLogin.Controls.Add(lblPassword)
        cardLogin.Controls.Add(txtPassword)
        cardLogin.Controls.Add(chkShowPassword)
        cardLogin.Controls.Add(lblError)
        cardLogin.Controls.Add(btnSignIn)
        cardLogin.Controls.Add(linkCreate)
        cardLogin.Controls.Add(linkSkip)
        cardLogin.Location = New System.Drawing.Point(275, 110)
        cardLogin.Margin = New Padding(4)
        cardLogin.Name = "cardLogin"
        cardLogin.Size = New System.Drawing.Size(575, 655)
        cardLogin.TabIndex = 0
        ' 
        ' LogoPBSI
        ' 
        LogoPBSI.Image = CType(resources.GetObject("LogoPBSI.Image"), Drawing.Image)
        LogoPBSI.Location = New System.Drawing.Point(45, 30)
        LogoPBSI.Name = "LogoPBSI"
        LogoPBSI.Size = New System.Drawing.Size(76, 76)
        LogoPBSI.SizeMode = PictureBoxSizeMode.StretchImage
        LogoPBSI.TabIndex = 10
        LogoPBSI.TabStop = False
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New System.Drawing.Font("Verdana", 19.8F, Drawing.FontStyle.Bold)
        lblTitle.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        lblTitle.Location = New System.Drawing.Point(42, 122)
        lblTitle.Margin = New Padding(4, 0, 4, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New System.Drawing.Size(485, 48)
        lblTitle.TabIndex = 11
        lblTitle.Text = "Welcome back"
        lblTitle.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblSubtitle
        ' 
        lblSubtitle.Font = New System.Drawing.Font("Verdana", 10.2F)
        lblSubtitle.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblSubtitle.Location = New System.Drawing.Point(48, 170)
        lblSubtitle.Margin = New Padding(4, 0, 4, 0)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New System.Drawing.Size(485, 30)
        lblSubtitle.TabIndex = 12
        lblSubtitle.Text = "Sign in to keep tracking every peso."
        lblSubtitle.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblEmail
        ' 
        lblEmail.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblEmail.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblEmail.Location = New System.Drawing.Point(45, 225)
        lblEmail.Margin = New Padding(4, 0, 4, 0)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New System.Drawing.Size(485, 25)
        lblEmail.TabIndex = 13
        lblEmail.Text = "Email"
        lblEmail.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtEmail
        ' 
        txtEmail.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtEmail.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtEmail.Location = New System.Drawing.Point(45, 253)
        txtEmail.Margin = New Padding(4)
        txtEmail.MaxLength = 190
        txtEmail.Name = "txtEmail"
        txtEmail.PlaceholderText = "  you@example.com"
        txtEmail.Size = New System.Drawing.Size(484, 29)
        txtEmail.TabIndex = 0
        ' 
        ' lblPassword
        ' 
        lblPassword.Font = New System.Drawing.Font("Verdana", 10.2F, Drawing.FontStyle.Bold)
        lblPassword.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        lblPassword.Location = New System.Drawing.Point(45, 318)
        lblPassword.Margin = New Padding(4, 0, 4, 0)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New System.Drawing.Size(485, 25)
        lblPassword.TabIndex = 14
        lblPassword.Text = "Password"
        lblPassword.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Drawing.Color.FromArgb(CByte(13), CByte(20), CByte(37))
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.Font = New System.Drawing.Font("Verdana", 10.8F)
        txtPassword.ForeColor = Drawing.Color.FromArgb(CByte(244), CByte(247), CByte(253))
        txtPassword.Location = New System.Drawing.Point(45, 346)
        txtPassword.Margin = New Padding(4)
        txtPassword.MaxLength = 128
        txtPassword.Name = "txtPassword"
        txtPassword.PlaceholderText = "  Your password"
        txtPassword.Size = New System.Drawing.Size(484, 29)
        txtPassword.TabIndex = 1
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.Cursor = Cursors.Hand
        chkShowPassword.Font = New System.Drawing.Font("Verdana", 10.2F)
        chkShowPassword.ForeColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        chkShowPassword.Location = New System.Drawing.Point(45, 395)
        chkShowPassword.Margin = New Padding(4)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New System.Drawing.Size(165, 24)
        chkShowPassword.TabIndex = 2
        chkShowPassword.Text = "Show password"
        chkShowPassword.UseVisualStyleBackColor = True
        ' 
        ' lblError
        ' 
        lblError.Font = New System.Drawing.Font("Segoe UI", 9.5F)
        lblError.ForeColor = Drawing.Color.FromArgb(CByte(255), CByte(91), CByte(117))
        lblError.Location = New System.Drawing.Point(45, 432)
        lblError.Margin = New Padding(4, 0, 4, 0)
        lblError.Name = "lblError"
        lblError.Size = New System.Drawing.Size(485, 50)
        lblError.TabIndex = 15
        lblError.TextAlign = Drawing.ContentAlignment.MiddleLeft
        ' 
        ' btnSignIn
        ' 
        btnSignIn.BackColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        btnSignIn.Cursor = Cursors.Hand
        btnSignIn.FlatAppearance.BorderSize = 0
        btnSignIn.FlatStyle = FlatStyle.Flat
        btnSignIn.Font = New System.Drawing.Font("Verdana", 10.8F, Drawing.FontStyle.Bold)
        btnSignIn.ForeColor = Drawing.Color.White
        btnSignIn.Location = New System.Drawing.Point(45, 495)
        btnSignIn.Margin = New Padding(4)
        btnSignIn.Name = "btnSignIn"
        btnSignIn.Size = New System.Drawing.Size(485, 58)
        btnSignIn.TabIndex = 3
        btnSignIn.Text = "Sign in"
        btnSignIn.UseVisualStyleBackColor = False
        ' 
        ' linkCreate
        ' 
        linkCreate.ActiveLinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        linkCreate.Cursor = Cursors.Hand
        linkCreate.Font = New System.Drawing.Font("Verdana", 10.2F)
        linkCreate.LinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        linkCreate.Location = New System.Drawing.Point(45, 567)
        linkCreate.Margin = New Padding(4, 0, 4, 0)
        linkCreate.Name = "linkCreate"
        linkCreate.Size = New System.Drawing.Size(485, 28)
        linkCreate.TabIndex = 4
        linkCreate.TabStop = True
        linkCreate.Text = "New here? Create an account"
        linkCreate.TextAlign = Drawing.ContentAlignment.MiddleCenter
        linkCreate.VisitedLinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        ' 
        ' linkSkip
        ' 
        linkSkip.ActiveLinkColor = Drawing.Color.FromArgb(CByte(22), CByte(189), CByte(139))
        linkSkip.Cursor = Cursors.Hand
        linkSkip.Font = New System.Drawing.Font("Verdana", 10.2F)
        linkSkip.LinkColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        linkSkip.Location = New System.Drawing.Point(45, 605)
        linkSkip.Margin = New Padding(4, 0, 4, 0)
        linkSkip.Name = "linkSkip"
        linkSkip.Size = New System.Drawing.Size(485, 28)
        linkSkip.TabIndex = 5
        linkSkip.TabStop = True
        linkSkip.Text = "Continue without an account"
        linkSkip.TextAlign = Drawing.ContentAlignment.MiddleCenter
        linkSkip.VisitedLinkColor = Drawing.Color.FromArgb(CByte(154), CByte(171), CByte(194))
        ' 
        ' LoginForm
        ' 
        AcceptButton = btnSignIn
        AutoScaleDimensions = New System.Drawing.SizeF(120F, 120F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Drawing.Color.FromArgb(CByte(16), CByte(24), CByte(39))
        ClientSize = New System.Drawing.Size(1125, 875)
        Controls.Add(cardLogin)
        Margin = New Padding(4)
        MinimumSize = New System.Drawing.Size(670, 898)
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "PesoMint - Sign in"
        cardLogin.ResumeLayout(False)
        cardLogin.PerformLayout()
        CType(LogoPBSI, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardLogin As CardPanel
    Friend WithEvents LogoPBSI As PictureBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents lblError As Label
    Friend WithEvents btnSignIn As Button
    Friend WithEvents linkCreate As LinkLabel
    Friend WithEvents linkSkip As LinkLabel

End Class
