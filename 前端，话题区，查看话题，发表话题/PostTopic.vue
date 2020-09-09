<template>
  <div id="post-topic-page">
    <div id="photo-div">
        <img src="../assets/images/banner.png" />
    </div>
    <div id="my-question">我的提问</div>
    <div id="panel">
      <div id="editor">
        <quill-editor ref="text" class="myQuillEditor" />
      </div>
      <div id="add-tag">
        <div id="add-tag-text">添加tag：</div>
        <input id="add-tag-input" type="text"></input>
      </div>
      <div id="buttons">
        <div id="post">发表</div>
        <div id="cancle">取消</div>
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
    overflow: hidden;
  }
  #photo-div>img{
    width: var(--page-content-width);
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
  #add-tag{
    margin-top: 20px;
    display: flex;
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
