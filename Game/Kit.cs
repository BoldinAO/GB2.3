using Game.Properties;
using System;
using System.Drawing;

namespace MyGame
{
    class Kit : BaseObject
    {
        readonly Image image;
        Random random;

        public Kit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            random = new Random();
            image = Resources.kit;
        }

        /// <summary>
        /// Рисование объекта на форме
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Передать случайное кол-во энергии (0 - 50)
        /// </summary>
        /// <param name="spaceShip">Корабль для пополнения энергии</param>
        public void GiveEnergy(SpaceShip spaceShip)
        {
            spaceShip.ReplenishEnergy(random.Next(0, 50));
        }

        /// <summary>
        /// Обновление объекта на форме
        /// </summary>
        public override void Update() { }
    }
}
