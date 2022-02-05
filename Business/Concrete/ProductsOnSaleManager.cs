using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ProductsOnSaleManager : IProductsOnSaleService
    {
        private readonly IProductsOnSaleDal _productsOnSaleDal;
        private readonly IAuthService _authService;
        private IUserImageDal _userImageDal;

        public ProductsOnSaleManager(IProductsOnSaleDal productsOnSaleDal, IAuthService authService,
            IUserImageDal userImageDal)
        {
            _productsOnSaleDal = productsOnSaleDal;
            _authService = authService;
            _userImageDal = userImageDal;
        }

        [SecuredOperations("admin,user,customer")]
        [CacheAspect(10)]
        public async Task<IDataResult<List<ProductsOnSale>>> GetAll()
        {
            var result = await _productsOnSaleDal.GetAll();

            return new SuccessDataResult<List<ProductsOnSale>>(result);
        }

        [SecuredOperations("admin,user,customer")]
        [CacheAspect(10)]
        public async Task<IDataResult<ProductDetailDto>> GetById(int id)
        {
            var result = await _productsOnSaleDal.GetProductById(p=>p.Id == id);

            return new SuccessDataResult<ProductDetailDto>(result);
        }

        public async Task<IDataResult<List<ProductsOnSale>>> GetUserProducts(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }

            var result = await _productsOnSaleDal.GetAll(u => u.SellerId == id);

            return new SuccessDataResult<List<ProductsOnSale>>(result);
        }

        [TransactionScopeAspect]
        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public async  Task<IResult> Add(ProductsOnSale productsOnSale, IFormFile file, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }

            var product = new ProductsOnSale
            {
                Id = productsOnSale.Id,
                Name = productsOnSale.Name,
                Price = productsOnSale.Price,
                CategoryId = productsOnSale.CategoryId,
                Description = productsOnSale.Description,
                EntryDate = DateTime.Now,
                SellerId = productsOnSale.SellerId,
                ImagePath =  await FileHelper.Add(file)
            };



         await   _productsOnSaleDal.Add(product);

            return new SuccessResult($"Product {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public async Task<IResult> Delete(int productId, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }
            
            //Find product
            var product = await _productsOnSaleDal.Get(p => p.Id == productId);

            if (product == null)
            {
                return new ErrorResult("Product not found!");
            }
            // Delete image from local.
            FileHelper.Delete(product.ImagePath);
            //Delete product from DB.
            await _productsOnSaleDal.Delete(product);

            return new SuccessResult($"Product {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public async Task<IResult> Update(ProductsOnSale productsOnSale, int id, string securityKey)
        {
            await _productsOnSaleDal.Update(productsOnSale);

            return new SuccessResult($"Product {Messages.SuccessfullyUpdated}");
        }
    }
}