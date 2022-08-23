using System.Collections;
using Bowling.MyVer2.Frames;
using Bowling.MyVer2.Utils;

namespace Bowling.MyVer2
{
    public struct HitPin
    {
        public static readonly int MinHitPin = 0;
        public static readonly int MaxHitPin = 10;

        public static implicit operator HitPin(int hitPin) => new(hitPin);

        public readonly int _pin;

        public bool IsStrike => _pin == MaxHitPin;

        public HitPin(int pin)
        {
            if (pin < MinHitPin || MaxHitPin < pin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(pin={pin})");
            _pin = pin;
        }

        public override string ToString()
        {
            return _pin.ToString();
        }

        public struct HitPins : IEnumerable<HitPin>
        {
            public static HitPins Of(params int[] hitPins)
            {
                return new HitPins(hitPins.Select(x => (HitPin)x).ToArray());
            }

            private readonly IReadOnlyList<HitPin> _hitPins;

            public int ThrowCount => _hitPins.Count;

            public bool IsSpare => ThrowCount == SpareFrame.MaxThrowCount && Sum() == MaxHitPin;

            public bool IsStrike => ThrowCount == StrikeFrame.MaxThrowCount && Sum() == MaxHitPin;

            public bool IsComplete => IsStrike || IsSpare || ThrowCount == NormalFrame.MaxThrowCount;

            public HitPin this[int i] => _hitPins[i];

            public HitPins()
            {
                _hitPins = Array.Empty<HitPin>();
            }

            public HitPins(params HitPin[] hitPins)
            {
                _hitPins = hitPins;
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

            internal HitPins Add(HitPin hitPin)
            {
                return Concat(new HitPins(hitPin));
            }

            internal HitPins Concat(HitPins concatHitPins)
            {
                return new HitPins(_hitPins.Concat(concatHitPins));
            }

            public IEnumerator<HitPin> GetEnumerator()
            {
                return _hitPins.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            internal HitPins Range(int start, int count)
            {
                return new HitPins(_hitPins.Skip(start).Take(count).ToArray());
            }

            public override string ToString()
            {
                return _hitPins.ToStr();
            }
        }
    }
}
