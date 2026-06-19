namespace Vibes.Extensions
{
    public static class ListViewExtensions
    {
        public static void EnableRowHoverStyles(this ListView listView)
        {
            int hoveredIndex = -1;

            listView.MouseMove += (s, e) =>
            {
                var hitTest = listView.HitTest(e.Location);
                int currentHoverIndex = hitTest.Item != null ? hitTest.Item.Index : -1;

                listView.Cursor = currentHoverIndex != -1 ? Cursors.Hand : Cursors.Default;

                if (currentHoverIndex != hoveredIndex)
                {
                    int oldHoverIndex = hoveredIndex;
                    hoveredIndex = currentHoverIndex;

                    listView.BeginUpdate();

                    if (oldHoverIndex >= 0 && oldHoverIndex < listView.Items.Count)
                    {
                        listView.RedrawItems(oldHoverIndex, oldHoverIndex, false);
                    }

                    if (hoveredIndex >= 0 && hoveredIndex < listView.Items.Count)
                    {
                        listView.RedrawItems(hoveredIndex, hoveredIndex, false);
                    }

                    listView.EndUpdate();
                }
            };

            listView.MouseLeave += (s, e) =>
            {
                listView.Cursor = Cursors.Default;
                if (hoveredIndex != -1)
                {
                    int oldIndex = hoveredIndex;
                    hoveredIndex = -1;

                    if (oldIndex >= 0 && oldIndex < listView.Items.Count)
                    {
                        listView.RedrawItems(oldIndex, oldIndex, false);
                    }
                }
            };
        }

        public static bool IsRowHovered(this ListView listView, int itemIndex)
        {
            Point clientMousePos = listView.PointToClient(Control.MousePosition);
            var hitTest = listView.HitTest(clientMousePos);
            return hitTest.Item != null && hitTest.Item.Index == itemIndex;
        }

        public static void SetRowItemHeight(this ListView listView, int targetHeight)
        {
            ImageList heightSpacerContainer = new ImageList
            {
                ImageSize = new Size(1, targetHeight), 
                ColorDepth = ColorDepth.Depth32Bit
            };
            listView.SmallImageList = heightSpacerContainer;
        }
    }
}