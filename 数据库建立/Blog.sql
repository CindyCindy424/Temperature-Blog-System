# 用户表
create table USER (
    USER_ID varchar(8),
    STATE varchar(4),
    NICK_NAME varchar(20),
    PASSWORD varchar(20),
    AVATR varchar(80),
    GENDER varchar(2),
    LOCATION varchar(20),
    DOB DATE,
    EMAIL varchar(30),
    TEL varchar(20), #type: number in Document
    WECHAT varchar(20),
    ARTICLE_NUM INTEGER,
    FOLLOW_NUM INTEGER,
    primary key(USER_ID)
);

#分区表
create table ZONE (
    ZONE_ID varchar(8),
    ZONE_NAME varchar(10),
    ZONE_ARTICLE_NUM INTEGER,
    ZONE_TOPIC_NUM INTEGER,
    primary key (ZONE_ID)
);

#文章表
create table ARTICLE (
    ARTICLE_ID varchar(8),
    TITLE varchar(40),
    USER_ID varchar(8),
    ARTICLE_CONTENT varchar(4000), #too few？
    ARTICLE_LIKES INTEGER,
    COLLECT_NUM INTEGER,
    READ_NUM INTEGER,
    ARTICLE_UPLOAD_TIME datetime, # 1000-01-01 00:00:00
    ZONE_ID varchar(8),
    primary key (ARTICLE_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (ZONE_ID) references ZONE(ZONE_ID)
);

#文章评论回复表
create table ARTICLE_COMMENT_REPLY (
    ARTICLE_CR_ID varchar(8),
    ARTICLE_CR_CONTENT varchar(1000), #1000？
    ARTICLE_ID varchar(8),
    USER_ID varchar(8),
    ARTICLE_CR_TIME datetime, #1000-01-01 00:00:00
    PARENT_CR_ID varchar(8),
    primary key (ARTICLE_CR_ID),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (PARENT_CR_ID) references ARTICLE_COMMENT_REPLY(ARTICLE_CR_ID)
);

#文章排行榜表
create table ARTICLE_RANK (
    ARTICLE_RANK_DATE date,
    ARTICLE_RANK_TIME time,
    ARTICLE_RANK_TYPE varchar(1), #just 1？
    ARTICLE_RANK SMALLINT, #type: varchar in Document
    ARTICLE_ID varchar(8),
    primary key (ARTICLE_RANK_DATE, ARTICLE_RANK_TIME, ARTICLE_RANK_TYPE, ARTICLE_RANK),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID)
);

#文章访问表
create table ARTICLE_VISIT (
    USER_ID varchar(8),
    ARTICLE_ID varchar(8),
    ARTICLE_VISIT_TIME datetime, #1000-01-01 00:00:00
    primary key (USER_ID, ARTICLE_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID)
);

