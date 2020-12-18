using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleGeneralFramework
{
    public class ApiHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httprequest"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendWebApiRequest(HttpRequest httprequest)
        {
            Task<HttpResponseMessage> task = null;
            
            HttpClient client = new HttpClient();           

            var baseAddress = string.IsNullOrEmpty(httprequest.Version) ? string.Format("{0}/api/{1}/", httprequest.BaseURL, httprequest.ControllerName) : string.Format("{0}/api/{1}/{2}/", httprequest.BaseURL, httprequest.Version, httprequest.ControllerName);
            client.BaseAddress = new Uri(baseAddress);
                
            switch (httprequest.Verb)
            {
                case "get":                    
                    task = client.GetAsync(httprequest.Action);
                    break;
                case "post":
                    task = client.PostAsync(httprequest.Action, httprequest.Parameter);
                    break;
                case "delete":
                    task = client.DeleteAsync(httprequest.Action);
                    break;
                case "put":                    
                    task = client.PutAsync(httprequest.Action, httprequest.Parameter);
                    break;
                default:
                    break;
            }
            return task;
        }
        public async static Task<string> GetJasonWithWebApi(HttpRequest httprequest)
        {
            var jason = "";
            await SendWebApiRequest(httprequest).ContinueWith(
                 (task) =>
                 {
                     try
                     {
                         var response = task.Result;
                         response.EnsureSuccessStatusCode();
                         response.Content.ReadAsStringAsync().ContinueWith(
                             (readTask) =>
                             {
                                 jason = readTask.Result;
                             });
                     }
                     catch(Exception ex)
                     {
                         jason = ex.Message;
                     }
                 });
            return jason;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httprequest"></param>
        /// <param name="targetFileName"></param>
        /// <returns>如果返回的错误信息为空，表示下载成功</returns>
        public static string DownloadFileWithWebApi(HttpRequest httprequest, string targetFileName)
        {
            var errorMessage = "";
            try
            {
                SendWebApiRequest(httprequest).ContinueWith(
                    (task) =>
                    {
                        var response = task.Result;
                        response.EnsureSuccessStatusCode();
                        response.Content.DownloadAsFileAsync(targetFileName, true).ContinueWith((readTask) => errorMessage = "");
                    });
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            return errorMessage;
        }
    }
    public class KeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class HttpRequest
    {
        public string BaseURL { get; set; }
        public string Version { get; set; }
        public string ControllerName { get; set; }
        public string Action { get; set; }
        public string Verb { get; set; }
        public HttpContent Parameter { get; set; }        
    }
    public static class HttpContentExtension
    {
        /// <summary>
        /// HttpContent异步读取响应流并写入本地文件方法扩展
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName">本地文件名</param>
        /// <param name="overwrite">是否允许覆盖本地文件</param>
        /// <returns></returns>
        public static Task DownloadAsFileAsync(this HttpContent content, string fileName, bool overwrite)
        {
            string filePath = Path.GetFullPath(fileName);
            if (!overwrite && File.Exists(filePath))
            {
                throw new InvalidOperationException(string.Format("The file {0} is existed！", filePath));
            }
            return content.ReadAsByteArrayAsync().ContinueWith(
                (readBytestTask) =>
                {
                    byte[] data = readBytestTask.Result;
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fs.Write(data, 0, data.Length);
                        //清空缓冲区
                        fs.Flush();
                        fs.Close();
                    }
                });
        }
    }
}
