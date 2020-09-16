<template>
	<div id="SearchResult">
		<input :class="['search']" v-model="SearchContent" style="position: absolute; left: 418px; top: -47px; z-index: 999;">
		<el-button icon="el-icon-search" circle style="position:absolute;top:-50px;left:780px; z-index: 999;" @click="selectedPage = 1;getSearchResult();"></el-button>
		<img :class="['Background']" src="../assets/banner.jpg">
		<div id="search-result-info">
			<p> >>包含 {{SearchContent}} 的搜索结果 共{{resultNum}}条 </p>
			<el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" @select="handleSelect" text-color="#000000"
			 active-text-color="#A52A2A">
				<el-menu-item id="search-topic" index="1" @click="selectedPage = 1;getSearchResult();">文章</el-menu-item>
				<el-menu-item id="search-article" index="2" @click="selectedPage = 1;getSearchResult();">话题</el-menu-item>
			</el-menu>
			<span id="search-sort"></span>
			<div id="line"></div>
		</div>
		<div class="search-result-container" id="search-result-container-failinfo">
			<div class="article-container" id="nul-search" style="display: none;">
				<div class="article-title">关键词不能为空！</div>
				<div class="article-summary">搜索热词：学习、生活、情感、游戏、音乐...</div>
			</div>
			<div class="article-container" id="search-fail" style="display: none;">
				<div class="article-title">很抱歉，没有找到相关内容！</div>
				<div class="article-summary">你可以：1.请检查输入是否正确 &nbsp;&nbsp;&nbsp;&nbsp; 2.前往创作板块发表相关文章或话题</div>
			</div>
		</div>
		<div v-show="activeIndex == '1'" class="search-result-container" id="search-result-container-article">
			<div class="article-container" v-for="item in MyArticle" :key="item.ArticleID">
				<div class="article-title">{{item.articleTitle}}</div>
				<div class="article-summary">{{item.articleContent}}</div>
				<div class="publish-date">{{item.articleUploadTime}}</div>
			</div>
		</div>
		<div v-show="activeIndex == '2'" class="search-result-container" id="search-result-container-topic">
			<div class="article-container" v-for="item in MyArticle" :key="item.topicID">
				<div class="article-title">{{item.topicTitle}}</div>
				<div class="article-summary">{{item.topicContent}}</div>
				<div class="publish-date">{{item.topicUploadTime}}</div>
			</div>
		</div>
		<div id="myarticle-page-controller">
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
		name: "SearchResult",
		data() {
			return {
				loc: 'http://139.224.255.43:7779/',
				token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiIxNmM1NzJhZS1iMzA4LTQ3NzgtODViMS01N2UzODg0MjdkMDIiLCJleHAiOjE2MDAxNzYzMTcsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.6ssG6Q93Ck9sODXntZgJgS2e46aOHSzMatPCRouF77Y",
				activeIndex: '1',
				GLCajax: 0,
				GABajax: 0,
				GHAajax: 0,
				GABRajax: 0,
				resultNum: 0,
				MyArticle: [],
				MyTopic: [],
				selectedPage: 1,
				SearchContent: this.$route.query.SearchContent,
			}
		},
		created: function() {
			this.getSearchResultR();
		},
		computed: {
			SearchContent:function() {
				return this.$route.query.SearchContent;
			}
		},
		methods: {
			handleSelect(key, keyPath) {
				console.log(key, keyPath);
				this.activeIndex = key;
			},
			myArtlastpage(){
							   if(this.selectedPage!=1){
								   this.selectedPage=this.selectedPage - 1;
								   this.getSearchResult();
							   }
			},
			myArtnextapage(){
							   		this.selectedPage=this.selectedPage + 1;
									this.getSearchResult();
									if (this.selectedPage > 1 && this.MyArticle.length == 0) {
										this.myArtlastpage();
									}
			},
			getSearchResult() {
				var pagenum = this.selectedPage;
				var id = 3;
				var pagesize = 6;
				var searchContent = this.SearchContent;
				var headerToken = this.token;
				this.GABajax = new XMLHttpRequest();
				if (this.activeIndex == '1') {
					document.getElementById("search-result-container-article").style.display = "block";
					document.getElementById("search-result-container-topic").style.display = "none";
					this.GABajax.open("POST", "http://139.224.255.43:7779/Article/getSearchedArticle?searchContent=" + searchContent +
						"&pageNum=" + pagenum + "&pageSize=" + pagesize, true);
				} else {
					document.getElementById("search-result-container-article").style.display = "none";
					document.getElementById("search-result-container-topic").style.display = "block";
					this.GABajax.open("POST", "http://139.224.255.43:7779/Topic/getSearchedTopic?searchContent=" + searchContent +
						"&pageNum=" + pagenum + "&pageSize=" + pagesize, true);
				}
				this.GABajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GABajax.onreadystatechange = this.GABsuccessfully;
				this.GABajax.send();
			},
			GABsuccessfully() {
				if (this.activeIndex == '1') {
					if (this.GABajax.readyState == 4 && this.GABajax.status == 200) {
						var receive = JSON.parse(this.GABajax.responseText).articleDetail;
						this.MyArticle = receive;
						this.resultNum = this.MyArticle.length;
						for(var i=0;i<this.MyArticle.length;i++){
						   this.MyArticle[i].articleUploadTime=this.MyArticle[i].articleUploadTime.replace("T"," ");
						}
					}
				} else {
					if (this.GABajax.readyState == 4 && this.GABajax.status == 200) {
						var receive = JSON.parse(this.GABajax.responseText).topicDetail;
						this.MyArticle = receive;
						this.resultNum = this.MyArticle.length;
						for(var i=0;i<this.MyArticle.length;i++){
						   this.MyArticle[i].uploadtime=this.MyArticle[i].topicUploadTime.replace("T"," ");
						}
					}
				}
				if (this.SearchContent.length == 0) {
					document.getElementById("nul-search").style.display = "block";
					document.getElementById("search-fail").style.display = "none";
				} else if (this.MyArticle.length == 0) {
					document.getElementById("nul-search").style.display = "none";
					document.getElementById("search-fail").style.display = "block";
				} else {
					document.getElementById("nul-search").style.display = "none";
					document.getElementById("search-fail").style.display = "none";
				}
			},
			getSearchResultR() {
				var pagenum = 1;
				var id = 3;
				var pagesize = 6;
				var searchContent = this.$route.query.SearchContent;
				var headerToken = this.token;
				this.GABRajax = new XMLHttpRequest();
				if (this.activeIndex == '1') {
					//document.getElementById("search-result-container-article").style.display = "block";
					//document.getElementById("search-result-container-topic").style.display = "none";
					this.GABRajax.open("POST", "http://139.224.255.43:7779/Article/getSearchedArticle?searchContent=" + searchContent +
						"&pageNum=" + pagenum + "&pageSize=" + pagesize, true);
				} else {
					//document.getElementById("search-result-container-article").style.display = "none";
					//document.getElementById("search-result-container-topic").style.display = "block";
					this.GABRajax.open("POST", "http://139.224.255.43:7779/Topic/getSearchedTopic?searchContent=" + searchContent +
						"&pageNum=" + pagenum + "&pageSize=" + pagesize, true);
				}
				this.GABRajax.setRequestHeader('Authorization', 'Bearer ' + headerToken);
				this.GABRajax.onreadystatechange = this.GABsuccessfullyR;
				this.GABRajax.send();
			},
			GABsuccessfullyR() {
				if (this.activeIndex == '1') {
					if (this.GABRajax.readyState == 4 && this.GABRajax.status == 200) {
						var receive = JSON.parse(this.GABRajax.responseText).articleDetail;
						this.MyArticle = receive;
						this.resultNum = this.MyArticle.length;
						for(var i=0;i<this.MyArticle.length;i++){
						   this.MyArticle[i].articleUploadTime=this.MyArticle[i].articleUploadTime.replace("T"," ");
						}
					}
				} else {
					if (this.GABRajax.readyState == 4 && this.GABRajax.status == 200) {
						var receive = JSON.parse(this.GABRajax.responseText).topicDetail;
						this.MyArticle = receive;
						this.resultNum = this.MyArticle.length;
						for(var i=0;i<this.MyArticle.length;i++){
						   this.MyArticle[i].uploadtime=this.MyArticle[i].topicUploadTime.replace("T"," ");
						}
					}
				}
				if (this.SearchContent.length == 0) {
					document.getElementById("nul-search").style.display = "block";
					document.getElementById("search-fail").style.display = "none";
				} else if (this.MyArticle.length == 0) {
					document.getElementById("nul-search").style.display = "none";
					document.getElementById("search-fail").style.display = "block";
				} else {
					document.getElementById("nul-search").style.display = "none";
					document.getElementById("search-fail").style.display = "none";
				}
			}
		}
	}
