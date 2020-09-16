<template>
	<div>
		<div id="UploadArticle-page">
			<div id="UploadArticle-photo">
				<img src="../assets/banner.png" alt="照片">
			</div>
		</div>
		<div class="UploadArticle-index-container">
			<span id="UploadArticle-index-path">创作中心 > 写博文</span>
		</div>
		<div id="UploadArticle-container">
			<div id="UploadArticle-article-title-container">
				<input id="UploadArticle-article-title" placeholder="请在此输入标题..." v-model="inputaricletitie" type="text" />
			</div>
			<div id="UploadArticle-head-container">
				<p id="UploadArticle-head">文本编辑器</p>
			</div>
			<div>
				<el-input id="UploadArticle-article-content" type="textarea" :rows="18" placeholder="请在此输入文字..." v-model="inputarticlecontent"></el-input>
				<!--<input id="article-content" placeholder="请在此输入文字..." v-model="inputarticlecontent" type="text" />-->
			</div>
			<div>
				<div id="UploadArticle-tag">添加tag:</div>
				<div id ="UploadArticle-zone-button">
					<el-select v-model="value" placeholder="请选择">
					    <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
							<span id="UploadArticle-tar" @click="selecttar(item.id)">{{item.label}}</span>
						</el-option>
					 </el-select>
				</div>
			</div>
			<div>
				<button id="UploadArticle-publish" @click="creatArticle()">
					 <router-link :to="{path:'/MyArticle',query:{MyArticleid:this.accoountownerid,MyArticlename:this.accoountownername}}"><span id="UploadArticle-publish-index">发表</span></router-link> 
					
				</button>
				<button id="UploadArticle-quit">
					<router-link :to="{path:'/MyArticle',query:{MyArticleid:this.accoountownerid,MyArticlename:this.accoountownername}}"><span id="UploadArticle-quit-index">取消</span></router-link>
				</button>
			</div>
		</div>
	</div>
</template>

<script>
	export default{
		name:"ArticleContent",
		created:function(){
			this.getUserInfo();
		},
		methods:{
			   getToken:function(){
			       this.token=document.cookie.split(";")[0].split("=")[1];
			    },
			creatArticle(){
				if(this.inputaricletitie!=""&&this.inputarticlecontent!=""&&this.zoneid!=""){
					var forms=new FormData();
					forms.append('content',this.inputarticlecontent);
					forms.append('title',this.inputaricletitie);
					forms.append('nick_name',this.accoountownername);
					forms.append('zoneid',this.zone);
					var headerToken=this.token;
					this.ajax = new XMLHttpRequest();
					this.ajax.open("POST", "http://139.224.255.43:7779/Article/createArticleByNickName", true);
					this.ajax.setRequestHeader('Authorization','Bearer '+headerToken);
					this.ajax.onreadystatechange = this.CAsuccessfully;
					this.ajax.send(forms);
				}
				if(this.inputaricletitie!=""){
					if(this.inputarticlecontent!=""){
						if(this.zoneid!=0){
							var forms=new FormData();
							forms.append('content',this.inputarticlecontent);
							forms.append('title',this.inputaricletitie);
							forms.append('nick_name',this.accoountownername);
							forms.append('zoneid',this.zone);
							var headerToken=this.token;
							this.ajax = new XMLHttpRequest();
							this.ajax.open("POST", "http://139.224.255.43:7779/Article/createArticleByNickName", true);
							this.ajax.setRequestHeader('Authorization','Bearer '+headerToken);
							this.ajax.onreadystatechange = this.CAsuccessfully;
							this.ajax.send(forms);
						}
						else{
							this.$alert('请选择分区！！', '提示', {
							          confirmButtonText: '确定'
							        });
						}
					}
					else{
						this.$alert('内容不能为空！！', '警告', {
						          confirmButtonText: '确定'
						        });
					}
				}
				else{
					this.$alert('标题不能为空！！', '警告', {
					          confirmButtonText: '确定'
					        });
				}
				
			},
			CAsuccessfully(){
				if (this.ajax.readyState == 4 && this.ajax.status == 200){
					if(JSON.parse(this.ajax.responseText).returnFlag==1){
						this.$alert('文章上传成功！', '提示', {
						          confirmButtonText: '确定'
						         });
					}
					else{
						this.$alert('文章上传失败，原因可能是您已上传过同名文章，请更改标题后重试...', '提示', {
						          confirmButtonText: '确定'
						        });
					}
					/*console.log(this.ajax.responseText);
					console.log(JSON.parse(this.ajax.responseText));
					console.log(JSON.parse(this.aajax.responseText).result);*/
					
				}
			},
			selecttar(v){
				this.zone=v;
				console.log(this.zone);
			},
			getUserInfo(){
				var name=this.accoountownername;
				var headerToken=this.token;
				this.GUIajax = new XMLHttpRequest();
				this.GUIajax.open("POST", "http://139.224.255.43:7779/Account/getUserInfoByNickName?nick_name="+name, true);
				this.GUIajax.setRequestHeader('Authorization','Bearer '+headerToken);
				this.GUIajax.onreadystatechange = this.GUIsuccessfully;
				this.GUIajax.send();
			},
			GUIsuccessfully(){
				if (this.GUIajax.readyState == 4 && this.GUIajax.status == 200){	
					var id=JSON.parse(this.GUIajax.responseText).userInfo.userId;
					this.accoountownerid=id;
				}
				
			},
		},
		data(){
			return {
				zone: 0,
				accoountownername:"cindy",
				accoountownerid:5,
				isMenuShow: false,
				isLimMenuShow: false,
				token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiIyNmQ3ZTU4My1jYjNiLTQ0NzctYjMzMy1lNTU5OTFjZWM5ZjMiLCJleHAiOjE2MDAxNzE1ODMsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.A_dAXi9n67QkY0_6e3L4pbaOW6A-ELBn0o8Vg_Nb1AQ",
				ajax:0,
				inputaricletitie:'',
				inputarticlecontent:'',
				options: [{
				          value: '1',
				          label: '学习',
						  id:1,
				        }, {
				          value: '2',
				          label: '生活',
						  id:2,
				        }, {
				          value: '3',
				          label: '开发',
						  id:3,
				        }, {
				          value: '4',
				          label: '游戏',
						  id:4,
				        }, {
				          value: '5',
				          label: '娱乐',
						  id:5,
				        }, {
				          value: '6',
				          label: '体育',
						  id:6,
				        }, {
				          value: '7',
				          label: '影视',
						  id:7,
				        }, {
				          value: '8',
				          label: '资讯',
						  id:8,
				        }, {
				          value: '9',
				          label: '时尚',
						  id:9,
				        }, {
				          value: '10',
				          label: '舞蹈',
						  id:10,
				        }, {
				          value: '11',
				          label: '音乐',
						  id:11,
				        }, {
				          value: '12',
				          label: '其他',
						  id:12,
				        }],
				        value: ''
			};
		}
	}
