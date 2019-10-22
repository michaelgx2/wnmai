using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WnMAiModule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var avatar in Tester.Hostiles)
            {
                avatar.Update();
            }
            foreach (var avatar in Tester.Allies)
            {
                avatar.Update();
            }
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            gp.Clear(Color.Green);
            foreach (var avatar in Tester.Hostiles)
            {
                DrawAvatar(gp, avatar);
            }
            foreach (var avatar in Tester.Allies)
            {
                DrawAvatar(gp, avatar);
            }
        }

        private void DrawAvatar(Graphics gp, TestAvatar avatar)
        {
            Brush pen = new SolidBrush(avatar.Color);
            Brush br = new SolidBrush(Color.White);
            Font font = new Font(FontFamily.GenericSansSerif, 10);
            Font talk = new Font(FontFamily.GenericSansSerif, 9);
            gp.FillEllipse(pen, avatar.X, avatar.Y, 16, 16);
            gp.DrawString(avatar.Position.ToString(), font, br, avatar.X+3, avatar.Y+1);
            gp.DrawString(avatar.Talking, talk, br, avatar.X - 12, avatar.Y - 12);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tester.TimeStep = timer1.Interval / 1000f;
            Tester.InitTesters();
            timer1.Enabled = true;
        }
    }
}
