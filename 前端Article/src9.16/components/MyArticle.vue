<template>
	<div id="MyArticle-page">
		<div id="MyArticle-photo">
			<img src="../assets/article/banner.png" alt="照片">
		</div>
		<div id="MyArticle-personal">
			    <div>
					<img id="MyArticle-head-photo" :src="this.articleowneravatar==undefined?'':loc+this.articleowneravatar" />
				</div>
				<div>
					<div id="MyArticle-name">{{nickname}}</div>
					<div id="MyArticle-sign">{{personalsign}}</div>
				</div>
				<button id="MyArticle-see">查看</button>
				<div id="MyArticle-person-info">
					<span id="MyArticle-focus">关注：{{focusnum}}</span>
					&nbsp;&nbsp;&nbsp;&nbsp;
					<span>|</span>
					&nbsp;&nbsp;&nbsp;&nbsp;
					<span id="MyArticle-fans">粉丝：{{fansnum}}</span>
				</div>
		</div>
		<div id="MyArticle-search-container">
			<div id="MyArticle-search-box">
				<input id="MyArticle-search-info" placeholder="搜索我的文章..." type="text" />
				<div id="MyArticle-search-button"></div>
			</div>
		</div>
		<div class="MyArticle-hottest-article">
				   <div id="MyArticle-article-title-box"></div>
				   <span id="MyArticle-hottest-article-title">最热文章</span>
				   <div id="MyArticle-article-list">
					   <div class="MyArticle-left-aside-list-item" v-for="item in HottestArticle">
					     <router-link :to="{path:'/ViewArticle',query:{ViewArticleTitle:item.title,ViewArticleNickname:item.authorname,ViewArticleUsername:nickname}}"><span id="MyArticle-hotarticlename">{{item.title}}</span></router-link>
					   </div>
				   </div>
		</div>
		<!--<div class="LatestComment">
				   <div id="LatestComment-title-box"></div>
				   <span id="LatestComment-title">最新评论</span>
				   <div id="comment-list">
					   <div class="left-aside-list-item" v-for="item in LatestComment">
					     {{item.Nick_name}}:{{item.Article_cr_content}}
					   </div>
				   </div>
		</div>-->
		<div id="MyArticle-article-info-container">
			<div id="MyArticle-article-info">
				<span id="MyArticle-article-num">我的文章:{{myarticlenum}}     </span>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<span>|</span>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<span id="MyArticle-total-comment">收到评论:{{receivedcommentnum}}    </span>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<span>|</span>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<span id="MyArticle-total-praise">收到点赞:{{receivedpraisenum}}    </span>
			</div>
		</div>
		<div id="MyArticle-container">
			<div class="MyArticle-article-container" v-for="item in MyArticle" :key="item.article_id">
				<router-link :to="{path:'/ViewArticle',query:{ViewArticleTitle:item.title,ViewArticleNickname:item.User_name,ViewArticleUsername:item.User_name}}">
					<div class="MyArticle-article-title">{{item.title}}</div>
					<div class="MyArticle-article-summary">{{item.articlcontent}}</div>
				</router-link>
				<div class="MyArticle-publish-date">{{item.uploadtime}}</div>
				<div class="MyArticle-comment-logo"></div>
				<div class="MyArticle-comment-num">{{item.commentnum}}</div>
				<span class="MyArticle-delete" @click="DeleteArticle(item.title)">删除</span>
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
	       name:'myarticle',
	       data(){
	       	return{
				loc:'http://139.224.255.43:7779/',
				token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiJhZDljN2FmYi03MjhiLTQ3ZDEtOGJmNC0yOWRhMWZmODE1YWMiLCJleHAiOjE2MDAyNjY1NjIsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.38y9RBwfqP0w6xhVRwQLio27H_ASiSVvGxv4RYtkwPE",
				nickname:"",
				userid:0,
				personalsign:"心如止水",
				articleowneravatar:"",
				focusnum:-1,
				fansnum:-1,
				myarticlenum:233,
				receivedcommentnum:123,
				receivedpraisenum:3456,
				GLCajax:0,
				GABajax:0,
				GHAajax:0,
				HottestArticle:[],
				LatestComment:[],
	       		MyArticle: [],
	       		/*pageNums:[
	       					{page: 1},
	       					{page: 2},
	       					{page: 3}
	       		],*/
	       		selectedPage: 1,
				maxpageNum:1,
	       	}
	       },
		   created:function(){
			this.getQuery();
			if(this.nickname!=""){
				this.getUserInfo();
			}
			if(this.userid!=""){
				this.UserIdToname();
			}
		   	this.getFocusNum();
			this.getFansNum();
			this.getHottestArticle();
			/*this.getLatestComment();*/
			this.getLikesReplyArticleNum();
			this.getArticleByPage();
		   },
		   methods:{
			     getToken:function(){
			       this.token=document.cookie.split(";")[0].split("=")[1];
			       },
			   getQuery: function () {
			    this.userid = this.$route.query.MyArticleid;
				this.nickname = this.$route.query.MyArticlename;
			   },
			   DeleteArticle(Articletitle){
				   var Nickname = this.nickname;
				   var title = Articletitle;
				   var headerToken=this.token;
				   this.DAajax = new XMLHttpRequest();
				   this.DAajax.open("POST", "http://139.224.255.43:7779/Article/deleteArticleByTitle?nick_name="+Nickname+"&title="+title, true);
				   this.DAajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.DAajax.onreadystatechange = this.DAsuccessfully;
				   this.DAajax.send();
			   },
			   DAsuccessfully(){
				   if (this.DAajax.readyState == 4 && this.DAajax.status == 200) {
					   this.getLikesReplyArticleNum();
					   if(this.selectedPage>=this.maxpagenum){
						   this.selectedPage--;
					   }
					   this.getArticleByPage();
				     if(JSON.parse(this.DAajax.responseText).returnFlag==1){
						 this.$alert('成功删除文章！', '提示', {
						           confirmButtonText: '确定'
						          });
						
				     }
				   }
			   },
			   getFocusNum(){
				   console.log("???");
				   var Nickname = this.nickname;
				   var headerToken=this.token;
				   this.GFNajax = new XMLHttpRequest();
				   this.GFNajax.open("POST", "http://139.224.255.43:7779/Account/getFollowNum?nick_name="+Nickname, true);
				   this.GFNajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.GFNajax.onreadystatechange = this.GFNsuccessfully;
				   this.GFNajax.send();
			   },
			   GFNsuccessfully(){
				   if (this.GFNajax.readyState == 4 && this.GFNajax.status == 200){
					   this.focusnum=JSON.parse(this.GFNajax.responseText).followCnt;
				   }
			   },
			   getFansNum(){
				   var Nickname = this.nickname;
				   var headerToken=this.token;
				   this.GFanNajax = new XMLHttpRequest();
				   this.GFanNajax.open("POST", "http://139.224.255.43:7779/Account/getFanNum?nick_name="+Nickname, true);
				   this.GFanNajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.GFanNajax.onreadystatechange = this.GFanNsuccessfully;
				   this.GFanNajax.send();
			   },
			   GFanNsuccessfully(){
				   if (this.GFanNajax.readyState == 4 && this.GFanNajax.status == 200){
					   /*console.log(this.GFanNajax.responseText);
					   console.log(JSON.parse(this.GFanNajax.responseText));*/
				   		this.fansnum=JSON.parse(this.GFanNajax.responseText).fanCnt;
				   }
			   },
			   getUserInfo(){
			   	var name = this.nickname;
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
			   		this.nickname=JSON.parse(this.GUIajax.responseText).userInfo.nickName;
					this.personalsign=JSON.parse(this.GUIajax.responseText).userAnnouncement;
					this.articleowneravatar=JSON.parse(this.GUIajax.responseText).userInfo.avatr;
					this.userid=JSON.parse(this.GUIajax.responseText).userInfo.userId;
			   		/*this.personalsign=JSON.parse(this.GUIajax.responseText).userInfo.nickName*/
			    	}
			   },
			   UserIdToname(){
				   var id=this.userid;
				   var headerToken=this.token;
				   this.UITajax = new XMLHttpRequest();
				   this.UITajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByID?user_id="+id, true);
				   this.UITajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.UITajax.onreadystatechange = this.UITsuccessfully;
				   this.UITajax.send();
			   },
			   UITsuccessfully(){
				   if (this.UITajax.readyState == 4 && this.UITajax.status == 200){
					   this.nickname=JSON.parse(this.GUNajax.responseText).userInfo.nickName;
					   this.personalsign=JSON.parse(this.GUNajax.responseText).userAnnouncement;
					   this.articleowneravatar=JSON.parse(this.GUNajax.responseText).userInfo.avatr;
				   }
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
					console.log(this.GHAajax.responseText);
					console.log(JSON.parse(this.GHAajax.responseText));
					console.log(JSON.parse(this.GHAajax.responseText).articleDetail);
			   		var receive = JSON.parse(this.GHAajax.responseText).articleDetail;
			   		this.HottestArticle = receive;
			   	}
			   },
			   /*getLatestComment(){
				   var commentnum=8;
				   var name= this.nickname;
				   var headerToken=this.token;
				   this.GLCajax = new XMLHttpRequest();
				   this.GLCajax.open("POST", "http://139.224.255.43:7779/Article/getNewestComment?takecommentNum="+commentnum+"&nick_name="+name, true);
				   this.GLCajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.GLCajax.onreadystatechange = this.GLCsuccessfully;
				   this.GLCajax.send();
			   },
			   GLCsuccessfully(){
				   if (this.GLCajax.readyState == 4 && this.GLCajax.status == 200){
					   console.log(this.GLCajax.responseText);
					   console.log(JSON.parse(this.GLCajax.responseText));
					   var receivecomment=JSON.parse(JSON.parse(this.GLCajax.responseText).comments);
					   this.LatestComment=receivecomment;
				   }
			   },*/
			   myArtlastpage(){
				   if(this.selectedPage!=1){
					   this.selectedPage=this.selectedPage - 1;
					   this.getArticleByPage();
				   }
			   },
			   myArtnextapage(){
				   if(this.selectedPage<this.maxpagenum){
				   		this.selectedPage=this.selectedPage + 1;
						this.getArticleByPage();
				   }
			   },
			   /*turntopage(page){
			   		this.selectedPage=page;
					this.getArticleByPage();
			   },*/
			   getLikesReplyArticleNum(){
				   var id = this.userid;
				   var headerToken=this.token;
				   this.GANajax = new XMLHttpRequest();
				   this.GANajax.open("POST", "http://139.224.255.43:7779/Account/getUserLikesReplyArticleNum?id="+id, true);
				   this.GANajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.GANajax.onreadystatechange = this.GANsuccessfully;
				   this.GANajax.send();
			   },
			   GANsuccessfully(){
				   
				   if (this.GANajax.readyState == 4 && this.GANajax.status == 200){
					   /*console.log(this.GANajax.responseText);
					   console.log(JSON.parse(this.GANajax.responseText));*/
					   this.myarticlenum=JSON.parse(this.GANajax.responseText).articleCnt;
					   this.receivedcommentnum=JSON.parse(this.GANajax.responseText).replyCnt;
					   this.receivedpraisenum=JSON.parse(this.GANajax.responseText).likesCnt;
					   var maxpageNum=this.myarticlenum/8;
					   this.maxpagenum=maxpageNum;
					   console.log(this.maxpagenum);
				   }
			   },
			   getArticleByPage(){
				   var pagenum=this.selectedPage;
				   var pagesize=8;
				   var id =this.userid;
				   var headerToken=this.token;
				   this.GABajax = new XMLHttpRequest();
				   this.GABajax.open("POST", "http://139.224.255.43:7779/Article/getpersonalArticlebypage?pageNum="+pagenum+"&pageSize="+pagesize+"&userid="+id, true);
				   this.GABajax.setRequestHeader('Authorization','Bearer '+headerToken);
				   this.GABajax.onreadystatechange = this.GABsuccessfully;
				   this.GABajax.send();
			   },
			   GABsuccessfully(){
				   if (this.GABajax.readyState == 4 && this.GABajax.status == 200){
					   /*console.log(JSON.parse(this.GABajax.responseText).Result);*/
					   var receive = JSON.parse(JSON.parse(this.GABajax.responseText).Result);
					   this.MyArticle=receive; 
					   for(var i=0;i<this.MyArticle.length;i++){
						   this.MyArticle[i].uploadtime=this.MyArticle[i].uploadtime.replace("T"," ");
					   }
				   }
			   },
			  

		   }
	    }
