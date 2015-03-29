using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DRayCast
{
    public partial class Editor : Form
    {
        Graphics g, _screenG;
        Bitmap bmp;
        Timer t;

        Map map;
        public Editor(Map map)
        {
            this.map = map;
            InitializeComponent();

            t = new Timer();
            t.Interval = 10;
            t.Tick += new EventHandler(T_loop);
            t.Start();
        }

        private void T_loop(object sender, EventArgs e)
        {
            g.DrawImage(bmp, new Point(0, 0));
            _screenG.Clear(Color.White);

            _screenG.DrawRectangle(Pens.Red, new Rectangle(0, 0, 50, 50));
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.Width, this.Height);
            g = this.CreateGraphics();
            _screenG = Graphics.FromImage(bmp);

            for(int i = 0; i < map.Size.Width; i++)
            {

            }
        }
    }
}
