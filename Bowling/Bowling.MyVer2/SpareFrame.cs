using System.Collections.ObjectModel;

namespace Bowling.MyVer2
{
    internal class SpareFrame : IFrame
    {
        private readonly int[] _hitPins;
        private readonly int _i;
        private readonly IReadOnlyList<int> _hitPinsInFrame;

        public bool IsComplete => (_i + 1) < _hitPins.Length;

        // スペア後の投球があれば「フレーム内のピン＋後の投球」がをスコアとする
        // スペア後の投球がなければ、スコアは0（未確定）
        public int Score => IsComplete ? ScorePins.Sum() : 0;

        public int[] KnockedDownPins => _hitPins.Skip(_i - 1).Take(2).ToArray();

        public int[] ScorePins => _hitPins.Skip(_i - 1).Take(3).ToArray();

        public SpareFrame(IReadOnlyList<int> hitPinsInFrame, int[] hitPins, int i)
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
