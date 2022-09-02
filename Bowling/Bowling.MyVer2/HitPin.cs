using Bowling.MyVer2.Frames;

namespace Bowling.MyVer2
{
    public struct HitPin
    {
        private static readonly int MinHitPin = 0;
        private static readonly int MaxHitPin = 10;

        public static implicit operator HitPin(int hitPin) => new(hitPin);

        public int Value { get; }

        public bool IsAllKnockDown => Value == MaxHitPin;

        public HitPin(int pin)
        {
            if (pin < MinHitPin || MaxHitPin < pin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(pin={pin})");
            Value = pin;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
