using System.Collections.ObjectModel;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class SpareFrame : IFrame
    {
        private readonly HitPins _hitPins;
        private readonly int _currentIndex;

        public bool IsComplete => (_currentIndex + 2) < _hitPins.Count;

        // スペア後の投球があれば「フレーム内のピン＋後の投球」がをスコアとする
        // スペア後の投球がなければ、スコアは0（未確定）
        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public HitPins KnockedDownPins => _hitPins.Range(_currentIndex, 2);

        public HitPins ScorePins => _hitPins.Range(_currentIndex, 3);

        public SpareFrame(HitPins hitPins, int i)
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
