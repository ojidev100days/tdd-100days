using System.Collections;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class FrameEnumerable : IEnumerable<IFrame>
    {
        private const int MAX_FRAME_NO = 10;


        private readonly HitPins _hitPins;

        public FrameEnumerable(HitPin[] hitPins)
        {
            this._hitPins = new HitPins(hitPins);
        }

        public IEnumerator<IFrame> GetEnumerator()
        {
            int currentFrameNo = 0; // 最終フレーム目を判定するために使用する
            var pinsInFrame = new HitPins();

            for (int i = 0; i < _hitPins.Count; i++)
            {
                var currentHitPin = _hitPins[i];

                // 現在倒したPinを保存
                pinsInFrame = pinsInFrame.Add(currentHitPin);

                if (pinsInFrame.IsComplete)
                {
                    currentFrameNo++;
                    if (pinsInFrame.IsStrike)
                    {
                        // Strike
                        yield return CreateFrame(currentFrameNo, () => new StrikeFrame(_hitPins, i));
                    }
                    else if (pinsInFrame.IsSpare)
                    {
                        // Spare
                        yield return CreateFrame(currentFrameNo, () => new SpareFrame(pinsInFrame, _hitPins, i));
                    }
                    else
                    {
                        // Normal
                        yield return CreateFrame(currentFrameNo, () => new NormalFrame(pinsInFrame));
                    }

                    // フレーム数が10を超えた時点で、処理を打ち切り
                    if (MAX_FRAME_NO <= currentFrameNo) yield break;

                    pinsInFrame = new HitPins();
                }
            }

            if (pinsInFrame.Any())
            {
                // Normal
                yield return CreateFrame(currentFrameNo, () => new NormalFrame(pinsInFrame));
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IFrame CreateFrame(int frameCount, Func<IFrame> createFrameFunc)
        {
            return frameCount == MAX_FRAME_NO
                ? new LastFrame(createFrameFunc.Invoke())
                : createFrameFunc.Invoke();
        }
    }
}
