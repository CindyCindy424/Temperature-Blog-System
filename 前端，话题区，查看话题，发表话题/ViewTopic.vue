<template>
  <div id="view-topic-page">
    <div id="left-aside">
      <div id="author-info">
        <div id="author-info-img-and-text">
          <img id="author-info-img" />
          <div id="author-info-text">
            <div id="author-info-name">TEMP</div>
            <div id="author-info-signature">心如止水。</div>
          </div>
        </div>
        <div id="author-info-buttons">
          <div id="follow">+关注</div>
          <div id="private-letter">私信</div>
          <div id="personel-center">个人中心</div>
        </div>
      </div>
      <div id="latest-topic" class="left-aside-topic-card">
        <div class="left-aside-subtitle">最新话题</div>
        <div class="left-aside-list">
          <div class="left-aside-list-item" v-for="item in LTopics">
            {{item.TopicTitle}}
          </div>
        </div>
      </div>
      <div id="hotest-topic" class="left-aside-topic-card">
        <div class="left-aside-subtitle">最热话题</div>
        <div class="left-aside-list">
          <div class="left-aside-list-item" v-for="item in HTopics">
            {{item.TopicTitle}}
          </div>
        </div>
      </div>
    </div>
    <div id="topic-and-answer">
      <div id="this-topic">
        <div id="topic-title">{{thisTopic.TopicTitle}}</div>
        <div id="topic-message">
          <div id="date-and-time">{{thisTopic.TopicUploadTime}}</div>
          <div id="view-count">浏览次数：233</div>
          <div id="topic-tag">tag:开发</div>
        </div>
        <div id="topic-content">{{thisTopic.TopicContent}}</div>
      </div>
      <div id="answer-count">{{Answers.length}} 个回答</div>
      <div class="answer-item" v-for="item in Answers">
        <div class="answer-author">
          <img class="answer-author-avatar" />
          <div class="answer-author-name">UserId={{item.topicInfo.UserID}}</div>
        </div>
        <div class="answer-content">{{item.topicInfo.Content}}</div>
        <div class="answer-message-time">发布于 {{item.topicInfo.UploadTime}}</div>
        <div class="answer-statistic">
          <div class="answer-statistic-comment">评论 {{item.userComments.length}} |</div>
          <div class="answer-statistic-likeit">&nbsp;点赞 {{item.topicInfo.AnswerLikes}}</div>
        </div>
        <div class="answer-comments">
          <div class="leave-comment">
            <input class="leave-comment-input" type="text" placeholder="    发表评论" />
            <div class="leave-comment-button">发表</div>
          </div>
          <div class="comment-item" v-for="com in item.userComments">
            <img class="comment-author-avatar" :src="com.avatr"></img>
            <div class="comment-author-name">{{com.userInfo.nickName}}：</div>
            <div class="comment-content">{{com.userComment.Content}}</div>
          </div>
        </div>
      </div>
      <div id="editor">
        <quill-editor ref="text" class="myQuillEditor" />

      </div>
    </div>
  </div>
</template>

