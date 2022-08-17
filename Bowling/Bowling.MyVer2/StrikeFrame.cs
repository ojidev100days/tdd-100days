namespace Bowling.MyVer2
{
    internal class StrikeFrame : IFrame
    {
        private readonly IReadOnlyList<int> _hitPins;
        private readonly int _i;

        public bool IsComplete => _i + 2 < _hitPins.Count;

        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public int[] KnockedDownPins => new int[] { _hitPins[_i] };

        public int[] ScorePins => _hitPins.Skip(_i).Take(3).ToArray();

        public StrikeFrame(int[] hitPins, int i)
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
