using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2
{
    internal class Game
    {
        private readonly int[] _hitPins;

        internal IReadOnlyList<IFrame> Frames { get; }

        internal int Score => Frames.TakeWhile(x => x.IsComplete).Sum(x => x.Score);

        public Game() {
            _hitPins = new int[0];
            Frames = new List<IFrame>();
        }

        public Game(params int[] hitPins)
        {
            this._hitPins = hitPins;
            this.Frames = new FrameEnumerable(_hitPins).ToList();
        }
                

        internal Game ThrowBall(params int[] hitPins)
        {
            if (Frames.LastOrDefault() is LastFrame lastFrame && lastFrame.IsComplete) throw new BowlingAppException("The game is already over.");
            return new Game(_hitPins.Concat(hitPins).ToArray());
        }


        public override string ToString()
        {
            return string.Join("|", Frames);
        }
    }
}
