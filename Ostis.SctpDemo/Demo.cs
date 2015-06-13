using System;

using Ostis.Sctp;           // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
using Ostis.Sctp.Commands;  // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses; // пространство имен ответов сервера

namespace Ostis.SctpDemo
{
	// ВНИМАНИЕ! Все примеры учебные, их правильное выполнение зависит от настроек вашей базы.
    // Если у вас нет элементов по указанным адресам - выйдут ошибки.
	// Все консультации и справки по скайпу ondister.
	// Сообщите, если хотите внести в этот код исправления или дополнения.
	internal class Demo
	{
		private readonly CommandPool commandPool;

		public Demo()
		{
			// Создание пула команд, который будет отправлять запросы на сервер.
            // Обратите внимание на IP-адрес сервера! При работе с сервером на удалённой или виртуальной машине, нужно выставить здесь правильный IP-адрес.
            // Номер порта, к которому нужно подключаться, можно посмотреть в конфигурации OSTIS в файле .../config/scweb.ini (параметр Port в секции Network).
		    const string defaultAddress = "127.0.0.1";
            Console.WriteLine("Введите адрес сервера (ввод для {0}):", defaultAddress);
		    string serverAddress = Console.ReadLine();
		    if (string.IsNullOrEmpty(serverAddress))
		    {
		        serverAddress = defaultAddress;
		    }

            Console.WriteLine("Введите номер порта сервера (ввод для {0}):", SctpProtocol.DefaultPortNumber);
            int serverPort;
            if (!int.TryParse(Console.ReadLine(), out serverPort))
            {
                serverPort = SctpProtocol.DefaultPortNumber;
            }

            commandPool = new CommandPool(serverAddress, serverPort, false);
		}

        // Код: 0x01 
        // Команда: Проверка существования элемента с указанным sc-адресом 
        // Аргументы: ScAddress
        // Результат: True или False.
		public void CheckElement()
		{
			// выбираем команду из пространства имен Ostis.Sctp.Commands
			var command = new CheckElementCommand(new ScAddress(0, 1));
			// отправка команды на сервер
			commandPool.Send(command);
			// Так как для каждой команды есть свой тип ответа, то нетипизированный ответ сервера нужно преобразовать к требуемому типу.
			// Это, конечно, не очень безопасно, зато точно получим то, что надо.
            var response = (CheckElementResponse) command.Response;
			Console.WriteLine(response.ElementExists);
			// Дополнительно обратите внимание, что в ответе есть свойства Header и BytesStream.
			// Это тот самый нижний уровень ответа, который можно не приводить к типу, и который описан на wiki товарища Корончика.
		}

        // Код: 0x02
        // Команда: Получение типа sc-элемента по sc-адресу
        // Аргументы: ScAddress
        // Результат: ElementType (подробнее тут Ostis.Sctp.Arguments файл ElementType.cs
		public void GetElementType()
		{
            var command = new GetElementTypeCommand(new ScAddress(0, 0));
			commandPool.Send(command);
            var response = (GetElementTypeResponse) command.Response;
            Console.WriteLine(response.ElementType.ToString());
		}

        // Код: 0x03
        // Команда: Удаление sc-элемента с указанным sc-адресом
        // Аргументы: ScAddress
        // Результат: Команда возвращает true в случае удачного выполнения, если элемент удален успешно, иначе возвращается false.
		public void DeleteElement()
		{
            var command = new DeleteElementCommand(new ScAddress(0, 0));
			commandPool.Send(command);
            var response = (DeleteElementResponse) command.Response;
            Console.WriteLine(response.IsDeleted);
		}

        // Код: 0x04
        // Команда: Создание нового sc-узла указанного типа
        // Аргументы: ElementType
		// Результат: Если выполнение команды успешно, то в качестве результата возвращается адрес созданного sc-узла.
        // Иначе адрес нулевой, то есть недействительный (сегмент и смещение (offset) равны нулю).
		public void CreateNode()
		{
            var command = new CreateNodeCommand(ElementType.ConstantNode);
			commandPool.Send(command);
            var response = (CreateNodeResponse) command.Response;
            Console.WriteLine(response.CreatedNodeAddress.ToString());
		}

