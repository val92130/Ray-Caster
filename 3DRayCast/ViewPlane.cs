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
    }
}
