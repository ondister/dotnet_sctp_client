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

#warning TODO: 1. AssemblyInfo.cs - вписать себя в авторы и поправить
#warning TODO: 2. Создать ответ с ошибкой, который и возвращать при ошибке вызова
#warning TODO: 3. Добавить в синхронный клиент асинхронные методы
    }
}
