namespace Bowling.MyVer;

internal class Game
{

    public int Score { get; private set; } = 0;
    public int Frame { get; internal set; } = 1;

    private int _addCount = 0;

    internal void Add(int pin)
    {
        if (pin < 0 || 10 < pin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(pin={pin})");

        Score += pin;
        if ((_addCount = ++_addCount % 2) == 0) Frame++;
    }
}
