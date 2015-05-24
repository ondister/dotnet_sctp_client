using System;
using Ostis.Sctp;//бщее пространство имен, оно всегда обязательно
using Ostis.Sctp.Commands;// пространство имен команд для сервера
using Ostis.Sctp.Responses;// пространсво имен ответов сервера
using Ostis.Sctp.Arguments;//пространство имен аргументов команд для сервера
using System.Collections.Generic;

namespace Ostis.SctpDemo
{
	//ВНИМАНИЕ!!! Все примеры учебные, их правильное выполнение зависит от вашей базы. Если у вас нет элементов по указанным адресам - выйдут ошибки!!!
	//Все консультации и справки по скайпу ondister
	//Будете править код, сообщите хоть
	public class Demo
	{
		private CommandPool pool;
		public Demo ()
		{
			//Создаем пул команд. Работает только синхронный клиент. Это связано в ограничениями сервера. Он тоже только синхронный
		 pool = new CommandPool ("127.0.0.1", 55770, ClientType.SyncClient);
		}

//				Код: 0x01 
//				Команда: Проверка существования элемента с указанным sc-адресом 
//				Аргументы: ScAddress
//				Результат: True или False.
		public void demoCheckElement()
		{
			//статический класс command содержит все команды, доступные для использования
			ACommand cmd_chk_element = Command.CheckElement (new ScAddress (0, 1));
			//отсылаем команду.
			pool.Send (cmd_chk_element);
			//так как для каждой команды есть свой ответ, то преобразуем ответ к нужному типу
			//это конечно не очень безопасно, но точно получим то, что надо
			Console.WriteLine ((cmd_chk_element.Response as RspCheckElement).ElementIsExist);
			//дополнительно обратите внимание, что в ответе есть свойства Header  и BytesStream
			//Это тот самый нижний уровень ответа, который можно не приводить к типу, и который описан на вика тов. Корончика
		}

//				Код: 0x02
//				Команда: Получение типа sc-элемента по sc-адресу
//				Аргументы: ScAddress
//				Результат: ElementType (подробнее тут sctp_client.Arguments файл ElementType.cs
		public void demoGetElementType()
		{
			ACommand cmd_get_element_type = Command.GetElementType(new ScAddress (0, 0));
			pool.Send (cmd_get_element_type);
			Console.WriteLine ((cmd_get_element_type.Response as RspGetElementType).ElementType.ToString());
		}
//				Код: 0x03
//				Команда: Удаление sc-элемента с указанным sc-адресом
//				Аргументы: ScAddress
//				Результат: Команда возвращает true в случае удачного выполнения, если элемент удален успешно, иначе возвращается false.
		public void demoDeleteElement()
		{
			ACommand cmd_delete_element = Command.DeleteElement(new ScAddress (0, 0));
			pool.Send (cmd_delete_element);
			Console.WriteLine ((cmd_delete_element.Response as RspDeleteElement).ElementIsDelete);
		}

//				Код: 0x04
//				Команда: Создание нового sc-узла указанного типа
//				Аргументы: ElementType
		//				Результат: Если выполнение команды успешно, то в качестве результата возвращается адрес созданного sc-узла. Иначе адрес нулевой, то есть недействительный (сегмент и оффсет равны нулю)
		public void demoCreateNode()
		{
			ACommand cmd_create_node = Command.CreateNode(ElementType.sc_type_node_const);
			pool.Send (cmd_create_node);
			Console.WriteLine ((cmd_create_node.Response as RspCreateNode).CreatedNodeAddress.ToString());
		}

//				Код: 0x05
//				Команда: Создание новой sc-ссылки
//				Аргументы: нет
//				Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих адрес созданной sc-ссылки. Иначе адрес нулевой, то есть недействительный (сегмент и оффсет равны нулю)
		public void demoCreateLink()
		{
			//есть нюанс в создании ссылки. Сначала создается ссылка, а затем ей можно задать контент.
			//вы можете сделать команду более верхнего уровня объединив две в одну для удобства
			ACommand cmd_create_link = Command.CreateLink ();
			pool.Send (cmd_create_link);
			Console.WriteLine ((cmd_create_link.Response as RspCreateLink).CreatedLinkAddress.ToString());
		}
//				Код: 0x06
//				Команда: Создание новой sc-дуги указанного типа, с указнным начальным и конечным элементами
//				Аргументы:
//				тип создаваемой sc-дуги
//				sc-адрес начального элемента sc-дуги
//				sc-адрес конечного элемента sc-дуги 
//				Результат: Если выполнение команды успешно, то в качестве результата возвращается  адрес созданной sc-дуги. Иначе адрес нулевой, то есть недействительный (сегмент и оффсет равны нулю)
		public void demoCreateArc()
		{
			ACommand cmd_create_arc = Command.CreateArc(ElementType.sc_type_arc_const_comm,new ScAddress(0,1),new ScAddress(0,2));
			pool.Send (cmd_create_arc);
			Console.WriteLine ((cmd_create_arc.Response as RspCreateArc).CreatedArcAddress.ToString());
		}

//				Код: 0x07
//				Команда: Получение начального и конечного элемента sc-дуги
//				Аргументы:  - sc-адрес дуги у которой необходимо получить начальный и конечный элемент
//				Результат:  - sc-адрес начального элемента дуги 
//				sc-адрес конечного элементе дуги 
		public void demoGetArc()
		{
			ACommand cmd_get_arc = Command.GetArc(new ScAddress(0,1));
			pool.Send (cmd_get_arc);
			Console.WriteLine ((cmd_get_arc.Response as RspGetArc).BeginElementAddress.ToString());
			Console.WriteLine ((cmd_get_arc.Response as RspGetArc).EndElementAddress.ToString());
		}

//				Код: 0x08
//				Команда: 
//				Аргументы:
//				Результат:
//эта команда не реализована на сервере

//				Код: 0x09
//				Команда: Получение содержимого sc-ссылки
//				Аргументы: sc-адрес ссылки для получения содержимого
		//		Результат: Если выполнение команды успешно, то возвращается содержимое в виде структуры LinkContent. 
		public void demoGetLinkContent()
		{
			ACommand cmd_get_link_content = Command.GetLinkContent(new ScAddress(0,1));
			pool.Send (cmd_get_link_content);

			Console.WriteLine( System.Text.Encoding.UTF8.GetString ((cmd_get_link_content.Response as RspGetLInkContent).LinkContent));
		//или так
			LinkContent.ConvertToString((cmd_get_link_content.Response as RspGetLInkContent).LinkContent);
		//Так вот, если с установкой контента все достаточно просто, то с принятием контента немного сложности есть. 
		//дело в том, что в базе хранится массив байт, и только. А вот что там в этом массива, это вам указывать в виде каких-то конструкций
	    //потом эти конструкции читайте и преобразуйте байты в то, что вам нужно
		//

		}

//				Код: 0x0a
//				Команда: Поиск всех sc-ссылок с указанным содержимым
//				Аргументы: содержимое для поиска 
//				Результат: Если выполнение команды успешно, то в качестве результата возвращается коллекция адресов ссылок

