using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DRayCast
{
    public class Player
    {
        Vector2 position, direction, velocity;
        double speed, rotationSpeed;
        bool isMoving;
        const float maxSpeed = 1;
        double _maxVisibleDistance = 30;
        Map map;
        public Player(double x, double y, double dirX, double dirY, Map map)
        {
            this.position = new Vector2(x, y);
            this.direction = new Vector2(dirX, dirY);
            this.speed = 0.1;
            this.rotationSpeed = 0.04;
            this.map = map;
            this.velocity = new Vector2(0, 0);
        }

        public double MaxVisibleDistance
        {
            get
            {
                return _maxVisibleDistance;
            }
        }

        public double Speed
        {
            get
            {
                return speed;
            }
        }

        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
        }
        public double RotationSpeed
        {
            get
            {
                return rotationSpeed;
            }
        }
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public void Rotate(EDirection direction)
        {
            switch(direction)
            {
                case EDirection.Left:
                    this.Direction.X = this.Direction.X * Math.Cos(this.RotationSpeed) - this.Direction.Y * Math.Sin(this.RotationSpeed);
                    this.Direction.Y = this.Direction.X * Math.Sin(this.RotationSpeed) + this.Direction.Y * Math.Cos(this.RotationSpeed);
                    break;
                case EDirection.Right:
                    this.Direction.X = this.Direction.X * Math.Cos(-this.RotationSpeed) - this.Direction.Y * Math.Sin(-this.RotationSpeed);
                    this.Direction.Y = this.Direction.X * Math.Sin(-this.RotationSpeed) + this.Direction.Y * Math.Cos(-this.RotationSpeed);
                    break;
            }
        }

        public void Update()
        {

            this.Position.X += this.velocity.X * this.Direction.X;
            this.Position.Y += this.velocity.Y * this.Direction.Y;

            this.velocity.Y = Vector2.Lerp(0, (float)this.velocity.Y, 0.05f);
            this.velocity.X = Vector2.Lerp(0, (float)this.velocity.X, 0.05f);
        }

        public void Move(EDirection direction)
        {
            switch(direction)
            {
                case EDirection.Forward :
                    
                    this.velocity.X +=  this.Speed;
                    this.velocity.Y +=  this.Speed;
                    if (this.velocity.X > maxSpeed)
                    {
                        this.velocity.X = maxSpeed;
                    }
                    if (this.velocity.Y > maxSpeed)
                    {
                        this.velocity.Y = maxSpeed;
                    }
                    break;
                case EDirection.BackWard:
                    this.velocity.X -= this.Speed;
                    this.velocity.Y -= this.Speed;
                    if (this.velocity.X < -maxSpeed)
                    {
                        this.velocity.X = -maxSpeed;
                    }
                    if (this.velocity.Y < -maxSpeed)
                    {
                        this.velocity.Y = -maxSpeed;
                    }
                    break;
            }
        }
    }
}
