namespace Vibes.Interfaces
{
    public interface IAvatarService
    {
        Task LoadAvatarIntoAsync(PictureBox avatarIcon, string? avatarUrl);
        void UpdateAvatarRegion(PictureBox avatarIcon);
    }
}
