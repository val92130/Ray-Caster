using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DRayCast
{
    public class Wall
    {
        bool _collision;
        Vector2 _position;
        Color _color;
        public Wall(bool collision, Vector2 position, Color color)
        {
            this._collision = collision;
            this._position = position;
            this._color = color;
        }

        public bool IsCollider
        {
            get
            {
                return _collision;
            }
            set
            {
                _collision = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }
        }

        public Color Color
        {
            get
            {
                return this._color;
            }
        }
    }
}
