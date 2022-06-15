using System.Collections;
using System.Collections.ObjectModel;

namespace Bowling.MyVer
{
    internal class Frames : IEnumerable<Frame>
    {
        private static readonly ReadOnlyCollection<Frame> _emptyFrames = new ReadOnlyCollection<Frame>(new Frame[] { new Frame() });
        private readonly ReadOnlyCollection<Frame> _frames;

        public int Count => _frames.Count;
        public Frame Current => _frames[^1];

        public Frame this[int i] => _frames[i];

        public Frames()
        {
            _frames = _emptyFrames;
        }

        internal Frames ChangeCurrentFrame(Frame addedCcurrentFrame)
        {
            return new Frames(_frames.Take(_frames.Count - 1).Concat(new Frame[] { addedCcurrentFrame }));
        }


        internal int GetNumberOfThrowsFrom(int frameIndex)
        {
            int numberOfThrows = 0;
            for (int i = frameIndex+1; i < _frames.Count; i++)
            {
                numberOfThrows += _frames[i].ThrowCount;
            }
            return numberOfThrows;
        }

        internal int GetNumberOfPinsFrom(int frameIndex, int times)
        {
            int numberOfPins = 0;
            var hitPins = GetHitPinsFrom(frameIndex).ToArray();


            for (int i = 0; i < times; i++)
            {
                numberOfPins += hitPins[i];
            }
            return numberOfPins;
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

        internal Frames Add(Frame frame)
        {
            return new Frames(_frames.Concat(new Frame[] { frame }));
        }

        public Frames(IEnumerable<Frame> frames)
        {
            _frames = frames.ToList().AsReadOnly();
        }


        public IEnumerator<Frame> GetEnumerator()
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
