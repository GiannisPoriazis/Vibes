public class ClickOutsideMessageFilter : IMessageFilter
{
    private readonly Control _targetControl;
    private readonly Control _excludeControl;
    private readonly Action _onClickOutside;

    public ClickOutsideMessageFilter(Control targetControl, Control excludeControl, Action onClickOutside)
    {
        _targetControl = targetControl; 
        _excludeControl = excludeControl; 
        _onClickOutside = onClickOutside; 
    }

    public bool PreFilterMessage(ref Message m)
    {
        if (m.Msg == 0x0201 || m.Msg == 0x0204)
        {
            if (!_targetControl.Visible) return false;

            Point mouseScreenPoint = Cursor.Position;

            bool clickedInTarget = _targetControl.RectangleToScreen(_targetControl.ClientRectangle).Contains(mouseScreenPoint);
            bool clickedInExclude = _excludeControl.RectangleToScreen(_excludeControl.ClientRectangle).Contains(mouseScreenPoint);

            if (!clickedInTarget && !clickedInExclude)
            {
                _onClickOutside.Invoke();
            }
        }
        return false;
    }
}