using System;

namespace Ostis.SctpDemo
{
    class Program
	{
		public static void Main (string[] args)
		{
            // Подробности работы приведённого примера смотри в классе Demo.
            var demo = new Demo();
			demo.CreateNode();
            Console.WriteLine("Программа успешно выполнена. Нажмите любую клавишу для завершения...");
		    Console.ReadKey();
		}
    }
}
