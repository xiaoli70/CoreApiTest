using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Net6Api.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace Net6Api.Filter
{
    /// <summary>
    /// null
    /// </summary>
    public class MSFromAttribute: SampleAsyncActionFilter
    {
     

        public override async Task OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is EmptyResult)
                context.Result = JsonView();
            else if (context.Result is ObjectResult res)
            {
                if (!(res.Value is JsonView))
                    context.Result = JsonView(res.Value);
            }
            await Task.CompletedTask;
        }
    }
}
