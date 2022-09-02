using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2.Frames
{
    internal interface IFrame
    {
        bool IsComplete { get; }

        int Score { get; }

        HitPins KnockedDownPins { get; }

        HitPins ScorePins { get; }

    }
}
