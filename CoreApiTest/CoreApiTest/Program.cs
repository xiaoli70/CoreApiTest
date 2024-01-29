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

#region ע��
//ע��ʵ��
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //��ע��JWT
    //builder.RegisterType<User>().As<IUser>();//�����������ӿں���
    // ע��Service����
    Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name);//��������������
    builder.RegisterAssemblyTypes(assembly)
    .AsImplementedInterfaces()
    .InstancePerDependency();
});
#endregion

#region ��ʼ����־
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

//app.UseAuthentication();//��ǰ ��Ȩ
app.UseCors("CorsPolicy");//����
//app.UseAuthorization();

//app.MapControllers();


app.Run();
