﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyApp.Program;

namespace ConsoleGame
    {
    internal class Unit
        {
        public int Damage;
        public int MaxHealth;
        public int CurrentHealth;
        public bool IsAlive = true;
        public int ShieldCount = 0;
        public bool LastDamageFromWeapon = false;
        public List<string> AbilityDescription = new();
        public Dictionary<AttackType, List<float>> DamageHistory = new();
        
        public Unit(int damage, int maxHealth)
            {
            Damage = damage;
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;

            DamageHistory[AttackType.Damage] = new List<float>();
            DamageHistory[AttackType.Self] = new List<float>();
            DamageHistory[AttackType.Heal] = new List<float>();
            }
        
        public void TakeDamage(int damage, Unit origin, bool IsWeaponDamage = false)
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

        public string GetAbilityDescription(int abilityNumber )
            {
            return AbilityDescription[abilityNumber];
            }

        public int GetAbilityCount () 
            {
            return AbilityDescription.Count;
            }

        public bool InShield ()
            {
            return ShieldCount > 0;
            }
        }

    }