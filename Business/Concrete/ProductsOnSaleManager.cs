using System;
using System.Collections.Generic;
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
        public IDataResult<List<ProductsOnSale>> GetAll()
        {
            var result = _productsOnSaleDal.GetAll();

            return new SuccessDataResult<List<ProductsOnSale>>(result);
        }

        [SecuredOperations("admin,user,customer")]
        [CacheAspect(10)]
        public IDataResult<ProductDetailDto> GetById(int id)
        {
            var result = _productsOnSaleDal.GetProductById(p=>p.Id == id);

            return new SuccessDataResult<ProductDetailDto>(result);
        }

        public IDataResult<List<ProductsOnSale>> GetUserProducts(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }

            var result = _productsOnSaleDal.GetAll(u => u.SellerId == id);

            return new SuccessDataResult<List<ProductsOnSale>>(result);
        }

        [TransactionScopeAspect]
        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public IResult Add(ProductsOnSale productsOnSale, IFormFile file, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }

            var product = new ProductsOnSale();

            product.Id = productsOnSale.Id;
            product.Name = productsOnSale.Name;
            product.Price = productsOnSale.Price;
            product.Description = productsOnSale.Description;
            product.EntryDate = DateTime.Now;
            product.SellerId = productsOnSale.SellerId;
            product.ImagePath = FileHelper.Add(file);


            _productsOnSaleDal.Add(product);

            return new SuccessResult($"Product {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public IResult Delete(int productId, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<ProductsOnSale>>(conditionResult.Message);
            }
            
            //Find product
            var product = _productsOnSaleDal.Get(p => p.Id == productId);

            if (product == null)
            {
                return new ErrorResult("Product not found!");
            }
            // Delete image from local.
            FileHelper.Delete(product.ImagePath);
            //Delete product from DB.
            _productsOnSaleDal.Delete(product);

            return new SuccessResult($"Product {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IProductsOnSaleService.Get")]
        public IResult Update(ProductsOnSale productsOnSale, int id, string securityKey)
        {
            _productsOnSaleDal.Update(productsOnSale);

            return new SuccessResult($"Product {Messages.SuccessfullyUpdated}");
        }
    }
}