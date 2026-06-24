using Vibes.Database;
using Vibes.Design;
using Vibes.Extensions;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class MediaDisplayControl : UserControl
    {
        private readonly IPlaybackQueueManagerService _queueService;
        private readonly IPlaylistService _playlistService;
        private Panel headerColorPanel = null!;
        private TableLayoutPanel masterLayout;
        private ListView trackListView = null!;
        private Label lblMediaTitle = null!;
        private Label lblMetaDetails = null!;
        private PictureBox pbCoverArt = null!;

        public IEntity<int> Entity;

        public MediaDisplayControl(IPlaybackQueueManagerService queueService, IPlaylistService playlistService)
        {
            _queueService = queueService;
            _playlistService = playlistService;
            InitializeComponent();
            BuildLayoutContainer();
        }

        private void BuildLayoutContainer()
        {
            masterLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.FromArgb(15, 15, 15),
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            masterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            masterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            masterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            masterLayout.BorderStyle = BorderStyle.None;

            Controls.Add(masterLayout);

            headerColorPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 20, 25),
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            pbCoverArt = new PictureBox { Location = new Point(30, 30), Size = new Size(160, 160), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent };
            lblMediaTitle = new Label { Location = new Point(210, 80), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 36, FontStyle.Bold), BackColor = Color.Transparent };
            lblMetaDetails = new Label { Location = new Point(210, 155), AutoSize = true, ForeColor = Color.FromArgb(200, 200, 200), Font = new Font("Segoe UI", 10, FontStyle.Regular), BackColor = Color.Transparent };

            headerColorPanel.Controls.Add(pbCoverArt);
            headerColorPanel.Controls.Add(lblMediaTitle);
            headerColorPanel.Controls.Add(lblMetaDetails);

            masterLayout.Controls.Add(headerColorPanel, 0, 0);

            trackListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                BackColor = Color.FromArgb(15, 15, 15),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                OwnerDraw = true,
                Margin = new Padding(0)
            };

            var colNum = new ColumnHeader { Text = "#", Width = 50 };
            var colTitle = new ColumnHeader { Text = "Title", Width = 250 };
            var colAlbum = new ColumnHeader { Text = "Album", Width = 150 };
            var colType = new ColumnHeader { Text = "Type", Width = 100 };
            var colDuration = new ColumnHeader { Text = "Duration", Width = 100 };
            var colOptions = new ColumnHeader { Text = "", Width = 40 };
            trackListView.Columns.AddRange(new ColumnHeader[] { colNum, colTitle, colAlbum, colType, colDuration, colOptions });

            trackListView.SetRowItemHeight(56);

            trackListView.DrawColumnHeader += TrackListView_DrawColumnHeader;
            trackListView.DrawSubItem += TrackListView_DrawSubItem;
            trackListView.Resize += TrackListView_Resize;
            trackListView.ColumnWidthChanging += TrackListView_ColumnWidthChanging;
            trackListView.MouseClick += TrackListView_MouseClick;
            trackListView.EnableRowHoverStyles();

            masterLayout.Controls.Add(trackListView, 0, 1);
        }

        private void TrackListView_ColumnWidthChanging(object? sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = trackListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void TrackListView_Resize(object? sender, EventArgs e)
        {
            double indexWidth = 0.05;
            double albumWidth = 0.35;
            double typeWidth = 0.10;
            double durationWidth = 0.10;
            double optionsWidth = 0.05;

            int totalWidth = trackListView.ClientSize.Width - 4;
            if (totalWidth < 300) return;

            trackListView.BeginUpdate();
            trackListView.Columns[0].Width = (int)(totalWidth * indexWidth);
            trackListView.Columns[2].Width = (int)(totalWidth * albumWidth);
            trackListView.Columns[3].Width = (int)(totalWidth * typeWidth);
            trackListView.Columns[4].Width = (int)(totalWidth * durationWidth);
            trackListView.Columns[5].Width = (int)(totalWidth * optionsWidth);

            int workingWidth = trackListView.ClientSize.Width;
            int fixedColumnsWidth = trackListView.Columns[0].Width + trackListView.Columns[2].Width + trackListView.Columns[3].Width + trackListView.Columns[4].Width + trackListView.Columns[5].Width;

            trackListView.Columns[1].Width = Math.Max(200, workingWidth - fixedColumnsWidth);
            trackListView.EndUpdate();
        }

        public void AutoplayTracks()
        {
            if (Entity is Playlist playlist) _queueService.PlayPlaylist(playlist.Tracks);
            else if (Entity is Track track) _queueService.PlaySearchTrackNow(track);
        }

        public void RenderContentContext(string mainHeaderTitle, string metadataString, IEnumerable<Track> tracksToLoad)
        {
            lblMediaTitle.Text = mainHeaderTitle;
            lblMetaDetails.Text = metadataString;
            trackListView.Items.Clear();

            if (Entity is Playlist playlist)
            {
                pbCoverArt.Image = playlist.CachedCover;
                Color derivedAccent = ExtractDominantColor(playlist.CachedCover!);
                headerColorPanel.BackColor = derivedAccent;
                masterLayout.BackColor = derivedAccent;
            }
            else if (Entity is Track track)
            {
                pbCoverArt.Image = track.CachedCover;
                Color derivedAccent = ExtractDominantColor(track.CachedCover!);
                headerColorPanel.BackColor = derivedAccent;
                masterLayout.BackColor = derivedAccent;
            }

            if (tracksToLoad == null || !tracksToLoad.Any()) return;

            for (int i = 0; i < tracksToLoad.Count(); i++)
            {
                var currentTrack = tracksToLoad.ElementAt(i);
                ListViewItem rowItem = new ListViewItem((i + 1).ToString()) { Tag = currentTrack };
                rowItem.SubItems.Add(currentTrack.Title);
                rowItem.SubItems.Add(currentTrack.Album);
                rowItem.SubItems.Add(currentTrack.Type.ToString());
                rowItem.SubItems.Add(currentTrack.FormattedDuration);
                rowItem.SubItems.Add(string.Empty);

                trackListView.Items.Add(rowItem);
            }

            int workingWidth = trackListView.ClientSize.Width;
            int fixedColumnsWidth = trackListView.Columns[0].Width + trackListView.Columns[2].Width + trackListView.Columns[3].Width + trackListView.Columns[4].Width + trackListView.Columns[5].Width;
            trackListView.Columns[1].Width = Math.Max(200, workingWidth - fixedColumnsWidth);
        }

        private void Play_List(object? sender, EventArgs e)
        {
            if (trackListView.SelectedItems.Count == 0) return;

            var selectedRow = trackListView.SelectedItems[0];
            int startingIndex = selectedRow.Index;

            var fullContextList = new List<Track>();
            foreach (ListViewItem item in trackListView.Items)
            {
                fullContextList.Add((Track)item.Tag!);
            }

            _queueService.PlayPlaylist(fullContextList, startingIndex);
        }

        private void TrackListView_MouseClick(object? sender, MouseEventArgs e)
        {
            var hitTest = trackListView.HitTest(e.Location);
            if (hitTest.Item == null || hitTest.Item.SubItems.Count <= 5) return;

            var track = (Track)hitTest.Item.Tag!;
            var ghostCellBounds = hitTest.Item.SubItems[5].Bounds;

            if (e.X >= ghostCellBounds.Left && e.X <= ghostCellBounds.Right)
            {
                bool isPlaylistView = (Entity is Playlist);
                ShowTrackContextMenu(track, e.Location, isPlaylistView);
            }
        }
    }
}