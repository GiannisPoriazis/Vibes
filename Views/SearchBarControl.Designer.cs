namespace Vibes.Views
{
    partial class SearchBarControl
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

        private Panel searchContainerPanel = null!;
        private TextBox searchTextBox = null!;
        private ListView searchResultsView = null!;

        private void InitializeComponent()
        {
            searchContainerPanel = new Panel();
            searchTextBox = new TextBox();
            searchResultsView = new ListView();
            SuspendLayout();
            // 
            // searchContainerPanel
            // 
            searchContainerPanel.BackColor = Color.Transparent;
            searchContainerPanel.Controls.Add(searchTextBox);
            searchContainerPanel.Location = new Point(5, 2);
            searchContainerPanel.Name = "searchContainerPanel";
            searchContainerPanel.Size = new Size(350, 40);
            searchContainerPanel.TabIndex = 0;
            searchContainerPanel.Paint += SearchContainerPanel_Paint;
            // 
            // searchTextBox
            // 
            searchTextBox.BackColor = Color.FromArgb(36, 36, 36);
            searchTextBox.BorderStyle = BorderStyle.None; // Removes boxy borders
            searchTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            searchTextBox.ForeColor = Color.White;
            searchTextBox.Location = new Point(36, 10); // Offset to clear the vector search icon
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(300, 20);
            searchTextBox.TabIndex = 0;
            searchTextBox.Text = "";
            searchTextBox.PlaceholderText = "What do you want to play?";
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;
            // 
            // searchResultsView
            // 
            searchResultsView.View = View.Details;
            searchResultsView.FullRowSelect = true;
            searchResultsView.Visible = false;
            searchResultsView.Size = new Size(360, 310);
            searchResultsView.BackColor = Color.FromArgb(20, 20, 20);
            searchResultsView.BorderStyle = BorderStyle.None;
            searchResultsView.HeaderStyle = ColumnHeaderStyle.None;
            searchResultsView.OwnerDraw = true;
            searchResultsView.Click += SearchResultsView_Click;
            searchResultsView.DrawItem += SearchResultsView_DrawItem;
            searchResultsView.DrawSubItem += SearchResultsView_DrawSubItem;
            searchResultsView.SizeChanged += (s, e) => {
                if (searchResultsView.Columns.Count > 0)
                    searchResultsView.Columns[0].Width = searchResultsView.ClientSize.Width;
            };
            // 
            // SearchBarControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(searchContainerPanel);
            Name = "SearchBarControl";
            Size = new Size(600, 60);
            ResumeLayout(false);
        }

        #endregion
    }
}