#收藏表
create table FAVOURITE (
    FAVOURITE_ID varchar(8),
    FAVOURITE_NAME varchar(20),
    ARTICLE_NUM mediumint, #type: number in Document
    USER_ID varchar(8),
    primary key (FAVOURITE_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#文章收藏表
create table FAVOURITE_ARTICLE (
    FAVOURITE_ID varchar(8),
    ARTICLE_ID varchar(8),
    FAVOURITE_TIME datetime, #1000-01-01 00:00:00
    primary key (FAVOURITE_ID, ARTICLE_ID),
    foreign key (FAVOURITE_ID) references FAVOURITE(FAVOURITE_ID),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID)
);

#相册表
create table ALBUM (
    ALBUM_ID varchar(8),
    ALBUM_INTRODUCTION varchar(50),
    ALBUM_NAME varchar(20),
    ALBUM_TIME datetime, #1000-01-01 00:00:00
    USER_ID varchar(8),
    primary key (ALBUM_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#相册访问表
create table ALBUM_VISIT (
    ALBUM_ID varchar(8),
    USER_ID varchar(8),
    ALBUM_VISIT_TIME datetime, #1000-01-01 00:00:00
    primary key (USER_ID, ALBUM_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (ALBUM_ID) references ALBUM(ALBUM_ID)
);

#相片表
create table PHOTO (
    PHOTO_ID varchar(8),
    ALBUM_ID varchar(8),
    PHOTO_LIKES varchar(8),
    VISIT_NUM INTEGER,
    PHOTO_ADDRESS varchar(80),
    PHOTO_UPLOAD_TIME datetime, #1000-01-01 00:00:00
    USER_ID varchar(8),
    primary key (PHOTO_ID),
    foreign key (ALBUM_ID) references ALBUM(ALBUM_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#相片访问表
create table PHOTO_VISIT (
    PHOTO_ID varchar(8),
    USER_ID varchar(8),
    PHOTO_VISIT_TIME datetime, #1000-01-01 00:00:00
    primary key (PHOTO_ID, USER_ID),
    foreign key (PHOTO_ID) references PHOTO(PHOTO_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#相片评论表
create table PHOTO_COMMENT (
    PHOTO_COMMENT_ID varchar(8),
    PHOTO_COMMENT_CONTENT varchar(1000),
    PHOTO_ID varchar(8),
    USER_ID varchar(8),
    PHOTO_COMMENT_TIME datetime, #1000-01-01 00:00:00
    primary key (PHOTO_COMMENT_ID),
    foreign key (PHOTO_ID) references PHOTO(PHOTO_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#话题表
create table TOPIC (
    TOPIC_ID varchar(8),
    TOPIC_CONTENT varchar(1000),
    ANSWER_NUM INTEGER,
    USER_ID varchar(8),
    TOPIC_UPLOAD_TIME datetime, #1000-01-01 00:00:00
    ZONE_ID varchar(8),
    primary key (TOPIC_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (ZONE_ID) references ZONE(ZONE_ID)
);

#话题回答回复表
create table TOPIC_ANSWER_REPLY (
    TOPIC_ANSWER_ID varchar(8),
    TOPIC_ID varchar(8),
    ANSWER_LIKES INTEGER,
    ANSWER_CONTENT varchar(1000),
    USER_ID varchar(8),
    ANSWER_UPLOAD_TIME datetime, #1000-01-01 00:00:00
    PARENT_ANSWER_ID varchar(8),
    primary key (TOPIC_ANSWER_ID),
    foreign key (TOPIC_ID) references TOPIC(TOPIC_ID),
    foreign key (USER_ID) references USER(USER_ID),
    foreign key (PARENT_ANSWER_ID) references TOPIC_ANSWER_REPLY(TOPIC_ANSWER_ID)
);

#话题排行榜表
create table TOPIC_RANK (
    TOPIC_RANK_DATE date,
    TOPIC_RANK_TIME time,
    TOPIC_ID varchar(8),
    TOPIC_RANK_TYPE varchar(1), #?
    TOPIC_RANK varchar(3),
    primary key (TOPIC_RANK_DATE, TOPIC_RANK_TIME, TOPIC_RANK_TYPE, TOPIC_RANK),
    foreign key (TOPIC_ID) references TOPIC(TOPIC_ID)
);

#消息库表
create table MESSAGE_LIBRARY (
    MESSAGE_ID varchar(8),
    MESSAGE_TYPE varchar(5),
    MESSAGE_CONTENT varchar(50),
    MESSAGE_TIME datetime, #1000-01-01 00:00:00
    primary key (MESSAGE_ID)
);

#消息接收表
create table MESSAGE_RECEIVE (
    MESSAGE_ID varchar(8),
    USER_ID varchar(8),
    READ_STATE bool,
    primary key (MESSAGE_ID, USER_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#消息发送表
create table MESSAGE_SEND (
    MESSAGE_ID varchar(8),
    USER_ID varchar(8),
    primary key (MESSAGE_ID, USER_ID),
    foreign key (USER_ID) references USER(USER_ID)
);

#公告表
create table ANNOUNCEMENT (
    ANNOUNCEMENT_ID varchar(8),
    ANNOUNCEMENT_CONTENT varchar(500),
    ANNOUNCEMENT_UPLOAD_TIME datetime, #1000-01-01 00:00:00
    USER_ID varchar(8),
    primary key (ANNOUNCEMENT_ID),
    foreign key (USER_ID) references USER(USER_ID)
);