</script>

<style>
	#UploadArticle-page{
		width: 100%;
		height: 2500px;
				
		font-family: Microsoft YaHei;
				
		background: #E5E5E5;
	}
	#UploadArticle-photo{
		width: calc(100% - 0px);
		height: 312px;
		overflow: hidden;
	}
	#UploadArticle-photo>img{
		width: 100%;
		margin-top: calc((0px - 600px + 352px) / 2 + 40px) ;	/*更换图片需要注意修改*/
	}
	.UploadArticle-index-container{
		position: absolute;
		width: 1347px;
		height: 108px;
		left: 46px;
		top: 398px;
		
		background: #F9F8F8;
		box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
	}
	#UploadArticle-index-path{
		position: absolute;
		width: 215px;
		height: 47px;
		left: 32px;
		top: 33px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: bold;
		font-size: 24px;
		line-height: 32px;
		display: flex;
		align-items: center;
		
		color: #756F6F;
	
	}
	
	
	#UploadArticle-container{
		position: absolute;
		width: 1345px;
		height: 1889px;
		left: 48px;
		top: 506px;
		
		background: #FFFFFF;
	}
	#UploadArticle-article-title-container{
		margin-top: 5%;
		margin-left: 4.5%;
		width: 1194px;
		height: 79px;
		
		background: #ffffff;
		border: 1px solid #000000;
		box-sizing: border-box;
		box-shadow: inset 0px 2px 4px rgba(0, 0, 0, 0.25);
	}
	#UploadArticle-article-title{
		width: 536px;
		height: 38px;
		margin-top: 2%;
		margin-left: 1%;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 32px;
		display: flex;
		align-items: center;
		border: none;/*input边框不显示*/
		
		color: #9F9E9E;
	}
	#UploadArticle-article-title:focus{
		outline:none;/*input边框获取时不显示*/
	}
	#UploadArticle-head-container{
		width: 1194px;
		height: 151px;
		margin-top: 2%;
		margin-left: 4.5%;
		text-align: left;
		
		background: #C4C4C4;
	}
	#UploadArticle-head{
		padding-top: 5%;
		padding-left: 5%;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 32px;
		display: block;
		
		color: #000000;
	}
	#UploadArticle-article-content{
		width: 89%;
		margin-left: 4.5%;
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 32px;
		display: block;
		background: #FFFFFF;
		border: 1px solid #847E7E;
		box-sizing: border-box;
	}
	#UploadArticle-tag{
		margin-top: 3.8%;
		margin-left: 10%;
		width: 177px;
		height: 40px;	
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 24px;
		line-height: 32px;
		display: flex;
		align-items: center;
		
		color: #000000;
	}
	#UploadArticle-tar{
		display: block;
		width: 100%;
		height:100%;
	}
	#UploadArticle-publish{
		position: absolute;
		width: 159px;
		height: 42px;
		left: 902px;
		top: 1096px;
		
		background: #DA4646;
		box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
		border-radius: 4px;
		border: none;
	}
	#UploadArticle-publish-index{
		position: absolute;
		width: 103px;
		height: 20px;
		left: 28px;
		top: 13px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		display: flex;
		align-items: center;
		
		color: #F9F5F5;
	}
	#UploadArticle-quit{
		position: absolute;
		width: 159px;
		height: 42px;
		left: 1119px;
		top: 1096px;
		
		background: #EFE7E7;
		box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
		border-radius: 4px;
		border: none;
	}
	#UploadArticle-quit-index{
		position: absolute;
		width: 103px;
		height: 20px;
		left: 28px;
		top: 13px;
		
		font-family: Microsoft YaHei;
		font-style: normal;
		font-weight: normal;
		font-size: 18px;
		line-height: 24px;
		display: flex;
		align-items: center;
		text-align: center;
		
		color: #504C4C;
	
	}
	
	#UploadArticle-zone-button{
		margin-top: -3%;
		margin-left: 18%;
		width: 170px;
		height: 38px;
	}
</style>
