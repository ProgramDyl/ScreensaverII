using System;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace screensaver_final
{
    internal class ExplosionParticle
    {
        private float x, y; // position of particle
        private float velocityX, velocityY; 
        private float life;
        private Color color;


        public ExplosionParticle(float x, float y, float velocityX, float velocityY, float life, Color color)
        {
            this.x = x;
            this.y = y;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.life = life;
            this.color = color;
        }

        public bool Update()
        {
            //Update position
            x += velocityX;
            y += velocityY;
            //decrease life
            life -= 0.01f;
            //return true if particle is still alive, 
            // false if it should be removed
            return life > 0;
        }

        public void Draw(Graphics g)
        {
            // Ensure alpha value is clamped between 0 and 255
            int alpha = (int)(life * 255);
            alpha = Math.Max(0, Math.Min(255, alpha)); // "Clamps" the alpha value to be within the valid range

            //citations: [1] https://www.geeksforgeeks.org/c-sharp-math-min-method/
                          
            // Create a brush with the clamped alpha value
            using (Brush brush = new SolidBrush(Color.FromArgb(alpha, color)))
            {
                g.FillEllipse(brush, x, y, 5, 5);
            }
        }



    }
}