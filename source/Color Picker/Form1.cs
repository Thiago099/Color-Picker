using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Color_Picker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedToolWindow;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
        }
        int r { get => trackBar1.Value; set => trackBar1.Value = value > 0 ? value < 255 ? value : 255 : 0; }
        int g { get => trackBar2.Value; set => trackBar2.Value = value > 0 ? value < 255 ? value : 255 : 0; }
        int b { get => trackBar3.Value; set => trackBar3.Value = value > 0 ? value < 255 ? value : 255 : 0; }
        int l { get => trackBar4.Value; set => trackBar4.Value = value; }
        int rg_center { get => trackBar5.Value; set => trackBar5.Value = value; }
        int rb_center { get => trackBar6.Value; set => trackBar6.Value = value; }
        int gb_center { get => trackBar7.Value; set => trackBar7.Value = value; }
        // int sat { get => int.Parse(textBox2.Text); set => textBox2.Text = value.ToString(); }
        Color color { get => panel1.BackColor; set=>panel1.BackColor=value; }


     
        bool controler;
        private void RGB_Value_Changed(object sender, EventArgs e)
        {
            if (updating) return;
            color = Color.FromArgb(r, g, b);
            textBox1.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            if (controler)
            {
                l = (r + g + b)/3;
                rr = r - l;
                rg = g - l;
                rb = b - l;
            }
            if (rg_con)
            {
                rg_center = (r + g) / 2;
                rg_rr = r - rg_center;
                rg_rg = g - rg_center;
            }
            if (rb_con)
            {
                rb_center = (r + b) / 2;
                rb_rr = r - rb_center;
                rb_rb = b - rb_center;
            }
            if (gb_con)
            {
                gb_center = (g + b) / 2;
                gb_rg = g - gb_center;
                gb_rb = b - gb_center;
            }
            update_text();
        }
        void update_text()
        {
            textBox2.Text = r.ToString();
            textBox3.Text = g.ToString();
            textBox4.Text = b.ToString();
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;

            int[] c = { r, g, b };
            char[] name = { 'R', 'G', 'B' };
            int[] scale;

            { 
                  if(r>g)
                  if(g>b)  scale = new int[] { 0, 1, 2 };// >>
            else  if(r>b)  scale = new int[] { 0, 2, 1 };// <>
            else           scale = new int[] { 2, 0, 1 };
            else  if(b>g)  scale = new int[] { 2, 1, 0 };// >>
            else  if(r>b)  scale = new int[] { 1, 0, 2 };// ><
            else           scale = new int[] { 1, 2, 0 };
            }

            var bg = c[scale[0]];
            var md = c[scale[1]];
            var sm = c[scale[2]];
            var mdf = bg - md;
            var smf = bg - sm;
            
            textBox5.Text = $"{name[scale[0]]}, {name[scale[1]]}, {name[scale[2]]}";
            textBox6.Text =$"md:{mdf}, sm:{smf}";
            textBox7.Text = (bg + md + sm).ToString();

        }
        int i = 0;
        bool updating=false;
        void calculate_center()
        {
            l = (r + g + b) / 3;
            rr = r - l;
            rg = g - l;
            rb = b - l;
            
            rg_center = (r + b) / 2;
            rg_rr = r - rg_center;
            rg_rg = g - rg_center;

            rb_center = (r + b) / 2;
            rb_rr = r - rb_center;
            rb_rb = b - rb_center;

            gb_center = (g + b) / 2;
            gb_rg = g - gb_center;
            gb_rb = b - gb_center;
        }
        bool rg_con = false;
        bool rb_con = false;
        bool gb_con = false;
        int rg_rr;
        int rg_rg;
        int rb_rr;
        int rb_rb;
        int gb_rg;
        int gb_rb;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                try
                {
                    color = (Color)new ColorConverter().ConvertFromString(textBox1.Text);
                    updating = true;
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    updating = false;
                    
                    calculate_center();
                    update_text();
                    textBox1.BackColor = Color.White;
                    textBox1.ForeColor = Color.Black;
                }
                catch
                {
                    textBox1.BackColor = Color.Red;
                    textBox1.ForeColor = Color.White;
                }
            }
        }
        int rr, rg, rb;

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            controler = true;
            rg_con = true;
            rb_con = true;
            gb_con = true;
        }
        private void trackBar4_MouseDown(object sender, MouseEventArgs e)
        {
            controler = false;
            rg_con = true;
            rb_con = true;
            gb_con = true;
            
        }
        private void trackBar5_MouseDown(object sender, MouseEventArgs e)
        {
            controler = true;
            rg_con = false;
            rb_con = true;
            gb_con = true;
        }
        private void trackBar6_MouseDown(object sender, MouseEventArgs e)
        {
            controler = true;
            rg_con = true;
            rb_con = false;
            gb_con = true;
        }

        private void trackBar7_MouseDown(object sender, MouseEventArgs e)
        {
            controler = true;
            rg_con = true;
            rb_con = true;
            gb_con = false;
        }


        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var t = g;
            g = b;
            b = t;
            rg = g - l;
            rb = b - l;




            update_text();

        }

    



        private void button4_Click(object sender, EventArgs e)
        {
            r = 255 - r;
            g = 255 - g;
            b = 255 - b;
            rr = r - l;
            rb = b - l;
            rg = g - l;
            l = (r + g + b )/ 3;
            update_text();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = g;
            g = b;
            b = t;
            rg = g - l;
            rb = b - l;

            update_text();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var t = r;
            r = g;
            g = t;
            rr = r - l;
            rg = g - l;



            update_text();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var t = r;
            r = b;
            b = t;
            rr = r - l;
            rb = b - l;



            update_text();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    r = int.Parse(textBox2.Text);
                   
                    calculate_center();
                    update_text();
                    textBox2.BackColor = Color.White;
                    textBox2.ForeColor = Color.Black;
                }
                catch
                {
                    textBox2.BackColor = Color.Red;
                    textBox2.ForeColor = Color.White;
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    g = int.Parse(textBox3.Text);
                    
                    calculate_center();
                    update_text();
                    textBox3.BackColor = Color.White;
                    textBox3.ForeColor = Color.Black;
                }
                catch
                {
                    textBox3.BackColor = Color.Red;
                    textBox3.ForeColor = Color.White;
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    b = int.Parse(textBox4.Text);
                    
                    calculate_center();
                    update_text();
                    textBox4.BackColor = Color.White;
                    textBox4.ForeColor = Color.Black;
                }
                catch
                {
                    textBox4.BackColor = Color.Red;
                    textBox4.ForeColor = Color.White;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var avg = (r + g)/2;
            r = g = avg;
            calculate_center();

            update_text();
            rg_center = avg;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var avg = (b + g) / 2;
            b = g = avg;
            calculate_center();
            update_text();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var avg = (b + r) / 2;
            b = r = avg;
            calculate_center();
            update_text();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            if (!rg_con)
            {
                
                r = rg_center + rg_rr;
                g = rg_center + rg_rg;
            }   
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            if (!rb_con)
            {
                r = rb_center + rb_rr;
                b = rb_center + rb_rb;
            }
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            if (!gb_con)
            {
                b = gb_center + gb_rb;
                g = gb_center + gb_rg;
            }
        }
        private void trackBar5_MouseEnter(object sender, EventArgs e)
        {

            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel3.Left -= 1;
            panel5.Left -= 1;

            panel2.Top -= 1;
            panel3.Top -= 1;
            panel5.Top -= 1;

        }

        private void trackBar5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BorderStyle = BorderStyle.None;
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel3.Left += 1;
            panel5.Left += 1;

            panel2.Top += 1;
            panel3.Top += 1;
            panel5.Top += 1;
        }



        private void trackBar4_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel3.Left -= 1;
            panel4.Left -= 1;
            panel9.Left -= 1;
            panel5.Left -= 1;
            panel6.Left -= 1;
            panel7.Left -= 1;

            panel2.Top -= 1;
            panel3.Top -= 1;
            panel4.Top -= 1;
            panel9.Top -= 1;
            panel5.Top -= 1;
            panel6.Top -= 1;
            panel7.Top -= 1;

        }

        private void trackBar4_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel9.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel3.Left += 1;
            panel4.Left += 1;
            panel9.Left += 1;
            panel5.Left += 1;
            panel6.Left += 1;
            panel7.Left += 1;

            panel2.Top += 1;
            panel3.Top += 1;
            panel4.Top += 1;
            panel9.Top += 1;
            panel5.Top += 1;
            panel6.Top += 1;
            panel7.Top += 1;

        }

        private void trackBar6_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel4.Left -= 1;
            panel6.Left -= 1;

            panel2.Top -= 1;
            panel4.Top -= 1;
            panel6.Top -= 1;
        }

        private void trackBar6_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel4.Left += 1;
            panel6.Left += 1;

            panel2.Top += 1;
            panel4.Top += 1;
            panel6.Top += 1;
        }

        private void trackBar7_MouseEnter(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;

            panel3.Left -= 1;
            panel4.Left -= 1;
            panel7.Left -= 1;

            panel3.Top -= 1;
            panel4.Top -= 1;
            panel7.Top -= 1;

        }

        private void trackBar7_MouseLeave(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;

            panel3.Left += 1;
            panel4.Left += 1;
            panel7.Left += 1;

            panel3.Top += 1;
            panel4.Top += 1;
            panel7.Top += 1;

        }
        private void trackBar1_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel9.Left -= 1;
            panel5.Left -= 1;
            panel6.Left -= 1;

            panel2.Top -= 1;
            panel9.Top -= 1;
            panel5.Top -= 1;
            panel6.Top -= 1;
        }

        private void trackBar1_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel9.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel9.Left += 1;
            panel5.Left += 1;
            panel6.Left += 1;

            panel2.Top += 1;
            panel9.Top += 1;
            panel5.Top += 1;
            panel6.Top += 1;
        }

        private void trackBar2_MouseEnter(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;

            panel3.Left -= 1;
            panel9.Left -= 1;
            panel7.Left -= 1;
            panel5.Left -= 1;

            panel3.Top -= 1;
            panel9.Top -= 1;
            panel7.Top -= 1;
            panel5.Top -= 1;

        }

        private void trackBar2_MouseLeave(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.None;
            panel9.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;

            panel3.Left += 1;
            panel9.Left += 1;
            panel7.Left += 1;
            panel5.Left += 1;

            panel3.Top += 1;
            panel9.Top += 1;
            panel7.Top += 1;
            panel5.Top += 1;
        }

        private void trackBar3_MouseEnter(object sender, EventArgs e)
        {
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;

            panel4.Left -=1;
            panel9.Left -= 1;
            panel6.Left -= 1;
            panel7.Left -= 1;

            panel4.Top -= 1;
            panel9.Top -= 1;
            panel6.Top -= 1;
            panel7.Top -= 1;
        }

        private void trackBar3_MouseLeave(object sender, EventArgs e)
        {
            panel4.BorderStyle = BorderStyle.None;
            panel9.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;

            panel4.Left += 1;
            panel9.Left += 1;
            panel6.Left += 1;
            panel7.Left += 1;

            panel4.Top += 1;
            panel9.Top += 1;
            panel6.Top += 1;
            panel7.Top += 1;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel3.Left -= 1;

            panel2.Top -= 1;
            panel3.Top -= 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel3.Left += 1;

            panel2.Top += 1;
            panel3.Top += 1;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel4.Left -= 1;

            panel2.Top -= 1;
            panel4.Top -= 1;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel4.Left += 1;

            panel2.Top -= 1;
            panel4.Top -= 1;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;

            panel3.Left -= 1;
            panel4.Left -= 1;

            panel3.Top -= 1;
            panel4.Top -= 1;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;

            panel3.Left += 1;
            panel4.Left += 1;

            panel3.Top += 1;
            panel4.Top += 1;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel3.Left -= 1;
            panel5.Left -= 1;

            panel2.Top -= 1;
            panel3.Top -= 1;
            panel5.Top -= 1;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel3.Left += 1;
            panel5.Left += 1;

            panel2.Top += 1;
            panel3.Top += 1;
            panel5.Top += 1;
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel4.Left -= 1;
            panel6.Left -= 1;

            panel2.Top -= 1;
            panel4.Top -= 1;
            panel6.Top -= 1;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {

            panel2.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel4.Left += 1;
            panel6.Left += 1;

            panel2.Top += 1;
            panel4.Top += 1;
            panel6.Top += 1;
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;

            panel3.Left -= 1;
            panel4.Left -= 1;
            panel7.Left -= 1;

            panel3.Top -= 1;
            panel4.Top -= 1;
            panel7.Top -= 1;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;

            panel3.Left += 1;
            panel4.Left += 1;
            panel7.Left += 1;

            panel3.Top += 1;
            panel4.Top += 1;
            panel7.Top += 1;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;

            panel2.Left -= 1;
            panel3.Left -= 1;
            panel4.Left -= 1;
            panel5.Left -= 1;
            panel7.Left -= 1;
            panel6.Left -= 1;

            panel2.Top -= 1;
            panel3.Top -= 1;
            panel4.Top -= 1;
            panel5.Top -= 1;
            panel7.Top -= 1;
            panel6.Top -= 1;

        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            panel4.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;
            panel7.BorderStyle = BorderStyle.None;
            panel6.BorderStyle = BorderStyle.None;

            panel2.Left += 1;
            panel3.Left += 1;
            panel4.Left += 1;
            panel5.Left += 1;
            panel7.Left += 1;
            panel6.Left += 1;

            panel2.Top += 1;
            panel3.Top += 1;
            panel4.Top += 1;
            panel5.Top += 1;
            panel7.Top += 1;
            panel6.Top += 1;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            r = b = g = l;
            rr = rb = rg = 0;
            update_text();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (!controler)
            {
                r = l + rr;
                g = l + rg;
                b = l + rb;
            }
            update_text();
        }


    }
}
/*
 * rgb - hex Kate Orlova
 * https://stackoverflow.com/questions/13354892/converting-from-rgb-ints-to-hex/13354940
 * hex - rgb Thorarin
 * https://stackoverflow.com/questions/2109756/how-do-i-get-the-color-from-a-hexadecimal-color-code-using-net
 */
