namespace Bowling.MyVer
{
    internal interface IFrame
    {
        int ThrowCount { get; }

        IEnumerable<int> HitPins { get; }

        bool CanBeAddPin { get; }
        bool IsComplite { get; }
        bool IsStrike { get; }
        int PinCount { get; }
        bool IsSpare { get; }

        IFrame Add(int hitPin);
    }
}
