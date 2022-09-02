using Bowling.MyVer2.Frames;
using static Bowling.MyVer2.HitPin;

namespace Bowling.MyVer2
{
    internal class Game
    {

        public static Game Of(params int[] hitPins)
        {
            return new Game(HitPins.Of(hitPins));
        }

        private readonly HitPins _hitPins;

        internal IReadOnlyList<IFrame> Frames { get; }

        internal int Score => Frames.TakeWhile(x => x.IsComplete).Sum(x => x.Score);

        public bool IsComplete => Frames.LastOrDefault() is LastFrame lastFrame && lastFrame.IsComplete;

        public Game()
        {
            _hitPins = new HitPins();
            Frames = new List<IFrame>();
        }

        public Game(HitPins hitPins)
        {
            this._hitPins = hitPins;
            this.Frames = FrameFactory.Create(_hitPins).ToList();
        }

        internal Game ThrowBall(HitPins hitPins)
        {
            return new Game(_hitPins.Concat(hitPins));
        }

        internal Game ThrowBall(params int[] hitPins)
        {
            return ThrowBall(HitPins.Of(hitPins));
        }

        public override string ToString()
        {
            return string.Join("|", Frames);
        }
    }
}
