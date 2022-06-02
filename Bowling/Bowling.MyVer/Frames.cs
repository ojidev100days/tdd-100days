using System.Collections;
using System.Collections.ObjectModel;

namespace Bowling.MyVer
{
    internal class Frames : IEnumerable<Frame>
    {
        private static readonly ReadOnlyCollection<Frame> _emptyFrames = new ReadOnlyCollection<Frame>(new Frame[] { new Frame() });
        private readonly ReadOnlyCollection<Frame> _frames;

        public int Count => _frames.Count;
        public Frame Current => _frames[_frames.Count - 1];

        public int Score
        {
            get { return _frames.Sum(x => x.Score); }
        }

        public Frames()
        {
            _frames = _emptyFrames;
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
    }
}
