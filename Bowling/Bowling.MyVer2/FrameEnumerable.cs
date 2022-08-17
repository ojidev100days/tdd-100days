using System.Collections;

namespace Bowling.MyVer2
{
    internal class FrameEnumerable : IEnumerable<IFrame>
    {
        private int[] hitPins;

        public FrameEnumerable(int[] hitPins)
        {
            this.hitPins = hitPins;
        }

        public IEnumerator<IFrame> GetEnumerator()
        {
            int frameCount = 1;// 10フレーム目を判定するために使用する
            List<int> pinsInFrame = new List<int>();

            for (int i = 0; i < hitPins.Length; i++)
            {
                var currentHitPin = hitPins[i];

                // 現在倒したPinを保存
                pinsInFrame.Add(currentHitPin);

                if (pinsInFrame.Count == 1) // is first throw
                {
                    if (currentHitPin == 10)
                    {
                        // Strike
                        IFrame frame = CreateFrame(frameCount, () => new StrikeFrame(hitPins, i));
                        yield return frame;

                        frameCount++;
                        pinsInFrame.Clear();
                    }
                }
                else
                {
                    // Spareかどうか判定
                    var hitPinsInFrame = pinsInFrame.Sum(x => x);
                    if (hitPinsInFrame == 10)
                    {
                        // Spare
                        yield return CreateFrame(frameCount, () => new SpareFrame(pinsInFrame.ToList().AsReadOnly(), hitPins, i));
                    }
                    else
                    {
                        // Strike
                        yield return CreateFrame(frameCount, () => new NormalFrame(pinsInFrame.ToList().AsReadOnly()));
                    }
                    frameCount++;
                    pinsInFrame.Clear();
                }

            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IFrame CreateFrame(int frameCount, Func<IFrame> createFrameFunc)
        {
            return frameCount == 10
                ? new LastFrame(createFrameFunc.Invoke())
                : createFrameFunc.Invoke();
        }
    }
}
