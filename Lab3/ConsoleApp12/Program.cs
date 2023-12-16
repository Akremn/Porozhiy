using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    // Клас Key, що представляє ключ для хеш-таблиці
    class Key
    {
        public string Stock { get; }
        public int DayOfYear { get; }

        public Key(string stock, int dayOfYear)
        {
            Stock = stock;
            DayOfYear = dayOfYear;
        }

        // Перевизначення методів GetHashCode та Equals для коректної роботи з хеш-таблицею
        public override int GetHashCode()
        {
            return HashCode.Combine(Stock, DayOfYear);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Key other = (Key)obj;
            return Stock == other.Stock && DayOfYear == other.DayOfYear;
        }
    }

    // Клас для реалізації хеш-таблиці
    class HashTable
    {
        private Dictionary<Key, double> dictionary;

        public HashTable(int size)
        {
            dictionary = new Dictionary<Key, double>(size);
        }

        public void Put(Key key, double value)
        {
            dictionary[key] = value;
        }

        public double Get(Key key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                // В даному випадку, якщо ключа немає у таблиці, можна повертати певне значення за замовчуванням або кидати виняток
                throw new KeyNotFoundException("Key not found");
            }
        }

        public bool ContainsKey(Key key)
        {
            return dictionary.ContainsKey(key);
        }

        public double Remove(Key key)
        {
            if (dictionary.ContainsKey(key))
            {
                double value = dictionary[key];
                dictionary.Remove(key);
                return value;
            }
            else
            {
                // Якщо ключа немає у таблиці, можна повертати певне значення за замовчуванням або кидати виняток
                throw new KeyNotFoundException("Key not found");
            }
        }

        public int Size()
        {
            return dictionary.Count;
        }
    }

    // Приклад використання
    class Program
    {
        static void Main(string[] args)
        {
            HashTable ht = new HashTable(1024);
            int choice;
            do
            {
                Console.WriteLine("1.Покласти значення за ключем в таблицю");
                Console.WriteLine("2.Взяти значення з таблицi за ключем");
                Console.WriteLine("3.Перевiрити чи є даний ключ в таблицi");
                Console.WriteLine("4.Видалити елемент з таблицi за ключем");
                Console.WriteLine("5.Повернути кiлькiсть елементiв в хеш-таблицi.");
                Console.WriteLine("Для виходу з програми введіть 0");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        int day1 = int.Parse(Console.ReadLine());
                        string mounth1 = Console.ReadLine();
                        int number = int.Parse(Console.ReadLine());
                        ht.Put(new Key(mounth1, day1), number);
                        break;
                    case 2:
                        int day2 = int.Parse(Console.ReadLine());
                        string mounth2 = Console.ReadLine();
                        double metasPrice = ht.Get(new Key(mounth2, day2));
                        Console.WriteLine("Meta Price: " + metasPrice);
                        break;
                    case 3:
                        int day3 = int.Parse(Console.ReadLine());
                        string mounth3 = Console.ReadLine();
                        bool t = ht.ContainsKey(new Key(mounth3, day3));
                        Console.WriteLine(t);
                        break;
                    case 4:
                        int day4 = int.Parse(Console.ReadLine());
                        string mounth4 = Console.ReadLine();
                        ht.Remove(new Key(mounth4, day4));
                        break;
                    case 5:
                        int n = ht.Size();
                        Console.WriteLine(n);
                        break;
                    case 0:
                        Console.WriteLine("Зараз завершимо, тільки натисніть будь ласка ще раз Enter");
                        break;
                    default:
                        Console.WriteLine("Команда ``{0}'' не розпізнана. Зробіь, будь ласка, вибір із 1, 2, 3, 0.", choice);
                        break;
                }
            } while (choice != 0);

            ht.Put(new Key("APPL", 223), 180.0);
            ht.Put(new Key("META", 300), 160.34);

            try
            {
                double metaPrice = ht.Get(new Key("META", 300));
                Console.WriteLine("Meta Price: " + metaPrice);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
