using Vibes.Design;

namespace Vibes.Views
{
    partial class LoginControl
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
            btnLogin = new Button();
            formLayout = new TableLayoutPanel();
            logoFull = new PictureBox();
            pageLayout = new TableLayoutPanel();
            formLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoFull).BeginInit();
            pageLayout.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(3, 229);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(250, 40);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Log In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            btnLogin.Paint += BtnLogin_Paint;
            btnLogin.MouseEnter += BtnLogin_MouseEnter;
            btnLogin.MouseLeave += BtnLogin_MouseLeave;
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 1;
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            formLayout.Controls.Add(logoFull, 0, 1);
            formLayout.Controls.Add(btnLogin, 0, 2);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(166, 3);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 4;
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            formLayout.Size = new Size(212, 374);
            formLayout.TabIndex = 4;
            // 
            // logoFull
            // 
            logoFull.Dock = DockStyle.Fill;
            logoFull.Image = Properties.Resources.logo_full;
            logoFull.Location = new Point(3, 77);
            logoFull.Name = "logoFull";
            logoFull.Size = new Size(206, 144);
            logoFull.SizeMode = PictureBoxSizeMode.Zoom;
            logoFull.TabIndex = 1;
            logoFull.TabStop = false;
            // 
            // pageLayout
            // 
            pageLayout.ColumnCount = 3;
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            pageLayout.Controls.Add(formLayout, 1, 0);
            pageLayout.Dock = DockStyle.Fill;
            pageLayout.Location = new Point(16, 16);
            pageLayout.Name = "pageLayout";
            pageLayout.RowCount = 1;
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pageLayout.Size = new Size(545, 380);
            pageLayout.TabIndex = 4;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pageLayout);
            Name = "LoginControl";
            Padding = new Padding(16);
            Size = new Size(577, 412);
            Load += LoginControl_Load;
            formLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoFull).EndInit();
            pageLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnLogin;
        private TableLayoutPanel formLayout;
        private TableLayoutPanel pageLayout;
        private PictureBox logoFull;
    }
}