</script>


<style>

	#MyArticle-page{
		margin: 0 auto;
		width: 1500px;
		height: 2800px;
				
		font-family: Microsoft YaHei;
				
		background: #E5E5E5;
	}
	#MyArticle-photo{
		width: calc(100% - 0px);
		height: 332px;
		overflow: hidden;
	}
	#MyArticle-photo>img{
		width: 1500px;
		margin-top: calc((0px - 600px + 352px) / 2 + 40px) ;	/*更换图片需要注意修改*/
	}
	#MyArticle-personal{
		position: absolute;
		width: 303px;
		height: 154px;
		left:50%;
		margin-left: -713px;
		margin-top: 51px;
		background-color: #ffffff;
	}
	#MyArticle-head-photo{
		position:absolute;
		width: 45px;
		height: 45px;
		left: 22px;
		top: 26px;
		
	}
	#MyArticle-name{
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
	#MyArticle-sign{
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
		
		white-space: nowrap;
		text-overflow: ellipsis;
		overflow: hidden;
		
		color: #767373;
	
	}
	#MyArticle-see{
		position: absolute;
		width: 58px;
		height: 24px;
		left: 226px;
		top: 90px;
		background-color: #DA4646;
		color: #ffffff;
		border: none;
	}
	#MyArticle-see:focus{
		outline:#000000;
	}
	#MyArticle-person-info{
		position: absolute;
		width: 177px;
		height: 20px;
		left: 30px;
		top: 93px;
		
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
	
	
	#MyArticle-search-container{
		position: absolute;
		width: 303px;
		height: 54px;
		left:50%;
		margin-left: -713px;
		top: 569px;
		
		background: #FFFFFF;
	}
	#MyArticle-search-box{
		position: absolute;
		width: 256px;
		height: 35px;
		left: 23px;
		top: 9px;
		
		background: #E8E2E2;
		border-radius: 8px;
	}
	#MyArticle-search-info{
		position: absolute;
		width: 188px;
		height: 18px;
		left: 18px;
		top: 9px;
		
		background-color:#E8E2E2 ;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 14px;
		line-height: 18px;
		display: flex;
		align-items: center;
		letter-spacing: 0.75px;
		text-transform: uppercase;
		border: none;/*input边框不显示*/
		
		color: #767373;
	}
	#MyArticle-search-info:focus{
		outline: none;
	}
	#MyArticle-search-button{
		position:absolute;
		width: 75.08px;
		height: 35px;
		left: 190px;
		top: 0px;
		
		background-image: url('../assets/article/search.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	
	.MyArticle-hottest-article{
		position: absolute;
		width: 302px;
		height: 375px;
		left:50%;
		    margin-left: -713px;
		top: 649px;
		
		background: #FFFFFF;
		border-radius: 20px;
	}
	#MyArticle-article-title-box{
		position: absolute;
		width: 120px;
		height: 56px;
		left: 0px;
		top: 0px;
		
		background: #DA4646;
		border-radius: 0px 0px 30px 0px;
	}
	#MyArticle-hottest-article-title{
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
		text-transform: uppercase;
		/*实现超过一行的显示省略号*/
		    white-space: nowrap;
		    text-overflow: ellipsis;
		    overflow: hidden;
		
		color: #FFFFFF;
	}
	#MyArticle-article-list{
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
	.MyArticle-left-aside-list-item{
		padding: 5px 10px;
		
		/*实现超过一行的显示省略号*/
		white-space: nowrap;
		text-overflow: ellipsis;
		overflow: hidden;
	}
	
	
	.MyArticle-LatestComment{
		position: absolute;
		width: 302px;
		height: 344px;
		left: 45px;
		top: 1067px;
		
		background: #FFFFFF;
		border-radius: 20px;
	}
	#MyArticle-LatestComment-title-box{
		position: absolute;
		width: 120px;
		height: 56px;
		left: 0px;
		top: 0px;
		
		background: #B23535;
		border-radius: 0px 0px 30px 0px;
	}
	#MyArticle-LatestComment-title{
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
		text-transform: uppercase;
		
		color: #FFFFFF;
	}
	#MyArticle-comment-list{
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
	
	#MyArticle-article-info-container{
		position: absolute;
		width: 982px;
		height: 91px;
		left:50%;
		    margin-left: -360px;
		top: 389px;
		
		background: #FFFFFF;
	}
	#MyArticle-article-info{
		margin-top: 3.5%;
		margin-left: 4%;
		width: 724px;
		height: 51px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: 290;
		font-size: 18px;
		line-height: 24px;
		display: flex;
		
		color: #000000;
	}
	
	
	#MyArticle-container{
		position: absolute;
		width: 982px;
		height: 1000px;
		left:50%;
		    margin-left: -360px;
		top: 485px;
		
		border: 1px solid #C4C4C4;
		box-sizing: border-box;
		border: none;
	}
	.MyArticle-article-container{
		width: 935px;
		margin-top: 5px;
		text-align: left;
		padding: 20px;
			
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
		cursor: pointer;
	}

	.MyArticle-article-title{
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
	.MyArticle-article-summary{
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
		    word-break: break-word;
	
	}
	.MyArticle-test{
		position: absolute;
		left:5px;
		top: 0px;
	}
	.MyArticle-publish-date{
		display: inline-block;
		width: max-content;
			
		font-size: 18px;
		line-height: 18px;
		color: #000000;
	}
	.MyArticle-publish-time{
		display: inline-block;
		margin-left: 20px;
		width: fit-content;
		font-size: 18px;
		line-height: 18px;
		color: #000000;
	}
	.MyArticle-comment-logo{
		display: inline-block;
		margin: 6px 0px 0px 10px;
		width: 20px;
		height: 20px;
		margin-left: 51px;
		margin-top: 10px;
		
		background-image: url('../assets/article/评论.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	.MyArticle-comment-num{
		display: inline-block;
		margin-left: 10px;
		width: fit-content;
			
		font-size: 18px;
		line-height: 18px;
			
		color: #000000;
	}
	.MyArticle-delete{
		display: inline-block;
		margin-left: 60px;
		width: fit-content;
			
		font-size: 18px;
		line-height: 25px;
		text-decoration-line: underline;
			
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		/* identical to box height */
		
		color: #999494;
	
	}
	/*#test1{
		position: absolute;
		width: 200px;
		height: 30px;
		left: 485px;
		top: 121px;
		color: #999494;
		 z-index:999;
	}*/
	
	
	#myarticle-page-controller{
		position: absolute;
		width: 982px;
		height: 100px;
		left:50%;
		    margin-left: -372px;
		top: 1858px;
		
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
	
	a {
	  text-decoration: none;
	}
	.router-link-active {
	  text-decoration: none;
	}
	#MyArticle-hotarticlename{
		color: #00B0E8;
	}
	
</style>
