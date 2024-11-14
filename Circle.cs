using System.Drawing;
using System.Drawing.Drawing2D;

namespace screensaver_final
{
    internal class Circle : Shape
    {
        public Circle(int x, int y, int diameter, Color color)
            : base(x, y, diameter, diameter, color)
        {
        }

        public override void Draw(Graphics g)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(X, Y, Width, Height);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {

                    //adjust gradient to reduce white
                    brush.CenterColor = Color.FromArgb(180, Color);
                    brush.SurroundColors = new Color[] { Color.FromArgb(50, Color) };

                    //makes a smoother gradient
                    brush.FocusScales = new PointF(0.2f, 0.2f);
                    brush.CenterColor = Color.FromArgb(255, Color);
                    g.FillEllipse(brush, X, Y, Width, Height);
                }
            }
        }
    }
}
