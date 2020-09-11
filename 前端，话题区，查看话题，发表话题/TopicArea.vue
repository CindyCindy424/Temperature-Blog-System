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
        		<div class="l-t-item" v-for="item in currentPageLTopics" :key="item.TopicId">
        			<router-link :to="{path:'/ViewTopic', query:{topicID:item.TopicId,userID:myID}}" class="l-t-i-title">{{item.TopicTitle}}</router-link>
        			<router-link :to="{path:'/ViewTopic', query:{topicID:item.TopicId,userID:myID}}" class="l-t-i-content">{{item.TopicContent}}</router-link>
        			<span class="l-t-i-time">{{item.TopicUploadTime}}</span>
        			<span class="l-t-i-answerCount">回答：{{item.AnswerNum}}</span>
        		</div>
        	</div>
        	<div id="latest-topics-page-controller">
        		<div id="l-t-last-page" v-on:click="LTLastPage">上一页</div>
            <div :class="{'l-t-page-num':true,'l-t-page-num-selected':item.page==currentPage}" v-for="item in pageNums">
              {{item.page}}
            </div>
        		<div id="l-t-next-page" v-on:click="LTNextPage">下一页</div>
        	</div>
        </div>
        <div id="right-aside">
            <router-link :to="{path:'/PostTopic', query:{userID:myID}}" id="ask-question">我要提问</router-link>
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
            		<router-link v-for="item in HTopics" :to="{path:'/ViewTopic', query:{topicID:item.TopicId,userID:myID}}" class="right-aside-list-item">
                  {{item.TopicTitle}}
                </router-link>
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
        //myID: 5,
        zoneID: 1,
        LTopicSum: 0,
        LTopicPageSum: 1,
        LTopicCountPerPage: 2,
				pageNums:[],

				currentPage: 1,
				currentPageLTopics: [],

				myTopicsNum: 0,
				myTopicsAnsNum: 0,
        MTNum: 8,
        MTopics:[],
        HTNum: 8,
        HTopics:[],
        ajax_getLatestTopics: 0,
        ajax_getLTopicSum: 0,
        ajax_getHottestTopics: 0,
        ajax_getUserTopicNum: 0,
        ajax_getAnswerNumOfUser: 0,
        token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjaW5keSIsImp0aSI6IjgxZWNmNmE1LTJlM2EtNGMwZS04MDcyLWIwNjM4MDVhZjkyYiIsImV4cCI6MTU5OTgzNzU5MiwiaXNzIjoiaHR0cHM6Ly93d3cuY25ibG9ncy5jb20vY2hlbmd0aWFuIiwiYXVkIjoiaHR0cHM6Ly93d3cuY25ibG9ncy5jb20vY2hlbmd0aWFuIn0.W4DZo-UAB1MW-uo4v7bppdJwrn9JmjngBP96CXC-gZc",
			}
		},
    computed:{
      myID:function(){
        return this.$route.query.userID;
      }
    },
    created:function(){
      this.setCookie();
      this.getTokenFromCookie();
      this.getLatestTopicsPageMessage();
      this.getLatestTopicsList();
      this.getUserTopicNum();
      this.getAnswerNumOfUser();
      this.getHottestTopics();
    },
    methods:{
      setCookie(){
        document.cookie="token="+this.token+";expires=Sun, 31 Dec 2090 12:00:00 UTC";
      },
      getTokenFromCookie(){
        //console.log(document.cookie);
        var cookie=document.cookie;
        var cookieArr=cookie.split(";");
        for(var i=0;i<cookieArr.length;i++){
          var keyAndValue=cookieArr[i].split("=");
          if(keyAndValue[0].trim()=="token"){
            return keyAndValue[1];
          }
        }
      },
      getLatestTopicsList(){
        var pageNum = this.currentPage;
        var pageSize = this.LTopicCountPerPage;
        var zoneID = this.zoneID;
        this.ajax_getLatestTopics = new XMLHttpRequest();
        this.ajax_getLatestTopics.open("POST", "http://139.224.255.43:7779/Topic/getTopicByPage?pageNum="+pageNum+"&pageSize="+pageSize+"&zoneID="+zoneID, true);
        this.ajax_getLatestTopics.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getLatestTopics.onreadystatechange = this.getLT;
        this.ajax_getLatestTopics.send();
      },
      getLT(){
        if (this.ajax_getLatestTopics.readyState == 4 && this.ajax_getLatestTopics.status == 200) {
          var receive = JSON.parse(JSON.parse(this.ajax_getLatestTopics.responseText).Result);
          this.currentPageLTopics = receive;
          //console.log(receive);
          for(var i = 0; i < this.currentPageLTopics.length; i++){
            this.currentPageLTopics[i].TopicUploadTime=this.currentPageLTopics[i].TopicUploadTime.replace("T","  ");
          }
        }
      },
      getLatestTopicsPageMessage(){
        var pageNum = this.currentPage;
        var pageSize = this.LTopicCountPerPage;
        var zoneID = this.zoneID;
        this.ajax_getLTopicSum = new XMLHttpRequest();
        this.ajax_getLTopicSum.open("POST", "http://139.224.255.43:7779/Topic/getTopicNumberByZoneID?zoneID="+zoneID, true);
        this.ajax_getLTopicSum.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getLTopicSum.onreadystatechange = this.getLTS;
        this.ajax_getLTopicSum.send();
      },
      getLTS(){
        if (this.ajax_getLTopicSum.readyState == 4 && this.ajax_getLTopicSum.status == 200) {
          var receive = JSON.parse(this.ajax_getLTopicSum.responseText);
          var getTopicNumberFlag=receive.getTopicNumberFlag;
          this.LTopicSum=receive.totalNumber;
          this.LTopicPageSum=Math.ceil((this.LTopicSum)*1.0/this.LTopicCountPerPage);
          this.pageNums=[];                               //加载页控制按钮
          for(var i=1;i<=this.LTopicPageSum;i++){
            this.pageNums.push({page:i});
          }
        }
      },
      LTNextPage(){
        if(this.currentPage==this.LTopicPageSum){
          alert("没有下一页。");
        }
        else{
          this.currentPage++;
          this.getLatestTopicsList();
        }
      },
      LTLastPage(){
        if(this.currentPage==1){
          alert("没有上一页。");
        }
        else{
          this.currentPage--;
          this.getLatestTopicsList();
        }
      },
      getHottestTopics(){
        var takeTopicNum = this.HTNum;
        this.ajax_getHottestTopics = new XMLHttpRequest();
        this.ajax_getHottestTopics.open("POST", "http://139.224.255.43:7779/Topic/getHotestTopic?takeTopicNum="+takeTopicNum, true);
        this.ajax_getHottestTopics.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getHottestTopics.onreadystatechange = this.getHT;
        this.ajax_getHottestTopics.send();
      },
      getHT(){
        if (this.ajax_getHottestTopics.readyState == 4 && this.ajax_getHottestTopics.status == 200) {
          var receive = JSON.parse(JSON.parse(this.ajax_getHottestTopics.responseText).topics);
          //console.log(this.ajax_getHottestTopics.responseText);
          this.HTopics=receive;
        }
      },
      getUserTopicNum(){
        var userID = this.myID;
        this.ajax_getUserTopicNum = new XMLHttpRequest();
        this.ajax_getUserTopicNum.open("POST", "http://139.224.255.43:7779/Topic/getUserTopicNum?userID="+userID, true);
        this.ajax_getUserTopicNum.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getUserTopicNum.onreadystatechange = this.getUTN;
        this.ajax_getUserTopicNum.send();
      },
      getUTN(){
        if (this.ajax_getUserTopicNum.readyState == 4 && this.ajax_getUserTopicNum.status == 200) {
          var receive = JSON.parse(this.ajax_getUserTopicNum.responseText);
          //console.log(receive);
          if(receive.flag==1){
            this.myTopicsNum = receive.topicCount;
          }
          else{
            alert("getUserTopicNum返回错误");
          }
        }
      },
      getAnswerNumOfUser(){
        var userID = this.myID;//console.log(userID);
        this.ajax_getAnswerNumOfUser = new XMLHttpRequest();
        this.ajax_getAnswerNumOfUser.open("POST", "http://139.224.255.43:7779/Topic/getAnswerNumOfUser?userID="+userID, true);
        this.ajax_getAnswerNumOfUser.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getAnswerNumOfUser.onreadystatechange = this.getANOU;
        this.ajax_getAnswerNumOfUser.send();
      },
      getANOU(){
        if (this.ajax_getAnswerNumOfUser.readyState == 4 && this.ajax_getAnswerNumOfUser.status == 200) {
          var receive = JSON.parse(this.ajax_getAnswerNumOfUser.responseText);
          //console.log(receive);
          if(receive.flag==1){
            this.myTopicsAnsNum = receive.userTopicAnswerCount;
          }
          else{
            alert("getAnswerNumOfUser返回错误");
          }
        }
      }
    }
  }
</script>

<style>
  *{
  	margin: 0px;
  	padding: 0px;
  }
  a{
    text-decoration: none;
    color: black;
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
    background-color: rgb(15,13,24);

  	height: 352px;
  	width: 100%;
    overflow: hidden;
  }
  #photo>img{
    height: 100%;
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
  }
  .l-t-i-title{
  	display: block;
  	position: relative;
  	font-size: 24px;
  	line-height: 24px;
  	color: #000000;
    text-decoration: none;
    cursor: pointer;

    /*实现超过一行的显示省略号*/
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .l-t-i-content{
  	padding-top: 10px;
    margin-bottom: 10px;
  	height: 50px;
    text-decoration: none;

  	font-size: 18px;
  	line-height: 25px;
  	color: #727272;
    cursor: pointer;

    /*实现显示两行文字后显示省略号*/
  	display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    text-overflow: ellipsis;
    overflow: hidden;
    word-break: break-word;
  }
  .l-t-i-time{
  	display: inline-block;
  	width: max-content;

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
    text-decoration: none;
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
    display: block;
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
