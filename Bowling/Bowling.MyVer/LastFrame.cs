namespace Bowling.MyVer
{


    internal class LastFrame : IFrame
    {
        private readonly int[] _pins;

        public int ThrowCount => _pins.Length;

        public IEnumerable<int> HitPins => _pins;

        public bool CanBeAddPin
        {
            get
            {
                if (_pins.Length == 0) return true;

                if (IsStrike || IsSpare)
                {
                    return _pins.Length < 3;
                }
                else
                {
                    return _pins.Length < 2;
                }
            }
        }


        public bool IsComplite
        {
            get
            {
                if (IsStrike || IsSpare) return 3 <= _pins.Length;
                return 2 <= _pins.Length;

            }
        }

        public bool IsStrike => 1 <= _pins.Length && _pins[0] == 10;

        public bool IsSpare => 2 <= _pins.Length && (_pins[0] + _pins[1]) == 10;

        public int PinCount
        {
            get
            {
                if (IsStrike) return _pins[0];
                if (IsSpare) return _pins.Take(2).Sum();
                return _pins.Sum();
            }
        }


        public LastFrame(params int[] pins)
        {
            // TODO pins の Length は 0 ～ 3 のチェックが必要
            this._pins = pins;
        }

        public IFrame Add(int hitPin)
        {
            if (!CanBeAddPin) throw new BowlingAppException($"Cannot be added. frame={this}, hitPin={hitPin}");

            if (!IsStrike && !IsSpare)
            {
                if (10 < _pins.Sum(x => x) + hitPin)
                {
                    throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(frame=[{this}], hitPin={hitPin})");
                }
            }


            var newPins = new int[_pins.Length + 1];
            _pins.CopyTo(newPins, 0);
            newPins[_pins.Length] = hitPin;
            return new LastFrame(newPins);
        }

        public override string ToString()
        {
            return "LF=[" + string.Join(",", _pins) + "]";
        }
    }
}
