using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris666
{
    public partial class Form1 : Form
    {
        #region
        const int labelSize = 20;
        const int labelVelocity = 20;
        int x = 10;
        int y = 0;
        int occupiedRowCounter = 0;
        int[] allTetrominos = new int[300];
        bool startGame = false;
        int currentNumberOfTiles = 0;
        bool rotateBack;
        Color color;
        Label[] tetromino1 = new Label[4];  // S
        Label[] tetromino2 = new Label[4];  // Z
        Label[] tetromino3 = new Label[4];  // O
        Label[] tetromino4 = new Label[4];  // T
        Label[] tetromino5 = new Label[4];  // L
        Label[] tetromino6 = new Label[4];  // J
        Label[] tetromino7 = new Label[4];  // I
        Label[] currentTetromino = new Label[4];
        Label[] placedTetrominos = new Label[3000];
        Point[] occupiedPlaces = new Point[1472];
        Random rand = new Random();
        int score = 0;
        int nth = 0;
        #endregion


        public Form1()
        {
            InitializeComponent();
        }

        private bool MyContains(Point[] array, Point point)
        {
            foreach (Point p in array)
            {
                if (p == point)
                    return true;
            }
            return false;
        }

        private void FillAllTetrominos()
        {
            for (int i = 0; i < 300; i++)
                allTetrominos[i] = rand.Next(1, 8);
        }

        private void RemoveTetromino()
        {
            for (int i = 0; i < 4; i++)
            {
                panel1.Controls.Remove(currentTetromino[i]);
            }

        }

        private void ClearMap()
        {
            for (int i = 0; i < placedTetrominos.Length; i++)
            {
                panel1.Controls.Remove(placedTetrominos[i]);
                placedTetrominos[i] = null;
            }

            for (int i = 0; i < occupiedPlaces.Length; i++)
                occupiedPlaces[i] = new Point(600, 600);

            currentNumberOfTiles = 0;
            occupiedRowCounter = 0;
            score = 0;
        }

        private void SaveOccupiedPoints()
        {
            for (int i = currentNumberOfTiles; i < currentNumberOfTiles + 4; i++)
            {
                int x1 = currentTetromino[i - (currentNumberOfTiles)].Location.X;
                int y1 = currentTetromino[i - (currentNumberOfTiles)].Location.Y;
                occupiedPlaces[i] = new Point(x1, y1);
                RemoveTetromino();
                placedTetrominos[i] = AddLabelParticle(x1, y1, color);

            }
            currentNumberOfTiles += 4;
        }

        private void IsThereTetris()
        {
            for (int y = 440; y > 0; y -= 20)
            {
                occupiedRowCounter = 0;
                for (int x = 0; x < 320; x += 20)
                {
                    if (MyContains(occupiedPlaces, new Point(x, y)))
                    {
                        occupiedRowCounter += 1;
                        if (occupiedRowCounter == 16)
                        {
                            score += 100;
                            for (int i = 0; i < occupiedPlaces.Length; i++)
                            {
                                int y1 = occupiedPlaces[i].Y;
                                int x1 = occupiedPlaces[i].X;

                                if (y1 == y)
                                {
                                    x1 = 361;
                                    y1 = 461;
                                }
                                else if (y1 < y)
                                    y1 += labelVelocity;
                                occupiedPlaces[i] = new Point(x1, y1);
                            }

                            for (int i = 0; i < currentNumberOfTiles; i++)
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
                            x = 320;
                            y = 460;
                        }
                        
                    }

                }
            }
        }

        private Label AddLabelParticle(int x1, int y1, Color _color)
        {

            Label particle = new Label
            {
                Location = new Point(x1, y1),
                Size = new Size(labelSize, labelSize),
                BackColor = _color,
            };

            panel1.Controls.Add(particle);
            return particle;
        }

        private void GenerateTetromino()
        {
            if (nth == 300)
            {
                nth = 0;
                FillAllTetrominos();
            }
            Color color = GenerateColor();
            switch (allTetrominos[nth])
            {
                case 1:

                    tetromino1[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino1[1] = AddLabelParticle(tetromino1[0].Location.X, tetromino1[0].Location.Y + labelSize, color);
                    tetromino1[2] = AddLabelParticle(tetromino1[1].Location.X + labelSize, tetromino1[1].Location.Y, color);
                    tetromino1[3] = AddLabelParticle(tetromino1[2].Location.X, tetromino1[1].Location.Y + labelSize, color);
                    currentTetromino = tetromino1;

                    break;
                case 2:

                    tetromino2[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino2[1] = AddLabelParticle(tetromino2[0].Location.X, tetromino2[0].Location.Y + labelSize, color);
                    tetromino2[2] = AddLabelParticle(tetromino2[1].Location.X - labelSize, tetromino2[1].Location.Y, color);
                    tetromino2[3] = AddLabelParticle(tetromino2[2].Location.X, tetromino2[2].Location.Y + labelSize, color);
                    currentTetromino = tetromino2;

                    break;
                case 3:

                    tetromino3[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino3[1] = AddLabelParticle(tetromino3[0].Location.X, tetromino3[0].Location.Y + labelSize, color);
                    tetromino3[2] = AddLabelParticle(tetromino3[1].Location.X - labelSize, tetromino3[1].Location.Y, color);
                    tetromino3[3] = AddLabelParticle(tetromino3[2].Location.X, tetromino3[2].Location.Y - labelSize, color);
                    currentTetromino = tetromino3;

                    break;
                case 4:

                    tetromino4[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino4[1] = AddLabelParticle(tetromino4[0].Location.X + labelSize, tetromino4[0].Location.Y, color);
                    tetromino4[2] = AddLabelParticle(tetromino4[1].Location.X + labelSize, tetromino4[1].Location.Y, color);
                    tetromino4[3] = AddLabelParticle(tetromino4[2].Location.X - labelSize, tetromino4[2].Location.Y + labelSize, color);
                    currentTetromino = tetromino4;

                    break;
                case 5:

                    tetromino5[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino5[1] = AddLabelParticle(tetromino5[0].Location.X, tetromino5[0].Location.Y + labelSize, color);
                    tetromino5[2] = AddLabelParticle(tetromino5[1].Location.X, tetromino5[1].Location.Y + labelSize, color);
                    tetromino5[3] = AddLabelParticle(tetromino5[2].Location.X + labelSize, tetromino5[2].Location.Y, color);
                    currentTetromino = tetromino5;

                    break;
                case 6:

                    tetromino6[0] = AddLabelParticle(x * labelSize, y * labelSize, color);
                    tetromino6[1] = AddLabelParticle(tetromino6[0].Location.X, tetromino6[0].Location.Y + labelSize, color);
                    tetromino6[2] = AddLabelParticle(tetromino6[1].Location.X, tetromino6[1].Location.Y + labelSize, color);
                    tetromino6[3] = AddLabelParticle(tetromino6[2].Location.X - labelSize, tetromino6[2].Location.Y, color);
                    currentTetromino = tetromino6;

                    break;
                case 7:

                    for (int i = 0; i < 4; i++)
                    {
                        tetromino7[i] = AddLabelParticle(x * labelSize, y + (i * labelSize), color);
                    }
                    currentTetromino = tetromino7;

                    break;
            }

            // Show next piece
            switch (allTetrominos[nth + 1])
            {
                case 1: pictureBox1.Image = Image.FromFile(@"C:/Tetris/S.png"); break;
                case 2: pictureBox1.Image = Image.FromFile(@"C:/Tetris/Z.png"); break;
                case 3: pictureBox1.Image = Image.FromFile(@"C:/Tetris/O.png"); break;
                case 4: pictureBox1.Image = Image.FromFile(@"C:/Tetris/T.png"); break;
                case 5: pictureBox1.Image = Image.FromFile(@"C:/Tetris/L.png"); break;
                case 6: pictureBox1.Image = Image.FromFile(@"C:/Tetris/J.png"); break;
                case 7: pictureBox1.Image = Image.FromFile(@"C:/Tetris/I.png"); break;
            }
            nth += 1;
        }



        private Color GenerateColor()
        {

            switch (rand.Next(1, 6))
            {
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Blue;
                    break;
                case 3:
                    color = Color.Yellow;
                    break;
                case 4:
                    color = Color.Lime;
                    break;
                case 5:
                    color = Color.Purple;
                    break;

            }
            return color;
        }

        private bool IsLeftMovable()
        {
            for (int i = 0; i < 4; i++)
            {
                int x1 = currentTetromino[i].Location.X;
                int y1 = currentTetromino[i].Location.Y;
                if (x1 - labelVelocity >= 0 && !MyContains(occupiedPlaces, new Point(x1 - labelVelocity, y1)))
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

        private void MoveDown()
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

            if (!IsDownMovable())
            {
                SaveOccupiedPoints();
                IsThereTetris();
                GenerateTetromino();

                if (!IsDownMovable())
                {
                    timer1.Enabled = false;
                    startGame = false;
                    MessageBox.Show("Game Over");

                }

            }
            MoveDown();

        }

        private void Rotate()
        {
            int offTheBoundsRightCounter = 0;
            int offTheBoundsLeftCounter = 0;
            int offTheBoundsDownCounter = 0;
            
            if (currentTetromino != tetromino3)  // We dont rotate O because it has no center to rotate around
            {
                for (int i = 0; i < 4; i++)
                {
                    int x1 = currentTetromino[i].Location.X;
                    int y1 = currentTetromino[i].Location.Y;
                   
                    double angle = 90;
                    angle = angle * (Math.PI / 180);  // We need to convert the angle to radians
                    Point center = new Point(currentTetromino[1].Location.X, currentTetromino[1].Location.Y);


                    int x2 = (int)(Math.Cos(angle) * (x1 - center.X) - Math.Sin(angle) * (y1 - center.Y) + center.X);  // Rotation about a point anti-clockwise formula
                    int y2 = (int)(Math.Sin(angle) * (x1 - center.X) + Math.Cos(angle) * (y1 - center.Y) + center.Y);

                    if (x2 < 0)
                        offTheBoundsLeftCounter += 1;
                    else if (x2 > 310)
                        offTheBoundsRightCounter += 1;
                    else if (y2 > 450)
                        offTheBoundsDownCounter += 1;
                    
                    if (MyContains(occupiedPlaces, new Point(x2, y2)))
                    {
                        rotateBack = true;
                    }
                    
                        currentTetromino[i].Location = new Point(x2, y2);
                }
                if (rotateBack)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int x1 = currentTetromino[i].Location.X;
                        int y1 = currentTetromino[i].Location.Y;
                        double angle = -90;
                        angle = angle * (Math.PI / 180);  // We need to convert the angle to radians
                        Point center = new Point(currentTetromino[1].Location.X, currentTetromino[1].Location.Y);


                        int x2 = (int)(Math.Cos(angle) * (x1 - center.X) - Math.Sin(angle) * (y1 - center.Y) + center.X);  // Rotation about a point anti-clockwise formula
                        int y2 = (int)(Math.Sin(angle) * (x1 - center.X) + Math.Cos(angle) * (y1 - center.Y) + center.Y);
                        currentTetromino[i].Location = new Point(x2, y2);
                    }
                    rotateBack = false;
                }
                



                if (offTheBoundsLeftCounter != 0)
                    for (int i = 0; i < offTheBoundsLeftCounter; i++)
                        MoveRight();
                if (offTheBoundsRightCounter != 0)
                    for (int i = 0; i < offTheBoundsRightCounter; i++)
                        MoveLeft();
                if (offTheBoundsDownCounter != 0)
                    for (int i = 0; i < offTheBoundsDownCounter; i++)
                        MoveUp();

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            startGame = true;
            timer1.Enabled = true;
            timer2.Enabled = true;
            FillAllTetrominos();
            GenerateTetromino();
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!button1.Visible)
            {
                ClearMap();
                startGame = true;
                timer1.Enabled = true;
                RemoveTetromino();
                GenerateTetromino();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            score++;
            label1.Text = "Total Points: " + score.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (startGame)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        MoveLeft();
                        break;
                    case Keys.Right:
                        MoveRight();
                        break;
                    case Keys.Down:
                        MoveDown();
                        break;
                    case Keys.Up:
                        Rotate();
                        break;
                }
            }
        }

        private void button2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}
