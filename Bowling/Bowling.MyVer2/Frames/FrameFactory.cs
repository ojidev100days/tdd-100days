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

        public static IEnumerable<IFrame> Create(HitPins allHitPins)
        {
            return new FrameEnumerable(allHitPins);
        }

        private class FrameEnumerable : IEnumerable<IFrame>
        {

            private readonly HitPins _allHitPins;

            public FrameEnumerable(HitPins allHitPins)
            {
                this._allHitPins = allHitPins;
            }

            public IEnumerator<IFrame> GetEnumerator()
            {
                int currentThrowIndex = 0;
                int currentFrameNo = 0; // 最終フレームを判定するために使用する

                while (currentThrowIndex < _allHitPins.ThrowCount)
                {
                    currentFrameNo++;
                    IFrame frame = CreateFrame(_allHitPins, currentThrowIndex, currentFrameNo);

                    Console.WriteLine($"[DEBUG] hitPins={_allHitPins}, fn={currentFrameNo}, ci={currentThrowIndex}, kps={frame.KnockedDownPins}, sps={frame.ScorePins}");
                    currentThrowIndex += frame.KnockedDownPins.ThrowCount;

                    yield return currentFrameNo <= MAX_FRAME_NO
                        ? frame
                        : throw new BowlingAppException($"The game is already over. (hitPins={_allHitPins}, currentIndex={currentThrowIndex})");
                }
            }

            private static IFrame CreateFrame(HitPins allHitPins, int throwIndex, int frameNo)
            {
                foreach (var tryCreateFunc in _tryCreateFuncs)
                {
                    if (tryCreateFunc.Invoke(allHitPins, throwIndex, out var frame)) return frameNo == MAX_FRAME_NO ? new LastFrame(frame) : frame;
                }

                throw new ArgumentException($"Cannot create frame. (hitPins={allHitPins}, throwIndex={throwIndex})");
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
    }


}
