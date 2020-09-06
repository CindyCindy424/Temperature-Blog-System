<template>
  <div id="topic-area-page">
			<div id="photo">
        <img src="../assets/images/banner.png" />
			</div>
      <div id="main">
        <div id="latest-topics">
        	<div id="latest-topics-title">
        		<span>最新话题</span>
        		<input id="topics-search-box" type="text" placeholder="                         搜索话题"/>
        		<div id="topics-search-button"></div>
        	</div>
        	<div id="latest-topics-list">
        		<div class="l-t-item" v-for="item in LTopics" :key="item.topicID">
        			<span class="l-t-i-title">{{item.title}}</span>
        			<div class="l-t-i-content">{{item.content}}</div>
        			<span class="l-t-i-date">{{item.date}}</span>
        			<span class="l-t-i-time">{{item.time}}</span>
        			<span class="l-t-i-answerCount">回答：{{item.answerCount}}</span>
        			<span class="l-t-i-delete">删除</span>
        		</div>
        	</div>
        	<div id="latest-topics-page-controller">
        		<div id="l-t-last-page">上一页</div>
            <div class="l-t-page-num" v-for="item in pageNums">
              {{item.page}}
            </div>
        		<div id="l-t-next-page">下一页</div>
        	</div>
        </div>
        <div id="right-aside">
            <div id="ask-question">我要提问</div>
            <div id="my-topics">
              <div id="my-topics-title">我的话题</div>
              <div id="my-topisc-msg">
                我发起了<span>{{myTopicsNum}}</span>个话题，<br />
                收获了<span>{{myTopicsAnsNum}}</span>个回答
              </div>
              <div id="my-topics-list">
                <div id="m-t-latest">最近话题</div>
                <div class="right-aside-list-item" v-for="item in MTopics">
                  {{item.title}}
                </div>
              </div>
            </div>
            <div id="hottest-topics">
            	<div id="hottest-topics-title">最热话题</div>
            	<div id="hottest-topics-list">
            		<div class="right-aside-list-item" v-for="item in HTopics">
                  {{item.title}}
                </div>
            	</div>
            </div>
        </div>
      </div>
	</div>
</template>

<script>
  export default{
    name: 'TopicArea',
		data(){
			return{
				LTopics: [
				{
					topicID: 1,
					title: "啥都行",
					content: "谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的",
					date: "2020-08-22",
					time: "22:20",
					answerCount: 22
				},
				{
					topicID: 2,
					title: "啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					answerCount: 22
				}
				],
        MTopics:[
          {
            topicID: 1,
            title: "如何做一个博客网站啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊"
          },
          {
            topicID: 2,
            title: "如何做一个博客网站呢"
          }
        ],
        HTopics:[
          {
            topicID: 1,
            title: "如何做一个博客网站"
          },
          {
            topicID: 2,
            title: "如何做一个博客网站呢"
          }
        ],
				pageNums:[
					{page: 1},
					{page: 2},
					{page: 3}
				],
				selectedPage: 1,
				myTopicsNum: 40,
				myTopicsAnsNum: 0
			}
		},
    created:function(){

    }
  }
</script>

