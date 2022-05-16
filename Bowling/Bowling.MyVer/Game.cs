using System.Collections.ObjectModel;

namespace Bowling.MyVer;

internal class Game
{

    private static readonly int[] _emptyHitPins = new int[0];

    private readonly ReadOnlyCollection<int> _hitPins;

    public Game()
    {
        _hitPins = new ReadOnlyCollection<int>(_emptyHitPins);
    }

    public Game(ReadOnlyCollection<int> hitPins)
    {
        _hitPins = hitPins;
    }

    public Game(ReadOnlyCollection<int> hitPins, int hitPin)
    {
        List<int> addedHitPins = new List<int>(hitPins);
        addedHitPins.Add(hitPin);
        _hitPins = new ReadOnlyCollection<int>(addedHitPins);
    }

    public int GetScore()
    {


        return _hitPins.Sum(x => x);
    }


    public int GetFrame()
    {
        int frame = 0;
        bool isFirstInFrame = true; // フレーム中の最初の投球かどうかを判定するフラグ
        foreach (var hitPin in _hitPins)
        {
            if (isFirstInFrame)
            {
                frame++;
                isFirstInFrame = (hitPin == 10); // ストライクの場合、最初の投球のまま。それ以外は次の投球とする
            }
            else
            {
                // フレーム中の最初の投球でなければ、2投目、ということなので、次のフレームに移る
                // TODO １０フレーム目の場合、特殊処理あり
                isFirstInFrame = true;
            }
        }
        return frame;
    }

    private int _addCount = 0;

    private int _currentFrame = 0;


    internal Game Add(int hitPin)
    {
        if (hitPin < 0 || 10 < hitPin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(hitPin={hitPin})");
        //        if (10 < _frames[_currentFrame] + pin) throw new BowlingAppException($"The total number of pins that can be added in a frame is limited to 10.(currentFrame={_currentFrame}, frame={_frames[_currentFrame]}, pin={pin})");

        return new Game(_hitPins, hitPin);
    }

    internal Game Add(int[] pins)
    {
        // TODO 処理効率が悪いので、要検討
        var newGame = this;
        foreach(var pin in pins)
        {
            newGame = newGame.Add(pin);
        }
        return newGame;
    }
}
