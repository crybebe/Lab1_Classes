using System;

namespace Task1_2_Student
{
    public class Student
    {
        private string _name;
        private int _age;
        private double _gpa;

        // Властивість з валідацією: не null і не порожній рядок
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я не може бути порожнім.", nameof(Name));
                _name = value;
            }
        }

        // Властивість з валідацією: 16..60
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 16 || value > 60)
                    throw new ArgumentOutOfRangeException(nameof(Age), value, "Вік має бути в діапазоні від 16 до 60.");
                _age = value;
            }
        }

        // Властивість з валідацією: 0.0..5.0
        public double GPA
        {
            get { return _gpa; }
            set
            {
                if (value < 0.0 || value > 5.0)
                    throw new ArgumentOutOfRangeException(nameof(GPA), value, "GPA має бути в діапазоні від 0.0 до 5.0.");
                _gpa = value;
            }
        }

        // Автовластивість: читання ззовні, запис лише всередині класу
        public string StudentId { get; private set; }

        // Обчислювана властивість
        public bool IsHonorStudent
        {
            get { return _gpa >= 4.5; }
        }

        public Student(string name, int age, double gpa, string studentId)
        {
            Name = name;      // використовуємо властивості, щоб спрацювала валідація
            Age = age;
            GPA = gpa;
            StudentId = studentId;
        }

        public override string ToString()
        {
            return $"Студент: {Name}, Вік: {Age}, GPA: {GPA:F2}, ID: {StudentId}, " +
                   $"Відмінник: {(IsHonorStudent ? "Так" : "Ні")}";
        }
    }

    public static class Runner
    {
        public static void Run()
        {

            Console.WriteLine("=== Успішне створення ===");
            Student s1 = new Student("Олена Коваль", 19, 4.8, "KN-001");
            Student s2 = new Student("Петро Шевченко", 20, 3.9, "KN-002");
            Console.WriteLine(s1);
            Console.WriteLine(s2);

            Console.WriteLine("\n=== Зміна властивостей ===");
            s2.GPA = 4.6;
            Console.WriteLine(s2);

            Console.WriteLine("\n=== Некоректні значення ===");

            try
            {
                Student bad = new Student("", 20, 4.0, "KN-003");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка (Name): {ex.Message}");
            }

            try
            {
                s1.Age = 10;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Помилка (Age): {ex.Message}");
            }

            try
            {
                s1.GPA = 6.5;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Помилка (GPA): {ex.Message}");
            }

            // s1.StudentId = "X";  // Помилка компіляції: set — private

            Console.WriteLine($"\nСтудент після спроб змін: {s1}");

        }
    }
}
