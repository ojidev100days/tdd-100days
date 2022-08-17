using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2
{
    internal class Game : IEnumerable<IFrame>
    {
        private int[] _hitPins = new int[0];

        internal int Score => this.Where(x => x.IsComplete).Select(x => x.Score).Sum();

        public Game() { }

        public Game(params int[] hitPins)
        {
            this._hitPins = hitPins;
        }
                

        internal Game ThrowBall(params int[] hitPins)
        {
            // TODO: 不正な値をはじきたい（1フレームに11pin以上はいる、とか）
            return new Game(hitPins);
        }


        public IEnumerator<IFrame> GetEnumerator()
        {
            return new FrameEnumerable(_hitPins).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("|", this);
        }
    }
}
