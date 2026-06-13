using FontAwesome.Sharp;
using Vibes.Design;

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
            previousTrackBtn = new IconButton();
            playTrackBtn = new IconButton();
            nextTrackBtn = new IconButton();
            audioPlayLayout.SuspendLayout();
            trackControls.SuspendLayout();
            SuspendLayout();
            // 
            // audioPlayLayout
            // 
            audioPlayLayout.BackColor = Color.Transparent;
            audioPlayLayout.ColumnCount = 1;
            audioPlayLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            audioPlayLayout.Controls.Add(trackControls, 0, 0);
            audioPlayLayout.Dock = DockStyle.Fill;
            audioPlayLayout.Location = new Point(0, 0);
            audioPlayLayout.Name = "audioPlayLayout";
            audioPlayLayout.RowCount = 1;
            audioPlayLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            audioPlayLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            audioPlayLayout.Size = new Size(524, 135);
            audioPlayLayout.TabIndex = 0;
            // 
            // trackControls
            // 
            trackControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            trackControls.AutoSize = true;
            trackControls.ColumnCount = 3;
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            trackControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            trackControls.Controls.Add(previousTrackBtn, 0, 0);
            trackControls.Controls.Add(playTrackBtn, 1, 0);
            trackControls.Controls.Add(nextTrackBtn, 2, 0);
            trackControls.Location = new Point(167, 3);
            trackControls.Name = "trackControls";
            trackControls.RowCount = 1;
            trackControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            trackControls.Size = new Size(190, 129);
            trackControls.TabIndex = 0;
            // 
            // previousTrackBtn
            // 
            previousTrackBtn.Anchor = AnchorStyles.None;
            previousTrackBtn.BackColor = Color.Transparent;
            previousTrackBtn.Cursor = Cursors.Hand;
            previousTrackBtn.FlatAppearance.BorderSize = 0;
            previousTrackBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            previousTrackBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            previousTrackBtn.FlatStyle = FlatStyle.Flat;
            previousTrackBtn.ForeColor = Color.Transparent;
            previousTrackBtn.IconChar = IconChar.BackwardStep;
            previousTrackBtn.IconColor = Color.White;
            previousTrackBtn.IconFont = IconFont.Auto;
            previousTrackBtn.IconSize = 20;
            previousTrackBtn.Location = new Point(15, 49);
            previousTrackBtn.Name = "previousTrackBtn";
            previousTrackBtn.Size = new Size(30, 30);
            previousTrackBtn.TabIndex = 0;
            previousTrackBtn.TextImageRelation = TextImageRelation.ImageAboveText;
            previousTrackBtn.UseVisualStyleBackColor = false;
            // 
            // playTrackBtn
            // 
            playTrackBtn.Anchor = AnchorStyles.None;
            playTrackBtn.BackColor = Color.Transparent;
            playTrackBtn.Cursor = Cursors.Hand;
            playTrackBtn.FlatAppearance.BorderSize = 0;
            playTrackBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            playTrackBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            playTrackBtn.FlatStyle = FlatStyle.Flat;
            playTrackBtn.ForeColor = Color.Transparent;
            playTrackBtn.IconChar = IconChar.Play;
            playTrackBtn.IconColor = Color.White;
            playTrackBtn.IconFont = IconFont.Auto;
            playTrackBtn.IconSize = 20;
            playTrackBtn.Location = new Point(70, 39);
            playTrackBtn.Name = "playTrackBtn";
            playTrackBtn.Size = new Size(50, 50);
            playTrackBtn.TabIndex = 1;
            playTrackBtn.TextImageRelation = TextImageRelation.ImageAboveText;
            playTrackBtn.UseVisualStyleBackColor = false;
            playTrackBtn.Click += playTrackBtn_Click;
            // 
            // nextTrackBtn
            // 
            nextTrackBtn.Anchor = AnchorStyles.None;
            nextTrackBtn.BackColor = Color.Transparent;
            nextTrackBtn.Cursor = Cursors.Hand;
            nextTrackBtn.FlatAppearance.BorderSize = 0;
            nextTrackBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            nextTrackBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            nextTrackBtn.FlatStyle = FlatStyle.Flat;
            nextTrackBtn.ForeColor = Color.Transparent;
            nextTrackBtn.IconChar = IconChar.ForwardStep;
            nextTrackBtn.IconColor = Color.White;
            nextTrackBtn.IconFont = IconFont.Auto;
            nextTrackBtn.IconSize = 20;
            nextTrackBtn.Location = new Point(145, 49);
            nextTrackBtn.Name = "nextTrackBtn";
            nextTrackBtn.Size = new Size(30, 30);
            nextTrackBtn.TabIndex = 2;
            nextTrackBtn.TextImageRelation = TextImageRelation.ImageAboveText;
            nextTrackBtn.UseVisualStyleBackColor = false;
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(audioPlayLayout);
            Name = "AudioPlayerControl";
            Size = new Size(524, 135);
            audioPlayLayout.ResumeLayout(false);
            audioPlayLayout.PerformLayout();
            trackControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel audioPlayLayout;
        private TableLayoutPanel trackControls;
        private IconButton previousTrackBtn;
        private IconButton playTrackBtn;
        private IconButton nextTrackBtn;
    }
}
