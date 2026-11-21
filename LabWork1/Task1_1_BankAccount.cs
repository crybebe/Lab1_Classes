using System;

namespace Task1_1_BankAccount
{
    public class BankAccount
    {
        // Поля
        private string _accountNumber;
        private string _ownerName;
        private decimal _balance;
        private static int _totalAccounts = 0;

        // Конструктор за замовчуванням
        public BankAccount()
        {
            _accountNumber = "0000";
            _ownerName = "Невідомо";
            _balance = 0;
            _totalAccounts++;
        }

        // Параметризований конструктор
        public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _ownerName = ownerName;
            _balance = initialBalance >= 0 ? initialBalance : 0;
            _totalAccounts++;
        }

        // Конструктор копіювання
        public BankAccount(BankAccount other)
        {
            _accountNumber = other._accountNumber;
            _ownerName = other._ownerName;
            _balance = other._balance;
            _totalAccounts++;
        }

        // Поповнення рахунку
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Помилка: сума поповнення має бути більшою за 0.");
                return;
            }
            _balance += amount;
            Console.WriteLine($"Рахунок {_accountNumber} поповнено на {amount:F2} грн.");
        }

        // Зняття коштів
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Помилка: сума зняття має бути більшою за 0.");
                return false;
            }
            if (amount > _balance)
            {
                Console.WriteLine($"Помилка: недостатньо коштів (баланс {_balance:F2} грн, запит {amount:F2} грн).");
                return false;
            }
            _balance -= amount;
            Console.WriteLine($"З рахунку {_accountNumber} знято {amount:F2} грн.");
            return true;
        }

        // Статичний метод — кількість створених рахунків
        public static int GetTotalAccounts()
        {
            return _totalAccounts;
        }

        public override string ToString()
        {
            return $"Рахунок: {_accountNumber}, Власник: {_ownerName}, Баланс: {_balance:F2} грн";
        }
    }

    public static class Runner
    {
        public static void Run()
        {

            Console.WriteLine("=== Створення об'єктів ===");
            BankAccount acc1 = new BankAccount();                               // за замовчуванням
            BankAccount acc2 = new BankAccount("1234", "Іван", 1000m);          // параметризований
            BankAccount acc3 = new BankAccount(acc2);                           // копіювання

            Console.WriteLine(acc1);
            Console.WriteLine(acc2);
            Console.WriteLine(acc3);

            Console.WriteLine("\n=== Deposit ===");
            acc2.Deposit(500m);
            acc2.Deposit(-100m);   // некоректна сума
            Console.WriteLine(acc2);

            Console.WriteLine("\n=== Withdraw ===");
            bool ok1 = acc2.Withdraw(300m);
            Console.WriteLine($"Результат: {ok1}");
            bool ok2 = acc2.Withdraw(5000m);   // недостатньо коштів
            Console.WriteLine($"Результат: {ok2}");
            bool ok3 = acc2.Withdraw(-10m);    // некоректна сума
            Console.WriteLine($"Результат: {ok3}");
            Console.WriteLine(acc2);

            Console.WriteLine("\n=== Копія незалежна від оригіналу ===");
            acc3.Deposit(100m);
            Console.WriteLine($"Оригінал: {acc2}");
            Console.WriteLine($"Копія:    {acc3}");

            Console.WriteLine($"\nВсього створено рахунків: {BankAccount.GetTotalAccounts()}");

        }
    }
}
