<template>
  <div id="yyw-favorite">
    <img id="yyw-banner" src="../assets/images/banner.png" />

    <div id="yyw-left">
      <el-button type="text" @click="isCreateF = true">
        <button id="yyw-add-favorite-button" @click="confirmCreate">
          <span id="yyw-add-favorite-text">+新建收藏夹</span>
        </button>
      </el-button>
      <el-dialog title="新建收藏夹" :visible.sync="isCreateF">
        <span id="yyw-create-favorite-name">收藏夹名称：</span>
        <el-input id="yyw-input-favorite-name" v-model="cfavoriteName"></el-input>
        <div style="height:20px"></div>
        <div slot="footer" class="yyw-dialog-footer">
          <el-button type="primary" @click="isCreateF = false;confirmCreateIn()">创 建</el-button>
          <el-button @click="isCreateF = false">取 消</el-button>
        </div>
      </el-dialog>
      <div class="yyw-box-card" v-for="folder in favoriteList" :key="folder.favouriteId">
        <div class="yyw-folderList" @click="chooseFavorite(folder.favouriteId,folder.favouriteName)">
          <span class="yyw-folderPosition">{{folder.favouriteName}}</span>
        </div>
      </div>
    </div>

      <div id="yyw-right-top">
        <span id="yyw-favorite-name">{{favoriteName}}</span>
        <el-button id="yyw-delete-favorite-button" type="text" @click="isDeleteF=true">删除收藏夹</el-button>
        <el-dialog title="提示" :visible.sync="isDeleteF" width="30%">
          <span>您确定要删除收藏夹：{{favoriteName}}吗？</span>
          <span slot="footer" class="yyw-dialog-footer">
            <el-button type="primary" @click="isDeleteF = false;confirmDeleteF()">确 定</el-button>
            <el-button @click="isDeleteF = false">取 消</el-button>
          </span>
        </el-dialog>
      </div>

      <div id="yyw-favorite-list">
        <div class="yyw-f-item" v-for="item in favorite" :key="item.articleId">
          <router-link :to="{path:'/ViewArticle',query:{ViewArticleTitle:item.title,ViewArticleNickname:item.nickName,ViewArticleUsername:currentUserName}}"><span class="yyw-f-title">{{item.title}}</span></router-link>
          <router-link :to="{path:'/ViewArticle',query:{ViewArticleTitle:item.title,ViewArticleNickname:item.nickName,ViewArticleUsername:currentUserName}}"><span class="yyw-f-content">{{item.articleContent}}</span></router-link>
          <!-- <span class="yyw-f-title">{{item.title}}</span>
          <span class="yyw-f-content">{{item.articleContent}}</span> -->
          <span class="yyw-f-date">{{item.articleUploadTime}}</span>
          <span class="yyw-f-answerCount">
            <img class="yyw-readnum" src="../assets/images/eye(2).png" />
            {{item.readNum}}
          </span>
          <span class="yyw-f-delete" @click="isDeleteC=true;deleteContent(item.title,item.articleId)">删除</span>
          <el-dialog title="提示" :visible.sync="isDeleteC" width="30%">
            <span style="position:relative;left:70px">您确定要将：{{favoriteTitle}}从本收藏夹删除吗？</span>
            <span slot="footer" class="dialog-footer">
              <el-button type="primary" @click="isDeleteC = false;confirmDeleteC()">确 定</el-button>
              <el-button @click="isDeleteC = false">取 消</el-button>
            </span>
          </el-dialog>
        </div>
        <div class="yyw-fpage-block">
          <el-pagination
            layout="prev, pager, next"
            :total="ArticleNum"
            :page-size="10"
            :current-page.sync="pageNum"
            @current-change="handleCurrentPage"
            @prev-click="handleCurrentPagePre"
            @next-click="handleCurrentPageNext"
            hide-on-single-page="true"
          ></el-pagination>
        </div>
      </div>
    <p id="yyw-ffooter">Copyright ©2020 Temperature team, All Rights Reserved.</p>
    <!-- <div class="delete-favorite" v-if="isDeleteF">
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
    </div>-->
    <!-- <div class="delete-favorite-c" v-if="isDeleteC">
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
    </div> -->
    <!-- <div id="create-favorite-in" v-if="isCreateF">
      <div class="f-header">
        <span>创建收藏夹</span>
      </div>
      <span id="create-favorite-name">收藏夹名称：</span>
      <input id="input-favorite-name" type="text" v-model="favoriteName" />
      <div class="confirm-button" @click="confirmCreateIn">
        <span>创建</span>
      </div>
      <div class="cancelDelete-button" @click="cancelCreateIn">
        <span>取消</span>
      </div>
    </div>-->
  </div>
