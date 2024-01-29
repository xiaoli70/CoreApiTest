using System;
using System.Reflection;
using Autofac;

using Lucene.Net.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace WebApplMinapi
{
    /// <summary>
    /// Autofac容器扩展
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Autofac自动注入Service层应用
        /// </summary>
        /// <param name="builder">容器</param>
        /// <param name="serviceNameEndsWith">实例名统一已指定名字结尾</param>
        public static void AutoRegisterService(this ContainerBuilder builder)
        {
            //自动注入service
            //var assemblysServices = Storage.Assemblys.ToArray();
            //builder.RegisterAssemblyTypes(assemblysServices)
            //    .Where(x => x.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
            //    .AsSelf()//注入自身
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(); // 开启属性注入;

            //builder.RegisterAssemblyTypes(assemblysServices)
            //    .Where(x => x.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase))
            //    .AsSelf()//注入自身
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(); // 开启属性注入;

            ////注入控制器
            //var types = Storage.Assemblys.SelectMany(x => x.GetTypes())
            //    .Where(x => x.IsClass && !x.IsInterface && !x.IsAbstract && !x.IsGenericType);
            //var controller = typeof(ControllerBase);
            //var arrControllerType = types.Where(t => controller.IsAssignableFrom(t) && t != controller).ToArray();
            //builder.RegisterTypes(arrControllerType).PropertiesAutowired();


            // 注册 AppSettings
            builder.Register(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var appSettings = new AppSettings();
                configuration.Bind(appSettings);
                return appSettings;
            }).AsSelf().InstancePerLifetimeScope().PropertiesAutowired();

            // configuration绑定当前环境
            builder.Register(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var environment = new HostingEnvironment();
                configuration?.Bind(environment);
                return environment;
            }).As<IHostEnvironment>().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}
