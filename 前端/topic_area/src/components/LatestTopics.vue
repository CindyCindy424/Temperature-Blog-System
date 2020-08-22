<template>
	<div id="latest-topics">
		<div id="latest-topics-title">
			<span>最新话题</span>
			<input id="topics-search-box" type="text" placeholder="                         搜索话题"/>
			<div id="topics-search-button"></div>
		</div>
		<div id="latest-topics-list">
			<div class="l-t-item" v-for="item in LTopics" :key="item.topicID">
				<span class="l-t-i-title">{{item.title}}</span>
				<span class="l-t-i-content">{{item.content}}</span>
				<span class="l-t-i-date">{{item.date}}</span>
				<span class="l-t-i-time">{{item.time}}</span>
				<span class="l-t-i-answerCount">{{item.answerCount}}</span>
				<span class="l-t-i-delete">删除</span>
			</div>
		</div>
	</div>
</template>

<script>
	export default{
		name: "LatestTopics",
		data(){
			return{
				LTopics: [
				{
					topicID: "1",
					title: "啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					commentCount: "22"
				},
				{
					topicID: "1",
					title: "啥都行",
					content: "谁说的",
					date: "2020-08-22",
					time: "22:20",
					commentCount: "22"
				}
				]
			}
		}
	}
</script>

<style>
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
		
		background: url('../assets/search.png');
		background-size: contain;
		background-repeat: no-repeat;
		background-position: center;
		border-radius: 0px 30px 30px 0px;
	}
	.latest-topics-list{
		width: 100%;
	}
	.l-t-item{
		display: block;
		height: 130px;
		margin-top: 5px;

		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
		
	}
	.l-t-i-title{
		display: block;
		position: relative;
		padding-top: 5px;
		padding-left: 9.17px;

		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 34px;

		color: #000000;
	}
	.l-t-i-content{
		display: block;
		padding-top: 7px;
		padding-left: 9.17px;
		height: calc(50px - 7px);
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 25px;
		
		color: #727272;
	}
	.l-t-i-date{
		display: inline-block;
		padding-top: 7px;
		padding-left: 9.17px;
		width: fit-content;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 25px;
		
		color: #000000;

	}
	.l-t-i-time{
		display: inline-block;
		padding-top: 7px;
		padding-left: 9.17px;
		width: fit-content;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 25px;
		
		color: #000000;
	}
	.l-t-i-answerCount{
		display: inline-block;
		padding-top: 7px;
		padding-left: 9.17px;
		width: fit-content;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 25px;
		
		color: #000000;
	}
	.l-t-i-delete{
		display: inline-block;
		padding-top: 7px;
		padding-left: 9.17px;
		width: fit-content;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 25px;
		text-decoration-line: underline;
		
		color: #999494;
	}
	
</style>
