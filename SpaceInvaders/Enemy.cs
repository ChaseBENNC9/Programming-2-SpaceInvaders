public class Enemy
{
    private Point position;
    private Graphics graphics;
    private Image image;
    private Rectangle collider;
    private bool destroyed;
    private bool canShoot;
    public Enemy(Point position, Graphics graphics,Image image)
    {
        this.position = position;
        this.graphics = graphics;
        this.image = image;
        collider = new Rectangle(position.X, position.Y, 25, 25);
        destroyed = false;
        canShoot = false;
    }

    public Point Position { get => position; set => position = value; }
    public Rectangle Collider { get => collider; set => collider = value; }
    public bool Destroyed { get => destroyed; set => destroyed = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }

    public void Draw()
    {
        graphics.DrawImage(image, position.X, position.Y, 25, 25);
        if (canShoot)
        {
            graphics.DrawRectangle(Pens.Green, position.X, position.Y, 25, 25);

        }
        else
        {
            graphics.DrawRectangle(Pens.Red, position.X, position.Y, 25, 25);

        }

    }

    public void Move(int speed)
    {
        collider.X = position.X;
        collider.Y = position.Y;
        position.X += speed;
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