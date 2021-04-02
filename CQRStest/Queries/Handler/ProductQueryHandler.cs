using AutoMapper;
using CQRStest.Commands;
using CQRStest.Models;
using CQRStest.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRStest.Queries.Handler
{
    public class GetAllProductsHandler : IRequestHandler<AllProductDisplayQuery,List<ProductDisplayQuery>>
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            this._mapper = mapper;
        }

        public async Task<List<ProductDisplayQuery>> Handle(AllProductDisplayQuery request,CancellationToken cancellationToken)
        {
            try
            {
                List<Product> lv_products = await _storeDbContext.Product.ToListAsync();
                return _mapper.Map(lv_products, new List<ProductDisplayQuery>());
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Find Products in the store because {e.Message}.");
            }
        }
    }

    public class GetSingleProductHandler : IRequestHandler<ProductDisplayQuery, ProductDisplayQuery>
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public GetSingleProductHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            this._mapper = mapper;
        }

        public async Task<ProductDisplayQuery> Handle(ProductDisplayQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Product lv_product = await _storeDbContext.Product.FindAsync(request.Id);

                if (lv_product != null)
                {
                    _mapper.Map(lv_product, request);
                    return request;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot find Product #{request.Id} in the store, {e.Message}.");
            }
        }
    }

}
