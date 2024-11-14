using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace screensaver_final
{
    internal class PacMan : Shape
    {
        private Timer animationTimer; // Timer for animation
        private int mouthAngle; // Mouth angle for animation
        private bool mouthOpening; // Direction of mouth movement
        private SoundPlayer deathSoundPlayer; //when pac-man dies


        public PacMan(int x, int y, int diameter, Color color)
            : base(x, y, diameter, diameter, color) // diameter is both width and height
        {
            mouthAngle = 30; // initial mouth angle
            mouthOpening = true; // Start with mouth opening
            InitializeAnimation(); // Initialize the animation timer
            deathSoundPlayer =
                new SoundPlayer(
                    "C:\\Users\\dylan\\NSCC\\Semester 3\\4-Advanced Topics\\Assignments\\screensaver-final\\resources\\pacman_death.wav");
        }

        private void InitializeAnimation()
        {
            animationTimer = new Timer { Interval = 100 };
            animationTimer.Interval = 100; // Set the interval to 100 milliseconds
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Update the mouth angle for the animation
            if (mouthOpening)
            {
                mouthAngle += 10;
                if (mouthAngle >= 60) mouthOpening = false; // Max open angle
            }
            else
            {
                mouthAngle -= 10;
                if (mouthAngle <= 30) mouthOpening = true; // Min open angle
            }
        }
        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                // Create a Pac-Man shape using an arc and two lines to make the "mouth"
                GraphicsPath path = new GraphicsPath();
                path.AddArc(X, Y, Width, Height, mouthAngle, 360 - 2 * mouthAngle);
                path.AddLine(X + Width / 2, Y + Height / 2, X + Width, Y + Height / 2 - (Height / 4));
                path.AddLine(X + Width / 2, Y + Height / 2, X + Width, Y + Height / 2 + (Height / 4));
                g.FillPath(brush, path);

            }

            //check if pac-man has reached death size
            CheckForDeath();

        }

        //eat other shapes!
        public void EatShapes(List<Shape> shapes)
        {
            // loop through shapes list in reverse order 
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                Shape shape = shapes[i]; //grabs current shape
                if (this != shape && CollidesWith(shape)) //checks if the shape is not the current pac man and it's colliding with another shape 
                {
                    // check if other shape is pacman
                    if (shape is PacMan otherPacMan)
                    {
                        //pacman can only eat smaller pacmen
                        if (this.Width > otherPacMan.Width)
                        {
                            //remove smaller pacman from list
                            shapes.Remove(shape); 
                            //increase size when eating another pac-man
                            this.Width += 5;
                            //NOTE: you have to mirror the change with height, otherwise pac man will become pac-oval 
                            this.Height += 5;

                        }
                    }
                    // if shape is not pac-man
                    else
                    {
                        shapes.Remove(shape);
                        this.Width += 3;
                        this.Height += 3;
                    }
                    break; //exit the loop once a shape is eaten 
                }
            }
        }//endmethod

        //Check for death condition 
        private void CheckForDeath()
        {
            if (Width >= 30)
            {
                PlayDeathSound();
            }
        }

        private void PlayDeathSound()
        {
            deathSoundPlayer.Play();
        }


    }//end class pacman 
}//end namespc
