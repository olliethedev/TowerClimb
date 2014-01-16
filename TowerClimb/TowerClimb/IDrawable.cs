using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    interface IDrawable
    {
        void onDraw();
        void onUpdate(GameTime gameTime);
        bool removeRequest();
    }
}
