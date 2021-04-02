using AutoMapper;
using CQRStest.Commands;
using CQRStest.Models;
using CQRStest.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CQRStest.Handler
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDisplayQuery>
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public CreateProductHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            this._mapper = mapper;
        }

        public async Task<ProductDisplayQuery> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var lv_product = _mapper.Map<Product>(request);
                lv_product.IsOutOfStock = request.CurrentStock <= 0 ? true : false;
                lv_product.CurrentStock = request.CurrentStock < 0 ? 0 : request.CurrentStock;

                _storeDbContext.Product.Add(lv_product);
                await _storeDbContext.SaveChangesAsync();

                return _mapper.Map<ProductDisplayQuery>(lv_product);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot add Product #{request.Name} in the store because {e.Message}.");
            }
        }
    }


    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDisplayQuery>
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public UpdateProductHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            this._mapper = mapper;
        }

        public async Task<ProductDisplayQuery> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product lv_product = _storeDbContext.Product.Find(request.Id);

                if (lv_product != null)
                {
                    _mapper.Map(request, lv_product);
                    lv_product.IsOutOfStock = request.CurrentStock <= 0 ? true : false;
                    lv_product.CurrentStock = request.CurrentStock < 0 ? 0 : request.CurrentStock;

                    await _storeDbContext.SaveChangesAsync();
                }

                return _mapper.Map<ProductDisplayQuery>(lv_product);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot update Product #{request.Name} in the store because {e.Message}.");
            }
        }
    }


    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;
        public DeleteProductHandler(StoreDbContext storeDbContext, IMapper mapper)
        {
            this._storeDbContext = storeDbContext;
            this._mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product lv_product = _storeDbContext.Product.Find(request.Id);
                bool isSucces = false;
                if (lv_product != null)
                {
                    _storeDbContext.Remove(lv_product);
                    await _storeDbContext.SaveChangesAsync();

                    isSucces = true;
                }

                return isSucces;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot update Product #{request.Id} in the store because {e.Message}.");
            }
        }
    }

}
