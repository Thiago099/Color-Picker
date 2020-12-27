using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Color_Picker
{
    class Panel2:Panel
    {
        Color Border_color = SystemColors.Control;

        public Color border_color
        {
            get => Border_color;
            set 
            {
                Border_color = value;
                Refresh();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Border_color, ButtonBorderStyle.Solid);
        }
    }
    
}
