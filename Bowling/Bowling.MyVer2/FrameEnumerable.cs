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

            int currentIndex = 0;
            while (currentIndex < _hitPins.Count)
            {
                currentFrameNo++;
                IFrame frame = Create(_hitPins, currentIndex, currentFrameNo);

                Console.WriteLine($"[DEBUG] hitPins={_hitPins}, fn={currentFrameNo}, ci={currentIndex}, kps={frame.KnockedDownPins}, sps={frame.ScorePins}");
                currentIndex += frame.KnockedDownPins.Count;

                yield return MAX_FRAME_NO < currentFrameNo
                    ? throw new BowlingAppException($"The game is already over. (hitPins={_hitPins}, currentIndex={currentIndex})")
                    : frame;
            }
        }

        private IFrame Create(HitPins hitPins, int currentIndex, int currentFrameNo)
        {
            IFrame CreateFrame(Func<IFrame> createFrameFunc)
            {
                return currentFrameNo == MAX_FRAME_NO
                    ? new LastFrame(createFrameFunc.Invoke())
                    : createFrameFunc.Invoke();
            }

            var currentPin = hitPins[currentIndex];
            if (currentPin.IsStrike) return CreateFrame(() => new StrikeFrame(_hitPins, currentIndex));

            var currentFramePins = hitPins.Range(currentIndex, 2);
            if (currentFramePins.IsSpare) return CreateFrame(() => new SpareFrame(_hitPins, currentIndex));

            return CreateFrame(() => new NormalFrame(currentFramePins));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
