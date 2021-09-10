using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
    }
}
