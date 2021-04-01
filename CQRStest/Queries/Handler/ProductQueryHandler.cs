using AutoMapper;
using CQRStest.Commands;
using CQRStest.Models;
using CQRStest.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRStest.Queries.Handler
{
    public class ProductQueryHandler
    {
        private StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;

        public ProductQueryHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            _mapper = mapper;

        }


        public IEnumerable<ProductDisplayQuery> Handler()
        {
            List<Product> lv_products = _storeDbContext.Product.ToList();
            List<ProductDisplayQuery> query = new List<ProductDisplayQuery>();
            _mapper.Map(lv_products, query);
            return query;
        }

        public ProductDisplayQuery Handler(ProductDisplayQuery query)
        {
            try
            {
                Product lv_product = _storeDbContext.Product.Find(query.Id);
                
                if (lv_product != null)
                {
                    _mapper.Map(lv_product, query);
                    return query;
                }

                return null;
                
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot find Product #{query.Id} in the store, {e.Message}.");
            }
           
        }
    }
}
