using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static SpaceShip _spaceShip;
        private static Asteroid[] _asteroids;
        private static Kit _kit;

        public static void Load()
        {
            _objs = new BaseObject[30];
            _spaceShip = new SpaceShip(new Point(0, 200), new Point(5, 0), new Size(50, 20));
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length - 1; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
            
            //Вынес 1 астероид чтоб не ждать когда великий бог рандома даст проверить работоспособность
            _asteroids[2] = new Asteroid(new Point(1000, 180), new Point(-20 / 5, 10), new Size(50, 50));

            _kit = new Kit(new Point(300, 200), new Point(5, 0), new Size(50, 20));

            _spaceShip.Write += Log.Write;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            // Графическое устройство для вывода графики  
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
        }

        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet?.Draw();
            _spaceShip.Draw();
            _kit.Draw();
            Buffer.Render();
        }


        
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_spaceShip))
                {
                    System.Media.SystemSounds.Hand.Play();
                    a.Repaint();
                    _bullet?.Repaint();
                    _spaceShip?.Repaint();
                    _spaceShip.AnyDo(a);
                    _spaceShip.ShotAsteroids(a);
                }
            }
            if (_kit.Collision(_spaceShip))
            {
                _kit.Repaint();
                _kit.GiveEnergy(_spaceShip);
            }
            _bullet?.Update();
            _spaceShip.Update();

            //if (Game.Width > 1000 || Game.Height > 1000)
            //    throw new ArgumentOutOfRangeException("Высота и ширина экрана должны быть меньше 1000");
        }
    }
}
