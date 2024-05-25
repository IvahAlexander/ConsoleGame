using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
    {
    internal class Input
        {
        public Random Random;

        public Input ()
            {
            Random = new Random();
            }

        public int GetPlayerCommandNumber () //public
            {
            Console.Write("Введите номер: ");
            int commandNumber;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out commandNumber))
                {
                Console.WriteLine("Команда не распознана. Попробуйте еще раз");
                input = Console.ReadLine();
                }
            return commandNumber;
            }

        public int GetAICommandNumber ( int maxNumbers ) //public
            {
            int randomCommand = Random.Next(1, maxNumbers + 1);
            return randomCommand;
            }
        }
    }
