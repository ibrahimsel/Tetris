using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris666
{
    public partial class Form1 : Form
    {
        const int labelSize = 20;                       // One label is 20 pixels in width and 20 pixels in height
        const int labelVelocity = 20;                   // One label drops 20 pixels at a time
        int x = 10;                                     // Tetromino spawn point x
        int y = 0;                                      // Tetromino spawn point y
        int occupiedRowCounter = 0;                     // We will use this to check if a line is full

        int[] allTetrominos = new int[300];             // We will use this array to store randomly generated tetromino numbers
                                                            // So that we could generate current tetromino and see upcoming tetromino

        bool startGame = false;                         // This indicates if the game has started or not
        int currentNumberOfLabels = 0;                  // This counts how many labels have been deployed

        bool rotateBack;                                // We will be using this to rotate a piece back if it's unrotatable
                                                            // If it's unrotatable, we will rotate it anti-clockwise then clockwise
                                                            // So it won't be rotated
        Color color;                                    // Color of tetrominos
        Label[] tetromino1 = new Label[4];  // S
        Label[] tetromino2 = new Label[4];  // Z
        Label[] tetromino3 = new Label[4];  // O
        Label[] tetromino4 = new Label[4];  // T
        Label[] tetromino5 = new Label[4];  // L
        Label[] tetromino6 = new Label[4];  // J
        Label[] tetromino7 = new Label[4];  // I

        Label[] currentTetromino = new Label[4];                    // We will store the active tetromino in this Label array
        Label[] placedTetrominos = new Label[3000];                     // Once a tetromino is placed
                                                                        // We will save each label's info in currentTetromino to placedTetrominos
                                                                        // Then we will clear currentTetromino
                                                                        // So we could use it again for different tetrominos

        Point[] occupiedPlaces = new Point[3000];                   //  Location informations of the Labels in placedTetrominos
        Random rand = new Random();                                 //  Random object for generating random numbers
        int score = 0;                                              //  This will store user's score
        int tetrominoNumber = 0;                                    //  We will use this as index to access allTetrominos array            


        public Form1()
        {
            InitializeComponent();
        }

        private bool MyContains(Point[] array, Point point)  //  My implementation of Contains() method
        {
            foreach (Point p in array)
            {
                if (p == point)
                    return true;
            }
            return false;
        }

        private void FillAllTetrominos()  // Before we start the game
                                          // We need to determine which tetrominos will be generated
                                          // So we randomly fill allTetrominos array
        {
            for (int i = 0; i < 300; i++)
                allTetrominos[i] = rand.Next(1, 8);
        }

        private Label AddLabelParticle(int x1, int y1, Color _color)  // We generate each label with this method
        {

            Label particle = new Label
            {
                Location = new Point(x1, y1),
                Size = new Size(labelSize, labelSize),
                BackColor = _color,
            };

            panel1.Controls.Add(particle);  // Add it to panel
            return particle;
        }

        private void GenerateTetromino()  // With the combination of labels
                                          // We generate tetrominos with this method
        {
            if (tetrominoNumber == 300)  // If game lasts longer than expected
                                         // Restart allTetrominos array
            {
                tetrominoNumber = 0;
                FillAllTetrominos();
            }

            Color color = GenerateColor();  // Generate color for current tetromino
            switch (allTetrominos[tetrominoNumber])
            {
                case 1:  // Draw S
                    tetromino1[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino1[1] = AddLabelParticle(tetromino1[0].Location.X, tetromino1[0].Location.Y + labelSize, color);
                    tetromino1[2] = AddLabelParticle(tetromino1[1].Location.X + labelSize, tetromino1[1].Location.Y, color);
                    tetromino1[3] = AddLabelParticle(tetromino1[2].Location.X, tetromino1[1].Location.Y + labelSize, color);
                    currentTetromino = tetromino1;

                    break;
                case 2:  // Draw Z
                    tetromino2[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino2[1] = AddLabelParticle(tetromino2[0].Location.X, tetromino2[0].Location.Y + labelSize, color);
                    tetromino2[2] = AddLabelParticle(tetromino2[1].Location.X - labelSize, tetromino2[1].Location.Y, color);
                    tetromino2[3] = AddLabelParticle(tetromino2[2].Location.X, tetromino2[2].Location.Y + labelSize, color);
                    currentTetromino = tetromino2;

                    break;
                case 3:  // Draw O  

                    tetromino3[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino3[1] = AddLabelParticle(tetromino3[0].Location.X, tetromino3[0].Location.Y + labelSize, color);
                    tetromino3[2] = AddLabelParticle(tetromino3[1].Location.X - labelSize, tetromino3[1].Location.Y, color);
                    tetromino3[3] = AddLabelParticle(tetromino3[2].Location.X, tetromino3[2].Location.Y - labelSize, color);
                    currentTetromino = tetromino3;

                    break;
                case 4:  // Draw T

                    tetromino4[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino4[1] = AddLabelParticle(tetromino4[0].Location.X + labelSize, tetromino4[0].Location.Y, color);
                    tetromino4[2] = AddLabelParticle(tetromino4[1].Location.X + labelSize, tetromino4[1].Location.Y, color);
                    tetromino4[3] = AddLabelParticle(tetromino4[2].Location.X - labelSize, tetromino4[2].Location.Y + labelSize, color);
                    currentTetromino = tetromino4;

                    break;
                case 5:  // Draw L

                    tetromino5[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino5[1] = AddLabelParticle(tetromino5[0].Location.X, tetromino5[0].Location.Y + labelSize, color);
                    tetromino5[2] = AddLabelParticle(tetromino5[1].Location.X, tetromino5[1].Location.Y + labelSize, color);
                    tetromino5[3] = AddLabelParticle(tetromino5[2].Location.X + labelSize, tetromino5[2].Location.Y, color);
                    currentTetromino = tetromino5;

                    break;
                case 6:  // Draw J

                    tetromino6[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino6[1] = AddLabelParticle(tetromino6[0].Location.X, tetromino6[0].Location.Y + labelSize, color);
                    tetromino6[2] = AddLabelParticle(tetromino6[1].Location.X, tetromino6[1].Location.Y + labelSize, color);
                    tetromino6[3] = AddLabelParticle(tetromino6[2].Location.X - labelSize, tetromino6[2].Location.Y, color);
                    currentTetromino = tetromino6;

                    break;
                case 7:  // Draw I

                    for (int i = 0; i < 4; i++)
                    {
                        tetromino7[i] = AddLabelParticle(x * labelSize, y + (i * labelSize), color);
                    }
                    currentTetromino = tetromino7;
                    break;
            }

            // Show next tetromino
            switch (allTetrominos[tetrominoNumber + 1])  // +1 for the next tetromino
            {
                case 1: pictureBox1.Image = Image.FromFile(@"C:/Tetris/S.png"); break;
                case 2: pictureBox1.Image = Image.FromFile(@"C:/Tetris/Z.png"); break;
                case 3: pictureBox1.Image = Image.FromFile(@"C:/Tetris/O.png"); break;
                case 4: pictureBox1.Image = Image.FromFile(@"C:/Tetris/T.png"); break;
                case 5: pictureBox1.Image = Image.FromFile(@"C:/Tetris/L.png"); break;
                case 6: pictureBox1.Image = Image.FromFile(@"C:/Tetris/J.png"); break;
                case 7: pictureBox1.Image = Image.FromFile(@"C:/Tetris/I.png"); break;
            }
            tetrominoNumber += 1;  // After we generate it, we update current Tetromino number
        }
        private void RemoveTetromino()  // After a tetromino is placed
                                        // Add relevant information to placedTetrominos
                                        // Then remove it
        {
            for (int i = 0; i < 4; i++)
            {
                panel1.Controls.Remove(currentTetromino[i]);
            }

        }


        private void SaveOccupiedPoints()  // Save occupied points to detect collision and line detection
        {
            for (int i = currentNumberOfLabels; i < currentNumberOfLabels + 4; i++)
            {
                int x1 = currentTetromino[i - (currentNumberOfLabels)].Location.X;
                int y1 = currentTetromino[i - (currentNumberOfLabels)].Location.Y;
                occupiedPlaces[i] = new Point(x1, y1);
                RemoveTetromino();
                placedTetrominos[i] = AddLabelParticle(x1, y1, color);

            }
            currentNumberOfLabels += 4;
        }

        private void IsThereTetris()  // Method for line detection
        {
            for (int y = 440; y > 0; y -= 20)  // Access every possible place on map
            {
                occupiedRowCounter = 0;
                for (int x = 0; x < 320; x += 20)
                {
                    if (MyContains(occupiedPlaces, new Point(x, y)))  // If a point is found in occupiedPlaces
                    {
                        occupiedRowCounter += 1;  // Update
                        if (occupiedRowCounter == 16)  // 16 means a full line
                        {
                            score += 100;  // Update score
                            for (int i = 0; i < occupiedPlaces.Length; i++)  // First update occupiedPlaces
                            {
                                int y1 = occupiedPlaces[i].Y;
                                int x1 = occupiedPlaces[i].X;

                                if (y1 == y)  // Carry the line off-limits so it won't be seen
                                              // Definitely not the best solution but I'm reinventing the wheel
                                {
                                    x1 = 361;
                                    y1 = 461;
                                }
                                else if (y1 < y)  // Move every other label 20 pixels below
                                    y1 += labelVelocity;
                                occupiedPlaces[i] = new Point(x1, y1);
                            }

                            for (int i = 0; i < currentNumberOfLabels; i++)  // Then actually move labels 
                                                                                // to where they are supposed to be
                                                                                // after line destruction
                            {
                                int x1 = placedTetrominos[i].Location.X;
                                int y1 = placedTetrominos[i].Location.Y;
                                if (y1 == y)
                                {
                                    x1 = 361;
                                    y1 = 461;
                                }
                                else if (y1 < y)
                                    y1 += labelVelocity;
                                placedTetrominos[i].Location = new Point(x1, y1);
                            }
                            x = 320;  // We reset the inner and outer loop
                            y = 460;     // to detect full lines until there is none
                        }
                        
                    }

                }
            }
        }

        private Color GenerateColor()  // Randomly generate color for tetrominos
        {

            switch (rand.Next(1, 6))
            {
                case 1: color = Color.Red; break;
                case 2: color = Color.Blue; break;
                case 3: color = Color.Yellow; break;
                case 4: color = Color.Lime; break;
                case 5: color = Color.Purple; break;
            }
            return color;
        }

        private bool IsLeftMovable()  
        {
            for (int i = 0; i < 4; i++)
            {
                int x1 = currentTetromino[i].Location.X;
                int y1 = currentTetromino[i].Location.Y;
                if (x1 - labelVelocity >= 0 && !MyContains(occupiedPlaces, new Point(x1 - labelVelocity, y1)))  // Dont allow it if next move is off the bounds or occupied
                    continue;
                else
                    return false;
            }
            return true;
        }

        private bool IsRightMovable()
        {
            for (int i = 0; i < 4; i++)
            {
                int x1 = currentTetromino[i].Location.X;
                int y1 = currentTetromino[i].Location.Y;
                if (x1 + labelVelocity < 320 && !MyContains(occupiedPlaces, new Point(x1 + labelVelocity, y1)))
                    continue;
                else
                    return false;
            }
            return true;
        }

        private bool IsDownMovable()
        {
            for (int i = 0; i < 4; i++)
            {
                int x1 = currentTetromino[i].Location.X;
                int y1 = currentTetromino[i].Location.Y;
                if (y1 + labelVelocity < 460 && !MyContains(occupiedPlaces, new Point(x1, y1 + labelVelocity)))
                    continue;
                else
                    return false;
            }
            return true;
        }

        private void MoveDown()  // Moves a label labelVelocity pixels below
        {
            if (IsDownMovable())
            {
                for (int i = 0; i < 4; i++)
                {
                    int x1 = currentTetromino[i].Location.X;
                    int y1 = currentTetromino[i].Location.Y;

                    y1 += labelVelocity;
                    currentTetromino[i].Location = new Point(x1, y1);
                }
            }
        }

        private void MoveLeft()
        {
            if (IsLeftMovable())
            {
                for (int i = 0; i < 4; i++)
                {
                    int x1 = currentTetromino[i].Location.X;
                    int y1 = currentTetromino[i].Location.Y;

                    x1 -= labelVelocity;
                    currentTetromino[i].Location = new Point(x1, y1);
                }
            }

        }

        private void MoveRight()
        {

            if (IsRightMovable())
            {
                for (int i = 0; i < 4; i++)
                {
                    int x1 = currentTetromino[i].Location.X;
                    int y1 = currentTetromino[i].Location.Y;

                    x1 += labelVelocity;
                    currentTetromino[i].Location = new Point(x1, y1);
                }
            }

        }

        private void MoveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                int x1 = currentTetromino[i].Location.X;
                int y1 = currentTetromino[i].Location.Y;

                y1 -= labelVelocity;
                currentTetromino[i].Location = new Point(x1, y1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!IsDownMovable())  // If tetromino can't move down
            {
                SaveOccupiedPoints();  // Save its place
                IsThereTetris();       // Check and destroy if there is a full line
                GenerateTetromino();   // Generate new tetromino

                if (!IsDownMovable())  // If new tetromino has nowhere to go instantly after it's spawn
                {                           // Game over
                    timer1.Enabled = false;
                    startGame = false;
                    MessageBox.Show("Game Over");

                }
            }
            MoveDown();
            score++;
            label1.Text = "Total Points: " + score.ToString();

        }

        private void ClearMap()  // Get everything back to their initial positions 
                                 // In case of user wants to play again
        {
            for (int i = 0; i < placedTetrominos.Length; i++)
            {
                panel1.Controls.Remove(placedTetrominos[i]);
                placedTetrominos[i] = null;
            }

            for (int i = 0; i < occupiedPlaces.Length; i++)
                occupiedPlaces[i] = new Point(600, 600);

            currentNumberOfLabels = 0;
            occupiedRowCounter = 0;
            score = 0;
        }

        private void Rotate()
        {
            int offTheBoundsRightCounter = 0;  // We will use these 3 variables
            int offTheBoundsLeftCounter = 0;        // To offset the after rotation position
            int offTheBoundsDownCounter = 0;

            if (currentTetromino != tetromino3)  // We dont rotate O because it has no center to rotate around
            {
                for (int i = 0; i < 4; i++)
                {
                    int x1 = currentTetromino[i].Location.X;
                    int y1 = currentTetromino[i].Location.Y;

                    double angle = 90;  // anti-clockwise rotation 
                    angle = angle * (Math.PI / 180);  // We need to convert the angle to radians
                    Point center = new Point(currentTetromino[1].Location.X, currentTetromino[1].Location.Y);  // We define the center point 
                                                                                                               // of tetromino to rotate about it



                    int x2 = (int)(Math.Cos(angle) * (x1 - center.X) - Math.Sin(angle) * (y1 - center.Y) + center.X);  // Rotation about a point formula
                    int y2 = (int)(Math.Sin(angle) * (x1 - center.X) + Math.Cos(angle) * (y1 - center.Y) + center.Y);

                    if (x2 < 0)  // Check how many labels got off the bounds
                        offTheBoundsLeftCounter += 1;
                    else if (x2 > 310)
                        offTheBoundsRightCounter += 1;
                    else if (y2 > 450)
                        offTheBoundsDownCounter += 1;

                    if (MyContains(occupiedPlaces, new Point(x2, y2)))
                    // if rotated tetromino is on an occupied place
                    // we will rotate it back
                    {
                        rotateBack = true;
                    }

                    currentTetromino[i].Location = new Point(x2, y2);  // Get the label to it's rotated position
                }

                if (rotateBack)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int x1 = currentTetromino[i].Location.X;
                        int y1 = currentTetromino[i].Location.Y;
                        double angle = -90;  // clockwise rotation
                        angle = angle * (Math.PI / 180);  // We need to convert the angle to radians
                        Point center = new Point(currentTetromino[1].Location.X, currentTetromino[1].Location.Y);


                        int x2 = (int)(Math.Cos(angle) * (x1 - center.X) - Math.Sin(angle) * (y1 - center.Y) + center.X);  // Rotation about a point formula
                        int y2 = (int)(Math.Sin(angle) * (x1 - center.X) + Math.Cos(angle) * (y1 - center.Y) + center.Y);
                        currentTetromino[i].Location = new Point(x2, y2);  // Get the label to it's old position
                    }
                    rotateBack = false;
                }

                // offset out of bound labels
                for (int i = 0; i < offTheBoundsLeftCounter; i++)
                    MoveRight();

                for (int i = 0; i < offTheBoundsRightCounter; i++)
                    MoveLeft();

                for (int i = 0; i < offTheBoundsDownCounter; i++)
                    MoveUp();
            }
        }

        private void button1_Click(object sender, EventArgs e)  // Start the game
        {
            startGame = true;
            timer1.Enabled = true;
            FillAllTetrominos();
            GenerateTetromino();
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)  // Restart the game
        {
            if (!button1.Visible)  // Only allow this button to be clicked
                                   // when the game is started at least once
            {
                ClearMap();
                startGame = true;
                timer1.Enabled = true;
                RemoveTetromino();
                GenerateTetromino();
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)  // Key events
        {
            if (startGame)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left: MoveLeft(); break;
                    case Keys.Right: MoveRight(); break;
                    case Keys.Down: MoveDown(); break;
                    case Keys.Up: Rotate(); break;    
                }
            }
        }

        private void button2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)  // Make arrow keys only work for game
                                                                                        // rather than transition between buttons
        {
            switch (e.KeyCode)
            {
                case Keys.Down: case Keys.Left: case Keys.Right: case Keys.Up:
                    e.IsInputKey = true; 
                    break;
            }
        }
    }
}
