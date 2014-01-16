using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerClimb
{
    class MyCharacter:IDrawable,ICollidable,ICreator
    {
        private SpriteBatch sb;
        private Texture2D t;
        private Texture2D explosionTex;
        private Rectangle objBox;
        private GraphicsDevice gd;
        private float speed = 10;
        private Vector2 pos;
        private Vector2 vel;
        float bounce = 0.4f;
        float refect = -1;
        private List<IDrawable> createdDrawables = new List<IDrawable>();
        private List<ICollidable> createdColidables = new List<ICollidable>();
        private float health = 100;
        private MyTextPrinter tp;
        IScreenManager screen;
        
        public MyCharacter(SpriteBatch sb, Texture2D t, Rectangle box, GraphicsDevice gd, Texture2D explosionTex, MyTextPrinter tp,IScreenManager screen)
        {
            this.sb = sb;
            this.t = t;
            this.objBox = box;
            this.gd = gd;
            this.explosionTex = explosionTex;
            this.pos = new Vector2(0,gd.Viewport.Height-box.Height);
            this.vel = new Vector2(0,0);
            this.tp = tp;
            this.screen = screen;
        }
        public void onUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            Random r = new Random();
            if (ks.IsKeyDown(Keys.Up))
            {
                vel.Y -= speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
                createdDrawables.Add(new MyParticle(sb, t, new Rectangle(0, 0, 10, 10), gd, new Vector2((pos.X + objBox.Width/2), pos.Y+objBox.Height), new Vector2(r.Next(20)-15, 5)));
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                vel.Y += speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
                createdDrawables.Add(new MyParticle(sb, t, new Rectangle(0, 0, 10, 10), gd, new Vector2(pos.X + objBox.Width / 2, pos.Y), new Vector2(r.Next(20) - 15, -5)));
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                vel.X -= speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
                createdDrawables.Add(new MyParticle(sb, t, new Rectangle(0, 0, 10, 10), gd, new Vector2(pos.X + objBox.Width, pos.Y + objBox.Height / 2), new Vector2(5, r.Next(20) - 15)));
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                vel.X += speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
                createdDrawables.Add(new MyParticle(sb, t, new Rectangle(0, 0, 10, 10), gd, new Vector2(pos.X, pos.Y + objBox.Height / 2), new Vector2(-5, r.Next(20) - 15)));
            }
            if (ks.IsKeyDown(Keys.Space))
            {
                
                
            }
            pos += vel;
            
            this.objBox.X = (int)pos.X;
            this.objBox.Y = (int)pos.Y;
        }

        public void onDraw()
        {
            sb.Begin();
            Rectangle textureSource = new Rectangle(0, 0, t.Width, t.Height);
            sb.Draw(t, objBox, textureSource, Color.White);
            sb.End();
        }

        public void keepInside(Rectangle border)
        {
            if (objBox.Right>border.Right)
            {
                vel.X *= refect;
                vel.X *= bounce;
                objBox.X = border.Width - objBox.Width;
            }
            if (objBox.Left<border.Left)
            {
                vel.X *= refect;
                vel.X *= bounce;
                objBox.X = border.X ;
            }
            if (objBox.Bottom > border.Bottom)
            {
                vel.Y *= refect;
                vel.Y *= bounce;
                objBox.Y = border.Height - objBox.Height;
            }
            if (objBox.Top < border.Top)
            {
                vel.Y *= refect;
                vel.Y *= bounce;
                objBox.Y = border.Y;
            }
            pos.X = objBox.X;
            pos.Y = objBox.Y;
        }

        public void keepOutside(ICollidable col)
        {
            bool touching = false;
            Rectangle border = col.getBindBox();
            
            Vector2 tempVel = this.vel;
            if (objBox.Intersects(border)&&objBox.X<border.Right&&objBox.X>border.Left)
            {
                vel.X *= refect;
                vel.X *= bounce;
                objBox.X = border.Right;
                touching = true;
            }
            if (objBox.Intersects(border) && objBox.Right > border.Left && objBox.Right < border.Right)
            {
                vel.X *= refect;
                vel.X *= bounce;
                objBox.X = border.Left-objBox.Width;
                touching = true;
            }

            if (objBox.Intersects(border) && objBox.Y < border.Bottom && objBox.Y > border.Top)
            {
                vel.Y *= refect;
                vel.Y *= bounce;
                objBox.Y = border.Bottom;
                touching = true;
            }
            if (objBox.Intersects(border) && objBox.Bottom > border.Top && objBox.Bottom < border.Bottom)
            {
                vel.Y *= refect;
                vel.Y *= bounce;
                objBox.Y = border.Top - objBox.Height;
                touching = true;
            }
            pos.X = objBox.X;
            pos.Y = objBox.Y;
            if (touching)
            {
                createdDrawables.Add(new MyAnimation(sb, explosionTex, new Rectangle(0, 0, 100, 100), gd, pos, new Vector2(0, 1), 100, 1000));
                health -= 0.2f;
                if(health>0)
                {
                    tp.setText("Health: " + health.ToString("#.##") + "%");
                }
                else
                {
                    screen.showMenu();
                }
                
            }
        }
        public Vector2 getVelocity()
        {
            return this.vel;
        }
        public Rectangle getBindBox()
        {
            return this.objBox;
        }

        public List<IDrawable> getNewDrawables()
        {
            List<IDrawable> result = createdDrawables;
            createdDrawables = new List<IDrawable>();
            return result;
        }

        public List<ICollidable> getNewCollidables()
        {
            List<ICollidable>result = createdColidables;
            createdColidables = new List<ICollidable>();
            return result;
        }
        public bool removeRequest()
        {
            return false;
        }
    }
}
