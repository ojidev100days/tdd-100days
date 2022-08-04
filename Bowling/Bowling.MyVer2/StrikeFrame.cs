namespace Bowling.MyVer2
{
    internal class StrikeFrame : IFrame
    {
        private readonly int[] _hitPins;
        private readonly int _i;

        public bool IsComplete => _i + 2 < _hitPins.Length;

        public int Score => IsComplete ? _hitPins[_i] + _hitPins[_i + 1] + _hitPins[_i + 2] : 0;

        public StrikeFrame(int[] hitPins, int i)
        {
            this._hitPins = hitPins;
            this._i = i;
        }

        public override string ToString()
        {
            // TODO: 10フレーム目は3つ出したい
            return $"[{_hitPins[_i]}]";
        }

    }
}
