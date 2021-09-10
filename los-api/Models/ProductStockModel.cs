using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public class ProductStockModel : ProductModel
    {
        public int StockId { get; set; }
        public int Amount { get; set; }

    }
}
