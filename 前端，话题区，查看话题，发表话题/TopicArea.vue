<template>
  <div id="topic-area-page" :class="{loading:isLoading}">
    <div id="photo">
      <img src="../assets/images/banner.png" />
    </div>
    <div id="main">
      <div id="latest-topics">
        <div id="latest-topics-title">
          <span>最新话题</span>
          <div id="LT-zone-select-div">
          <select  v-model="zoneID">
            <option value="1">学习</option>
            <option value="2">生活</option>
            <option value="3">开发</option>
            <option value="4">游戏</option>
            <option value="5">娱乐</option>
            <option value="6">体育</option>
            <option value="7">影视</option>
            <option value="8">资讯</option>
            <option value="9">时尚</option>
            <option value="10">舞蹈</option>
            <option value="11">音乐</option>
            <option value="12">其他</option>
          </select>
          </div>
          <input id="topics-search-box" type="text" placeholder="搜索话题" v-model="LTSearchInput"/>
          <div id="topics-search-button" v-on:click="MTSearch"></div>
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
          <div :class="{'l-t-page-num':true,'l-t-page-num-selected':item.page==currentPage}" v-for="item in pageNums" v-on:click="turnToPage(item.page)">
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
              我发起了<span>&nbsp;{{myTopicsNum}}&nbsp;</span>个话题，<br />
              收获了<span>&nbsp;{{myTopicsAnsNum}}&nbsp;</span>个回答
            </div>
            <div id="my-topics-list">
              <div v-for="item in MTopics" class="right-aside-list-item MT-list-item">
                <router-link :to="{path:'/ViewTopic', query:{topicID:item.TopicId,userID:myID}}">{{item.TopicTitle}}</router-link>
                <span v-on:click="deleteTopic(item.TopicId)">删除</span>
              </div>
            </div>
            <div id="my-topic-controller">
              <div id="MT-last-page" v-on:click="MTLastPage">上一页</div>
              <div id="MT-next-page" v-on:click="MTNextPage">下一页</div>
            </div>
          </div>
          <div id="hottest-topics">
            <div id="hottest-topics-title">最热话题</div>
            <div id="hottest-topics-list">
              <div v-for="item in HTopics" class="right-aside-list-item">
                <router-link :to="{path:'/ViewTopic', query:{topicID:item.TopicId,userID:myID}}">{{item.TopicTitle}}</router-link>
              </div>
            </div>
          </div>
      </div>
    </div>
    <div class="loading-mask" v-show="isLoading"></div>
	</div>
</template>

