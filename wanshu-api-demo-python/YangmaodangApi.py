#!/usr/bin/env python
# coding=utf8

from urllib2 import Request, urlopen, URLError
import urllib
import json

#羊毛党检测

def post(url, data):
    params = urllib.urlencode(data)
    print(u'发起的请求为：%s' % url)
    user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
    headers = {'User-Agent': user_agent, 'Content-type': 'application/x-www-form-urlencoded',
               'Accept-Charset': 'utf-8'}
    request = Request(url, params, headers)
    try:
        response = urlopen(request, timeout=3)
    except URLError, e:
        print u'发送请求失败，原因：', e
        return None
    else:
        return response.read().decode('utf-8')


if __name__ == "__main__":
    invoke_url = 'https://api.253.com/open/wool/wcheck'
    invoke_data = {'appId': '12345678', 'appKey': '12345678', 'mobile': '1861234****', 'ip': '*.*.*.*'}

    # 1. 调用羊毛党检测api
    result_data = post(invoke_url, invoke_data)
    # 2.处理返回结果
    result = json.loads(result_data) if result_data is not None else exit(1)
    # 响应code码。200000：成功，其他失败
    if result is None or '200000' != result['code']:
        print u'调用羊毛党检测失败,code:', result['code'], ',msg:', result['message']
    else:
        # 调用羊毛党检测成功
        # 解析结果数据，进行业务处理
        # 检测结果  W1：白名单  W2：疑似白名单  B1 ：黑名单  B2 ：疑似黑名单  N：未找到
        status = result['data']['status']
        print u'调用羊毛党检测成功,status:', status
