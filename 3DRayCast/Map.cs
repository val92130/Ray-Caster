using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DRayCast
{
    class Map
    {
        Wall[,] _walls;
        int _mapWidth, _mapHeight;
        int _wallSize;
        public Map(int width, int height, int wallWidth)
        {
            _walls = new Wall[width, height];
            this._mapWidth = width;
            this._mapHeight = height;
            this._wallSize = wallWidth;

            for (int i = 0; i < this._mapWidth; i++)
            {
                for (int j = 0; j < this._mapHeight ; j++)
                {
                    _walls[i, j] = new Wall(false, new Vector2(i * wallWidth, j * wallWidth), Color.Purple);
                    if (j == 0 || j == _mapWidth / wallWidth - 1)
                    {
                        _walls[i, j].IsCollider = true;
                    }
                    else
                    {
                        if (i == 0 || i == _mapWidth / wallWidth - 1)
                        {
                            _walls[i, j].IsCollider = true;
                        }
                    }
                }
            }

         Debug.Print("Map size : " + _walls.Length.ToString());
        }

        public Wall this[int x, int y]
        {
            get
            {
                if( x >= Math.Sqrt(_walls.Length) || y >= Math.Sqrt(_walls.Length))
                {
                    return null;
                }
                if(_walls[x,y] != null)
                {
                    return _walls[x, y];
                }
                else
                {
                    return null;
                }
                
            }
        }

        public Wall[,] Walls
        {
            get
            {
                return _walls;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(this._mapWidth , this._mapHeight );
            }
        }

        public int WallSize
        {
            get
            {
                return _wallSize;
            }
        }
    }
}
