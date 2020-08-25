using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Temperature.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        /// <summary>
        /// 获取所有学生
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Tom";
        }

        /// <summary>
        /// 新增学生
        /// </summary>
        [HttpPost]
        public void Post()
        {

        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        [HttpPut]
        public void Put()
        {

        }

        /// <summary>
        /// 删除学生信息
        /// </summary>
        [HttpDelete]
        public void Delete()
        {

        }
    }
}
