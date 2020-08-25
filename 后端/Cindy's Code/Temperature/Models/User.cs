using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class User
    {
        public User()
        {
            Album = new HashSet<Album>();
            AlbumVisit = new HashSet<AlbumVisit>();
            Announcement = new HashSet<Announcement>();
            Article = new HashSet<Article>();
            ArticleCommentReply = new HashSet<ArticleCommentReply>();
            ArticleVisit = new HashSet<ArticleVisit>();
            Favourite = new HashSet<Favourite>();
            MessageReceive = new HashSet<MessageReceive>();
            MessageSend = new HashSet<MessageSend>();
            Photo = new HashSet<Photo>();
            PhotoComment = new HashSet<PhotoComment>();
            PhotoVisit = new HashSet<PhotoVisit>();
            Topic = new HashSet<Topic>();
            TopicAnswerReply = new HashSet<TopicAnswerReply>();
            UserFollowActiveUser = new HashSet<UserFollow>();
            UserFollowPassiveUser = new HashSet<UserFollow>();
        }

        public int UserId { get; set; }
        public string State { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Avatr { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public DateTime? Dob { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Wechat { get; set; }
        public int? ArticleNum { get; set; }
        public int? FollowNum { get; set; }

        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<AlbumVisit> AlbumVisit { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<ArticleCommentReply> ArticleCommentReply { get; set; }
        public virtual ICollection<ArticleVisit> ArticleVisit { get; set; }
        public virtual ICollection<Favourite> Favourite { get; set; }
        public virtual ICollection<MessageReceive> MessageReceive { get; set; }
        public virtual ICollection<MessageSend> MessageSend { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<PhotoComment> PhotoComment { get; set; }
        public virtual ICollection<PhotoVisit> PhotoVisit { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
        public virtual ICollection<TopicAnswerReply> TopicAnswerReply { get; set; }
        public virtual ICollection<UserFollow> UserFollowActiveUser { get; set; }
        public virtual ICollection<UserFollow> UserFollowPassiveUser { get; set; }
    }
}
