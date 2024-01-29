using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;

namespace Net6Api.Util.Logger
{
    public static class UnifyContext
    {
        /// <summary>
        /// 是否启用规范化结果
        /// </summary>
        internal static bool EnabledUnifyHandler = false;

    
        /// <summary>
        /// 检查是否是有效的结果（可进行规范化的结果）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static bool CheckVaildResult(IActionResult result, out object data)
        {
            data = default;

            // 排除以下结果，跳过规范化处理
            var isDataResult = result switch
            {
                ViewResult => false,
                PartialViewResult => false,
                FileResult => false,
                ChallengeResult => false,
                SignInResult => false,
                SignOutResult => false,
                RedirectToPageResult => false,
                RedirectToRouteResult => false,
                RedirectResult => false,
                RedirectToActionResult => false,
                LocalRedirectResult => false,
                ForbidResult => false,
                ViewComponentResult => false,
                PageResult => false,
                NotFoundResult => false,
                NotFoundObjectResult => false,
                _ => true,
            };

            // 目前支持返回值 ActionResult
            if (isDataResult) data = result switch
            {
                // 处理内容结果
                ContentResult content => content.Content,
                // 处理对象结果
                ObjectResult obj => obj.Value,
                // 处理 JSON 对象
                JsonResult json => json.Value,
                _ => null,
            };

            return isDataResult;
        }

        
    }
}
