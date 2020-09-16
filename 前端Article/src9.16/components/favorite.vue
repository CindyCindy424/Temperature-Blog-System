<template>
  <div id="favorite">
    <img id="banner" src="../assets/images/banner.png" />

    <div id="left">
      <div id="search-container">
        <input id="search-info" placeholder="搜索我的收藏夹..." type="text" />
        <div id="search-button"></div>
      </div>
      <button id="add-favorite-button">
        <span id="add-favorite-text">+新建收藏夹</span>
      </button>
    </div>

    <div id="right-top">
      <span id="favorite-name">学习收藏夹</span>
      <div id="editor-button"></div>
      <div id="delete-favorite-button" @click="deleteFavorite">删除收藏夹</div>
    </div>
    <div id="cover" ref="cover"></div>
    <div id="favorite-list">
      <div class="f-item" v-for="item in favorite" :key="item.favoriteID">
        <span class="f-title">{{item.title}}</span>
        <div class="f-content">{{item.content}}</div>
        <span class="f-date">{{item.date}}</span>
        <span class="f-time">{{item.time}}</span>
        <span class="f-answerCount">回答：{{item.viewCount}}</span>
        <span class="f-delete" @click="deleteContent">删除</span>
      </div>
    </div>
    <div id="favorite-page-controller">
      <div id="f-last-page">上一页</div>
      <div class="f-page-num" v-for="item in pageNums" :key="item.id">{{item.page}}</div>
      <div id="f-next-page">下一页</div>
    </div>

    <div class="delete-favorite" v-if="isDeleteF">
      <div class="f-header">
        <span>删除收藏夹</span>
      </div>
      <span id="confirmF">您确定要删除收藏夹：{{favoriteName}}吗？</span>
      <div class="confirm-button" @click="confirmDeleteF">
        <span style="cursor: pointer;">确认</span>
      </div>
      <div class="cancelDelete-button" @click="cancelDeleteF">
        <span style="cursor: pointer;">取消</span>
      </div>
    </div>
    <div class="delete-favorite" v-if="isDeleteC">
      <div class="f-header">
        <span>删除收藏夹内容</span>
      </div>
      <span id="confirmC">您确定要将：{{favoriteTitle}}从本收藏夹删除吗？</span>
      <div class="confirm-button" @click="confirmDeleteC">
        <span style="cursor: pointer;">确认</span>
      </div>
      <div class="cancelDelete-button" @click="cancelDeleteC">
        <span style="cursor: pointer;">取消</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "favorite",
  data() {
    return {
      favoriteName: "学习收藏夹",
      favoriteTitle: "如何写3D游戏",
      isDeleteF: false,
      isDeleteC: false,
      favorite: [
        {
          favoriteID: 1,
          title: "啥都行",
          content:
            "谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的谁说的",
          date: "2020-08-22",
          time: "22:20",
          viewCount: 22,
        },
        {
          favoriteID: 2,
          title:
            "啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行啥都行",
          content: "谁说的",
          date: "2020-08-22",
          time: "22:20",
          viewCount: 22,
        },
      ],
      pageNums: [{ page: 1 }, { page: 2 }, { page: 3 }],
      selectedPage: 1,
    };
  },
  methods: {
    deleteFavorite: function () {
      this.isDeleteF = true;
      this.$refs.cover.style.display = "block";
      return;
    },
    deleteContent: function () {
      this.isDeleteC = true;
      this.$refs.cover.style.display = "block";
      return;
    },
    cancelDeleteF: function () {
      this.isDeleteF = false;
      this.$refs.cover.style.display = "none";
      return;
    },
    cancelDeleteC: function () {
      this.isDeleteC = false;
      this.$refs.cover.style.display = "none";
      return;
    },
    confirmDeleteF: function () {
      return;
    },
  },
};
</script>

