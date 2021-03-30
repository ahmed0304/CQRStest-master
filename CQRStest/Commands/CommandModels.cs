using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRStest.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }


    }
    public class DeleteProductCommand
    {
        public int Id { get; set; }
    }
    public class UpdateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }

    }
}
