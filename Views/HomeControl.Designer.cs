using System.Xml.Linq;

namespace Vibes.Views
{
    partial class HomeControl
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
            mainScrollPanel = new Panel();
            contentLayout = new TableLayoutPanel();
            lblHotRightNow = new Label();
            hotRightNowLayout = new FlowLayoutPanel();
            lblAllTimeGreatest = new Label();
            allTimeGreatestLayout = new FlowLayoutPanel();
            lblVibesPicks = new Label();
            vibesPicksLayout = new FlowLayoutPanel();
            mainScrollPanel.SuspendLayout();
            contentLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainScrollPanel
            // 
            mainScrollPanel.AutoScroll = true;
            mainScrollPanel.Controls.Add(contentLayout);
            mainScrollPanel.Dock = DockStyle.Fill;
            mainScrollPanel.Location = new Point(0, 0);
            mainScrollPanel.Name = "mainScrollPanel";
            mainScrollPanel.Size = new Size(1100, 800);
            mainScrollPanel.TabIndex = 0;
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 1;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contentLayout.Controls.Add(lblHotRightNow, 0, 0);
            contentLayout.Controls.Add(hotRightNowLayout, 0, 1);
            contentLayout.Controls.Add(lblAllTimeGreatest, 0, 2);
            contentLayout.Controls.Add(allTimeGreatestLayout, 0, 3);
            contentLayout.Controls.Add(lblVibesPicks, 0, 4);
            contentLayout.Controls.Add(vibesPicksLayout, 0, 5);
            contentLayout.Dock = DockStyle.Top;
            contentLayout.Location = new Point(0, 0);
            contentLayout.Name = "contentLayout";
            contentLayout.Padding = new Padding(24);
            contentLayout.RowCount = 6;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));
            contentLayout.Size = new Size(1083, 890);
            contentLayout.TabIndex = 0;
            // 
            // lblHotRightNow
            // 
            lblHotRightNow.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHotRightNow.ForeColor = Color.White;
            lblHotRightNow.Location = new Point(27, 24);
            lblHotRightNow.Name = "lblHotRightNow";
            lblHotRightNow.Size = new Size(216, 40);
            lblHotRightNow.TabIndex = 0;
            lblHotRightNow.Text = "Hot right now";
            lblHotRightNow.TextAlign = ContentAlignment.BottomLeft;
            // 
            // hotRightNowLayout
            // 
            hotRightNowLayout.AutoScroll = true;
            hotRightNowLayout.Dock = DockStyle.Fill;
            hotRightNowLayout.Location = new Point(24, 72);
            hotRightNowLayout.Margin = new Padding(0, 8, 0, 0);
            hotRightNowLayout.Name = "hotRightNowLayout";
            hotRightNowLayout.Size = new Size(1035, 232);
            hotRightNowLayout.TabIndex = 1;
            hotRightNowLayout.WrapContents = false;
            // 
            // lblAllTimeGreatest
            // 
            lblAllTimeGreatest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAllTimeGreatest.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblAllTimeGreatest.ForeColor = Color.White;
            lblAllTimeGreatest.Location = new Point(27, 314);
            lblAllTimeGreatest.Name = "lblAllTimeGreatest";
            lblAllTimeGreatest.Size = new Size(216, 40);
            lblAllTimeGreatest.TabIndex = 2;
            lblAllTimeGreatest.Text = "All-Time Greatest";
            lblAllTimeGreatest.TextAlign = ContentAlignment.BottomLeft;
            // 
            // allTimeGreatestLayout
            // 
            allTimeGreatestLayout.AutoScroll = true;
            allTimeGreatestLayout.Dock = DockStyle.Fill;
            allTimeGreatestLayout.Location = new Point(24, 362);
            allTimeGreatestLayout.Margin = new Padding(0, 8, 0, 0);
            allTimeGreatestLayout.Name = "allTimeGreatestLayout";
            allTimeGreatestLayout.Size = new Size(1035, 232);
            allTimeGreatestLayout.TabIndex = 3;
            allTimeGreatestLayout.WrapContents = false;
            // 
            // lblVibesPicks
            // 
            lblVibesPicks.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVibesPicks.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblVibesPicks.ForeColor = Color.White;
            lblVibesPicks.Location = new Point(27, 604);
            lblVibesPicks.Name = "lblVibesPicks";
            lblVibesPicks.Size = new Size(216, 40);
            lblVibesPicks.TabIndex = 4;
            lblVibesPicks.Text = "Vibes Picks";
            lblVibesPicks.TextAlign = ContentAlignment.BottomLeft;
            // 
            // vibesPicksLayout
            // 
            vibesPicksLayout.AutoScroll = true;
            vibesPicksLayout.Dock = DockStyle.Fill;
            vibesPicksLayout.Location = new Point(24, 652);
            vibesPicksLayout.Margin = new Padding(0, 8, 0, 0);
            vibesPicksLayout.Name = "vibesPicksLayout";
            vibesPicksLayout.Size = new Size(1035, 232);
            vibesPicksLayout.TabIndex = 5;
            vibesPicksLayout.WrapContents = false;
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            Controls.Add(mainScrollPanel);
            Name = "HomeControl";
            Size = new Size(1100, 800);
            mainScrollPanel.ResumeLayout(false);
            contentLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel mainScrollPanel;
        private TableLayoutPanel contentLayout;
        private Label lblHotRightNow;
        private FlowLayoutPanel hotRightNowLayout;
        private Label lblAllTimeGreatest;
        private FlowLayoutPanel allTimeGreatestLayout;
        private Label lblVibesPicks;
        private FlowLayoutPanel vibesPicksLayout;
    }
}