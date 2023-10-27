using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class StringExtension
    {
        // Создаем статический метод расширения с модификатором this
        public static List<string> Split(this string s, int maxLength)
        {
            // Создаем список для хранения подстрок
            List<string> subs = new List<string>();
            // Пока строка не пустая, повторяем цикл
            while (s.Length > 0)
            {
                // Если длина строки больше максимальной длины подстроки
                if (s.Length > maxLength)
                {
                    // Обрезаем строку с начала до максимальной длины подстроки включительно
                    string sub = s.Substring(0, maxLength);
                    // Добавляем подстроку в список
                    subs.Add(sub);
                    // Удаляем подстроку из исходной строки
                    s = s.Remove(0, maxLength);
                }
                else
                {
                    // Иначе добавляем всю оставшуюся строку в список
                    subs.Add(s);
                    // Очищаем исходную строку
                    s = "";
                }
            }

            return subs;
        }
    }
}
