using System.Collections.ObjectModel;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal class LastFrame : IFrame
    {
        private IFrame _innerFrame;

        public bool IsComplete => _innerFrame.IsComplete;

        public int Score => _innerFrame.Score;

        public HitPins KnockedDownPins => _innerFrame.ScorePins;

        public HitPins ScorePins => _innerFrame.ScorePins;

        public LastFrame(IFrame innerFrame)
        {
            _innerFrame = innerFrame;
        }

        public override string ToString()
        {
            return $"[{string.Join(",", ScorePins)}]";
        }
    }
}
