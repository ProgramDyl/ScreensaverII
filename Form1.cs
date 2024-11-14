using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace screensaver_final
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes; // List to hold all shapes
        private List<ExplosionParticle> explosionParticles = new List<ExplosionParticle>();
        private Graphics g;
        private readonly Random random;
        private readonly SoundPlayer introSound; //sounds for intro music :)

        public Form1()
        {
            InitializeComponent();
            shapes = new List<Shape>(); // Initialize the list
            explosionParticles = new List<ExplosionParticle>();
            random = new Random();
            this.BackColor = Color.Azure;
            this.DoubleBuffered = true;
            introSound = new SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"C:\Users\dylan\NSCC\Semester 3\4-Advanced Topics\Assignments\screensaver-final\resources\pacman_beginning.wav"));
            PlayIntroSound();
            InitializeTimer();
            this.Resize += new EventHandler(Form1_Resize); // Subscribe to resize event
        }

        private void PlayIntroSound()
        {
            try
            {
                introSound.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

        private void InitializeTimer()
        {
            timer1 = new Timer();
            timer1.Interval = 100;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Move();
                shape.DetectEdge(this.ClientSize.Width, this.ClientSize.Height);
            }

            // Update explosion particles
            for (int i = explosionParticles.Count - 1; i >= 0; i--)
            {
                if (!explosionParticles[i].Update())
                {
                    explosionParticles.RemoveAt(i);
                }
            }

            // when Pac-Man eats another shape
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i] is PacMan pacMan)
                {
                    pacMan.EatShapes((shapes)); // call the eat method
                }
            }

            // Checks for collisions between the shapes
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    if (shapes[i].CollidesWith(shapes[j]))
                    {
                        shapes[i].ShapeCollide(shapes[j]); // Ensure collision response is handled
                        CreateExplosion(shapes[i].X, shapes[i].Y, 20); // Create explosion at collision point
                    }
                }
            }

            Invalidate(); // Redraws the form every tick
            Console.WriteLine("Timer ticked, form invalidated.");
        }



        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Check for left mouse button click
            if (e.Button == MouseButtons.Left) 
            {
                //Create a new shape (randomly selects from shape list)
                Shape shape = CreateRandomShape(e.X, e.Y);
                shapes.Add(shape);
            }
            else if (e.Button == MouseButtons.Right) // Check for right mouse button click
            {
                // Create a new Pac-Man
                Shape pacMan = new PacMan(e.X, e.Y, 50, Color.Yellow);
                shapes.Add(pacMan);
            }
            else if (e.Button == MouseButtons.Middle) //middle mouse button 
            {
                //creates picturebox 
                Shape pictureBoxShape = new PictureBoxShape(e.X, e.Y, 50, 50,
                    Image.FromFile(
                        "C:\\Users\\dylan\\NSCC\\Semester 3\\4-Advanced Topics\\Assignments\\screensaver-final\\resources\\bomb-with-animationx1000.gif"));
                ((PictureBoxShape)pictureBoxShape).AddToForm(this);
                shapes.Add(pictureBoxShape);
            }

            //Set the speed of newly created shape
            SetShapeSpeed(shapes[shapes.Count - 1], 5);
            Console.WriteLine($"Shape added at ({e.X}, {e.Y})");
        }

        private Shape CreateRandomShape(int x, int y)
        {
            int shapeType = random.Next(100);
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(255));

            switch (random.Next(5)) //don't forget to update this! 
            {
                case 0:
                    Console.WriteLine("Creating Rectangle");
                    return new Rectangle(x, y, 30, 30, color);
                case 1:
                    Console.WriteLine("Creating Circle");
                    return new Circle(x, y, 30, color);
                case 2:
                    Console.WriteLine("Creating Triangle");
                    return new Triangle(x, y, 30, color);
                case 3:
                    Console.WriteLine("Creating Ghost");
                    return new Ghost(x, y, 30,  30, color);
                case 4:
                    Console.WriteLine("Morphin' time!");
                    return new MorphingShape(x, y, 30, color);
                default:
                    Console.WriteLine("Defaulting to Rectangle");
                    return new Rectangle(x, y, 30, 30, color);
            }
        }

        //<< SPEED >>

        private void SetShapeSpeed(Shape shape, int speed)
        {
            shape.SetSpeed(speed);
        }

        private void SetAllShapesSpeed(int speed)
        {
            foreach (var shape in shapes)
            {
                shape.SetSpeed(speed); //sets for all shapes in the list
            }
        }

        //DONT DELETE
        private void Form1_Load(object sender, EventArgs e)
        {
            // BACKGROUND COLOR 
            this.BackColor = Color.Black;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Console.WriteLine("Paint event triggered. Number of shapes: " + shapes.Count);
            if (shapes.Count == 0) { return; }

            if (g == null)
            {
                Console.WriteLine("Graphics context is null.");
            }
            else
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(g);
                    Console.WriteLine($"Shape added at ({shape.X}, {shape.Y}) with size ({shape.Width}x{shape.Height})");
                }

                //draw explosion particles
                foreach (var particle in explosionParticles)
                {
                    particle.Draw(g);
                }
            }
        } //end form1Paint

        private void Form1_Resize(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.BoundWidth = this.ClientSize.Width;
                shape.BoundHeight = this.ClientSize.Height;
            }
        }


        //explosions!!
        private void CreateExplosion(float x, float y, int numParticles)
        {
            Random random = new Random();
            for (int i = 0; i < numParticles; i++)
            {
                float velocityX = (float)(random.NextDouble() * 2 - 1);
                float velocityY = (float)(random.NextDouble() * 2 - 1);
                ExplosionParticle particle = new ExplosionParticle(x, y, velocityX, velocityY, 20, Color.Orange);
                explosionParticles.Add(particle);
            }

            Console.WriteLine($"Created explosion at ({x}, {y}) with {numParticles} particles.");
        }
    }
}
