using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace screensaver_final
{
    internal class MorphingShape : Shape
    {
        private int shapeState; // Variable to keep track of the current shape state
        private Timer morphTimer; // Timer for morphing

        // Constructor
        public MorphingShape(int x, int y, int size, Color color)
            : base(x, y, size, size, color)
        {
            shapeState = 0; // Start with the first shape state
            InitializeMorphTimer(); // Initialize the morphing timer
        }

        private void InitializeMorphTimer()
        {
            morphTimer = new Timer();
            morphTimer.Interval = 1000; // Set morphing interval (e.g., 1000 ms)
            morphTimer.Tick += MorphTimer_Tick;
            morphTimer.Start();
        }

        private void MorphTimer_Tick(object sender, EventArgs e)
        {
            // Cycle through shape states
            shapeState = (shapeState + 1) % 5; // Assuming 5 unique shape states
        }

        // Override Draw method
        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    switch (shapeState)
                    {
                        case 0:
                            // Draw an ellipse (circle)
                            path.AddEllipse(X, Y, Width, Height);
                            break;
                        case 1:
                            // Draw a star
                            path.AddLines(new PointF[]
                            {
                                new PointF(X + Width / 2, Y),
                                new PointF(X + 3 * Width / 4, Y + Height / 2),
                                new PointF(X + Width, Y + Height / 2),
                                new PointF(X + 7 * Width / 8, Y + 3 * Height / 4),
                                new PointF(X + Width, Y + Height),
                                new PointF(X + Width / 2, Y + 7 * Height / 8),
                                new PointF(X, Y + Height),
                                new PointF(X + Width / 8, Y + 3 * Height / 4),
                                new PointF(X, Y + Height / 2),
                                new PointF(X + Width / 4, Y + Height / 2)
                            });
                            path.CloseFigure();
                            break;
                        case 2:
                            // Draw a polygon (triangle)
                            path.AddPolygon(new PointF[]
                            {
                                new PointF(X + Width / 2, Y),
                                new PointF(X + Width, Y + Height),
                                new PointF(X, Y + Height)
                            });
                            break;
                        case 3:
                            // heart shape <3 
                            //*
                            //*citation: [1] https://stackoverflow.com/questions/58333678/draw-heart-using-javascript-in-any-postionx-y
                            //*          [2] https://learn.microsoft.com/en-us/dotnet/api/system.drawing.drawing2d.graphicspath.addbezier?view=windowsdesktop-8.0&form=MG0AV3   
                            path.AddBezier(new PointF(X + Width / 2, Y + Height / 4), new PointF(X + Width / 4, Y), new PointF(X, Y + Height / 2), new PointF(X + Width / 2, Y + 3 * Height / 4));
                            path.AddBezier(new PointF(X + Width / 2, Y + 3 * Height / 4), new PointF(X + Width, Y + Height / 2), new PointF(X + 3 * Width / 4, Y), new PointF(X + Width / 2, Y + Height / 4));
                            break;
                        case 4:
                            // Draw a wave shape
                            path.AddBezier(new PointF(X, Y + Height / 2), new PointF(X + Width / 4, Y), new PointF(X + 3 * Width / 4, Y + Height), new PointF(X + Width, Y + Height / 2));
                            path.AddBezier(new PointF(X + Width, Y + Height / 2), new PointF(X + 3 * Width / 4, Y), new PointF(X + Width / 4, Y + Height), new PointF(X, Y + Height / 2));
                            break;
                    }

                    // Fill the shape
                    g.FillPath(brush, path);
                }
            }
        }
    }
}