</template>

<script>
export default {
  name: 'favorite',
  data () {
    return {
      favoriteName: '',
      cfavoriteName: '',
      favoriteTitle: '',
      favoriteId: '',
      ArticleNum: '',
      articleId: '',
      userID: '',
      currentUserName: '',
      pageNum: 1,
      isDeleteF: false,
      isDeleteC: false,
      isCreateF: false,
      getAjaxCreateFavorite: 0,
      getFavoriteFolderList: 0,
      getAjaxFavoriteContent: 0,
      getAjaxContentNum: 0,
      getAjaxDeleteFavorite: 0,
      getAjaxDeleteContent: 0,
      favoriteList: [],
      favorite: [],
      token:
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiI3NjE2MzZmMS01ZjUzLTQwNTMtYTNjNy1kOGZmN2Y3N2ViM2UiLCJleHAiOjE2MDAzNDczMjQsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.8YbTxW9Ae_xt1d2cDTL5TpXDxYIx3VclkhYwQqw4450'
    }
  },
  created: function () {
    this.getToken()
    this.getQuery()
    this.getFavoriteContentNum()
    this.getFolderList()
    this.showFavoriteContent()
  },
  methods: {
    getToken: function () {
      this.token = document.cookie.split(';')[0].split('=')[1]
    },
    getQuery: function () {
      this.userID = this.$route.query.userID
      this.currentUserName = this.$route.query.currentUserName
    },
    chooseFavorite: function (fid, fname) {
      this.favoriteId = fid
      this.favoriteName = fname
      this.getFavoriteContentNum()
      this.showFavoriteContent()
    },
    handleCurrentPage: function () {
      this.showFavoriteContent()
    },
    handleCurrentPagePre: function () {
      this.pageNum--
      this.showFavoriteContent()
    },
    handleCurrentPageNext: function () {
      this.pageNum++
      this.showFavoriteContent()
    },
    getFavoriteContentNum: function () {
      this.getAjaxContentNum = new XMLHttpRequest()
      this.getAjaxContentNum.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/getFolderArticleInfo?folderID=' +
          this.favoriteId +
          '&pageNum=1&pageSize=99999',
        true
      )
      this.getAjaxContentNum.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxContentNum.onreadystatechange = this.getCN
      this.getAjaxContentNum.send()
    },
    getCN: function () {
      if (
        this.getAjaxContentNum.readyState === 4 &&
        this.getAjaxContentNum.status === 200
      ) {
        var receive = JSON.parse(this.getAjaxContentNum.responseText).result
        this.ArticleNum = receive.length
      }
    },
    showFavoriteContent: function () {
      this.getAjaxFavoriteContent = new XMLHttpRequest()
      this.getAjaxFavoriteContent.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/getFolderArticleInfo?folderID=' +
          this.favoriteId +
          '&pageNum=' +
          this.pageNum +
          '&pageSize=8',
        true
      )
      this.getAjaxFavoriteContent.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxFavoriteContent.onreadystatechange = this.getFC
      this.getAjaxFavoriteContent.send()
    },
    getFC: function () {
      if (
        this.getAjaxFavoriteContent.readyState === 4 &&
        this.getAjaxFavoriteContent.status === 200
      ) {
        this.favorite = JSON.parse(
          this.getAjaxFavoriteContent.responseText
        ).result
        for (var i = 0; i < this.favorite.length; i++) {
          this.favorite[i].articleUploadTime = this.favorite[i].articleUploadTime.replace('T', ' ')
        }
      }
    },
    getFolderList: function () {
      this.getFavoriteFolderList = new XMLHttpRequest()
      this.getFavoriteFolderList.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/getFolderList?userID=' + this.userID,
        true
      )
      this.getFavoriteFolderList.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getFavoriteFolderList.onreadystatechange = this.getFFL
      this.getFavoriteFolderList.send()
    },
    getFFL: function () {
      if (
        this.getFavoriteFolderList.readyState === 4 &&
        this.getFavoriteFolderList.status === 200
      ) {
        this.favoriteList = JSON.parse(
          this.getFavoriteFolderList.responseText
        ).list
        this.favoriteId = this.favoriteList[0].favouriteId
        this.favoriteName = this.favoriteList[0].favouriteName
        this.showFavoriteContent()
      }
    },
    confirmCreate: function () {
      // this.isCreateF = true;
      // this.$refs.cover.style.display = "block";
    },
    confirmCreateIn: function () {
      this.getAjaxCreateFavorite = new XMLHttpRequest()
      this.getAjaxCreateFavorite.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/createFolderByNickName?nick_name=www&folderName=' +
          this.cfavoriteName,
        true
      )
      this.getAjaxCreateFavorite.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxCreateFavorite.onreadystatechange = this.getCF
      this.getAjaxCreateFavorite.send()
      this.isCreateF = false
    },
    getCF: function () {
      if (
        this.getAjaxCreateFavorite.readyState === 4 &&
        this.getAjaxCreateFavorite.status === 200
      ) {
        this.getFolderList()
      }
    },
    cancelCreateIn: function () {
      this.isCreateF = false
    },
    deleteFavorite: function () {
      this.$confirm(
        '您确定要删除收藏夹：' + this.favoriteName + '吗？',
        '提示',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }
      )
        .then(() => {
          this.$message({
            type: 'success',
            message: '删除成功!'
          })
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          })
        })
    },
    deleteContent: function (ftitle, aid) {
      this.favoriteTitle = ftitle
      this.articleId = aid
      this.isDeleteC = true
    },
    cancelDeleteF: function () {
      this.isDeleteF = false
    },
    cancelDeleteC: function () {
      this.isDeleteC = false
    },
    confirmDeleteC: function () {
      this.getAjaxDeleteContent = new XMLHttpRequest()
      this.getAjaxDeleteContent.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/deleteArticleByID?nick_name=www&folderName=' +
          this.favoriteName +
          '&articleID=' +
          this.articleId,
        true
      )
      this.getAjaxDeleteContent.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxDeleteContent.onreadystatechange = this.getDC
      this.getAjaxDeleteContent.send()
      this.isDeleteC = false
    },
    getDC: function () {
      if (
        this.getAjaxDeleteFavorite.readyState === 4 &&
        this.getAjaxDeleteFavorite.status === 200
      ) {
        this.getAjaxFavoriteContent()
      }
    },
    confirmDeleteF: function () {
      this.getAjaxDeleteFavorite = new XMLHttpRequest()
      this.getAjaxDeleteFavorite.open(
        'POST',
        'http://139.224.255.43:7779/Favourite/deleteFolderByName?nick_name=www&folderName=' +
          this.favoriteName,
        true
      )
      this.getAjaxDeleteFavorite.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxDeleteFavorite.onreadystatechange = this.getDF
      this.getAjaxDeleteFavorite.send()
      this.isDeleteF = false
    },
    getDF: function () {
      if (
        this.getAjaxDeleteFavorite.readyState === 4 &&
        this.getAjaxDeleteFavorite.status === 200
      ) {
        this.getFolderList()
      }
    }
  }
}
</script>

