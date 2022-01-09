using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Util
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /*** Daos ***/
            kernel.Bind<IUserDao>().To<UserDaoEntityFramework>();

            kernel.Bind<IImageDao>().To<ImageDaoEntityFramework>();
            kernel.Bind<ICategoryDao>().To<CategoryDaoEntityFramework>();

            kernel.Bind<ICommentDao>().To<CommentDaoEntityFramework>();

            /*** Services ***/
            kernel.Bind<IImageService>().To<ImageService>();

            kernel.Bind<IUserService>().To<UserService>();

            /*** DbContext ***/
            string connectionString =
                ConfigurationManager.ConnectionStrings["PracticaMaDConnectionString"].ConnectionString;

            kernel.Bind<DbContext>().
                    ToSelf().
                    InSingletonScope().
                    WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}