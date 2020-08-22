<template>
	<div id="my-topics">
		<span class="r-aside-subtitle">我的话题</span>
		<div id="my-topisc-msg">
			我发起了<span>{{myTopicsNum}}</span>个话题，<br />
			收获了<span>{{myTopicsAnsNum}}</span>个回答
		</div>
		<div id="my-topics-list"></div>
	</div>
</template>

<script>
	export default{
		name: "MyTopic",
		data(){
			return{
				myTopicsNum: 40,
				myTopicsAnsNum: 0
			};
		}
	}
</script>

<style>
	#my-topics{
		position: absolute;
		width: 363px;
		left: 996px;
		top: 442px;
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
		line-height: 24px;
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
</style>