<script>
  import { quillEditor } from 'vue-quill-editor'
  import 'quill/dist/quill.core.css'
  import 'quill/dist/quill.snow.css'
  import 'quill/dist/quill.bubble.css'
  export default{
    components: {
      quillEditor
    },
    data(){
      return {
        thisTopic: 0,
        Answers: [],

        LTNum: 8,
        LTopics:[],
        HTNum: 8,
        HTopics:[],
        answerUserList:[],

        ajax_getLatestTopics: 0,
        ajax_getHottestTopics: 0,
        ajax_getAnswers: 0,
        ajax_getTopicDetailByID: 0
      };
    },
    created:function(){
      this.getThisTopic();
      //this.getAnswers();
      this.getTopicDetailByID();
      this.getHottestTopics();
      this.getLatestTopics();
    },
    methods:{
      getTokenFromCookie(){
        //console.log(document.cookie);
        var cookie=document.cookie;
        var cookieArr=cookie.split(";");
        for(var i=0;i<cookieArr.length;i++){
          var keyAndValue=cookieArr[i].split("=");
          if(keyAndValue[0].trim()=="token"){
            //console.log(keyAndValue[1]);
            return keyAndValue[1];
          }
        }
      },
      getThisTopic(){
        this.thisTopic = this.$route.query.thisTopic;
        //console.log(this.thisTopic);
      },
      getAnswers(){
        var topicID = this.thisTopic.TopicId;
        this.ajax_getAnswers = new XMLHttpRequest();
        this.ajax_getAnswers.open("POST", "http://139.224.255.43:7779/Topic/getTopicCommentByID?topicID="+topicID, true);
        this.ajax_getAnswers.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getAnswers.onreadystatechange = this.getAns;
        this.ajax_getAnswers.send();
      },
      getAns(){
        if (this.ajax_getAnswers.readyState == 4 && this.ajax_getAnswers.status == 200) {
          //console.log(this.ajax_getAnswers.responseText);
          var receive = JSON.parse(JSON.parse(this.ajax_getAnswers.responseText).Result);
          this.Answers = receive;
          for(var i = 0; i < this.Answers.length; i++){
            this.Answers[i].AnswerUploadTime=this.Answers[i].AnswerUploadTime.replace("T","  ");
          }
        }
      },
      getLatestTopics(){
        var takeTopicNum = this.LTNum;
        this.ajax_getLatestTopics = new XMLHttpRequest();
        this.ajax_getLatestTopics.open("POST", "http://139.224.255.43:7779/Topic/getNewestTopic?takeTopicNum="+takeTopicNum, true);
        this.ajax_getLatestTopics.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getLatestTopics.onreadystatechange = this.getLT;
        this.ajax_getLatestTopics.send();
      },
      getLT(){
        if (this.ajax_getLatestTopics.readyState == 4 && this.ajax_getLatestTopics.status == 200) {
          var receive = JSON.parse(JSON.parse(this.ajax_getLatestTopics.responseText).topics);
          console.log(receive);
          this.LTopics=receive;
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
          //console.log(receive);
          this.HTopics=receive;
        }
      },
      getTopicDetailByID(){
        var topicID = this.thisTopic.TopicId;
        this.ajax_getTopicDetailByID = new XMLHttpRequest();
        this.ajax_getTopicDetailByID.open("POST", "http://139.224.255.43:7779/Topic/getTopicDetailByID?topicID="+topicID, true);
        this.ajax_getTopicDetailByID.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_getTopicDetailByID.onreadystatechange = this.getTD;
        this.ajax_getTopicDetailByID.send();
      },
      getTD(){
        if (this.ajax_getTopicDetailByID.readyState == 4 && this.ajax_getTopicDetailByID.status == 200) {
          var receive = JSON.parse(this.ajax_getTopicDetailByID.responseText).answerUserList;
          this.answerUserList=receive;
          //console.log(receive);
          //this.HTopics=receive;
          this.sortOutAnswerList();
        }
      },
      sortOutAnswerList(){
        var answerList=this.answerUserList;
        //console.log(answerList);
        for(var i=0;i<answerList.length;i++){
          answerList[i]=JSON.parse(answerList[i]);                      //将每个回答变成对象
          answerList[i].topicInfo=JSON.parse(answerList[i].topicInfo);  //将话题信息变成对象
          answerList[i].topicInfo.UploadTime=answerList[i].topicInfo.UploadTime.replace("T","  ");//去掉时间中的"T"
          for(var j=0;j<answerList[i].userComments.length;j++){         //将评论变成对象
            answerList[i].userComments[j]=JSON.parse(answerList[i].userComments[j]);
            answerList[i].userComments[j].userComment=JSON.parse(answerList[i].userComments[j].userComment);
            answerList[i].userComments[j].userInfo=JSON.parse(answerList[i].userComments[j].userInfo);
          }
        }
        //console.log(answerList);
        this.Answers=answerList;
      }
    }
  }
</script>

