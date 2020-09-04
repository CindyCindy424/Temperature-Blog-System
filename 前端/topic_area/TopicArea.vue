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
        			<span class="l-t-i-content">{{item.content}}</span>
        			<span class="l-t-i-date">{{item.date}}</span>
        			<span class="l-t-i-time">{{item.time}}</span>
        			<span class="l-t-i-answerCount">{{item.answerCount}}</span>
        			<span class="l-t-i-delete">删除</span>
        		</div>
        	</div>
        	<div id="latest-topics-page-controller">
        		<div id="l-t-last-page">上一页</div>
        			<div class="l-t-page-num" v-for="item in pageNums" :key="item.pageNums">
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
              </div>
            </div>
            <div id="hottest-topics">
            	<div id="hottest-topics-title">最热话题</div>
            	<div id="hottest-topics-list">
            		<div class="h-t-list-item" v-for="item in HTTopics" :key="item.topicID">
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
					topicID: "1",
					title: "啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					answerCount: 22
				},
				{
					topicID: "1",
					title: "啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					answerCount: 22
				}
				],
        MTopics:[],
        HTopics:[],
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
  }
  #photo{
  	height: 352px;
    width: calc(870px + 57px + 363px);
    align-self: center;
  }
  #photo>img{
  	width: 100%;
    height: 100%;
  }
  #main{
    display: flex;
    justify-content: center;
  }
  #latest-topics{
  	width: 870px;
  	margin-top: 17px;
  }
  #latest-topics-title{
    display: flex;
    align-items: center;
  	height: 91px;
    padding: 0px 27px 0px 15px;

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
  	height: 130px;
  	margin-top: 5px;
    text-align: left;
    padding: 5px 9.17px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	cursor: pointer;
  }
  .l-t-i-title{
  	display: block;
  	position: relative;

  	font-family: Microsoft YaHei;
  	font-style: normal;
  	font-weight: normal;
  	font-size: 24px;
  	line-height: 34px;

  	color: #000000;
  }
  .l-t-i-content{
  	display: block;
  	margin-top: 7px;
    margin-bottom: 7px;
  	height: 50px;

  	font-size: 18px;
  	line-height: 25px;
  	color: #727272;
  }
  .l-t-i-date{
  	display: inline-block;
  	width: max-content;

  	font-size: 18px;
  	line-height: 25px;
  	color: #000000;
  }
  .l-t-i-time{
  	display: inline-block;
  	padding-left: 9.17px;
  	width: fit-content;

  	font-family: Microsoft YaHei;
  	font-style: normal;
  	font-weight: normal;
  	font-size: 18px;
  	line-height: 25px;

  	color: #000000;
  }
  .l-t-i-answerCount{
  	display: inline-block;
  	margin-left: 70px;
  	width: fit-content;

  	font-size: 18px;
  	line-height: 25px;

  	color: #000000;
  }
  .l-t-i-delete{
  	display: inline-block;
  	margin-left: 70px;
  	width: fit-content;

  	font-size: 18px;
  	line-height: 25px;
  	text-decoration-line: underline;

  	color: #999494;
  }
  #latest-topics-page-controller{
    display: flex;
    align-items: center;
  	margin-top: 22px;
  }
  #l-t-last-page{
    flex-grow: 1;
  	margin-right: 70px;
    text-align: right;
  }
  .l-t-page-num{
  	width: 37px;
  	height: 35px;

  	font-size: 18px;
  	line-height: 35px;
  	text-align: center;

  	color: #727272;
  	border-radius: 5px;
  }
  .l-t-page-num-selected{
  	background: rgba(114, 114, 114, 0.3);
  }
  #l-t-next-page{
    flex-grow: 1;
  	margin-left: 70px;
    text-align: left;
  }
  #right-aside{
    display: flex;
    flex-direction: column;
  	width: 363px;
  	margin-left: 57px;
  	margin-top: 17px;
    text-align: left;
  }
  #ask-question{
  	height: 51px;

  	background: #B23535;
  	border-radius: 15px;
  	font-size: 20px;
  	line-height: 51px;
  	text-align: center;
  	color: #FFFFFF;
  	cursor: pointer;
  }
  #my-topics{
  	margin-top: 22px;
  }
  #my-topics-title{
  	height: 54px;
  	padding-left: 13px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	font-size: 24px;
  	line-height: 54px;
  	color: #B23535;
  }
  #my-topisc-msg{
  	padding: 7px 0px 0px 34px;

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
  	padding-top: 11px;
  	padding-left: 34px;

  	font-size: 14px;
  	line-height: 20px;
  	color: #727272;
  }
  #hottest-topics{
  	margin-top: 81px;
  }
  #hottest-topics-title{
  	height: 54px;
  	padding-left: 13px;

  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  	font-size: 24px;
  	line-height: 54px;
  	color: #B23535;
  }
  #hottest-topics-list{
  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
  }
</style>
