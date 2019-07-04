using MinesweeperApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.CreateNewGame();
            game.Simulate();

            // NOTE: Design Pattern used - Factory Method
            // read-up https://www.dofactory.com/net/factory-method-design-pattern

            Console.WriteLine("Press any key to exit application.");
            Console.ReadKey();
        }
    }
}
