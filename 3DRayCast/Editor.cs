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
        Rectangle mouseRect;
        int wallWidth;
        int wallHeight;
        Map map;
        Player player;
        int _width = 600;
        int _height = 600;
        public Editor(Map map, Player player)
        {
            this.map = map;
            this.player = player;
            InitializeComponent();

            wallWidth = _width / map.Size.Width;
            wallHeight = _height / map.Size.Height;
            t = new Timer();
            t.Interval = 10;
            t.Tick += new EventHandler(T_loop);
            t.Start();
        }

        private void T_loop(object sender, EventArgs e)
        {
            g.DrawImage(bmp, new Point(0, 0));
            _screenG.Clear(Color.White);

            mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(5, 5));

            _screenG.FillEllipse(Brushes.Blue, mouseRect);
            //_screenG.FillRectangle(Brushes.Black, new RectangleF((float)((player.Position.X + player.Direction.X) * wallWidth), (float)((player.Position.Y + player.Direction.Y) * wallWidth), 10f, 10f));

            _screenG.DrawLine(Pens.Black, new PointF((float)((player.Position.X + player.Direction.X) * wallWidth), (float)((player.Position.Y + player.Direction.Y) * wallWidth)), new PointF((float)(player.Position.X * wallWidth), (float)(player.Position.Y * wallHeight)));

            _screenG.FillRectangle(Brushes.Green, new RectangleF((float)(player.Position.X * wallWidth) - 5, (float)(player.Position.Y * wallHeight) - 5, 10f, 10f));

            for (int i = 0; i < map.Size.Width; i++)
            {
                for (int j = 0; j < map.Size.Height; j++)
                {
                    _screenG.DrawRectangle(Pens.Blue, new Rectangle(i * wallWidth, j * wallHeight, wallWidth, wallHeight));
                    if(map[i,j].IsCollider)
                    {
                        _screenG.FillRectangle(Brushes.IndianRed, new Rectangle(i * wallWidth, j * wallHeight, wallWidth, wallHeight));
                    }
                    if (mouseRect.IntersectsWith(new Rectangle(i * wallWidth, j * wallHeight, wallWidth, wallHeight)))
                    {
                        _screenG.FillRectangle(new SolidBrush(Color.FromArgb(100,Color.Red.R, Color.Red.G,Color.Red.B)), new Rectangle(i * wallWidth, j * wallHeight, wallWidth, wallHeight));
                    }
                }
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.Width, this.Height);
            g = this.CreateGraphics();
            _screenG = Graphics.FromImage(bmp);

            map[5, 5].IsCollider = true;

            
        }

        private void Editor_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                for (int i = 0; i < map.Size.Width; i++)
                {
                    for (int j = 0; j < map.Size.Height; j++)
                    {
                        if(mouseRect.IntersectsWith(new Rectangle(i * wallWidth, j * wallHeight, wallWidth, wallHeight)))
                        {
                            map[i, j].IsCollider = true;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var saveBox = new SaveFileDialog();
            saveBox.Filter = "Fichier Level Map(*.lvl)|*.lvl";
            if (saveBox.ShowDialog() == DialogResult.OK)
            {
                this.map.Save(saveBox.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var loadBox = new OpenFileDialog();
            loadBox.Filter = "Fichier Level Map(*.lvl)|*.lvl";
            if (loadBox.ShowDialog() == DialogResult.OK)
            {
                map.Load(loadBox.FileName);
            }
        }
    }
}