<script>
  export default{
    name: 'TopicArea',
		data(){
			return{
        zoneID: 0,
        LTSearchInput: "",
        LTopicSum: 0,
        LTopicPageSum: 1,
        LTopicCountPerPage: 8,
				pageNums:[],

				currentPage: 1,
				currentPageLTopics: [],

        MTcurrentPage: 1,
        MTopicCountPerPage: 5,
				myTopicsNum: 0,
				myTopicsAnsNum: 0,
        MTNum: 8,
        MTopics:[],

        HTNum: 8,
        HTopics:[],

        isLoading: false,
			}
		},
    computed:{
      myID:function(){
        if(this.$route.query.userID==undefined){
          alert("缺少值：this.$route.query.userID")
        }
        return this.$route.query.userID;
      },
      loadingX:function(){
        return document.clientX;
      },
      loadingY:function(){
        return document.clientY;
      }
    },
    watch:{
      zoneID:function(){
          this.getLatestTopicsPageMessage();
      }
    },
    created:function(){
      this.getZoneID();
      this.getUserTopicNum();
      this.getAnswerNumOfUser();
      this.getHottestTopics();
    },
    methods:{
      getZoneID(){
        if(this.$route.query.zoneID==undefined){
          alert("缺少值：this.$route.query.zoneID")
        }
        else{
          this.zoneID=this.$route.query.zoneID;
          this.getLatestTopicsPageMessage();
        }
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
          //var getTopicNumberFlag=receive.getTopicNumberFlag;
          this.LTopicSum=receive.totalNumber;
          this.LTopicPageSum=Math.ceil((this.LTopicSum)*1.0/this.LTopicCountPerPage);
          this.pageNums=[];                               //加载页控制按钮
          for(var i=1;i<=this.LTopicPageSum;i++){
            this.pageNums.push({page:i});
          }
          this.getLatestTopicsList();
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
            this.getMyTopic();
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
      },
      getMyTopic(){
        var pageNum = this.MTcurrentPage;
        var pageSize = this.MTopicCountPerPage;
        var userID = this.myID;
        this.ajax_getMyTopic = new XMLHttpRequest();
        this.ajax_getMyTopic.open("POST", "http://139.224.255.43:7779/Topic/getMyTopicByPage?pageNum="+pageNum+"&pageSize="+pageSize+"&userID="+userID, true);
        this.ajax_getMyTopic.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getMyTopic.onreadystatechange = this.getMT;
        this.ajax_getMyTopic.send();
      },
      getMT(){
        if (this.ajax_getMyTopic.readyState == 4 && this.ajax_getMyTopic.status == 200) {
          var receive = JSON.parse(this.ajax_getMyTopic.responseText);
          //console.log(JSON.parse(this.ajax_getMyTopic.responseText));
          if(receive.getTopicFlag==1){
            this.MTopics=JSON.parse(receive.Result);
          }
          else{
            alert("getMyTopicByPage失败");
          }
        }
      },
      turnToPage(targetPage){
        this.currentPage=targetPage;
        this.getLatestTopicsList();
      },
      deleteTopic(topicID){
        this.ajax_deleteTopic = new XMLHttpRequest();
        this.ajax_deleteTopic.open("POST", "http://139.224.255.43:7779/Topic/deleteTopicByID?topicID="+topicID, true);
        this.ajax_deleteTopic.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_deleteTopic.onreadystatechange = this.DT;
        this.ajax_deleteTopic.send();
      },
      DT(){
        if (this.ajax_deleteTopic.readyState == 4 && this.ajax_deleteTopic.status == 200) {
          var receive = JSON.parse(this.ajax_deleteTopic.responseText);
          //console.log(receive);
          if(receive.deleteTopicAnswerFlag==0){
            this.getUserTopicNum();
            this.getAnswerNumOfUser();
            this.getMyTopic();
          }
          else{
            alert("deleteTopicByID失败");
          }
        }
      },
      MTLastPage(){
        if(this.MTcurrentPage==1){
          alert("没有上一页。");
        }
        else{
          this.MTcurrentPage--;
          this.getMyTopic();
        }
      },
      MTNextPage(){
        if(this.MTcurrentPage==Math.ceil(this.myTopicsNum*1.0/this.MTopicCountPerPage)){
          alert("没有下一页。");
        }
        else{
          this.MTcurrentPage++;
          this.getMyTopic();
        }
      },
      MTSearch(){
        //this.$router.push({path:'/组件名',query:{text:this.LTSearchInput,zoneID:this.zoneID}});
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
  .loading-mask{
    position: fixed;
    height: 100%;
    width: 100%;
    background-color: rgba(0,0,0,0.4);
    cursor: wait;
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
  #LT-zone-select-div{
    display: flex;
    flex-grow: 1;
  }
  #LT-zone-select-div>select{
    margin-left: 40px;
    padding: 5px 20px;
    font-size: 18px;
  }
  #latest-topics-title>span{
    text-align: left;
  	font-size: 24px;
  	color: #000000;
  }
  #topics-search-box{
  	width: 280px;
    padding: 2px 20px;
  	line-height: 35px;
    font-size: 14px;

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
  .right-aside-list-item>span,.right-aside-list-item>a{
    cursor: pointer;
  }
  .right-aside-list-item>span:hover,.right-aside-list-item>a:hover{
    color: #B23535;
  }
  .MT-list-item{
    display: flex;
    justify-content: space-between;
  }
  .MT-list-item>span{
    color: gray;
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
  #my-topic-controller{
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 20px;
  	background: #FFFFFF;
  	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
  }
  #my-topic-controller>div{
    cursor: pointer;
  }
</style>
