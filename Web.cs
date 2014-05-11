using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
namespace WindowsFormsApplication1
{
    public class WebService
    {
        //获取整个网页
        public String GetWebContent(String Url, Encoding encode)
        {
            try
            {
                if (Url.Equals("about:blank")) return null;
                if (!Url.StartsWith("http://") && !Url.StartsWith("https://"))
                {
                    Url = "http://" + Url;
                }


                //根据获取网址向服务器发送请求：
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                //设置超时(10秒)
                myReq.Timeout = 60000;
                myReq.Accept = "Accept-Language:zh-cn";
                myReq.KeepAlive = true;
                myReq.Referer = Url;
                myReq.MaximumAutomaticRedirections = 100;
                myReq.AllowAutoRedirect = true;
                //网络响应请求
                HttpWebResponse myres = (HttpWebResponse)myReq.GetResponse();
                //获取网页字符流，并最终转换为系统默认的字符：
                Stream WebStream = myres.GetResponseStream();
                StreamReader Sreader = new StreamReader(WebStream, encode);
                String HtmlText = Sreader.ReadToEnd();
                //关闭字符流：
                Sreader.Close();
                WebStream.Close();
                return HtmlText;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return null;
            }
        }

    }
}
