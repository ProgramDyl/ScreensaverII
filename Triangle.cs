using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace screensaver_final
{
    internal class Triangle : Shape
    {
        
        private List<TrailParticle> trailParticles; // List to hold trail particles
        

        //constructor
        public Triangle(int x, int y, int size, Color color)
            : base(x, y, size, size, color) // size is the length of the sides
        {
            trailParticles = new List<TrailParticle>(); // Initialize the trail particles list
        }
        

        //region Override of draw routine
        public override void Draw(Graphics g)
        {
            // Update trail particles' positions and lifetimes
            UpdateTrailParticles();

            // Create the triangle using GraphicsPath
            using (GraphicsPath path = new GraphicsPath())
            {
                // Define the three points of the triangle
                Point[] points =
                {
                    new Point(X + Width / 2, Y), // Top point
                    new Point(X, Y + Height), // Bottom-left point
                    new Point(X + Width, Y + Height) // Bottom-right point
                };
                path.AddPolygon(points); // Add the points to form a polygon

                // Create a PathGradientBrush to fill the triangle with gradient
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(180, Color); // Set the center color with alpha
                    brush.SurroundColors = new Color[] { Color.FromArgb(50, Color) }; // Set the surround color with alpha
                    brush.FocusScales = new PointF(0.2f, 0.2f); // Adjust the gradient focus

                    g.FillPolygon(brush, points); // Fill the triangle
                }
            }

            // Draw each trail particle
            foreach (var particle in trailParticles)
            {
                particle.Draw(g); // Draw the particle
            }
        }
        //endregion Override of draw routine

        //region Trail particle update method
        private void UpdateTrailParticles()
        {

            // Add a new particle at the current position
            trailParticles.Add(new TrailParticle(new Point(X + Width / 2, Y + Height / 2), Color, 50));

            // Update existing particles
            for (int i = trailParticles.Count - 1; i >= 0; i--)
            {
                trailParticles[i].Update(); // Update particle's life
                if (trailParticles[i].LifeTime <= 0)
                {
                    trailParticles.RemoveAt(i); // Remove particle if its lifetime is over
                }
            }
        }
        //endregion Trail particle update method
    }
}

//CODE SUMMARY: 

// The `trailparticles` list is used to store particles than form the trail
// 'constructor' initializes the list

// <Draw method>:
// updates the trail particles' position and lifetime
// draws the triangle using `graphicspath` and `pathgradientbrush` (citation: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.drawing2d.graphicspath?view=net-8.0)
// draws each trail particle

//<Updatetrailparticles method>: 
    // adds a new particle at the current iterative position
    // updates the existing particles and removes them when their lifetime is over