namespace Bowling.MyVer
{
    internal class NormalFrame : IFrame
    {

        public static readonly NormalFrame Empty = new();

        private readonly int[] _pins;

        public NormalFrame(params int[] pins)
        {
            // TODO pins の Length は 0 ～ 3 のチェックが必要
            this._pins = pins;
        }

        public bool CanBeAddPin
        {
            get
            {
                if (_pins.Length == 0) return true;
                if (_pins.Length == 2) return false; // TODO: 10Frameの特殊ケースはまだ
                return _pins[0] != 10; // Strike 以外は追加可能

            }
        }


        public int PinCount => _pins.Sum();

        public bool IsComplite => IsStrike || _pins.Length == 2;

        public bool IsStrike => _pins.Length == 1 && _pins[0] == 10;

        public bool IsSpare => _pins.Length == 2 && _pins.Sum() == 10;

        public int ThrowCount => _pins.Length;

        public IEnumerable<int> HitPins => _pins;


        public IFrame Add(int hitPin)
        {
            if (!CanBeAddPin) throw new BowlingAppException($"Cannot be added. frame={this}, hitPin={hitPin}");

            if (10 < _pins.Sum(x => x) + hitPin)
            {
                throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(frame=[{this}], hitPin={hitPin})");
            }

            var newPins = new int[_pins.Length + 1];
            _pins.CopyTo(newPins, 0);
            newPins[_pins.Length] = hitPin;
            return new NormalFrame(newPins);
        }

        public override string ToString()
        {
            return "NF=[" + string.Join(",", _pins) + "]";
        }
    }
}
