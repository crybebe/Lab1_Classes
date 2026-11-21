using System;

namespace LabWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Лабораторна робота: Класи та об'єкти в C# ===");
                Console.WriteLine("1 - Завдання 1.1 (BankAccount)");
                Console.WriteLine("2 - Завдання 1.2 (Student)");
                Console.WriteLine("3 - Завдання 2.1 (Product, масиви, LINQ)");
                Console.WriteLine("4 - Завдання 3.1 (Університет: асоціація, агрегація, композиція)");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": Task1_1_BankAccount.Runner.Run(); break;
                    case "2": Task1_2_Student.Runner.Run(); break;
                    case "3": Task2_1_Products.Runner.Run(); break;
                    case "4": Task3_1_University.Runner.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися до меню...");
                Console.ReadKey();
            }
        }
    }
}
