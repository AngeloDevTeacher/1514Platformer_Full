using Microsoft.Xna.Framework;

namespace PlatformerGame
{
    /// <summary>
    /// Describes general data as a point in space, with rotation + scale.
    /// </summary>
    internal struct Transform
    {
        Vector2 _position;
        float _rotation;
        int _scale;

        public Vector2 Position => _position;
        public float Rotation => _rotation;
        public int Scale => _scale;

        public Transform(Vector2 position, float rotation, int scale)
        {
            _position = position;
            _rotation = rotation;
            _scale = scale;
            
        }
        
    }
    /// <summary>
    /// Exentsion methods for the struct. Syntatic sugar to make it easier and compartmentalize the code better.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the top-left corner of the transform, using half the reference size.
        /// </summary>
        /// <param name="referenceSize"></param>
        /// <returns></returns>
        public static Point ToLocation(this Vector2 _position, Point referenceSize)
        {
            int x = (int)_position.X - referenceSize.X / 2;
            int y = (int)_position.Y - referenceSize.Y / 2;
            return new Point(x, y);
        }
    }
}
