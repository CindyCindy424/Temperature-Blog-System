# 话题区，查看话题，发表话题

[TOC]

## 开发环境

1. 富文本编辑器：在项目根目录下执行命令cnpm install vue-quill-editor 
2. ECharts:在项目根目录下执行命令cnpm install echarts -S

## query传值

* 跳转传值形式如下（两种皆可）：

    * `<router-link :to="{path:'/ViewTopic', query:{topicID:TopicId,userID:myID}}">链接</router-link>`
    * `this.$router.push({path:'/ViewTopic',query:{userID:this.myID,topicID:receive.topicID}});`

* 导航到TopicArea页面需要传值：

    * userID（当前登录的用户ID）

    * zoneID（按分区查看话题）

* 导航到ViewTopic页面需要传值：

    * userID（当前登录的用户ID）

    * topicID（要查看的话题的ID）

* 导航到PostTopic页面需要传值：

    * userID（当前登录的用户ID）