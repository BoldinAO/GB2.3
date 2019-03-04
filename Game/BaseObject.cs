using System;
using System.Drawing;
using System.IO;

namespace MyGame
{
    abstract class BaseObject : ICollision
    {
        //Точка на карте
        protected Point Pos;
        //
        protected Point Dir;
        //Размер
        protected Size Size;

        /// <summary>
        /// Имя для объекта
        /// </summary>
        public string Name { get; set; }

        protected BaseObject(Point pos, Point dir, Size size)
        {
            //if (size.Height > 10 && this is Asteroid) throw new GameException("Высота астероида должна быть < 10");
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        /// <summary>
        /// Рисование объекта на форме
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Обновление объекта на форме
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Прямоугольние
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

        /// <summary>
        /// Проверка на столкновение с объектом
        /// </summary>
        /// <param name="o">Объект для проверки</param>
        /// <returns>true/false</returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Определение следующей позиции объекта
        /// </summary>
        public void Repaint()
        {
            Random random = new Random();
            Pos.X = this is Asteroid ? Game.Width : 0;
            if (this is Asteroid) Pos.X = Game.Width;
            if (this is Kit)
            {
                Pos.X = random.Next(0, Game.Width);
                Pos.Y = random.Next(0, Game.Height);
            }
        }

        //Делегат для события подписки
        public delegate void subscription(string obj);

        //Событие для подписки
        public event subscription Write;

        /// <summary>
        /// Логирование событий
        /// </summary>
        /// <param name="obj">объект для записи в лог</param>
        public void AnyDo(BaseObject obj) { Write?.Invoke($"{this.Name} столкнулся с {obj.Name}"); }
    }
}
