### 修改记录
### 0825修改
### 修改人：邓欣凌
### 主要修改内容：1）所有的id改为int类型，加上了auto_increment
### 所有的auto_increment = 0

# 用户表
create table USER (
    USER_ID int auto_increment,  #auto_increment=0，但是只能后续设置
    STATE varchar(4),
    NICK_NAME varchar(20),
    PASSWORD varchar(20),
    AVATR varchar(80),
    GENDER varchar(2) check ( GENDER in ('男', '女') ),
    LOCATION varchar(20),
    DOB DATE,
    EMAIL varchar(30),
    TEL varchar(20),
    WECHAT varchar(20),
    ARTICLE_NUM INTEGER,
    FOLLOW_NUM INTEGER,
    primary key(USER_ID)
);

alter table USER auto_increment=0;


#分区表
create table ZONE (
    ZONE_ID int, #分区id不需要自增，直接初始定义即可，但还是修改为int型
    ZONE_NAME varchar(10),
    ZONE_ARTICLE_NUM INTEGER,
    ZONE_TOPIC_NUM INTEGER,
    primary key (ZONE_ID)
);


#文章表
create table ARTICLE (
    ARTICLE_ID int auto_increment,
    TITLE varchar(40),
    USER_ID int,
    ARTICLE_CONTENT text,
    ARTICLE_LIKES INTEGER,
    COLLECT_NUM INTEGER,
    READ_NUM INTEGER,
    ARTICLE_UPLOAD_TIME datetime, # 1000-01-01 00:00:00
    ZONE_ID int,
    primary key (ARTICLE_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (ZONE_ID) references ZONE(ZONE_ID) on delete set null
);

alter table ARTICLE auto_increment=0;

#文章评论回复表
create table ARTICLE_COMMENT_REPLY (
    ARTICLE_CR_ID int auto_increment,
    ARTICLE_CR_CONTENT text,
    ARTICLE_ID int,
    USER_ID int,
    ARTICLE_CR_TIME datetime,
    PARENT_CR_ID int,
    primary key (ARTICLE_CR_ID),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID) on delete cascade,
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (PARENT_CR_ID) references ARTICLE_COMMENT_REPLY(ARTICLE_CR_ID) on delete cascade 
);

alter table ARTICLE_COMMENT_REPLY auto_increment=0;

#文章排行榜表
create table ARTICLE_RANK (
    ARTICLE_RANK_DATE date,
    ARTICLE_RANK_TIME time,
    ARTICLE_RANK_TYPE varchar(1),
    ARTICLE_RANK varchar(3),
    ARTICLE_ID int,
    primary key (ARTICLE_RANK_DATE, ARTICLE_RANK_TIME, ARTICLE_RANK_TYPE, ARTICLE_RANK),
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID)
);

#文章访问表
create table ARTICLE_VISIT (
    USER_ID int,
    ARTICLE_ID int,
    ARTICLE_VISIT_TIME datetime,
    primary key (USER_ID, ARTICLE_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID) on delete cascade
);

