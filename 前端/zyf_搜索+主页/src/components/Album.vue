<template>
  <div id="Album">
    <img id="banner" src="../assets/banner.jpg" />
    <div id="p-info">
      <el-avatar id="p-avatar" :size="160" :src="avatar" @error="errorHandler">
        <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
      </el-avatar>
      <div id="info">
        <span id="name">{{name}}</span>
        <br />
        <span id="sign">{{sign}}</span>
        <br />
        <span id="fans">粉丝：{{fans}}</span>
        <span id="follows">关注：{{follows}}</span>
        <div id="create-album" v-on:click="createAlbum" v-if="isUser">
          <span>创建相册</span>
        </div>
        <el-dialog title="新建相册" :visible.sync="isCreate">
          <span>相册名称：</span>
          <el-input v-model="albumName" style="width:300px"></el-input>
          <div style="height:20px"></div>
          <span>相册简介：</span>
          <el-input
            type="textarea"
            v-model="albumintro"
            style="width:350px;position:relative;top:25px"
          ></el-input>
          <div slot="footer" class="dialog-footer">
            <el-button type="primary" @click="isCreate = false;confirmCreateIn()">创 建</el-button>
            <el-button @click="isCreate = false">取 消</el-button>
          </div>
        </el-dialog>
        <br />
        <div id="del-album" v-on:click="deleteAlbum" v-bind="isDelete" v-if="isUser">
          <span ref="delete">删除相册</span>
        </div>
      </div>
    </div>
    <div id="album-list">
      <el-row>
        <el-col
          :span="8"
          v-for="(item, index) in items"
          :key="item.albumId"
          :offset="index > 0 ? 0.8 : 0"
        >
          <el-card :body-style="{padding:'0px'}" class="card">
            <i
              class="el-icon-close"
              v-if="isDelete"
              style="position:absolute; left:262px; font-size:25px"
              @click="confirmDelete(item.albumName,item.albumId)"
            ></i>
            <el-dialog title="提示" :visible.sync="isDelIn" width="30%">
              <span>您确定要删除相册：{{albumName}}吗？</span>
              <span slot="footer" class="dialog-footer">
                <el-button type="primary" @click="isDelIn = false;confirmDeleteIn()">确 定</el-button>
                <el-button @click="isDelIn = false">取 消</el-button>
              </span>
            </el-dialog>
            <router-link
              :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
            >
              <!-- <div class="image" v-for="firstPhoto in items.Photo" :key="firstPhoto.PhotoId">
              <img :src="firstPhoto.PhotoAddress"  style="cursor: pointer;" object-fit="fit" @click="setCookieForPhoto(item.AlbumId)" />
              </div>-->
              <img :src="item.firstPhoto" class="image" style="cursor: pointer;" object-fit="fit" />
            </router-link>
            <div style="padding:24px;border: 1px solid #B9B4B4;">
              <router-link
                :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
              >
                <span class="albumName" style="cursor: pointer;">{{item.AlbumName}}</span>
              </router-link>
              <br />
              <br />
              <router-link
                :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
              >
                <span class="albumPeriod" style="cursor: pointer;">{{item.AlbumTime}}</span>
              </router-link>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <div class="page-block">
        <el-pagination
          layout="prev, pager, next"
          :total="albumNum"
          :page-size="9"
          :current-page.sync="pageNum"
          @current-change="handleCurrentPage"
          @prev-click="handleCurrentPagePre"
          @next-click="handleCurrentPageNext"
          hide-on-single-page="true"
        ></el-pagination>
      </div>
    </div>
    <!-- <div id="cover" ref="cover"></div> -->

    <!-- <div id="create-album-in" v-if="isCreate">
      <div id="c-header">
        <span>创建相册</span>
      </div>
      <span id="create-album-name">相册名称：</span>
      <input id="input-album-name" type="text" ref="inputAlbumName" />
      <span id="tip">(10个字符以内)</span>
      <br />
      <span id="create-album-intro">相册简介：</span>
      <textarea cols="2" id="input-album-intro" placeholder="请输入内容" v-model="albumintro"></textarea>
      <div id="create-button" @click="confirmCreateIn">
        <span>创建</span>
      </div>
      <div id="cancel-button" @click="cancelCreateIn">
        <span>取消</span>
      </div>
    </div>-->
    <!-- <div id="delete-album-in" v-if="isDelIn">
      <div id="d-header">
        <span>删除相册</span>
      </div>
      <span id="confirm">您确定要删除相册：{{albumName}}吗？</span>
      <div id="confirm-button" @click="confirmDeleteIn">
        <span style="cursor: pointer;">确认</span>
      </div>
      <div id="cancelDelete-button" @click="cancelDeleteIn">
        <span style="cursor: pointer;">取消</span>
      </div>
    </div>-->
  </div>
