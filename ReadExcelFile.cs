using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApp
{
    public class ReadExcelFile
    {
        /// <summary>
        /// Прочитать .xlsx файл.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Список элеметов товара.</returns>
        public static List<Item> ReadXlsxFile(string filePath)
        {
            var items = new List<Item>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);

                var rows = worksheet.RowsUsed();

                for (int i = 2; i <= rows.Count(); i++)
                {
                    var row = worksheet.Row(i);
                    var item = new Item
                    {
                        Id = row.Cell(1).GetValue<int>(),
                        Name = row.Cell(2).GetValue<string>(),
                        MeasureUnit = row.Cell(3).GetValue<string>(),
                        Price = row.Cell(4).GetValue<double>()
                    };
                    items.Add(item);
                }
            }


            return items;
        }
    }
}
