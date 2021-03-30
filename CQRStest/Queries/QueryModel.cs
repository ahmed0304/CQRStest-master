using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRStest.Queries
{
    public class ProductDisplayQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsOutOfStock { get; set; }
    }
}
