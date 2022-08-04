using System.Runtime.CompilerServices;
using Bowling.MyVer2;

[assembly: InternalsVisibleTo("Bowling.MyVer2.Test")]

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var game = new Game(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0);
Console.WriteLine(game);
