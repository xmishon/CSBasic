using System;
using System.Collections.Generic;
using System.IO;

namespace HomeWork6
{
    // Змеевский Михаил

    public delegate double Fun(double x, double a);

    class Program
    {
        // 1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double, double). 
        // Продемонстрировать работу на функции с функцией a* x^2 и функцией a* sin(x).
        static void Task1()
        {
            Console.WriteLine("Вывод функции a*x^2 с a = 2.5 в интервале (1, 10)");
            Table(aX2, 1, 10, 2.5d);
            Console.WriteLine("\nВывод функции a*sin(x) с a = 2.5 в интервале (1, 10)");
            Table(aSinX, 1, 10, 2.5d);
        }

        public static void Table(Fun F, double x, double b, double a)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x, a));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static double aX2(double x, double a)
        {
            return a * Math.Pow(x, 2);
        }
        
        public static double aSinX(double x, double a)
        {
            return a * Math.Sin(x);
        }

        // 2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
        // а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
        // б) Используйте массив (или список) делегатов, в котором хранятся различные функции.
        // в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений. Пусть она возвращает минимум через параметр.
        static void Task2()
        {
            Func[] f = { aX2, aSinX }; // Перегруженные функции без параметра 'a' aX2, aSinX написаны ниже
            Console.WriteLine("Выберите функцию, для которой будем искать минимум:\n" +
                "1 - a*x^2,\n2 - a*sin(x)");
            int num = int.Parse(Console.ReadLine());

            if (num > f.Length) num = f.Length - 1;
            if (num < 1) num = 1;

            Console.WriteLine("Поиск идёт в интервале от a до b. Введите a:");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите b:");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите шаг расчёта:");
            double h = double.Parse(Console.ReadLine());
            Console.WriteLine("Минимум функции на указанном интервале - {0:0.000}", Min(f[num - 1], a, b, h));
        }

        delegate double Func(double x);

        private static string fileName = "../../funcResult.txt";

        static double Min(Func F, double a, double b, double h)
        {
            SaveFunc(fileName, a, b, h, F);
            Load(fileName, out double min);
            return min;
        }

        static void SaveFunc(string fileName, double a, double b, double h, Func F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }

        static List<double> Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            min = double.MaxValue;
            double d;
            List<double> list = new List<double>();
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                list.Add(d);
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return list;
        }

        public static double aX2(double x)
        {
            return Math.Pow(x, 2);
        }

        public static double aSinX(double x)
        {
            return Math.Sin(x);
        }

        //3. Переделать программу «Пример использования коллекций» для решения следующих задач:
        //а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
        //б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив);
        //в) отсортировать список по возрасту студента;
        //г) *отсортировать список по курсу и возрасту студента;
        //д) разработать единый метод подсчета количества студентов по различным параметрам
        //   выбора с помощью делегата и методов предикатов.
        // P.S.: к сожалению не все пункты успел реализовать.
        static void Task3()
        {
            List<Student> students = ReadStudentsFile(studentsFileName);
            //а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
            Console.WriteLine("Кол-во студентов на 5 и 6 курсах: {0}", CountOfStudents(students, 5) + CountOfStudents(students, 6));
            //в) отсортировать список по возрасту студента;
            Console.WriteLine("\nСортируем список по возрасту студента");
            students.Sort(CompareByAge);
            foreach (Student student in students)
            {
                Console.WriteLine(student.firstName + " " + student.lastName + ", возраст: " + student.age);
            }
            //г) *отсортировать список по курсу и возрасту студента;
            Console.WriteLine("\nСортируем по курсу и возрасту студента");
            students.Sort(CompareByCourseAndAge);
            foreach(Student student in students)
            {
                Console.WriteLine($"Курс: {student.course}, возраста: {student.age}, {student.firstName} {student.lastName}");
            }
        }

        static string studentsFileName = "../../students.csv";

        static int CompareByFirstName(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            return String.Compare(st1.firstName, st2.firstName);          // Сравниваем две строки
        }

        static int CompareByAge(Student st1, Student st2)
        {
            if (st1.age == st2.age) return 0;
            else if (st1.age < st2.age) return -1;
            else return 1;
        }

        static int CompareByCourseAndAge(Student st1, Student st2)
        {
            if (st1.course == st2.course) return CompareByAge(st1, st2);
            else if (st1.course < st2.course) return -1;
            else return 1;
        }

        static List<Student> ReadStudentsFile(String studentsFileName)
        {
            List<Student> list = new List<Student>();                             // Создаем список студентов
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader(studentsFileName);
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(',');
                    // Добавляем в список новый экземпляр класса Student
                    list.Add(new Student(s[0], s[1], s[3], s[4], s[5], int.Parse(s[6]), int.Parse(s[7]), int.Parse(s[8]), s[9]));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                }
            }
            sr.Close();
            return list;
        }

        static int CountOfStudents(List<Student> students, int courseNumber)
        {
            int count = 0;
            foreach(Student student in students)
            {
                if (student.course == courseNumber)
                    count++;
            }
            return count;
        }

        static void Main(string[] args)
        {
            int taskNumber = Menu();
            while (taskNumber != 0)
            {
                switch (taskNumber)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    default:
                        break;
                }
                Console.WriteLine(); // чтобы визуально отделить одно задание от другого
                taskNumber = Menu();
            }
        }

        static int Menu()
        {
            Console.WriteLine("1 - Задание 1");
            Console.WriteLine("2 - Задание 2");
            Console.WriteLine("3 - Задание 3");
            Console.WriteLine("0 - Выход");
            int result = 0;
            try
            {
                result = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }

    //К заданию 3
    public class Student
    {
        public string lastName;
        public string firstName;
        public string university;
        public string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        public int age;
        // Создаем конструктор
        public Student(string firstName, string lastName, string university, string faculty, string department, int age, int course, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }
    }

}
