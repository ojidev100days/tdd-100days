using System.Collections.ObjectModel;

namespace Bowling.MyVer2
{
    internal class NormalFrame : IFrame
    {
        private readonly IReadOnlyList<int> _hitPinsInFrame;


        public bool IsComplete => _hitPinsInFrame.Count == 2;

        public int Score => ScorePins.Sum();

        public int[] KnockedDownPins => _hitPinsInFrame.ToArray();

        public int[] ScorePins => _hitPinsInFrame.ToArray();

        public NormalFrame(IReadOnlyList<int> hitPinsInFrame)
        {
            if (10 <= hitPinsInFrame.Sum()) throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(hitPinsInFrame={hitPinsInFrame.ToMsg()})");
            this._hitPinsInFrame = hitPinsInFrame;
        }


        public override string ToString()
        {
            return $"[{_hitPinsInFrame[0]},{_hitPinsInFrame[1]}]";
        }

    }
}