<style>
/* * {
  margin: 0 auto;
} */
#yyw-favorite {
  position: absolute;
  background-color: #c5c5c5;
  width: 100%;
  height: 2300px;
  margin-left:-10px;
  margin-right: 5px;
}
#yyw-left {
  margin:0 auto;
  position: relative;
  width: 278px;
  height: 1441px;
  left: -580px;
  top: 30px;
  overflow-y:auto;

  background: #ffffff;
}
#yyw-add-favorite-button {
  position: absolute;
  width: 278px;
  height: 87px;
  left: 0px;
  top: 0px;

  background: #ffffff;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  border: none;
}
#yyw-add-favorite-text {
  position: absolute;
  width: 152px;
  height: 32px;
  left: 62px;
  top: 27px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 22px;
  line-height: 32px;
  /* identical to box height */

  align-items: center;
  letter-spacing: 0.75px;
  text-transform: uppercase;

  color: #000000;
}
#yyw-right-top {
  margin: 0 auto;
  position: relative;
  height: 91px;
  left:149px;
  top: -1409px;
  width: 1150px;

  background: #ffffff;
}
#yyw-favorite-name {
  position: absolute;
  width: 1036px;
  height: 51px;
  left: 40px;
  top: 22px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 24px;
  line-height: 32px;

  align-items: center;

  color: #000000;
}
#yyw-delete-favorite-button {
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

  align-items: center;
  text-decoration-line: underline;
  cursor: pointer;

  color: rgba(16, 67, 247, 0.72);
}

