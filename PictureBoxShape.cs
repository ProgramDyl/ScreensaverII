using System;
using System.Drawing;
using System.Windows.Forms;

namespace screensaver_final
{


    // PictureBox is compatible with .gif files. 
    internal class PictureBoxShape : Shape
    {
        private PictureBox pictureBox; // PictureBox control to display the image

        // Constructor
        public PictureBoxShape(int x, int y, int width, int height, Image image)
            : base(x, y, width, height, Color.Transparent)
        {
            pictureBox = new PictureBox
            {
                //attributes of pic box
                Location = new Point(x, y), 
                Size = new Size(width, height),
                Image = image, // Set img for picbox
                SizeMode = PictureBoxSizeMode.StretchImage, //fits whole
                BackColor = Color.Transparent //bkg color
            };
        }

        // Override Draw method
        public override void Draw(Graphics g)
        {
            pictureBox.Location = new Point(X, Y); // Update PictureBox location to current shape location
        }

        // Method to add PictureBox to the form
        public void AddToForm(Form form)
        {
            form.Controls.Add(pictureBox); // Add PictureBox to the form's controls
        }
    }
}