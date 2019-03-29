



//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EBBS_1.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EBBS_1.App_Start.NinjectWebCommon), "Stop")]

//namespace EBBS_1.App_Start
//{
//    using System;
//    using System.Web;
//    using Ninject;
//    using Ninject.Web.Common;
//    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//    using System.Web.Security;
//    using EBBS_1.Service.IService;
//    using EBBS_1.Service.Service;



//    public static class NinjectWebCommon
//    {
//        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

//        //        /// <summary>
//        //        /// Starts the application
//        //        /// </summary>
//        public static void Start()
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
//            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
//            bootstrapper.Initialize(CreateKernel);
//        }

//        /// <summary>
//        /// Stops the application.
//        /// </summary>
//        public static void Stop()
//        {
//            bootstrapper.ShutDown();
//        }

//        //        /// <summary>
//        //        /// Creates the kernel that will manage your application.
//        //        /// </summary>
//        //        /// <returns>The created kernel.</returns>
//        private static IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel();
//            try
//            {
//                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
//                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
//                RegisterServices(kernel);
//                return kernel;
//            }
//            catch
//            {
//                kernel.Dispose();
//                throw;
//            }
//        }

//        //        /// <summary>
//        //        /// Load your modules or register your services here!
//        //        /// </summary>
//        //        /// <param name="kernel">The kernel.</param>
//        //        private static void RegisterServices(IKernel kernel)
//        //        {
//        //            kernel.Bind<IPostService>().To<PostService>();
//        //            kernel.Bind<IUserService>().To<UserService>();
//        //            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
//        //            kernel.Bind<IImageService>().To<ImageService>();
//        //            kernel.Bind<ICategoryService>().To<CategoryService>();
//        //            kernel.Bind<ICommentService>().To<CommentService>();
//        //            kernel.Bind<ISettingService>().To<SettingService>();
//        //            kernel.Bind<IRoleService>().To<RoleService>();
//        //            kernel.Bind<ISecurityQuestionService>().To<SecurityQuestionService>();
//        //            kernel.Bind<IDEncryptionService>().To<DEncryptionService>();

//        //        }
//        //    }
//        //}



//        public class HttpApplicationInitializationHttpModule : DisposableObject, IHttpModule
//        {
//            private readonly Func<IKernel> lazyKernel;

//            /// <summary>
//            /// Initializes a new instance of the <see cref="HttpApplicationInitializationHttpModule"/> class.
//            /// </summary>
//            /// <param name="lazyKernel">The kernel retriever.</param>
//            public HttpApplicationInitializationHttpModule(Func<IKernel> lazyKernel)
//            {
//                this.lazyKernel = lazyKernel;
//            }

//            /// <summary>
//            /// Initializes a module and prepares it to handle requests.
//            /// </summary>
//            /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
//            public void Init(HttpApplication context)
//            {
//                this.lazyKernel().Inject(context);
//            }
//        }
//    }
//}