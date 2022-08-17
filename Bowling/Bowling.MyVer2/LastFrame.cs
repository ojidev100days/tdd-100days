using System.Collections.ObjectModel;

namespace Bowling.MyVer2
{
    internal class LastFrame : IFrame
    {
        private IFrame _innerFrame;

        public bool IsComplete => true;

        public int Score => _innerFrame.Score;

        public int[] KnockedDownPins => _innerFrame.ScorePins;

        public int[] ScorePins => _innerFrame.ScorePins;

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