        // Код: 0x05
        // Команда: Создание новой sc-ссылки
        // Аргументы: нет
        // Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих адрес созданной sc-ссылки.
        // Иначе адрес нулевой, то есть недействительный (сегмент и смещение (offset равны нулю)
		public void CreateLink()
		{
			// Есть нюанс в создании ссылки. Сначала создается ссылка, а затем ей можно задать контент.
			// Вы можете сделать команду более высокого уровня, объединив две в одну для удобства.
            var command = new CreateLinkCommand();
			commandPool.Send(command);
            var response = (CreateLinkResponse) command.Response;
            Console.WriteLine(response.CreatedLinkAddress.ToString());
		}

        // Код: 0x06
        // Команда: Создание новой sc-дуги указанного типа, с указнным начальным и конечным элементами
        // Аргументы:
        // тип создаваемой sc-дуги
        // sc-адрес начального элемента sc-дуги
        // sc-адрес конечного элемента sc-дуги 
        // Результат: Если выполнение команды успешно, то в качестве результата возвращается  адрес созданной sc-дуги.
        // Иначе адрес нулевой, то есть недействительный (сегмент и смещение (offset) равны нулю).
		public void CreateArc()
		{
            var command = new CreateArcCommand(ElementType.ConstantCommonArc, new ScAddress(0, 1), new ScAddress(0, 2));
			commandPool.Send(command);
            var response = (CreateArcResponse) command.Response;
            Console.WriteLine(response.CreatedArcAddress.ToString());
		}

        // Код: 0x07
        // Команда: Получение начального и конечного элемента sc-дуги
        // Аргументы:  - sc-адрес дуги у которой необходимо получить начальный и конечный элемент
        // Результат:  - sc-адрес начального элемента дуги 
        // sc-адрес конечного элементе дуги 
		public void GetArc()
		{
            var command = new GetArcCommand(new ScAddress(0, 1));
			commandPool.Send(command);
            var response = (GetArcResponse) command.Response;
            Console.WriteLine(response.ToString());
            Console.WriteLine(response.EndElementAddress.ToString());
		}

        // Код: 0x08
        // Команда: 
        // Аргументы:
        // Результат:
        // Эта команда не реализована на сервере!

        // Код: 0x09
        // Команда: Получение содержимого sc-ссылки
        // Аргументы: sc-адрес ссылки для получения содержимого
		//		Результат: Если выполнение команды успешно, то возвращается содержимое в виде структуры LinkContent. 
		public void GetLinkContent()
		{
            var command = new GetLinkContentCommand(new ScAddress(0, 1));
			commandPool.Send(command);
            var response = (GetLinkContentResponse) command.Response;

            Console.WriteLine(SctpProtocol.TextEncoding.GetString(response.LinkContent));
		    // Так вот, если с установкой контента все достаточно просто, то с принятием контента немного сложности есть. 
		    // Дело в том, что в базе хранится массив байт, и только. А вот что там в этом массива, это вам указывать в виде каких-то конструкций.
	        // Потом эти конструкции читайте и преобразуйте байты в то, что вам нужно.
		}

        // Код: 0x0a
        // Команда: Поиск всех sc-ссылок с указанным содержимым
        // Аргументы: содержимое для поиска 
        // Результат: Если выполнение команды успешно, то в качестве результата возвращается коллекция адресов ссылок.
		public void FindLinks()
		{
            var command = new FindLinksCommand(new LinkContent("aaaaa"));
			commandPool.Send(command);
            var response = (FindLinksResponse) command.Response;
            Console.WriteLine(response.Addresses.Count);
            /*foreach (var address in response.ScAddresses)
		    {
		        Console.WriteLine(".. " + address.);
		    }*/
		}

        // Код: 0x0b
        // Команда: Установка содержимого sc-ссылки
        // Аргументы: sc-адрес ссылки 
        // данные устанавливаемого содержимого 
        // Результат: True или False
		public void SetLinkContent()
		{
            var command = new SetLinkContentCommand(new ScAddress(0, 1), new LinkContent("aaa"));
			commandPool.Send(command);
            var response = (SetLinkContentResponse) command.Response;
			Console.WriteLine(response.ContentIsSet);
		}

