using FontAwesome.Sharp;
using Vibes.Design;

namespace Vibes
{
    partial class Vibes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vibes));
            avatarMenu = new ContextMenuStrip(components);
            accountToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            mainGrid = new TableLayoutPanel();
            titleBar = new Panel();
            titleBarLayout = new TableLayoutPanel();
            windowActions = new TableLayoutPanel();
            minimizeButton = new IconButton();
            enlargeButton = new IconButton();
            exitButton = new IconButton();
            logoIcon = new PictureBox();
            headerCenterLayout = new TableLayoutPanel();
            homeButton = new IconButton();
            userAvatar = new PictureBox();
            copyrightLabel = new Label();
            pageContainer = new TableLayoutPanel();
            avatarMenu.SuspendLayout();
            mainGrid.SuspendLayout();
            titleBar.SuspendLayout();
            titleBarLayout.SuspendLayout();
            windowActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoIcon).BeginInit();
            headerCenterLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userAvatar).BeginInit();
            pageContainer.SuspendLayout();
            SuspendLayout();
            // 
            // avatarMenu
            // 
            avatarMenu.BackColor = Color.FromArgb(28, 28, 28);
            avatarMenu.Items.AddRange(new ToolStripItem[] { accountToolStripMenuItem, logoutToolStripMenuItem });
            avatarMenu.Name = "avatarMenu";
            avatarMenu.Size = new Size(127, 72);
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.Font = new Font("Segoe UI", 10F);
            accountToolStripMenuItem.ForeColor = Color.FromArgb(230, 230, 230);
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Padding = new Padding(12, 6, 12, 6);
            accountToolStripMenuItem.Size = new Size(150, 34);
            accountToolStripMenuItem.Text = "Account";
            accountToolStripMenuItem.Click += AvatarMenu_Account_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Font = new Font("Segoe UI", 10F);
            logoutToolStripMenuItem.ForeColor = Color.FromArgb(230, 230, 230);
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Padding = new Padding(12, 6, 12, 6);
            logoutToolStripMenuItem.Size = new Size(150, 34);
            logoutToolStripMenuItem.Text = "Log out";
            logoutToolStripMenuItem.Click += UserLogout;
            // 
            // mainGrid
            // 
            mainGrid.ColumnCount = 1;
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            mainGrid.Controls.Add(titleBar, 0, 0);
            mainGrid.Dock = DockStyle.Fill;
            mainGrid.Location = new Point(1, 1);
            mainGrid.Margin = new Padding(0);
            mainGrid.Name = "mainGrid";
            mainGrid.RowCount = 3;
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainGrid.Size = new Size(1898, 1022);
            mainGrid.TabIndex = 0;
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.Black;
            mainGrid.SetColumnSpan(titleBar, 3);
            titleBar.Controls.Add(titleBarLayout);
            titleBar.Dock = DockStyle.Fill;
            titleBar.Location = new Point(0, 0);
            titleBar.Margin = new Padding(0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(1898, 50);
            titleBar.TabIndex = 0;
            // 
            // titleBarLayout
            // 
            titleBarLayout.ColumnCount = 3;
            titleBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            titleBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            titleBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            titleBarLayout.Controls.Add(windowActions, 2, 0);
            titleBarLayout.Controls.Add(logoIcon, 0, 0);
            titleBarLayout.Controls.Add(headerCenterLayout, 1, 0);
            titleBarLayout.Dock = DockStyle.Fill;
            titleBarLayout.Location = new Point(0, 0);
            titleBarLayout.Margin = new Padding(0);
            titleBarLayout.Name = "titleBarLayout";
            titleBarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            titleBarLayout.Size = new Size(1898, 50);
            titleBarLayout.TabIndex = 0;
            titleBarLayout.MouseDown += Logo_MouseDown;
            titleBarLayout.MouseMove += Logo_MouseMove;
            titleBarLayout.MouseUp += Logo_MouseUp;
            // 
            // windowActions
            // 
            windowActions.ColumnCount = 3;
            windowActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            windowActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            windowActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            windowActions.Controls.Add(minimizeButton, 0, 0);
            windowActions.Controls.Add(enlargeButton, 1, 0);
            windowActions.Controls.Add(exitButton, 2, 0);
            windowActions.Dock = DockStyle.Fill;
            windowActions.Location = new Point(1748, 0);
            windowActions.Margin = new Padding(0);
            windowActions.Name = "windowActions";
            windowActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            windowActions.Size = new Size(150, 50);
            windowActions.TabIndex = 0;
            // 
            // minimizeButton
            // 
            minimizeButton.BackColor = Color.Transparent;
            minimizeButton.Dock = DockStyle.Fill;
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 40, 40, 40);
            minimizeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 40, 40, 40);
            minimizeButton.FlatStyle = FlatStyle.Flat;
            minimizeButton.ForeColor = SystemColors.Control;
            minimizeButton.IconChar = IconChar.Subtract;
            minimizeButton.IconColor = Color.White;
            minimizeButton.IconFont = IconFont.Auto;
            minimizeButton.IconSize = 16;
            minimizeButton.Location = new Point(0, 0);
            minimizeButton.Margin = new Padding(0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.Size = new Size(50, 50);
            minimizeButton.TabIndex = 2;
            minimizeButton.UseVisualStyleBackColor = false;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // enlargeButton
            // 
            enlargeButton.BackColor = Color.Transparent;
            enlargeButton.Dock = DockStyle.Fill;
            enlargeButton.FlatAppearance.BorderSize = 0;
            enlargeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 40, 40, 40);
            enlargeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 40, 40, 40);
            enlargeButton.FlatStyle = FlatStyle.Flat;
            enlargeButton.IconChar = IconChar.Expand;
            enlargeButton.IconColor = Color.White;
            enlargeButton.IconFont = IconFont.Auto;
            enlargeButton.IconSize = 16;
            enlargeButton.Location = new Point(50, 0);
            enlargeButton.Margin = new Padding(0);
            enlargeButton.Name = "enlargeButton";
            enlargeButton.Size = new Size(50, 50);
            enlargeButton.TabIndex = 1;
            enlargeButton.UseVisualStyleBackColor = false;
            enlargeButton.Click += EnlargeButton_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Transparent;
            exitButton.Dock = DockStyle.Fill;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatAppearance.MouseDownBackColor = Color.IndianRed;
            exitButton.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.ForeColor = SystemColors.Control;
            exitButton.IconChar = IconChar.Close;
            exitButton.IconColor = Color.White;
            exitButton.IconFont = IconFont.Auto;
            exitButton.IconSize = 18;
            exitButton.Location = new Point(100, 0);
            exitButton.Margin = new Padding(0);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(50, 50);
            exitButton.TabIndex = 0;
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += ExitButton_Click;
            // 
            // logoIcon
            // 
            logoIcon.BackColor = Color.Transparent;
            logoIcon.Image = Properties.Resources.vibes_logo;
            logoIcon.InitialImage = null;
            logoIcon.Location = new Point(5, 5);
            logoIcon.Margin = new Padding(5, 5, 0, 5);
            logoIcon.Name = "logoIcon";
            logoIcon.Size = new Size(40, 40);
            logoIcon.SizeMode = PictureBoxSizeMode.Zoom;
            logoIcon.TabIndex = 1;
            logoIcon.TabStop = false;
            logoIcon.WaitOnLoad = true;
            // 
            // headerCenterLayout
            // 
            headerCenterLayout.ColumnCount = 4;
            headerCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            headerCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            headerCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            headerCenterLayout.Controls.Add(homeButton, 1, 0);
            headerCenterLayout.Controls.Add(userAvatar, 3, 0);
            headerCenterLayout.Dock = DockStyle.Fill;
            headerCenterLayout.Location = new Point(53, 3);
            headerCenterLayout.Name = "headerCenterLayout";
            headerCenterLayout.RowCount = 1;
            headerCenterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            headerCenterLayout.Size = new Size(1692, 44);
            headerCenterLayout.TabIndex = 2;
            // 
            // homeButton
            // 
            homeButton.BackColor = Color.FromArgb(36, 36, 36);
            homeButton.Cursor = Cursors.Hand;
            homeButton.Dock = DockStyle.Fill;
            homeButton.FlatAppearance.BorderSize = 0;
            homeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50);
            homeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            homeButton.FlatStyle = FlatStyle.Flat;
            homeButton.IconChar = IconChar.House;
            homeButton.IconColor = Color.White;
            homeButton.IconFont = IconFont.Auto;
            homeButton.IconSize = 22;
            homeButton.Location = new Point(1592, 0);
            homeButton.Margin = new Padding(0);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(50, 44);
            homeButton.TabIndex = 3;
            homeButton.UseVisualStyleBackColor = false;
            homeButton.Click += HomeButton_Click;
            homeButton.Paint += HomeButton_Paint;
            // 
            // userAvatar
            // 
            userAvatar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            userAvatar.ContextMenuStrip = avatarMenu;
            userAvatar.Cursor = Cursors.Hand;
            userAvatar.Location = new Point(1642, 2);
            userAvatar.Margin = new Padding(0, 2, 0, 2);
            userAvatar.Name = "userAvatar";
            userAvatar.Size = new Size(50, 40);
            userAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            userAvatar.TabIndex = 2;
            userAvatar.TabStop = false;
            userAvatar.Visible = false;
            userAvatar.MouseUp += UserAvatar_MouseUp;
            // 
            // copyrightLabel
            // 
            copyrightLabel.AutoSize = true;
            copyrightLabel.Dock = DockStyle.Fill;
            copyrightLabel.ForeColor = Color.White;
            copyrightLabel.Location = new Point(3, 548);
            copyrightLabel.Name = "copyrightLabel";
            copyrightLabel.Size = new Size(892, 50);
            copyrightLabel.TabIndex = 1;
            copyrightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pageContainer
            // 
            pageContainer.ColumnCount = 1;
            pageContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pageContainer.Controls.Add(mainGrid);
            pageContainer.Dock = DockStyle.Fill;
            pageContainer.Location = new Point(0, 0);
            pageContainer.Name = "pageContainer";
            pageContainer.Padding = new Padding(1);
            pageContainer.RowCount = 1;
            pageContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pageContainer.Size = new Size(1900, 1024);
            pageContainer.TabIndex = 2;
            pageContainer.Paint += pageContainer_Paint;
            // 
            // Vibes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1900, 1024);
            ControlBox = false;
            Controls.Add(pageContainer);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Vibes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vibes";
            avatarMenu.ResumeLayout(false);
            mainGrid.ResumeLayout(false);
            titleBar.ResumeLayout(false);
            titleBarLayout.ResumeLayout(false);
            windowActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoIcon).EndInit();
            headerCenterLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)userAvatar).EndInit();
            pageContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainGrid;
        private Panel titleBar;
        private TableLayoutPanel titleBarLayout;
        private TableLayoutPanel windowActions;
        private IconButton exitButton;
        private IconButton enlargeButton;
        private IconButton minimizeButton;
        private IconButton homeButton;
        private PictureBox logoIcon;
        private Label copyrightLabel;
        private Control currentControl;
        private TableLayoutPanel pageContainer;
        private PictureBox userAvatar;
        private ContextMenuStrip avatarMenu;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem accountToolStripMenuItem1;
        private TableLayoutPanel headerCenterLayout;
    }
}
