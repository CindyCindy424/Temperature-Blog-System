<template>
  <div id="post-topic-page">
    <div id="photo-div">
        <img src="../assets/images/banner.png" />
    </div>
    <div id="my-question">我的提问</div>
    <div id="panel">
      <div id="editor">
        <div class="subtitle">问题</div>
        <input id="title-input" v-model="title"/>
        <!--<quill-editor ref="text" class="myQuillEditor" />-->
        <div class="subtitle">问题详情</div>
        <textarea id="contentInput" v-model="content"/>
      </div>
      <div class="subtitle">分区：</div>
      <select id="zone-select" v-model="zoneID">
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
      <div id="buttons">
        <div id="post" v-on:click="post">发表</div>
        <div id="cancle" v-on:click="cancle">取消</div>
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
    data(){
      return {
        title: "",
        content: "",
        zoneID: 1,
        ajax_post: 0,
      };
    },
    computed:{
      myID:function(){
        return this.$route.query.userID;
      }
    },
    components: {
      quillEditor
    },
    methods:{
      cancle(){
        this.$router.go(-1);
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
      post(){
        var forms=new FormData();
        forms.append('content',this.content);
        forms.append('title',this.title);
        forms.append('userID',this.myID);
        forms.append('zoneID',this.zoneID);
        this.ajax_post = new XMLHttpRequest();
        this.ajax_post.open("POST", "http://139.224.255.43:7779/Topic/createTopicByID", true);
        this.ajax_post.setRequestHeader('Authorization','Bearer '+ this.getTokenFromCookie());
        this.ajax_post.onreadystatechange = this.po;
        this.ajax_post.send(forms);
      },
      po(){
        if (this.ajax_post.readyState == 4 && this.ajax_post.status == 200) {
          var receive = JSON.parse(this.ajax_post.responseText);
          console.log(receive);
          if(receive.createTopicFlag==1){
            alert("发表成功！");
            //this.$router.go(-1);
            this.$router.push({path:'/ViewTopic',query:{userID:this.myID,topicID:receive.topicDetail.topicId}});
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
  #post-topic-page{
    --page-content-width: calc(880px + 40px + 360px);
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: #E5E5E5;
    min-width: var(--page-content-width);
    text-align: left;
  }
  #photo-div{
    display: flex;
    align-self: center;
    justify-content: center;
    align-items: center;

  	height: 352px;
  	width: 100%;
    background-color: rgb(15,13,24);
  }
  #photo-div>img{
    height: 100%;
  }
  #my-question{
    width: calc(var(--page-content-width) - 40px);
    height: 100px;
    margin: 20px 20px 0px 20px;
    padding-left: 40px;
    background-color: #F9F8F8;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);

    font-weight: bold;
    font-size: 24px;
    line-height: 100px;
    color: #756F6F;
  }
  #panel{
    margin: 0px 20px;
    padding: 40px;
    margin-bottom: 20px;
    width: calc(var(--page-content-width) - 2 * 40px);
    background-color: white;
    box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);;
  }
  .ql-editor{
    height:400px;
  }
  .subtitle{
    font-size: 20px;
    font-weight: bold;
    margin: 20px 0px;
  }
  #title-input{
    width: calc(100% - 40px);
    padding: 20px;
    font-size: 16px;
    font-family: "microsoft yahei";
  }
  #contentInput{
    width: calc(100% - 40px);
    height: 400px;
    font-size: 16px;
    font-family: "microsoft yahei";
    padding: 20px;
  }
  #add-tag{
    margin-top: 20px;
    display: flex;
  }
  #zone-select{
    padding: 10px 20px;
    font-size: 16px;
    font-family: "microsoft yahei";
  }
  #buttons{
    display: flex;
    justify-content: flex-end;
    margin-top: 20px;
  }
  #post{
    width: 140px;
    height: 40px;

    background: #DA4646;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 4px;
    font-size: 18px;
    line-height: 40px;
    text-align: center;
    color: #F9F5F5;
    cursor: pointer;
  }
  #cancle{
    width: 140px;
    height: 40px;
    margin-left: 40px;

    background: #EFE7E7;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 4px;
    font-size: 18px;
    line-height: 40px;
    text-align: center;
    color: #504C4C;
    cursor: pointer;
  }

</style>
