using System;

namespace MyGame
{
    class GameException : Exception
    {
        public GameException(string mes) : base(mes) { }
    }
}
