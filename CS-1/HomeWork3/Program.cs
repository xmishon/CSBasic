using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    class Program
    {
        // 1. а) Дописать структуру Complex, добавив метод вычитания комплексных чисел. Продемонстрировать работу структуры;
        //    б) Дописать класс Complex, добавив методы вычитания и произведения чисел.Проверить работу класса;
        static void Task1()
        {

        }

        // 2.
        static void Task2()
        {

        }

        //3.
        static void Task3()
        {

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
            Console.WriteLine("7 - Задание 7");
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
}
