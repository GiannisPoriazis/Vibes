using FontAwesome.Sharp;

namespace Vibes.Views
{
    partial class AudioPlayerControl
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
            audioPlayLayout = new TableLayoutPanel();
            currentTrackPanel = new Panel();
            lblFooterArtist = new Label();
            lblFooterTitle = new Label();
            picFooterCover = new PictureBox();
            trackControls = new TableLayoutPanel();
            previousTrackBtn = new IconButton();
            playTrackBtn = new IconButton();
            nextTrackBtn = new IconButton();
            volumePanel = new FlowLayoutPanel();
            volumeSlider = new AudioVolumeSlider();
            btnVolumeIcon = new IconButton();
            audioPlayLayout.SuspendLayout();
            currentTrackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFooterCover).BeginInit();
            trackControls.SuspendLayout();
            volumePanel.SuspendLayout();
            SuspendLayout();
            // 
            // audioPlayLayout
            // 
            audioPlayLayout.BackColor = Color.Transparent;
            audioPlayLayout.ColumnCount = 3;
            audioPlayLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            audioPlayLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            audioPlayLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            audioPlayLayout.Controls.Add(currentTrackPanel, 0, 0);
            audioPlayLayout.Controls.Add(trackControls, 1, 0);
            audioPlayLayout.Controls.Add(volumePanel, 2, 0);
            audioPlayLayout.Dock = DockStyle.Fill;
            audioPlayLayout.Location = new Point(0, 0);
            audioPlayLayout.Name = "audioPlayLayout";
            audioPlayLayout.RowCount = 1;
            audioPlayLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            audioPlayLayout.Size = new Size(800, 90);
            audioPlayLayout.TabIndex = 0;
            // 
            // currentTrackPanel
            // 
            currentTrackPanel.Controls.Add(lblFooterArtist);
            currentTrackPanel.Controls.Add(lblFooterTitle);
            currentTrackPanel.Controls.Add(picFooterCover);
            currentTrackPanel.Dock = DockStyle.Fill;
            currentTrackPanel.Location = new Point(16, 0);
            currentTrackPanel.Margin = new Padding(16, 0, 0, 0);
            currentTrackPanel.Name = "currentTrackPanel";
            currentTrackPanel.Size = new Size(224, 90);
            currentTrackPanel.TabIndex = 1;
            // 
            // lblFooterArtist
            // 
            lblFooterArtist.AutoEllipsis = true;
            lblFooterArtist.Font = new Font("Segoe UI", 8.5F);
            lblFooterArtist.ForeColor = Color.FromArgb(175, 175, 175);
            lblFooterArtist.Location = new Point(68, 44);
            lblFooterArtist.Name = "lblFooterArtist";
            lblFooterArtist.Size = new Size(150, 18);
            lblFooterArtist.TabIndex = 2;
            lblFooterArtist.Text = "Artist Name";
            // 
            // lblFooterTitle
            // 
            lblFooterTitle.AutoEllipsis = true;
            lblFooterTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFooterTitle.ForeColor = Color.White;
            lblFooterTitle.Location = new Point(68, 22);
            lblFooterTitle.Name = "lblFooterTitle";
            lblFooterTitle.Size = new Size(150, 20);
            lblFooterTitle.TabIndex = 1;
            lblFooterTitle.Text = "Track Title";
            // 
            // picFooterCover
            // 
            picFooterCover.BackColor = Color.FromArgb(30, 30, 30);
            picFooterCover.Location = new Point(0, 17);
            picFooterCover.Name = "picFooterCover";
            picFooterCover.Size = new Size(56, 56);
            picFooterCover.SizeMode = PictureBoxSizeMode.Zoom;
            picFooterCover.TabIndex = 0;
            picFooterCover.TabStop = false;
            // 
            // trackControls
            // 
            trackControls.Anchor = AnchorStyles.None;
            trackControls.AutoSize = true;
            trackControls.ColumnCount = 3;
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            trackControls.Controls.Add(previousTrackBtn, 0, 0);
            trackControls.Controls.Add(playTrackBtn, 1, 0);
            trackControls.Controls.Add(nextTrackBtn, 2, 0);
            trackControls.Location = new Point(320, 19);
            trackControls.Name = "trackControls";
            trackControls.RowCount = 1;
            trackControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            trackControls.Size = new Size(160, 51);
            trackControls.TabIndex = 0;
            // 
            // previousTrackBtn
            // 
            previousTrackBtn.Anchor = AnchorStyles.None;
            previousTrackBtn.BackColor = Color.Transparent;
            previousTrackBtn.Cursor = Cursors.Hand;
            previousTrackBtn.FlatAppearance.BorderSize = 0;
            previousTrackBtn.FlatStyle = FlatStyle.Flat;
            previousTrackBtn.IconChar = IconChar.BackwardStep;
            previousTrackBtn.IconColor = Color.FromArgb(180, 180, 180);
            previousTrackBtn.IconFont = IconFont.Auto;
            previousTrackBtn.IconSize = 22;
            previousTrackBtn.Location = new Point(10, 10);
            previousTrackBtn.Name = "previousTrackBtn";
            previousTrackBtn.Size = new Size(30, 30);
            previousTrackBtn.TabIndex = 0;
            previousTrackBtn.UseVisualStyleBackColor = false;
            previousTrackBtn.Click += previousTrackBtn_Click;
            // 
            // playTrackBtn
            // 
            playTrackBtn.Anchor = AnchorStyles.None;
            playTrackBtn.BackColor = Color.Transparent;
            playTrackBtn.Cursor = Cursors.Hand;
            playTrackBtn.FlatAppearance.BorderSize = 0;
            playTrackBtn.FlatStyle = FlatStyle.Flat;
            playTrackBtn.IconChar = IconChar.Play;
            playTrackBtn.IconColor = Color.White;
            playTrackBtn.IconFont = IconFont.Auto;
            playTrackBtn.IconSize = 32;
            playTrackBtn.Location = new Point(57, 3);
            playTrackBtn.Name = "playTrackBtn";
            playTrackBtn.Size = new Size(45, 45);
            playTrackBtn.TabIndex = 1;
            playTrackBtn.UseVisualStyleBackColor = false;
            playTrackBtn.Click += playTrackBtn_Click;
            // 
            // nextTrackBtn
            // 
            nextTrackBtn.Anchor = AnchorStyles.None;
            nextTrackBtn.BackColor = Color.Transparent;
            nextTrackBtn.Cursor = Cursors.Hand;
            nextTrackBtn.FlatAppearance.BorderSize = 0;
            nextTrackBtn.FlatStyle = FlatStyle.Flat;
            nextTrackBtn.IconChar = IconChar.ForwardStep;
            nextTrackBtn.IconColor = Color.FromArgb(180, 180, 180);
            nextTrackBtn.IconFont = IconFont.Auto;
            nextTrackBtn.IconSize = 22;
            nextTrackBtn.Location = new Point(120, 10);
            nextTrackBtn.Name = "nextTrackBtn";
            nextTrackBtn.Size = new Size(30, 30);
            nextTrackBtn.TabIndex = 2;
            nextTrackBtn.UseVisualStyleBackColor = false;
            nextTrackBtn.Click += nextTrackBtn_Click;
            // 
            // volumePanel
            // 
            volumePanel.Controls.Add(volumeSlider);
            volumePanel.Controls.Add(btnVolumeIcon);
            volumePanel.Dock = DockStyle.Fill;
            volumePanel.Location = new Point(563, 3);
            volumePanel.Name = "volumePanel";
            volumePanel.Padding = new Padding(20, 28, 20, 0);
            volumePanel.RightToLeft = RightToLeft.Yes;
            volumePanel.Size = new Size(234, 84);
            volumePanel.TabIndex = 2;
            // 
            // volumeSlider
            // 
            volumeSlider.BackColor = Color.Transparent;
            volumeSlider.Cursor = Cursors.Hand;
            volumeSlider.Location = new Point(84, 28);
            volumeSlider.Margin = new Padding(0);
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(110, 32);
            volumeSlider.TabIndex = 1;
            volumeSlider.Value = 70;
            volumeSlider.Scroll += volumeSlider_Scroll;
            // 
            // btnVolumeIcon
            // 
            btnVolumeIcon.Anchor = AnchorStyles.Right;
            btnVolumeIcon.BackColor = Color.Transparent;
            btnVolumeIcon.Cursor = Cursors.Hand;
            btnVolumeIcon.FlatAppearance.BorderSize = 0;
            btnVolumeIcon.FlatStyle = FlatStyle.Flat;
            btnVolumeIcon.IconChar = IconChar.VolumeUp;
            btnVolumeIcon.IconColor = Color.FromArgb(180, 180, 180);
            btnVolumeIcon.IconFont = IconFont.Auto;
            btnVolumeIcon.IconSize = 18;
            btnVolumeIcon.Location = new Point(55, 31);
            btnVolumeIcon.Name = "btnVolumeIcon";
            btnVolumeIcon.Size = new Size(26, 26);
            btnVolumeIcon.TabIndex = 0;
            btnVolumeIcon.UseVisualStyleBackColor = false;
            btnVolumeIcon.Click += btnVolumeIcon_Click;
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(audioPlayLayout);
            Name = "AudioPlayerControl";
            Size = new Size(800, 90);
            audioPlayLayout.ResumeLayout(false);
            audioPlayLayout.PerformLayout();
            currentTrackPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picFooterCover).EndInit();
            trackControls.ResumeLayout(false);
            volumePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel audioPlayLayout;
        private TableLayoutPanel trackControls;
        private IconButton previousTrackBtn;
        private IconButton playTrackBtn;
        private IconButton nextTrackBtn;

        public Panel currentTrackPanel;
        private PictureBox picFooterCover;
        private Label lblFooterTitle;
        private Label lblFooterArtist;

        private FlowLayoutPanel volumePanel;
        private AudioVolumeSlider volumeSlider;
        private IconButton btnVolumeIcon;
    }
}