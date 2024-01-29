using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplMinapi
{
    public static class Order
    {
        public static void OrderEx(this WebApplication app) {

            app.MapPost("/Query", (HttpContext content) =>
            {
                var a = content.Request.Query;

                return new
                {
                    Sussecc = true,
                    Mgs = "成功"
                };
            })
            .WithTags("Order");

            app.MapGet("/Say",  async (ISay say) =>
            {
                int ind = 1;
                await DoAsyncOperation(ind);

                return new
                {
                    Sussecc = true,
                    Mgs = "成功",
                    Data = "nihao"/*await say.saynihao()*/,
                };
            })
             .WithTags("Order"); 
            
            
            app.MapPost("/UpLodeimag",  async ([FromForm] IFormFile image) =>
            {
                if (image == null || image.Length == 0)
                    return "" ;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                return "3";
            })
             .WithTags("Order");

        }
        static async Task DoAsyncOperation(int operationNumber)
        {
            Console.WriteLine($"Executing operation {operationNumber}");
           // Thread.Sleep(10000); // 模拟异步操作的延迟
            await Task.Delay(100000);
        }


    }
}
