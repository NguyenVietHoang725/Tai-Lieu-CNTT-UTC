using System;
using System.Drawing;

namespace LibraryManagerApp.Helpers
{
    public class StatusRequestEventArgs : EventArgs
    {
        public string TitleText { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public StatusRequestEventArgs(string text, Color back, Color fore)
        {
            TitleText = text;
            BackColor = back;
            ForeColor = fore;
        }
    }
}
