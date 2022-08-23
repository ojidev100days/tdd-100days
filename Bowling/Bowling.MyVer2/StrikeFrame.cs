using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class StrikeFrame : IFrame
    {
        private readonly HitPins _hitPins;
        private readonly int _i;

        public bool IsComplete => _i + 2 < _hitPins.Count;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_i, 1);

        public HitPins ScorePins => _hitPins.Range(_i, 3);

        public StrikeFrame(HitPins hitPins, int i)
        {
            this._hitPins = hitPins;
            this._i = i;
        }

        public override string ToString()
        {
            return $"[{_hitPins[_i]}]";
        }

    }
}
