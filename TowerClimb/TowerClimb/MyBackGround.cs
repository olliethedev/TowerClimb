using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    class MyBackGround:IDrawable,ILinkableParent
    {
        private SpriteBatch sb;
        private Texture2D t;
        private Rectangle box;
        private GraphicsDevice gd;
        private List<ILinkableChild> children = new List<ILinkableChild>();
        private float yPos = 0;
        private float speed=80;
        public MyBackGround(SpriteBatch sb,Texture2D t,Rectangle box, GraphicsDevice gd)
        {
            this.sb=sb;
            this.t = t;
            this.box = box;
            this.gd = gd;
        }
        public void onUpdate(GameTime gameTime)
        {
            if (yPos <= gd.Viewport.Height)
            {
                yPos += speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            }
            else
            {
                yPos = 0;
            }            
        }
        public void onDraw()
        {
            sb.Begin();
            Rectangle offsetBox = new Rectangle(0, (int)yPos, gd.Viewport.Width, gd.Viewport.Height);
            Rectangle offsetBox2 = new Rectangle(0, (int)yPos - gd.Viewport.Height, gd.Viewport.Width, gd.Viewport.Height);
            Rectangle textureSource = new Rectangle(0, 0, t.Width, t.Height);
            sb.Draw(t, offsetBox, textureSource, Color.White);
            sb.Draw(t, offsetBox2, textureSource, Color.White);
            sb.End();
        }

        public void addChild(ILinkableChild cha)
        {
            this.children.Add(cha);
        }

        public void onLinkableUpdate()
        {
            foreach (ILinkableChild ch in children)
            {
                ch.onParentUpdate(this);
            }
        }
        public Vector2 getParentPosChange()
        {
            return new Vector2(0, yPos);
        }
        public bool removeRequest()
        {
            return false;
        }
    }
}
