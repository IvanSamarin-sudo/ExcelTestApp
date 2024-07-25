using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApp
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Заказы.
        /// </summary>
        public List<Order> orders;
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Название организации.
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// Является ли клиент золотым.
        /// </summary>
        public bool GoldClient { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name"></param>
        public Client(string name)
        {
            Name = name;
            orders = new List<Order>();
        }
    }
}
