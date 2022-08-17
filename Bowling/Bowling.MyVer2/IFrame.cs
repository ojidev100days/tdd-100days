namespace Bowling.MyVer2
{
    internal interface IFrame
    {
        bool IsComplete { get; }

        int Score { get; }

        int[] KnockedDownPins { get; }

        int[] ScorePins { get; }

    }
}
