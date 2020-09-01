using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Temperature.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Temperature.Controllers {
    [Route("[controller]/[action]")]
    [ApiController]
    public class PhotoController : Controller {
        blogContext entity = new blogContext();
        string albumRootPath = "albums";

        /// <summary>
        /// 新建相簿
        /// </summary>
        /// <param name="albumIntro"></param>
        /// <param name="albumName"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法创建，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createAlbumByID(string albumIntro, string albumName, string userID) {
            DateTime dateTime = DateTime.Now; //创建当前时间
            int createAlbumFlag = 0;
            Album album = new Album();
            album.AlbumIntroduction = albumIntro;
            album.AlbumName = albumName;
            album.UserId = int.Parse(userID);
            album.AlbumTime = dateTime;

            try {
                entity.Album.Add(album);
                entity.SaveChanges();
                entity.Entry(album); //返回插入的记录并注入到album
                createAlbumFlag = 1;
                Response.StatusCode = 200;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
            }
            if(createAlbumFlag == 1) {
                string newPath = Path.Combine(albumRootPath, userID, album.AlbumId.ToString());
                if(!Directory.Exists(newPath)) { //创建相簿文件目录
                    Directory.CreateDirectory(newPath);
                }
            }

            return Json(new { createAlbumFlag = createAlbumFlag, albumID = album.AlbumId.ToString() });
        }

        /// <summary>
        /// 记录相簿访问
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法新建记录，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createAlbumHistoryByID(string albumID, string userID) {
            AlbumVisit albumVisit = new AlbumVisit();
            DateTime dateTime = DateTime.Now;
            int createAlbumHistoryFlag = 0;

            albumVisit.AlbumId = int.Parse(albumID);
            albumVisit.UserId = int.Parse(userID);
            albumVisit.AlbumVisitTime = dateTime;

            try {
                entity.AlbumVisit.Add(albumVisit);
                entity.SaveChanges();
                createAlbumHistoryFlag = 1;
                Response.StatusCode = 200;
            } catch(Exception e) {
                createAlbumHistoryFlag = 0;
                Response.StatusCode = 403;
            }

            return Json(new {createAlbumHistoryFlag = createAlbumHistoryFlag });

        }

        /// <summary>
        /// 上传图片到指定相册 支持多文件上传
        /// </summary>
        /// <param name="uploadedPhoto"></param>
        /// <param name="albumID"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法新建图片，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createPhotoByID(IFormFileCollection uploadedPhoto, string albumID, string userID) {
            DateTime dateTime = DateTime.Now;
            int createPhotoFlag = 0;
            string uploadsFolder = "";
            string uniqueFileName = "";
            string filePath = "";
            List<string> allFilePath = new List<string>();
            List<string> allPhotoID = new List<string>();

            if (uploadedPhoto == null) return BadRequest();

            for (int i = 0; i < uploadedPhoto.Count; i++) {
                try {
                    uploadsFolder = Path.Combine(albumRootPath, userID, albumID); //计算存储路径
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedPhoto[i].FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Photo photo = new Photo();
                    photo.AlbumId = int.Parse(albumID);
                    photo.PhotoLikes = 0;
                    photo.VisitNum = 0;
                    photo.PhotoAddress = filePath;
                    photo.PhotoUploadTime = dateTime;
                    photo.UserId = int.Parse(userID);

                    entity.Photo.Add(photo); //将图片信息添加到数据库中
                    entity.SaveChanges();
                    entity.Entry(photo); //获取新插入的photo的photoID

                    uploadedPhoto[i].CopyTo(new FileStream(filePath, FileMode.Create)); //存储图片到本地 持久化

                    allFilePath.Add(filePath);
                    allPhotoID.Add(photo.PhotoId.ToString());

                    Response.StatusCode = 200;
                    createPhotoFlag = 1;
                }
                catch (Exception e) {
                    Response.StatusCode = 403;
                    createPhotoFlag = 0;
                    Console.WriteLine(e.Message);
                }
            }

            var resultJson = new {
                uploadPaths = JsonConvert.SerializeObject(allFilePath),
                coorespondingPhotoIDs = JsonConvert.SerializeObject(allPhotoID),
                createPhotoFlag = createPhotoFlag,
            };

            return Json(resultJson);
        }

        /// <summary>
        /// 评论图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="photoID"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法评论图片，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createPhotoCommentByID(string content, string photoID, string userID) {
            DateTime dateTime = DateTime.Now;
            int createPhotoCommentFlag = 0;

            PhotoComment photoComment = new PhotoComment();
            photoComment.PhotoCommentContent = content;
            photoComment.PhotoId = int.Parse(photoID);
            photoComment.UserId = int.Parse(userID);
            photoComment.PhotoCommentTime = dateTime;

            try {
                entity.PhotoComment.Add(photoComment);
                entity.SaveChanges();
                entity.Entry(photoComment);

                Response.StatusCode = 200;
                createPhotoCommentFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                createPhotoCommentFlag = 0;
            }
            var returnJson = new {
                PhotoCommentByID = photoComment.PhotoCommentId.ToString(),
                createPhotoCommentFlag = createPhotoCommentFlag,
            };
            return Json(returnJson);
        }

        /// <summary>
        /// 删除相册本地内容以及数据库记录
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除相册，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteAlbumByID(string albumID, string userID) {
            int deleteAlbumFlag = 0;
            string deleteFilePath = "";

            try {
                deleteFilePath = Path.Combine(albumRootPath, userID, albumID);
                Directory.Delete(deleteFilePath, true); //删除本地文件夹 （删除文件不用写true）

                Album album = entity.Album.Single(c => c.AlbumId == int.Parse(albumID)); //查找
                entity.Album.Remove(album);
                entity.SaveChanges(); //提交删除

                Response.StatusCode = 200;
                deleteAlbumFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                deleteAlbumFlag = 0;
            }
            return Json(new {deleteAlbumFlag = deleteAlbumFlag });
        }

        /// <summary>
        /// 删除相册访问历史记录
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除历史，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteAlbumHistoryByID(string albumID, string userID) {
            int deleteAlbumHistoryFlag = 0;

            try {
                AlbumVisit albumVisit = entity.AlbumVisit.Single(c => c.AlbumId == int.Parse(albumID) && c.UserId == int.Parse(userID) );
                entity.AlbumVisit.Remove(albumVisit);
                entity.SaveChanges();

                Response.StatusCode = 200;
                deleteAlbumHistoryFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                deleteAlbumHistoryFlag = 0;
            }
            return Json(new {deleteAlbumHistoryFlag = deleteAlbumHistoryFlag });
        }

        /// <summary>
        /// 删除某一张图片
        /// </summary>
        /// <param name="photoID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deletePhotoByID(string photoID) {
            int deletePhotoFlag = 0;
            string filePath = "";
            try {
                Photo photo = entity.Photo.Single(c => c.PhotoId == int.Parse(photoID));
                filePath = photo.PhotoAddress;
                entity.Photo.Remove(photo);
                entity.SaveChanges();

                System.IO.File.Delete(filePath); //删除单个文件

                Response.StatusCode = 200;
                deletePhotoFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                deletePhotoFlag = 0;
            }

            return Json(new {deletePhotoFlag = deletePhotoFlag });
        }

        /// <summary>
        /// 刪除图片评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法删除，出现错误/异常</response>
        /// <returns>123</returns>
        [HttpPost]
        public ActionResult deletePhotoCommentByID(string commentID) {
            int deletePhotoCommentFlag = 0;

            try {
                PhotoComment photoComment = entity.PhotoComment.Single(c => c.PhotoCommentId == int.Parse(commentID));
                entity.PhotoComment.Remove(photoComment);
                entity.SaveChanges();

                Response.StatusCode = 200;
                deletePhotoCommentFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                deletePhotoCommentFlag = 0;
            }
            return Json(new { deletePhotoCommentFlag = deletePhotoCommentFlag });
        }

        /// <summary>
        /// 获取相簿浏览历史信息
        /// </summary>
        /// <param name="albumID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getAlbumHistryByID(string albumID) {
            int getAlbumHistryFlag = 0;
            //IQueryable<AlbumVisit> albumVisits = null;
            List<Dictionary<string, string>> visitors = new List<Dictionary<string, string>>();
             try {
                var albumVisits = (from c in entity.AlbumVisit
                                    where c.AlbumId == int.Parse(albumID)
                                    select c);
                foreach(var visitor in albumVisits.ToList()) {
                    var visitedUser = new Dictionary<string, string>();
                    var user = entity.User.Single(c => c.UserId == visitor.UserId);
                    visitedUser.Add("visitor", user.NickName);
                    visitedUser.Add("visitTime", visitor.AlbumVisitTime.ToString());
                    visitors.Add(visitedUser);
                }

                Response.StatusCode = 200;
                getAlbumHistryFlag = 1;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                getAlbumHistryFlag = 0;
            }
            var returnJson = new {
                albumVisits = JsonConvert.SerializeObject(visitors),
                getAlbumHistryFlag = getAlbumHistryFlag,
            };

            return Json(returnJson);
        }

        /// <summary>
        /// 获取所有相簿
        /// </summary>
        /// <param name="userID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getAllAlbumByID(string userID) {
            int getAllAlbumFlag = 0;
            IQueryable<Album> albums = null;

            try {
                albums = (from c in entity.Album
                          where c.UserId == int.Parse(userID)
                          select c);

                Response.StatusCode = 200;
                getAllAlbumFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                getAllAlbumFlag = 0;
            }
            var returnJson = new {
                albums = JsonConvert.SerializeObject(albums),
                getAllAlbumFlag = getAllAlbumFlag,
            };

            return Json(returnJson);
        }

        /// <summary>
        /// 获取相簿内所有图片
        /// </summary>
        /// <param name="albumID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getAllPhotoByID(string albumID, string userID) {
            DateTime dateTime = DateTime.Now;
            int getAllPhotoFlag = 0;
            IQueryable<Photo> photos = null;
            try {
                photos = (from c in entity.Photo
                          where c.AlbumId == int.Parse(albumID)
                          select c);

                AlbumVisit albumVisit = new AlbumVisit();
                albumVisit.AlbumId = int.Parse(albumID);
                albumVisit.UserId = int.Parse(userID);
                albumVisit.AlbumVisitTime = dateTime;
                entity.AlbumVisit.Add(albumVisit);
                entity.SaveChanges(); //更新访问记录

                Response.StatusCode = 200;
                getAllPhotoFlag = 1;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                getAllPhotoFlag = 0;
            }
            var returnJson = new {
                photos = JsonConvert.SerializeObject(photos),
                getAllPhotoFlag = getAllPhotoFlag,
            };
            return Json(returnJson);
        }

        /// <summary>
        /// 获取图片评论
        /// </summary>
        /// <param name="photoID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getPhotoCommentByID(string photoID) {
            int getPhotoCommentFlag = 0;
            IQueryable<PhotoComment> photoComments = null;
            try {
                photoComments = (from c in entity.PhotoComment
                                 where c.PhotoId == int.Parse(photoID)
                                 select c);

                Response.StatusCode = 200;
                getPhotoCommentFlag = 1;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                getPhotoCommentFlag = 0;
            }
            var returnJson = new {
                photoComments = JsonConvert.SerializeObject(photoComments),
                getPhotoCommentFlag = getPhotoCommentFlag,
            };
            return Json(returnJson);
        }

        /// <summary>
        /// 获取图片浏览历史
        /// </summary>
        /// <param name="photoID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法获取，出现错误/异常</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getPhotoHistoryByID(string photoID) {
            int getPhotoHistryFlag = 0;
            //IQueryable<AlbumVisit> albumVisits = null;
            List<Dictionary<string, string>> visitors = new List<Dictionary<string, string>>();
            try {
                var photoVisits = (from c in entity.PhotoVisit
                                   where c.PhotoId == int.Parse(photoID)
                                   select c);
                foreach (var visitor in photoVisits.ToList()) {
                    var visitedUser = new Dictionary<string, string>();
                    var user = entity.User.Single(c => c.UserId == visitor.UserId);
                    visitedUser.Add("visitor", user.NickName);
                    visitedUser.Add("visitTime", visitor.PhotoVisitTime.ToString());
                    visitors.Add(visitedUser);
                }

                Response.StatusCode = 200;
                getPhotoHistryFlag = 1;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                getPhotoHistryFlag = 0;
            }
            var returnJson = new {
                photoVisits = JsonConvert.SerializeObject(visitors),
                gePhotoHistryFlag = getPhotoHistryFlag,
            };

            return Json(returnJson);
        }

        /// <summary>
        /// 图片点赞量更新
        /// </summary>
        /// <param name="photoID"></param>
        /// <response code="200">成功</response>
        /// <response code="403">无法更新点赞，出现错误/异常</response>
        /// <returns></returns>
        [HttpGet]
        public ActionResult setPhotoLike(string photoID) {
            int setPhotoLikeFlag = 0;
            int likes = 0;

            try {
                var photo = entity.Photo.Single(c => c.PhotoId == int.Parse(photoID));
                photo.PhotoLikes += 1;
                likes = (int)photo.PhotoLikes;
                entity.Photo.Update(photo);
                entity.SaveChanges();

                Response.StatusCode = 200;
                setPhotoLikeFlag = 1;

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.StatusCode = 403;
                setPhotoLikeFlag = 0;
            }
            var returnJson = new {
                photoID = photoID,
                photoLikes = likes,
                setPhotoLikeFlag = setPhotoLikeFlag,
            };
            return Json(returnJson);
        }

    }
}