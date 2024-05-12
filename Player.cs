using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
    {
    internal class Player
        {
        private string Name; //public
        public Unit PlayerUnit;
        public Unit EnemyUnit;
        public Input Input;
        public ConsoleWriter Writer;
        private int FireballDamage = 200; //public

        public Player( string name, Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer  )
            {
            Name = name;
            PlayerUnit = playerUnit;
            EnemyUnit = enemyUnit;
            Input = input;
            Writer = writer;

            PlayerUnit.AbilityDescription.Add($"Ударить оружием (урон {playerUnit.Damage}");
            PlayerUnit.AbilityDescription.Add($"Щит: следующая атака противника не нанесет урона");
            PlayerUnit.AbilityDescription.Add($"Огненный шар: наносит урон в размере {FireballDamage}");
            }

        public void Turn()
            {
            Writer.WriteUnitHealth("Ваше здоровье: ", PlayerUnit);
            Writer.WriteUnitHealth("Здоровье противника: ", EnemyUnit);

            Writer.WriteAbilitiesList($"{Name} Выберите действие: ", PlayerUnit);

            switch (Input.GetPlayerCommandNumber())
                {
                case 1:
                    EnemyUnit.TakeDamage(PlayerUnit.Damage, PlayerUnit, true);
                    break;
                case 2:
                    PlayerUnit.ShieldCount = 1;
                    break;
                case 3:
                    EnemyUnit.TakeDamage(FireballDamage, PlayerUnit, true);
                    break;
                }
            }
        }
    }
    