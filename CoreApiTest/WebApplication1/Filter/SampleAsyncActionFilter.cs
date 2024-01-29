using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net6Api.Domain;
using Net6Api.Util;

namespace Net6Api.Filter
{
    public class SampleAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        
        public async virtual Task OnActionExecuting(ActionExecutingContext context)
        {
            await Task.CompletedTask;
        }

        public async virtual Task OnActionExecuted(ActionExecutedContext context)
        {
            await Task.CompletedTask;
        }


        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            await OnActionExecuting(context);

            var nextContext=await next();
            //_ = await next.Invoke();
            //return;
            // Do something after the action executes.
            await OnActionExecuted(nextContext);
        }

        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public ContentResult JsonContent(string json)
        {
            return new ContentResult { Content = json, StatusCode = 200, ContentType = "application/json; charset=utf-8" };
        }



        protected virtual ContentResult JsonView()
        {
             return JsonContent(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功" }.ToJson());
        } protected virtual ContentResult JsonView(object obj)
        {
            return JsonContent(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj }.ToJson());
        }
        protected virtual ContentResult JsonView(object obj, int count)
        {
            return JsonContent(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj, Count = count }.ToJson());
        }
        protected virtual ContentResult JsonView(string msg)
        {
            return JsonContent(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg }.ToJson());
        }
        protected virtual ContentResult JsonView(bool s)
        {
            if (s)
            {
                return JsonContent(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功" }.ToJson());
            }
            else
            {
                return JsonContent(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = "操作失败" }.ToJson());
            }
        }
        protected virtual ContentResult JsonView(bool s, string msg)
        {
            if (s)
            {
                return JsonContent(new JsonView { Code = StatusCodes.Status200OK, Msg = msg }.ToJson());
            }
            else
            {
                return JsonContent(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg }.ToJson());
            }
        }


    }
}
