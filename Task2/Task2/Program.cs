using Task2;

Console.WriteLine($"Initial count: {Server.GetCount()}");

Server.AddToCount(1);
Console.WriteLine($"Add 1: {Server.GetCount()}");

Server.AddToCount(5);
Console.WriteLine($"Add 5: {Server.GetCount()}");

Server.AddToCount(-3);
Console.WriteLine($"Add -3: {Server.GetCount()}");
