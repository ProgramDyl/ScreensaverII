using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screensaver_final
{
    internal class Rectangle : Shape
    {
        private Color _currentColor; 

        public Rectangle(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color)
        {
            _currentColor = color; // Set the initial color
        }

        public override void Draw(Graphics g)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(new System.Drawing.Rectangle(X, Y, Width, Height));
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    // Adjust the gradient to give a 3d-like effect
                    brush.CenterColor = Color.FromArgb(180, Color);

                    brush.SurroundColors = new Color[] { Color.FromArgb(50, Color) };

                    brush.FocusScales = new PointF(0.2f, 0.2f);

                    g.FillRectangle(brush, X, Y, Width, Height);
                }
            }
        }

    }
}