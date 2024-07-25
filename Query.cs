using DocumentFormat.OpenXml.Bibliography;
using SQLTestApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTestApp
{
    /// <summary>
    /// По наименованию товара выводит информацию о клиентах, заказавших этот товар, с указанием информации по количеству товара, цене и дате заказа.
    /// </summary>
    public class GetInfoQuery
    {
        public string GetInfo(string itemName, List<Client> clients, List<Order> orders)
        {
            string res = string.Empty;

            foreach (var client in clients)
            {
                int count = 0;

                foreach (var order in orders)
                {
                    var matchingKey = order.itemsDict.FirstOrDefault(kvp => kvp.Value.Name.Contains(itemName)).Key;

                    count += matchingKey;
                }
                res += "\r\n" + client.Name + "\r\nСтатистика по заказу товара" + itemName.ToString() + ": " + count.ToString() + " заказов.";
            }

            return res;
        }
    }
    /// <summary>
    /// Запрос на переименование клиента.
    /// </summary>
    public class RenameClientQuery
    {
        public void RenameClient(string oldName, string newName, List<Client> clients)
        {
            foreach (var client in clients)
            {
                if (client.Name.Contains(oldName))
                    client.Name = newName;
                else
                    throw new ArgumentException("Не найден пользователь с указанным именем");
            }
        }
    }
    /// <summary>
    /// Запрос на получение статуса золотого клиента.
    /// </summary>
    public class GoldClientQuery
    {
        public bool GoldClient(string clientName, List<Client> clients)
        {
            foreach (var client in clients)
            {
                if (client.Name.Contains(clientName))
                    return client.GoldClient;
            }
            return false;
        }
    }
    /// <summary>
    /// Запрос на получение информации о клиенте с самым большим количеством заказов за год.
    /// </summary>
    public class MostActiveYearClientQuery
    {
        public Client MostActiveYearClient(List<Client> clients)
        {
            Dictionary<int, Client> sum = new Dictionary<int, Client>();

            foreach (var client in clients)
            {
                foreach (var order in client.orders)
                {
                    if (order.date >= DateTime.Now.AddYears(-1))
                    {
                        sum.Add(order.itemsDict.Keys.Sum(), client);
                    }
                }
            }

            return sum.OrderByDescending(o => o.Key).First().Value;
        }
    }
    /// <summary>
    /// Запрос на получение информации о клиенте с самым большим количеством заказов за месяц.
    /// </summary>
    public class MostActiveMonthClientQuery
    {
        public Client MostActiveMonthClient(List<Client> clients)
        {
            Dictionary<int, Client> sum = new Dictionary<int, Client>();

            foreach (var client in clients)
            {
                foreach (var order in client.orders)
                {
                    if (order.date >= DateTime.Now.AddMonths(-1))
                    {
                        sum.Add(order.itemsDict.Keys.Sum(), client);
                    }
                }
            }

            return sum.OrderByDescending(o => o.Key).First().Value;
        }
    }
}
