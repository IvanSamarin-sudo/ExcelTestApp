using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApp
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Словарь с заказанными товарами. Ключ - количество товара, значение - товар.
        /// </summary>
        public Dictionary<int, Item> itemsDict;

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime date;
    }
}
