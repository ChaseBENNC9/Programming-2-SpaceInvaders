public class Enemy
{
    private Point position;
    private Graphics graphics;
    private Image image;
    public Enemy(Point position, Graphics graphics,Image image)
    {
        this.position = position;
        this.graphics = graphics;
        this.image = image;
    }

    public Point Position { get => position; set => position = value; }

    public void Draw()
    {
        graphics.DrawImage(image, Position.X, Position.Y, 25, 25);
        graphics.DrawRectangle(Pens.Red, Position.X, Position.Y, 25, 25);
    }

    public void Move(int speed)
    {
        position.X += speed;
    }

    public void ShiftLevel()
    {
        position.Y += 20;
    }
}