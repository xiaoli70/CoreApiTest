using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6Api.Domain;
using Net6Api.Filter;

namespace Net6Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary>
    [MSFrom]
    [ApiController]
    public class BaseControllers : ControllerBase
    {

        protected virtual JsonView JsonView(object obj)
        {
            return new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj };
        }
        protected virtual JsonView JsonView(object obj, int count)
        {
            return new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj, Count = count };
        }
        protected virtual JsonView JsonView(string msg)
        {
            return new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg };
        }
        protected virtual JsonView JsonView(bool s)
        {
            if (s)
            {
                return new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功" };
            }
            else
            {
                return new JsonView { Code = StatusCodes.Status400BadRequest, Msg = "操作失败" };
            }
        }
        protected virtual JsonView JsonView(bool s, string msg)
        {
            if (s)
            {
                return new JsonView { Code = StatusCodes.Status200OK, Msg = msg };
            }
            else
            {
                return new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg };
            }
        }

    }
}
