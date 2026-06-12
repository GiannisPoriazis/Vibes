using Vibes.Design;

namespace Vibes.Views
{
    partial class AccountControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            viewLayout = new TableLayoutPanel();
            emailInput = new TextBox();
            emailLabel = new Label();
            avatarIcon = new PictureBox();
            usernameLabel = new Label();
            usernameInput = new TextBox();
            viewLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)avatarIcon).BeginInit();
            SuspendLayout();
            // 
            // viewLayout
            // 
            viewLayout.ColumnCount = 1;
            viewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            viewLayout.Controls.Add(emailInput, 0, 4);
            viewLayout.Controls.Add(emailLabel, 0, 3);
            viewLayout.Controls.Add(avatarIcon, 0, 0);
            viewLayout.Controls.Add(usernameLabel, 0, 1);
            viewLayout.Controls.Add(usernameInput, 0, 2);
            viewLayout.Dock = DockStyle.Fill;
            viewLayout.Location = new Point(0, 0);
            viewLayout.Margin = new Padding(10);
            viewLayout.Name = "viewLayout";
            viewLayout.Padding = new Padding(10);
            viewLayout.RowCount = 6;
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            viewLayout.Size = new Size(639, 497);
            viewLayout.TabIndex = 0;
            // 
            // emailInput
            // 
            emailInput.BackColor = Color.FromArgb(28, 28, 36);
            emailInput.BorderStyle = BorderStyle.FixedSingle;
            emailInput.Dock = DockStyle.Fill;
            emailInput.ForeColor = Color.White;
            emailInput.Location = new Point(13, 203);
            emailInput.Multiline = true;
            emailInput.Name = "emailInput";
            emailInput.ReadOnly = true;
            emailInput.Size = new Size(613, 34);
            emailInput.TabIndex = 4;
            emailInput.Text = "E-mail";
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Dock = DockStyle.Fill;
            emailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            emailLabel.ForeColor = Color.White;
            emailLabel.Location = new Point(13, 160);
            emailLabel.Name = "emailLabel";
            emailLabel.Padding = new Padding(0, 20, 0, 0);
            emailLabel.Size = new Size(613, 40);
            emailLabel.TabIndex = 3;
            emailLabel.Text = "E-mail";
            // 
            // avatarIcon
            // 
            avatarIcon.Dock = DockStyle.Left;
            avatarIcon.Location = new Point(13, 10);
            avatarIcon.Margin = new Padding(3, 0, 3, 0);
            avatarIcon.Name = "avatarIcon";
            avatarIcon.Size = new Size(70, 70);
            avatarIcon.SizeMode = PictureBoxSizeMode.Zoom;
            avatarIcon.TabIndex = 0;
            avatarIcon.TabStop = false;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Dock = DockStyle.Fill;
            usernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            usernameLabel.ForeColor = Color.White;
            usernameLabel.Location = new Point(13, 80);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Padding = new Padding(0, 20, 0, 0);
            usernameLabel.Size = new Size(613, 40);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Username";
            // 
            // usernameInput
            // 
            usernameInput.BackColor = Color.FromArgb(28, 28, 36);
            usernameInput.BorderStyle = BorderStyle.FixedSingle;
            usernameInput.Dock = DockStyle.Fill;
            usernameInput.ForeColor = Color.White;
            usernameInput.Location = new Point(13, 123);
            usernameInput.Multiline = true;
            usernameInput.Name = "usernameInput";
            usernameInput.ReadOnly = true;
            usernameInput.Size = new Size(613, 34);
            usernameInput.TabIndex = 1;
            usernameInput.Text = "Username";
            // 
            // AccountControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 28, 36);
            Controls.Add(viewLayout);
            Name = "AccountControl";
            Size = new Size(639, 497);
            viewLayout.ResumeLayout(false);
            viewLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)avatarIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel viewLayout;
        private PictureBox avatarIcon;
        private TextBox usernameInput;
        private Label usernameLabel;
        private Label emailLabel;
        private TextBox emailInput;
    }
}
