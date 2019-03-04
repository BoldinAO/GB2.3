using System.Drawing;

namespace MyGame
{
    class Square : BaseObject
    {
        public Square(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Рисование объекта на форме
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Black, Pos.X, Pos.Y - 400, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawRectangle(Pens.Black, Pos.X, Pos.Y - 100 + Size.Height, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление объекта на форме
        /// </summary>
        public override void Update()
        {
            Pos.Y = Pos.Y - 200;
            if (Pos.Y < 0) Pos.Y = Game.Height;
        }
    }
}
