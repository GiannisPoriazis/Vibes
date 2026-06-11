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
            SuspendLayout();
            // 
            // applicationLayout
            // 
            applicationLayout.ColumnCount = 3;
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.1052628F));
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.7894745F));
            applicationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.1052628F));
            applicationLayout.Dock = DockStyle.Fill;
            applicationLayout.Location = new Point(0, 0);
            applicationLayout.Name = "applicationLayout";
            applicationLayout.RowCount = 1;
            applicationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            applicationLayout.Size = new Size(798, 482);
            applicationLayout.TabIndex = 0;
            // 
            // ApplicationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(applicationLayout);
            Name = "ApplicationControl";
            Size = new Size(798, 482);
            ResumeLayout(false);
        }

        #endregion

        public TableLayoutPanel applicationLayout;
    }
}
