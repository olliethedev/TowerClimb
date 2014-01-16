using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    class MyPlatform : IDrawable,ILinkableChild,ICollidable
    {
        private SpriteBatch sb;
        private Texture2D t;
        private Rectangle objBox;
        private GraphicsDevice gd;
        private Vector2 pos;
        private Vector2 offset;

        public MyPlatform(SpriteBatch sb, Texture2D t, Rectangle box, GraphicsDevice gd, Vector2 offset)
        {
            this.sb = sb;
            this.t = t;
            this.objBox = box;
            this.gd = gd;
            this.offset = offset;
            pos=new Vector2(box.X,box.Y);
        }
        

        public Microsoft.Xna.Framework.Vector2 getVelocity()
        {
            return new Vector2(0,0);
        }

        public Microsoft.Xna.Framework.Rectangle getBindBox()
        {
            return this.objBox;
        }

        public void onParentUpdate(ILinkableParent per)
        {
            Vector2 posFromParent = per.getParentPosChange();
            this.pos = posFromParent + this.offset; ;
            this.objBox.X = (int)pos.X;
            this.objBox.Y = (int)pos.Y;

            if (objBox.Y + objBox.Height > gd.Viewport.Height)
            {
                objBox.Y -= gd.Viewport.Height;
                pos.X = objBox.X;
            }
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
            
        }
        public void keepInside(Microsoft.Xna.Framework.Rectangle border)
        {

        }

        public void keepOutside(ICollidable col)
        {

        }
        public bool removeRequest()
        {
            return false;
        }
    }
}
