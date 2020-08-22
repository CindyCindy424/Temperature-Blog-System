<template>
	<div id="top-nav">
		<img id="logo" src="../assets/logo.png" alt="logo"/>
		<span id="title">Temperature</span>
		<input id="search-box" type="text" />
		<div id="search-button"></div>
		<span id="msg-ctr">消息中心</span>
		<span id="fav">收藏夹</span>
		<span id="create"><span>创作</span><img/></span>
		<img id="avatar" src="" alt="用户">
		<span id="sign-in-up">登录|注册</span>
	</div>
</template>

<script>
	export default{
		name:"topNav"
	}
</script>

<style>
	*{
		margin: 0px;
		padding: 0px;
	}
	#top-nav {
		position: fixed;
		width: 1440px;
		height: 59px;
		left: 0px;
		top: 0px;
		
		background: rgba(48, 46, 46, 0.77);
	}
	#logo{
		position:absolute;
		width: 38px;
		height: 38px;
		left: 26px;
		top: 10px;
	}
	#title{
		position:absolute;
		width: 173px;
		height: 33px;
		left: 81px;
		top: 13px;
		
		font-family: Lucida Handwriting;
		font-style: italic;
		font-weight: normal;
		font-size: 24px;
		line-height: 33px;
		
		color: #FFFFFF;
	}
	#search-box{
		position:absolute;
		width: 339px;
		height: 35px;
		left: 417px;
		top: 12px;
		
		background: #E8E2E2;
		border-radius: 30px;
		border-width: 0px;
	}
	#search-button{
		position:absolute;
		position: relative;
		width: 84px;
		height: 35px;
		left: 672px;
		top: 12px;
		
		background-image: url('../assets/search.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	#msg-ctr{
		position:absolute;
		width: 72px;
		height: 24px;
		left: 832px;
		top: 17px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		/* identical to box height */
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
	}
	#fav{
		position:absolute;
		width: 54px;
		height: 24px;
		left: 969px;
		top: 17px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		/* identical to box height */
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
	}
	#create{
		position:absolute;
		width: 111px;
		height: 59px;
		left: 1080px;
		top: 0px;
		
		background: rgba(254, 6, 6, 0.6);
	}
	#create>span{
		position:absolute;
		width: 36px;
		height: 24px;
		left: 34px;
		top: 17px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		/* identical to box height */
		
		color: #FFFFFF;
	}
	#avatar{
		position: absolute;
		width: 33px;
		height: 34px;
		left: 1244px;
		top: 15px;
	}
	#sign-in-up{
		position: absolute;
		width: 88px;
		height: 24px;
		left: 1304px;
		top: 20px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		
		text-decoration-line: underline;
		
		color: #FFFFFF;
	}
</style>
