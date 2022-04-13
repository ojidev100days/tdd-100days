namespace Bowling.MyVer;

internal class Game
{

    public int Score { get; private set; } = 0;

    internal void Add(int pin)
    {
        Score += pin;
    }
}
