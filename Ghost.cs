using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace screensaver_final
{
    internal class Ghost : Shape
    {
        // Constructor
        public Ghost(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color)
        {
        }

        // Override Draw method
        public override void Draw(Graphics g)
        {
            //graphicspath contains necessary methods to draw the ghost
            //citation: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.drawing2d.graphicspath?view=windowsdesktop-8.0&form=MG0AV3

            using (GraphicsPath path = new GraphicsPath())
            {
                // Define the body of the ghost
                path.AddArc(X, Y, Width, Height / 2, 0, -180); // Head (half circle)
                path.AddLine(X + Width, Y + Height / 4, X + Width, Y + Height); // Right side
                path.AddBezier(X + Width, Y + Height, X + 3 * Width / 4, Y + 3 * Height / 4, X + Width / 4, Y + 3 * Height / 4, X, Y + Height); // Bottom wave (bezier curve)
                path.AddLine(X, Y + Height, X, Y + Height / 4); // Left side

                // Define the eyes
                path.AddEllipse(X + Width / 3, Y + Height / 6, Width / 6, Height / 6); // Left eye
                path.AddEllipse(X + 2 * Width / 3 - Width / 6, Y + Height / 6, Width / 6, Height / 6); // Right eye

                // Fill the ghost shape with the specified color
                using (Brush brush = new SolidBrush(Color))
                {
                    g.FillPath(brush, path);
                }

                // Draw the outline of the ghost
                using (Pen pen = new Pen(Color.BlueViolet, 2))
                {
                    g.DrawPath(pen, path);
                }
            }
        }
    }
}