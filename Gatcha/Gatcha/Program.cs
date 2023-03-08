// See https://aka.ms/new-console-template for more information
using GamePage;

Console.WriteLine("Hello, World!");
Random rd = new Random();

Console.WriteLine($"{rd.Next(0,99)}");

Player player = new Player();
Item item = new Item("ok");
Item item2 = new Item("ok2");
Item[] hehe = new[] { item , item2 };
Gatcha gatcha = new Gatcha(hehe);
Console.WriteLine(player.money);
gatcha.Pull(player);
Console.WriteLine(player.money);