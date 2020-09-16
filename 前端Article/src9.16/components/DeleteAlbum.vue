<template>
  <div id="delete-album">
    <div id="d-header">
      <span>删除相册</span>
    </div>
    <span id="confirm">您确定要删除相册：{{albumName}}吗？</span>
    <div id="confirm-button" @click="confirm">
      <span>确认</span>
    </div>
    <div id="cancel-button" @click="cancel">
      <span>取消</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "deleteAlbum",
  methods: {
    confirm: function () {
      return;
    },
    cancel: function () {
      return;
    },
  },
  data() {
    return {
      albumName: "成长相册",
    };
  },
};
</script>

<style>
#delete-album {
  position: absolute;
  width: 875px;
  height: 254.65px;
  left: 299px;
  top: 500px;

  background: #f0eeee;
}
#d-header {
  width: 875px;
  height: 66.89px;
  left: 299px;
  top: 246px;

  background: #ba2222;
}
#d-header > span {
  position: absolute;
  width: 222px;
  height: 21.14px;
  left: 30px;
  top: 20px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 36px;
  line-height: 48px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #f2efef;
}
#confirm {
  position: absolute;
  left: 300px;
  top: 135px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 18px;
  line-height: 24px;
  display: flex;
  align-items: center;

  color: #000000;
}
#confirm-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 550px;
  top: 200px;

  background: #ba2222;
}
#confirm-button > span {
  position: absolute;
  width: 96px;
  height: 20px;
  left: 48px;
  top: 5px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #fffcfc;
}
#cancel-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 710px;
  top: 200px;

  background: #e5d4d4;
}
#cancel-button > span {
  position: absolute;
  width: 96px;
  height: 20px;
  left: 48px;
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