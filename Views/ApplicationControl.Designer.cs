using Vibes.Design;
using Vibes.Extensions;

namespace Vibes.Views
{
    partial class ApplicationControl
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
            applicationLayout.ColumnCount = 2;
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationLayout.Controls.Add(playlistPanel, 0, 0);
            applicationLayout.Dock = DockStyle.Fill;
            applicationLayout.Location = new Point(0, 0);
            applicationLayout.Margin = new Padding(0);
            applicationLayout.Name = "applicationLayout";
            applicationLayout.RowCount = 1;
            applicationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            applicationLayout.Size = new Size(800, 500);
            applicationLayout.TabIndex = 0;
            // 
            // playlistPanel
            // 
            playlistPanel.BackColor = Color.FromArgb(18, 18, 18);
            playlistPanel.Controls.Add(playlistPanelLayout);
            playlistPanel.Dock = DockStyle.Fill;
            playlistPanel.Location = new Point(3, 3);
            playlistPanel.Name = "playlistPanel";
            playlistPanel.Size = new Size(394, 494);
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
            playlistPanelLayout.Margin = new Padding(0);
            playlistPanelLayout.Name = "playlistPanelLayout";
            playlistPanelLayout.RowCount = 2;
            playlistPanelLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            playlistPanelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            playlistPanelLayout.Size = new Size(394, 494);
            playlistPanelLayout.TabIndex = 1;
            // 
            // playlistView
            // 
            playlistView.BackColor = Color.FromArgb(18, 18, 18);
            playlistView.BorderStyle = BorderStyle.None;
            playlistView.Dock = DockStyle.Fill;
            playlistView.ForeColor = Color.White;
            playlistView.FullRowSelect = true;
            playlistView.HeaderStyle = ColumnHeaderStyle.None;
            playlistView.Location = new Point(0, 50);
            playlistView.Margin = new Padding(0);
            playlistView.MultiSelect = false;
            playlistView.Name = "playlistView";
            playlistView.OwnerDraw = true;
            playlistView.Size = new Size(394, 444);
            playlistView.TabIndex = 0;
            playlistView.UseCompatibleStateImageBehavior = false;
            playlistView.View = View.Details;
            // 
            // playlistPanelHeaderLayout
            // 
            playlistPanelHeaderLayout.ColumnCount = 2;
            playlistPanelHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            playlistPanelHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            playlistPanelHeaderLayout.Controls.Add(createPlaylistButton, 1, 0);
            playlistPanelHeaderLayout.Controls.Add(playlistPanelHeader, 0, 0);
            playlistPanelHeaderLayout.Dock = DockStyle.Fill;
            playlistPanelHeaderLayout.Location = new Point(0, 0);
            playlistPanelHeaderLayout.Margin = new Padding(0);
            playlistPanelHeaderLayout.Name = "playlistPanelHeaderLayout";
            playlistPanelHeaderLayout.RowCount = 1;
            playlistPanelHeaderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            playlistPanelHeaderLayout.Size = new Size(394, 50);
            playlistPanelHeaderLayout.TabIndex = 1;
            // 
            // createPlaylistButton
            // 
            createPlaylistButton.BackColor = Color.Transparent;
            createPlaylistButton.Dock = DockStyle.Fill;
            createPlaylistButton.FlatAppearance.BorderSize = 0;
            createPlaylistButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            createPlaylistButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 25, 25);
            createPlaylistButton.FlatStyle = FlatStyle.Flat;
            createPlaylistButton.IconChar = FontAwesome.Sharp.IconChar.Add;
            createPlaylistButton.IconColor = Color.FromArgb(180, 180, 180);
            createPlaylistButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            createPlaylistButton.IconSize = 22;
            createPlaylistButton.Location = new Point(344, 0);
            createPlaylistButton.Margin = new Padding(0);
            createPlaylistButton.Name = "createPlaylistButton";
            createPlaylistButton.Size = new Size(50, 50);
            createPlaylistButton.TabIndex = 0;
            createPlaylistButton.UseVisualStyleBackColor = false;
            createPlaylistButton.Click += AddPlaylistBtn_Click;
            // 
            // playlistPanelHeader
            // 
            playlistPanelHeader.AutoSize = true;
            playlistPanelHeader.Dock = DockStyle.Fill;
            playlistPanelHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            playlistPanelHeader.ForeColor = Color.FromArgb(180, 180, 180);
            playlistPanelHeader.Location = new Point(3, 0);
            playlistPanelHeader.Name = "playlistPanelHeader";
            playlistPanelHeader.Padding = new Padding(16, 0, 0, 0);
            playlistPanelHeader.Size = new Size(338, 50);
            playlistPanelHeader.TabIndex = 1;
            playlistPanelHeader.Text = "Your Library";
            playlistPanelHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ApplicationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(applicationLayout);
            Name = "ApplicationControl";
            Size = new Size(800, 500);
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
    }
}