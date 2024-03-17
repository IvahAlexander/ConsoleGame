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
        static void Main(string[] args)
        {
            string? name;
            float maxPlayerHealth = 1000f;
            float maxEnemyHealth = 2000f;

            float currentPlayerHealth = maxPlayerHealth;
            float currentEnemyHealth = maxEnemyHealth;

            float playerDamage = 50f;
            float enemyDamage = 55f;

            float fireballDamage = 200f;
            float enemyHealDamage = 100f;
            float playerHealDamage = 100f;

            string? input;

            int commandCount = 3;
            int enemySecondAbilityModifier = 3;
            float dangerHealthLevel = 0.2f;

            Random randomizer = new Random();

            Dictionary<AttackType, List<float>> damagesToPlayer = new();
            Dictionary<AttackType, List<float>> damagesToEnemy = new();

            damagesToPlayer[AttackType.Damage] = new List<float>();
            damagesToPlayer[AttackType.Self] = new List<float>();
            damagesToPlayer[AttackType.Heal] = new List<float>();

            damagesToEnemy[AttackType.Heal] = new List<float>();
            damagesToEnemy[AttackType.Damage] = new List<float>();
            damagesToEnemy[AttackType.Self] = new List<float>();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите ваше имя: ");
            name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine($"\nДобро пожаловать в игру, {name}!");

            while (currentPlayerHealth > 0 && currentEnemyHealth > 0)
            {
                if (currentPlayerHealth < dangerHealthLevel * maxPlayerHealth)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ваше здоровье: {currentPlayerHealth}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Ваше здоровье: {currentPlayerHealth}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (currentEnemyHealth < dangerHealthLevel * maxEnemyHealth)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Здоровье противника: {currentEnemyHealth}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Здоровье противника: {currentEnemyHealth}");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"{name}! Выберите действие:\n" +
                    $"1. Ударить оружием: урон {playerDamage}\n" +
                    $"2. Щит: следующая атака не нанесет урона\n" +
                    $"3. Огненный шар: наносит урон в размере {fireballDamage}\n");

                input = Console.ReadLine();

                bool playerShield = false;
                bool playerDamageWithWeapon = false;
                bool enemyDamageWithWeapon = false;

                switch (input)
                {
                    case "1":
                        playerDamageWithWeapon = true;
                        currentEnemyHealth -= playerDamage;
                        damagesToEnemy[AttackType.Damage].Add(playerDamage);
                        break;
                    case "2":
                        playerShield = true;
                        break;
                    case "3":
                        currentEnemyHealth -= fireballDamage;
                        damagesToEnemy[AttackType.Damage].Add(fireballDamage);
                        if (!enemyDamageWithWeapon)
                        {
                            currentPlayerHealth += playerHealDamage;
                            if (currentPlayerHealth > maxPlayerHealth) { currentPlayerHealth = maxPlayerHealth; }
                        }
                        break;
                    default:
                        Console.WriteLine("Команда не распознана!");
                        break;
                }

                int enemyCommand = randomizer.Next(1, commandCount + 1);

                switch (enemyCommand)
                {
                    case 1:
                        if (!playerShield)
                        {
                            currentPlayerHealth -= enemyDamage;
                            damagesToPlayer[AttackType.Damage].Add(enemyDamage);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Типа атаки врага - Щит");
                        if (!playerShield)
                        {
                            currentEnemyHealth -= enemyDamage;
                            currentPlayerHealth -= enemyDamage * enemySecondAbilityModifier;
                            damagesToPlayer[AttackType.Self].Add(enemyDamage);
                            damagesToPlayer[AttackType.Damage].Add(enemyDamage * enemySecondAbilityModifier);
                        }
                        break;
                    case 3:
                        if (playerDamageWithWeapon)
                        {
                            currentEnemyHealth -= enemyHealDamage;
                            damagesToPlayer[AttackType.Self].Add(enemyHealDamage);
                        }
                        else
                        {
                            currentEnemyHealth += enemyHealDamage;
                            damagesToPlayer[AttackType.Heal].Add(enemyHealDamage);
                            if (currentEnemyHealth > maxEnemyHealth) { currentEnemyHealth = maxEnemyHealth; }
                        }
                        break;
                    default:
                        break;
                }

                WriteDamagesFromTo(damagesToEnemy, name, "Enemy");
                WriteDamagesFromTo(damagesToPlayer, "Enemy", name);

                EndTurn();

            void EndTurn()
            {
                Console.WriteLine("\nДля продолжения нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
            }

            void DamageMessage(string damager, string defender, float damage)
            {
                if (damage > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Игрок {damager} наносит игроку {defender} урон в размере {damage} единиц.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            void HealMessage(string healer, float healValue)
            {
                Console.WriteLine($"Игрок {healer} восстановил себе здоровье в размере {healValue} единиц.");

            }

            void WriteDamageWithType(AttackType attackType, string damagerName, string defenderName, List<float> damages)
            {
                foreach (var damage in damages)
                {
                    Console.WriteLine(damage);
                    Console.WriteLine(attackType);
                    switch (attackType)
                    {
                        case AttackType.Damage:
                            DamageMessage(damagerName, defenderName, damage);
                            break;
                        case AttackType.Self:
                            DamageMessage(defenderName, defenderName, damage);
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

            void WriteDamagesFromTo(Dictionary<AttackType, List<float>> damages, string damagerName, string defenderName)
            {
                foreach (var damage in damages)
                {
                    WriteDamageWithType(damage.Key, damagerName, defenderName, damage.Value);
                }
            }

        }
    } }
}