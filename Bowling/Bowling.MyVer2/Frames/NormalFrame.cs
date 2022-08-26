using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal class NormalFrame : IFrame
    {
        internal static readonly int MaxThrowCount = 2;

        internal static bool TryCreate(HitPins hitPins, int currentIndex, out IFrame result)
        {
            var framePins = hitPins.Range(currentIndex, 2);
            result = new NormalFrame(framePins);
            return true;
            
        }

        private readonly HitPins _hitPinsInFrame;

        public bool IsComplete => _hitPinsInFrame.ThrowCount == MaxThrowCount;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPinsInFrame;

        public HitPins ScorePins => _hitPinsInFrame;

        public NormalFrame(HitPins hitPinsInFrame)
        {
            if (HitPin.MaxHitPin <= hitPinsInFrame.Sum()) throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(hitPinsInFrame={hitPinsInFrame})");
            _hitPinsInFrame = hitPinsInFrame;
        }


        public override string ToString()
        {
            return KnockedDownPins.ToString();
        }
    }
}
