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
#region ע��
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailInfo").Get<EmailInfoConst>());
builder.Services.AddSqlsugarSetup(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
#region ���swaggerע��
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api"
    });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Net6Api.xml");
    c.IncludeXmlComments(xmlPath, true);
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme//����swagger��ͷ
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
#region ���У��
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
//���ȫ����Ȩ
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
#endregion
//ע��ȫ����־
//builder.Services.Configure<MvcOptions>(options =>
//{
//    options.Filters.Add(new LoggingMonitorAttribute());


//});
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new LoggingMonitorAttribute());
});




//��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});
// ������־����
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
app.UseSwagger(); // ����Swagger�ĵ����Զ�չ������  
app.UseSwaggerUI(options => options.DocExpansion(DocExpansion.None));
//}

//ʹ�ÿ������
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
