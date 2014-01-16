using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerClimb
{
    public class GameLoop : Microsoft.Xna.Framework.Game,IScreenManager
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<IDrawable> drawables;
        List<ICollidable> collidables;
        List<ILinkableParent> linkParents;
        List<ICreator> creators;
        bool menuScreen =true;
        SpriteFont font;
        MyTextPrinter menuPrinter;
        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            drawables=new List<IDrawable>();
            collidables = new List<ICollidable>();
            linkParents = new List<ILinkableParent>();
            creators = new List<ICreator>();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            font =Content.Load<SpriteFont>("Courier New");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menuPrinter = new MyTextPrinter(spriteBatch, GraphicsDevice, new Vector2(0, GraphicsDevice.Viewport.Height / 2), font, "Controls: ArrowKeys to move. \nObjectives: Avoid the floating rocks. \nTo start new game press Enter, to exit press Esc");
        }
        protected override void UnloadContent()
        {
        }
        private void loadGameObjects()
        {
            drawables = new List<IDrawable>();
            collidables = new List<ICollidable>();
            linkParents = new List<ILinkableParent>();
            creators = new List<ICreator>();
            
            String health = new String("Health:".ToCharArray());
            SpriteFont font = Content.Load<SpriteFont>("Courier New");
            MyTextPrinter textPrinter = new MyTextPrinter(spriteBatch, GraphicsDevice, new Vector2(0, 0), font, health);


            Texture2D bgTex = Content.Load<Texture2D>("bggame2d");
            Rectangle bgRect = new Rectangle(0,0,GraphicsDevice.Viewport.Width,GraphicsDevice.Viewport.Height);
            MyBackGround bgObj = new MyBackGround(spriteBatch, bgTex, bgRect, GraphicsDevice);

            Rectangle chRect = new Rectangle(0,0,100,100);
            Texture2D shipTex = Content.Load<Texture2D>("ship");
            Texture2D explosionTex = Content.Load<Texture2D>("explosion");
            MyCharacter mainCharacter = new MyCharacter(spriteBatch, shipTex, chRect, GraphicsDevice, explosionTex, textPrinter,this);

            List<MyPlatform> platforms = new List<MyPlatform>();
            Texture2D rockTex = Content.Load<Texture2D>("rock");
            Texture2D rockHitTex = Content.Load<Texture2D>("rock2");
            int scrW = GraphicsDevice.Viewport.Width;
            int scrH = GraphicsDevice.Viewport.Height;
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int pW = r.Next(25)+25;
                int pH = r.Next(25)+25;

                int pY = r.Next(scrH);
                int pX = r.Next(scrW - pW);
                
                Rectangle platRect = new Rectangle(pX, pY, pW, pH);
                MyPlatform platform = new MyPlatform(spriteBatch, rockTex, platRect, GraphicsDevice, new Vector2(pX, pY));
                platforms.Add(platform);
            }
            drawables.Add(bgObj);
            drawables.Add(mainCharacter);
            drawables.Add(textPrinter);
            collidables.Add(mainCharacter);

            creators.Add(mainCharacter);
            

            linkParents.Add(bgObj);
            foreach(MyPlatform p in platforms)
            {
                bgObj.addChild(p);
                drawables.Add(p);
                collidables.Add(p);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            if (menuScreen)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    this.menuScreen = false;
                    loadGameObjects();
                }
            }
            else
            {
                foreach (ICreator c in creators)
                {
                    List<IDrawable> newDrawables = c.getNewDrawables();
                    List<ICollidable> newColidables = c.getNewCollidables();
                    foreach (IDrawable newD in newDrawables)
                    {
                        this.drawables.Add(newD);
                    }
                    foreach (ICollidable newC in newColidables)
                    {
                        this.collidables.Add(newC);
                    }
                }
                foreach (IDrawable d in drawables.ToList())
                {

                    if (d.removeRequest())
                    {
                        drawables.Remove(d);
                    }
                    d.onUpdate(gameTime);
                }
                foreach (ICollidable d in collidables)
                {
                    d.keepInside(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
                    foreach (ICollidable e in collidables)
                    {
                        if (d != e)
                        {
                            d.keepOutside(e);
                        }
                    }
                }
                foreach (ILinkableParent lp in linkParents)
                {
                    lp.onLinkableUpdate();
                }

                base.Update(gameTime);
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (menuScreen)
            {
                menuPrinter.onDraw();
            } 
            else
            {
                foreach (IDrawable d in drawables)
                {
                    d.onDraw();
                }
                base.Draw(gameTime);
            }
            
        }

        public void showMenu()
        {
            this.menuScreen = true;
        }
    }
}
