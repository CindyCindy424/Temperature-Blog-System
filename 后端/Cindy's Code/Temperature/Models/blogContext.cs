using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Temperature.Models
{
    public partial class blogContext : DbContext
    {
        public blogContext()
        {
        }

        public blogContext(DbContextOptions<blogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<AlbumVisit> AlbumVisit { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCommentReply> ArticleCommentReply { get; set; }
        public virtual DbSet<ArticleRank> ArticleRank { get; set; }
        public virtual DbSet<ArticleVisit> ArticleVisit { get; set; }
        public virtual DbSet<Favourite> Favourite { get; set; }
        public virtual DbSet<FavouriteArticle> FavouriteArticle { get; set; }
        public virtual DbSet<MessageLibrary> MessageLibrary { get; set; }
        public virtual DbSet<MessageReceive> MessageReceive { get; set; }
        public virtual DbSet<MessageSend> MessageSend { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<PhotoComment> PhotoComment { get; set; }
        public virtual DbSet<PhotoVisit> PhotoVisit { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<TopicAnswerReply> TopicAnswerReply { get; set; }
        public virtual DbSet<TopicRank> TopicRank { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserFollow> UserFollow { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=cindy0424_mysql;database=blog_new");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("album");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.AlbumId).HasColumnName("ALBUM_ID");

                entity.Property(e => e.AlbumIntroduction)
                    .HasColumnName("ALBUM_INTRODUCTION")
                    .HasColumnType("tinytext");

                entity.Property(e => e.AlbumName)
                    .HasColumnName("ALBUM_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AlbumTime).HasColumnName("ALBUM_TIME");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Album)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("album_ibfk_1");
            });

            modelBuilder.Entity<AlbumVisit>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AlbumId })
                    .HasName("PRIMARY");

                entity.ToTable("album_visit");

                entity.HasIndex(e => e.AlbumId)
                    .HasName("ALBUM_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.AlbumId).HasColumnName("ALBUM_ID");

                entity.Property(e => e.AlbumVisitTime).HasColumnName("ALBUM_VISIT_TIME");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumVisit)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("album_visit_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AlbumVisit)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("album_visit_ibfk_1");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("announcement");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.AnnouncementId).HasColumnName("ANNOUNCEMENT_ID");

                entity.Property(e => e.AnnouncementContent).HasColumnName("ANNOUNCEMENT_CONTENT");

                entity.Property(e => e.AnnouncementUploadTime).HasColumnName("ANNOUNCEMENT_UPLOAD_TIME");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Announcement)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("announcement_ibfk_1");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.HasIndex(e => e.ZoneId)
                    .HasName("ZONE_ID");

                entity.Property(e => e.ArticleId).HasColumnName("ARTICLE_ID");

                entity.Property(e => e.ArticleContent).HasColumnName("ARTICLE_CONTENT");

                entity.Property(e => e.ArticleLikes).HasColumnName("ARTICLE_LIKES");

                entity.Property(e => e.ArticleUploadTime).HasColumnName("ARTICLE_UPLOAD_TIME");

                entity.Property(e => e.CollectNum).HasColumnName("COLLECT_NUM");

                entity.Property(e => e.ReadNum).HasColumnName("READ_NUM");

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ZoneId).HasColumnName("ZONE_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_ibfk_1");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("article_ibfk_2");
            });

            modelBuilder.Entity<ArticleCommentReply>(entity =>
            {
                entity.HasKey(e => e.ArticleCrId)
                    .HasName("PRIMARY");

                entity.ToTable("article_comment_reply");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("ARTICLE_ID");

                entity.HasIndex(e => e.ParentCrId)
                    .HasName("PARENT_CR_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.ArticleCrId).HasColumnName("ARTICLE_CR_ID");

                entity.Property(e => e.ArticleCrContent).HasColumnName("ARTICLE_CR_CONTENT");

                entity.Property(e => e.ArticleCrTime).HasColumnName("ARTICLE_CR_TIME");

                entity.Property(e => e.ArticleId).HasColumnName("ARTICLE_ID");

                entity.Property(e => e.ParentCrId).HasColumnName("PARENT_CR_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCommentReply)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_comment_reply_ibfk_1");

                entity.HasOne(d => d.ParentCr)
                    .WithMany(p => p.InverseParentCr)
                    .HasForeignKey(d => d.ParentCrId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_comment_reply_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ArticleCommentReply)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_comment_reply_ibfk_2");
            });

            modelBuilder.Entity<ArticleRank>(entity =>
            {
                entity.HasKey(e => new { e.ArticleRankDate, e.ArticleRankTime, e.ArticleRankType, e.ArticleRank1 })
                    .HasName("PRIMARY");

                entity.ToTable("article_rank");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("ARTICLE_ID");

                entity.Property(e => e.ArticleRankDate)
                    .HasColumnName("ARTICLE_RANK_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ArticleRankTime).HasColumnName("ARTICLE_RANK_TIME");

                entity.Property(e => e.ArticleRankType)
                    .HasColumnName("ARTICLE_RANK_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ArticleRank1)
                    .HasColumnName("ARTICLE_RANK")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ArticleId).HasColumnName("ARTICLE_ID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleRank)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("article_rank_ibfk_1");
            });

            modelBuilder.Entity<ArticleVisit>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ArticleId })
                    .HasName("PRIMARY");

                entity.ToTable("article_visit");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("ARTICLE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ArticleId).HasColumnName("ARTICLE_ID");

                entity.Property(e => e.ArticleVisitTime).HasColumnName("ARTICLE_VISIT_TIME");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleVisit)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("article_visit_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ArticleVisit)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("article_visit_ibfk_1");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.ToTable("favourite");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.FavouriteId).HasColumnName("FAVOURITE_ID");

                entity.Property(e => e.ArticleNum).HasColumnName("ARTICLE_NUM");

                entity.Property(e => e.FavouriteName)
                    .HasColumnName("FAVOURITE_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("favourite_ibfk_1");
            });

            modelBuilder.Entity<FavouriteArticle>(entity =>
            {
                entity.HasKey(e => new { e.FavouriteId, e.ArticleId })
                    .HasName("PRIMARY");

                entity.ToTable("favourite_article");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("ARTICLE_ID");

                entity.Property(e => e.FavouriteId).HasColumnName("FAVOURITE_ID");

                entity.Property(e => e.ArticleId).HasColumnName("ARTICLE_ID");

                entity.Property(e => e.FavouriteTime).HasColumnName("FAVOURITE_TIME");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.FavouriteArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("favourite_article_ibfk_2");

                entity.HasOne(d => d.Favourite)
                    .WithMany(p => p.FavouriteArticle)
                    .HasForeignKey(d => d.FavouriteId)
                    .HasConstraintName("favourite_article_ibfk_1");
            });

            modelBuilder.Entity<MessageLibrary>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PRIMARY");

                entity.ToTable("message_library");

                entity.Property(e => e.MessageId).HasColumnName("MESSAGE_ID");

                entity.Property(e => e.MessageContent)
                    .HasColumnName("MESSAGE_CONTENT")
                    .HasColumnType("tinytext");

                entity.Property(e => e.MessageTime).HasColumnName("MESSAGE_TIME");

                entity.Property(e => e.MessageType)
                    .HasColumnName("MESSAGE_TYPE")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MessageReceive>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("message_receive");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.MessageId).HasColumnName("MESSAGE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ReadState)
                    .HasColumnName("READ_STATE")
                    .HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReceive)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("message_receive_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReceive)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("message_receive_ibfk_1");
            });

            modelBuilder.Entity<MessageSend>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("message_send");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.MessageId).HasColumnName("MESSAGE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ReceiveState)
                    .HasColumnName("RECEIVE_STATE")
                    .HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageSend)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("message_send_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageSend)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("message_send_ibfk_1");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("photo");

                entity.HasIndex(e => e.AlbumId)
                    .HasName("ALBUM_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.PhotoId).HasColumnName("PHOTO_ID");

                entity.Property(e => e.AlbumId).HasColumnName("ALBUM_ID");

                entity.Property(e => e.PhotoAddress)
                    .HasColumnName("PHOTO_ADDRESS")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoLikes).HasColumnName("PHOTO_LIKES");

                entity.Property(e => e.PhotoUploadTime).HasColumnName("PHOTO_UPLOAD_TIME");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.VisitNum).HasColumnName("VISIT_NUM");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("photo_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("photo_ibfk_2");
            });

            modelBuilder.Entity<PhotoComment>(entity =>
            {
                entity.ToTable("photo_comment");

                entity.HasIndex(e => e.PhotoId)
                    .HasName("PHOTO_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.PhotoCommentId).HasColumnName("PHOTO_COMMENT_ID");

                entity.Property(e => e.PhotoCommentContent).HasColumnName("PHOTO_COMMENT_CONTENT");

                entity.Property(e => e.PhotoCommentTime).HasColumnName("PHOTO_COMMENT_TIME");

                entity.Property(e => e.PhotoId).HasColumnName("PHOTO_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.PhotoComment)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("photo_comment_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhotoComment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("photo_comment_ibfk_2");
            });

            modelBuilder.Entity<PhotoVisit>(entity =>
            {
                entity.HasKey(e => new { e.PhotoId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("photo_visit");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.PhotoId).HasColumnName("PHOTO_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.PhotoVisitTime).HasColumnName("PHOTO_VISIT_TIME");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.PhotoVisit)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("photo_visit_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhotoVisit)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("photo_visit_ibfk_2");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topic");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.HasIndex(e => e.ZoneId)
                    .HasName("ZONE_ID");

                entity.Property(e => e.TopicId).HasColumnName("TOPIC_ID");

                entity.Property(e => e.AnswerNum).HasColumnName("ANSWER_NUM");

                entity.Property(e => e.TopicContent).HasColumnName("TOPIC_CONTENT");

                entity.Property(e => e.TopicUploadTime).HasColumnName("TOPIC_UPLOAD_TIME");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ZoneId).HasColumnName("ZONE_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_ibfk_1");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_ibfk_2");
            });

            modelBuilder.Entity<TopicAnswerReply>(entity =>
            {
                entity.HasKey(e => e.TopicAnswerId)
                    .HasName("PRIMARY");

                entity.ToTable("topic_answer_reply");

                entity.HasIndex(e => e.ParentAnswerId)
                    .HasName("PARENT_ANSWER_ID");

                entity.HasIndex(e => e.TopicId)
                    .HasName("TOPIC_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.TopicAnswerId).HasColumnName("TOPIC_ANSWER_ID");

                entity.Property(e => e.AnswerContent).HasColumnName("ANSWER_CONTENT");

                entity.Property(e => e.AnswerLikes).HasColumnName("ANSWER_LIKES");

                entity.Property(e => e.AnswerUploadTime).HasColumnName("ANSWER_UPLOAD_TIME");

                entity.Property(e => e.ParentAnswerId).HasColumnName("PARENT_ANSWER_ID");

                entity.Property(e => e.TopicId).HasColumnName("TOPIC_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.ParentAnswer)
                    .WithMany(p => p.InverseParentAnswer)
                    .HasForeignKey(d => d.ParentAnswerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_answer_reply_ibfk_3");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicAnswerReply)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_answer_reply_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TopicAnswerReply)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_answer_reply_ibfk_2");
            });

            modelBuilder.Entity<TopicRank>(entity =>
            {
                entity.HasKey(e => new { e.TopicRankDate, e.TopicRankTime, e.TopicRankType, e.TopicRank1 })
                    .HasName("PRIMARY");

                entity.ToTable("topic_rank");

                entity.HasIndex(e => e.TopicId)
                    .HasName("TOPIC_ID");

                entity.Property(e => e.TopicRankDate)
                    .HasColumnName("TOPIC_RANK_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.TopicRankTime).HasColumnName("TOPIC_RANK_TIME");

                entity.Property(e => e.TopicRankType)
                    .HasColumnName("TOPIC_RANK_TYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TopicRank1)
                    .HasColumnName("TOPIC_RANK")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TopicId).HasColumnName("TOPIC_ID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicRank)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topic_rank_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ArticleNum).HasColumnName("ARTICLE_NUM");

                entity.Property(e => e.Avatr)
                    .HasColumnName("AVATR")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FollowNum).HasColumnName("FOLLOW_NUM");

                entity.Property(e => e.Gender)
                    .HasColumnName("GENDER")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasColumnName("NICK_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("STATE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasColumnName("TEL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Wechat)
                    .HasColumnName("WECHAT")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFollow>(entity =>
            {
                entity.HasKey(e => new { e.ActiveUserId, e.PassiveUserId })
                    .HasName("PRIMARY");

                entity.ToTable("user_follow");

                entity.HasIndex(e => e.PassiveUserId)
                    .HasName("PASSIVE_USER_ID");

                entity.Property(e => e.ActiveUserId).HasColumnName("ACTIVE_USER_ID");

                entity.Property(e => e.PassiveUserId).HasColumnName("PASSIVE_USER_ID");

                entity.HasOne(d => d.ActiveUser)
                    .WithMany(p => p.UserFollowActiveUser)
                    .HasForeignKey(d => d.ActiveUserId)
                    .HasConstraintName("user_follow_ibfk_1");

                entity.HasOne(d => d.PassiveUser)
                    .WithMany(p => p.UserFollowPassiveUser)
                    .HasForeignKey(d => d.PassiveUserId)
                    .HasConstraintName("user_follow_ibfk_2");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.ToTable("zone");

                entity.Property(e => e.ZoneId).HasColumnName("ZONE_ID");

                entity.Property(e => e.ZoneArticleNum).HasColumnName("ZONE_ARTICLE_NUM");

                entity.Property(e => e.ZoneName)
                    .HasColumnName("ZONE_NAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZoneTopicNum).HasColumnName("ZONE_TOPIC_NUM");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
