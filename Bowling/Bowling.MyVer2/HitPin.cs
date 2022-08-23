using System.Collections;

namespace Bowling.MyVer2
{
    public struct HitPin
    {
        public static readonly int MAX_HIT_PIN = 10;

        public static implicit operator HitPin(int hitPin) => new(hitPin);


        public readonly int _pin;

        public bool IsStrike => _pin == 10;

        public HitPin(int pin)
        {
            if (pin < 0 || 10 < pin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(pin={pin})");
            _pin = pin;
        }

        public override string ToString()
        {
            return _pin.ToString();
        }



        public struct HitPins : IEnumerable<HitPin>
        {

            private readonly IReadOnlyList<HitPin> _hitPins;

            internal HitPins Add(HitPin currentHitPin)
            {
                return new HitPins(_hitPins.Concat(new HitPin[] { currentHitPin }));
            }

            public int Count => _hitPins.Count;

            public bool IsSpare => Count == 2 && Sum() == MAX_HIT_PIN;

            public bool IsStrike => Count == 1 && Sum() == MAX_HIT_PIN;

            public bool IsComplete => IsStrike || IsSpare || Count == 2;

            public HitPin this[int i] => _hitPins[i];


            public HitPins()
            {
                _hitPins = new HitPin[] { };
            }

            public HitPins(IReadOnlyList<HitPin> hitPins)
            {
                _hitPins = hitPins;
            }

            public HitPins(IEnumerable<HitPin> hitPins) : this(hitPins.ToArray()) { }


            public int Sum()
            {
                return _hitPins.Sum(x => x._pin);
            }

            public IEnumerator<HitPin> GetEnumerator()
            {
                return _hitPins.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return _hitPins.ToMsg();
            }

            internal HitPins Range(int start, int count)
            {
                return new HitPins(_hitPins.Skip(start).Take(count).ToArray());
            }
        }
    }
}
