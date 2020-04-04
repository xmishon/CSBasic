using System;
using System.IO;

namespace HomeWork4
{
    class Program
    {
        private static String arrayFilename = "../../myArray.txt";
        private static String accountFilename = "../../Accounts.txt";

        static void Main(string[] args)
        {
            // Демонстрация задания 1
            Console.WriteLine(" - - - Задание 1 - - -");
            MyArray myArray = new MyArray(20, -10000, 10000);
            Console.WriteLine(myArray);
            myArray.CheckPairs(3);
            Console.WriteLine();

            // Демонстрация задания 2
            Console.WriteLine(" - - - Задание 2 - - -");
            myArray = new MyArray(20, 2, (byte)3);
            Console.WriteLine(myArray);
            Console.WriteLine("Сумма элементов массива равна {0}", myArray.Sum);
            Console.WriteLine("Меняем знаки у всех элементов массива:");
            myArray.Inverse();
            Console.WriteLine(myArray);
            Console.WriteLine("Умножаем каждый элемент массива на 4:");
            myArray.Multi(4);
            Console.WriteLine(myArray);
            Console.WriteLine("Создадим массив с несколькими одинаковыми максимальными элементами");
            int[] a = { 4, 6, 24, 14, 24, 6, 0, 24, 15, -3, -10, 24, 5};
            myArray = new MyArray(a);
            Console.WriteLine(myArray);
            Console.WriteLine("Количество максимальных элементов в массиве: {0}", myArray.MaxCount);
            Console.WriteLine("Записываем массив в файл");
            myArray.WriteToFile(arrayFilename);
            Console.WriteLine("Читаем из этого же файла");
            myArray = new MyArray(arrayFilename);
            Console.WriteLine(myArray);
            myArray.ReadFromFile(arrayFilename);
            Console.WriteLine(myArray);
            Console.WriteLine();

            // Демонстрация задания 3
            Console.WriteLine(" - - - Задание 3 - - -");
            Console.WriteLine("Создаём экземпляр класса accountChecker");
            AccountChecker accountChecker = new AccountChecker(accountFilename);
            accountChecker.checkAccounts();

            Console.ReadKey();
        }
    }

    class MyArray
    {
        public int[] a;

        // Конструктор по умолчанию
        public MyArray()
        {
            a = new int[10];
        }

        // Создание массива и заполнение его значениями el
        public MyArray(int n, int el)
        {
            a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = el;
            }
        }

        // Создание массива и заполнение его случайными числами от min до max
        public MyArray(int n, int min, int max)
        {
            a = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                a[i] = rnd.Next(min, max);
        }

        // Создание массива размерности n, заполняющегося значениями от first с шагом step
        public MyArray(int n, int first, byte step)
        {
            a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = first + step * i;
        }

        // Конструктор, принимающий на вход массив (используется для демонстрации свойства MaxCount в задании 2)
        public MyArray(int[] a)
        {
            this.a = new int[a.Length];
            a.CopyTo(this.a, 0);
        }

        // Конструктор, принимающий на вход путь к файлу и заполняющий массив из файла
        public MyArray(String filename)
        {
            ReadFromFile(filename);
        }

        public int Max
        {
            get
            {
                int max = a[0];
                for (int i = 1; i < a.Length; i++)
                    if (a[i] > max) max = a[i];
                return max;
            }
        }
        public int Min
        {
            get
            {
                int min = a[0];
                for (int i = 1; i < a.Length; i++)
                    if (a[i] < min) min = a[i];
                return min;
            }
        }

        public int CountPositiv
        {
            get
            {
                int count = 0;
                for (int i = 0; i < a.Length; i++)
                    if (a[i] > 0) count++;
                return count;
            }
        }

        // Возвращает количество пар элементов массива arr, в которых хотя бы одно число делится на div
        // P.S.: в методичке было указано "только одно число делится на 3", а на сайте "хотя бы одно число делится на 3".
        // Я реализовал задание, как указано на сайте - хотя бы одно число делится на div. 
        // Чтобы сделать так, чтобы только одно число делилось на div, достаточно в условии || заменить на ^.
        public static void CheckPairs(int[] arr, int div)
        {
            int count = 0;
            for(int i = 0; i < arr.Length - 1; i++)
            {
                if (((arr[i] % div) == 0) || (((arr[i + 1]) % div) == 0))
                {
                    count++;
                }
            }
            Console.WriteLine("Количество пар элементов, делящихся на {0}: {1}", div, count);
        }

        public void CheckPairs(int div)
        {
            CheckPairs(a, div);
        }

        // Свойство, возвращающее количество элементов массива
        public int Sum
        {
            get
            {
                int sum = 0;
                foreach (int elem in a)
                    sum += elem;
                return sum;
            }
        }

        // Изменение знаков у всех элементов массива
        public void Inverse()
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = -a[i];
        }

        public override string ToString()
        {
            string s = "";
            foreach (int v in a)
                s = s + v + " ";
            return s;
        }

        // Умножение всех элементов массива на число m
        public void Multi(int m)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] *= m;
        }

        // Возвращает количество максимальных элементов массива
        public int MaxCount
        {
            get
            {
                int max;
                int count = 0;
                if (a != null && a.Length > 0)
                {
                    if (a.Length > 1)
                    {
                        max = a[0];
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
                foreach (int elem in a)
                {
                    if (elem > max)
                    {
                        count = 1;
                        max = elem;
                    }
                    else if (elem == max)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        // Запись элементов в файл
        public void WriteToFile(String filename)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filename);
                sw.WriteLine(a.Length);
                foreach (int elem in a)
                    sw.WriteLine(elem);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Указанная директория не существует.\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        public void ReadFromFile(String filename)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filename);
                try
                {
                    int n = int.Parse(sr.ReadLine());
                    a = new int[n];
                    for(int i = 0; i < n; i++)
                    {
                        a[i] = int.Parse(sr.ReadLine());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Файл содержит неверные данные! \n" + e.Message);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден" + e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Указанная директория не существует.\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }

    class AccountChecker
    {
        private static String login = "root";
        private static String password = "GeekBrains";

        private Account[] accounts;

        // Создаёт экземпляр класса, заполняя массив accounts логинами и паролями из файла filename
        public AccountChecker(String filename)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filename);
                try
                {
                    int n = int.Parse(sr.ReadLine());
                    accounts = new Account[n];
                    for (int i = 0; i < n; i++)
                    {
                        accounts[i].Login = sr.ReadLine();
                        accounts[i].Password = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Файл содержит неверные данные! \n" + e.Message);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден" + e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Указанная директория не существует.\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        // Выводит соответствующую надпись, если среди аккаунтов из файла есть правильный аккаунт
        public void checkAccounts()
        {
            foreach(Account account in accounts)
            {
                if (checkAccount(account))
                {
                    Console.WriteLine("Найден подходящий аккаунт с логином: {0}", account.Login);
                }
            }
        }

        private bool checkAccount(Account account)
        {
            return account.Login.Equals(login) && account.Password.Equals(password);
        }
    }

    struct Account
    {
        public String Login;
        public String Password;
    }
        
}
