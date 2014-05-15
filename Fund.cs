using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace WindowsFormsApplication1
{
    public class Fund
    {
        private String code;
        public String Code
        {
            get { return code; }
            set { code = value; }
        }
        private String abbr;
        public String Abbr
        {
            get { return abbr; }
            set { abbr = value; }
        }
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String type;
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public List<Fund> GetAllFund()
        {
            string HtmlText = string.Empty;
            //获取网页上代码转化为字符串
            try {

                WebClient MyWebClient = new WebClient();

        
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Byte[] pageData = MyWebClient.DownloadData("http://fund.eastmoney.com/js/fundcode_search.js?v=20130718.js"); //从指定网站下载数据

                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                HtmlText = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
            }

            catch (WebException webEx)
            {

                Console.WriteLine(webEx.Message.ToString());

            }
            //对获取的字符串进行处理
            HtmlText = HtmlText.Replace(",", "");
            HtmlText = HtmlText.Replace("][", "");
            HtmlText = HtmlText.Replace("\"\"", "\"");

            //对处理过后的数据进行匹配
            Regex regex = new Regex("(?<=\").+?(?=\")", RegexOptions.None);
            MatchCollection MC = regex.Matches(HtmlText);
            List<String> AllFund = new List<string>();

            //将匹配后的数据存在一个字符串数组中
            foreach (Match ma in MC)
            {
                AllFund.Add(ma.Value);
            }

            //按四个一组读，并将读后的数据存入基金类列表中
            List<Fund> Fundlist = new List<Fund>();
            try
            {
             
                for (int n = 0; n < AllFund.Count; n = n + 4)
                {
                    Fund found = new Fund();
                    found.Code = AllFund[n];
                    found.Abbr = AllFund[n + 1];
                    found.Name = AllFund[n + 2];
                    found.Type = AllFund[n + 3];
                    Fundlist.Add(found);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //将基金类列表保存一份在本地放在debug中
            string OutFileName = @"funds.txt";
            FileStream fs = new FileStream(OutFileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < Fundlist.Count; i++)
            {
                sw.Write(Fundlist[i].Code + "\t");
                sw.Write(Fundlist[i].Abbr + "\t");
                sw.Write(Fundlist[i].Name + "\t");
                sw.Write(Fundlist[i].Type + "\t");
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            return Fundlist;
        }
    }
}
