using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Temperature.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

/** 备注：
 * 这里用户登录只需要输入NickName,然后自动匹配数据库中该用户的id，然后验证密码正确性
 * 用户不需要知道自己的id
 */


namespace Temperature.Controllers {
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller {
        private blogContext entity = new blogContext(); //整体数据库类型
        //private static int idNum = 0;//id自增
        //private static int idForAnnouncement = 0;//公告id自增
        private IWebHostEnvironment My_Environment;
        public AccountController(IWebHostEnvironment _environment) {
            My_Environment = _environment;
        }



        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        /// <response code="200">成功</response>
        /// <response code="404">用户名不存在</response>
        /// <response code="403">密码错误</response>
        [HttpPost]
        public ActionResult Login(string nick_name, string password) {
            User user = new User();
            int flag = 0;
            //JsonData jsondata = new JsonData();  //json格式的数据
            var userid =    //EF中的Linq语法
                        (from u in entity.User
                         where u.NickName == nick_name
                         select u.UserId).Distinct(); //与sql中的select distinct类同
            var id = userid.FirstOrDefault();
            if (userid.FirstOrDefault() != default) {
                //var id = userid.FirstOrDefault();
                user = entity.User.Find(id);  //根据主键id找
                if (user.Password != password) {
                    flag = 2;//密码错误
                }
                else {
                    //jsondata["userID"] = id;
                    flag = 0;//成功
                }
            }
            else {
                flag = 1;//用户名不存在
            }
            var data = new {
                userid = id,
                loginFlag = flag
            };

            //jsondata["LoginFlag"] = flag.ToString();
            //var data = Json(jsondata.ToJson());
            //return Ok(data);
            //return Ok(Json(jsondata.ToJson()));
            if (flag == 0) {
                Response.StatusCode = 200;//成功
            }
            else if (flag == 1) {
                Response.StatusCode = 404;//用户名不存在
            }
            else {
                Response.StatusCode = 403;//密码错误
            }
            //return Ok(data);
            return Json(data);

        }


        /*
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>"RegisterFlag"用户名已经被注册为0，注册成功为1</returns>
        //[Route("Register")]
        [HttpPost]
        public ActionResult Register(string nick_name, string password)
        {
            User user = new User();
            //JsonData jsondata = new JsonData();  //json格式的数据
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            if (userid.FirstOrDefault() != null)  //这里是null吗？id不可以为null 所以即使没有也bushinull
            {
                flag = 0; //用户名名已经被占用
                //?userid 本来就不可以为空啊？ 那这个地方是表示的？
            }
            else
            {
                user.UserId = (++idNum).ToString();
                user.NickName = nick_name;
                user.Password = password;
                entity.User.Add(user); //把user这个实体加入数据库
                entity.SaveChanges();
                flag = 1; //注册成功
            }
            Dictionary<string, string> jsondata = new Dictionary<string, string>();
            jsondata.Add("RegisterFlag", flag.ToString());
            return Json(JsonConvert.SerializeObject(jsondata, Formatting.Indented));

        }*/
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="nick_name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <response code="200">注册成功</response>
        /// <response code="402">用户名被占用</response>
        [HttpPost]
        public ActionResult register(string nick_name, string password) {
            User user = new User();
            //JsonData jsondata = new JsonData();  //json格式的数据
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            if (userid.FirstOrDefault() != default) {
                flag = 0; //用户名名已经被占用

            }
            else {
                //user.UserId = (++idNum).ToString();
                //这里id是自增类型
                user.NickName = nick_name;
                user.Password = password;
                entity.User.Add(user); //把user这个实体加入数据库
                entity.SaveChanges();
                flag = 1; //注册成功
            }
            //Dictionary<string, string> jsondata = new Dictionary<string, string>();
            //jsondata.Add("RegisterFlag", flag.ToString());
            var data = new {
                RegisterFlag = flag
            };
            switch (flag) {
                case 0: //用户名被占用
                    Response.StatusCode = 402;
                    break;
                case 1: //注册成功
                    Response.StatusCode = 200;
                    break;
            }
            return Json(data);
            //return Json(JsonConvert.SerializeObject(jsondata, Formatting.Indented));

        }


        /// <summary>
        /// 维护个人信息（不含头像维护）
        /// </summary>
        /// <param name="nick_name"></param>
        /// <param name="gender"></param>
        /// <param name="location"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="wechat"></param>
        /// <returns></returns>
        /// <response code="404">没有找到该用户</response>
        /// <response code ="200">成功修改用户信息</response>
        [HttpPost]
        public JsonResult personalInfo(string nick_name, string gender, string location, string year, string month, string day, string email, string tel, string wechat) {
            //User user = new User();
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            var user = entity.User.Find(id); //在数据库中根据key找到相应记录

            if (id == default) {
                flag = 0; //没有找到该用户
            }
            else {
                //user.UserId = id;
                user.Gender = gender;
                user.Location = location;
                //DateTime dateOfBirth = new DateTime();
                //string.Format("yyyy年MM月dd日", dateOfBirth);
                //user.Dob = dateOfBirth;

                var yyyy = year;
                var mm = month;
                var dd = day;
                DateTime dateTime = new DateTime();
                DateTime.TryParse(yyyy + "-" + mm + "-" + dd, out dateTime);
                user.Dob = dateTime;

                user.Email = email;
                user.Tel = tel;
                user.Wechat = wechat;
                //entity.User.Add(user); //把user这个实体加入数据库
                entity.Entry(user).State = EntityState.Modified;
                entity.SaveChanges();
                flag = 1; //信息存储完成
            }
            var data = new {
                Infoflag = flag
            };
            if (flag == 0) {
                Response.StatusCode = 404;//没有找到该用户
                return Json(data);
            }
            else {
                Response.StatusCode = 200; //成功修改信息
                return Json(data);
            }
        }

        /*public JsonResult UpdateAvatr(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    //string filePath = Microsoft.AspNetCore.Http.HttpContext.Current.Server.MapPath("~/Uploads/");
                    string filePath = My_Environment.ContentRootPath;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }*/

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="collection">[FromForm]头像文件</param>
        /// <param name="nick_name">昵称</param>
        /// <returns></returns>
        /// <response code="404">没有找到该用户</response>
        /// <response code ="200">成功修改用户头像</response>
        [HttpPost]
        public JsonResult updateAvatr([FromForm] IFormCollection collection, string nick_name) {
            //var files = Request.Form.Files;
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                flag = 0; //没有找到该用户
                Response.StatusCode = 202;//没有该用户
                return Json(new { code = 404, UploadFlag = flag });
            }
            var user = entity.User.Find(id); //在数据库中根据key找到相应记录



            FormFileCollection files = (FormFileCollection)collection.Files;
            IFormFile file = files.FirstOrDefault();

            string filename = file.FileName;//--"360截图20191119113847612.jpg"
            string fileExtention = System.IO.Path.GetExtension(file.FileName);//--.jpg
            string path = Guid.NewGuid().ToString() + fileExtention;
            string basepath = My_Environment.ContentRootPath;//en.WebRootPath-》wwwroot的目录; .ContentRootPath到达WebApplication的项目目录
            string savePath = basepath + "\\user's_avatr\\" + path;
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }
            //  using (FileStream fstream = System.IO.File.Create(newFile)) 也可以
            using (FileStream fstream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                file.CopyTo(fstream); // 复制文件
                fstream.Flush();//清空缓存区
                user.Avatr = savePath;
                entity.Entry(user).State = EntityState.Modified;
                entity.SaveChanges();
                flag = 1;
                Response.StatusCode = 201;//成功

            }
            return Json(new { code = 200, UploadFlag = flag });
        }

        /// <summary>
        /// 返回用户头像（存储的地址）
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getAvatrResource(string nick_name) {
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            var user = entity.User.Find(id); //在数据库中根据key找到相应记录
            return Json(user.Avatr);
        }




        /// <summary>
        /// 查询用户所有基本信息
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <returns></returns>
        /// <response code = "200">成功</response>
        /// <response code ="404">没有找到该用户</response>
        [HttpPost]
        public JsonResult getUserInfoByNickName(string nick_name) {
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                flag = 0; //没有找到该用户
                Response.StatusCode = 404;//没有该用户
                return Json(new { code = 202, UploadFlag = flag });
            }
            var user = entity.User.Find(id); //在数据库中根据key找到相应记录
            flag = 1;//找到该用户
            Response.StatusCode = 200;//成功
            return Json(user);
        }







        /// <summary>
        /// 添加/更新 公告内容
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="content">公告内容</param>
        /// <returns></returns>
        /// <response code = "404">没有找到该用户</response>
        /// <response code ="200">成功</response>
        [HttpPost]
        public JsonResult createAnnouncementByNickName(string nick_name, string content) {
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();

            if (id == default) {
                flag = 0; //没有找到该用户
                Response.StatusCode = 404;//没有找到该用户
            }
            else {
                var announcementRecordID =
                (from c in entity.Announcement
                 where c.UserId == id
                 select c.AnnouncementId).Distinct(); //根据userid找到记录(userid不是主码，不能直接find)
                var announcement = entity.Announcement.Find(announcementRecordID.FirstOrDefault()); //在数据库中根据key找到相应记录

                if (announcement == null) {
                    flag = 1; //该用户没有添加过公告，新建公告
                    var announcement2 = new Announcement();
                    announcement2.UserId = id;
                    //announcement2.AnnouncementId = (++idForAnnouncement).ToString();
                    //这里id也会自增
                    announcement2.AnnouncementContent = content;
                    announcement2.AnnouncementUploadTime = DateTime.Now;
                    entity.Announcement.Add(announcement2); //把announcement这个实例加入数据库
                    entity.SaveChanges();

                    Response.StatusCode = 200;//成功新建
                }
                else {
                    //该用户已经创建过公告，需要rewrite
                    flag = 2;
                    //announcement.UserId = id;
                    //announcement.AnnouncementId = (++idForAnnouncement).ToString(); 不修改公告id
                    announcement.AnnouncementContent = content;
                    announcement.AnnouncementUploadTime = DateTime.Now;

                    entity.Entry(announcement).State = EntityState.Modified;
                    entity.SaveChanges();

                    Response.StatusCode = 200;//成功重写公告
                }


            }
            return Json(new { Userid = id, CreateFlag = flag });
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <returns></returns>
        /// <response code = "404">没有找到该用户</response>
        /// <response code ="403">该用户没有创建过公告</response>
        /// <response code ="200">成功删除公告</response>
        [HttpPost]
        public JsonResult deleteAnnouncementByNickName(string nick_name) {
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                flag = 0; //没有找到该用户
                Response.StatusCode = 404;//没有找到该用户
                return Json(new { DeleteFlag = flag });
            }
            var announcementRecordID =
                (from c in entity.Announcement
                 where c.UserId == id
                 select c.AnnouncementId).Distinct(); //根据userid找到记录(userid不是主码，不能直接find)
            var announcement = entity.Announcement.Find(announcementRecordID.FirstOrDefault()); //在数据库中根据key找到相应记录
            if (announcement == null) {
                flag = 1;//该用户没有创建过公告
                Response.StatusCode = 403;//该用户没有创建过公告
                return Json(new { DeleteFlag = flag });
            }


            /*announcement.UserId = null;
            announcement.AnnouncementUploadTime = null;
            announcement.AnnouncementContent = null;*/

            entity.Entry(announcement).State = EntityState.Deleted;//删除该项
            entity.SaveChanges();

            flag = 2;
            Response.StatusCode = 200;//成功删除公告
            return Json(new { DeleteFlag = flag });
        }

        /// <summary>
        /// 获取用户公告内容
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <returns></returns>
        /// <response code ="403">该没有创建的公告</response>
        /// <response code ="404">没有找到该用户</response>
        /// <response code ="200">成功返回</response>
        [HttpPost]
        public JsonResult getAnnouncementByNickName(string nick_name) {
            int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                flag = 0; //没有找到该用户
                Response.StatusCode = 404;//没有找到该用户
                //return Json(new { GetFlag = flag });
                return null;
            }
            var announcementRecordID =
                (from c in entity.Announcement
                 where c.UserId == id
                 select c.AnnouncementId).Distinct(); //根据userid找到记录(userid不是主码，不能直接find)
            if (announcementRecordID == null) {
                flag = 1;//该用户没有创建的公告，无法返回
                Response.StatusCode = 403;

                return null;
                //return Json(new { GetFlag = flag });
            }
            var announcement = entity.Announcement.Find(announcementRecordID.FirstOrDefault());
            Response.StatusCode = 200;//成功返回
            return Json(announcement);
        }


        /// <summary>
        /// 创建关注关系
        /// </summary>
        /// <param name="nameOfBlogger">被关注者</param>
        /// <param name="nameOfFans">关注者</param>
        /// <returns></returns>
        /// <response code = "200">成功</response>
        /// <response code ="404">用户不存在</response>
        [HttpPost]
        public JsonResult createFollowByNickNames(string nameOfBlogger, string nameOfFans) {
            int flag = 0;
            var BloggerId =
                    (from c in entity.User
                     where c.NickName == nameOfBlogger
                     select c.UserId).Distinct();
            var id_1 = BloggerId.FirstOrDefault();
            var FansId =
                    (from c in entity.User
                     where c.NickName == nameOfFans
                     select c.UserId).Distinct();
            var id_2 = FansId.FirstOrDefault();
            if (id_1 == default || id_2 == default) {
                if (id_1 == default && id_2 == default)
                    flag = 1; //两个用户都不存在
                else if (id_1 == default)
                    flag = 2; //博主用户不存在
                else
                    flag = 3; //粉丝用户不存在

                Response.StatusCode = 404;//用户不存在
                return Json(new { GetFlag = flag });
            }

            var newFollow = new UserFollow { ActiveUserId = id_2, PassiveUserId = id_1 };
            entity.UserFollow.Add(newFollow);
            entity.SaveChanges();

            var user = entity.User.Find(id_1);  //user里面更新被关注数量
            var num = user.FollowNum;
            if (num == default)
                user.FollowNum = 1;
            else
                user.FollowNum = num + 1;
            entity.Entry(user).State = EntityState.Modified;
            entity.SaveChanges();


            Response.StatusCode = 200;//关注成功
            flag = 4;//成功
            return Json(new { CreateFlag = flag });
        }

        /// <summary>
        /// 删除关注关系
        /// </summary>
        /// <param name="nameOfBlogger">被关注者</param>
        /// <param name="nameOfFans">关注者</param>
        /// <returns></returns>
        /// <response code ="404">用户不存在</response>
        /// <response code ="200">成功</response>
        [HttpPost]
        public JsonResult deleteFollowByNickName(string nameOfBlogger, string nameOfFans) {
            int flag = 0;
            var BloggerId =
                    (from c in entity.User
                     where c.NickName == nameOfBlogger
                     select c.UserId).Distinct();
            var id_1 = BloggerId.FirstOrDefault();
            var FansId =
                    (from c in entity.User
                     where c.NickName == nameOfFans
                     select c.UserId).Distinct();
            var id_2 = FansId.FirstOrDefault();
            if (id_1 == default || id_2 == default) {
                if (id_1 == default && id_2 == default)
                    flag = 1; //两个用户都不存在
                else if (id_1 == default)
                    flag = 2; //博主用户不存在
                else
                    flag = 3; //粉丝用户不存在

                Response.StatusCode = 404;//用户不存在
                return Json(new { GetFlag = flag });
            }

            var follow = entity.UserFollow.Find(id_2, id_1);
            entity.Entry(follow).State = EntityState.Deleted;//删除该项
            entity.SaveChanges();
            Response.StatusCode = 200;//成功
            flag = 4; //成功
            return Json(new { GetFlag = flag });

        }

        /// <summary>
        /// 返回粉丝列表
        /// </summary>
        /// <param name="nick_name">博主用户名</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getFansListByNickName(string nick_name) {
            //int flag = 0;
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            //var user = entity.User.Find(id); //在数据库中根据key找到相应记录

            var fansID =
                (from u in entity.UserFollow
                 where u.PassiveUserId == id
                 select u.ActiveUserId).Distinct();
            Response.StatusCode = 200;
            return Json(fansID);
        }

        /// <summary>
        /// 返回关注的博主列表
        /// </summary>
        /// <param name="nick_name">博主用户名</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getFollowListByNickName(string nick_name) {
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            //var user = entity.User.Find(id); //在数据库中根据key找到相应记录

            var followID =
                (from u in entity.UserFollow
                 where u.ActiveUserId == id
                 select u.PassiveUserId).Distinct();
            Response.StatusCode = 200; //成功
            return Json(followID);
        }



    }



}


