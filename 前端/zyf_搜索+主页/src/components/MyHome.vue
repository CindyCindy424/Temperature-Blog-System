<template>
	<div id="Home">
		<img :class="['Background']" src="../assets/banner.jpg">
		<div id="my-info-container">
			<el-avatar id="touxiang" src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"></el-avatar>
			<span id="name">{{nickname}}</span>
			<span id="motto">{{personalsign}}</span>
			<div id="follow-btn" v-show="!isMyTopic" @click="follow()">{{isFollowed?"已关注":"+关注"}}</div>
			<span id="fan">粉丝</span>
			<span id="fanNum">{{fansnum}}</span>
			<span id="follow">关注</span>
			<span id="followNum">{{focusnum}}</span>
		</div>

		<div class="hottest-article" style="top: 600px;">
			<div id="article-title-box"></div>
			<span id="hottest-article-title">最热文章</span>
			<div id="article-list">
				<div class="left-aside-list-item" v-for="item in HottestArticle">
					{{item.title}}
				</div>
			</div>
		</div>
		<div class="latest-article" style="top: 1040px;">
			<div id="article-title-box"></div>
			<span id="hottest-article-title">最新文章</span>
			<div id="article-list">
				<div class="left-aside-list-item" v-for="item in LatestArticle">
					{{item.title}}
				</div>
			</div>
		</div>
		<div class="latest-topic" style="height: 344px;">
			<div id="article-title-box"></div>
			<span id="hottest-article-title">最新话题</span>
			<el-button type="danger" id="manage-album-btn" style="background-color: #B23535; top: 10px; right: 20px;">全部话题</el-button>
			<div id="article-list">
				<div class="left-aside-list-item" v-for="item in LatestTopic">
					{{item.TopicTitle}}
				</div>
			</div>
		</div>
		<div id="my-album-container">&nbsp;&#160;&nbsp;&#160;我的相册:{{albumNum}}
			<router-link to="/Album">
				<el-button type="danger" id="manage-album-btn" style="background-color: #B23535;">全部相册</el-button>
			</router-link>
		</div>

		<div id="h-album-list" style="position: absolute; top: 530px; left: 500px; height: 120px; width: 800px;">
			<el-carousel :interval="5000" type="card" height="300px" width="200px">
				<el-carousel-item v-for="item in items" :key="item">
					<h3 class="medium">{{ item }}</h3>
					<img :src="item.firstPhoto" class="image" style="cursor: pointer;" object-fit="fit">
				</el-carousel-item>
			</el-carousel>
		</div>

		<div id="my-article-container">&nbsp;&#160;&nbsp;&#160;我的文章:{{myarticlenum}}
			<router-link to="/MyArticle">
				<el-button type="danger" id="manage-article-btn" style="background-color: #B23535;">全部文章</el-button>
			</router-link>
		</div>
		<div id="home-article-container">
			<div class="article-container" v-for="item in MyArticle" :key="item.article_id">
				<div class="article-title">{{item.title}}</div>
				<div class="article-summary">{{item.articlcontent}}</div>
				<div class="publish-date">{{item.uploadtime}}</div>
				<div class="h-comment-logo"></div>
				<div class="comment-num">{{item.commentnum}}</div>
			</div>
		</div>
		<div id="home-page-controller">
			<div id="m-last-page" @click="myArtlastpage()">上一页</div>
			<!--<div class="m-page-num" v-for="item in pageNums" @click="turntopage(item.page)">
		        {{item.page}}
		     </div>-->
			<div class="m-page-num">
				{{this.selectedPage}}
			</div>
			<div id="m-next-page" @click="myArtnextapage()">下一页</div>
		</div>
	</div>
</template>

