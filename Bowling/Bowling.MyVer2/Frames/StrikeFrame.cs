using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal class StrikeFrame : IFrame
    {
        public static readonly int MaxThrowCount = 1;
        private static readonly int ThrowCountOfIncludedInScore = 2;


        private readonly HitPins _hitPins;
        private readonly int _currentIndex;

        public bool IsComplete => ScorePins.ThrowCount == MaxThrowCount + ThrowCountOfIncludedInScore;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_currentIndex, MaxThrowCount);

        public HitPins ScorePins => _hitPins.Range(_currentIndex, MaxThrowCount + ThrowCountOfIncludedInScore);


        public StrikeFrame(HitPins hitPins, int i)
        {
            _hitPins = hitPins;
            _currentIndex = i;
        }

        public override string ToString()
        {
            return KnockedDownPins.ToString();
        }

    }
}