</script>

<style>
	#SearchResult {
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
		top: 60px;
	}

	#search-result-info {
		position: absolute;
		height: 160px;
		width: 980px;
		top: 383px;
		left: 380px;

		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
	}

	#search-info>p {
		position: absolute;
		top: 0px;
		vertical-align: top;
		left: 18px;
		font-size: 14px;
		line-height: 18px;
		color: #727272;
	}

	#search-topic {
		position: absolute;
		width: 110px;
		height: 45px;
		left: 24px;
		top: 64px;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 24px;
		/* identical to box height */

		text-decoration-line: none;
		color: #000000;
	}

	#search-article {
		position: absolute;
		width: 110px;
		height: 45px;
		left: 134px;
		top: 64px;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 24px;
		/* identical to box height */

		text-decoration-line: none;
		color: #000000;
	}

	#search-sort {
		display: inline-block;
		position: absolute;
		width: 137px;
		height: 22px;
		left: 1100px;
		top: 80px;

		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 14px;
		line-height: 18px;

		color: #938E8E;
	}

	#line {
		position: absolute;
		width: 982px;
		height: 0px;
		left: 0px;
		top: 130px;
		z-index: 10;

		border: 1px solid #ADA8A8;
		transform: rotate(-0.09deg);
	}

	#search-result-list {
		position: absolute;
		top: 500px;
		left: 262px;
		width: 100%;
	}

	.search-result-container {
		position: absolute;
		width: 990px;
		height: 800px;
		left: 377px;
		top: 560px;

		border: 1px solid #C4C4C4;
		box-sizing: border-box;
		border: none;
	}
	
	#myarticle-page-controller{
		position: absolute;
		width: 982px;
		height: 100px;
		left: 364px;
		top: 1558px;
		
	  display: flex;
	  align-items: center;
		margin-top: 20px;
		font-size: 18px;
	  line-height: 18px;
	}
	#m-last-page{
	  flex-grow: 1;
		margin-right: 60px;
	  text-align: right;
	  cursor: pointer;
	}
	.m-page-num{
	  display: flex;
	  justify-content: center;
	  align-items: center;
		width: 40px;
		height:40px;
		color: #727272;
		border-radius: 5px;
	  cursor: pointer;
	}
	.m-page-num:hover{
	  background: rgba(114, 114, 114, 0.2);
	}
	.m-page-num-selected{
		background: rgba(114, 114, 114, 0.3);
	}
	#m-next-page{
	  flex-grow: 1;
		margin-left: 60px;
	  text-align: left;
	  cursor: pointer;
	}
</style>
