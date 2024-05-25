using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
    {
    internal class Enemy
        {
        private Unit PlayerUnit; //public
        private Unit EnemyUnit; //public
        private Input Input; //public
        private int AbilitiesCount; //public
        private int SecondAbilityModifier; //public
        private int ThirdAbilityValue;

        public Enemy(Unit playerUnit, Unit enemyUnit, Input input) 
            {
            PlayerUnit = playerUnit;
            EnemyUnit = enemyUnit;
            Input = input;
            AbilitiesCount = 3;
            SecondAbilityModifier = 3;
            ThirdAbilityValue = 100;
            }

        public void Turn () //public
            {
            switch (Input.GetAICommandNumber(AbilitiesCount))
                {
                case 1:
                    PlayerUnit.TakeDamage(EnemyUnit.Damage, EnemyUnit, true);
                    break;
                case 2:
                    int modifiedDamage = EnemyUnit.Damage * SecondAbilityModifier;
                    EnemyUnit.TakeDamage(EnemyUnit.Damage, EnemyUnit);
                    PlayerUnit.TakeDamage(modifiedDamage, EnemyUnit);
                    break;
                case 3:
                    if (EnemyUnit.LastDamageFromWeapon)
                        { 
                        EnemyUnit.TakeDamage(ThirdAbilityValue, EnemyUnit);
                        }
                    else
                        {
                        EnemyUnit.TakeDamage(-ThirdAbilityValue, EnemyUnit);
                        }
                    break;
                }
            }
        }
    }
