namespace Pract6
{
    class Porgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла, который хотете открыть");
            string? put = Console.ReadLine();
            if (put == null)
            {
                return;
            }
            Converter converter = new Converter();
            List<string> lines = converter.ReadFile(put);
            Redaktor red = new Redaktor(lines);
            bool closed = red.StartEdit();
            if (closed)
            {
                return;
            }
            
            Console.WriteLine("Введите путь до файла, куда вы хотите сохранить текст");
            put = Console.ReadLine();
            if (put == null)
            {
                return;
            }
            converter.SaveToFile(lines, put);
        }
    }
}