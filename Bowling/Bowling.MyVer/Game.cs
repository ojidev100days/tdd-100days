namespace Bowling.MyVer;

internal class Game
{

    public int Score { get; private set; } = 0;
    public int Frame => _currentFrame + 1;

    private int _addCount = 0;

    private int _currentFrame = 0;

    private int[] _frames = new int[10];

    internal void Add(int pin)
    {
        if (pin < 0 || 10 < pin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(pin={pin})");
        if (10 < _frames[_currentFrame] + pin) throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(currentFrame={_currentFrame}, frame={_frames[_currentFrame]}, pin={pin})");

        _frames[_currentFrame] += pin;
        Score += pin;

        // ピンがストライクの場合
        if (pin == 10)
        {
            _addCount = 0;
            _currentFrame++;
            return;
        }
        if ((_addCount = ++_addCount % 2) == 0) _currentFrame++;
    }
}
