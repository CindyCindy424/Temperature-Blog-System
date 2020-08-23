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
		<div id="latest-topics-page-controller">
			<div id="l-t-last-page">上一页</div>
				<div class="l-t-page-num" v-for="item in pageNums" :key="item.pageNums">
					{{item.page}}
				</div>
			<div id="l-t-next-page">下一页</div>
			
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
				],
				pageNums:[
					{page: 1},
					{page: 2},
					{page: 3}
				],
				selectedPage: 1,
			}
		}
	}
</script>

<style>
	#latest-topics{
		width: 870px;
		display: inline-block;
		vertical-align: top;
		margin-top: 17px;
		margin-left: calc((100% - 1290px) / 2);
	}
	#latest-topics-title{
		width: 100%;
		height: 91px;
		
		background: #FFFFFF;
		box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.25);
	}
	#latest-topics-title>span{
		display: inline-block;
		vertical-align: top;
		
		height: 100%;
		width: 547px;
		
		margin-left: 15px;
		
		font-size: 24px;
		line-height: 91px;
		align-items: center;
		
		color: #000000;
	}
	#topics-search-box{
		display: inline-block;
		vertical-align: top;
		width: 282px;
		height: 35px;
		line-height: 35px;
		
		margin-top: calc((91px - 35px) / 2);
		
		background: #E8E2E2;
		border-radius: 8px;
		border-width: 0px;
	}
	#topics-search-button{
		display: inline-block;
		vertical-align: top;
		width: 82.65px;
		height: 35px;
		
		margin-left: -82.65px;
		margin-top: calc((91px - 35px) / 2);
		
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
	#latest-topics-page-controller{
		margin-top: 22px;
		text-align: center;
	}
	#l-t-last-page{
		display: inline-block;
		vertical-align: top;
		margin-right: 70px;
	}
	.l-t-page-num{
		display: inline-block;
		vertical-align: top;
		
		width: 37px;
		height: 35px;
		
		font-size: 18px;
		line-height: 35px;
		text-align: center;
		
		color: #727272;
		border-radius: 5px;
	}
	.l-t-page-num-selected{
		background: rgba(114, 114, 114, 0.3);
	}
	#l-t-next-page{
		display: inline-block;
		vertical-align: top;
		margin-left: 70px;
	}
	
</style>
