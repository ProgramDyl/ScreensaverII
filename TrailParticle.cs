using System.Drawing;

namespace screensaver_final
{
    internal class TrailParticle
    {
        //region Basic properties
        public Point Position { get; set; } // Position of the particle
        public Color Color { get; set; } // Color of the particle
        public int LifeTime { get; set; } // LifeTime of the particle
        //endregion Basic properties

        //region Constructor
        public TrailParticle(Point position, Color color, int lifeTime)
        {
            Position = position; // Initialize position
            Color = color; // Initialize color
            LifeTime = lifeTime; // Initialize lifetime
        }
        //endregion Constructor

        //region Methods
        public void Draw(Graphics g)
        {
            // Draw particle with transparency based on remaining lifetime
            using (Brush brush = new SolidBrush(Color.FromArgb((int)(255 * (LifeTime / 100.0)), Color)))
            {
                g.FillEllipse(brush, Position.X, Position.Y, 5, 5);
            }
        }

        public void Update()
        {
            // Decrease particle's lifetime
            LifeTime--;
        }
        //endregion Methods
    }
}