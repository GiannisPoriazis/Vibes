namespace Vibes.Views
{
    partial class AccountControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            viewLayout = new TableLayoutPanel();
            avatarIcon = new PictureBox();
            userIdLabel = new Label();
            userIdInput = new TextBox();
            usernameLabel = new Label();
            usernameInput = new TextBox();
            emailLabel = new Label();
            emailInput = new TextBox();
            buttonsLayout = new TableLayoutPanel();
            btnBack = new Button();
            btnLogout = new Button();
            viewLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)avatarIcon).BeginInit();
            buttonsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // viewLayout
            // 
            viewLayout.ColumnCount = 1;
            viewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            viewLayout.Controls.Add(avatarIcon, 0, 0);
            viewLayout.Controls.Add(userIdLabel, 0, 1);
            viewLayout.Controls.Add(userIdInput, 0, 2);
            viewLayout.Controls.Add(usernameLabel, 0, 3);
            viewLayout.Controls.Add(usernameInput, 0, 4);
            viewLayout.Controls.Add(emailLabel, 0, 5);
            viewLayout.Controls.Add(emailInput, 0, 6);
            viewLayout.Controls.Add(buttonsLayout, 0, 8);
            viewLayout.Dock = DockStyle.Fill;
            viewLayout.Location = new Point(0, 0);
            viewLayout.Name = "viewLayout";
            viewLayout.Padding = new Padding(32);
            viewLayout.RowCount = 9;
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            viewLayout.Size = new Size(867, 540);
            viewLayout.TabIndex = 0;
            // 
            // avatarIcon
            // 
            avatarIcon.Anchor = AnchorStyles.Left;
            avatarIcon.Location = new Point(35, 38);
            avatarIcon.Name = "avatarIcon";
            avatarIcon.Size = new Size(88, 88);
            avatarIcon.SizeMode = PictureBoxSizeMode.Zoom;
            avatarIcon.TabIndex = 0;
            avatarIcon.TabStop = false;
            // 
            // userIdLabel
            // 
            userIdLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            userIdLabel.AutoSize = true;
            userIdLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            userIdLabel.ForeColor = Color.FromArgb(160, 160, 160);
            userIdLabel.Location = new Point(35, 147);
            userIdLabel.Name = "userIdLabel";
            userIdLabel.Size = new Size(49, 15);
            userIdLabel.TabIndex = 1;
            userIdLabel.Text = "User ID";
            // 
            // userIdInput
            // 
            userIdInput.BackColor = Color.FromArgb(30, 30, 30);
            userIdInput.BorderStyle = BorderStyle.None;
            userIdInput.Font = new Font("Segoe UI", 10F);
            userIdInput.ForeColor = Color.FromArgb(180, 180, 180);
            userIdInput.Location = new Point(35, 165);
            userIdInput.Multiline = true;
            userIdInput.Name = "userIdInput";
            userIdInput.ReadOnly = true;
            userIdInput.Size = new Size(500, 34);
            userIdInput.TabIndex = 2;
            userIdInput.Text = "Subject ID";
            // 
            // usernameLabel
            // 
            usernameLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            usernameLabel.ForeColor = Color.FromArgb(160, 160, 160);
            usernameLabel.Location = new Point(35, 217);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(64, 15);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Username";
            // 
            // usernameInput
            // 
            usernameInput.BackColor = Color.FromArgb(30, 30, 30);
            usernameInput.BorderStyle = BorderStyle.None;
            usernameInput.Font = new Font("Segoe UI", 10F);
            usernameInput.ForeColor = Color.White;
            usernameInput.Location = new Point(35, 235);
            usernameInput.Multiline = true;
            usernameInput.Name = "usernameInput";
            usernameInput.ReadOnly = true;
            usernameInput.Size = new Size(500, 34);
            usernameInput.TabIndex = 4;
            usernameInput.Text = "Username";
            // 
            // emailLabel
            // 
            emailLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            emailLabel.AutoSize = true;
            emailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            emailLabel.ForeColor = Color.FromArgb(160, 160, 160);
            emailLabel.Location = new Point(35, 287);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(41, 15);
            emailLabel.TabIndex = 5;
            emailLabel.Text = "E-mail";
            // 
            // emailInput
            // 
            emailInput.BackColor = Color.FromArgb(30, 30, 30);
            emailInput.BorderStyle = BorderStyle.None;
            emailInput.Font = new Font("Segoe UI", 10F);
            emailInput.ForeColor = Color.White;
            emailInput.Location = new Point(35, 305);
            emailInput.Multiline = true;
            emailInput.Name = "emailInput";
            emailInput.ReadOnly = true;
            emailInput.Size = new Size(500, 34);
            emailInput.TabIndex = 6;
            emailInput.Text = "E-mail";
            // 
            // buttonsLayout
            // 
            buttonsLayout.ColumnCount = 4;
            buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonsLayout.Controls.Add(btnBack, 0, 0);
            buttonsLayout.Controls.Add(btnLogout, 2, 0);
            buttonsLayout.Dock = DockStyle.Fill;
            buttonsLayout.Location = new Point(32, 463);
            buttonsLayout.Margin = new Padding(0);
            buttonsLayout.Name = "buttonsLayout";
            buttonsLayout.RowCount = 1;
            buttonsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonsLayout.Size = new Size(803, 45);
            buttonsLayout.TabIndex = 7;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(35, 35, 35);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Dock = DockStyle.Fill;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(0, 0);
            btnBack.Margin = new Padding(0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 45);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(200, 50, 50);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(140, 0);
            btnLogout.Margin = new Padding(0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 45);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // AccountControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(viewLayout);
            Dock = DockStyle.Fill;
            Name = "AccountControl";
            Size = new Size(867, 540);
            viewLayout.ResumeLayout(false);
            viewLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)avatarIcon).EndInit();
            buttonsLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel viewLayout;
        private PictureBox avatarIcon;
        private Label userIdLabel;
        private TextBox userIdInput;
        private Label usernameLabel;
        private TextBox usernameInput;
        private Label emailLabel;
        private TextBox emailInput;
        private TableLayoutPanel buttonsLayout;
        private Button btnBack;
        private Button btnLogout;
    }
}