using System;

namespace HomeWork1
{
    class Program
    {
        // Змеевский Михаил.
        static void Main(string[] args)
        {
            // Задание 1. Написать программу «Анкета». Последовательно задаются вопросы (имя, фамилия, возраст, рост, вес). 
            // В результате вся информация выводится в одну строчку.
            // а) используя склеивание;
            // б) используя форматированный вывод;
            // в) *используя вывод со знаком $.
            Console.WriteLine("Введите ваше имя:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите вашу фамилию:");
            string fname = Console.ReadLine();
            Console.WriteLine("Ведите ваш возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите ваш рост (в метрах): ");
            double height = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите ваш вес (в килограммах):");
            double weight = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine(name + " " + fname + ", возраст (лет): " + age + ", рост: " + height + " м, вес: " + weight + " кг");
            Console.WriteLine("{0} {1}, возраст (лет): {2}, рост: {3} м, вес: {4} кг", name, fname, age, height, weight);
            Console.WriteLine($"{name} {fname}, возраст (лет): {age}, рост: {height} м, вес: {weight} кг");
            Console.WriteLine();

            // Задание 2. Рассчитать и вывести индекс массы тела
            double weightIndex = weight / (height * height);
            Console.WriteLine("Индекс массы тела: {0}", weightIndex);

            // Задание 3. 
            // а) Написать программу, которая подсчитывает расстояние между точками с координатами x1, y1 и x2,y2 
            // по формуле r=Math.Sqrt(Math.Pow(x2-x1,2)+Math.Pow(y2-y1,2). Вывести результат, используя спецификатор 
            // формата .2f (с двумя знаками после запятой);
            // б) *Выполните предыдущее задание, оформив вычисления расстояния между точками в виде метода;
            Console.WriteLine("Подстчитаем расстояние между точками. Введите координату x1: ");
            double x1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите координату y1: ");
            double y1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите координату x2: ");
            double x2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите координату y2: ");
            double y2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Расстояние между двумя точками ({0}, {1}) и ({2}, {3}): {4}", x1, y1, x2, y2, Distance(x1, y1, x2, y2));
        }

        public static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
