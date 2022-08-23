using System.Collections;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal class FrameEnumerable : IEnumerable<IFrame>
    {
        private const int MAX_FRAME_NO = 10;

        private readonly HitPins _hitPins;

        public FrameEnumerable(HitPins hitPins)
        {
            this._hitPins = hitPins;
        }

        public IEnumerator<IFrame> GetEnumerator()
        {
            int currentThrowIndex = 0;
            int currentFrameNo = 0; // 最終フレームを判定するために使用する

            while (currentThrowIndex < _hitPins.ThrowCount)
            {
                currentFrameNo++;
                IFrame frame = Create(_hitPins, currentThrowIndex, currentFrameNo);

                Console.WriteLine($"[DEBUG] hitPins={_hitPins}, fn={currentFrameNo}, ci={currentThrowIndex}, kps={frame.KnockedDownPins}, sps={frame.ScorePins}");
                currentThrowIndex += frame.KnockedDownPins.ThrowCount;

                yield return MAX_FRAME_NO < currentFrameNo
                    ? throw new BowlingAppException($"The game is already over. (hitPins={_hitPins}, currentIndex={currentThrowIndex})")
                    : frame;
            }
        }

        private IFrame Create(HitPins hitPins, int throwIndex, int frameNo)
        {
            IFrame CreateFrame(Func<IFrame> createFrameFunc)
            {
                return frameNo == MAX_FRAME_NO
                    ? new LastFrame(createFrameFunc.Invoke())
                    : createFrameFunc.Invoke();
            }

            var currentPin = hitPins[throwIndex];
            if (currentPin.IsStrike) return CreateFrame(() => new StrikeFrame(_hitPins, throwIndex));

            var currentFramePins = hitPins.Range(throwIndex, 2);
            if (currentFramePins.IsSpare) return CreateFrame(() => new SpareFrame(_hitPins, throwIndex));

            return CreateFrame(() => new NormalFrame(currentFramePins));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