#收藏夹表
create table FAVOURITE (
    FAVOURITE_ID int auto_increment,
    FAVOURITE_NAME varchar(20),
    ARTICLE_NUM integer check( ARTICLE_NUM >= 0),
    USER_ID int,
    primary key (FAVOURITE_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);
alter table FAVOURITE auto_increment=0;

#文章收藏表
create table FAVOURITE_ARTICLE (
    FAVOURITE_ID int,
    ARTICLE_ID int,
    FAVOURITE_TIME datetime,
    primary key (FAVOURITE_ID, ARTICLE_ID),
    foreign key (FAVOURITE_ID) references FAVOURITE(FAVOURITE_ID) on delete cascade,
    foreign key (ARTICLE_ID) references ARTICLE(ARTICLE_ID) on delete cascade
);

#相册表
create table ALBUM (
    ALBUM_ID int auto_increment,
    ALBUM_INTRODUCTION tinytext,
    ALBUM_NAME varchar(20),
    ALBUM_TIME datetime,
    USER_ID int,
    primary key (ALBUM_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);
alter table ALBUM auto_increment=0;

#相册访问表
create table ALBUM_VISIT (
    ALBUM_ID int,
    USER_ID int,
    ALBUM_VISIT_TIME datetime, #1000-01-01 00:00:00
    primary key (USER_ID, ALBUM_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (ALBUM_ID) references ALBUM(ALBUM_ID) on delete cascade
);

#相片表
create table PHOTO (
    PHOTO_ID int auto_increment,
    ALBUM_ID int,
    PHOTO_LIKES INTEGER check ( PHOTO_LIKES >= 0 ),
    VISIT_NUM INTEGER check ( VISIT_NUM >= 0 ),
    PHOTO_ADDRESS varchar(80),
    PHOTO_UPLOAD_TIME datetime,
    USER_ID int,
    primary key (PHOTO_ID),
    foreign key (ALBUM_ID) references ALBUM(ALBUM_ID) on delete cascade,
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);
alter table PHOTO auto_increment=0;

#相片访问表
create table PHOTO_VISIT (
    PHOTO_ID int,
    USER_ID int,
    PHOTO_VISIT_TIME datetime,
    primary key (PHOTO_ID, USER_ID),
    foreign key (PHOTO_ID) references PHOTO(PHOTO_ID) on delete cascade,
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);

#相片评论表
create table PHOTO_COMMENT (
    PHOTO_COMMENT_ID int auto_increment,
    PHOTO_COMMENT_CONTENT text,
    PHOTO_ID int,
    USER_ID int,
    PHOTO_COMMENT_TIME datetime,
    primary key (PHOTO_COMMENT_ID),
    foreign key (PHOTO_ID) references PHOTO(PHOTO_ID) on delete cascade,
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);
alter table PHOTO_COMMENT auto_increment=0;

#话题表
create table TOPIC (
    TOPIC_ID int auto_increment,
    TOPIC_CONTENT text,
    ANSWER_NUM INTEGER,
    USER_ID int,
    TOPIC_UPLOAD_TIME datetime, #1000-01-01 00:00:00
    ZONE_ID int,
    primary key (TOPIC_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (ZONE_ID) references ZONE(ZONE_ID) on delete cascade
);
alter table TOPIC auto_increment=0;

#话题回答回复表
create table TOPIC_ANSWER_REPLY (
    TOPIC_ANSWER_ID int auto_increment,
    TOPIC_ID int,
    ANSWER_LIKES INTEGER,
    ANSWER_CONTENT text,
    USER_ID int,
    ANSWER_UPLOAD_TIME datetime,
    PARENT_ANSWER_ID int,
    primary key (TOPIC_ANSWER_ID),
    foreign key (TOPIC_ID) references TOPIC(TOPIC_ID) on delete cascade,
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key (PARENT_ANSWER_ID) references TOPIC_ANSWER_REPLY(TOPIC_ANSWER_ID) on delete cascade
);
alter table TOPIC_ANSWER_REPLY auto_increment=0;

#话题排行榜表
create table TOPIC_RANK (
    TOPIC_RANK_DATE date,
    TOPIC_RANK_TIME time,
    TOPIC_ID int,
    TOPIC_RANK_TYPE varchar(1),
    TOPIC_RANK varchar(3),
    primary key (TOPIC_RANK_DATE, TOPIC_RANK_TIME, TOPIC_RANK_TYPE, TOPIC_RANK),
    foreign key (TOPIC_ID) references TOPIC(TOPIC_ID) on delete cascade
);

#消息库表
create table MESSAGE_LIBRARY (
    MESSAGE_ID int auto_increment,
    MESSAGE_TYPE varchar(5),
    MESSAGE_CONTENT tinytext,
    MESSAGE_TIME datetime,
    primary key (MESSAGE_ID)
);
alter table MESSAGE_LIBRARY auto_increment=0;


#消息接收表
create table MESSAGE_RECEIVE (
    MESSAGE_ID int,
    USER_ID int,
    READ_STATE bool,
    primary key (MESSAGE_ID, USER_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key(MESSAGE_ID) references MESSAGE_LIBRARY(MESSAGE_ID) on delete cascade ##之前没写这个外码约束
);

#消息发送表
create table MESSAGE_SEND (
    MESSAGE_ID int,
    USER_ID int,
    RECEIVE_STATE bool, ##之前没有这个
    primary key (MESSAGE_ID, USER_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade,
    foreign key(MESSAGE_ID) references MESSAGE_LIBRARY(MESSAGE_ID) on delete cascade##之前没写这个外码约束
);

#公告表
create table ANNOUNCEMENT (
    ANNOUNCEMENT_ID int auto_increment,
    ANNOUNCEMENT_CONTENT text,
    ANNOUNCEMENT_UPLOAD_TIME datetime,
    USER_ID int,
    primary key (ANNOUNCEMENT_ID),
    foreign key (USER_ID) references USER(USER_ID) on delete cascade
);
alter table ANNOUNCEMENT auto_increment=0;

#关注表
create table USER_FOLLOW(
    ACTIVE_USER_ID int,
	PASSIVE_USER_ID int,
	primary key (ACTIVE_USER_ID, PASSIVE_USER_ID),
	foreign key (ACTIVE_USER_ID) references USER(USER_ID) on delete cascade,
	foreign key (PASSIVE_USER_ID) references USER(USER_ID) on delete cascade
);

