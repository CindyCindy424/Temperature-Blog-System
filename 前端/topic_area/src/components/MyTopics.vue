<template>
	<div id="my-topics">
		<div id="my-topics-title">我的话题</div>
		<div id="my-topisc-msg">
			我发起了<span>{{myTopicsNum}}</span>个话题，<br />
			收获了<span>{{myTopicsAnsNum}}</span>个回答
		</div>
		<div id="my-topics-list">
			<div id="m-t-latest">最近话题</div>
		</div>
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
		width: 100%;
		margin-top: 22px;
	}
	#my-topics-title{
		height: 54px;
		padding-left: 13px;
		
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
		
		font-size: 24px;
		line-height: 54px;
		
		color: #B23535;
	}
	#my-topisc-msg{
		padding: 7px 0px 0px 34px;
		
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
		
		font-size: 14px;
		line-height: 24px;
		
		color: #000000;
	}
	#my-topisc-msg>span{
		color: #B23535;
	}
	#my-topics-list{
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
	}
	#m-t-latest{
		padding-top: 11px;
		padding-left: 34px;
		
		font-size: 14px;
		line-height: 20px;
		
		color: #727272;
	}
</style>
