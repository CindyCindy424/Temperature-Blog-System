<template>
	<div id="container">
		<TopNav></TopNav>
		<div id="page">
			<div id="photo">
				<img src="./assets/banner.png" alt="照片">
			</div>
			<div>
				<LatestTopics></LatestTopics>
				<div id="r-aside">
					<div id="ask-question">
					我要提问
					</div>
					<MyTopics></MyTopics>
					<HottestTopics></HottestTopics>
				</div>
			</div>
			<footer>
				Copyright &copy;2020 Temperature team, All Rights Reserved.<br />
				<span>举报中心       联系我们</span>
			</footer>
		</div>
	</div>
</template>

<script>
	import TopNav from "./components/TopNav.vue"
	import LatestTopics from "./components/LatestTopics.vue"
	import MyTopics from "./components/MyTopics.vue"
	import HottestTopics from "./components/HottestTopics.vue"
	
	export default {
		name: 'App',
		components:{
			TopNav,
			LatestTopics,
			MyTopics,
			HottestTopics
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
		text-align: left;
	}
	#container{
		width: 100%;
		height: 100%;
		overflow: auto;
	}
	#page{
		width: 100%;
		height: 100%;
		
		font-family: Microsoft YaHei;
		
		background: #E5E5E5;
	}
	#photo{
		width: calc(100% - 0px);
		height: 352px;
		overflow: hidden;
	}
	#photo>img{
		width: 100%;
		margin-top: calc((0px - 600px + 352px) / 2 + 40px) ;	/*更换图片需要注意修改*/
	}
	#r-aside{
		display: inline-block;
		vertical-align: top;
		width: 363px;
		
		margin-left: 57px;
		margin-top: 17px;
		
	}
	#ask-question{
		width: 100%;
		height: 51px;
		
		background: #B23535;
		border-radius: 15px;
		
		font-size: 20px;
		line-height: 51px;
		text-align: center;
		
		color: #FFFFFF;
	}
	footer{
		padding: 100px 0px;
		font-size: 18px;
		line-height: 25px;
		text-align: center;
		
		color: #999494;
	}
</style>
