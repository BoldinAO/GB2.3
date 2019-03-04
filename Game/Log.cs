using System.IO;
using System.Text;

namespace MyGame
{
    static class Log
    {
        //Путь до файла лога
        static readonly string path = "log.txt";

        /// <summary>
        /// Запись текста в файл log.txt
        /// </summary>
        /// <param name="text">текст для записи в файл</param>
        static public void Write(string text)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(text);
            }
        }
    }
}
