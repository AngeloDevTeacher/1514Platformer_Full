using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PlatformerGame
{
    public class Collider
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        protected ColliderType colliderType;

        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 dimensions;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }
        public Collider(Vector2 position, Vector2 dimensions, ColliderType colliderType)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.colliderType = colliderType;
        }
        internal void LoadContent(ContentManager Content)
        {
            string textureString = "Collider" + colliderType.ToString();
            texture = Content.Load<Texture2D>(textureString);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        internal bool ProcessCollisions(ActorObject actor)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(actor._rectangleBounds))
            {
                didCollide = true;
                switch (colliderType)
                {
                    case ColliderType.Left:
                        //if the player is moving rightwards
                        if (actor.Velocity.X > 0)
                        {
                            actor.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        if (actor.Velocity.X < 0)
                        {
                            actor.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Top:
                        actor.Land(BoundingBox);
                        actor.StandOn(BoundingBox);
                        break;
                    case ColliderType.Bottom:
                        if (actor.Velocity.Y < 0)
                        {
                            actor.MoveVertically(0);
                        }
                        break;
                }
            }

            return didCollide;
        }

    }
}
