using System.Collections.ObjectModel;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class SpareFrame : IFrame
    {
        private readonly HitPins _hitPins;
        private readonly int _i;
        private readonly HitPins _hitPinsInFrame;

        public bool IsComplete => (_i + 1) < _hitPins.Count;

        // スペア後の投球があれば「フレーム内のピン＋後の投球」がをスコアとする
        // スペア後の投球がなければ、スコアは0（未確定）
        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_i - 1, 2);

        public HitPins ScorePins => _hitPins.Range(_i - 1, 3);

        public SpareFrame(HitPins hitPinsInFrame, HitPins hitPins, int i)
        {
            this._hitPinsInFrame = hitPinsInFrame;
            this._hitPins = hitPins;
            this._i = i;
        }

        public override string ToString()
        {
            return $"[{_hitPinsInFrame[0]},{_hitPinsInFrame[1]}]";
        }

    }
}
