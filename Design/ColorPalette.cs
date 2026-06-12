using System.Drawing;

namespace Vibes.Design
{
    public static class ColorPalette
    {
        // Application Window
        public static readonly Color ApplicationBorder = Color.Red;

        // Form & Card Backgrounds
        public static readonly Color Background = Color.FromArgb(18, 18, 24);
        public static readonly Color CardBackground = Color.FromArgb(28, 28, 36);

        // Gradients / Accents
        public static readonly Color AccentPurple = Color.FromArgb(255, 80, 180, 255);
        public static readonly Color AccentPink = Color.FromArgb(255, 180, 80, 255);

        // Borders & Dividers
        public static readonly Color BorderMuted = Color.FromArgb(45, 45, 58);

        // Typography
        public static readonly Color TextMain = Color.FromArgb(255, 255, 255);
        public static readonly Color TextMuted = Color.FromArgb(160, 160, 175);

        //ContextMenu
        public static readonly Color ContextMenuBackground = Color.FromArgb(240, 245, 244);
        public static readonly Color ContextMenuForeColor = Color.Black;
    }
}
