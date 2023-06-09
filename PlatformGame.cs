﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PlatformerGame
{
    public class PlatformGame : Game
    {   
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        internal const int Gravity = 5;

        //Texture2D boxTop, boxBottom, boxLeft, boxRight;
        Transform GO_Transform;
        ActorObject player;

        Texture2D playerIdle;
        Texture2D playerRun;
        Texture2D playerJump;

        Platform p;

        Rectangle testRect = new Rectangle(0,0,48,48);
        CelAnimationSet playerSet;


        public PlatformGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            p = new Platform(new Vector2(120, Window.ClientBounds.Height-120), new Vector2(100, 25), "");
            base.Initialize();
            GO_Transform = new Transform(new Vector2(48,24), 0, 1);
            player = new ActorObject(this, GO_Transform, testRect, playerIdle, playerSet);
            Window.Title = "DMIT 1514 - Platformer Game (2023)";


            
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerIdle = Content.Load<Texture2D>("Character Idle 48x48");
            playerRun = Content.Load<Texture2D>("run cycle 48x48");
            playerJump = Content.Load<Texture2D>("player jump 48x48");
            playerSet = new CelAnimationSet(playerIdle, playerRun, playerJump);
            p.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Right) == kbState.IsKeyDown(Keys.Left))
            {
                player.HorizontalStop();
                player._State = ActorState.Idle;
            }
            else if (kbState.IsKeyDown(Keys.Right))
            {
                player.MoveHorizontally(1);
            }
            else if (kbState.IsKeyDown(Keys.Left))
            {
                player.MoveHorizontally(-1);
            }
            if (kbState.IsKeyDown(Keys.Space))
            {
                player.Jump();
            }
            p.ProcessCollisions(player);
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            p.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}