#yyw-favorite-list {
  margin: 0 auto;
  position: relative;
  width: 1150px;

  left:150px;
  top: -1400px;

}
.yyw-f-item {
  margin-top: 5px;
  text-align: left;
  padding: 20px;
  width: 1110px;
  height: 122px;

  background: #ffffff;
  box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
}
.yyw-f-title {
  position: relative;
  font-size: 24px;
  line-height: 24px;
  color: #000000;
  cursor: pointer;

  /*实现超过一行的显示省略号*/
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
.yyw-f-content {
  margin-top: 10px;
  margin-bottom: 10px;
  height: 50px;

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
}
.yyw-f-date {

  /* width: max-content; */

  font-size: 18px;
  line-height: 18px;
  color: #000000;
}
.yyw-f-time {

  margin-left: 20px;
  /* width: fit-content; */
  font-size: 18px;
  line-height: 18px;
  color: #000000;
}
.yyw-f-answerCount {

  margin-left: 60px;
  /* width: fit-content; */

  font-size: 18px;
  line-height: 18px;

  color: #000000;
}
.yyw-f-delete {

  margin-left: 60px;
  /* width: fit-content; */

  font-size: 18px;
  line-height: 25px;
  text-decoration-line: underline;
  cursor: pointer;

  color: #999494;
}

.yyw-folderPosition {
    margin: 0 auto;
  position: relative;
  top: 12px;
  left:-3px;
}
.yyw-folderList {
  background: #ffffff;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  border: none;
  font-size: 18px;
  width: 278px;
  height: 47px;
  margin-bottom: 5px;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  overflow-x:auto;
  cursor: pointer;
}
.yyw-box-card {
  position: relative;
  width: 278px;
  left: 0px;
  top: 80px;
}
.yyw-fpage-block {
  position: relative;
  left: 0px;
  top: 20px;
}
.yyw-readnum {
  position: relative;
  height: 25px;
  width: 25px;
  left: -6px;
  top: 6px;
}
.yyw-el-dialog {
  position: relative;
  top: 170px;
}
#yyw-ffooter{
  margin: 0 auto;
  position: relative;
  bottom:750px;
}
</style>
