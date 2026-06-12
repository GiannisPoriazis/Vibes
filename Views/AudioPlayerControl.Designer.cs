namespace Vibes.Views
{
    partial class AudioPlayerControl
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
            audioPlayLayout = new TableLayoutPanel();
            trackControls = new TableLayoutPanel();
            previousTrackBtn = new FontAwesome.Sharp.IconButton();
            playTrackBtn = new FontAwesome.Sharp.IconButton();
            nextTrackBtn = new FontAwesome.Sharp.IconButton();
            audioPlayLayout.SuspendLayout();
            trackControls.SuspendLayout();
            SuspendLayout();
            // 
            // audioPlayLayout
            // 
            audioPlayLayout.BackColor = Color.Transparent;
            audioPlayLayout.ColumnCount = 1;
            audioPlayLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            audioPlayLayout.Controls.Add(trackControls, 0, 0);
            audioPlayLayout.Dock = DockStyle.Fill;
            audioPlayLayout.Location = new Point(0, 0);
            audioPlayLayout.Name = "audioPlayLayout";
            audioPlayLayout.RowCount = 2;
            audioPlayLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            audioPlayLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            audioPlayLayout.Size = new Size(524, 135);
            audioPlayLayout.TabIndex = 0;
            // 
            // trackControls
            // 
            trackControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackControls.ColumnCount = 3;
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            trackControls.Controls.Add(previousTrackBtn, 0, 0);
            trackControls.Controls.Add(playTrackBtn, 1, 0);
            trackControls.Controls.Add(nextTrackBtn, 2, 0);
            trackControls.Location = new Point(3, 3);
            trackControls.Name = "trackControls";
            trackControls.RowCount = 1;
            trackControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            trackControls.Size = new Size(518, 61);
            trackControls.TabIndex = 0;
            // 
            // previousTrackBtn
            // 
            previousTrackBtn.Dock = DockStyle.Fill;
            previousTrackBtn.IconChar = FontAwesome.Sharp.IconChar.BackwardStep;
            previousTrackBtn.IconColor = Color.Black;
            previousTrackBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            previousTrackBtn.Location = new Point(3, 3);
            previousTrackBtn.Name = "previousTrackBtn";
            previousTrackBtn.Size = new Size(54, 55);
            previousTrackBtn.TabIndex = 0;
            previousTrackBtn.Text = "iconButton1";
            previousTrackBtn.UseVisualStyleBackColor = true;
            // 
            // playTrackBtn
            // 
            playTrackBtn.Dock = DockStyle.Fill;
            playTrackBtn.IconChar = FontAwesome.Sharp.IconChar.Play;
            playTrackBtn.IconColor = Color.Black;
            playTrackBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            playTrackBtn.Location = new Point(63, 3);
            playTrackBtn.Name = "playTrackBtn";
            playTrackBtn.Size = new Size(74, 55);
            playTrackBtn.TabIndex = 1;
            playTrackBtn.Text = "iconButton2";
            playTrackBtn.UseVisualStyleBackColor = true;
            playTrackBtn.Click += playTrackBtn_Click;
            // 
            // nextTrackBtn
            // 
            nextTrackBtn.Dock = DockStyle.Fill;
            nextTrackBtn.IconChar = FontAwesome.Sharp.IconChar.ForwardStep;
            nextTrackBtn.IconColor = Color.Black;
            nextTrackBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            nextTrackBtn.Location = new Point(143, 3);
            nextTrackBtn.Name = "nextTrackBtn";
            nextTrackBtn.Size = new Size(372, 55);
            nextTrackBtn.TabIndex = 2;
            nextTrackBtn.Text = "iconButton3";
            nextTrackBtn.UseVisualStyleBackColor = true;
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(audioPlayLayout);
            Name = "AudioPlayerControl";
            Size = new Size(524, 135);
            audioPlayLayout.ResumeLayout(false);
            trackControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel audioPlayLayout;
        private TableLayoutPanel trackControls;
        private FontAwesome.Sharp.IconButton previousTrackBtn;
        private FontAwesome.Sharp.IconButton playTrackBtn;
        private FontAwesome.Sharp.IconButton nextTrackBtn;
    }
}