		public void demoFindLinks()
		{
			ACommand cmd_find_links = Command.FindLinks(new LinkContent("aaaaa"));
			pool.Send (cmd_find_links);
			Console.WriteLine ((cmd_find_links.Response as RspFindLinks).LinksCount);
			List<ScAddress> adr = (cmd_find_links.Response as RspFindLinks).ScAddresses;

		}

//				Код: 0x0b
//				Команда: Установка содержимого sc-ссылки
//				Аргументы: sc-адрес ссылки 
//			    данные устанавливаемого содержимого 
//				Результат: True или False
		public void demoSetLinkContent()
		{
			ACommand cmd_set_content = Command.SetLinkContent(new ScAddress(0,1),  new LinkContent("aaa"));
			pool.Send (cmd_set_content);
			Console.WriteLine ((cmd_set_content.Response as RspSetLinkContent).ContentIsSet);

		}

//				Код: 0x0c
//				Команда: Найти конструкции по указанному 3-х или 5-ти элементному шаблону
//				Аргументы:  тип шаблонной конструкции для поиска 
//			Результат: Если выполнение команды успешно, то в качестве результата возвращается коллекция конструкций, соответствующих шаблону

		public void demoIterator()
		{
			ConstrTemplate tmpl = new ConstrTemplate (new ScAddress (0, 1), ElementType.sc_type_arc_access, ElementType.sc_type_node);
			ACommand cmd_iterator = Command.IterateElements (tmpl);
			pool.Send (cmd_iterator);
			Console.WriteLine ((cmd_iterator.Response as RspIterateElements).ConstructionsCount);
			List<Construction> lst = (cmd_iterator.Response as RspIterateElements).GetConstructions ();

		}
//				Код: 0x0d
//				Команда: Итерирование сложных конструкций (улучшить описание)
//				//пока не реализована на клиенте
//
//				Код: 0x0e
//				Команда: Создание подписки на событие
//				Аргументы: 
//				1 байт - тип события 
//				4 байта - адрес sc-элемента для подписки на событие
//				Результат: В случае, если подписка на событие произведена успешно, то возвращается id подписки состоящий из 4 байт. Если же подписаться на событие не удалось, то возвращается лишь заголовок ответа, который содержит код ошибки.
//
//				Код: 0x0f
//				Команда: Удаление подписки на событие
//				Аргументы: 4 байта - id подписки, которую необходимо удалить
//				Результат: В результате неудачнго выполнения запроса возвращается только заголовок с кодом ошибки. Если же запрос обработан без ошибок, то в заголовке будет код удачного выполнения а в результате будет 4 байта - id подписки, которая удалена
//
//				Код: 0x10
//				Команда: Запрос произошедших событий
//				Аргументы: нет
//				Результат: Если запрос обработан успешно, то в результате содержится количество произошедших событий 4 байта и описание всех этих событий. Каждое из которых описывается следующей последовательностью байт: 
//				4 байта - id события 
//				4 байта - адрес sc-элемента событие с которым произошло 
//				4 байта - аргумент события (адрес sc-дуги, которая была удалена/создана (для событий генерации/удаления дуги))
//
//				Код: 0xa0
//				Команда: Поиск sc-элемента по его системному идентификатору
//				Аргументы: 
//				4 байта - N (размер идентификатора в байтах) 
//				N байт - данные идентификатора 
//				Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих адрес найденого sc-эелемента. Иначе размер поля с результатами равен нулю (результатов нет)
//
//				Код: 0xa1
//				Команда: Установка системного идентификатора sc-элемента
//				Аргументы: 
//				4 байта - адрес sc-элемента 
//				4 байта - N (размер идентификатора в байтах) 
//				N байт - данные идентификатора 
//				Результат: 
//
//				Код: 0xa2
//				Команда: Получение статистики с сервера, в ременных границах. Время используется в формате http://en.wikipedia.org/wiki/Unix_time
//				Аргументы: 
//				8 байт - Нижняя временная граница 
//				8 байт - Верхняя временная граница 
//				Результат: Если выполнение команды успешно, то в качестве результата возвращаются 4 байта обозначающих количество временных отметок результаты которых возвращаются. Каждая такая временная отметка состоит из следуюзик полей: 
//				8 байт - Время сбора статистики
//				8 байт - Общее количество sc-узлов, которые есть в sc-памяти
//				8 байт - Общее количество sc-дуг, которые есть в sc-памяти
//				8 байт - Общее количество sc-ссылок, которые есть в sc-памяти
//				8 байт - Количество пустых ячеек в sc-памяти
//				8 байт - Общее количество подключений клиентов к sctp серверу (не активных, а общее число включая и завершенные)
//				8 байт - Количество обработанных sctp команд (включая обработанные с ошибками)
//				8 байт - Количество обработанных с ошибками sctp команд
//				1 байт - Флаг начального сбора статистики. Другими словами, если это значание равно 1, то статистика была собрана при запуске sctp сервера. Если значание равно 0, то статистика собрана уже во время работы сервера
//
//				Код: 0xa3
//				Команда: Получение версии протокола
//				Аргументы: нет
//				Результат: Если команда выполнена успешно, то в качестве результата возвращается 4 байта, которые обозначают версию используемого сервером sctp протокола. (способ кодирования версии необходимо уточнить)



	}
}

