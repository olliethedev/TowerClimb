using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    interface ICollidable
    {
        void keepInside(Rectangle border);
        void keepOutside(ICollidable col);
        Vector2 getVelocity();
        Rectangle getBindBox();
    }
}
