<template>
	<div id="top-nav">
		<img id="logo" src="../assets/logo.png" alt="logo"/>
		<span id="title">Temperature</span>
		<input id="search-box" type="text" />
		<div id="search-button"></div>
		<span id="msg-ctr">消息中心</span>
		<span id="fav">收藏夹</span>
		<span id="create" @click="topMenuToggle">创作<img src="../assets/menu.png" alt="按钮"/></span>
		<div id="top-menu" v-if="isMenuShow">
			<div id="t-m-publish-article" class="t-m-button" :style="{top: TMTop, left: MLeft + 'px'}">发表文章</div>
			<div id="t-m-publish-topic" class="t-m-button">发表话题</div>
			<div id="t-m-upload-image" class="t-m-button">上传图片</div>
		</div>
		<img id="avatar" src="../assets/avatar.png" alt="用户">
		<span id="sign-in-up">登录|注册</span>
	</div>
</template>

<script>
	export default{
		name:"topNav",
		methods:{
			topMenuToggle(){
				this.isMenuShow = this.isMenuShow == false?true:false;
			}
		},
		data(){
			return {
				isMenuShow: false
			};
		}
	}
</script>

<style>
	#top-nav {
		position: fixed;
		z-index: 10px;
		width: 100%;
		height: 59px;
		
		font-family: Microsoft YaHei;
		font-size: 18px;
		line-height: 59px;
		
		color: #FFFFFF;
		background: rgba(48, 46, 46, 0.77);
		
		user-select: none;
	}
	#logo{
		display: inline-block;
		vertical-align: top;
		width: 38px;
		height: 38px;
		margin: 0px 0px 0px 26px;
		padding: 10px 0px 11px 0px;
	}
	#title{
		display: inline-block;
		vertical-align: top;
		width: 173px;
		height: 100%;
		margin:0px 0px 0px 17px;
		
		font-family: Lucida Handwriting;
		font-style: italic;
		font-size: 24px;
	}
	#search-box{
		display: inline-block;
		vertical-align: top;
		width: 339px;
		height: 35px;
		
		margin-top: calc((59px - 35px) / 2);
		margin-left: calc((100% - 593px - 599px) / 2);
		
		background: #E8E2E2;
		border-radius: 30px;
		border-width: 0px;
	}
	#search-button{
		display: inline-block;
		vertical-align: top;
		width: 84px;
		height: 35px;
		
		margin-top: 12px;
		margin-left: -84px;
		margin-right: calc((100% - 593px - 599px) / 2);
		
		background-image: url('../assets/search.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
		
		cursor: pointer;
	}
	#msg-ctr{
		display: inline-block;
		vertical-align: top;
		height: 100%;
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
		
		cursor: pointer;
	}
	#fav{
		display: inline-block;
		vertical-align: top;
		height: 100%;
		margin-left: 65px;
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
		
		cursor: pointer;
	}
	#create{
		display: inline-block;
		vertical-align: top;
		height: 100%;
		width: 70px;
		margin-left: 57px;
		padding: 0px 20px 0px 24px;
		
		background: rgba(254, 6, 6, 0.6);
		
		cursor: pointer;
	}
	#create>img{
		display: inline-block;
		vertical-align: top;
		width: 20px;
		height: 20px;
		margin: calc((59px - 20px) / 2) 0px calc((59px - 20px) / 2) 14px;
	}
	#top-menu{
		display: inline-block;
		margin: 62px -8px 0px -122px;
		width: 125px;
		
		background: rgba(48, 46, 46, 0.77);
	}
	.t-m-button{
		display: block;
		width: calc(100% - 10px);
		height: 54px;

		
		margin: 4px 5px;
		
		text-align: center;
		line-height: 54px;
		font-size: 18px;

		color: #FFFFFF;
		background: rgba(48, 46, 46, 0.77);
		border-radius: 6px;
		
		background: rgba(48, 46, 46, 0.77);
		
		cursor: pointer;
	}
	#avatar{
		display: inline-block;
		vertical-align: top;
		width: 33px;
		height: 33px;
		margin: 13px 0px 13px 13px;
		
		border-radius: 17px;
	}
	#sign-in-up{
		display: inline-block;
		vertical-align: top;
		width: 88px;
		height: 100%;
		margin-left: 27px;
		margin-right: 27px;
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
		
		cursor: pointer;
	}
</style>
