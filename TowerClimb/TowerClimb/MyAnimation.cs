using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    class MyAnimation : IDrawable
    {
        private SpriteBatch sb;
        private Texture2D t;
        private Rectangle objBox;
        private GraphicsDevice gd;
        private Vector2 pos;
        private Vector2 vel;

        private bool done = false;
        private int frameWidth, frameHoldTime;
        private int milliSecondsCount = 0;
        private int gtOld = 0;
        private int offset = 0;
        public MyAnimation(SpriteBatch sb, Texture2D t, Rectangle box, GraphicsDevice gd, Vector2 pos, Vector2 vel,int frameWidth, int frameHoldTime)
        {
            this.sb = sb;
            this.t = t;
            this.objBox = box;
            this.gd = gd;
            this.vel = vel;
            this.pos = pos;
            this.frameWidth = frameWidth;
            this.frameHoldTime = frameHoldTime;
        }
        public void onDraw()
        {
            sb.Begin();
            Rectangle textureSource = new Rectangle(offset, 0, objBox.Width, objBox.Height);
            sb.Draw(t, objBox, textureSource, Color.White);
            sb.End();
        }

        public void onUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            int gt = gameTime.ElapsedGameTime.Milliseconds;
            if (gtOld==0)
            {
                gtOld=gt;
            }
            else
            {
                int change = gt - gtOld;
                milliSecondsCount += (-1)*change;
                if (milliSecondsCount%frameHoldTime==0)
                {
                    offset += frameWidth;
                }
            }

            this.pos += vel;
            this.objBox.X = (int)pos.X;
            this.objBox.Y = (int)pos.Y;
        }

        public bool removeRequest()
        {
            return done;
        }
    }
}
