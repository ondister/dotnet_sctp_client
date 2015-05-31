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
		    Console.ReadKey();
		}

#warning TODO: Создать ответ с ошибкой, который и возвращать при ошибке вызова
#warning TODO: Добавить в синхронный клиент асинхронные методы
    }
}
