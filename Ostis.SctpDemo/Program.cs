using System;

namespace Ostis.SctpDemo
{
    class Program
	{
		public static void Main (string[] args)
		{
            // Подробности работы приведённого примера смотри в классе Demo.
            var demo = new Demo();

		    Console.WriteLine(string.Empty);
            Console.WriteLine("Проверка синхронной отправки:");
            var address = demo.CreateNode();

            Console.WriteLine(string.Empty);
            Console.WriteLine("Проверка синхронного получения созданного узла:");
            demo.CheckElement(address);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Проверка асинхронной отправки:");
            address = demo.CreateNodeAsync();

            Console.WriteLine(string.Empty);
            Console.WriteLine("Проверка асинхронного получения созданного узла:");
            demo.CheckElementAsync(address);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Программа успешно выполнена. Нажмите любую клавишу для завершения...");
		    Console.ReadKey();
		}
    }
}
