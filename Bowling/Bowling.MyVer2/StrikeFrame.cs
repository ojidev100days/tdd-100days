using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class StrikeFrame : IFrame
    {
        private readonly HitPins _hitPins;
        private readonly int _currentIndex;

        public bool IsComplete => _currentIndex + 2 < _hitPins.Count;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_currentIndex, 1);

        public HitPins ScorePins => _hitPins.Range(_currentIndex, 3);

        public StrikeFrame(HitPins hitPins, int i)
        {
            this._hitPins = hitPins;
            this._currentIndex = i;
        }

        public override string ToString()
        {
            return KnockedDownPins.ToString();
        }

    }
}
