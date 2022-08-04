using System.Collections.ObjectModel;

namespace Bowling.MyVer2
{
    internal class NormalFrame : IFrame
    {
        private ReadOnlyCollection<int> _hitPinsInFrame;


        public bool IsComplete => true;

        public int Score => _hitPinsInFrame.Sum();

        public NormalFrame(ReadOnlyCollection<int> hitPinsInFrame)
        {
            this._hitPinsInFrame = hitPinsInFrame;
        }

    }
}
