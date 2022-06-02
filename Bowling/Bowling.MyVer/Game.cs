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

    public int TotalScore => _frames.Score;

    public int CurrentFrameNo => _frames.Count;

    internal Game Add(int hitPin)
    {
        if (hitPin < 0 || 10 < hitPin) throw new BowlingAppException($"The number of pins that can be added at one time is 0-10.(hitPin={hitPin})");


        var addedCcurrentFrame = _frames.Current.Add(hitPin);
        var newFrames = addedCcurrentFrame.CanBeAdded
            ? new Frames(_frames.Take(_frames.Count - 1).Concat(new Frame[] { addedCcurrentFrame })) // CurrentFrameの入れ替え
            : new Frames(_frames.Take(_frames.Count - 1).Concat(new Frame[] { addedCcurrentFrame, new Frame() })); //  CurrentFrameの入れ替え&新しいフレームの用意
        return new Game(newFrames);
    }

    internal Game Add(int[] pins)
    {
        // TODO 処理効率が悪いので、要検討
        var newGame = this;
        foreach (var pin in pins)
        {
            newGame = newGame.Add(pin);
        }
        return newGame;
    }
}
