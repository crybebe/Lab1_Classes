using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2_1_Products
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, string category, decimal price, int quantity)
        {
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalValue() => Price * Quantity;

        public override string ToString()
        {
            return $"{Name} [{Category}]: {Price:F2} грн x {Quantity} = {GetTotalValue():F2} грн";
        }
    }

    public static class Runner
    {
        public static void Run()
        {

            // TODO 1: масив з 8 продуктів, 3 категорії
            Product[] products = new Product[]
            {
                new Product("Ноутбук",     "Електроніка", 25000m, 5),
                new Product("Смартфон",    "Електроніка", 18000m, 10),
                new Product("Навушники",   "Електроніка", 1500m,  25),
                new Product("Хліб",        "Продукти",    35m,    100),
                new Product("Молоко",      "Продукти",    45m,    60),
                new Product("Сир",         "Продукти",    220m,   30),
                new Product("Футболка",    "Одяг",        400m,   40),
                new Product("Куртка",      "Одяг",        2800m,  12)
            };

            // TODO 2: вивід усіх продуктів
            Console.WriteLine("=== Усі продукти ===");
            foreach (Product p in products)
                Console.WriteLine(p);

            // TODO 3: найдорожчий (foreach)
            Product mostExpensive = products[0];
            foreach (Product p in products)
            {
                if (p.Price > mostExpensive.Price)
                    mostExpensive = p;
            }
            Console.WriteLine("\n=== Найдорожчий продукт ===");
            Console.WriteLine(mostExpensive);

            // TODO 4: найдешевший (foreach)
            Product cheapest = products[0];
            foreach (Product p in products)
            {
                if (p.Price < cheapest.Price)
                    cheapest = p;
            }
            Console.WriteLine("\n=== Найдешевший продукт ===");
            Console.WriteLine(cheapest);

            // TODO 5: загальна вартість
            decimal total = 0;
            foreach (Product p in products)
                total += p.GetTotalValue();
            Console.WriteLine($"\n=== Загальна вартість усіх продуктів: {total:F2} грн ===");

            // TODO 6: фільтр за категорією "Електроніка"
            Console.WriteLine("\n=== Категорія: Електроніка ===");
            foreach (Product p in products)
            {
                if (p.Category == "Електроніка")
                    Console.WriteLine(p);
            }

            // TODO 7: бульбашкове сортування за ціною (за зростанням)
            Product[] sorted = (Product[])products.Clone();
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                for (int j = 0; j < sorted.Length - 1 - i; j++)
                {
                    if (sorted[j].Price > sorted[j + 1].Price)
                    {
                        Product temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("\n=== Відсортовано за ціною (бульбашкове сортування) ===");
            foreach (Product p in sorted)
                Console.WriteLine(p);

            // TODO 8: List<Product> та LINQ
            List<Product> list = new List<Product>(products);

            Console.WriteLine("\n=== LINQ: Where (ціна > 1000 грн) ===");
            var expensive = list.Where(p => p.Price > 1000m);
            foreach (Product p in expensive)
                Console.WriteLine(p);

            Console.WriteLine("\n=== LINQ: OrderBy (за спаданням ціни) ===");
            var ordered = list.OrderByDescending(p => p.Price);
            foreach (Product p in ordered)
                Console.WriteLine(p);

            Console.WriteLine("\n=== LINQ: OrderBy (за назвою) ===");
            foreach (Product p in list.OrderBy(p => p.Name))
                Console.WriteLine(p);

            decimal avg = list.Average(p => p.Price);
            Console.WriteLine($"\n=== LINQ: Average — середня ціна: {avg:F2} грн ===");

        }
    }
}
