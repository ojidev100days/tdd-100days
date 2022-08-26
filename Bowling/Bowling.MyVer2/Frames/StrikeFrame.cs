using System.Diagnostics.CodeAnalysis;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal class StrikeFrame : IFrame
    {
        private static readonly int MaxThrowCount = 1;
        private static readonly int ThrowCountOfIncludedInScore = 2;


        public static bool TryCreate(HitPins hitPins, int currentIndex, [MaybeNullWhen(false)] out StrikeFrame result)
        {
            var currentPin = hitPins[currentIndex];

            if (currentPin.AllDowned)
            {
                result = new StrikeFrame(hitPins, currentIndex);
                return true;
            }

            result = null;
            return false;
        }

        private readonly HitPins _hitPins;
        private readonly int _currentIndex;

        public bool IsComplete => ScorePins.ThrowCount == MaxThrowCount + ThrowCountOfIncludedInScore;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_currentIndex, MaxThrowCount);

        public HitPins ScorePins => _hitPins.Range(_currentIndex, MaxThrowCount + ThrowCountOfIncludedInScore);


        public StrikeFrame(HitPins hitPins, int currentIndex)
        {
            _hitPins = hitPins;
            _currentIndex = currentIndex;
        }

        public override string ToString()
        {
            return KnockedDownPins.ToString();
        }
    }
}