<script>
	export default {
		data() {
			return {
				loc: 'http://139.224.255.43:7779/',
				rootUrl: "http://139.224.255.43:7779/",
				token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiIxNmM1NzJhZS1iMzA4LTQ3NzgtODViMS01N2UzODg0MjdkMDIiLCJleHAiOjE2MDAxNzYzMTcsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.6ssG6Q93Ck9sODXntZgJgS2e46aOHSzMatPCRouF77Y",
				myId: 3,
				hostId: 3,
				nickname: "www",
				isMyTopic: true,
				isFollowed: false,
				personalsign: "心如止水",
				articleowneravatar: "",
				focusnum: -1,
				fansnum: -1,
				myarticlenum: 233,
				albumNum: 233,
				receivedcommentnum: 123,
				receivedpraisenum: 3456,
				GLCajax: 0,
				GABajax: 0,
				GHAajax: 0,
				GLAajax: 0,
				GLTajax: 0,
				getAjaxAlbumList: 0,
				getAjaxFirstPhoto: 0,
				getAjaxCreateAlbum: 0,
				getAjaxDeleteAlbum: 0,
				getAjaxAlbumNum: 0,
				HottestArticle: [],
				LatestArticle: [],
				LatestTopic: [],
				MyArticle: [],
				items: [],
				selectedPage: 1,
				maxpagenum: 1,
			}
		},
		created: function() {
			this.getFocusNum();
			this.getFansNum();
			this.getUserInfo();
			this.getHottestArticle();
			this.getLatestArticle();
			this.getLatestTopic();
			this.getLikesReplyArticleNum();
			this.getArticleByPage();
			this.showAlbumList();
			this.getAlbumNum();
			this.checkFollow();
		},
		methods: {
			getToken: function() {
				this.token = document.cookie.split(";")[0].split("=")[1];
			},
			myArtlastpage() {
				if (this.selectedPage != 1) {
					this.selectedPage = this.selectedPage - 1;
					this.getArticleByPage();
				}
			},
			myArtnextapage() {
				if (this.selectedPage <= this.maxpagenum) {
					this.selectedPage = this.selectedPage + 1;
					this.getArticleByPage();
				}
			},
			getFocusNum() {
				var Nickname = this.nickname;
				var headerToken = this.token;
				this.GFNajax = new XMLHttpRequest();
				this.GFNajax.open("POST", "http://139.224.255.43:7779/Account/getFollowNum?nick_name=" + Nickname, true);
				this.GFNajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GFNajax.onreadystatechange = this.GFNsuccessfully;
				this.GFNajax.send();
			},
			GFNsuccessfully() {
				if (this.GFNajax.readyState == 4 && this.GFNajax.status == 200) {
					this.focusnum = JSON.parse(this.GFNajax.responseText).followCnt;
				}
			},
			getFansNum() {
				var Nickname = this.nickname;
				var headerToken = this.token;
				this.GFanNajax = new XMLHttpRequest();
				this.GFanNajax.open("POST", "http://139.224.255.43:7779/Account/getFanNum?nick_name=" + Nickname, true);
				this.GFanNajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GFanNajax.onreadystatechange = this.GFanNsuccessfully;
				this.GFanNajax.send();
			},
			GFanNsuccessfully() {
				if (this.GFanNajax.readyState == 4 && this.GFanNajax.status == 200) {
					/*console.log(this.GFanNajax.responseText);
					console.log(JSON.parse(this.GFanNajax.responseText));*/
					this.fansnum = JSON.parse(this.GFanNajax.responseText).fanCnt;
				}
			},
			getAnnoncement() {
				var Nickname = this.nickname;
				var headerToken = this.token;
				this.GAajax = new XMLHttpRequest();
				this.GAajax.open("POST", "http://139.224.255.43:7779/Account/getFanNum?nick_name=" + Nickname, true);
				this.GAajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GAajax.onreadystatechange = this.GAsuccessfully;
				this.GAajax.send();
			},
			GAsuccessfully() {
				if (this.GAajax.readyState == 4 && this.GAajax.status == 200) {
					this.personalsign = JSON.parse(this.GAajax.responseText).fanCnt;
				}
			},
			getUserInfo() {
				var id = 3;
				var name = this.nickname;
				var headerToken = this.token;
				this.GUIajax = new XMLHttpRequest();
				this.GUIajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByID?user_id=" + id, true);
				this.GUIajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GUIajax.onreadystatechange = this.GUIsuccessfully;
				this.GUIajax.send();
			},
			GUIsuccessfully() {
				if (this.GUIajax.readyState == 4 && this.GUIajax.status == 200) {
					console.log(JSON.parse(this.GUIajax.responseText));
					this.nickname = JSON.parse(this.GUIajax.responseText).userInfo.nickName;
					this.personalsign = JSON.parse(this.GUIajax.responseText).userAnnouncement;
					if (this.personalsign == null) {
						this.personalsign = "心如止水。";
					}
					this.articleowneravatar = JSON.parse(this.GUIajax.responseText).userInfo.avatr;
					/*this.personalsign=JSON.parse(this.GUIajax.responseText).userInfo.nickName*/
				}
			},
			follow() { //关注功能
				var idOfBlogger = 3; //alert(1);
				var idOfFans = 1;
				//console.log(this.thisTopicUser);
				if (!this.isFollowed) {
					this.ajax_follow = new XMLHttpRequest();
					this.ajax_follow.open("POST", "http://139.224.255.43:7779/Account/createFollowByID?idOfBlogger=" + idOfBlogger +
						"&idOfFans=" + idOfFans, true);
					this.ajax_follow.setRequestHeader('Authorization', 'Bearer ' + this.token);
					this.ajax_follow.onreadystatechange = this.fw;
					this.ajax_follow.send();
				} else {
					this.ajax_dfollow = new XMLHttpRequest();
					this.ajax_dfollow.open("POST", "http://139.224.255.43:7779/Account/deleteFollowByID?idOfBlogger=" + idOfBlogger +
						"&idOfFans=" + idOfFans, true);
					this.ajax_dfollow.setRequestHeader('Authorization', 'Bearer ' + this.token);
					this.ajax_dfollow.onreadystatechange = this.dfw;
					this.ajax_dfollow.send();
				}
			},
			fw() { //关注功能附属函数
				if (this.ajax_follow.readyState == 4 && this.ajax_follow.status == 200) {
					var receive = JSON.parse(this.ajax_follow.responseText);
					//console.log(receive);
					if (receive.createFlag == 1) {
						this.isFollowed = !this.isFollowed;
					}
				}
			},
			dfw() { //关注功能附属函数
				if (this.ajax_dfollow.readyState == 4 && this.ajax_dfollow.status == 200) {
					var receive = JSON.parse(this.ajax_dfollow.responseText);
					//console.log(receive);
					if (receive.getFlag == 1) {
						this.isFollowed = !this.isFollowed;
					}
				}
			},
			checkFollow() {
				var idOfCurrentUser = 3;
				var idOfCheckUser = 1;
				//console.log(idOfCheckUser);
				this.ajax_checkFollow = new XMLHttpRequest();
				this.ajax_checkFollow.open("POST", "http://139.224.255.43:7779/Account/checkFollow?idOfCurrentUser=" +
					idOfCurrentUser + "&idOfCheckUser=" + idOfCheckUser, true);
				this.ajax_checkFollow.setRequestHeader('Authorization', 'Bearer ' + this.token);
				this.ajax_checkFollow.onreadystatechange = this.CF;
				this.ajax_checkFollow.send();
			},
			CF() {
				if (this.ajax_checkFollow.readyState == 4 && this.ajax_checkFollow.status == 200) {
					var receive = JSON.parse(this.ajax_checkFollow.responseText);
					if (receive.result == "False") {
						this.isFollowed = false;
					} else {
						this.isFollowed = true;
					}
					//console.log(receive);
				}
			},
			getHottestArticle() {
				var id = 3;
				var number = 8;
				var headerToken = this.token;
				this.GHAajax = new XMLHttpRequest();
				this.GHAajax.open("POST", "http://139.224.255.43:7779/Article/getPersonalhottestArticle?userid=" + id +
					"&getarticleNum=8", true);
				this.GHAajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GHAajax.onreadystatechange = this.GHAsuccessfully;
				this.GHAajax.send();
			},
			GHAsuccessfully() {
				if (this.GHAajax.readyState == 4 && this.GHAajax.status == 200) {
					var receive = JSON.parse(this.GHAajax.responseText).articleDetail;
					this.HottestArticle = receive;
				}
			},
			getLatestArticle() {
				var id = 3;
				var number = 8;
				var headerToken = this.token;
				this.GLAajax = new XMLHttpRequest();
				this.GLAajax.open("POST", "http://139.224.255.43:7779/Article/getPersonalnewestArticle?userid=" + id +
					"&getarticleNum=8", true);
				this.GLAajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GLAajax.onreadystatechange = this.GLAsuccessfully;
				this.GLAajax.send();
			},
			GLAsuccessfully() {
				if (this.GLAajax.readyState == 4 && this.GLAajax.status == 200) {
					var receive = JSON.parse(this.GLAajax.responseText).articleDetail;
					this.LatestArticle = receive;
				}
			},
			getLatestTopic() { //获取最新话题
				var id = 3;
				var takeTopicNum = 8;
				var headerToken = this.token;
				this.ajax_getLatestTopics = new XMLHttpRequest();
				this.ajax_getLatestTopics.open("POST", "http://139.224.255.43:7779/Topic/getNewestTopic?takeTopicNum=" +
					takeTopicNum, true);
				this.ajax_getLatestTopics.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.ajax_getLatestTopics.onreadystatechange = this.getLT;
				this.ajax_getLatestTopics.send();
			},
			getLT() { //最新话题附属函数
				if (this.ajax_getLatestTopics.readyState == 4 && this.ajax_getLatestTopics.status == 200) {
					var receive = JSON.parse(JSON.parse(this.ajax_getLatestTopics.responseText).topics);
					//console.log(receive);
					this.LatestTopic = receive;
				}
			},
			getLikesReplyArticleNum() {
				var id = 3;
				var headerToken = this.token;
				this.GANajax = new XMLHttpRequest();
				this.GANajax.open("POST", "http://139.224.255.43:7779/Account/getUserLikesReplyArticleNum?id=" + id, true);
				this.GANajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GANajax.onreadystatechange = this.GANsuccessfully;
				this.GANajax.send();
			},
			GANsuccessfully() {
				if (this.GANajax.readyState == 4 && this.GANajax.status == 200) {
					/*console.log(this.GANajax.responseText);
					console.log(JSON.parse(this.GANajax.responseText));*/
					this.myarticlenum = JSON.parse(this.GANajax.responseText).articleCnt;
					this.receivedcommentnum = JSON.parse(this.GANajax.responseText).replyCnt;
					this.receivedpraisenum = JSON.parse(this.GANajax.responseText).likesCnt;
				}
			},
			getLikesReplyArticleNum() {
				var id = this.hostId;
				var headerToken = this.token;
				this.GANajax = new XMLHttpRequest();
				this.GANajax.open("POST", "http://139.224.255.43:7779/Account/getUserLikesReplyArticleNum?id=" + id, true);
				this.GANajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GANajax.onreadystatechange = this.GANsuccessfully;
				this.GANajax.send();
			},
			GANsuccessfully() {
				if (this.GANajax.readyState == 4 && this.GANajax.status == 200) {
					/*console.log(this.GANajax.responseText);
					console.log(JSON.parse(this.GANajax.responseText));*/
					this.myarticlenum = JSON.parse(this.GANajax.responseText).articleCnt;
					this.receivedcommentnum = JSON.parse(this.GANajax.responseText).replyCnt;
					this.receivedpraisenum = JSON.parse(this.GANajax.responseText).likesCnt;
					var maxpageNum = this.myarticlenum / 5;
					this.maxpagenum = maxpageNum;
					console.log(this.maxpagenum);
				}
			},
			getArticleByPage() {
				var pagenum = this.selectedPage;
				var pagesize = 5;
				var id = 3;
				var headerToken = this.token;
				this.GABajax = new XMLHttpRequest();
				this.GABajax.open("POST", "http://139.224.255.43:7779/Article/getpersonalArticlebypage?pageNum=" + pagenum +
					"&pageSize=" + pagesize + "&userId=" + id, true);
				this.GABajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GABajax.onreadystatechange = this.GABsuccessfully;
				this.GABajax.send();
			},
			GABsuccessfully() {
				if (this.GABajax.readyState == 4 && this.GABajax.status == 200) {
					var receive = JSON.parse(JSON.parse(this.GABajax.responseText).Result);
					this.MyArticle = receive;
					for (var i = 0; i < this.MyArticle.length; i++) {
						this.MyArticle[i].uploadtime = this.MyArticle[i].uploadtime.replace("T", " ");
					}
				}
			},
			getAlbumNum() {
				this.getAjaxAlbumNum = new XMLHttpRequest();
				this.getAjaxAlbumNum.open(
					"POST",
					"http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=" + this.hostId + "&pageNum=1&pageSize=9999",
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
			showAlbumList() {
				this.getAjaxAlbumList = new XMLHttpRequest();
				this.getAjaxAlbumList.open(
					"POST",
					"http://139.224.255.43:7779/Photo/getAllAlbumByPage?userID=" + this.hostId + "&pageNum=1&pageSize=4",
					true
				);
				this.getAjaxAlbumList.setRequestHeader(
					"Authorization",
					"Bearer " + this.token
				);
				this.getAjaxAlbumList.onreadystatechange = this.getAL;
				this.getAjaxAlbumList.send();
			},
			getAN() {
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
			getAL() {
				if (
					this.getAjaxAlbumList.readyState == 4 &&
					this.getAjaxAlbumList.status == 200
				) {
					this.items = JSON.parse(
						JSON.parse(this.getAjaxAlbumList.responseText).albums
					); // eslint-disable-line no-unused-vars
					var receive = JSON.parse(this.getAjaxAlbumList.responseText).firstPhoto;
					console.log(receive)

					for (var i = 0; i < this.items.length; i++) {

						this.items[i].AlbumTime = this.items[i].AlbumTime.split("T")[0];
						if (receive[i] != "#")
							this.items[i].firstPhoto = this.rootUrl + receive[i];

					}
				}
			},
		}
	}
</script>

<style>
	#Home {
		position: absolute;
		background-color: #E5E5E5;
		width: 100%;
		height: 3000px;
		top: 59px;
	}

	.Background {
		position: absolute;
		width: 100%;
		height: 352px;
		left: 0px;
		top: 0px;
	}

	#my-info-container {
		position: absolute;
		top: 389px;
		left: 46px;
		width: 303px;
		height: 154px;
		background: #FFFFFF;
	}

	#touxiang {
		position: absolute;
		top: 23px;
		left: 26px;
	}

	#name {
		position: absolute;
		top: 20px;
		left: 100px;
	}

	#motto {
		position: absolute;
		top: 48px;
		left: 100px;
	}

	#follow {
		position: absolute;
		top: 95px;
		left: 30px;
	}

	#followNum {
		position: absolute;
		top: 97px;
		left: 70px;
	}

	#fan {
		position: absolute;
		top: 95px;
		left: 150px;
	}

	#fanNum {
		position: absolute;
		top: 97px;
		left: 190px;
	}

	#follow-btn {
		position: absolute;
		top: 20px;
		left: 200px;
		background: #B23535;
		color: white;
	}

	.container-title {
		position: absolute;
		top: 0px;
		left: 0px;
		width: 120px;
		height: 56px;
		background: #B23535;
		border-radius: 0px 0px 30px 0px;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 18px;
		line-height: 24px;
		display: flex;
		align-items: center;
		text-align: center;
		letter-spacing: 0.75px;
		text-transform: uppercase;
		color: #FFFFFF;
	}

	#my-album-container {
		position: absolute;
		top: 389px;
		left: 414px;
		width: 982px;
		height: 91px;
		background: #FFFFFF;

		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: 290;
		font-size: 18px;
		line-height: 24px;

		display: flex;
		align-items: center;
		text-align: left;
	}

	#manage-album-btn {
		position: absolute;
		top: 24px;
		right: 58px;
		width: 99px;
		height: 42px;
	}

	#my-article-container {
		position: absolute;
		top: 833px;
		left: 414px;
		width: 982px;
		height: 91px;
		background: #FFFFFF;

		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: 290;
		font-size: 18px;
		line-height: 24px;

		display: flex;
		align-items: center;
		text-align: left;
	}

	#manage-article-btn {
		position: absolute;
		top: 24px;
		right: 58px;
		width: 99px;
		height: 42px;
	}



	#home-article-container {
		position: absolute;
		width: 982px;
		height: 500px;
		left: 414px;
		top: 965px;

		border: 1px solid #C4C4C4;
		box-sizing: border-box;
		border: none;
	}

	.latest-topic {
		position: absolute;
		width: 302px;
		height: 375px;
		left: 45px;
		top: 1449px;

		background: #FFFFFF;
		border-radius: 20px;
	}

	.latest-article {
		position: absolute;
		width: 302px;
		height: 344px;
		left: 45px;
		top: 1067px;

		background: #FFFFFF;
		border-radius: 20px;
	}

	#home-card-1 {
		display: inline;
		position: absolute;
		width: 144px;
		height: 177px;
		top: 620px;
		left: 420px;
		margin-bottom: 70px;
		margin-left: 95px;
	}

	#home-card-2 {
		display: inline;
		position: absolute;
		width: 144px;
		height: 177px;
		top: 620px;
		left: 620px;
		margin-bottom: 70px;
		margin-left: 95px;
	}

	#home-card-3 {
		display: inline;
		position: absolute;
		width: 144px;
		height: 177px;
		top: 620px;
		left: 820px;
		margin-bottom: 70px;
		margin-left: 95px;
	}

	#home-card-4 {
		display: inline;
		position: absolute;
		width: 144px;
		height: 177px;
		top: 620px;
		left: 1020px;
		margin-bottom: 70px;
		margin-left: 95px;
	}

	.halbumName {
		position: absolute;
		width: 144px;
		height: 30px;
		top: 177px;
	}

	.el-carousel__item h3 {
		position: absolute;
		top: 520px;
		left: 420px;
		height: 100%;
		color: #475669;
		font-size: 14px;
		opacity: 0.75;
		line-height: 200px;
		margin: 0;
	}

	#home-page-controller {
		position: absolute;
		width: 982px;
		height: 100px;
		left: 404px;
		top: 1800px;

		display: flex;
		align-items: center;
		margin-top: 20px;
		font-size: 18px;
		line-height: 18px;
	}

	#m-last-page {
		flex-grow: 1;
		margin-right: 60px;
		text-align: right;
		cursor: pointer;
	}

	.m-page-num {
		display: flex;
		justify-content: center;
		align-items: center;
		width: 40px;
		height: 40px;
		color: #727272;
		border-radius: 5px;
		cursor: pointer;
	}

	.m-page-num:hover {
		background: rgba(114, 114, 114, 0.2);
	}

	.m-page-num-selected {
		background: rgba(114, 114, 114, 0.3);
	}

	#m-next-page {
		flex-grow: 1;
		margin-left: 60px;
		text-align: left;
		cursor: pointer;
	}

	.h-comment-logo {
		display: inline-block;
		margin: 6px 0px 0px 10px;
		width: 20px;
		height: 20px;
		margin-left: 51px;
		margin-top: 10px;

		background-image: url('../assets/comment.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
</style>
