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
            // TODO: 不正な値をはじきたい（1フレームに11pin以上はいる、とか）
            return new Game(hitPins);
        }


        public override string ToString()
        {
            var frames = new FrameEnumerable(_hitPins);
            return String.Join("|", frames);
        }
    }
}
