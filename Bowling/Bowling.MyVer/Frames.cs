using System.Collections;
using System.Collections.ObjectModel;

namespace Bowling.MyVer
{
    internal class Frames : IEnumerable<IFrame>
    {
        private static readonly ReadOnlyCollection<IFrame> _emptyFrames = new(Array.Empty<IFrame>());
        private readonly ReadOnlyCollection<IFrame> _frames;

        public int Count => _frames.Count;
        public IFrame Current => _frames.Count == 0 ? NormalFrame.Empty : _frames[^1];

        public IFrame this[int i] => _frames[i];

        public Frames()
        {
            _frames = _emptyFrames;
        }

        public Frames(IEnumerable<IFrame> frames)
        {
            _frames = frames.ToList().AsReadOnly();
        }

        public Frames(params IFrame[] frames)
        {
            _frames = new ReadOnlyCollection<IFrame>(frames);
        }

        public Frames(IList<IFrame> frames)
        {
            _frames = new ReadOnlyCollection<IFrame>(frames);
        }


        internal Frames ChangeCurrentFrame(IFrame addedCcurrentFrame)
        {
            return new Frames(_frames.Take(_frames.Count - 1).Concat(new IFrame[] { addedCcurrentFrame }));
        }


        internal int GetNumberOfThrowsFrom(int frameIndex)
        {
            int numberOfThrows = 0;
            if (frameIndex == 9)
            {
                // TODO 間違ったロジック
                // この処理は、Strike or Spear を取った後の投球数を知りたいので、
                // 引数が frameIndex 担っていることが間違い
                // （10 Frame のパターンに対応できていない
                return _frames[frameIndex].ThrowCount - 1;

            } else
            {
                for (int i = frameIndex + 1; i < _frames.Count; i++)
                {
                    numberOfThrows += _frames[i].ThrowCount;
                }
                return numberOfThrows;
            }
        }

        internal int GetNumberOfPinsFrom(int frameIndex, int times)
        {
            int numberOfPins = 0;
            var hitPins = GetHitPinsFrom(frameIndex).ToArray();


            if (frameIndex == 9)
            {
                // TODO 間違ったロジック
                // この処理は、Strike or Spear を取った後の投球数を知りたいので、
                // 引数が frameIndex 担っていることが間違い
                // （10 Frame のパターンに対応できていない
                var lastFrame = _frames[frameIndex];

                if (lastFrame.IsStrike) return lastFrame.HitPins.Skip(1).Take(times).Sum();
                if (lastFrame.IsSpare) return lastFrame.HitPins.Skip(2).Take(times).Sum();
                return 0;

            }
            else
            {
                for (int i = 0; i < times; i++)
                {
                    numberOfPins += hitPins[i];
                }
                return numberOfPins;
            }
        }

        private IEnumerable<int> GetHitPinsFrom(int frameIndex)
        {

            for (int i = frameIndex + 1; i < _frames.Count; i++)
            {
                var frame = _frames[i];
                foreach (var hitPin in frame.HitPins)
                {
                    yield return hitPin;
                }
            }
        }

        internal Frames Add(IFrame frame)
        {
            if (10 <= _frames.Count) throw new BowlingAppException($"Cannot add a frame. frames.Count={_frames.Count}");
            return new Frames(_frames.Concat(new IFrame[] { frame }));
        }


        public IEnumerator<IFrame> GetEnumerator()
        {
            return _frames.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return "[" + string.Join(",", _frames) + "]";
        }
    }
}
