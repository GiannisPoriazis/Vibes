namespace Vibes.Design
{
    public class ContextMenuThemeRenderer : ToolStripProfessionalRenderer
    {
        public ContextMenuThemeRenderer() : base(new ContextMenuColorTable()) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.FromArgb(230, 230, 230);
            base.OnRenderItemText(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                base.OnRenderMenuItemBackground(e);
                return;
            }

            var g = e.Graphics;
            var bounds = new Rectangle(Point.Empty, e.Item.Size);

            using (var hoverBrush = new SolidBrush(Color.FromArgb(45, 45, 45)))
            {
                g.FillRectangle(hoverBrush, bounds);
            }
        }
    }

    public class ContextMenuColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.FromArgb(28, 28, 28);
        public override Color MenuBorder => Color.FromArgb(45, 45, 45);

        public override Color MenuItemSelected => Color.FromArgb(45, 45, 45);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(45, 45, 45);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(45, 45, 45);
    }
}
