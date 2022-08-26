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
                IFrame frame = CreateFrame(_hitPins, currentThrowIndex, currentFrameNo);

                Console.WriteLine($"[DEBUG] hitPins={_hitPins}, fn={currentFrameNo}, ci={currentThrowIndex}, kps={frame.KnockedDownPins}, sps={frame.ScorePins}");
                currentThrowIndex += frame.KnockedDownPins.ThrowCount;

                yield return MAX_FRAME_NO < currentFrameNo
                    ? throw new BowlingAppException($"The game is already over. (hitPins={_hitPins}, currentIndex={currentThrowIndex})")
                    : frame;
            }
        }


        private IFrame CreateFrame(HitPins hitPins, int currentIndex, int frameNo)
        {
            static IFrame Create(int frameNo, IFrame frame)
            {
                return frameNo == MAX_FRAME_NO ? new LastFrame(frame) : frame;
            }

            // TODO 個々の処理をListに突っ込んでぐるぐるしたい

            if (StrikeFrame.TryCreate(hitPins, currentIndex, out var strikeFrame)) return Create(frameNo, strikeFrame);

            if (SpareFrame.TryCreate(hitPins, currentIndex, out var spareFrame)) return Create(frameNo, spareFrame);

            if (NormalFrame.TryCreate(hitPins, currentIndex, out var normalFrame)) return Create(frameNo, normalFrame);

            throw new ArgumentException($"Cannot create frame. (hitPins={hitPins}, currentIndex={currentIndex})");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
