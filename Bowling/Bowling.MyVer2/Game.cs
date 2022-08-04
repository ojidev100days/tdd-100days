using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2
{
    internal class Game
    {
        private int[] _hitPins = new int[0];

        public Game() { }

        public Game(params int[] hitPins)
        {
            this._hitPins = hitPins;
        }

        internal int GetScore()
        {
            var frames = new FrameEnumerable(_hitPins);
            return frames.Where(x => x.IsComplete).Select(x => x.Score).Sum();
        }

        internal Game ThrowBall(params int[] hitPins)
        {
            return new Game(hitPins);
        }
    }
}
