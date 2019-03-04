using System.Drawing;

namespace MyGame
{
    class Asteroid : BaseObject
    {
        //Так и не понял для чего этот бред
        //public int Power { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            //Power = 1;
            Name = $"Астероид";
        }

        /// <summary>
        /// Рисование объекта на форме
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление объекта на форме
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}