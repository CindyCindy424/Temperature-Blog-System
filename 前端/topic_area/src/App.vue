<template>
	<top-nav>
		<img id="logo" src="../dist/img/logo.png" alt="logo"/>
		<span id="title">Temperature</span>
		<input id="search-box" type="text" />
		<div id="search-button"></div>
		<span id="msg-ctr">消息中心</span>
		<span id="fav">收藏夹</span>
		<span id="create"><span>创作</span><img/></span>
		<img id="avatar" src="../dist/img/logo.png" alt="用户">
		<span id="sign-in-up">登录|注册</span>
	</top-nav>
	<div id="page">
		<img id="photo" src="../dist/img/banner.png" alt="照片">
		<div>
			<div id="latest-topics">
				<div id="latest-topics-title">
					<span>最新话题</span>
					<input id="topics-search-box" type="text" placeholder="                      搜索话题"/>
					<div id="topics-search-button"></div>
				</div>
			</div>
			<div id="ask-question">我要提问</div>
			<my-topics>
				<span class="r-aside-subtitle">我的话题</span>
				<div id="my-topisc-msg">
					我发起了<span>{{myTopicsNum}}</span>个话题，<br />
					收获了<span>{{myTopicsAnsNum}}</span>个回答
				</div>
				<div id="my-topics-list"></div>
			</my-topics>
			<div id="hottest-topics">
				<span class="r-aside-subtitle">最热话题</span>
			</div>
		</div>
	</div>
</template>

<script>
	export default {
		name: 'App',
		props:{
			myTopicsNum: {
				type: Number,
				default: 40
			},
			myTopicsAnsNum: {
				type: Number,
				default: 0
			}
		},
		data() {
			return 0;
		}
	}
</script>

<style>
*{
	margin: 0px;
	padding: 0px;
}
top-nav {
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
	
	background-image: url('../dist/img/search.png');
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
	/* identical to box height */
	
	text-decoration-line: underline;
	
	color: #FFFFFF;
}
#page{
	position: fixed;
	z-index: -10;
	width: 1440px;
	height: 100%;
	background: #E5E5E5;
	overflow: auto;
}
#photo{
	position: absolute;
	width: 1440px;
	height: 352px;
	left: 0px;
	top: 0px;
}
#ask-question{
	position: absolute;
	width: 363px;
	height: 51px;
	left: 993px;
	top: 369px;
	
	background: #B23535;
	border-radius: 15px;
	
	font-family: Microsoft YaHei;
	font-style: normal;
	font-weight: normal;
	font-size: 20px;
	line-height: 51px;
	text-align: center;
	
	color: #FFFFFF;
}
my-topics{
	position: absolute;
	width: 363px;
	left: 996px;
	top: 442px;
}
.r-aside-subtitle{
	position: absolute;
	width: 350px;
	height: 54px;
	padding: 0px 0px 0px 13px;
	z-index: -1;
	
	background: #FFFFFF;
	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
	
	font-family: Microsoft YaHei;
	font-style: normal;
	font-weight: normal;
	font-size: 24px;
	line-height: 32px;
	line-height: 54px;
	
	letter-spacing: 0.75px;
	text-transform: uppercase;
	
	color: #B23535;
}
#my-topisc-msg{
	position: absolute;
	width: 329px;
	height: 51px;
	top: 54px;
	z-index: -2;
	
	padding: 7px 0px 0px 34px;
	
	background: #FFFFFF;
	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
	
	font-family: Microsoft YaHei;
	font-style: normal;
	font-weight: normal;
	font-size: 14px;
	line-height: 18px;
	letter-spacing: 0.75px;
	text-transform: uppercase;
	
	color: #000000;
}
#my-topisc-msg>span{
	color: #B23535;
}
#my-topics-list{
	position: absolute;
	top: 112px;
	width: 100%;
	height: 30px;
	z-index: -3;
	
	background: #FFFFFF;
	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
}
#hottest-topics{
	position: absolute;
	width: 363px;
	left: 996px;
	top: 910px;
	
	background: #FFFFFF;
	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
}

#latest-topics{
	position: absolute;
	width: 870.14px;
	left: 68.43px;
	top: 369px;
}
#latest-topics-title{
	width: 100%;
	height: 91px;
	
	background: #FFFFFF;
	box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
}
#latest-topics-title>span{
	position: absolute;
	left: calc(83.88px - 68.43px);
	
	font-family: Microsoft YaHei;
	font-style: normal;
	font-weight: normal;
	font-size: 24px;
	line-height: 91px;
	display: flex;
	align-items: center;
	
	color: #000000;
}
#topics-search-box{
	position: absolute;
	width: 281.83px;
	height: 35px;
	left: calc(630.32px - 68.43px);
	top: calc(396px - 369px);
	line-height: 35px;
	
	background: #E8E2E2;
	border-radius: 8px;
	border-width: 0px;
}
#topics-search-button{
	position: absolute;
	width: 82.65px;
	height: 35px;
	left: calc(829.49px - 68.43px);
	top: calc(396px - 369px);
	
	background: url(../dist/img/search.png);
	background-size: contain;
	background-repeat: no-repeat;
	background-position: center;
	border-radius: 0px 30px 30px 0px;
}
</style>
