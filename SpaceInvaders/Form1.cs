using System.IO;
using System.Media;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {//All functionality will be moved to the Controller class//
        private Bitmap bufferImage;
        private bool canShoot;
        private Graphics graphics;
        private Graphics bufferGraphics;
        private Controller controller;
        //All of theese will eventually become part of a Fleet Object
        // private List<Enemy> enemies; //2D array? or keep as a list? new var[4,10] (r,c)
        // private List<Bomb> bombs; //List
        //private List<Missile> missiles; //Array
        /**********************************/
        //Fleet has different fields for each // 3 constructors?
        private Random rand;
        private Rectangle boundary;
        private StreamWriter sw;
        private List<string> scores;
        private SoundPlayer backgroundSound;
        

        public Form1()
        {
            
            InitializeComponent();
            backgroundSound = new SoundPlayer(Properties.Resources.theme);

            backgroundSound.PlayLooping();
            scores = new List<string>();
            sw = new StreamWriter("Highscores.txt", true);
            sw.Close();
            pictureBox1.Hide();
            rand = new Random();
            panel2.Hide();
            canShoot = true;
            graphics = CreateGraphics();
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);     
            //enemies = new List<Enemy>();
            boundary = ClientRectangle;
            controller = new Controller(pictureBox1,boundary, bufferGraphics, rand);

            timer1.Enabled = false;
            menuStrip1.Hide();

            //Grid Layout
            //Picture Box For Player Graphics draw image for ( Missile, Enemies, Bomb) -Enemies in a List and add the missiles to an array or list to limit how many at once.



        }




        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show($"{e.KeyCode}");
            


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled)
            {
                controller.MovePlayer(e.X);

            }


        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {


            if(canShoot && controller.MissileCount() < 15)
            {
                controller.FireMissile();
            }
           
            
            

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"Score: {controller.Score}";
            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.RunGame();
            controller.DrawObjects();
            if (controller.GameOver == true)
            {
                timer1.Enabled = false;
                MessageBox.Show("Test");
                panel3.Show(); //Lose Screen
                panel3.BringToFront();
                restartGameToolStripMenuItem.Enabled = false;
                newGameToolStripMenuItem.Enabled = true;
                SaveScore();
            }
            else if(controller.GameWon == true)
            {
                restartGameToolStripMenuItem.Enabled = false;
                newGameToolStripMenuItem.Enabled = true;
                timer1.Enabled = false;
                panel4.Show(); //Win Screen                               
                panel4.BringToFront();
                SaveScore();
            }




            graphics.DrawImage(bufferImage, 0, 0);

        }
        private void SaveScore()
        {
            sw = new StreamWriter("Highscores.txt", true);
            sw.WriteLine($"Score: {controller.Score}");
            sw.Close();
        }



        private void HighScoreMenu() //reads the scores from the text file and displays the last 5  into a listbox
        {
            highscores.Items.Clear();
            scores.Clear();
            highscores.Items.Add("Highscores!");

            StreamReader sr = new StreamReader("HighScores.txt");
            while (!sr.EndOfStream)
            {
                scores.Add(sr.ReadLine()); //Reads all the entries in the text file into a list.
            }
            scores.Reverse(); //Reverses the list so the most recent are at the top



             for (int i = 0; i < scores.Count; i++)
            {
                if (i < 5)
                {
                    highscores.Items.Add($"<{i + 1}> {scores[i]}"); //DIsplays the First Five entries of the reversed list into the listbox

                }
            }

            sr.Close();
            highscores.Show();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundSound.Stop();
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            timer1.Enabled = true;
            pictureBox1.Show();
            menuStrip1.Show();
            newGameToolStripMenuItem.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            HighScoreMenu();
            panel2.Show();
            panel2.BringToFront();

        }

        private void restartGameToolStripMenuItem_Click(object sender, EventArgs e) //The restart button is available at any time during the game but the scores will not be saved.
        {
            timer1.Enabled = false;
            string message = "Are you Sure you want to restart? \n The Scores for this round will NOT be saved!";
            string caption = "Restart Game?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
               controller = new Controller(pictureBox1, boundary, bufferGraphics, rand); //re-initialising the controller class will restart the game
                panel3.Hide();
                panel4.Hide();
            }
            timer1.Enabled = true;
            
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameToolStripMenuItem.Enabled = false;
            controller = new Controller(pictureBox1, boundary, bufferGraphics, rand); //re-initialising the controller class will restart the game
            panel3.Hide();
            panel4.Hide();
            timer1.Enabled = true;
            restartGameToolStripMenuItem.Enabled = true;
            //menuStrip1.Hide();
        }

        private void quitToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            string message = "Are you Sure you want to Quit? \nIf the game is unfinished, the scores for this round \nwill be lost!";
            string caption = "Quit to Menu?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                controller = new Controller(pictureBox1, boundary, bufferGraphics, rand); //re-initialising the controller class will restart the game
                panel1.Show();
                panel1.BringToFront();
                //menuStrip1.Hide();
                //gameStarted = false;
                menuStrip1.Hide();

            }
            else
            {
                timer1.Enabled = true;
                //menuStrip1.Hide();

            }
        }
    }
    
}