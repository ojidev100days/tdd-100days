using System.Runtime.CompilerServices;
using Bowling.MyVer2;

[assembly: InternalsVisibleTo("Bowling.MyVer2.Test")]

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var game = new Game(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 1);
Console.WriteLine($"KnockedDoun : {game}");
Console.WriteLine($"TrameScore  : {string.Join(",", game.Select(s => s.Score))}");
Console.WriteLine($"Score       : {game.Score}");



var last = game.Last();
Console.WriteLine($"Last Type            : {last.GetType()}");
Console.WriteLine($"Last KnockedDownPins : {string.Join(",", last.KnockedDownPins)}");
Console.WriteLine($"Last ScorePins       : {string.Join(",", last.ScorePins)}");
Console.WriteLine($"Last ToString        : {last}");




