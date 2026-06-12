using System.Data;
using Vibes.Design;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class ApplicationControl : UserControl
    {
        private readonly IAuth0Service? _authService;

        public ApplicationControl()
        {
            InitializeComponent();
            HandleCreated += (s, e) => LoadPlaylists();
            SetupContextMenu();
        }

        public ApplicationControl(IAuth0Service? auth0Service) : this()
        {
            _authService = auth0Service;
        }

        private void LoadPlaylists()
        {
            if (_authService == null || _authService.CurrentUser == null)
            {
                return;
            }

            using (var context = new Database.VibesDbContext())
            {
                var playlists = context.Playlists
                               .Where(p => p.UserId == _authService.CurrentUser.Subject)
                               .ToList();

                playlistView.Items.Clear();
                playlistView.Columns.Clear();
                playlistView.Columns.Add("Playlist Name", playlistView.Width - 4);

                foreach (var playlist in playlists)
                {
                    ListViewItem item = new ListViewItem(playlist.Name);
                    item.Tag = playlist;

                    playlistView.Items.Add(item);
                }
            }
        }

        private void SetupContextMenu()
        {
            playlistContextMenu = new ContextMenuStrip();
            playlistContextMenu.BackColor = ColorPalette.CardBackground;
            playlistContextMenu.ForeColor = Color.White;
            playlistContextMenu.RenderMode = ToolStripRenderMode.Professional;

            ToolStripMenuItem playItem = new ToolStripMenuItem("Play");
            ToolStripMenuItem renameItem = new ToolStripMenuItem("Rename");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");

            playItem.Click += MenuPlay_Click;
            renameItem.Click += MenuRename_Click;
            deleteItem.Click += MenuDelete_Click;

            playlistContextMenu.Items.AddRange(new ToolStripItem[] { playItem, renameItem, deleteItem });
            playlistView.ContextMenuStrip = playlistContextMenu;
            playlistView.MouseDown += PlaylistView_MouseDown;
        }

        private void PlaylistView_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = playlistView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    playlistView.SelectedItems.Clear();
                    item.Selected = true; 
                }
                else
                {
                    playlistView.ContextMenuStrip?.Hide();
                }
            }
        }

        private void MenuPlay_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;
            Playlist selectedPlaylist = (Playlist)playlistView.SelectedItems[0].Tag;

            MessageBox.Show($"Playing playlist tracks for: {selectedPlaylist.Name}");
        }

        private void MenuRename_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = playlistView.SelectedItems[0];
            Playlist selectedPlaylist = (Playlist)selectedItem.Tag;

            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new name:", "Rename Playlist", selectedPlaylist.Name).Trim();

            if (string.IsNullOrEmpty(newName) || newName == selectedPlaylist.Name) return;

            using (var context = new Database.VibesDbContext())
            {
                context.Playlists.Attach(selectedPlaylist);
                selectedPlaylist.Name = newName;
                context.SaveChanges();
            }

            selectedItem.Text = newName;
        }

        private void MenuDelete_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = playlistView.SelectedItems[0];
            Playlist selectedPlaylist = (Playlist)selectedItem.Tag;

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete '{selectedPlaylist.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (var context = new Database.VibesDbContext())
                {
                    context.Playlists.Remove(selectedPlaylist);
                    context.SaveChanges();
                }

                playlistView.Items.Remove(selectedItem);
            }
        }

        private void AddPlaylistBtn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_authService?.CurrentUser?.Subject)) return;

            string playlistName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new playlist name:",
                "Create Playlist",
                "New Playlist"
            ).Trim();

            if (string.IsNullOrEmpty(playlistName)) return;

            using (var context = new Database.VibesDbContext())
            {
                var newPlaylist = new Playlist { Name = playlistName, UserId = _authService.CurrentUser.Subject };

                context.Playlists.Add(newPlaylist);
                context.SaveChanges();
            }

            LoadPlaylists();
        }

        private void playlistView_Resize(object sender, EventArgs e)
        {
            if (playlistView.Columns.Count > 0)
            {
                playlistView.Columns[0].Width = playlistView.Width - 4;
            }
        }
    }
}
