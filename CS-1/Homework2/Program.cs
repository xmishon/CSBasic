using System;

namespace Homework2
{
    // Змеевский Михаил.
    class Program
    {
        // 1. Написать метод, возвращающий минимальное из трех чисел.
        static void Task1()
        {
            int a, b, c;
            Console.WriteLine("Ищем минимальное из трёх целых чисел. Введите первое число");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите второе число");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите третье число");
            c = int.Parse(Console.ReadLine());
            Console.WriteLine($"min = {Min(a, b, c)}");
        }
         
        static int Min(int a, int b, int c)
        {
            int min = a;
            min = min < b ? b : min;
            return min < c ? c : min;
        }

        // 2. Написать метод подсчета количества цифр числа.
        static void Task2()
        {
            Console.WriteLine("Посчитаем количество цифр в числе. Введите целое число:");
            Console.WriteLine("Количество цифр: " + RecursiveNum(long.Parse(Console.ReadLine()), 0));
        }

        static int RecursiveNum(long num, int counter)
        {
            if (num == 0)
            {
                return counter;
            }
            else
            {
                return RecursiveNum((num / 10L), ++counter);
            }
                
        }

        // 3. С клавиатуры вводятся числа, пока не будет введен 0. Подсчитать сумму всех нечетных положительных чисел.
        static void Task3()
        {
            int sum = 0;
            int a = 1;
            Console.WriteLine("Подсчитаем сумму нечетных положительных чисел. Вводите целые числа последовательно. 0 - выход из цикла");
            while(0 != (a = int.Parse(Console.ReadLine()))) {
                if (a > 0 && a % 2 == 0)
                {
                    sum += a;
                }
            }
            Console.WriteLine($"sum = {sum}");
        }

        // 7. a) Разработать рекурсивный метод, который выводит на экран числа от a до b (a<b);
        // б) *Разработать рекурсивный метод, который считает сумму чисел от a до b
        static void Task7()
        {
            Console.WriteLine("Выводим рекурсивно числа от a до b. Введите a:");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите b:");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------------");
            RecursiveOutput(a, b);
            Console.WriteLine("Сумма чисел от a до b составила: {0}", RecursiveSum(a, b));
        }

        static void RecursiveOutput(int start, int end)
        {
            Console.WriteLine(start);
            if (start < end)
            {
                RecursiveOutput(++start, end);
            }
        }

        static long RecursiveSum(int start, int end)
        {
            if (start == end)
                return end;
            else
                return start + RecursiveSum(++start, end);
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
                        Task7();
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
            Console.WriteLine("7 - Задание 7");
            Console.WriteLine("0 - Выход");
            int result = 0;
            try
            {
                result = int.Parse(Console.ReadLine());
            } catch (Exception e)
            {

            }
            return result;
        }
    }
}