        // Код: 0x0c
        // Команда: Найти конструкции по указанному 3-х или 5-ти элементному шаблону
        // Аргументы:  тип шаблонной конструкции для поиска 
        // Результат: Если выполнение команды успешно, то в качестве результата возвращается коллекция конструкций, соответствующих шаблону.
		public void Iterator()
		{
			var template = new ConstructionTemplate(new ScAddress(0, 1), ElementType.AccessArg, ElementType.Node);
            var command = new IterateElementsCommand(template);
			commandPool.Send(command);
            var response = (IterateElementsResponse) command.Response;
            Console.WriteLine(response.Constructions.Count);
            /*foreach (var construction in response.GetConstructions())
		    {
		        Console.WriteLine(".. " + construction.);
		    }*/
		}

// Код: 0x0d
// Команда: Итерирование сложных конструкций (улучшить описание)
// //пока не реализована на клиенте
//
// Код: 0x0e
// Команда: Создание подписки на событие
// Аргументы: 
// 1 байт - тип события 
// 4 байта - адрес sc-элемента для подписки на событие
// Результат: В случае, если подписка на событие произведена успешно, то возвращается id подписки состоящий из 4 байт. Если же подписаться на событие не удалось, то возвращается лишь заголовок ответа, который содержит код ошибки.
//
// Код: 0x0f
// Команда: Удаление подписки на событие
// Аргументы: 4 байта - id подписки, которую необходимо удалить
// Результат: В результате неудачнго выполнения запроса возвращается только заголовок с кодом ошибки. Если же запрос обработан без ошибок, то в заголовке будет код удачного выполнения а в результате будет 4 байта - id подписки, которая удалена
//
// Код: 0x10
// Команда: Запрос произошедших событий
// Аргументы: нет
// Результат: Если запрос обработан успешно, то в результате содержится количество произошедших событий 4 байта и описание всех этих событий. Каждое из которых описывается следующей последовательностью байт: 
// 4 байта - id события 
// 4 байта - адрес sc-элемента событие с которым произошло 
// 4 байта - аргумент события (адрес sc-дуги, которая была удалена/создана (для событий генерации/удаления дуги))
//
// Код: 0xa0
// Команда: Поиск sc-элемента по его системному идентификатору
// Аргументы: 
// 4 байта - N (размер идентификатора в байтах) 
// N байт - данные идентификатора 
// Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих адрес найденого sc-эелемента. Иначе размер поля с результатами равен нулю (результатов нет)
//
// Код: 0xa1
// Команда: Установка системного идентификатора sc-элемента
// Аргументы: 
// 4 байта - адрес sc-элемента 
// 4 байта - N (размер идентификатора в байтах) 
// N байт - данные идентификатора 
// Результат: 
//
// Код: 0xa2
// Команда: Получение статистики с сервера, в ременных границах. Время используется в формате http://en.wikipedia.org/wiki/Unix_time
// Аргументы: 
// 8 байт - Нижняя временная граница 
// 8 байт - Верхняя временная граница 
// Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих количество временных отметок результаты которых возвращаются. Каждая такая временная отметка состоит из следуюзик полей: 
// 8 байт - Время сбора статистики
// 8 байт - Общее количество sc-узлов, которые есть в sc-памяти
// 8 байт - Общее количество sc-дуг, которые есть в sc-памяти
// 8 байт - Общее количество sc-ссылок, которые есть в sc-памяти
// 8 байт - Количество пустых ячеек в sc-памяти
// 8 байт - Общее количество подключений клиентов к sctp серверу (не активных, а общее число включая и завершенные)
// 8 байт - Количество обработанных sctp команд (включая обработанные с ошибками)
// 8 байт - Количество обработанных с ошибками sctp команд
// 1 байт - Флаг начального сбора статистики. Другими словами, если это значание равно 1, то статистика была собрана при запуске sctp сервера. Если значание равно 0, то статистика собрана уже во время работы сервера
//
// Код: 0xa3
// Команда: Получение версии протокола
// Аргументы: нет
// Результат: Если команда выполнена успешно, то в качестве результата возвращается 4 байта, которые обозначают версию используемого сервером sctp протокола. (способ кодирования версии необходимо уточнить)
	}
}

