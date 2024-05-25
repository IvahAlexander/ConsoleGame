using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyApp.Program;

namespace ConsoleGame
    {
    internal class Unit
        {
        public int Damage; //public
        private int MaxHealth; //public
        public int CurrentHealth;
        public bool IsAlive = true; //public
        public int ShieldCount = 0; //public
        public bool LastDamageFromWeapon = false; //public
        public List<string> AbilityDescription = new();  //public
        public Dictionary<AttackType, List<float>> DamageHistory = new(); //public

        public Unit(int damage, int maxHealth)
            {
            Damage = damage;
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;

            DamageHistory[AttackType.Damage] = new List<float>();
            DamageHistory[AttackType.Self] = new List<float>();
            DamageHistory[AttackType.Heal] = new List<float>();
            }

        public void TakeDamage(int damage, Unit origin, bool IsWeaponDamage = false ) //public
            {
            LastDamageFromWeapon = IsWeaponDamage;
            bool isHeal = damage < 0;
            AttackType attackType = AttackType.Damage;
            if (isHeal)
                attackType = AttackType.Heal;
            else if (origin == this )
                attackType = AttackType.Self;

            if (InShield() && !isHeal)
                {
                ShieldCount = ShieldCount - 1;
                DamageHistory[attackType].Add(0);
                return;
                }
            CurrentHealth -= damage;

            DamageHistory[attackType].Add(damage);

            if (CurrentHealth <=0 )
                {
                IsAlive = false;
                }

            if (CurrentHealth > MaxHealth)
                {
                CurrentHealth = MaxHealth;
                }
            }

        public string GetAbilityDescription(int abilityNumber ) //public
            {
            return AbilityDescription[abilityNumber];
            }

        public int GetAbilityCount ()  //public
            {
            return AbilityDescription.Count;
            }

        private bool InShield () //public
            {
            return ShieldCount > 0;
            }
        }

    }