</template>

<script>
export default {
  name: "Album",
  data() {
    return {
      rootUrl: "http://139.224.255.43:7779/",
      userID:"",
      currentUserID:"",
      name: "TEMP",
      avatar: "",
      sign: "心如止水。",
      fans: 233,
      follows: 233,
      albumintro: "",
      albumName: "",
      albumNum: "",
      pageNum: 1,
      deleteAlbumId: "",
      defaultUrl: "",
      items: [],
      isCreate: false,
      isDelete: false,
      isDelIn: false,
      isUser: true,
      getAjaxAlbumList: 0,
      getAjaxFirstPhoto: 0,
      getAjaxCreateAlbum: 0,
      getAjaxDeleteAlbum: 0,
      getAjaxAlbumNum: 0,
      getAjaxPersonInfo: 0,
      getAjaxFans: 0,
      getAjaxPhotos:0,
      token:
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiJjZjc0NzBjMC01OWI3LTQ0YzUtYTUzNC1kOGFlNDM2YWM2NTQiLCJleHAiOjE2MDAxNjY0NDAsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.2bbXkzImup8tVwqUfj4xhOGm-sWy8qY3x1Q2ZIg8xkI",
    };
  },
  created: function () {
    this.getAlbumNum();
    this.showAlbumList();
    this.getPersonInfo();
  },
  methods: {
    // getToken:function(){
    //   this.token=document.cookie.split(";")[0].split("=")[1];
    // },
    getFans: function () {
      this.getAjaxFans = new XMLHttpRequest();
      this.getAjaxFans.open(
        "POST",
        "http://139.224.255.43:7779/Account/getFanNum?nick_name=www",
        true
      );
      this.getAjaxFans.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxFans.onreadystatechange = this.getFANS;
      this.getAjaxFans.send();
    },
    getFANS: function () {
      if (this.getAjaxFans.readyState == 4 && this.getAjaxFans.status == 200) {
        this.fans = JSON.parse(this.getAjaxFans.responseText).fanCnt;
      }
    },
    getPersonInfo: function () {
      this.getAjaxPersonInfo = new XMLHttpRequest();
      this.getAjaxPersonInfo.open(
        "POST",
        "http://139.224.255.43:7779/Account/getUserInfoByID?user_id=3",
        true
      );
      this.getAjaxPersonInfo.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxPersonInfo.onreadystatechange = this.getPI;
      this.getAjaxPersonInfo.send();
    },
    getPI: function () {
      if (
        this.getAjaxPersonInfo.readyState == 4 &&
        this.getAjaxPersonInfo.status == 200
      ) {
        var receive = JSON.parse(this.getAjaxPersonInfo.responseText).userInfo;
        this.name = receive.nickName;
        this.sign =
          JSON.parse(this.getAjaxPersonInfo.responseText).userAnnouncement +
          "（测试）";
        this.follows = receive.followNum;
        this.avatar = this.rootUrl + receive.avatr;
        this.getFans();
      }
    },
    handleCurrentPage: function () {
      this.showAlbumList();
    },
    handleCurrentPagePre: function () {
      this.pageNum--;
      this.showAlbumList();
    },
    handleCurrentPageNext: function () {
      this.pageNum++;
      this.showAlbumList();
    },
    createAlbum: function () {
      this.isCreate = true;
      // this.$refs.cover.style.display = "block";
      return;
    },
    deleteAlbum: function () {
      if (this.isDelete == false) {
        this.$refs.delete.innerHTML = "取消";
        this.isDelete = true;
      } else {
        this.$refs.delete.innerHTML = "删除相册";
        this.isDelete = false;
      }
      return;
    },
    errorHandler: function () {
      return true;
    },
    confirmCreateIn: function () {
      this.getAjaxCreateAlbum = new XMLHttpRequest();
      var inputAlbumName = this.albumName;
      this.getAjaxCreateAlbum.open(
        "POST",
        "http://139.224.255.43:7779/Photo/createAlbumByID?albumIntro=" +
          this.albumintro +
          "&albumName=" +
          inputAlbumName +
          "&userID=3",
        true
      );
      this.getAjaxCreateAlbum.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxCreateAlbum.onreadystatechange = this.getCA;
      this.getAjaxCreateAlbum.send();
      this.isCreate = false;
      // this.$refs.cover.style.display = "none";

      return;
    },
    getCA: function () {
      if (
        this.getAjaxCreateAlbum.readyState == 4 &&
        this.getAjaxCreateAlbum.status == 200
      ) {
        this.getAlbumNum();
        this.showAlbumList();
      }
    },
    getAlbumNum: function () {
      this.getAjaxAlbumNum = new XMLHttpRequest();
      this.getAjaxAlbumNum.open(
        "POST",
        "http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=3&pageNum=1&pageSize=9999",
        true
      );
      this.getAjaxAlbumNum.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxAlbumNum.onreadystatechange = this.getAN;
      // this.getAjaxAlbumList.onreadystatechange = this.getAL;
      this.getAjaxAlbumNum.send();
    },
    showAlbumList: function () {
      this.getAjaxAlbumList = new XMLHttpRequest();
      this.getAjaxAlbumList.open(
        "POST",
        "http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=3&pageNum=" +
          this.pageNum +
          "&pageSize=9",
        true
      );
      this.getAjaxAlbumList.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxAlbumList.onreadystatechange = this.getAL;
      this.getAjaxAlbumList.send();
    },
    getAN: function () {
      if (
        this.getAjaxAlbumNum.readyState == 4 &&
        this.getAjaxAlbumNum.status == 200
      ) {
        var receive = JSON.parse(
          JSON.parse(this.getAjaxAlbumNum.responseText).albums
        ); // eslint-disable-line no-unused-vars
        this.albumNum = receive.length;
      }
    },
    getAL: function () {
      if (
        this.getAjaxAlbumList.readyState == 4 &&
        this.getAjaxAlbumList.status == 200
      ) {
        this.items = JSON.parse(
          JSON.parse(this.getAjaxAlbumList.responseText).albums
        ); // eslint-disable-line no-unused-vars
        var receive=JSON.parse(this.getAjaxAlbumList.responseText).firstPhoto;
                  console.log(receive)

        for (var i = 0; i < this.items.length; i++) {

          this.items[i].AlbumTime = this.items[i].AlbumTime.split("T")[0];
          if(receive[i]!="#")
          this.items[i].firstPhoto = this.rootUrl + receive[i];
          
        }
      }
    },

    //   ///////////////////////////////////////////////////////////////////
    // getPhotoList: function (id) {
    //   console.log(i)
    //   this.getAjaxPhotos = new XMLHttpRequest();
    //   this.getAjaxPhotos.open(
    //     "POST",
    //     "http://139.224.255.43:7779/Photo/getAllPhotoByPage?albumID=" +
    //       id +
    //       "&userID=3&pageNum=1&pageSize=9999",
    //     true
    //   );
    //   this.getAjaxPhotos.setRequestHeader(
    //     "Authorization",
    //     "Bearer " + this.token
    //   );
    //   this.getAjaxPhotos.onreadystatechange = this.getPL();
    //   this.getAjaxPhotos.send();
    // },
    // getPL: function () {
    //   console.log(this.getAjaxPhotos.readyState)
    //   if (
    //     this.getAjaxPhotos.readyState == 4 &&
    //     this.getAjaxPhotos.status == 200
    //   ) {
    //     var receive = JSON.parse(
    //       JSON.parse(this.getAjaxPhotos.responseText).photos
    //     ); // eslint-disable-line no-unused-vars
    //     console.log(receive)
    //     this.items[i].firstPhoto = this.rootUrl + receive[0].PhotoAddress;
    //     console.log(receive[0].PhotoAddress)
    //     }
    //   },
     ///////////////////////////////////////////////////////////////
         // this.
       // }
        //this.items[0].url = this.rootUrl+this.getFirstPhoto(this.items[1].AlbumId);
        //console.log(this.items[0].url);
        //  for (var i = 0; i < this.items.length; i++) {
        // this.items[i].url = this.getFirstPhoto(this.items[i].AlbumId);
        //console.log(this.getFirstPhoto(12));
        //}
     // }
    //},
    // cancelCreateIn: function () {
    //   this.isCreate = false;
    //   this.$refs.cover.style.display = "none";
    //   return;
    // },
    confirmDelete: function (aname, aid) {
      this.albumName = aname;
      this.deleteAlbumId = aid;
      //console.log(aid);
      //console.log(aname);
      this.isDelIn = true;
      // this.$refs.cover.style.display = "block";
      return;
    },
    confirmDeleteIn: function () {
      this.getAjaxDeleteAlbum = new XMLHttpRequest();
      this.getAjaxDeleteAlbum.open(
        "POST",
        "http://139.224.255.43:7779/Photo/deleteAlbumByID?albumID=" +
          this.deleteAlbumId +
          "&userID=3",
        true
      );
      this.getAjaxDeleteAlbum.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxDeleteAlbum.onreadystatechange = this.getDA;
      this.getAjaxDeleteAlbum.send();
      this.isDelIn = false;
      // this.$refs.cover.style.display = "none";
      return;
    },
    getDA: function () {
      if (
        this.getAjaxDeleteAlbum.readyState == 4 &&
        this.getAjaxDeleteAlbum.status == 200
      ) {
        //????????????????????????????????
        this.getAlbumNum();
        this.showAlbumList();
        return;
      }
    },
  },
    // cancelDeleteIn: function () {
    //   this.isDelIn = false;
    //   this.$refs.cover.style.display = "none";
    //   return;
    //},
 // },
};
</script>

