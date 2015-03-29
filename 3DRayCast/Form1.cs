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
    public partial class Form1 : Form
    {
        Graphics g, _screenG;
        Bitmap bmp;
        Timer t;

        

        // Fov of the viewPlane is  * atan(0.66/1.0) = 66° 
        ViewPlane viewPlane = new ViewPlane(0, 0.66);
        Map map ;
        Player player;
        bool forward, backward, left, right;

        Editor editor;


        public Form1()
        {
            

            map = new Map(36, 36, 1);
            player = new Player(22, 12, -1, 0, map);
            InitializeComponent();

            editor = new Editor(map);
            editor.Show();

            t = new Timer();
            t.Interval = 10;
            t.Tick += new EventHandler(T_loop);
            t.Start();

            
        }

        private void T_loop(object sender, EventArgs e)
        {
            g.DrawImage(bmp, new Point(0, 0));
            _screenG.Clear(Color.White);
            player.Update();
            for (int x = 0; x < this.Width; x++)
            {
                double w = this.Width;
                double cameraX = 2 * x / (double)w - 1;
                double rayPosX = player.Position.X;
                double rayPosY = player.Position.Y;
                double rayDirX = player.Direction.X + viewPlane.Position.X * cameraX;
                double rayDirY = player.Direction.Y + viewPlane.Position.Y * cameraX;

                //which box of the map we're in  
                int mapX = (int)rayPosX;
                int mapY = (int)rayPosY;


                //length of ray from current position to next x or y-side
                double sideDistX;
                double sideDistY;

                //length of ray from one x or y-side to next x or y-side
                double deltaDistX = Math.Sqrt(1 + (rayDirY * rayDirY) / (rayDirX * rayDirX));
                double deltaDistY = Math.Sqrt(1 + (rayDirX * rayDirX) / (rayDirY * rayDirY));
                double perpWallDist;

                //what direction to step in x or y-direction (either +1 or -1)
                int stepX;
                int stepY;


                int hit = 0; //was there a wall hit?
                int side = 0; //was a NS or a EW wall hit?
                //calculate step and initial sideDist
                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (rayPosX - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - rayPosX) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (rayPosY - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - rayPosY) * deltaDistY;
                }

                while (hit == 0)
                {
                    //jump to next map square, OR in x-direction, OR in y-direction
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }
                    //Check if ray has hit a wall
                    if (map[mapX, mapY] != null)
                    {
                        if (map[mapX, mapY].IsCollider) hit = 1;
                    }
                    
                }
                //Calculate distance projected on camera direction (oblique distance will give fisheye effect!)
                if (side == 0)
                    perpWallDist = Math.Abs((mapX - rayPosX + (1 - stepX) / 2) / rayDirX);
                else
                    perpWallDist = Math.Abs((mapY - rayPosY + (1 - stepY) / 2) / rayDirY);

                map[mapX, mapY].Distance = perpWallDist;

                //Calculate height of line to draw on screen
                int lineHeight = Math.Abs((int)(this.Height / perpWallDist));

                //calculate lowest and highest pixel to fill in current stripe
                int drawStart = -lineHeight / 2 + this.Height / 2;
                if (drawStart < 0) drawStart = 0;
                int drawEnd = lineHeight / 2 + this.Height / 2;
                if (drawEnd >= this.Height) drawEnd = this.Height - 1;

                // wall color
                Color color = map[mapX, mapY].Color;

                // if the wall is on the side, we shade it
                if (side == 1) {
                    color = Color.FromArgb(150,color.R / 2, color.G, color.B);
                }

                _screenG.DrawLine(Pens.Gray, new Point(x, 0), new Point(x, drawEnd)); // draw ceil
                _screenG.DrawLine(new Pen(color), new Point(x, drawStart), new Point(x, drawEnd));
                _screenG.DrawLine(Pens.DarkGray, new Point(x, this.Height), new Point(x, drawEnd)); // draw floor
            }

            if (forward)
            {
                player.Move(EDirection.Forward);
            }
            else if (backward)
            {
                player.Move(EDirection.BackWard);
            } 

            if(left)
            {
                player.Rotate(EDirection.Left);
                viewPlane.Rotate(EDirection.Left, player);

            } else if(right)
            {
                player.Rotate(EDirection.Right);
                viewPlane.Rotate(EDirection.Right, player);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.Width, this.Height);
            g = this.CreateGraphics();
            _screenG = Graphics.FromImage(bmp);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    forward = true;
                    backward = false;
                    break;
                case Keys.S:
                    backward = true;
                    forward = false;
                    break;
                case Keys.D:
                    right = true;
                    left = false;
                    break;
                case Keys.Q:
                    right = false;
                    left = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Z:
                    forward = false;
                    backward = false;
                    break;
                case Keys.S:
                    backward = false;
                    forward = false;
                    break;
                case Keys.D:
                    right = false;
                    left = false;
                    break;
                case Keys.Q:
                    right = false;
                    left = false;
                    break;
            }

                    
        }
    }
}
