<template>
	<div>
		<TopNav></TopNav>
		<div id="page">
			<img id="photo" src="./assets/banner.png" alt="照片">
			<div>
				<LatestTopics></LatestTopics>
				<div id="ask-question">
					我要提问
					</div>
				<MyTopics></MyTopics>
				<HottestTopics></HottestTopics>
			</div>
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
</style>
