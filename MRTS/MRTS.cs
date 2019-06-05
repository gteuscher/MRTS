
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MRTS.GameComponents;
using System;
using System.Collections.Generic;
using System.IO;

namespace MRTS
{
    public class MRTS : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;

        public MRTS()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var towerGraphics = new List<Texture2D>();

            try
            {
                DirectoryInfo dir = new DirectoryInfo(Content.RootDirectory + "\\Tower\\" );
                FileInfo[] files = dir.GetFiles("*.*");
                foreach (FileInfo file in files)
                {
                    var tempName = (Path.GetFileNameWithoutExtension(file.Name));
                    towerGraphics.Add(Content.Load<Texture2D>("Tower/" + Path.GetFileNameWithoutExtension(file.Name)));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load image content", e);
            }
            level = new Level(10, 6, towerGraphics);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                level.Update(gameTime, mouseState.Position.X, mouseState.Position.Y);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
