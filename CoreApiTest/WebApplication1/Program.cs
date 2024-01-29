using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net6Api.Controllers;
using Net6Api.Domain;
using Net6Api.Domain.Helper;
using Net6Api.Filter;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

//builder.Services.AddHostedService<AutoServices>();
#region 注入
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailInfo").Get<EmailInfoConst>());
builder.Services.AddSqlsugarSetup(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
#region 添加swagger注释
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api"
    });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Net6Api.xml");
    c.IncludeXmlComments(xmlPath, true);
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme//增加swagger报头
    {
        Description = "Value: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          },Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,
        },new List<string>()
      }
    });
});
#endregion
#region 添加校验
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "net6api",
        ValidIssuer = "net6api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
    };
    
});
//添加全局授权
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
#endregion
//注册全局日志
//builder.Services.Configure<MvcOptions>(options =>
//{
//    options.Filters.Add(new LoggingMonitorAttribute());


//});
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new LoggingMonitorAttribute());
});




//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});
// 设置日志过滤
builder.Logging.AddFilter((provider, category, logLevel) =>
{

    return !new[] { "Microsoft.Hosting.Lifetime", "Microsoft.AspNetCore" }.Any(u => category.StartsWith(u)) && logLevel >= LogLevel.Information;
});
builder.Logging.AddLog4Net("Log4net.config");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ 
app.UseSwagger(); // 禁用Swagger文档的自动展开功能  
app.UseSwaggerUI(options => options.DocExpansion(DocExpansion.None));
//}

//使用跨域策略
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
