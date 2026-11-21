using System;
using System.Collections.Generic;

namespace Task3_1_University
{
    // Клас кімнати — частина будівлі.
    // КОМПОЗИЦІЯ: Building створює Room всередині себе, Room не існує без Building.
    public class Room
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

        public Room(string number, string type, int capacity)
        {
            Number = number;
            Type = type;
            Capacity = capacity;
        }

        public override string ToString() => $"Аудиторія {Number} ({Type}, {Capacity} місць)";
    }

    // Кафедра — створюється всередині факультету.
    // КОМПОЗИЦІЯ: Faculty ---◆ Department
    public class Department
    {
        public string Name { get; set; }
        public string HeadName { get; set; }

        public Department(string name, string headName)
        {
            Name = name;
            HeadName = headName;
        }

        public override string ToString() => $"Кафедра \"{Name}\" (завідувач: {HeadName})";
    }

    // Професор — існує незалежно від факультету.
    // АГРЕГАЦІЯ: Faculty ---◇ Professor
    public class Professor
    {
        public string Name { get; set; }
        public string Degree { get; set; }

        // АСОЦІАЦІЯ: професор знає про кафедру (посилання), але не володіє нею.
        public Department Department { get; set; }

        public Professor(string name, string degree)
        {
            Name = name;
            Degree = degree;
        }

        public override string ToString()
        {
            string dep = Department != null ? Department.Name : "не призначено";
            return $"Професор {Name}, {Degree}, кафедра: {dep}";
        }
    }

    public class Faculty
    {
        private string _name;
        private List<Department> _departments; // Композиція — створюються всередині
        private List<Professor> _professors;   // Агрегація — передаються ззовні

        public Faculty(string name)
        {
            _name = name;
            _departments = new List<Department>();
            _professors = new List<Professor>();
        }

        // КОМПОЗИЦІЯ: кафедра створюється (new) всередині факультету
        public void CreateDepartment(string name, string headName)
        {
            _departments.Add(new Department(name, headName));
        }

        // Допоміжний метод: знайти кафедру факультету за назвою
        public Department GetDepartment(string name)
        {
            foreach (Department d in _departments)
            {
                if (d.Name == name)
                    return d;
            }
            return null;
        }

        // АГРЕГАЦІЯ: професор створений ззовні й лише передається у факультет
        public void AddProfessor(Professor professor)
        {
            if (professor != null)
                _professors.Add(professor);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Факультет: {_name}");
            Console.WriteLine("  Кафедри (композиція):");
            foreach (Department d in _departments)
                Console.WriteLine($"    {d}");
            Console.WriteLine("  Професори (агрегація):");
            foreach (Professor p in _professors)
                Console.WriteLine($"    {p}");
        }
    }

    // КОМПОЗИЦІЯ: Building ---◆ Room
    public class Building
    {
        private string _name;
        private List<Room> _rooms;

        public Building(string name)
        {
            _name = name;
            _rooms = new List<Room>();
        }

        // Кімната створюється всередині будівлі (композиція)
        public void AddRoom(string number, string type, int capacity)
        {
            _rooms.Add(new Room(number, type, capacity));
        }

        public Room FindRoom(string number)
        {
            foreach (Room room in _rooms)
            {
                if (room.Number == number)
                    return room;
            }
            return null;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Будівля: {_name}");
            foreach (var room in _rooms)
                Console.WriteLine($"  {room}");
        }
    }

    public static class Runner
    {
        public static void Run()
        {

            // 1. Факультет
            Faculty faculty = new Faculty("Факультет інформаційних технологій");

            // 2. Кафедри — композиція
            faculty.CreateDepartment("Програмної інженерії", "Мельник О.І.");
            faculty.CreateDepartment("Комп'ютерних систем", "Бондар В.П.");

            // 3. Професори — створюються окремо (існують незалежно)
            Professor prof1 = new Professor("Іваненко Андрій Петрович", "д.т.н.");
            Professor prof2 = new Professor("Петренко Марія Сергіївна", "к.т.н.");
            Professor prof3 = new Professor("Сидоренко Олег Іванович", "к.ф.-м.н.");

            // Асоціація: професор отримує посилання на кафедру
            prof1.Department = faculty.GetDepartment("Програмної інженерії");
            prof2.Department = faculty.GetDepartment("Програмної інженерії");
            prof3.Department = faculty.GetDepartment("Комп'ютерних систем");

            // 4. Агрегація — AddProfessor
            faculty.AddProfessor(prof1);
            faculty.AddProfessor(prof2);
            faculty.AddProfessor(prof3);

            // 5. Будівля та аудиторії — композиція
            Building building = new Building("Головний корпус");
            building.AddRoom("101", "Лекційна", 120);
            building.AddRoom("205", "Комп'ютерний клас", 25);
            building.AddRoom("312", "Лабораторія", 20);
            building.AddRoom("410", "Семінарська", 30);

            // 6. Виведення інформації
            Console.WriteLine("=== Інформація про факультет ===");
            faculty.DisplayInfo();

            Console.WriteLine("\n=== Інформація про будівлю ===");
            building.DisplayInfo();

            Console.WriteLine("\n=== Пошук аудиторії ===");
            Room found = building.FindRoom("205");
            Console.WriteLine(found != null ? $"Знайдено: {found}" : "Аудиторію не знайдено");
            Room notFound = building.FindRoom("999");
            Console.WriteLine(notFound != null ? $"Знайдено: {notFound}" : "Аудиторія 999 не знайдена");

            // 7. Професор існує незалежно від факультету
            Console.WriteLine("\n=== Незалежне існування професора (агрегація) ===");
            faculty = null; // факультет (і його кафедри) більше не доступні
            Console.WriteLine("Факультет видалено (faculty = null).");
            Console.WriteLine("Професор усе ще існує:");
            Console.WriteLine($"  {prof1.Name}, {prof1.Degree}");

        }
    }
}
