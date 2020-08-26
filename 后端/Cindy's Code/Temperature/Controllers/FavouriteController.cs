using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Temperature.Models;

namespace Temperature.Controllers {
    [Route("[controller]/[action]")]
    [ApiController]
    public class FavouriteController : Controller {
        private blogContext entity = new blogContext(); //整体数据库类型

        /// <summary>
        /// 新建收藏夹(禁止同一用户创建同名收藏夹）
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">收藏夹名</param>
        /// <returns></returns>
        /// <response code ="400">该用户已存在同名收藏夹</response>
        [HttpPost]
        public JsonResult createFolderByNickName(string nick_name, string folderName) {
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            //var user = entity.User.Find(id); //在数据库中根据key找到相应记录

            //检查之前是否已经有过同名收藏夹
            var checkFolder =
                (from c in entity.Favourite
                 where (c.FavouriteName == folderName && c.UserId == id)
                 select c.FavouriteId).Distinct();
            var check = checkFolder.FirstOrDefault();
            if (check != default) {
                Response.StatusCode = 400;//该用户已存在同名收藏夹
                return Json(new { UserName = nick_name, Folder_Name = folderName, result = "Already Exists!" });
            }

            var folder = new Favourite();
            folder.FavouriteName = folderName;
            folder.ArticleNum = 0;
            folder.UserId = id;
            entity.Favourite.Add(folder);
            entity.SaveChanges();
            Response.StatusCode = 200;
            return Json(new { user = nick_name, folder = folderName });
        }

        /// <summary>
        /// 向收藏夹中添加文章
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">文件夹名</param>
        /// <param name="articleID">文章ID</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        [HttpPost]
        public JsonResult addArticleByID(string nick_name, string folderName, int articleID) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == folderName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            //FAVOURITE里面更新收藏夹文章数量
            var folder = entity.Favourite.Find(F_id);
            var num = folder.ArticleNum;
            if (num == default)
                folder.ArticleNum = 1;
            else
                folder.ArticleNum = num + 1;
            entity.Entry(folder).State = EntityState.Modified;
            //entity.SaveChanges();

            //ARTICLE表里面更新文章收藏量
            var article = entity.Article.Find(articleID);
            if (article == default) {
                Response.StatusCode = 400;//没有该文章
                return Json(new { ArticleID = articleID, result = "Article NOT FOUND!" });
            }
            var collectNum = article.CollectNum;
            if (collectNum == default)
                article.CollectNum = 1;
            else
                article.CollectNum = collectNum + 1;
            entity.Entry(article).State = EntityState.Modified;
            //entity.SaveChanges();

            //加入FAVOURITE_ARTICLE表
            var item = new FavouriteArticle();
            item.FavouriteId = F_id;
            item.ArticleId = articleID;
            item.FavouriteTime = DateTime.Now;
            entity.FavouriteArticle.Add(item);
            entity.SaveChanges();

            Response.StatusCode = 200;//成功
            return Json(new { folderID = F_id, articleId = articleID });
        }

