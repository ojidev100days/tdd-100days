using System.Collections.ObjectModel;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class NormalFrame : IFrame
    {

        private readonly HitPins _hitPinsInFrame;


        public bool IsComplete => _hitPinsInFrame.Count == 2;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPinsInFrame;

        public HitPins ScorePins => _hitPinsInFrame;

        public NormalFrame(HitPins hitPinsInFrame)
        {
            if (HitPin.MAX_HIT_PIN <= hitPinsInFrame.Sum()) throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(hitPinsInFrame={hitPinsInFrame})");
            this._hitPinsInFrame = hitPinsInFrame;
        }


        public override string ToString()
        {
            return KnockedDownPins.ToString();
        }

    }
}
