using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    class MyTextPrinter : IDrawable
    {
        private SpriteBatch sb;
        private GraphicsDevice gd;

        private Vector2 pos;
        private SpriteFont font;
        private bool remove = false;
        private string text;

        public MyTextPrinter(SpriteBatch sb,GraphicsDevice gd, Vector2 pos, SpriteFont font, string text)
        {
            this.sb = sb;
            this.gd = gd;
            this.pos = pos;
            this.font = font;
            this.text = text;
        }
        public void setText(string text)
        {
            this.text = text;
        }
        public void onDraw()
        {
            sb.Begin();
            sb.DrawString(font, text, pos, Color.White);
            sb.End();
        }

        public void onUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }

        public bool removeRequest()
        {
            return remove;
        }
    }
}
