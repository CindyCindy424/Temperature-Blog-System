<template>
	<div id="ViewArticle-page">
		<div id="top-menu" v-if="isMenuShow">
			<button class="t-m-button" v-for="item in Favoritelist" @click="collectArticle(item.favouriteName)">
			  {{item.favouriteName}}
			</button>
			<!-- <div id="t-m-publish-article" class="t-m-button" :style="{top: TMTop, left: MLeft + 'px'}">发表文章</div>
			<div id="t-m-publish-topic" class="t-m-button">发表话题</div>
			<div id="t-m-upload-image" class="t-m-button">上传图片</div> -->
		</div>
		<div class="ViewArticle-personal">
			    <div>
					<img id="ViewArticle-photo" :src="this.articleowneravatar==undefined?'':loc+this.articleowneravatar" />
				</div>
				<div>
					<div id="ViewArticle-name">{{articleownername}}</div>
					<div id='ViewArticle-sign'>{{personalsign}}</div>
				</div>
				<div>
					<button id="ViewArticle-focus">
						<span>+关注</span>
					</button>
					<button id="ViewArticle-prv-let">
						<span>私信</span>
					</button>
					<button id="ViewArticle-per-cen">
						<span>个人中心</span>
					</button>		
				</div>
		</div>
		<div class="ViewArticle-HowPost">
				   <div id="ViewArticle-HowPost-title-box"></div>
				   <span id="ViewArticle-HowPost-title">热门文章</span>
				   <div id="ViewArticle-list2">
					   <div class="ViewArticle-left-aside-list-item" v-for="item in HArticles" @click="reflashpage(item.title,item.authorname)">
					     <span id="ViewArticle-hotarticlename">{{item.title}}</span>
					   </div>
				   </div>
		</div>
		<div id="ViewArticle-content-container">
			<div id="ViewArticle-content-title">
				{{title}}
			</div>
			<div id="ViewArticle-content-data">
				<div id="ViewArticle-time-logo"></div>
				&nbsp;&nbsp;&nbsp;
				<span id ="ViewArticle-content-time">{{date}}</span>
				&nbsp;&nbsp;&nbsp;&nbsp;
				<div id="ViewArticle-praise-logo"></div>
				&nbsp;&nbsp;&nbsp;
				<span id="ViewArticle-praise-text">{{articlepraisenum}}</span>
				&nbsp;&nbsp;&nbsp;&nbsp;
				<div id="ViewArticle-collect-logo1"></div>
				&nbsp;&nbsp;&nbsp;
				<span id="ViewArticle-collect-rext"> {{articlecollectnum}}</span>
			</div>
			<div>
				<div id="ViewArticle-target-logo"></div>
				<div id="ViewArticle-target">
					<span>tag:{{tag}}</span>
				</div>
			</div>
			<div id="ViewArticle-article-text">{{article}}</div>
			<div id="ViewArticle-bottom">
				<div id="ViewArticle-agree" @click="praise()" v-if="praiselimit==1">
					<div id="ViewArticle-agree-logo"></div>
					<span>赞同该作者</span>
				</div>
				<div id="ViewArticle-agree" v-if="praiselimit==2">
					<div id="ViewArticle-agreed-logo"></div>
					<span id="ViewArticle-praised">赞同该作者</span>
				</div>
				<div id="ViewArticle-collect" @click="getFolderlist()" v-if="collectlimit==1"><!-- @click="collectArticle()" -->
				<div id="ViewArticle-collect-logo"></div>
					<span @click="topMenuToggle">收藏</span>
				</div>
				<div id="ViewArticle-collect" v-if="collectlimit==2">
					<div id="ViewArticle-collected-logo"></div>
					<span id="ViewArticle-collected">收藏</span>
				</div>
				
			</div>
		</div>
		<div class="ViewArticle-comment-container-larger">
			<div id="ViewArticle-comment-edit">
				<img id="ViewArticle-my-person-logo" :src="this.accountowneravatar==undefined?'':loc+this.accountowneravatar" />
				<input id="ViewArticle-comment-container" v-model="inputcomment" placeholder="请输入评论内容..(10个字符以内)"/>
				<el-button id="ViewArticle-add-comment" @click="addComment()" >添加评论</el-button>
			</div>
			
			<div id="ViewArticle-comment-list">
				<div class="ViewArticle-c-item" v-for="item in comment" :key="item.article_cr_id">
					<img class="ViewArticle-citem-person-logo" :src="loc+item.avatr">
					<div class="ViewArticle-citem-comment-text">{{item.nick_name}}:{{item.article_cr_content}}</div>
					<div class="ViewArticle-citem-comment-tar" v-if="item.parent_cr_id">
						<p class="ViewArticle-citem-comment-tar-p">回复{{item.parent_cr_name}}:{{item.parent_cr_content}}</p>
					</div>
					<div class="ViewArticle-citem-comment-tar-not" v-if="!item.parent_cr_id">
						<p class="ViewArticle-citem-comment-tar-p-not">空白</p>
					</div>
					<div class="ViewArticle-citem-comment-time">
						<p class="ViewArticle-citem-comment-tar-p">{{item.article_cr_time}}</p>
					</div>
					<div class="ViewArticle-citem-comtocom">
						<el-button type="text" @click="addSonComment(item.article_cr_id)">评论</el-button>
					</div>
					<!-- <img class="ViewArticle-citem-praise-logo" src="../assets/article/hand.png"/>
					<div class="ViewArticle-citem-praise" @click="praisecom()" v-if="praisecomlimit==1"><span>赞！</span></div> 
					<div class="ViewArticle-citem-praised" v-if="praisecomlimit==2"><span>赞！</span></div> -->
					<div class="ViewArticle-citem-comment-delete" v-if="item.nick_name==accountownername" @click="DeleteComment(item.article_cr_id)">删除</div>
					<div class="ViewArticle-citem-comment-delete-not" v-if="item.nick_name!=accountownername" >空白</div>
					
					<!-- <el-dialog title="回复评论" :visible.sync="dialogFormVisible">
					  <el-form :model="form">
					    <el-form-item label="内容" :label-width="formLabelWidth">
					      <el-input v-model="inputcommenttocomment" autocomplete="off" placeholder="请输入回复..(10个字符以内)"></el-input>
					    </el-form-item>
					  </el-form>
					  <div slot="footer" class="dialog-footer">
					    <el-button @click="dialogFormVisible = false">取 消</el-button>
					    <el-button type="primary" @click="addSonComment(item.article_cr_id)">确 定</el-button>
					  </div>
					</el-dialog> -->
				</div>
			</div>
		</div>
		<div id="ViewArticle-comment-page-controller">
			<div id="ViewArticle-c-last-page" @click="myComlastpage()">上一页</div>
		    <!--<div class="c-page-num" v-for="item in pageNums" @click="turntopage(item.page)">
		        {{item.page}}
		     </div>-->
			 <div class="ViewArticle-c-page-num">
				 {{this.selectedPage}}
			 </div>
			<div id="ViewArticle-c-next-page" @click="myComnextapage()">下一页</div>
		</div>
	
	</div>
