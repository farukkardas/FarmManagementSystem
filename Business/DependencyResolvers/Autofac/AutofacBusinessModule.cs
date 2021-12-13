using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT.Abstract;
using Core.Utilities.Security.JWT.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<CalfManager>().As<ICalfService>().SingleInstance();
            builder.RegisterType<EfCalfDal>().As<ICalfDal>().SingleInstance();

            builder.RegisterType<CowManager>().As<ICowService>().SingleInstance();
            builder.RegisterType<EfCowDal>().As<ICowDal>().SingleInstance();

            builder.RegisterType<BullManager>().As<IBullService>().SingleInstance();
            builder.RegisterType<EfBullDal>().As<IBullDal>().SingleInstance();

            builder.RegisterType<SheepManager>().As<ISheepService>().SingleInstance();
            builder.RegisterType<EfSheepDal>().As<ISheepDal>().SingleInstance();
            
            builder.RegisterType<FuelConsumptionManager>().As<IFuelConsumptionService>().SingleInstance();
            builder.RegisterType<EfFuelConsumptionDal>().As<IFuelConsumptionDal>().SingleInstance();
            
            builder.RegisterType<FertilizerManager>().As<IFertilizerService>().SingleInstance();
            builder.RegisterType<EfFertilizerDal>().As<IFertilizerDal>().SingleInstance();

            builder.RegisterType<ProvenderManager>().As<IProvenderService>().SingleInstance();
            builder.RegisterType<EfProvenderDal>().As<IProvenderDal>().SingleInstance();

            builder.RegisterType<MilkSalesManager>().As<IMilkSalesService>().SingleInstance();
            builder.RegisterType<EfMilkSalesDal>().As<IMilkSalesDal>().SingleInstance();
            
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();
            
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            
            builder.RegisterType<UserImageManager>().As<IUserImageService>();
            builder.RegisterType<EfUserImageDal>().As<IUserImageDal>();

            builder.RegisterType<AnimalSalesManager>().As<IAnimalSalesService>();
            builder.RegisterType<EfAnimalSalesDal>().As<IAnimalSalesDal>();
            
            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>();
            
            builder.RegisterType<ProductsOnSaleManager>().As<IProductsOnSaleService>();
            builder.RegisterType<EfProductsOnSaleDal>().As<IProductsOnSaleDal>();

            builder.RegisterType<BasketManager>().As<IBasketService>();
            builder.RegisterType<EfBasketDal>().As<IBasketDal>();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}