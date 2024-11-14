using System;
using System.Drawing;


namespace screensaver_final
{
    internal abstract class Shape
    {
        //region Basic properties
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Width { get; protected set; } = 0;
        public int Height { get; protected set; }
        public Color Color { get; protected set; }

        public int BoundWidth { get; set; }
        public int BoundHeight { get; set; }

        protected int[] Velocity = new int[2];

        // Add properties for the bounding box
        protected int Bx, By, Bwidth, Bheight;

        //endregion Basic properties

        //region Constructor
        protected Shape(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;

            //speed
            InitializeVelocity();
        }
        //endregion Constructor

        //region Override of draw routine
        public abstract void Draw(Graphics g);
        //endregion

        //region Physics / Methods
        public void Move()
        {

            // y and x velocity declared
            Y += Velocity[1];
            X += Velocity[0];

            // Declare bounding box size variables
            Bx = X;
            By = Y;
            Bwidth = Width;
            Bheight = Height;
        }

        public bool CollidesWith(Shape other)
        {
            //Checks for collision between this shape's bounding box and the other shape's bounding box
            return Bx < other.Bx + other.Bwidth &&
                   Bx + Bwidth > other.X &&
                   By < other.By + other.Bheight &&
                   By + Bheight > other.By;
        }

        public void ShapeCollide(Shape other)
        {
            // Check for collisions with the other shapes
            if (this != other && CollidesWith(other))
            {

                //changes colour on collision
                ChangeColor();
                other.ChangeColor();

                int tempVelocityX = Velocity[0];
                int tempVelocityY = Velocity[1];

                // Detects collision and updates the movement of the shape
                Velocity[0] = -Velocity[0];
                Velocity[1] = -Velocity[1];
                other.Velocity[0] = -other.Velocity[0];
                other.Velocity[1] = -other.Velocity[1];
            }
        }

        public void InitializeVelocity()
        {
            Random random = new Random();
            Velocity[0] = random.Next(-3, 3);
            Velocity[1] = random.Next(-3, 3);
        }

        // Speed setting!
        public void SetSpeed(int speed)
        {
            //direction needs to be consistent with the current velocity
            Velocity[0] = Math.Sign(Velocity[0]) * speed;
            Velocity[1] = Math.Sign(Velocity[1]) * speed;
        }


        public void DetectEdge(int panelWidth, int panelHeight)
        {

            if (X <= 0 || X + Width >= panelWidth || Y <= 0 || Y + Height >= panelHeight)
            {
                ChangeColor(); //changes on edge collision
            }
            if (X <= 0 || X + Width >= panelWidth) Velocity[0] *= -1;
            if (Y <= 0 || Y + Height >= panelHeight) Velocity[1] *= -1;
        }

        public void ChangeColor()
        {
            Random random = new Random();
            Color = Color.FromArgb(random.Next(256), random.Next(256), (random.Next(256)));
        }

        //endregion
    } //end Shape

}
