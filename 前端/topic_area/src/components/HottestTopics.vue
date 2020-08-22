<template>
	<div id="hottest-topics">
		<span class="r-aside-subtitle">最热话题</span>
		<div id="hottest-topics-list">
			<div class="h-t-list-item" v-for="item in HTTopics" :key="item.topicID">
				<span class="title">{{item.title}}</span>
				<span class="title">{{item.content}}</span>
				<span class="title">{{item.date}}</span>
				<span class="title">{{item.time}}</span>
				<span class="title">{{item.answerCount}}</span>
				<span class="title">删除</span>
			</div>
		</div>
	</div>
</template>

<script>
	export default{
		name: "HottestTopics",
		props: {
			HTTopics: [
				{
					topicID: "1",
					title: "啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					answerCount: "22"
				}
			]
		},
		methods:{
			
		}
	}
</script>

<style>
	#hottest-topics{
		position: absolute;
		width: 363px;
		left: 996px;
		top: 910px;
		
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
	}
</style>