<style>
* {
  margin: 0 auto;
}
#Album {
  position: absolute;
  background-color: darkgray;
  width: 100%;
  height: 3000px;
}
#banner {
  height: 400px;
}
#p-info {
  position: relative;
  width: 1440px;
  height: 260px;
  top: 30px;

  background: #ffffff;
}
#p-avatar {
  position: absolute;
  left: 90px;
  top: 40px;
}
#info {
  position: absolute;
  width: 431px;
  height: 152px;
  left: 332px;
  top: 70px;

  font-family: Roboto;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 21px;
  border-block: 20px;
  color: #000000;
}
#name {
  text-align: left;
  display: block;
}
#sign {
  text-align: left;
  display: block;
}
#fans {
  text-align: left;
  display: block;
}
#follows {
  position: relative;
  float: right;
  bottom: 20px;
  right: 200px;
}
#create-album {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 562px;
  top: 0;
  cursor: pointer;

  background: #ba2222;
  border: 1px solid #9b0a0a;
  box-sizing: border-box;
  border-radius: 6px;
}
#create-album > span {
  position: absolute;
  width: 244px;
  height: 21px;
  left: 12px;
  top: 8px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  text-align: center;

  color: #f0e8e8;
}
#del-album {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 562px;
  top: 80px;
  cursor: pointer;

  background: #f2dede;
  border: 1px solid #c79e9e;
  box-sizing: border-box;
  border-radius: 6px;
}
#del-album > span {
  position: absolute;
  width: 142px;
  height: 24px;
  left: 60px;
  top: 6px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  /* identical to box height */

  text-align: center;

  color: #000000;
}
#album-list {
  position: relative;
  width: 1440px;
  height: 1381px;
  top: 80px;

  background: #ffffff;
}
/* 增加相册
#add-album {
  position: absolute;
  width: 278px;
  height: 314px;
  left: 152px;
  top: 1140px;

  background: #f6f0f0;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}
#vertical {
  position: absolute;
  width: 24px;
  height: 112px;
  left: 128px;
  top: 110px;

  background: #c4c4c4;
}
#transverse {
  position: absolute;
  width: 24px;
  height: 112px;
  left: 128px;
  top: 110px;

  background: #c4c4c4;
  transform: rotate(-90deg);
}*/
.card {
  position: relative;
  width: 288px;
  height: 354px;
  top: 50px;
  margin-bottom: 70px;
  margin-left: 95px;
}
.image {
  height: 248px;
  width: 100%;
  display: block;
}

