# 话题区，查看话题，发表话题

[TOC]

## 开发环境

需要安装富文本编辑器的环境：

1. 在项目根目录下执行命令cnpm install vue-quill-editor 

## query传值

* 传值形式如下：

    * `<router-link :to="{path:'/ViewTopic', query:{topicID:TopicId,userID:myID}}">链接</router-link>`

* 导航到TopicArea页面需要传值：

    * userID（当前登录的用户ID）

* 导航到ViewTopic页面需要传值：

    * userID（当前登录的用户ID）

    * topicID（要查看的话题的ID）

* 导航到PostTopic页面需要传值：

    * userID（当前登录的用户ID）