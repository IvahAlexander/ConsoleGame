using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyApp.Program;

namespace ConsoleGame
    {
    internal class ConsoleWriter
        {

        void DamageMessage ( string damager, string defender, float damage )
            {
            if (damage > 0)
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Игрок {damager} наносит игроку {defender} урон в размере {damage} единиц.");
                Console.ForegroundColor = ConsoleColor.White;
                }
            }

        void HealMessage ( string healer, float healValue )
            {
            Console.WriteLine($"Игрок {healer} восстановил себе здоровье в размере {healValue} единиц.");

            }

        void WriteDamageWithType ( AttackType attackType, string damagerName, string defenderName, List<float> damages )
            {
            foreach (var damage in damages)
                {
                switch (attackType)
                    {
                    case AttackType.Damage:
                        DamageMessage(damagerName, defenderName, damage);
                        break;
                    case AttackType.Self:
                        DamageMessage(damagerName, damagerName, damage);
                        break;
                    case AttackType.Heal:
                        HealMessage(damagerName, damage);
                        break;
                    default:
                        break;
                    }
                }
            damages.Clear();
            }

        public void WriteDamagesFromTo ( Dictionary<AttackType, List<float>> damages, string damagerName, string defenderName ) //public
            {
            foreach (var damage in damages)
                {
                WriteDamageWithType(damage.Key, damagerName, defenderName, damage.Value);
                }
            }
        public void WriteUnitHealth (string owner, Unit unit )  //public
            {
            Console.WriteLine($"{owner}: {unit.CurrentHealth}");
            }

        public void WriteAbilitiesList (string message, Unit unit ) //public
            {
            Console.WriteLine($"{message}");
            for (int i = 0; i< unit.GetAbilityCount(); i++)
                {
                Console.WriteLine($"{i+1}. {unit.GetAbilityDescription(i)} ");
                }
            Console.WriteLine();
            }
        }
    }

