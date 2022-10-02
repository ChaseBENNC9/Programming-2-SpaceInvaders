public class Enemy
{
    private Point position;
    private Graphics graphics;
    private Image image;
    private Rectangle collider;
    private bool destroyed;
    private bool canShoot;
    private int velocity;
    private int direction;
    public Enemy(Point position,int velocity, Graphics graphics,Image image)
    {
        this.position = position;
        this.graphics = graphics;
        this.velocity = velocity;
        this.image = image;
        collider = new Rectangle(position.X, position.Y, 25, 25);
        destroyed = false;
        canShoot = false;
        direction = 1;
    }

    public Point Position { get => position; set => position = value; }
    public Rectangle Collider { get => collider; set => collider = value; }
    public bool Destroyed { get => destroyed; set => destroyed = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    public int Velocity { get => velocity; set => velocity = value; }
    public int Direction { get => direction; set => direction = value; }

    public void Draw()
    {
        graphics.DrawImage(image, position.X, position.Y, 32, 32);
        if (canShoot)
        {
            graphics.DrawRectangle(Pens.Green, position.X, position.Y, 32, 32);

        }
        else
        {
            graphics.DrawRectangle(Pens.Red, position.X, position.Y, 32, 32);

        }

    }

    public void Move()
    {
        collider.X = position.X;
        collider.Y = position.Y;
        position.X += direction*velocity;
    }

    public void ShiftLevel()
    {
        position.Y += 20;
    }

    public void Destroy()
    {
        destroyed = true;
    }
}