<style>
  *{
  	margin: 0px;
  	padding: 0px;
  }
  #topic-area-page{
    display: flex;
    flex-direction: column;
    background-color: #E5E5E5;
    min-width: calc(880px + 40px + 360px);
  }
  #photo{
    display: flex;
    align-self: center;
    justify-content: center;
    align-items: center;

  	height: 352px;
  	width: 100%;
    overflow: hidden;
  }
  #photo>img{
    width: calc(880px + 40px + 360px);
  }
  #main{
    display: flex;
    justify-content: center;
    padding: 20px;
  }
  #latest-topics{
  	width: 880px;
  }
  #latest-topics-title{
    display: flex;
    align-items: center;
    padding: 20px 20px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  }
  #latest-topics-title>span{
    flex-grow: 1;

    text-align: left;
  	font-size: 24px;
  	color: #000000;
  }
  #topics-search-box{
  	width: 282px;
  	height: 35px;
  	line-height: 35px;

  	background: #E8E2E2;
  	border-radius: 8px;
  	border-width: 0px;
  }
  #topics-search-button{
  	width: 82.65px;
  	height: 35px;
  	margin-left: -82.65px;

    background-image: url(../assets/images/search.png);
  	background-size: contain;
  	background-repeat: no-repeat;
  	background-position: center;
  	border-radius: 0px 30px 30px 0px;
  	cursor: pointer;
  }
  .latest-topics-list{
  	display: flex;
    flex-direction: column;
  }
  .l-t-item{
  	margin-top: 5px;
    text-align: left;
    padding: 20px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	cursor: pointer;
  }
  .l-t-i-title{
  	display: block;
  	position: relative;
  	font-size: 24px;
  	line-height: 24px;
  	color: #000000;

    /*实现超过一行的显示省略号*/
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .l-t-i-content{
  	margin-top: 10px;
    margin-bottom: 10px;
  	height: 50px;

  	font-size: 18px;
  	line-height: 25px;
  	color: #727272;

    /*实现显示两行文字后显示省略号*/
  	display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .l-t-i-date{
  	display: inline-block;
  	width: max-content;

  	font-size: 18px;
  	line-height: 18px;
  	color: #000000;
  }
  .l-t-i-time{
  	display: inline-block;
  	margin-left: 20px;
  	width: fit-content;
  	font-size: 18px;
  	line-height: 18px;
  	color: #000000;
  }
  .l-t-i-answerCount{
  	display: inline-block;
  	margin-left: 60px;
  	width: fit-content;

  	font-size: 18px;
  	line-height: 18px;

  	color: #000000;
  }
  .l-t-i-delete{
  	display: inline-block;
  	margin-left: 60px;
  	width: fit-content;

  	font-size: 18px;
  	line-height: 25px;
  	text-decoration-line: underline;

  	color: #999494;
  }
  #latest-topics-page-controller{
    display: flex;
    align-items: center;
  	margin-top: 20px;
  	font-size: 18px;
    line-height: 18px;
  }
  #l-t-last-page{
    flex-grow: 1;
  	margin-right: 60px;
    text-align: right;
    cursor: pointer;
  }
  .l-t-page-num{
    display: flex;
    justify-content: center;
    align-items: center;
  	width: 40px;
  	height:40px;
  	color: #727272;
  	border-radius: 5px;
    cursor: pointer;
  }
  .l-t-page-num:hover{
    background: rgba(114, 114, 114, 0.2);
  }
  .l-t-page-num-selected{
  	background: rgba(114, 114, 114, 0.3);
  }
  #l-t-next-page{
    flex-grow: 1;
  	margin-left: 60px;
    text-align: left;
    cursor: pointer;
  }
  #right-aside{
    display: flex;
    flex-shrink: 0;
    flex-direction: column;
  	width: 360px;
  	margin-left: 40px;
    text-align: left;
  }
  #ask-question{
  	height: 50px;

  	background: #B23535;
  	border-radius: 15px;
  	font-size: 20px;
  	line-height: 51px;
  	text-align: center;
  	color: #FFFFFF;
  	cursor: pointer;
  }
  #my-topics{
  	margin-top: 20px;
  }
  #my-topics-title{
  	height: 60px;
  	padding-left: 20px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	font-size: 24px;
  	line-height: 60px;
  	color: #B23535;
  }
  #my-topisc-msg{
  	padding: 10px 20px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
  	font-size: 14px;
  	line-height: 24px;
  	color: #000000;
  }
  #my-topisc-msg>span{
  	color: #B23535;
  }
  #my-topics-list{
  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
  }
  #m-t-latest{
  	padding-top: 10px;
  	padding-left: 20px;

  	font-size: 14px;
  	line-height: 20px;
  	color: #727272;
  }
  .right-aside-list-item{
    padding: 10px 20px;

    /*实现超过一行的显示省略号*/
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  #hottest-topics{
  	margin-top: 20px;
  }
  #hottest-topics-title{
  	height: 60px;
  	padding-left: 20px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	font-size: 24px;
  	line-height: 60px;
  	color: #B23535;
  }
  #hottest-topics-list{
  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
  }
</style>
