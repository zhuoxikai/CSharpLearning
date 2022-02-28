using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Script.Common;

namespace Script
{
    public class InsertMultiData : IDisposable
    {
        public HttpClient Client = new HttpClient();

        public void Dispose()
        {
            Client?.Dispose();
        }

        public HttpResponseMessage SendPost(string url, HttpContent content)
        {
            return Client.PostAsync(url, content).Result;
        }

        // public HttpContent BuildContent()
        // {
        //     var domain = "dev-multitenanttool.italent-inc.cn";
        //     var aspSessionId = "cojwrplzbmo3ghtyqoghyltq";
        //     var auth = "DFEC63D72DF297B1CC16ACCDF5CC783A95C5C1F54C5B7492EE55A178BCB23F3D15283944415ADF9592C7ECCB578C0DA18E54B5D8E95DBFA23A79870EB95246BA6D687195F406F504FE468A16E3F98F23357BBDC40329122BB6CEE3A3B63A696C";
        //     HttpContent content = new HttpContentHeaders();
        //     var cookies = GetMultiCookiesFromBrowser(domain, aspSessionId, auth);
        //     content.Headers.Add("Cookies",GetMultiDataCookieStr(cookies));
        //     
        // }

        public HttpContent DefaultHeader(HttpContent content)
        {
            content.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36");
            content.Headers.Add("Accept", "*/*");
            content.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            content.Headers.Add("Connection", "keep-alive");
            return content;
        }

        public HttpContent MockAjax(HttpContent content)
        {
            content.Headers.Add("X-Requested-With", "xmlhttprequest");
            content.Headers.Add("X-Sourced-By", "ajax");
            return content;
        }

        public void SetHeader(string name, string value)
        {
            Client.DefaultRequestHeaders.Add(name,value);
        }
        

        public Dictionary<string,Cookie> GetMultiCookiesFromBrowser(string domain,string aspSessionId,string auth)
        {
            //postman是利用Google的Native Messaging技术，
            //让插件Google调用自己的InterceptorBridge.exe实现从浏览器获取。
            //若需自动获取可以从本地应用程序或自定义Google插件入手 
            //postman提供了接口可以直接获取。。。
            var cookies = new Dictionary<string, Cookie>();
            var ASP_SessionId = new Cookie()
            {
                Value = aspSessionId, //"cojwrplzbmo3ghtyqoghyltq"
                Path = "/",
                Domain = domain,
                HttpOnly = true
            };
            cookies.Add(Const.ASP_SessionId, ASP_SessionId);

            var BSMultitenantDataCenterUserName = new Cookie()
            {
                Value = "zhuoxikai",
                Path = "/",
                Domain = domain,
            };
            cookies.Add(Const.BSMultitenantDataCenterUserName, BSMultitenantDataCenterUserName);
            
            var ASPXAUTH = new Cookie()
            {
                Value = auth,//DFEC63D72DF297B1CC16ACCDF5CC783A95C5C1F54C5B7492EE55A178BCB23F3D15283944415ADF9592C7ECCB578C0DA18E54B5D8E95DBFA23A79870EB95246BA6D687195F406F504FE468A16E3F98F23357BBDC40329122BB6CEE3A3B63A696C
                Path = "/",
                Domain = domain,
                HttpOnly = true
            };
            cookies.Add(Const.ASPXAUTH, ASPXAUTH);

            return cookies;
        }

        private string GetMultiDataCookieStr(Dictionary<string, Cookie> cookies)
        {
            var cookieStr = string.Empty;
            cookieStr += CookieWrapper(Const.ASP_SessionId, cookies[Const.ASP_SessionId].Value);
            cookieStr += CookieWrapper(Const.BSMultitenantDataCenterUserName, cookies[Const.BSMultitenantDataCenterUserName].Value);
            cookieStr += CookieWrapper(Const.ASPXAUTH, cookies[Const.ASPXAUTH].Value);
            return cookieStr;
        }
        private string CookieWrapper(string name,string value)
        {
            return $"{name}={value}; ";
        }
        
        
    }
}