</template>

<script>
	export default {
	       name:"viewArticle",
		   data(){
		   	return {
				loc:'http://139.224.255.43:7779/',
				token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiJhZDljN2FmYi03MjhiLTQ3ZDEtOGJmNC0yOWRhMWZmODE1YWMiLCJleHAiOjE2MDAyNjY1NjIsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.38y9RBwfqP0w6xhVRwQLio27H_ASiSVvGxv4RYtkwPE",
				articleid:0,
				articleownerid:0,
				accountownerid:0,
				articleownername:"",
				personalsign:"",
				articleowneravatar:"",
				accountownername:"",
				title:"",
				date:"",
				articlepraisenum:"",
				praiselimit:1,
				articlecollectnum:"",
				collectlimit:1,
				praisecomlimit:1,
				article:"内容",
				tag:"分区",
				GHAajax:0,
				GCajax:0,
		   		HArticles:[],
				comment:[],
				Favoritelist:[],
				inputcomment:'',
				inputcommenttocomment:'',
				parentcrname:"",
				parentcrcontent:"",
				/*pageNums:[
							{page: 1},
							{page: 2},
							{page: 3}
				],*/
				selectedPage: 1,
				isMenuShow: false,
				maxcommentpage:1,
				 dialogFormVisible: false,
				        form: {
				          name: '',
				          region: '',
				          date1: '',
				          date2: '',
				          delivery: false,
				          type: [],
				          resource: '',
				          desc: ''
				        },
				        formLabelWidth: '120px',
		   	};
		   },
		   created:function(){
			  this.getQuery();
			  if(this.articleownername!=""){
				  this.getArticleownerInfo();
			  }
			  if(this.articleownerid!=""){
				   this.Articleowneridtoname();
			  }
			  if(this.accountownername!=""){
				  this.getAccountownerInfo();
			  }
			  if(this.accountownerid!=""){
				  this.Accountowneridtoname();
			  }
		   	  this.getArticle();
			  this.getComment();
			  this.getHottestArticle();
			  this.getCommentNum();
		   },
	       methods:{
			     getToken:function(){
			       this.token=document.cookie.split(";")[0].split("=")[1];
			       },
			   getQuery: function () {
			     this.title = this.$route.query.ViewArticleTitle;
				 this.articleownername = this.$route.query.ViewArticleNickname;
			     this.accountownername = this.$route.query.ViewArticleUsername;
				 this.articleownerid = this.$route.query.ViewArticleNickid; 
				 this.accountownerid = this.$route.query.ViewArticleUserid; 
			   },
			   
			   topMenuToggle(){
			   	this.isMenuShow = this.isMenuShow == false?true:false;
			   },
	       	addComment(){
				if(this.inputcomment!=""&&this.inputcomment.length<=10){
					var Nickname = this.accountownername;
					var title = this.title;
					var content =this.inputcomment;
					var headerToken=this.token;
					this.ACajax = new XMLHttpRequest();
					this.ACajax.open("POST", "http://139.224.255.43:7779/Article/createArticleCommentByNickName?nick_name="+Nickname+"&title="+title+"&content="+content, true);
					this.ACajax.setRequestHeader('Authorization','Bearer '+headerToken);
					this.ACajax.onreadystatechange = this.ACsuccessfully;
					this.ACajax.send();
				}
				else if(this.inputcomment==""){
					this.$alert('评论不能为空！！', '警告', {
					          confirmButtonText: '确定'
					        });
				}
				else if(this.inputcomment.length>10){
					this.$alert('评论不能超过10个字符！！', '警告', {
					          confirmButtonText: '确定'
					        });
				}
			},
			ACsuccessfully(){
				if (this.ACajax.readyState == 4 && this.ACajax.status == 200) {
					if(JSON.parse(this.ACajax.responseText).returnFlag == 1){
						this.$alert('评论成功！', '提示', {
						          confirmButtonText: '确定'
						        });
					    this.getComment();
						this.getCommentNum();
						this.inputcomment="";
					}
					else if(JSON.parse(this.ACajax.responseText).returnFlag == 2){
						this.$alert('没有找到该用户，请重试', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.ACajax.responseText).returnFlag == 3){
						this.$alert('没有找到文章，请重试', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else{
						this.$alert('操作失败，请重试', '提示', {
						          confirmButtonText: '确定'
						        });
					}
				}
			},
			getArticle(){
				var Nickname = this.accountownername;
				var title = this.title;
				var headerToken=this.token;
				this.GAajax = new XMLHttpRequest();
				this.GAajax.open("POST", "http://139.224.255.43:7779/Article/getArticleInfoByTitle?nick_name="+Nickname+"&title="+title, true);
			    this.GAajax.setRequestHeader('Authorization','Bearer '+headerToken);
			    this.GAajax.onreadystatechange = this.GAsuccessfully;
			    this.GAajax.send();
			},
			GAsuccessfully(){
				if (this.GAajax.readyState == 4 && this.GAajax.status == 200){
					console.log(this.GAajax.responseText);
					
					document.getElementById("ViewArticle-article-text").innerHTML=JSON.parse(this.GAajax.responseText).info.articleContent;
					this.date=JSON.parse(this.GAajax.responseText).info.articleUploadTime.replace("T","  ");
					this.articlepraisenum=JSON.parse(this.GAajax.responseText).info.articleLikes;
					this.articlecollectnum=JSON.parse(this.GAajax.responseText).info.collectNum;
					this.articleid=JSON.parse(this.GAajax.responseText).info.articleId;
					var zone=JSON.parse(this.GAajax.responseText).info.zoneId;
					this.getZoneName(zone);
					
				}
			},
			getComment(){
				var Nickname = this.accountownername;
				var title = this.title;
				var pagenum=this.selectedPage;
				var pagesize=5;
				var headerToken=this.token;
				this.GCajax = new XMLHttpRequest();
				this.GCajax.open("POST", "http://139.224.255.43:7779/Article/getArticleCommentByTitle?nick_name="+Nickname+"&title="+title+"&pageNum="+pagenum+"&pageSize="+pagesize, true);
				this.GCajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GCajax.onreadystatechange = this.GCsuccessfully;
				this.GCajax.send();
			},
			GCsuccessfully(){
				if (this.GCajax.readyState == 4 && this.GCajax.status == 200){
					console.log(JSON.parse(this.GCajax.responseText).item);
					
					var receive = JSON.parse(this.GCajax.responseText).item;
					this.comment = receive;
					for(var i=0;i<this.comment.length;i++){
						this.comment[i].article_cr_time=this.comment[i].article_cr_time.replace("T"," ");
					}
					
				}
				/*else if(this.GCajax.readyState!=4){
					console.log(this.GCajax.readyState);
				}*/
			},
			getHottestArticle(){
				var number = 8;
				var headerToken=this.token;
				this.GHAajax = new XMLHttpRequest();
				this.GHAajax.open("POST", "http://139.224.255.43:7779/Article/getArticlebyreadnum?takeArticleNum="+number, true);
				this.GHAajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GHAajax.onreadystatechange = this.GHAsuccessfully;
				this.GHAajax.send();
			},
			GHAsuccessfully(){
				if (this.GHAajax.readyState == 4 && this.GHAajax.status == 200){
					var receive = JSON.parse(this.GHAajax.responseText).articleDetail;
					this.HArticles = receive;
					console.log(receive);
					/*console.log(this.GHAajax.responseText);
					console.log(JSON.parse(this.GHAajax.responseText));
					console.log(JSON.parse(this.GHAajax.responseText).articless);
					console.log(JSON.parse(JSON.parse(this.GHAajax.responseText).articless));*/
					/*for(var i=0;i<this.HArticles.length;i++){
						this.HArticles[i].title=this.hottestarticle[i].Title;
					}*/
				}
			},
			praise(){
				this.addArticlelike();
			},
			praisecom(){
				this.praisecomlimit=2;
				this.$alert('点赞评论成功！', '提示', {
				          confirmButtonText: '确定'
				        });
			},
			collectArticle(foldername){
				var name=this.accountownername;
				var collectfolder=foldername;
				var articleID=this.articleid;
				var headerToken=this.token;
				this.CAajax = new XMLHttpRequest();
				this.CAajax.open("POST", "http://139.224.255.43:7779/Favourite/addArticleByID?nick_name="+name+"&folderName="+collectfolder+"&articleID="+articleID, true);
				this.CAajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.CAajax.onreadystatechange = this.CAsuccessfully;
				this.CAajax.send();
			},
			CAsuccessfully(){
				if (this.CAajax.readyState == 4 && this.CAajax.status == 200){
					if(JSON.parse(this.CAajax.responseText).returnFlag==1){
						this.articlecollectnum=this.articlecollectnum+1;
						this.collectlimit=2;
						this.addArticlecollect();
						this.isMenuShow=false;
						this.$alert('收藏成功！', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.CAajax.responseText).returnFlag==2){
						this.$alert('用户不存在...', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.CAajax.responseText).returnFlag==3){
						this.$alert('收藏夹不存在', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.CAajax.responseText).returnFlag==4){
						this.$alert('文章不存在', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.CAajax.responseText).flag==0){
						this.collectlimit=2;
						this.isMenuShow=false;
						this.$alert('已收藏！', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					
				}
			},
			collect(){
				this.articlecollectnum=this.articlecollectnum+1;
				this.collectlimit=2;
				this.addArticlecollect();
				this.$alert('收藏成功！', '提示', {
				          confirmButtonText: '确定'
				        });
			},
			getArticleownerInfo(){
				var name = this.articleownername;
				var headerToken=this.token;
				this.GUIajax = new XMLHttpRequest();
				this.GUIajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByNickName?nick_name="+name, true);
				this.GUIajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GUIajax.onreadystatechange = this.GUIsuccessfully;
				this.GUIajax.send();
			},
			GUIsuccessfully(){
				if (this.GUIajax.readyState == 4 && this.GUIajax.status == 200){
					/*console.log(JSON.parse(this.GUIajax.responseText));*/
					this.articleownerid=JSON.parse(this.GUIajax.responseText).userInfo.userId;
					this.personalsign=JSON.parse(this.GUIajax.responseText).userAnnouncement;
					this.articleowneravatar=JSON.parse(this.GUIajax.responseText).userInfo.avatr;
				}
			},
			Articleowneridtoname(){
				var id= this.articleownerid;
				var headerToken=this.token;
				this.GUNajax = new XMLHttpRequest();
				this.GUNajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByID?user_id="+id, true);
				this.GUNajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GUNajax.onreadystatechange = this.GUNsuccessfully;
				this.GUNajax.send();
			},
			GUNsuccessfully(){
				if (this.GUNajax.readyState == 4 && this.GUNajax.status == 200){
					this.articleownername=JSON.parse(this.GUNajax.responseText).userInfo.nickName;
					this.personalsign=JSON.parse(this.GUNajax.responseText).userAnnouncement;
					this.articleowneravatar=JSON.parse(this.GUNajax.responseText).userInfo.avatr;
				}
			},
			getAccountownerInfo(){
				var name = this.accountownername;
				var headerToken=this.token;
				this.GAIajax = new XMLHttpRequest();
				this.GAIajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByNickName?nick_name="+name, true);
				this.GAIajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GAIajax.onreadystatechange = this.GAIsuccessfully;
				this.GAIajax.send();
			},
			GAIsuccessfully(){
				if (this.GAIajax.readyState == 4 && this.GAIajax.status == 200){
					/*console.log(JSON.parse(this.GAIajax.responseText));*/
					this.accountowneravatar=JSON.parse(this.GAIajax.responseText).userInfo.avatr;
					this.accountownerid=JSON.parse(this.GAIajax.responseText).userInfo.userId;
				}
			},
			Accountowneridtoname(){
				var id = this.accountownerid;
				var headerToken=this.token;
				this.GANajax = new XMLHttpRequest();
				this.GANajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByID?user_id="+id, true);
				this.GANajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GANajax.onreadystatechange = this.GANsuccessfully;
				this.GANajax.send();
			},
			GANsuccessfully(){
				if (this.GAIajax.readyState == 4 && this.GAIajax.status == 200){
					/*console.log(JSON.parse(this.GANajax.responseText));*/
					this.accountowneravatar=JSON.parse(this.GANajax.responseText).userInfo.avatr;
					this.accountownername=JSON.parse(this.GANajax.responseText).userInfo.nickName;
				}
			},
			addArticlelike(){
				var name = this.articleownername;
				var title = this.title;
				var headerToken=this.token;
				this.AALajax = new XMLHttpRequest();
				this.AALajax.open("POST", "http://139.224.255.43:7779/Article/addArticleLikeByTitle?nick_name="+name+"&title="+title, true);
				this.AALajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.AALajax.onreadystatechange = this.AALsuccessfully;
				this.AALajax.send();
			},
			AALsuccessfully(){
				if (this.AALajax.readyState == 4 && this.AALajax.status == 200){
					if(JSON.parse(this.AALajax.responseText).returnFlag==1){
						this.articlepraisenum=this.articlepraisenum+1;
						this.praiselimit=2;
						this.$alert('点赞成功！', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.AALajax.responseText).returnFlag==4){
						this.praiselimit=2;
						this.$alert('不能重复点赞..', '提示', {
						          confirmButtonText: '确定'
						        });
					}
				}
			},
			addArticlecollect(){
				var name = this.articleownername;
				var title = this.title;
				var headerToken=this.token;
				this.ADCajax = new XMLHttpRequest();
				this.ADCajax.open("POST", "http://139.224.255.43:7779/Article/addArticleCollectByTitle?nick_name="+name+"&title="+title, true);
				this.ADCajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.ADCajax.onreadystatechange = this.ADCsuccessfully;
				this.ADCajax.send();
			},
			ADCsuccessfully(){
				if (this.ADCajax.readyState == 4 && this.ADCajax.status == 200){
					/*console.log(this.ADCajax.responseText);*/
				}
			},
			myComlastpage(){
				if(this.selectedPage>1){
					this.selectedPage=this.selectedPage - 1;
					this.getComment();
				}
			},
			myComnextapage(){
				if(this.selectedPage<this.maxcommentpage){
					this.selectedPage=this.selectedPage + 1;
					this.getComment();
				}
			},
			/*turntopage(page){
					this.selectedPage=page;
					this.getComment();
			},*/
			DeleteComment(crid){
				var name = this.accountownername;
				var commentid = crid;
				var headerToken=this.token;
				this.DCajax = new XMLHttpRequest();
				this.DCajax.open("POST", "http://139.224.255.43:7779/Article/deleteArticleCommentByID?nick_name="+name+"&articlecommentID="+commentid, true);
				this.DCajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.DCajax.onreadystatechange = this.DCsuccessfully;
				this.DCajax.send();
			},
			DCsuccessfully(){
				if (this.DCajax.readyState == 4 && this.DCajax.status == 200){
					/*console.log(JSON.parse(this.DCajax.responseText).returnFlag);*/
					if(JSON.parse(this.DCajax.responseText).returnFlag==1){
						this.getCommentNum();
						if(this.selectedPage>=this.maxcommentpage){
							this.selectedPage--;
						}
						this.getComment();
						this.$alert('评论删除成功！', '标题名称', {
						          confirmButtonText: '确定'
						        });
					}
					else if(JSON.parse(this.DCajax.responseText).returnFlag==3){
						this.$alert('未找到该文章...', '标题名称', {
						          confirmButtonText: '确定'
						        });
					}
						
				}
			},
			addSonComment(comid){
				if(this.inputcomment!=""&&this.inputcomment.length<=10){
					var name=this.accountownername;
					var commentid =comid;
					var comtocom=this.inputcomment;
					var headerToken=this.token;
					this.ASCajax = new XMLHttpRequest();
					this.ASCajax.open("POST", "http://139.224.255.43:7779/Article/createCommentCommentByID?nick_name="+name+"&articlecommentID1="+commentid+"&content="+comtocom, true);
					this.ASCajax.setRequestHeader('Authorization','Bearer '+headerToken);
					this.ASCajax.onreadystatechange = this.ASCsuccessfully;
					this.ASCajax.send();
				}
				else if(this.inputcomment==""){
					this.$alert('评论不能为空！！', '警告', {
					          confirmButtonText: '确定'
					        });
				}
				else if(this.inputcomment.length>10){
					this.$alert('评论不能超过10个字符！！', '警告', {
					          confirmButtonText: '确定'
					        });
				}
			},
			ASCsuccessfully(){
				if (this.ASCajax.readyState == 4 && this.ASCajax.status == 200){
					this.dialogFormVisible = false;
					this.getComment();
					this.getCommentNum();
					this.$alert('回复评论成功！', '标题名称', {
					          confirmButtonText: '确定'
					        });
					this.inputcomment="";
				}
			},
			getZoneName(id){
						   var zoneid=id;
						   var headerToken=this.token;
						   this.GZNajax = new XMLHttpRequest();
						   this.GZNajax.open("POST", "http://139.224.255.43:7779/Zone/getZoneName?zoneID="+zoneid, true);
						   this.GZNajax.setRequestHeader('Authorization','Bearer '+headerToken);
						   this.GZNajax.onreadystatechange = this.GZNsuccessfully;
						   this.GZNajax.send();  
			},
			GZNsuccessfully(){
				if (this.GZNajax.readyState == 4 && this.GZNajax.status == 200){
					this.tag=JSON.parse(this.GZNajax.responseText).zoneName;
				}
						   
						   console.log(this.GZNajax.responseText);
						   console.log(JSON.parse(this.GZNajax.responseText));
			},
			getFolderlist(){
				var id=this.accountownerid;
				var headerToken=this.token;
				this.GFLajax = new XMLHttpRequest();
				this.GFLajax.open("POST", "http://139.224.255.43:7779/Favourite/getFolderList?userID="+id, true);
				this.GFLajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GFLajax.onreadystatechange = this.GFLsuccessfully;
				this.GFLajax.send();  
			},
			GFLsuccessfully(){
				if (this.GFLajax.readyState == 4 && this.GFLajax.status == 200){
					console.log(JSON.parse(this.GFLajax.responseText).list);
					var receive=JSON.parse(this.GFLajax.responseText).list;
					this.Favoritelist=receive;
					if(this.isMenuShow==true){
						this.$alert('请选择收藏夹', '提示', {
						          confirmButtonText: '确定'
						        });
					}
				}
			},
			reflashpage(title,author){
				this.title=title;
				this.articleownername=author;
				this.getArticle();
				this.getComment();
				this.getHottestArticle();
				this.getArticleownerInfo();
				this.getAccountownerInfo();
				this.getCommentNum();
				this.praiselimit=1;
				this.collectlimit=1;
				this.praisecomlimit=1;
				
				
			},
			getCommentNum(){
				var title=this.title;
				var headerToken=this.token;
				this.GCNajax = new XMLHttpRequest();
				this.GCNajax.open("POST", "http://139.224.255.43:7779/Article/getcommentNumbytitle?title="+title, true);
				this.GCNajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GCNajax.onreadystatechange = this.GCNsuccessfully;
				this.GCNajax.send();  
			},
			GCNsuccessfully(){
				if (this.GCNajax.readyState == 4 && this.GCNajax.status == 200){
					var receive=JSON.parse(this.GCNajax.responseText).commentNUM;
					this.maxcommentpage=receive/5;
					
				}
			}
			// getparentcom(arid){
			// 	var id=arid;
			// 	var headerToken=this.token;
			// 	this.GPCajax = new XMLHttpRequest();
			// 	this.GPCajax.open("POST", "http://139.224.255.43:7779/Article/getparentCRByID?articleCRid="+id, true);
			// 	this.GPCajax.setRequestHeader('Authorization','Bearer '+headerToken);
			// 	this.GPCajax.onreadystatechange = this.GPCsuccessfully;
			// 	this.GPCajax.send(); 
			// },
			// GPCsuccessfully(){
			// 	if (this.GPCajax.readyState == 4 && this.GPCajax.status == 200){
			// 		var it=JSON.parse(this.GPCajax.responseText).item;
			// 		console.log(it[0].content);
					
			// 	}
			// }
	       },
	   
	    }
</script>

<style>
	#ViewArticle-page{
		margin: 0 auto;
		width: 1500px;
		height: 1500px;
				
		font-family: Microsoft YaHei;
				
		background: #E5E5E5;
		
	
	}
	.ViewArticle-personal{
		position: absolute;
		width: 303px;
		height: 154px;
		left:50%;
		    margin-left: -713px;
		margin-top: 81px;
		background-color: #ffffff;
	}
	#ViewArticle-photo{
		position:absolute;
		width: 45px;
		height: 45px;
		left: 22px;
		top: 26px;
		
		
	}
	#ViewArticle-name{
		position: absolute;
		width: 148px;
		height: 20px;
		left: 98px;
		top: 19px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 14px;
		line-height: 18px;
		display: flex;
		letter-spacing: 0.75px;
		
		color: #000000;
	
	}
	#ViewArticle-sign{
		position: absolute;
		width: 151px;
		height: 20px;
		left: 98px;
		top: 47px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 14px;
		line-height: 18px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		text-transform: uppercase;
		
		color: #767373;
	
	}
	#ViewArticle-focus{
		position: absolute;
		width: 75px;
		height: 24px;
		left: 16px;
		top: 103px;
		background-color: #DA4646;
		color: #ffffff;
		border: none;
	}
	#ViewArticle-prv-let{
		position: absolute;
		width: 76px;
		height: 24px;
		left: 106px;
		top: 103px;
		background-color: #aaaa7f;
		color: #550000;
		border: none;
	}
	#ViewArticle-per-cen{
		position: absolute;
		width: 76px;
		height: 24px;
		left: 202px;
		top: 103px;
		background-color: #aaaa7f;
		color: #550000;
		border: none;
	}
	.ViewArticle-left-aside-list-item{
		padding: 5px 10px;
		white-space: nowrap;
		text-overflow: ellipsis;
		overflow: hidden;
	}
	
	.ViewArticle-HowPost{
		position: absolute;
		width: 302px;
		height: 344px;
		left:50%;
		    margin-left: -713px;
		top: 267px;
		
		background: #FFFFFF;
		border-radius: 20px;
	}
	#ViewArticle-HowPost-title-box{
		position: absolute;
		width: 120px;
		height: 56px;
		left: 0px;
		top: 0px;
		
		background: #DA4646;
		border-radius: 0px 0px 30px 0px;
	}
	#ViewArticle-HowPost-title{
		position: absolute;
		width: 230px;
		height: 37px;
		left: 22px;
		top: 10px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 18px;
		line-height: 24px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		
		color: #FFFFFF;
	}
	#ViewArticle-list2{
		margin-top: 24%;
		margin-left: 12%;
		text-align: left;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 14px;
		line-height: 18px;
		letter-spacing: 0.75px;
		
		color: #363535;
	}
	
	#ViewArticle-content-container{
		position: absolute;
		width: 1010px;
		height: 561px;
		left:50%;
		    margin-left: -378px;
		top: 80px;
		
		background: #ffffff;
	}
	#ViewArticle-content-title{
		position: absolute;
		width: 471px;
		height: 46px;
		left: 26px;
		top: 26px;
		
		text-align: left;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 24px;
		line-height: 32px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		white-space: nowrap;
		    text-overflow: ellipsis;
		    overflow: hidden;
		
		color: #000000;
	}
	#ViewArticle-content-data{
		position: absolute;
		width: 925px;
		height: 39px;
		left: 41px;
		top: 66px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 24px;
		line-height: 32px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		color: #000000;
	}
	#ViewArticle-time-logo{
		width: 20px;
		height: 20px;
		margin: 0;
		
		background-image: url('../assets/article/timelogo.jpg');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#ViewArticle-content-time{
		margin: 0;
		font-size: 15px;
		font-family: Microsoft YaHei;
		font-style: normal;
		color: gray;
	}
	#ViewArticle-praise-logo{
		width: 20px;
		height: 20px;
		margin: 0;
		
		background-image: url('../assets/article/hand.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#ViewArticle-praise-text{
		margin: 0;
		font-size: 15px;
		font-family: Microsoft YaHei;
		font-style: normal;
		color: gray;
	}
	#ViewArticle-collect-logo1{
		width: 20px;
		height: 20px;
		margin: 0;
		
		background-image: url('../assets/article/star.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#ViewArticle-collect-rext{
		margin: 0;
		font-size: 15px;
		font-family: Microsoft YaHei;
		font-style: normal;
		color: gray;
	}
	#ViewArticle-article-text{
		position:absolute;
		width: 900px;
		height: 350px;
		left: 54px;
		top: 135px;
		
		display: -webkit-box;
		 -webkit-line-clamp: 2;
		 -webkit-box-orient: vertical;
		 text-overflow: ellipsis;
		 overflow: hidden;
		 word-break: break-word;
		 
		font-size: 16px;
        text-align: left;
	}
	
	
	#ViewArticle-target-logo{
		position: absolute;
		width: 20px;
		height: 20px;
		left: 39px;
		top: 112px;
		
		background-image: url('../assets/article/tag.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 12px 12px 0px;
	}
	#ViewArticle-target{
		position: absolute;
		width: 55px;
		height: 20px;
		left: 64px;
		top: 110px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 13px;
		line-height: 17px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		
		color: #6A6969;
	}
	#ViewArticle-bottom{
		position: absolute;
		width: 1010px;
		height: 37px;
		left: 0px;
		top: 524px;
		
		background: #9B9797;
	}
	#ViewArticle-agree-logo{
		position: absolute;
		width: 15px;
		height: 15px;
		left: -20px;
		top: 2px;
		
		background-image: url('../assets/article/点赞2.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#ViewArticle-agreed-logo{
		position: absolute;
		width: 15px;
		height: 15px;
		left: -20px;
		top: 1px;
		
		background-image: url('../assets/article/点赞3.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#ViewArticle-agree{
		position: absolute;
		width: 104px;
		height: 17px;
		left: 51px;
		top: 11px;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 14px;
		line-height: 18px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		
		color: #E5E5E5;
	}
	#ViewArticle-collect-logo{
		position: absolute;
		width: 15px;
		height: 15px;
		left: -20px;
		top: 2px;
		background-image: url('../assets/article/收藏2.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;	
	}
	#ViewArticle-collected-logo{
		position: absolute;
		width: 15px;
		height: 15px;
		left: -20px;
		top: 2px;
		background-image: url('../assets/article/收藏3.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;	
	}
	#ViewArticle-collect{
		position: absolute;
		width: 91px;
		height: 16px;
		left: 201px;
		top: 11px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 14px;
		line-height: 18px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		text-transform: uppercase;		
		color: #E5E5E5;
	}
	
	.ViewArticle-comment-container-larger{
		position: absolute;
		width: 1010px;
		height: 95px;
		left:50%;
		    margin-left: -378px;
		top: 668px;
		
		background: #FFFFFF;
	}
	#ViewArticle-comment-edit{
		position: absolute;
		width: 1010px;
		height: 10px;
		left: 0px;
		top: 0px;
		
		background: #FFFFFF;
	}
	#ViewArticle-my-person-logo{
		position:absolute;
		width: 45px;
		height: 45px;
		left: 26px;
		top: 20px;
		
	}
	#ViewArticle-comment-container{
		position: absolute;
		width: 751px;
		height: 45px;
		left: 85px;
		top: 19px;
		
		background: #F7F7F7;
		border: 1px solid #8B8888;
		box-sizing: border-box;
	}
	#ViewArticle-add-comment{
		position: absolute;
		width: 138px;
		height: 45px;
		left: 852px;
		top: 19px;
		color: #FFFFFF;
		background: #DA4646;
		border-radius: 4px;
	}
	#ViewArticle-comment-list{
		position: absolute;
		width:1010px;
		height: 400px;
		left: 0px;
		top: 85px;
		display: flex;
		flex-direction: column;
	}
	.ViewArticle-c-item{
		display: flex;
		padding: 12px 0px;
		width: 1010px;
		border-bottom-width: 1px ;
		border-bottom-style: solid;
		border-bottom-color: #969292;
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
		cursor: pointer;
	}
	.ViewArticle-citem-person-logo{
		flex-shrink: 0;
		margin: 0;
		margin-left: 27px;
	    width: 45px;
	    height: 45px;
	    border-radius: 30px;
	}
	.ViewArticle-citem-comment-text{
		flex-shrink: 0;
		width:177px;
		margin: 0;
	    margin-left: 20px;
		margin-top: 15px;
		line-height: 30px;
		font-size: 14px;
		text-align: left;
		background: #FFFFFF;
	}
	.ViewArticle-citem-comment-tar{
		display: inline-block;
		width: 200px;
		margin: 0;
		margin-top: 4%;
		font-size: 14px;
		line-height: 25px;	
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		text-align: left;
		color: #999494;
	}
	.ViewArticle-citem-comment-tar-p{
		margin: 0;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
	.ViewArticle-citem-comment-tar-not{
		display: inline-block;
		width: 200px;
		margin: 0;
		margin-top: 4%;
		font-size: 14px;
		line-height: 25px;	
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		text-align: left;
	}
	.ViewArticle-citem-comment-tar-p-not{
		margin: 0;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
		opacity:0;
	}
	.ViewArticle-citem-comment-time{
		display: inline-block;
		margin-top: 4.7%;
		margin-right: 120px;
		height:35px;
		width: fit-content;
			
		font-size: 12px;
		line-height: 15px;	
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		color: #999494;
	}
	.ViewArticle-citem-comment-time-p{
		margin: 0;
	}
	.ViewArticle-citem-comtocom{
		margin-top: 3.4%;
		margin-right: 210px;
		height:35px;
		margin-left: 0px;
	}
	.ViewArticle-citem-praise-logo{
		margin: 0;
		margin-right: 10px;
		margin-top: 43px;
		width: 20px;
		height: 20px;
		border-radius: 30px;
		
	}
	.ViewArticle-citem-praise{
		margin: 0;
		margin-right: 140px;
		margin-top: 43px;
	}
	.ViewArticle-citem-praised{
		margin: 0;
		margin-right: 140px;
		margin-top: 43px;
		color: red;
	}
	
	.ViewArticle-citem-comment-delete{
		position:relative;
		margin: 0;
		right:70px;
		top:30px;

		width: fit-content;
			
		font-size: 18px;
		line-height: 25px;
		text-decoration-line: underline;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		color: #999494;
	}
	.ViewArticle-citem-comment-delete-not{
		position:relative;
		margin: 0;
		right:70px;
		top:30px;
		
		opacity:0;
		width: fit-content;
			
		font-size: 18px;
		line-height: 25px;
		text-decoration-line: underline;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		color: #ffffff;
	}
	
	#ViewArticle-comment-page-controller{
		position: absolute;
		width: 982px;
		height: 100px;
		left:50%;
		    margin-left: -378px;
		top: 1278px;
		
		
	  display: flex;
	  align-items: center;
		margin-top: 20px;
		font-size: 18px;
	  line-height: 18px;
	}
	#ViewArticle-c-last-page{
	  flex-grow: 1;
		margin-right: 60px;
	  text-align: right;
	  cursor: pointer;
	}
	.ViewArticle-c-page-num{
	  display: flex;
	  justify-content: center;
	  align-items: center;
		width: 40px;
		height:40px;
		color: #727272;
		border-radius: 5px;
	  cursor: pointer;
	}
	.ViewArticle-c-page-num:hover{
	  background: rgba(114, 114, 114, 0.2);
	}
	.ViewArticle-c-page-num-selected{
		background: rgba(114, 114, 114, 0.3);
	}
	#ViewArticle-c-next-page{
	  flex-grow: 1;
		margin-left: 60px;
	  text-align: left;
	  cursor: pointer;
	}
	

	#ViewArticle-praised{
		color: #DA4646 ;
	}
	#ViewArticle-collected{
		color: #FFDA15 ;
	}
	#top-menu{
		position: absolute;
		width: 100px;
		height:300px;
		left: 581px;
		top: 642px;
		overflow-y: auto;
		
		z-index:999;
		
		background: #9B9797;
		
			
		}
	.t-m-button{
			display: block;
			width: calc(100% - 10px);
			height: 54px;
			
			border: none;
			margin: 4px 5px;
			
			text-align: center;
			line-height: 54px;
			font-size: 18px;
			color: #FFFFFF;
			background: #9B9797;
			border-radius: 6px;
			
			background: #9B9797;
			
			white-space: nowrap;
			text-overflow: ellipsis;
	 }	
	 
	 a {
	   text-decoration: none;
	 }
	 .router-link-active {
	   text-decoration: none;
	 }
	 #ViewArticle-hotarticlename{
		 color: #00B0E8;
		 /*实现超过一行的显示省略号*/
		     white-space: nowrap;
		     text-overflow: ellipsis;
		     overflow: hidden;
	 }
</style>
