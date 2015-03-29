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
        double _distance;
        double _maxVisibleDistance = 30;
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
                int r = _color.R;
                int g = _color.G;
                int b = _color.B;

                if (this.Distance > _maxVisibleDistance)
                {
                    return Color.FromArgb(r / (int)(this.Distance), g/(int)(this.Distance), b/(int)(this.Distance)) ;
                }

                return Color.FromArgb(255, _color.R, _color.G, _color.B);
            }
        }

        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
            }
        }
    }
}