<style>
  *{
    margin: 0px;
    padding: 0px;
  }
  #view-topic-page{
    display: flex;
    justify-content: center;
    padding-top: calc(59px + 20px);
    padding: calc(59px + 20px) 20px 0px 20px;
    text-align: left;
    background-color: #E5E5E5;
    min-width: calc(880px + 40px + 360px);
  }
  #left-aside{
    display: flex;
    flex-shrink: 0;
    flex-direction: column;
    width: 300px;
  }
  #author-info{
    display: flex;
    flex-direction: column;
    background-color: white;
    padding: 20px;
  }
  #author-info-img-and-text{
    display: flex;
    align-items: center;
    height:max-content;
  }
  #author-info-img{
    width: 45px;
    height: 45px;
    background-color: #00FF00;
    border-radius: 45px;
  }
  #author-info-text{
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
    margin-left: 20px;
  }
  #author-info-name{
  }
  #author-info-signature{
    color: #767373;
  }
  #author-info-buttons{
    display: flex;
    justify-content: space-between;
    margin-top: 20px;
    height: 30px;
    font-size: 14px;
    line-height: 14px;
  }
  #author-info-buttons>div{
    display: flex;
    justify-content: center;
    align-items: center;
    width: 76px;
    border-radius: 4px;
    cursor: pointer;
  }
  #follow{
    background: #B23535;
    color: white;
  }
  #private-letter{
    background: #DAD4D4;
  }
  #personel-center{
    background: #DAD4D4;
  }
  .left-aside-topic-card{
    display: flex;
    flex-direction: column;
    margin-top: 20px;
    background: #FFFFFF;
    border-radius: 0px 20px 20px 20px;
    overflow: hidden;
  }
  .left-aside-subtitle{
    width: max-content;
    padding: 20px 30px;
    background: #B23535;
    border-radius: 0px 0px 30px 0px;
    color: white;
    line-height: 18px;
    font-weight: bold;
    font-size: 18px;
  }
  .left-aside-list{
    padding: 10px 0px;
  }
  .left-aside-list-item{
    padding: 10px 20px;
    cursor: pointer;

    /*实现超过一行的显示省略号*/
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .left-aside-list-item:hover{
    background-color: rgba(0,0,0,0.15);
  }
  #topic-and-answer{
    display: flex;
    flex-direction: column;
    width: 1000px;
    margin-left: 40px;
  }
  #this-topic{
    background-color: white;
    padding: 40px;
  }
  #topic-title{
    font-weight: bold;
    font-size: 24px;
    line-height: 24px;
    overflow: hidden;
    word-wrap: break-word;
  }
  #topic-message{
    display: flex;
    margin-top: 10px;
    color: #6A6969;
    font-size: 13px;
    line-height: 13px;
  }
  #view-count,#topic-tag{
    margin-left: 60px;
  }
  #topic-content{
    margin-top: 10px;
    overflow: hidden;
    word-wrap: break-word;
  }
  #answer-count{
    display: flex;
    align-items: center;
    height: 40px;
    margin-top: 10px;
    padding-left: 40px;
    background-color: white;
    font-size: 14px;
    line-height: 14px;
    color: #767373;
  }
  .answer-item{
    display: flex;
    flex-direction: column;
    margin-top: 10px;
    padding: 40px;
    background-color: white;
  }
  .answer-author{
    display: flex;
    align-items: center;
  }
  .answer-author-avatar{
    height: 40px;
    width: 40px;
    background-color: #0000FF;
    border-radius: 40px;
  }
  .answer-author-name{
    margin-left: 20px;
    font-size: 18px;
    line-height: 18px;
  }
  .answer-content{
    margin-top: 20px;
    overflow: hidden;
    word-wrap: break-word;
  }
  .answer-message-time{
    margin-top: 20px;
    font-size: 14px;
    line-height: 14px;
    color: #958F8F;
  }
  .answer-statistic{
    display: flex;
    margin-top: 20px ;
    font-size: 14px;
    line-height: 14px;
  }
  .answer-statistic-comment{
    cursor: pointer;
  }
  .answer-comments{
    display: flex;
    flex-direction: column;
    margin-top: 20px;
    box-shadow: inset 0px 0px 0px 1px #DAD4D4;
    padding: 20px;
  }
  .leave-comment{
    display: flex;
  }
  .leave-comment-input{
    flex-grow: 1;
    height: 28px;
    border-width: 0px;
    background-color: #F3F1F1;
    font-size: 14px;
    line-height: 28px;
  }
  .leave-comment-button{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 28px;
    padding: 0px 20px;
    background: #B23535;
    color: white;
    line-height: 16px;
    cursor: pointer;
  }
  .comment-item{
    display: flex;
    padding: 20px 0px;
    border-bottom-width: 1px ;
    border-bottom-style: solid;
    border-bottom-color: #969292;
  }
  .comment-author-avatar{
    flex-shrink: 0;
    background-color: #13CE66;
    width: 30px;
    height: 30px;
    border-radius: 30px;
  }
  .comment-author-name{
    flex-shrink: 0;
    margin-left: 20px;
    line-height: 30px;
    font-size: 14px;
  }
  .comment-content{
    margin: 6px 0px 0px 10px;
    font-size: 14px;
    color: #767373;
    line-height: 18px;
    overflow: hidden;
    word-wrap: break-word;
  }
  #editor{
    margin: 20px 0px;
    background-color: white;
  }
  .ql-editor{
       height:300px;
   }

</style>
