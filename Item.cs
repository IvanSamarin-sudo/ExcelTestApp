using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApp
{
    /// <summary>
    /// Товар.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Код.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Единица измерения.
        /// </summary>
        public string MeasureUnit { get; set; }
        /// <summary>
        /// Цена.
        /// </summary>
        public double Price { get; set; }
    }
}
