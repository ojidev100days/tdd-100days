using System.Collections;
using Bowling.MyVer2.Utils;

namespace Bowling.MyVer2
{
    public struct HitPins : IEnumerable<HitPin>
    {
        public static HitPins Of(params int[] hitPins)
        {
            return new HitPins(hitPins.Select(x => (HitPin)x).ToArray());
        }

        private readonly IReadOnlyList<HitPin> _hitPins;

        public int ThrowCount => _hitPins.Count;

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
            return _hitPins.Sum(x => x.Value);
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
