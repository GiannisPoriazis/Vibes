using Vibes.Design;

namespace Vibes.Views
{
    partial class ApplicationControl
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
            applicationLayout = new TableLayoutPanel();
            playlistPanel = new Panel();
            playlistPanelLayout = new TableLayoutPanel();
            playlistView = new ListView();
            playlistPanelHeaderLayout = new TableLayoutPanel();
            createPlaylistButton = new FontAwesome.Sharp.IconButton();
            playlistPanelHeader = new Label();
            applicationLayout.SuspendLayout();
            playlistPanel.SuspendLayout();
            playlistPanelLayout.SuspendLayout();
            playlistPanelHeaderLayout.SuspendLayout();
            SuspendLayout();
            // 
            // applicationLayout
            // 
            applicationLayout.ColumnCount = 3;
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            applicationLayout.Controls.Add(playlistPanel, 0, 0);
            applicationLayout.Dock = DockStyle.Fill;
            applicationLayout.Location = new Point(0, 0);
            applicationLayout.Name = "applicationLayout";
            applicationLayout.RowCount = 1;
            applicationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            applicationLayout.Size = new Size(798, 482);
            applicationLayout.TabIndex = 0;
            // 
            // playlistPanel
            // 
            playlistPanel.BackColor = Color.FromArgb(28, 28, 36);
            playlistPanel.Controls.Add(playlistPanelLayout);
            playlistPanel.Dock = DockStyle.Fill;
            playlistPanel.Location = new Point(3, 3);
            playlistPanel.Name = "playlistPanel";
            playlistPanel.Size = new Size(193, 476);
            playlistPanel.TabIndex = 0;
            // 
            // playlistPanelLayout
            // 
            playlistPanelLayout.ColumnCount = 1;
            playlistPanelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            playlistPanelLayout.Controls.Add(playlistView, 0, 1);
            playlistPanelLayout.Controls.Add(playlistPanelHeaderLayout, 0, 0);
            playlistPanelLayout.Dock = DockStyle.Fill;
            playlistPanelLayout.Location = new Point(0, 0);
            playlistPanelLayout.Name = "playlistPanelLayout";
            playlistPanelLayout.RowCount = 2;
            playlistPanelLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            playlistPanelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            playlistPanelLayout.Size = new Size(193, 476);
            playlistPanelLayout.TabIndex = 1;
            // 
            // playlistView
            // 
            playlistView.BackColor = Color.FromArgb(28, 28, 36);
            playlistView.Dock = DockStyle.Fill;
            playlistView.ForeColor = Color.White;
            playlistView.FullRowSelect = true;
            playlistView.HeaderStyle = ColumnHeaderStyle.None;
            playlistView.Location = new Point(3, 43);
            playlistView.MultiSelect = false;
            playlistView.Name = "playlistView";
            playlistView.RightToLeft = RightToLeft.No;
            playlistView.Size = new Size(187, 430);
            playlistView.TabIndex = 0;
            playlistView.UseCompatibleStateImageBehavior = false;
            playlistView.View = View.Details;
            playlistView.Resize += playlistView_Resize;
            // 
            // playlistPanelHeaderLayout
            // 
            playlistPanelHeaderLayout.ColumnCount = 2;
            playlistPanelHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            playlistPanelHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            playlistPanelHeaderLayout.Controls.Add(createPlaylistButton, 1, 0);
            playlistPanelHeaderLayout.Controls.Add(playlistPanelHeader, 0, 0);
            playlistPanelHeaderLayout.Location = new Point(3, 3);
            playlistPanelHeaderLayout.Name = "playlistPanelHeaderLayout";
            playlistPanelHeaderLayout.RowCount = 1;
            playlistPanelHeaderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            playlistPanelHeaderLayout.Size = new Size(187, 34);
            playlistPanelHeaderLayout.TabIndex = 1;
            // 
            // createPlaylistButton
            // 
            createPlaylistButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            createPlaylistButton.BackColor = Color.Transparent;
            createPlaylistButton.IconChar = FontAwesome.Sharp.IconChar.Plus;
            createPlaylistButton.IconColor = Color.Black;
            createPlaylistButton.IconFont = FontAwesome.Sharp.IconFont.Regular;
            createPlaylistButton.IconSize = 34;
            createPlaylistButton.Location = new Point(147, 0);
            createPlaylistButton.Margin = new Padding(0);
            createPlaylistButton.Name = "createPlaylistButton";
            createPlaylistButton.Size = new Size(40, 34);
            createPlaylistButton.TabIndex = 0;
            createPlaylistButton.UseVisualStyleBackColor = false;
            createPlaylistButton.Click += AddPlaylistBtn_Click;
            // 
            // playlistPanelHeader
            // 
            playlistPanelHeader.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            playlistPanelHeader.AutoSize = true;
            playlistPanelHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playlistPanelHeader.ForeColor = Color.White;
            playlistPanelHeader.Location = new Point(3, 0);
            playlistPanelHeader.Name = "playlistPanelHeader";
            playlistPanelHeader.Padding = new Padding(0, 15, 0, 0);
            playlistPanelHeader.Size = new Size(73, 34);
            playlistPanelHeader.TabIndex = 1;
            playlistPanelHeader.Text = "Your Library";
            // 
            // ApplicationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(applicationLayout);
            Name = "ApplicationControl";
            Size = new Size(798, 482);
            applicationLayout.ResumeLayout(false);
            playlistPanel.ResumeLayout(false);
            playlistPanelLayout.ResumeLayout(false);
            playlistPanelHeaderLayout.ResumeLayout(false);
            playlistPanelHeaderLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public TableLayoutPanel applicationLayout;
        private Panel playlistPanel;
        private ListView playlistView;
        private TableLayoutPanel playlistPanelLayout;
        private TableLayoutPanel playlistPanelHeaderLayout;
        private FontAwesome.Sharp.IconButton createPlaylistButton;
        private Label playlistPanelHeader;
        private ContextMenuStrip playlistContextMenu = null!;
    }
}
