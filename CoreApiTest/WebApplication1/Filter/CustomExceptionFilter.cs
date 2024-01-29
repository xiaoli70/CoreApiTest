using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net6Api.Domain;

namespace Net6Api.Filter
{
   
    /// <summary>
    /// 异常拦截
    /// 
    /// </summary>
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            // 获取异常信息  
            var exception = context.Exception;
            var httpContext = context.HttpContext;

            // 记录异常信息（可选）  
            // ...  

            // 返回自定义的错误响应  
            var errorResponse = new JsonView { Code = StatusCodes.Status500InternalServerError, Msg = "操作失败",Data=exception.Message };
            //    new ErrorResponse
            //{
            //    Message = exception.Message,
            //    StackTrace = exception.StackTrace,
            //    // 其他错误信息...  
            //};
            context.Result = new JsonResult(errorResponse);
        }
    }
}
