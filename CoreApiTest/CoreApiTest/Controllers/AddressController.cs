using CoreApiTest.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoreApiTest.Controllers
{
    /// <summary>
    /// 地址相关接口
    /// </summary>
    [Route("address")]
    public class AddressController : BaseControllers
    {
        

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAsync(int page = 1, int size = 15)
        {
           
            return Ok(JsonView("1", 5));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(AddressDto dto)
        {
            try
            {
                string str = "aa";
                int a=Convert.ToInt32(str);
                return Ok(JsonView(false));
            }
            catch (Exception e)
            {
                
                Log.Error("添加异常：" + e.Message);
                return Ok(JsonView("添加异常"));
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditAsync(string Id, AddressDto dto)
        {
            try
            {
                //开启事务
                
                if (true) return Ok(JsonView(true));
                return Ok(JsonView(false));
            }
            catch (Exception e)
            {

                Log.Error("修改异常：" + e.Message);
                return Ok(JsonView("修改异常"));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            try
            {
                //开启事务
                
                if (true) return Ok(JsonView(true));
                return Ok(JsonView(false));
            }
            catch (Exception e)
            {

                Log.Error("删除异常：" + e.Message);
                return Ok(JsonView("删除异常"));
            }
        }

    }
}
