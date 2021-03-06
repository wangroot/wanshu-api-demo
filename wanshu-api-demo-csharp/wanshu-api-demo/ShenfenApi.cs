﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wanshu_api_demo
{
	/**
	 * 身份二要素认证
	 */
	public class ShenfenApi
	{
		const string APP_ID_SHENFEN = "12345678";
	    const string APP_KEY_SHENFEN = "12345678";
	    const string API_URL_SHENFEN = "http://localhost:8888/open/idcard/id-verify";
	
	    public void Check() {
	        // 1.调用身份信息校验api
	        JObject jsonObject = invokeShenfen("李*", "34253019930526****");
	
	        // 2.处理返回结果
	        if (jsonObject != null) {
	            //响应code码。200000：成功，其他失败
	            string code = jsonObject["code"].ToString();
	            if ("200000".Equals(code) && null != jsonObject["data"]) {
	                // 调用身份信息校验成功
	                // 解析结果数据，进行业务处理
	                // 校验状态码  000000：成功，其他失败
	                string value = jsonObject["data"][0]["value"].ToString();
	                Console.WriteLine("调用身份信息校验成功,value:" + value);
	            } else {
	                // 记录错误日志，正式项目中请换成log打印
	                Console.WriteLine("调用身份信息校验失败,code:" + code + ",msg:" + jsonObject["message"]);
	            }
	        }
	    }
	
	
	    private JObject invokeShenfen(string name, string idNum) {
	        IDictionary<string, string> dic = new Dictionary<string, string>();
	        dic.Add("appId", APP_ID_SHENFEN);
	        dic.Add("appKey", APP_KEY_SHENFEN);
	        dic.Add("name", name);
	        dic.Add("idNum", idNum);
	        string result = HttpUtility.Post(API_URL_SHENFEN, dic);
	        // 解析json,并返回结果
	        return string.IsNullOrEmpty(result) ? null : (JObject)JsonConvert.DeserializeObject(result);
	    }
	}
}
