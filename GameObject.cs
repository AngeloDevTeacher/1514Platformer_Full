using Microsoft.Xna.Framework;

namespace PlatformerGame
{
    internal class GameObject : DrawableGameComponent
    {
        Transform _transform;
        public Transform Transform { get { return _transform; } }
        public GameObject(Game game, Transform transform) : base(game)
        {
            _transform = transform;
            game.Components.Add(this);
        }
    }
}
