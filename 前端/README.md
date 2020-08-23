# 前端

[TOC]

## 开发环境

需要准备：
1. node.js
2. npm
3. cnpm
4. webpack
5. HbuilderX

配置流程：
1. vue官方教程，第14节单文件组件视频： https://learning.dcloud.io/#/?vid=14 。
2. 安装node.js很麻烦，需要配置环境变量，参考 https://www.cnblogs.com/yominhi/p/7039795.html （这个很管用）， https://www.cnblogs.com/zhouyu2017/p/6485265.html ，https://www.cnblogs.com/liangsongbai/p/5506073.html 。node.js建议下载12.18.3 LTS版。

## 项目开发

### 新建项目

1. 可以在HbuilderX里面新建
2. 也可以参考vue官方教程，第14节单文件组件视频： https://learning.dcloud.io/#/?vid=14 ，使用cmd，输入"vue ui"，开启图形界面新建项目。

### 运行项目

1. 在HbuilderX中点击“运行”→“运行到终端”→“npm run serve”，会出现黑框，编译完成后，黑框显示地址，如“ http://localhost:8080/ ”，将地址复制到浏览器即可打开。
2. 开发期间不要关闭黑框，这样每次保存代码会立即编译，效果立即呈现在浏览器里。
3. 如果代码有错误，可以到浏览器里看报错。

### 打包项目

1. 在HbuilderX中点击“运行”→“运行到终端”→“npm run build”，编译完成后，项目文件夹下的dist文件夹中的内容，就是部署到服务器上的内容。如果电脑上安装了本地服务器如WAMP，部署进去通过localhost地址访问即可。
2. 部署时如果不将dist内容部署到根目录下，会出现静态资源访问路径不正确的情况。网上有通过修改配置文件解决的办法，但是我找不到配置文件😭。

### 开发项目

1. 将整个网页视为一个组件，就是src/App.vue，在这个文件里添加东西。
2. 可以引用src/components下的自建组件
3. 图片等静态资源放在src/assets目录下。
4. 其他目录不用管，只需知道注意dist目录就是打包后的结果目录。