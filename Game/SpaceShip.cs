using Game.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    class SpaceShip : BaseObject
    {
        /// <summary>
        /// Подсчет сбитых астероидов
        /// </summary>
        public int ShotAsteroidsCount { get; private set; }

        readonly Image image;

        public SpaceShip(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Resources.spaceship;
            Name = $"Корабль";
            ShotAsteroidsCount = 0;
        }
        
        /// <summary>
        /// Запас энергии корабля
        /// </summary>
        public int SpaceShipEnergy { get; private set; }

        /// <summary>
        /// Рисование объекта на форме
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление объекта на форме
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

        /// <summary>
        /// Подсчет сбитых астероидов
        /// </summary>
        /// <param name="obj">сбитый объект</param>
        public void ShotAsteroids(object obj)
        {
            ShotAsteroidsCount = obj is Asteroid ? ShotAsteroidsCount++ : ShotAsteroidsCount;
        }

        /// <summary>
        /// Пополнение запаса энергии
        /// </summary>
        /// <param name="energy">кол-во энергии для пополнения</param>
        public void ReplenishEnergy(int energy)
        {
            SpaceShipEnergy += SpaceShipEnergy >= 100 ? 0 : SpaceShipEnergy + energy > 100 ? 100 : energy;
        }
    }
}
