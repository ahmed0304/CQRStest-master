using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRStest.Queries;
namespace CQRStest.Commands
{
    public class CreateProductCommand: IRequest<ProductDisplayQuery>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }
    }
    public class DeleteProductCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class UpdateProductCommand: IRequest<ProductDisplayQuery>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }

    }
}
