using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3_Task3
{
   
    class Program
    {
        // Метод підраховує кількість унікальних (різних) слів у тексті (Завдання 14б)
        static int CountUniqueWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;

            // Символи-роздільники слів у тексті
            char[] delimiters = new char[]
            {
                ' ', ',', '.', '!', '?', ';', ':', '-', '(', ')',
                '"', '\'', '\t', '\n', '\r', '[', ']', '{', '}'
            };

            // Розбиваємо текст на масив слів, приводимо до нижнього регістру (ToLower)
            // та вибираємо унікальні слова за допомогою Distinct()
            string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            int uniqueWordsCount = words.Select(w => w.ToLower()).Distinct().Count();

            return uniqueWordsCount;
        }

        // Автоматична підготовка файлу input.txt у разі його відсутності
        static void PrepareInputFile(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                // Запис тестового тексту через StreamWriter (згідно з методичкою)
                using (StreamWriter writer = new StreamWriter(inputPath))
                {
                    writer.WriteLine("Студент виконує лабораторну роботу.");
                    writer.WriteLine("Студент вивчає мову програмування C#.");
                    writer.WriteLine("Робота з файлами у C# є важливою та цікавою!");
                }
                Console.WriteLine($"\n[ІНФО] Створено вхідний файл: {Path.GetFullPath(inputPath)}");
            }
        }

        
        static void Main(string[] args)
        {
            // Налаштування UTF-8 кодування у консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string inputPath = "input.txt";
            string outputPath = "output.txt";

            // Створюємо початковий файл input.txt, якщо його немає
            PrepareInputFile(inputPath);

            while (true)
            {
                
                Console.WriteLine(" ЗАВДАННЯ 3 ");
               
                Console.WriteLine($"Натисніть Enter для зчитування з '{inputPath}' та запису в '{outputPath}'");
                Console.WriteLine("або введіть 0 для виходу з програми:");
                

                string choice = Console.ReadLine();

                // Вихід при введенні 0
                if (choice.Trim() == "0")
                {
                    Console.WriteLine("\nЗавершення роботи програми.");
                    break;
                }

                try
                {
                    if (!File.Exists(inputPath))
                    {
                        Console.WriteLine($"\nПомилка! Вхідний файл '{inputPath}' відсутній.");
                        continue;
                    }

                    string textFromFile = "";

                    // 1. Потокове зчитування даних з input.txt через StreamReader
                    using (StreamReader reader = new StreamReader(inputPath))
                    {
                        textFromFile = reader.ReadToEnd(); // Зчитуємо весь вміст файлу
                    }

                    Console.WriteLine("\nВміст вхідного файлу input.txt:");
                   
                    Console.WriteLine(textFromFile);
                   

                    // 2. Виконання алгоритму підрахунку різних (унікальних) слів (Завдання 14б)
                    int uniqueCount = CountUniqueWords(textFromFile);

                    // 3. Запис результату у новостворений файл output.txt через StreamWriter
                    using (StreamWriter writer = new StreamWriter(outputPath, false)) // false — перезапис файлу
                    {
                        writer.WriteLine("=== РЕЗУЛЬТАТ ОБРОБКИ (Завдання 14-б) ===");
                        writer.WriteLine($"Кількість різних (унікальних) слів у тексті: {uniqueCount}");
                        writer.WriteLine($"Дата виконання: {DateTime.Now}");
                    }

                    Console.WriteLine($"\n[УСПІХ] Обробку завершено!");
                    Console.WriteLine($"Кількість унікальних слів: {uniqueCount}");
                    Console.WriteLine($"Результати записано у файл: {Path.GetFullPath(outputPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nВиникла помилка під час обробки файлів: {ex.Message}");
                }
            }
        }
    }
}