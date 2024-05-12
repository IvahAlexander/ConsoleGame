using ConsoleGame;

using System;
using static MyApp.Program;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
        {
        public enum AttackType
            {
            Damage,
            Self,
            Heal
            }
        static void Main ( string[] args )
            {
            Input input = new Input();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Unit playerUnit = new Unit(50, 1000);
            Unit enemyUnit = new Unit(55, 2000);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();

            Player player = new Player(name, playerUnit, enemyUnit, input, consoleWriter);
            Enemy enemy = new Enemy(playerUnit, enemyUnit, input);

            Console.Clear();
            Console.WriteLine($"\nДобро пожаловать в игру, {name}!");

            while (playerUnit.IsAlive && enemyUnit.IsAlive)
                {
                player.Turn();
                enemy.Turn();

                consoleWriter.WriteDamagesFromTo(enemyUnit.DamageHistory, name, "Enemy");
                consoleWriter.WriteDamagesFromTo(playerUnit.DamageHistory, "Enemy", name);

                EndTurn();

                void EndTurn ()
                    {
                    Console.WriteLine("\nДля продолжения нажмите любую клавишу.");
                    Console.ReadKey();
                    Console.Clear();
                    }


                }
            }
        }
}