<style>
#favorite {
  position: absolute;
  background-color: #c5c5c5;
  width: 100%;
  height: 3000px;
}
#left {
  position: relative;
  width: 278px;
  height: 1441px;
  left: -580px;
  top: 30px;

  background: #ffffff;
}
#search-container {
  position: absolute;
  width: 278px;
  height: 91px;
  left: 0px;
  top: 0px;

  background: #ffffff;
  border: 1px solid #c5c5c5;
  box-sizing: border-box;
}
#search-info {
  position: absolute;
  width: 226px;
  height: 33.18px;
  left: 20px;
  top: 18.01px;

  background: #e8e2e2;
  border-radius: 8px;
}
#search-button {
  position: absolute;
  width: 75.08px;
  height: 33.18px;
  left: 189.92px;
  top: 19.01px;

  background-image: url("../assets/icon/search.png");
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
  border-radius: 0px 30px 30px 0px;
}
#add-favorite-button {
  position: absolute;
  width: 278px;
  height: 87px;
  left: 0px;
  top: 91px;

  background: #ffffff;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  border: none;
}
#add-favorite-text {
  position: absolute;
  width: 152px;
  height: 32px;
  left: 60px;
  top: 27px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 24px;
  line-height: 32px;
  /* identical to box height */

  display: flex;
  align-items: center;
  letter-spacing: 0.75px;
  text-transform: uppercase;

  color: #000000;
}
#right-top {
  position: relative;
  height: 91px;
  left: 150px;
  top: -1409px;
  width: 1150px;

  background: #ffffff;
}
#favorite-name {
  position: absolute;
  width: 136px;
  height: 51px;
  left: 50px;
  top: 26px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;

  color: #000000;
}
#editor-button {
  position: absolute;
  width: 25px;
  height: 25px;
  left: 15px;
  top: 39px;

  /* Icons Color */
  background-image: url(../assets/icon/评论.jpg);
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
  border-radius: 0px 30px 30px 0px;
}
#delete-favorite-button {
  position: absolute;
  width: 90px;
  height: 24px;
  left: 1043px;
  top: 43px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 18px;
  line-height: 24px;
  /* identical to box height */

  display: flex;
  align-items: center;
  text-decoration-line: underline;
  cursor: pointer;

  color: rgba(16, 67, 247, 0.72);
}

#favorite-list {
  position: relative;
  width: 1150px;
  height: 1000px;
  left: 150px;
  top: -1400px;

  display: flex;
  flex-direction: column;
}
.f-item {
  margin-top: 5px;
  text-align: left;
  padding: 20px;
  width: 1110px;

  background: #ffffff;
  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
  cursor: pointer;
}
.f-title {
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
.f-content {
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
.f-date {
  display: inline-block;
  width: max-content;

  font-size: 18px;
  line-height: 18px;
  color: #000000;
}
.f-time {
  display: inline-block;
  margin-left: 20px;
  width: fit-content;
  font-size: 18px;
  line-height: 18px;
  color: #000000;
}
.f-answerCount {
  display: inline-block;
  margin-left: 60px;
  width: fit-content;

  font-size: 18px;
  line-height: 18px;

  color: #000000;
}
.f-delete {
  display: inline-block;
  margin-left: 60px;
  width: fit-content;

  font-size: 18px;
  line-height: 25px;
  text-decoration-line: underline;

  color: #999494;
}

#favorite-page-controller {
  position: relative;
  width: 982px;
  height: 100px;
  left: 184px;
  top: -1150px;

  display: flex;
  align-items: center;
  margin-top: 20px;
  font-size: 18px;
  line-height: 18px;
}
#f-last-page {
  flex-grow: 1;
  margin-right: 60px;
  text-align: right;
  cursor: pointer;
}
.f-page-num {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  color: #727272;
  border-radius: 5px;
  cursor: pointer;
}
.f-page-num:hover {
  background: rgba(114, 114, 114, 0.2);
}
.f-page-num-selected {
  background: rgba(114, 114, 114, 0.3);
}
#f-next-page {
  flex-grow: 1;
  margin-left: 60px;
  text-align: left;
  cursor: pointer;
}

#cover {
  background: #000;
  position: absolute;
  top: 0;
  height: 100%;
  width: 100%;
  filter: alpha(opacity=30);
  opacity: 0.3;
  display: none;
  z-index: 2;
}

.delete-favorite {
  position: relative;
  width: 875px;
  height: 254.65px;
  top: -2200px;
  z-index: 3;
  border: 2px solid #b9b4b4;
  box-shadow: 0px 0px 4px 2px darkgray;
  background: #f0eeee;
}
.f-header {
  width: 875px;
  height: 66.89px;
  left: 299px;
  top: 246px;

  background: #ba2222;
}
.f-header > span {
  position: absolute;
  width: 600px;
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
#confirmF {
  position: relative;
  left: 0;
  top: 50px;
  width: 500px;
  text-align: center;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 18px;
  line-height: 24px;
  /* display: flex; */
  align-items: center;

  color: #000000;
}
#confirmC {
  position: relative;
  left: 0;
  top: 50px;
  width: 500px;
  text-align: center;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 18px;
  line-height: 24px;
  /* display: flex; */
  align-items: center;

  color: #000000;
}
.confirm-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 550px;
  top: 200px;

  background: #ba2222;
}
.confirm-button > span {
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
.cancelDelete-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 710px;
  top: 200px;

  background: #e5d4d4;
}
.cancelDelete-button > span {
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
