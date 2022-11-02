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




        public Form1()
        {
            InitializeComponent();
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
            
            //Grid Layout
            //Picture Box For Player Graphics draw image for ( Missile, Enemies, Bomb) -Enemies in a List and add the missiles to an array or list to limit how many at once.
            


        }

     


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ////MessageBox.Show($"{e.KeyCode}");
            //if (e.KeyCode == Keys.NumPad5)
            //{
            //    enemies.Clear();

            //}
           

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
            else { MessageBox.Show("OUT of missiles!"); }
            //
            //canShoot = !canShoot;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox1.Text = controller.MissileCount().ToString();
            toolStripTextBox1.Text = $"Score: {controller.Score}";
            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.RunGame();
            controller.DrawObjects();
            if (controller.GameOver == true)
            {
                timer1.Enabled = false;
                //MessageBox.Show("Game Over");
                panel3.Show();
            }




            graphics.DrawImage(bufferImage, 0, 0);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            timer1.Enabled = true;
            pictureBox1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Show();

        }


    }
    
}