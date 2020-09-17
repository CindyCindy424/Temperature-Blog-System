<template>
  <div id="yyw-Album">
    <img id="yyw-banner" src="../assets/images/newbanner.jpg" />
    <div id="yyw-p-info">
      <el-avatar id="yyw-p-avatar" :size="160" :src="avatar" @error="errorHandler">
        <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
      </el-avatar>
      <div id="yyw-info">
        <span id="yyw-name">{{name}}</span>
        <br />
        <span id="yyw-sign">{{sign}}</span>
        <br />
        <span id="yyw-fans">粉丝：{{fans}}</span>
        <span id="yyw-follows">关注：{{follows}}</span>
        <div id="yyw-create-album" v-on:click="createAlbum" v-if="isUser">
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
        <div id="yyw-del-album" v-on:click="deleteAlbum" v-bind="isDelete" v-if="isUser">
          <span ref="delete">删除相册</span>
        </div>
      </div>
    </div>
    <div id="yyw-album-list">
      <el-row>
        <el-col
          :span="8"
          v-for="(item, index) in items"
          :key="item.AlbumId"
          :offset="index > 0 ? 0.8 : 0"
        >
          <img
            src="../assets/images/close.png"
            v-if="isDelete"
            style="position:relative; left:142px; top:68px; width:20px; height:20px; z-index:3; cursor:pointer"
            @click="confirmDelete(item.AlbumName,item.AlbumId)"
          />
          <el-dialog title="提示" :visible.sync="isDelIn" width="30%">
            <span>您确定要删除相册：{{albumName}}吗？</span>
            <span slot="footer" class="dialog-footer">
              <el-button type="primary" @click="isDelIn = false;confirmDeleteIn()">确 定</el-button>
              <el-button @click="isDelIn = false">取 消</el-button>
            </span>
          </el-dialog>
          <el-card :body-style="{padding:'0px'}" class="yyw-card" style="margin-bottom:40px">
            <router-link
              :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
            >
              <!-- <div class="image" v-for="firstPhoto in items.Photo" :key="firstPhoto.PhotoId">
              <img :src="firstPhoto.PhotoAddress"  style="cursor: pointer;" object-fit="fit" @click="setCookieForPhoto(item.AlbumId)" />
              </div>-->
              <img :src="item.firstPhoto" class="yyw-image" style="cursor: pointer;" object-fit="fit" />
            </router-link>
            <div style="padding:24px;border: 1px solid #B9B4B4;">
              <router-link
                :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
              >
                <span class="yyw-albumName" style="cursor: pointer;">{{item.AlbumName}}</span>
              </router-link>
              <br />
              <br />
              <router-link
                :to="{path:'/Photo',query:{fromAlbum:item.AlbumId,fromAlbumName:item.AlbumName,fromAlbumIntro:item.AlbumIntroduction,fromAlbumTime:item.AlbumTime,fromAlbumCover:item.firstPhoto}}"
              >
                <span class="yyw-albumPeriod" style="cursor: pointer;">{{item.AlbumTime}}</span>
              </router-link>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <div class="yyw-page-block" style="margin-top:20px">
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
     <p id="yyw-footer">Copyright ©2020 Temperature team, All Rights Reserved.</p>
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
  name: 'Album',
  data () {
    return {
      rootUrl: 'http://139.224.255.43:7779/',
      userID: '',
      currentUserID: '',
      name: 'TEMP',
      avatar: '',
      sign: '心如止水。',
      fans: 233,
      follows: 233,
      albumintro: '',
      albumName: '',
      albumNum: '',
      pageNum: 1,
      deleteAlbumId: '',
      defaultUrl: '',
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
      getAjaxPhotos: 0,
      token: ''
    }
  },
  created: function () {
    this.getToken()
    this.getQuery()
    this.ifUser()
    this.getAlbumNum()
    this.showAlbumList()
    this.getPersonInfo()
  },
  methods: {
    ifUser: function () {
      if (this.userId === this.currentUserID) { this.isUser = true } else { this.isUser = false }
    },
    getToken: function () {
      this.token = document.cookie.split(';')[0].split('=')[1]
    },
    getQuery: function () {
      this.userID = this.$route.query.userID
      this.currentUserID = this.$route.query.currentUserID
    },
    getFans: function () {
      this.getAjaxFans = new XMLHttpRequest()
      this.getAjaxFans.open(
        'POST',
        'http://139.224.255.43:7779/Account/getFanNum?nick_name=www',
        true
      )
      this.getAjaxFans.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxFans.onreadystatechange = this.getFANS
      this.getAjaxFans.send()
    },
    getFANS: function () {
      if (this.getAjaxFans.readyState === 4 && this.getAjaxFans.status === 200) {
        this.fans = JSON.parse(this.getAjaxFans.responseText).fanCnt
      }
    },
    getPersonInfo: function () {
      this.getAjaxPersonInfo = new XMLHttpRequest()
      this.getAjaxPersonInfo.open(
        'POST',
        'http://139.224.255.43:7779/Account/getUserInfoByID?user_id=3',
        true
      )
      this.getAjaxPersonInfo.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxPersonInfo.onreadystatechange = this.getPI
      this.getAjaxPersonInfo.send()
    },
    getPI: function () {
      if (
        this.getAjaxPersonInfo.readyState === 4 &&
        this.getAjaxPersonInfo.status === 200
      ) {
        var receive = JSON.parse(this.getAjaxPersonInfo.responseText).userInfo
        this.name = receive.nickName
        this.sign =
          JSON.parse(this.getAjaxPersonInfo.responseText).userAnnouncement +
          '（测试）'
        this.follows = receive.followNum
        this.avatar = this.rootUrl + receive.avatr
        this.getFans()
      }
    },
    handleCurrentPage: function () {
      this.showAlbumList()
    },
    handleCurrentPagePre: function () {
      this.pageNum--
      this.showAlbumList()
    },
    handleCurrentPageNext: function () {
      this.pageNum++
      this.showAlbumList()
    },
    createAlbum: function () {
      this.isCreate = true
      // this.$refs.cover.style.display = "block";
    },
    deleteAlbum: function () {
      if (this.isDelete === false) {
        this.$refs.delete.innerHTML = '取消'
        this.isDelete = true
      } else {
        this.$refs.delete.innerHTML = '删除相册'
        this.isDelete = false
      }
    },
    errorHandler: function () {
      return true
    },
    confirmCreateIn: function () {
      this.getAjaxCreateAlbum = new XMLHttpRequest()
      var inputAlbumName = this.albumName
      this.getAjaxCreateAlbum.open(
        'POST',
        'http://139.224.255.43:7779/Photo/createAlbumByID?albumIntro=' +
          this.albumintro +
          '&albumName=' +
          inputAlbumName +
          '&userID=' + this.userID,
        true
      )
      this.getAjaxCreateAlbum.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxCreateAlbum.onreadystatechange = this.getCA
      this.getAjaxCreateAlbum.send()
      this.isCreate = false
      // this.$refs.cover.style.display = "none";
    },
    getCA: function () {
      if (
        this.getAjaxCreateAlbum.readyState === 4 &&
        this.getAjaxCreateAlbum.status === 200
      ) {
        this.getAlbumNum()
        this.showAlbumList()
      }
    },
    getAlbumNum: function () {
      this.getAjaxAlbumNum = new XMLHttpRequest()
      this.getAjaxAlbumNum.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=' + this.userID + '&pageNum=1&pageSize=9999',
        true
      )
      this.getAjaxAlbumNum.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxAlbumNum.onreadystatechange = this.getAN
      // this.getAjaxAlbumList.onreadystatechange = this.getAL;
      this.getAjaxAlbumNum.send()
    },
    showAlbumList: function () {
      this.getAjaxAlbumList = new XMLHttpRequest()
      this.getAjaxAlbumList.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=' + this.userID + '&pageNum=' +
          this.pageNum +
          '&pageSize=9',
        true
      )
      this.getAjaxAlbumList.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxAlbumList.onreadystatechange = this.getAL
      this.getAjaxAlbumList.send()
    },
    getAN: function () {
      if (
        this.getAjaxAlbumNum.readyState === 4 &&
        this.getAjaxAlbumNum.status === 200
      ) {
        var receive = JSON.parse(
          JSON.parse(this.getAjaxAlbumNum.responseText).albums
        ) // eslint-disable-line no-unused-vars
        this.albumNum = receive.length
      }
    },
    getAL: function () {
      if (
        this.getAjaxAlbumList.readyState === 4 &&
        this.getAjaxAlbumList.status === 200
      ) {
        this.items = JSON.parse(
          JSON.parse(this.getAjaxAlbumList.responseText).albums
        ) // eslint-disable-line no-unused-vars
        var receive = JSON.parse(this.getAjaxAlbumList.responseText).firstPhoto

        for (var i = 0; i < this.items.length; i++) {
          this.items[i].AlbumTime = this.items[i].AlbumTime.split('T')[0]
          if (receive[i] !== '#') { this.items[i].firstPhoto = this.rootUrl + receive[i] }
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
    /// ////////////////////////////////////////////////////////////
    // this.
    // }
    // this.items[0].url = this.rootUrl+this.getFirstPhoto(this.items[1].AlbumId);
    // console.log(this.items[0].url);
    //  for (var i = 0; i < this.items.length; i++) {
    // this.items[i].url = this.getFirstPhoto(this.items[i].AlbumId);
    // console.log(this.getFirstPhoto(12));
    // }
    // }
    // },
    // cancelCreateIn: function () {
    //   this.isCreate = false;
    //   this.$refs.cover.style.display = "none";
    //   return;
    // },
    confirmDelete: function (aname, aid) {
      this.albumName = aname
      this.deleteAlbumId = aid
      // console.log(aid);
      // console.log(aname);
      this.isDelIn = true
      // this.$refs.cover.style.display = "block";
    },
    confirmDeleteIn: function () {
      this.getAjaxDeleteAlbum = new XMLHttpRequest()
      this.getAjaxDeleteAlbum.open(
        'POST',
        'http://139.224.255.43:7779/Photo/deleteAlbumByID?albumID=' +
          this.deleteAlbumId +
          '&userID=' + this.userID,
        true
      )
      this.getAjaxDeleteAlbum.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxDeleteAlbum.onreadystatechange = this.getDA
      this.getAjaxDeleteAlbum.send()
      this.isDelIn = false
      // this.$refs.cover.style.display = "none";
    },
    getDA: function () {
      if (
        this.getAjaxDeleteAlbum.readyState === 4 &&
        this.getAjaxDeleteAlbum.status === 200
      ) {
        // ????????????????????????????????
        this.getAlbumNum()
        this.showAlbumList()
      }
    }
  }
  // cancelDeleteIn: function () {
  //   this.isDelIn = false;
  //   this.$refs.cover.style.display = "none";
  //   return;
  // },
  // },
}
</script>

<style>
/* * {
  margin: 0 auto;
} */
#yyw-Album {
  position: relative;
  background-color: darkgray;
  width: 100%;
  height: 2300px;
}
#yyw-banner {
  height: 400px;
  position: relative;
  left:-10px;
}
#yyw-p-info {
  margin: 0 auto;
  position: relative;
  width: 1440px;
  height: 260px;
  top: 30px;

  background: #ffffff;
}
#yyw-p-avatar {
  position: absolute;
  left: 90px;
  top: 40px;
}
#yyw-info {
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
#yyw-name {
  text-align: left;
  display: block;
}
#yyw-sign {
  text-align: left;
  display: block;
}
#yyw-fans {
  text-align: left;
  display: block;
}
#yyw-follows {
  position: relative;
  float: right;
  bottom: 20px;
  right: 200px;
}
#yyw-create-album {
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
#yyw-create-album > span {
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
#yyw-del-album {
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
#yyw-del-album > span {
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
#yyw-album-list {
  margin: 0 auto;
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
.yyw-card {
  position: relative;
  width: 288px;
  height: 354px;
  top: 50px;
  margin-bottom: 70px;
  margin-left: 95px;
}
.yyw-image {
  height: 248px;
  width: 100%;
  display: block;
}

.yyw-page-block {
  position: relative;
  top: 20px;
}
#yyw-footer{
  margin: 0 auto;
  position:relative;
  top:170px;

}
</style>
