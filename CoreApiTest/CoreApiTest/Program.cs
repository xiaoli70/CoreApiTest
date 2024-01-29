using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreApiTest;
using CoreApiTest.IClass;
using CoreApiTest.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

#region 注入
//注入实现
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //先注入JWT
    //builder.RegisterType<User>().As<IUser>();//可以是其他接口和类
    // 注入Service程序集
    Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name);//可以是其他程序集
    builder.RegisterAssemblyTypes(assembly)
    .AsImplementedInterfaces()
    .InstancePerDependency();
});
#endregion

#region 初始化日志
Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Error()
       .WriteTo.File(Path.Combine("Logs", @"Log.txt"), rollingInterval: RollingInterval.Day)
       .CreateLogger();
#endregion


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

//app.UseAuthentication();//在前 鉴权
app.UseCors("CorsPolicy");//跨域
//app.UseAuthorization();

//app.MapControllers();


app.Run();
