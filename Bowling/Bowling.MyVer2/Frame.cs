using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2
{
    internal static class Frame
    {
        private static readonly int MaxHitPin = 10;

        public static bool IsAllKnockDown(this HitPins hitPins)
        {
            return hitPins.Sum() == MaxHitPin;
        }

        public static bool ExceededMaxPinsInFrame(this HitPins hitPins)
        {
            return MaxHitPin < hitPins.Sum();
        }
    }
}
