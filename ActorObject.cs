using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PlatformerGame
{
    internal class ActorObject : GameObject
    {
        CelAnimationPlayer _CelPlayer;
        CelAnimationSet _CelAnimationSet;

        ActorDirection _Direction = ActorDirection.Right;
        internal ActorState _State = ActorState.Idle;


        protected const int JumpForce = -300;
        protected const int MoveSpeed = 150;

        protected Vector2 _velocity;
        internal Vector2 Velocity => _velocity;

        Point spriteDimensions;


        public ActorObject(Game game, Transform transform, Rectangle rectangle, Texture2D texture,CelAnimationSet set) : base(game, transform, rectangle, texture)
        {
            _CelPlayer = new CelAnimationPlayer();
            _CelAnimationSet = set;
            _CelPlayer.Play(_CelAnimationSet.Idle);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            _CelPlayer.Update(gameTime);
            _velocity.Y += PlatformGame.Gravity;
            _transform.MovePosition(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Math.Abs(_velocity.Y) > PlatformGame.Gravity)
            {
                _State = ActorState.Jump;
                _CelPlayer.Play(_CelAnimationSet.Jump);
            }
            if (_transform.Position.Y  > Game.Window.ClientBounds.Height - _rectangleBounds.Height)
            {
                _transform.SetPosition(_transform.Position.X, Game.Window.ClientBounds.Height - _rectangleBounds.Height);
                _velocity.Y = 0;
                _State = ActorState.Walking;
            }
            _Direction = Velocity.X >= 0 ? ActorDirection.Right : ActorDirection.Left;




        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _CelPlayer.Draw(spriteBatch, _transform.Position, (SpriteEffects)_Direction);
            spriteBatch.End();
        }

        internal void HorizontalStop()
        {
            if(_State == ActorState.Walking)
            {
                _velocity = Vector2.Zero;
                _State = ActorState.Idle;
                _CelPlayer.Play(_CelAnimationSet.Idle);
            }
        }


        internal void MoveHorizontally(float direction)
        {
            float previousDirection = _velocity.X;
            _velocity.X = direction * MoveSpeed;
            if(_State != ActorState.Jump)
            {
                _CelPlayer.Play(_CelAnimationSet.Run);
                _State = ActorState.Walking;
            }
        }
        internal void MoveVertically(float direction)
        {
            _velocity.Y = direction * MoveSpeed;
        }
        internal void Land(Rectangle colliderRect)
        {
            if(_State == ActorState.Jump)
            {
                _transform.SetPosition(_transform.Position.X,colliderRect.Top - _rectangleBounds.Height + 1);
                _velocity.Y = 0;
                _State = ActorState.Walking;
            }
        }
        internal void StandOn(Rectangle colliderRect)
        {
            _velocity.Y -= PlatformGame.Gravity;
        }
        internal void Jump()
        {
            if (_State != ActorState.Jump)
            {
                _velocity.Y = JumpForce;
            }
        }
    }

    internal enum ActorDirection
    {
        Right,
        Left
    }
    internal enum ActorState
    {
        Idle,
        Walking,
        Jump
    }
}
