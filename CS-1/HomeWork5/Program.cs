using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HomeWork5
{
    class Program
    {
        private static string messageFileName = "../../message.txt";

        static void Main(string[] args)
        {
            // Демонстрация задания 1
            Console.WriteLine("Введите логин и нажмите Enter (если логин пустой - выход из цикла)");
            while (true)
            {
                string str = Console.ReadLine();
                if (str.Length == 0) break;
                Console.WriteLine(isCorrectLogin(str) ? "Корректный" : "Не корректный");
            }
            Console.WriteLine("Проверка логина с использованием Regex");
            Console.WriteLine("Введите логин и нажмите Enter (если логин пустой - выход из цикла)");
            while (true)
            {
                string str = Console.ReadLine();
                if (str.Length == 0) break;
                Console.WriteLine(isCorrectLoginRegex(str) ? "Корректный" : "Не корректный");
            }

            // Демонстрация задания 2
            Console.WriteLine("Читаем строку из файла");
            Message message = new Message(messageFileName); // некоторые слова в файле я искусственно удлинил, чтобы было несколько самых длинных слов
            Console.WriteLine("Удаляем из сообщения слова, заканчивающиеся на заданную букву. Введите эту букву:");
            string temp = Console.ReadLine();
            char ch = ';'; // просто любая не буква на случай если пользователь ничего не введёт
            if (temp.Length > 0)
                ch = temp[0];
            message.DeleteWords(ch);
            Console.WriteLine("Новое сообщение:");
            message.PrintMessage();

            Console.WriteLine("\nСамое длинное слово в сообщении: ");
            message.PrintTheLongestWord();

            Console.WriteLine("\nСписок самых длинных слов, сформированный с помощью stringBuilder'а:");
            Console.WriteLine(message.GetTheLongestWords().ToString());
            Console.ReadLine();
        }

// Задание 1 -------------------------------------------------------------------------------------
        static bool isCorrectLogin(string login)
        {
            if(login.Length >= 2 && login.Length <= 10)
            {
                if(char.IsDigit(login[0]))
                {
                    return false;
                }
                else
                {
                    for (int i = 1; i < login.Length; i++)
                        if (!char.IsLetterOrDigit(login[i]))
                            return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        
        static bool isCorrectLoginRegex(string login)
        {
            Regex expression = new Regex("^[A-z]{1}[A-z\\d]{1,9}$");
            return expression.IsMatch(login);
        }        
    }
    
    // Задание 2 -------------------------------------------------------------------------------------
    
    public class Message
    {
        private string message;
        private string[] words; //массив с отдельными словами
        private int[] wordsLength; //массив с длинами слов

        public Message(string messageFileName)
        {
            message = ReadFromFile(messageFileName);
            words = SplitWordsArray(message);
            wordsLength = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                wordsLength[i] = words[i].Length;
            }
            Console.WriteLine(message);
        }

        private string[] SplitWordsArray(string message)
        {
            string[] words = message.Split(' ');
            for (int j = 0; j < words.Length; j++) // удаляем из слов знаки препинания
            {
                string word = words[j];
                for (int i = 0; i < word.Length; i++)
                {
                    if (!char.IsLetterOrDigit(word[i]))
                    {
                        words[j] = word.Remove(i, 1);
                    }
                }
            }
            return words;
        }

        // а) Вывести только те слова сообщения, которые содержат не более n букв
        public void WordsWithNumberOfLetters(int n)
        {
            string pattern = "^{1," + n + "}$";
            Regex regex = new Regex(pattern);
            foreach(string word in words)
            {
                if (regex.IsMatch(word))
                {
                    Console.WriteLine(word);
                }
            }
        }
        // б) Удалить из сообщения все слова, которые заканчиваются на заданный символ
        public void DeleteWords(char symbol)
        {
            string pattern = ".*" + symbol + "$";
            Regex regex = new Regex(pattern);
            foreach (string word in words)
            {
                if (regex.IsMatch(word))
                    message = message.Replace(word, "");
            }
            this.words = SplitWordsArray(message);

            wordsLength = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                wordsLength[i] = words[i].Length;
            }
            Console.WriteLine(message);
        }
        // в) найти самое длинное слово сообщения (возвращает последнее найденное)
        public void PrintTheLongestWord()
        {
            int[] maxIndexes = MaxIndexes();
            Console.WriteLine(words[maxIndexes[maxIndexes.Length - 1]]);
        }

        // г) Сформировать строку с помощью StringBuilder из самых длинных слов сообщения.
        public StringBuilder GetTheLongestWords()
        {
            StringBuilder sb = new StringBuilder();
            int[] maxIndexes = MaxIndexes();
            foreach(int index in maxIndexes)
            {
                sb.Append(words[index]).Append(" ");
            }
            return sb;
        }

        private static int Max(int[] arr) // ищет максимальное число в массиве
        {
            int max = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > max) max = arr[i];
            return max;
        }

        private int[] MaxIndexes() // получает индексы самых длинных слов
        {
            int count = 0;
            int max = Max(wordsLength);
            for (int i = 0; i < wordsLength.Length; i++)
                if (wordsLength[i] == max)
                    count++;
            int[] indexes = new int[count];
            int j = 0;
            for(int i = 0; i < wordsLength.Length; i++)
            {
                if (wordsLength[i] == max)
                {
                    indexes[j++] = i;
                }
            }
            return indexes;
        }

        public void PrintMessage()
        {
            Console.WriteLine(message);
        }

        private static string ReadFromFile(String filename)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filename);
                try
                {
                    return sr.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Файл содержит неверные данные! \n" + e.Message);
                    return null;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден" + e.Message);
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Указанная директория не существует.\n" + e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
}
