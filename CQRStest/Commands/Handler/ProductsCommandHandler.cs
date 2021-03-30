using AutoMapper;
using CQRStest.Commands;
using CQRStest.Models;
using CQRStest.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CQRStest.Handler
{
    public class ProductsCommandHandler
    {
        private StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public ProductsCommandHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            _mapper = mapper;
        }

        public int Handler(CreateProductCommand command)
        {
            try
            {
                int id = 0;
                if (command != null)
                {
                    Product lv_product = new Product();
                    _mapper.Map(command, lv_product);
                    lv_product.IsOutOfStock = command.CurrentStock <= 0 ? true : false;
                    lv_product.CurrentStock = command.CurrentStock < 0 ? 0 : command.CurrentStock;
                    
                    _storeDbContext.Product.Add(lv_product);
                    _storeDbContext.SaveChanges();

                    id = lv_product.Id;
                }

                return id;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot add Product #{command.Name} in the store because {e.Message}.");
            }
        }


        public bool Handler(int id, UpdateProductCommand command)
        {
            try
            {
                bool isExist = false;
                if (id > 0 && command != null)
                {
                    Product lv_product = _storeDbContext.Product.Find(id);

                    if (lv_product != null)
                    {
                        isExist = true;
                        _mapper.Map(command,lv_product);
                        lv_product.IsOutOfStock = command.CurrentStock <= 0 ? true : false;
                        lv_product.CurrentStock = command.CurrentStock < 0 ? 0 : command.CurrentStock;

                        _storeDbContext.SaveChanges();
                    }
                    
                }

                return isExist;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot update Product #{command.Name} in the store because {e.Message}.");
            }
        }

        public bool Handler(DeleteProductCommand command)
        {
            try
            {
                bool isExist = false;
                if (command.Id > 0)
                {
                    Product lv_product = _storeDbContext.Product.Find(command.Id);
                    if (lv_product != null)
                    {
                        isExist = true;
                        _storeDbContext.Remove(lv_product);
                        _storeDbContext.SaveChanges();
                    }

                }

                return isExist;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot delete Product with Id #{command.Id} in the store because {e.Message}.");
            }
        }

    }
}
