需求：客户需要用主域名下的二级域名在本地服务器中使用。 
	 1.网站主域名www要指向阿里云服务器，而二级域名就指向本地服务器（动态IP）。
	 2.不同分公司用不同的二级域名
综合以上问题写一个简单小工具来实现阿里云的二级域名动态更新IP
流程图和原理就不一一说明了开始干活。

第一：阿里云注册一个域名 ymxxxx.com(要实名认证不然解析不了)

第二：阿里云API的密钥   AccessKey ID和AccessKey Secret

剩下就到配置文件去配置一下即可



