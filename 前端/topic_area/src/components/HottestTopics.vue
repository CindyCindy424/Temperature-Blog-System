<template>
	<div id="hottest-topics">
		<div id="hottest-topics-title">最热话题</div>
		<div id="hottest-topics-list">
			<div class="h-t-list-item" v-for="item in HTTopics" :key="item.topicID">
			</div>
		</div>
	</div>
</template>

<script>
	export default{
		name: "HottestTopics",
		methods:{
			
		}
	}
</script>

<style>
	#hottest-topics{
		width: 100%;
		margin-top: 81px;
	}
	#hottest-topics-title{
		height: 54px;
		padding-left: 13px;
		
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
		
		font-size: 24px;
		line-height: 54px;
		
		color: #B23535;
	}
	#hottest-topics-list{
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25), inset 0px 1px 1px rgba(0, 0, 0, 0.25);
	}
</style>
