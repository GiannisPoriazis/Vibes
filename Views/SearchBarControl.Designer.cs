namespace Vibes.Views
{
    partial class SearchBarControl
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

        private TextBox searchTextBox = null!;
        private FontAwesome.Sharp.IconButton searchButton = null!;
        private ListView searchResultsView = null!;
        private AudioStreamingService streamingService = new AudioStreamingService();
        private ListViewItem? _hoveredItem = null;

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            searchTextBox = new TextBox();
            searchButton = new FontAwesome.Sharp.IconButton();
            searchResultsView = new ListView();
            searchBarLayout = new TableLayoutPanel();
            searchBarLayout.SuspendLayout();
            SuspendLayout();
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchTextBox.BackColor = Color.FromArgb(30, 30, 35);
            searchTextBox.Font = new Font("Segoe UI", 10F);
            searchTextBox.ForeColor = Color.White;
            searchTextBox.Location = new Point(538, 3);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(300, 25);
            searchTextBox.TabIndex = 0;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;
            // 
            // searchButton
            // 
            searchButton.BackColor = Color.FromArgb(50, 50, 60);
            searchButton.Cursor = Cursors.Hand;
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.ForeColor = Color.White;
            searchButton.IconChar = FontAwesome.Sharp.IconChar.None;
            searchButton.IconColor = Color.Black;
            searchButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            searchButton.Location = new Point(844, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(80, 30);
            searchButton.TabIndex = 1;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = false;
            searchButton.Click += SearchButton_Click;
            // 
            // searchResultsView
            // 
            searchResultsView = new ListView();
            searchResultsView.View = View.Details;
            searchResultsView.FullRowSelect = true;
            searchResultsView.Visible = false;
            searchResultsView.Size = new Size(400, 310);
            searchResultsView.BackColor = Color.FromArgb(20, 20, 20);
            searchResultsView.BorderStyle = BorderStyle.None;
            searchResultsView.HeaderStyle = ColumnHeaderStyle.None;
            searchResultsView.Columns.Add("MainData", searchResultsView.ClientSize.Width);

            var heightSpacer = new ImageList { ImageSize = new Size(48, 48) };
            searchResultsView.SmallImageList = heightSpacer;
            searchResultsView.OwnerDraw = true;
            searchResultsView.DrawColumnHeader += (s, e) => e.DrawDefault = false; 
            searchResultsView.DrawItem += SearchResultsView_DrawItem; 
            searchResultsView.DrawSubItem += SearchResultsView_DrawSubItem;   
            searchResultsView.Click += SearchResultsView_Click;
            searchResultsView.MouseMove += SearchResultsView_MouseMove;
            searchResultsView.MouseLeave += SearchResultsView_MouseLeave;
            searchResultsView.SizeChanged += (s, e) => {
                if (searchResultsView.Columns.Count > 0)
                {
                    searchResultsView.Columns[0].Width = searchResultsView.ClientSize.Width;
                }
            };
            // 
            // searchBarLayout
            // 
            searchBarLayout.ColumnCount = 2;
            searchBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            searchBarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            searchBarLayout.Controls.Add(searchTextBox, 0, 0);
            searchBarLayout.Controls.Add(searchButton, 1, 0);
            searchBarLayout.Dock = DockStyle.Fill;
            searchBarLayout.Location = new Point(0, 0);
            searchBarLayout.Name = "searchBarLayout";
            searchBarLayout.RowCount = 1;
            searchBarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchBarLayout.Size = new Size(941, 466);
            searchBarLayout.TabIndex = 3;
            // 
            // SearchBarControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(searchBarLayout);
            Form topLevelForm = this.FindForm() ?? (Form)Application.OpenForms[0]!;
            topLevelForm.Controls.Add(searchResultsView);
            Name = "SearchBarControl";
            Size = new Size(941, 466);
            searchBarLayout.ResumeLayout(false);
            searchBarLayout.PerformLayout();
            ResumeLayout(false);

            var clickFilter = new ClickOutsideMessageFilter(
                searchResultsView,
                searchTextBox,
                () => searchResultsView.Visible = false 
            );

            Application.AddMessageFilter(clickFilter);
            Disposed += (s, e) => Application.RemoveMessageFilter(clickFilter);
        }

        #endregion

        private TableLayoutPanel searchBarLayout;
    }
}
