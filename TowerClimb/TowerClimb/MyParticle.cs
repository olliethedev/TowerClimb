using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    class MyParticle:IDrawable
    {
        private SpriteBatch sb;
        private Texture2D t;
        private Rectangle objBox;
        private GraphicsDevice gd;
        private Vector2 pos;
        private Vector2 vel;

        public MyParticle(SpriteBatch sb, Texture2D t, Rectangle box, GraphicsDevice gd, Vector2 pos,Vector2 vel)
        {
            this.sb = sb;
            this.t = t;
            this.objBox = box;
            this.gd = gd;
            this.vel = vel;
            this.pos = pos;
        }
        public void onDraw()
        {
            sb.Begin();
            Rectangle textureSource = new Rectangle(0, 0, t.Width, t.Height);
            sb.Draw(t, objBox, textureSource, Color.White);
            sb.End();
        }

        public void onUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.pos += vel;
            this.objBox.X = (int)pos.X;
            this.objBox.Y = (int)pos.Y;
        }
        public bool removeRequest()
        {
            bool outside = false;
            Rectangle border = new Rectangle(0, 0, gd.Viewport.Width, gd.Viewport.Height);
            if (objBox.Right > border.Right)
            {
                outside = true;
            }
            if (objBox.Left < border.Left)
            {
                outside = true;
            }
            if (objBox.Bottom > border.Bottom)
            {
                outside = true;
            }
            if (objBox.Top < border.Top)
            {
                outside = true;
            }
            if (outside)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
