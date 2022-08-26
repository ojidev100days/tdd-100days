using System.Collections;
using System.Diagnostics.CodeAnalysis;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{

    internal static class FrameFactory
    {
        private const int MAX_FRAME_NO = 10;

        delegate bool TryCreateFunc(HitPins hitPins, int index, [MaybeNullWhen(false)] out IFrame result);

        private static readonly IReadOnlyList<TryCreateFunc> _tryCreateFuncs = new List<TryCreateFunc>
        {
            StrikeFrame.TryCreate,
            SpareFrame.TryCreate,
            NormalFrame.TryCreate,
        }.AsReadOnly();

        public static IEnumerable<IFrame> Create(HitPins hitPins)
        {
            return new FrameEnumerable(hitPins);
        }

        private class FrameEnumerable : IEnumerable<IFrame>
        {

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

                    yield return currentFrameNo <= MAX_FRAME_NO
                        ? frame
                        : throw new BowlingAppException($"The game is already over. (hitPins={_hitPins}, currentIndex={currentThrowIndex})");
                }
            }

            private static IFrame CreateFrame(HitPins hitPins, int currentIndex, int frameNo)
            {
                foreach (var tryCreateFunc in _tryCreateFuncs)
                {
                    if (tryCreateFunc.Invoke(hitPins, currentIndex, out var frame)) return frameNo == MAX_FRAME_NO ? new LastFrame(frame) : frame;
                }

                throw new ArgumentException($"Cannot create frame. (hitPins={hitPins}, currentIndex={currentIndex})");
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
    }


}
