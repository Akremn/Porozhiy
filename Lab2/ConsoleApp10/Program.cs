using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    class TuringMachine
    {
        private char[] tape; // Стрічка (тейп)
        private int headPosition; // Позиція голови
        private char[] alphabet; // Алфавіт
        private Dictionary<string, Tuple<char, int, string>> transitions; // Матриця переходів
        private string currentState; // Поточний стан
        private string initialState; // Початковий стан

        public TuringMachine(string input, char[] alphabet, Dictionary<string, Tuple<char, int, string>> transitions, string initialState, int headPosition)
        {
            tape = ("_" + input + "_").ToCharArray(); // Початкова стрічка з пустими символами на початку та в кінці
            this.alphabet = alphabet;
            this.transitions = transitions;
            this.initialState = initialState;
            currentState = initialState;
            this.headPosition = headPosition + 1; // Додаємо 1, оскільки додали "_" на початку стрічки
        }

        public void Simulate()
        {
            while (currentState != "#")
            {
                PrintCurrentState();
                if (!alphabet.Contains(tape[headPosition]))
                {
                    Console.WriteLine("Error: Неприпустимий символ на стрічці.");
                    return;
                }

                var key = currentState + "_" + tape[headPosition];
                if (transitions.ContainsKey(key))
                {
                    var transition = transitions[key];
                    tape[headPosition] = transition.Item1;
                    headPosition += transition.Item2;
                    currentState = transition.Item3;
                }
                else
                {
                    Console.WriteLine("Error: Немає визначеного переходу для даного стану та символу.");
                    return;
                }

                if (headPosition < 0 || headPosition >= tape.Length)
                {
                    Console.WriteLine("Error: Голова виходить за межі стрічки.");
                    return;
                }
            }

            Console.WriteLine("\nРезультат:");
            PrintResult();
        }

        private void PrintCurrentState()
        {
            Console.WriteLine("Стрічка: " + new string(tape));
            Console.WriteLine("Позиція голови: " + headPosition);
            Console.WriteLine("Поточний перехід: " + currentState + " -> " + tape[headPosition]);
            Console.WriteLine("---------------------------------------------");
            Console.ReadKey(); // Затримка для демонстрації кроку машини Тьюрінга
        }

        private void PrintResult()
        {
            string result = new string(tape).Trim('_');
            Console.WriteLine("Непусте слово: " + result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = { '0', '1', '_', 'e', 'o' };

            // Матриця переходів (стан_символ: (заміна, напрям, новий_стан))
            var transitions = new Dictionary<string, Tuple<char, int, string>>
        {
            { "s0_0", new Tuple<char, int, string>('0', 1, "s1") },
            { "s0_1", new Tuple<char, int, string>('1', 1, "s1") },
            { "s0_e", new Tuple<char, int, string>('e', 0, "#") },
            { "s0_o", new Tuple<char, int, string>('o', 0, "#") },

            { "s1_0", new Tuple<char, int, string>('0', 1, "s0") },
            { "s1_1", new Tuple<char, int, string>('1', 1, "s0") },
            { "s1_e", new Tuple<char, int, string>('e', 0, "#") },
            { "s1_o", new Tuple<char, int, string>('o', 0, "#") }
        };

            string initialState = "s0";
            int headPosition = 0; // Початкова позиція голови

            Console.WriteLine("Введіть слово (з символів 0 або 1):");
            string input = Console.ReadLine();

            TuringMachine turingMachine = new TuringMachine(input, alphabet, transitions, initialState, headPosition);

            Console.WriteLine("\nДемонстрація поетапного процесу роботи машини Тьюрінга:\n");
            turingMachine.Simulate();
        }
    }
}
