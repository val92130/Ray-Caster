using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DRayCast
{
    public class ViewPlane
    {
        Vector2 position;
        public ViewPlane(double x, double y)
        {
            this.position = new Vector2(x, y);
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public void Rotate(EDirection direction, Player player)
        {
            switch (direction)
            {
                case EDirection.Left:
                    this.Position.X = this.Position.X * Math.Cos(player.RotationSpeed) - this.Position.Y * Math.Sin(player.RotationSpeed);
                    this.Position.Y = this.Position.X * Math.Sin(player.RotationSpeed) + this.Position.Y * Math.Cos(player.RotationSpeed);
                    break;
                case EDirection.Right:
                    this.Position.X = this.Position.X * Math.Cos(-player.RotationSpeed) - this.Position.Y * Math.Sin(-player.RotationSpeed);
                    this.Position.Y = this.Position.X * Math.Sin(-player.RotationSpeed) + this.Position.Y * Math.Cos(-player.RotationSpeed);
                    break;
            }
        }
    }
}
