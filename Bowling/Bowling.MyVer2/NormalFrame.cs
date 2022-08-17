using System.Collections.ObjectModel;

namespace Bowling.MyVer2
{
    internal class NormalFrame : IFrame
    {
        private IReadOnlyList<int> _hitPinsInFrame;


        public bool IsComplete => true;

        public int Score => ScorePins.Sum();

        public int[] KnockedDownPins => _hitPinsInFrame.ToArray();

        public int[] ScorePins => _hitPinsInFrame.ToArray();

        public NormalFrame(IReadOnlyList<int> hitPinsInFrame)
        {
            this._hitPinsInFrame = hitPinsInFrame;
        }


        public override string ToString()
        {
            return $"[{_hitPinsInFrame[0]},{_hitPinsInFrame[1]}]";
        }
    }
}