#create-album-in {
  position: relative;
  width: 875px;
  height: 504.65px;
  top: -1300px;
  z-index: 3;
  border: 2px solid #b9b4b4;
  box-shadow: 0px 0px 4px 2px darkgray;

  background: #f0eeee;
}
#c-header {
  width: 875px;
  height: 66.89px;
  left: 299px;
  top: 246px;

  background: #ba2222;
}
#c-header > span {
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
#create-album-name {
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
#create-album-intro {
  position: absolute;
  width: 213px;
  height: 46px;
  left: 105px;
  top: 195px;

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
#input-album-name {
  position: absolute;
  width: 333px;
  height: 48px;
  left: 235px;
  top: 95px;
  font-size: 20px;

  background: #fffefe;
  border: 1px solid #d1cfcf;
  box-sizing: border-box;
}
#input-album-intro {
  position: absolute;
  width: 383px;
  height: 148px;
  left: 230px;
  top: 200px;
  font-size: 20px;

  background: #fffefe;
  border: 1px solid #d1cfcf;
  box-sizing: border-box;
}
#tip {
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
#create-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 550px;
  top: 450px;
  cursor: pointer;

  background: #ba2222;
}
#create-button > span {
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
  top: 450px;
  cursor: pointer;

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
/* #cover {
  background: #000;
  position: absolute;
  top: 0;
  height: 100%;
  width: 100%;
  filter: alpha(opacity=30);
  opacity: 0.3;
  display: none;
  z-index: 2;
} */
.el-icon-close:hover {
  color: #dd4d4d;
}
#delete-album-in {
  position: relative;
  width: 875px;
  height: 254.65px;
  top: -1300px;
  z-index: 3;
  border: 2px solid #b9b4b4;
  box-shadow: 0px 0px 4px 2px darkgray;
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
  left: 270px;
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
#cancelDelete-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 710px;
  top: 200px;

  background: #e5d4d4;
}
#cancelDelete-button > span {
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
.page-block {
  position: relative;
  top: 20px;
}
</style>