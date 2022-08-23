using System.Runtime.CompilerServices;
using Bowling.MyVer2;

[assembly: InternalsVisibleTo("Bowling.MyVer2.Test")]

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Bowling App 2");


ShowGame("Game1", new Game(0, 1, 2, 3, 4, 5, 5, 5, 10, 5, 5, 4, 3, 2, 1, 0, 10, 8, 1));
ShowGame("Game2", new Game(0, 1, 2, 3, 4, 5, 5, 5, 10, 5, 5, 4, 3, 2, 1, 0, 10, 10, 9, 8));
ShowGame("Game3", new Game(0, 1, 2, 3, 4, 5, 5, 5, 10, 5, 5, 4, 3, 2, 1, 0, 10, 9, 1, 8));




void ShowGame(string gameName, Game game) {
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Game1");
    Console.WriteLine($"Game       : {game}");
    Console.WriteLine($"FrameScore : {string.Join(",", game.Frames.Select(s => s.Score))}");
    Console.WriteLine($"ScorePins  : {string.Join(",", game.Frames.Select(s => s.ScorePins.ToMsg()))}");
    Console.WriteLine($"Score      : {game.Score}");
    var last = game.Frames.Last();
    Console.WriteLine($"Last Type            : {last.GetType()}");
    Console.WriteLine($"Last KnockedDownPins : {string.Join(",", last.KnockedDownPins)}");
    Console.WriteLine($"Last ScorePins       : {string.Join(",", last.ScorePins)}");
    Console.WriteLine($"Last ToString        : {last}");
    Console.WriteLine("--------------------------------");
}

