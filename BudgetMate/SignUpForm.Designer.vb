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
        cardSignUp = New CardPanel()
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
        SuspendLayout()
        ' 
        ' cardSignUp
        ' 
        cardSignUp.Anchor = AnchorStyles.None
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
        cardSignUp.Location = New System.Drawing.Point(220, 16)
        cardSignUp.Name = "cardSignUp"
        cardSignUp.Size = New System.Drawing.Size(460, 668)
        cardSignUp.TabIndex = 0
        ' 
        ' lblLogo
        ' 
        lblLogo.AutoSize = False
        lblLogo.BackColor = System.Drawing.Color.FromArgb(CType(CByte(22), Integer), CType(CByte(189), Integer), CType(CByte(139), Integer))
        lblLogo.Font = New System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLogo.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(255), Integer), CType(CByte(255), Integer), CType(CByte(255), Integer))
        lblLogo.Location = New System.Drawing.Point(36, 32)
        lblLogo.Name = "lblLogo"
        lblLogo.Size = New System.Drawing.Size(44, 44)
        lblLogo.TabIndex = 10
        lblLogo.Text = "₱"
        lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = False
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(244), Integer), CType(CByte(247), Integer), CType(CByte(253), Integer))
        lblTitle.Location = New System.Drawing.Point(36, 92)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New System.Drawing.Size(388, 38)
        lblTitle.TabIndex = 11
        lblTitle.Text = "Create your account"
        lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblSubtitle
        ' 
        lblSubtitle.AutoSize = False
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        lblSubtitle.Location = New System.Drawing.Point(36, 132)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New System.Drawing.Size(388, 24)
        lblSubtitle.TabIndex = 12
        lblSubtitle.Text = "Start tracking every peso with BudgetMate."
        lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' lblFullName
        ' 
        lblFullName.AutoSize = False
        lblFullName.Font = New System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblFullName.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        lblFullName.Location = New System.Drawing.Point(36, 176)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New System.Drawing.Size(388, 20)
        lblFullName.TabIndex = 13
        lblFullName.Text = "Full name"
        lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtFullName
        ' 
        txtFullName.BackColor = System.Drawing.Color.FromArgb(CType(CByte(13), Integer), CType(CByte(20), Integer), CType(CByte(37), Integer))
        txtFullName.BorderStyle = BorderStyle.FixedSingle
        txtFullName.Font = New System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtFullName.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(244), Integer), CType(CByte(247), Integer), CType(CByte(253), Integer))
        txtFullName.Location = New System.Drawing.Point(36, 198)
        txtFullName.MaxLength = 100
        txtFullName.Name = "txtFullName"
        txtFullName.PlaceholderText = "Juan Dela Cruz"
        txtFullName.Size = New System.Drawing.Size(388, 31)
        txtFullName.TabIndex = 0
        ' 
        ' lblEmail
        ' 
        lblEmail.AutoSize = False
        lblEmail.Font = New System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        lblEmail.Location = New System.Drawing.Point(36, 250)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New System.Drawing.Size(388, 20)
        lblEmail.TabIndex = 14
        lblEmail.Text = "Email"
        lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtEmail
        ' 
        txtEmail.BackColor = System.Drawing.Color.FromArgb(CType(CByte(13), Integer), CType(CByte(20), Integer), CType(CByte(37), Integer))
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Font = New System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(244), Integer), CType(CByte(247), Integer), CType(CByte(253), Integer))
        txtEmail.Location = New System.Drawing.Point(36, 272)
        txtEmail.MaxLength = 190
        txtEmail.Name = "txtEmail"
        txtEmail.PlaceholderText = "you@example.com"
        txtEmail.Size = New System.Drawing.Size(388, 31)
        txtEmail.TabIndex = 1
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = False
        lblPassword.Font = New System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        lblPassword.Location = New System.Drawing.Point(36, 324)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New System.Drawing.Size(388, 20)
        lblPassword.TabIndex = 15
        lblPassword.Text = "Password"
        lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = System.Drawing.Color.FromArgb(CType(CByte(13), Integer), CType(CByte(20), Integer), CType(CByte(37), Integer))
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.Font = New System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(244), Integer), CType(CByte(247), Integer), CType(CByte(253), Integer))
        txtPassword.Location = New System.Drawing.Point(36, 346)
        txtPassword.MaxLength = 128
        txtPassword.Name = "txtPassword"
        txtPassword.PlaceholderText = "At least 8 characters"
        txtPassword.Size = New System.Drawing.Size(388, 31)
        txtPassword.TabIndex = 2
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' lblConfirm
        ' 
        lblConfirm.AutoSize = False
        lblConfirm.Font = New System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblConfirm.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        lblConfirm.Location = New System.Drawing.Point(36, 398)
        lblConfirm.Name = "lblConfirm"
        lblConfirm.Size = New System.Drawing.Size(388, 20)
        lblConfirm.TabIndex = 16
        lblConfirm.Text = "Confirm password"
        lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' txtConfirm
        ' 
        txtConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CByte(13), Integer), CType(CByte(20), Integer), CType(CByte(37), Integer))
        txtConfirm.BorderStyle = BorderStyle.FixedSingle
        txtConfirm.Font = New System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtConfirm.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(244), Integer), CType(CByte(247), Integer), CType(CByte(253), Integer))
        txtConfirm.Location = New System.Drawing.Point(36, 420)
        txtConfirm.MaxLength = 128
        txtConfirm.Name = "txtConfirm"
        txtConfirm.PlaceholderText = "Type it again"
        txtConfirm.Size = New System.Drawing.Size(388, 31)
        txtConfirm.TabIndex = 3
        txtConfirm.UseSystemPasswordChar = True
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.Cursor = Cursors.Hand
        chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(154), Integer), CType(CByte(171), Integer), CType(CByte(194), Integer))
        chkShowPassword.Location = New System.Drawing.Point(36, 466)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New System.Drawing.Size(140, 24)
        chkShowPassword.TabIndex = 4
        chkShowPassword.Text = "Show passwords"
        chkShowPassword.UseVisualStyleBackColor = True
        ' 
        ' lblError
        ' 
        lblError.AutoSize = False
        lblError.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblError.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(255), Integer), CType(CByte(91), Integer), CType(CByte(117), Integer))
        lblError.Location = New System.Drawing.Point(36, 498)
        lblError.Name = "lblError"
        lblError.Size = New System.Drawing.Size(388, 40)
        lblError.TabIndex = 17
        lblError.Text = ""
        lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        ' 
        ' btnCreate
        ' 
        btnCreate.BackColor = System.Drawing.Color.FromArgb(CType(CByte(22), Integer), CType(CByte(189), Integer), CType(CByte(139), Integer))
        btnCreate.Cursor = Cursors.Hand
        btnCreate.FlatAppearance.BorderSize = 0
        btnCreate.FlatStyle = FlatStyle.Flat
        btnCreate.Font = New System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnCreate.ForeColor = System.Drawing.Color.FromArgb(CType(CByte(255), Integer), CType(CByte(255), Integer), CType(CByte(255), Integer))
        btnCreate.Location = New System.Drawing.Point(36, 548)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New System.Drawing.Size(388, 46)
        btnCreate.TabIndex = 5
        btnCreate.Text = "Create account"
        btnCreate.UseVisualStyleBackColor = False
        ' 
        ' linkSkip
        ' 
        linkSkip.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CByte(22), Integer), CType(CByte(189), Integer), CType(CByte(139), Integer))
        linkSkip.AutoSize = False
        linkSkip.Cursor = Cursors.Hand
        linkSkip.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        linkSkip.LinkColor = System.Drawing.Color.FromArgb(CType(CByte(22), Integer), CType(CByte(189), Integer), CType(CByte(139), Integer))
        linkSkip.Location = New System.Drawing.Point(36, 612)
        linkSkip.Name = "linkSkip"
        linkSkip.Size = New System.Drawing.Size(388, 22)
        linkSkip.TabIndex = 6
        linkSkip.TabStop = True
        linkSkip.Text = "Skip for now"
        linkSkip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        linkSkip.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CByte(22), Integer), CType(CByte(189), Integer), CType(CByte(139), Integer))
        ' 
        ' SignUpForm
        ' 
        AcceptButton = btnCreate
        AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = System.Drawing.Color.FromArgb(CType(CByte(16), Integer), CType(CByte(24), Integer), CType(CByte(39), Integer))
        ClientSize = New System.Drawing.Size(900, 700)
        Controls.Add(cardSignUp)
        MinimumSize = New System.Drawing.Size(540, 728)
        Name = "SignUpForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "BudgetMate - Create account"
        cardSignUp.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardSignUp As CardPanel
    Friend WithEvents lblLogo As Label
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

End Class
