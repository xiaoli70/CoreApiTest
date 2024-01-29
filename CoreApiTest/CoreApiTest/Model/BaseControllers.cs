﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiTest.Model
{
    /// <summary>
    /// 基类
    /// </summary>
    //[Authorize]
    [ApiController]
    public class BaseControllers: ControllerBase
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
