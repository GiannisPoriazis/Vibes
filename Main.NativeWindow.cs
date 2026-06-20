using Vibes.Design;

namespace Vibes
{
    public partial class Vibes
    {
        private void TriggerNativeDrag()
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HT_CAPTION, 0);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= NativeMethods.WS_MINIMIZEBOX |
                             NativeMethods.WS_MAXIMIZEBOX |
                             NativeMethods.WS_THICKFRAME |
                             NativeMethods.WS_SYSMENU;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_NCACTIVATE)
            {
                m.Result = (IntPtr)1;
                return;
            }

            if (m.Msg == NativeMethods.WM_NCCALCSIZE)
            {
                if (m.WParam == (IntPtr)1)
                {
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            if (m.Msg == NativeMethods.WM_WINDOWPOSCHANGED)
            {
                base.WndProc(ref m);

                int currentStyle = (int)NativeMethods.GetWindowLong(this.Handle, NativeMethods.GWL_STYLE);
                if ((currentStyle & NativeMethods.WS_THICKFRAME) == 0)
                {
                    NativeMethods.SetWindowLong(this.Handle, NativeMethods.GWL_STYLE, (IntPtr)(currentStyle | NativeMethods.WS_THICKFRAME));
                    NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0,
                        NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOZORDER | NativeMethods.SWP_FRAMECHANGED);
                }
                return;
            }

            base.WndProc(ref m);
        }
    }
}