        /// <summary>
        /// 获取收藏夹总览信息
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">收藏夹名</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        /// <response code="405">没找到该用户</response>
        [HttpPost]
        public JsonResult getFolderInfoByName(string nick_name, string folderName) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            //根据用户ID和收藏夹名找到对应收藏夹项
            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == folderName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            var info = entity.Favourite.Find(F_id);
            Response.StatusCode = 200;//成功
            return Json(info);
        }

        /// <summary>
        /// 删除收藏夹
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">收藏夹名</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        /// <response code="405">没找到该用户</response>
        [HttpPost]
        public JsonResult deleteFolderByName(string nick_name, string folderName) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            //根据用户ID和收藏夹名找到对应收藏夹项
            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == folderName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            //ARTICLE表中找到对应记录,并更新记录
            var articles =
                (from c in entity.FavouriteArticle
                 from u in entity.Article
                 where c.FavouriteId == F_id && c.ArticleId == u.ArticleId
                 select u).ToList(); //这里需要把查询的内容转化为list，不然后面的foreach会报重复连接数据库的错误
            foreach (var article in articles) {
                article.CollectNum--;
                entity.Entry(article).State = EntityState.Modified;
                entity.SaveChanges();
            }

            //获得FAVORITE_ARTICLE表中记录
            var F_A_record =
                (from c in entity.FavouriteArticle
                 where c.FavouriteId == F_id
                 select c).ToList();
            foreach (var record in F_A_record) {
                var articleID = record.ArticleId;
                //var article = entity.Article.Find(articleID);

                //FAVORITE_ARTICLE表中删除项
                entity.Entry(record).State = EntityState.Deleted;
                entity.SaveChanges();
            }

            //FAVOURITE表中删除整个文件夹
            var info = entity.Favourite.Find(F_id);
            entity.Entry(info).State = EntityState.Deleted;
            entity.SaveChanges();

            Response.StatusCode = 200;
            return Json(new { UserName = nick_name, FolderName = folderName, result = "success!" });
        }

        /// <summary>
        /// 更新收藏夹名字
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="oldName">旧收藏夹名</param>
        /// <param name="newName">新收藏夹名</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        /// <response code="405">没找到该用户</response>
        [HttpPost]
        public JsonResult updateFolderNameByName(string nick_name, string oldName, string newName) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            //根据用户ID和收藏夹名找到对应收藏夹项
            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == oldName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            var info = entity.Favourite.Find(F_id);
            info.FavouriteName = newName;
            entity.Entry(info).State = EntityState.Modified;
            entity.SaveChanges();

            Response.StatusCode = 200;//成功
            return Json(new { UserName = nick_name, OldName = oldName, NewName = newName });
        }

        /// <summary>
        /// 删除收藏夹中的文章
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">收藏夹名</param>
        /// <param name="articleID">文章ID</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        /// <response code="405">没找到该用户</response>
        /// <response code="400">收藏夹内没有文章</response>
        [HttpPost]
        public JsonResult deleteArticleByID(string nick_name, string folderName, int articleID) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            //根据用户ID和收藏夹名找到对应收藏夹项
            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == folderName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            var folder = entity.Favourite.Find(F_id);

            var item = entity.FavouriteArticle.Find(F_id, articleID);
            if (item == default) {
                Response.StatusCode = 406;//收藏夹里没有该文章
                return Json(new { folderID = F_id, ArticleID = articleID, result = "NOT FOUND" });
            }

            //FAVORITE表中文章数量修改
            var num = folder.ArticleNum;
            if (num == default) {
                Response.StatusCode = 400;//收藏夹内没有文章
                return Json(new { result = "EMPTY FOLDER" });
            }
            folder.ArticleNum = num - 1;
            entity.Entry(folder).State = EntityState.Modified;
            //entity.SaveChanges();

            //ARTICLE中收藏数更新
            var article = entity.Article.Find(articleID);
            article.CollectNum--;
            entity.Entry(article).State = EntityState.Modified;
            //entity.SaveChanges();

            //FAVORITE_ARTICLE表删除
            //var item = entity.FavouriteArticle.Find(F_id, articleID);

            entity.Entry(item).State = EntityState.Deleted;
            entity.SaveChanges();

            Response.StatusCode = 200;
            return Json(new { result = "successful deleted" });
        }

        /// <summary>
        /// 查看收藏夹内收藏文章(ID)
        /// </summary>
        /// <param name="nick_name">用户名</param>
        /// <param name="folderName">收藏夹名</param>
        /// <returns></returns>
        /// <response code="404">没找到该文件夹</response>
        /// <response code="405">没找到该用户</response>
        [HttpPost]
        public JsonResult getArticleListByName(string nick_name, string folderName) {
            //根据用户名找到用户ID
            var userid =
                    (from c in entity.User
                     where c.NickName == nick_name
                     select c.UserId).Distinct();
            var id = userid.FirstOrDefault();
            if (id == default) {
                Response.StatusCode = 405;//没找到该用户
                return Json(new { UserID = id, result = "NOT FOUND" });
            }

            //根据用户ID和收藏夹名找到对应收藏夹ID
            var folderid =
                    (from c in entity.Favourite
                     where (c.FavouriteName == folderName && c.UserId == id)
                     select c.FavouriteId).Distinct();
            var F_id = folderid.FirstOrDefault();
            if (F_id == default) {
                Response.StatusCode = 404;//没找到该文件夹
                return Json(new { folderID = F_id, result = "NOT FOUND" });
            }

            var item =
                (from u in entity.FavouriteArticle
                 where u.FavouriteId == F_id
                 select u).Distinct();

            Response.StatusCode = 200;//成功
            return Json(item);

        }





    }
}
