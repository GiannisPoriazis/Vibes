using System.Drawing.Drawing2D;
using FontAwesome.Sharp;

namespace Vibes.Design
{
    public class CircularIconButton : IconButton
    {
        public CircularIconButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(45, 45); 
            this.Cursor = Cursors.Hand;

            this.Text = string.Empty;
            this.ImageAlign = ContentAlignment.MiddleCenter;
            this.TextImageRelation = TextImageRelation.ImageAboveText;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            int size = Math.Min(this.Width, this.Height);
            if (this.Width != size || this.Height != size)
            {
                this.Size = new Size(size, size);
            }

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, size - 1, size - 1);

                this.Region = new Region(path);
            }
        }
    }
}