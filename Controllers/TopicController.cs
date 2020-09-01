using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Temperature.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Temperature.Controllers {
    [Route("[controller]/[action]")]
    [ApiController]
    public class TopicController : Controller {
        blogContext entity = new blogContext();

        /// <summary>
        /// 创建话题回复
        /// </summary>
        /// <param name="content"></param>
        /// <param name="topicID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createTopicAnswerByID(string content, string topicID, string userID) {
            DateTime dateTime = DateTime.Now; //获取当前时间
            Topic topic = new Topic {
                TopicId = int.Parse(topicID),
                TopicContent = content,
                AnswerNum = 0,
                UserId = int.Parse(userID),
                TopicUploadTime = dateTime,
            };
            entity.Add(topic);
            int nums = entity.SaveChanges();



            return Json(new { a = 1 });
        }

        /// <summary>
        /// 创建话题
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="userID"></param>
        /// <param name="zoneID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法创建</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createTopicByID(string content, string title, string userID, string zoneID) {
            DateTime dateTime = DateTime.Now; //获取当前时间
            Topic topic = new Topic(); //新建topic条目项
            int createTopicFlag = 0;

            //向条目中插入数据
            topic.TopicContent = content;
            topic.TopicTitle = title;
            topic.AnswerNum = 0;
            topic.UserId = int.Parse(userID);
            topic.TopicUploadTime = dateTime;
            topic.ZoneId = int.Parse(zoneID);

            entity.Add(topic);
            try {
                createTopicFlag = entity.SaveChanges(); //存入数据库 返回值为1表示成功
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            if (createTopicFlag == 1) Response.StatusCode = 200;
            else Response.StatusCode = 403; //无法创建

            return Json(new { createTopicFlag = createTopicFlag });
        }


        /// <summary>
        /// 根据ID删除话题评论
        /// </summary>
        /// <param name="answerID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteTopicAnswerByID(string answerID) {
            int deleteTopicAnswerFlag = 0;
            Dictionary<string, string> returnJson = new Dictionary<string, string>();

            try {
                TopicAnswerReply topicAnswerReply = entity.TopicAnswerReply.Single(c => c.TopicAnswerId == int.Parse(answerID)); //查找
                entity.TopicAnswerReply.Remove(topicAnswerReply); //建立删除语句
                entity.SaveChanges(); //提交删除
                deleteTopicAnswerFlag = 1; //成功
                Response.StatusCode = 200;
                returnJson.Add("deleteTopicAnswerID", topicAnswerReply.TopicAnswerId.ToString());
            } catch(Exception e) {
                deleteTopicAnswerFlag = 0; //失败
                Response.StatusCode = 403;
            } finally {
                returnJson.Add("deleteTopicAnswerFlag", deleteTopicAnswerFlag.ToString());

            }
            return Json(returnJson);
        }

        /// <summary>
        /// 根据用户ID删除相应用户
        /// </summary>
        /// <param name="topicID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deleteTopicByID(string topicID) {
            int deleteTopicFlag = 0;
            Dictionary<string, string> returnJson = new Dictionary<string, string>();

            try {
                Topic topic = entity.Topic.Single(c => c.TopicId == int.Parse(topicID));
                entity.Topic.Remove(topic);
                entity.SaveChanges();
                Response.StatusCode = 200;
                returnJson.Add("deleteTopic", topic.TopicId.ToString());
            } catch(Exception e) {
                deleteTopicFlag = 0; //失败
                Response.StatusCode = 403;
            } finally {
                returnJson.Add("deleteTopicAnswerFlag", deleteTopicFlag.ToString());
            }
            return Json(returnJson);
        }

        [HttpPost]
        public void getBriefAnnouncementByID(int pageNum, int pageSize) {

        }

        /// <summary>
        /// 分页获取topic
        /// </summary>
        /// <param name="pageNum">页号</param>
        /// <param name="pageSize">每页大小</param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getTopicByPage(int pageNum, int pageSize) {
            int getTopicFlag = 0;
            Dictionary<string, string> returnJson = new Dictionary<string, string>();
            returnJson.Add("Result", "");

            try {
                var content = entity.Topic.Skip((pageNum - 1) * pageSize).Take(pageSize);
                string contentJson = JsonConvert.SerializeObject(content); //序列化对象
                returnJson["Result"] = contentJson;
                Response.StatusCode = 200;
            } catch(Exception e) {
                getTopicFlag = 0;
                Response.StatusCode = 403;
            } finally {
                returnJson.Add("getTopicFlag", getTopicFlag.ToString());
            }
            return Json(returnJson);
        }

        /// <summary>
        /// 获取topicID话题下的评论
        /// </summary>
        /// <param name="topicID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getTopicCommentByID(string topicID) {
            int getTopicCommentFlag = 0;
            Dictionary<string, string> returnJson = new Dictionary<string, string>();
            returnJson.Add("Result", "");

            try {
                var content = (from c in entity.TopicAnswerReply
                               where c.TopicId == int.Parse(topicID)
                               select c);
                string contentJson = JsonConvert.SerializeObject(content);
                returnJson["Result"] = contentJson;
                Response.StatusCode = 200;
            } catch(Exception e) {
                getTopicCommentFlag = 0;
                Response.StatusCode = 403;
            } finally {
                returnJson.Add("getTopicCommentFlag", getTopicCommentFlag.ToString());
            }
            return Json(returnJson);
        }
    }
}
