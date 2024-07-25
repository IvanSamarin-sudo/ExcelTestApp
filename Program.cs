using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Data;
using ClosedXML.Excel;
using ExcelTestApp.Query;
using SQLTestApp;

namespace SQLTestApp;

class Program
{
    static void Main(string[] args)
    {
        List<Item> items = new List<Item>();

        List<Order> orders = new List<Order>();

        GetInfoQuery getInfoQuery = new GetInfoQuery();
        RenameClientQuery renameClientQuery = new RenameClientQuery();
        GoldClientQuery goldClientQuery = new GoldClientQuery();
        MostActiveYearClientQuery mostActiveYearClientQuery = new MostActiveYearClientQuery();
        MostActiveMonthClientQuery mostActiveMonthClientQuery = new MostActiveMonthClientQuery();

        List<string> clientsNames = new List<string>()
            {
                "Иванов Иван Иванович",
                "Петров Петр Петрович",
                "Юлина Юлия Юлиановна",
                "Сидоров Сидор Сидорович",
                "Павлов Павел Павлович",
                "Георгиев Георг Георгиевич"
            };

        List<Client> clients = new List<Client>();

        foreach (var name in clientsNames)
        {
            clients.Add(new Client(name));
        }

        //По-хорошему повторяющиеся строки с описанием перенести в .resx файл
        var rootCommand = new RootCommand
        {
            new Option<bool>("-h", "Получить справку и список команд"),
            new Option<bool>("--help", "Получить справку и список команд"),
            new Option<string>("-path", "Указание пути к файлу с данными"),
            new Option<string>("-getinfo", "По наименованию товара выводит информацию о клиентах, заказавших этот товар, с указанием информации по количеству товара, цене и дате заказа"),
            new Option<string>("-rename", "Запрос на переименование клиента."),
            new Option<string>("-gold", "Проверка на статус золотого клиента."),
            new Option<string>("-year", "Получить самого активного клиента за год."),
            new Option<string>("-month", "Получить самого активного клиента за месяц.")
        };

        rootCommand.Handler = CommandHandler.Create((ParseResult parseResult) =>
        {
            var h = parseResult.ValueForOption<bool>("-h");
            var help = parseResult.ValueForOption<bool>("--help");
            var path = parseResult.ValueForOption<string>("-path");
            var getinfo = parseResult.ValueForOption<string>("-getinfo");
            var rename = parseResult.ValueForOption<string>("-rename");
            var gold = parseResult.ValueForOption<string>("-gold");
            var year = parseResult.ValueForOption<string>("-year");
            var month = parseResult.ValueForOption<string>("-month");

            if (h || help)
            {
                Console.WriteLine("  -h, --help  Получить справку и список команд");
                Console.WriteLine("  -path       Указание пути к файлу с данными");
                Console.WriteLine("  -getinfo    По наименованию товара выводит информацию о клиентах, заказавших этот товар, с указанием информации по количеству товара, цене и дате");
                Console.WriteLine("  -rename    Запрос на переименование клиента");
                Console.WriteLine("  -gold    Проверка на статус золотого клиента");
                Console.WriteLine("  -year    Получить самого активного клиента за год");
                Console.WriteLine("  -month    Получить самого активного клиента за месяц");
            }
            else
            {
                if (string.IsNullOrEmpty(path))
                {
                    Console.WriteLine("Укажите путь к файлу:");
                    path = Console.ReadLine();
                }

                if (!string.IsNullOrEmpty(path))
                {
                    items = ReadExcelFile.ReadXlsxFile(path);

                    Console.WriteLine("Файл загружен успешно. Содержание:");
                    foreach (var item in items)
                    {
                        Console.WriteLine(item.Id.ToString() + " " + item.Name + " " + item.MeasureUnit + " " + item.Price.ToString());
                    }


                    if (!string.IsNullOrEmpty(getinfo))
                    {
                        Console.WriteLine(getInfoQuery.GetInfo(getinfo, clients, orders));
                    }

                    if (!string.IsNullOrEmpty(rename))
                    {
                        string[] parts = rename.Split(' ');

                        renameClientQuery.RenameClient(parts[0], parts[1], clients);
                    }

                    if (!string.IsNullOrEmpty(gold))
                    {
                        if (goldClientQuery.GoldClient(gold, clients))
                            Console.WriteLine("{0} - золотой клиент.", gold);
                        else
                            Console.WriteLine("{0} - не золотой клиент.", gold);
                    }

                    if (!string.IsNullOrEmpty(year))
                    {
                        Console.WriteLine("Самый активный клиент за прошедший год - " + mostActiveYearClientQuery.MostActiveYearClient(clients).Name);
                    }

                    if (!string.IsNullOrEmpty(month))
                    {
                        Console.WriteLine("Самый активный клиент за прошедший месяц - " + mostActiveMonthClientQuery.MostActiveMonthClient(clients).Name);
                    }
                }
                else
                {
                    Console.WriteLine("Не получилось получить путь к файлу.");
                }
            }
        });

        rootCommand.Invoke(args);
    }
}