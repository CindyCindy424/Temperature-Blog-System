<template>
  <div id="create-album-in">
    <div id="c-header">
      <span>创建相册</span>
    </div>
    <span id="create-album-name">相册名称：</span>
    <input id="input-album-name" type="text" />
    <span id="tip">(10个字符以内)</span>
    <div id="create-button" @click="createAlbum">
      <span>创建</span>
    </div>
    <div id="cancel-button" @click="cancelCreate">
      <span>取消</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "createAlbum",
  methods: {
    createAlbum: function () {
      return;
    },
    cancelCreate: function () {
      return;
    },
  },
};
</script>

<style>
#create-album-in{
    position: absolute;
width: 875px;
height: 254.65px;
left: 299px;
top: 2000px;

background: #F0EEEE;
}
#c-header{
width: 875px;
height: 66.89px;
left: 299px;
top: 246px;

background: #BA2222;
}
#c-header>span{
    position: absolute;
width: 222px;
height: 21.14px;
left:30px;
top:20px;

font-family: Microsoft YaHei;
font-style: normal;
font-weight: bold;
font-size: 36px;
line-height: 48px;
display: flex;
align-items: center;
text-align: center;

color: #F2EFEF;

}
#create-album-name{
    position: absolute;
width: 213px;
height: 46px;
left: 105px;
top: 95px;

font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
font-size: 24px;
line-height: 32px;
display: flex;
align-items: center;
text-align: center;

color: #000000;
}
#input-album-name{
    position: absolute;
width: 333px;
height: 48px;
left: 235px;
top: 95px;

background: #FFFEFE;
border: 1px solid #D1CFCF;
box-sizing: border-box;
}
#tip{
    position: absolute;
width: 236px;
height: 51px;
left: 590px;
top: 95px;

font-family: Microsoft YaHei;
font-style: normal;
font-weight: 290;
font-size: 18px;
line-height: 24px;
display: flex;
align-items: center;

color: #000000;

}
#create-button{
    position: absolute;
width: 137px;
height: 33px;
left: 550px;
top: 200px;

background: #BA2222;

}
#create-button>span{
    position: absolute;
width: 96px;
height: 20px;
left:48px;
top: 5px;

font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
font-size: 18px;
line-height: 24px;
display: flex;
align-items: center;
text-align: center;

color: #FFFCFC;

}
#cancel-button{
    position: absolute;
width: 137px;
height: 33px;
left: 710px;
top: 200px;

background: #E5D4D4;

}
#cancel-button>span{
    position: absolute;
width: 96px;
height: 20px;
left:48px;
top: 5px;

font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
font-size: 18px;
line-height: 24px;
display: flex;
align-items: center;
text-align: center;

color: #535151;
}
</style>