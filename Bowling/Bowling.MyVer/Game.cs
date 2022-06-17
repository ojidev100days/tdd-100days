using System.Collections.ObjectModel;

namespace Bowling.MyVer;

internal class Game
{

    private static readonly Frames _emptyFrames = new Frames();

    private readonly Frames _frames;

    public Game()
    {
        _frames = _emptyFrames;
    }

    public Game(Frames frames) : this()
    {
        _frames = frames;
    }

    public int TotalScore
    {
        get
        {
            int totalScore = 0;
            for (int i = 0; i < _frames.Count; i++)
            {
                var frame = this._frames[i];
                // フレーム内の投球が完了していない場合は、スコアの計算を打ち切り
                if (!frame.IsComplite) break;

                if (frame.IsStrike)
                {
                    // ストライクの場合、次の2回分の投球がなければ、スコアの計算を打ち切り
                    if (this._frames.GetNumberOfThrowsFrom(i) < 2) break;

                    totalScore += frame.PinCount + _frames.GetNumberOfPinsFrom(i, 2);
                }
                else if (frame.IsSpare)
                {
                    // スペアの場合、次の1回分の投球がなければ、スコアの計算を打ち切り
                    if (this._frames.GetNumberOfThrowsFrom(i) < 1) break;

                    totalScore += frame.PinCount + _frames.GetNumberOfPinsFrom(i, 1);
                }
                else
                {
                    totalScore += frame.PinCount;
                }
            }
            return totalScore;
        }
    }

    public int CurrentFrameNo => _frames.Count;

    internal Game Add(int hitPin)
    {
        if (hitPin < 0 || 10 < hitPin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(hitPin={hitPin})");


        var addedCcurrentFrame = _frames.Current.Add(hitPin);
        var newFrames = addedCcurrentFrame.CanBeAdded
            ? _frames.ChangeCurrentFrame(addedCcurrentFrame) // CurrentFrameの入れ替え
            : _frames.ChangeCurrentFrame(addedCcurrentFrame).Add(new Frame()); //  CurrentFrameの入れ替え&新しいフレームの用意
        return new Game(newFrames);
    }

    internal Game Add(params int[] pins)
    {
        // TODO 処理効率が悪いので、要検討
        var newGame = this;
        foreach (var pin in pins)
        {
            newGame = newGame.Add(pin);
        }
        return newGame;
    }

    internal Game Add(Frame frame)
    {
        return new Game(_frames.Add(frame));
    }

    internal Game Add(params Frame[] frames)
    {
        var newGame = this;
        foreach (var frame in frames)
        {
            newGame = newGame.Add(frame);
        }
        return newGame;
    }

}
