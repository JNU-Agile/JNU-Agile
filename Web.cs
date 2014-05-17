using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using GetNetValueOfFund;
using System.Text.RegularExpressions;
namespace WindowsFormsApplication1
{
    public class WebService
    {
        public List<Fund> getFundCode()
        {
            string HtmlText = string.Empty;
            //获取网页上代码转化为字符串
            try
            {
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
            string OutFileName = "D:\\funds\\fundcode_search.txt";
            FileStream fs = new FileStream(OutFileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            //sw.Flush();
            //string separate = new string('\0',20);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < Fundlist.Count; i++)
            {
                sw.Write(Fundlist[i].Code + "\t\t");
                sw.Write(Fundlist[i].Abbr.PadRight(20) + "\t");
                sw.Write(Fundlist[i].Name + "\t\t");
                sw.Write(Fundlist[i].Type + "\t");
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            return Fundlist;
        }

        public void getNetValue(List<Fund> fundlist)
        {
            for (int k = 0; k < fundlist.Count; k++)
            {
                string fundCode = fundlist[k].Code;
                String Url = "http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code=" + fundCode + "&page=1&per=10000";
                Encoding encode = Encoding.Default;
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


                int flag = 1;
                String h2 = string.Empty;
                String date = String.Empty;
                String netValue = String.Empty;
                String accumulativeNetValue = String.Empty;
                String dailyGrowthRate = String.Empty;
                String stateOfPurse = String.Empty;
                String earningRecently = String.Empty;
                String stateOfRedemption = String.Empty;
                String distribution = String.Empty;
                String earningPer10000 = String.Empty;
                String earningPer7 = String.Empty;
                int type;
                String s = "<th>";
                String e = "</th>";

                String s1 = "<td>";
                String e1 = "</td>";
                String s2 = "<td class='tor bold'>";
                String s3 = "<td class='tor bold red'>";
                String s4 = "<td class='red unbold'>";

                List<NetValueOfFund> FundList2 = new List<NetValueOfFund>();
                //NetValueOfFund fund; 
                if (HtmlText.Length < 320)
                    flag = 10000;
            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</th>") + 4, HtmlText.Length - HtmlText.IndexOf("</th>") - 4);
            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</th>") + 4, HtmlText.Length - HtmlText.IndexOf("</th>") - 4);
            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</th>") + 4, HtmlText.Length - HtmlText.IndexOf("</th>") - 4);
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            if (rg.Match(HtmlText).Value == "最近运作期年化收益率")
                type = 1;
            else if (rg.Match(HtmlText).Value == "日增长率")
                type = 2;
            else
                type = 3;
            if (type == 1)
                {
                    while (flag != 10000)
                    {
                        if (HtmlText[HtmlText.IndexOf("<td") + 15] == 's')
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("<span") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        else
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("</td>") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        Regex rg2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        earningPer10000 = rg2.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg3 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        earningPer7 = rg3.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg4 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        earningRecently = rg4.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg5 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfPurse = rg5.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg6 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfRedemption = rg6.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg7 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        distribution = rg7.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);

                        FundList2.Add(new NetValueOfFund(date, netValue, earningPer10000, earningPer7, earningRecently, accumulativeNetValue, dailyGrowthRate, stateOfPurse, stateOfRedemption, distribution));
                        flag++;
                        if (HtmlText.Length < 60)
                            flag = 10000;
                    }
                    h2 = " 净值日期	每万份收益（元）	7日年化收益率（%）	最近运作期年化收益率	申购状态	赎回状态	分红送配" + "\r\n";
                }
                else if(type == 3)
                {
                    while (flag != 10000)
                    {
                        if (HtmlText[HtmlText.IndexOf("<td") + 15] == 's')
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("<span") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        else
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("</td>") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        Regex rg2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        earningPer10000 = rg2.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg3 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        earningPer7 = rg3.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg5 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfPurse = rg5.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg6 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfRedemption = rg6.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg7 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        distribution = rg7.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);

                        FundList2.Add(new NetValueOfFund(date, netValue, earningPer10000, earningPer7, earningRecently, accumulativeNetValue, dailyGrowthRate, stateOfPurse, stateOfRedemption, distribution));
                        flag++;
                        if (HtmlText.Length < 60)
                            flag = 10000;
                    }
                    h2 = "净值日期 每万份收益（元） 7日年化收益率（%） 申购状态 赎回状态 分红送配" + "\r\n";
                }
                else
                {
                    while (flag != 10000)
                    {
                        if (HtmlText[HtmlText.IndexOf("<td") + 15] == 's')
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("<span") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        else
                        {
                            date = HtmlText.Substring(HtmlText.IndexOf("<td") + 4, HtmlText.IndexOf("</td>") - HtmlText.IndexOf("<td") - 4);
                            HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        }
                        Regex rg2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        netValue = rg2.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg3 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        accumulativeNetValue = rg3.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg4 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        dailyGrowthRate = rg4.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg5 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfPurse = rg5.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg6 = new Regex("(?<=(" + s1 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        stateOfRedemption = rg6.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        Regex rg7 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e1 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        distribution = rg7.Match(HtmlText).Value;
                        HtmlText = HtmlText.Substring(HtmlText.IndexOf("</td>") + 4, HtmlText.Length - HtmlText.IndexOf("</td>") - 4);
                        FundList2.Add(new NetValueOfFund(date, netValue, earningPer10000, earningPer7, earningRecently, accumulativeNetValue, dailyGrowthRate, stateOfPurse, stateOfRedemption, distribution));
                        flag++;
                        if (HtmlText.Length < 60)
                            flag = 10000;
                    }
                    h2 = "净值日期	单位净值（元）	累计净值（元）	日增长率 申购状态	赎回状态	分红送配" + "\r\n";
                }
                

                //new NetValueOfFund(date, netValue, accumulativeNetValue, dailyGrowthRate, stateOfPurse, stateOfRedemption);

                String result2 = String.Empty;
                String FILE_NAME2 = "D:\\funds\\" + fundCode + ".txt";
                StreamWriter sr;
                sr = File.CreateText(FILE_NAME2);

                if (type == 3)
                {
                    for (int m = 0; m < FundList2.Count; m++)
                    {
                        h2 += FundList2[m].date + '\t' + FundList2[m].earningPer10000 + '\t' + FundList2[m].earningPer7
                            + '\t' + FundList2[m].stateOfPurse + '\t' + FundList2[m].stateOfRedemption + FundList2[m].distribution + "\r\n";
                    }
                }
                else if(type == 1)
                {
                    for (int m = 0; m < FundList2.Count; m++)
                    {
                        h2 += FundList2[m].date + '\t' + FundList2[m].earningPer10000 + '\t' + FundList2[m].earningPer7 + FundList2[m].earningRecently + '\t'
                            + '\t' + FundList2[m].stateOfPurse + '\t' + FundList2[m].stateOfRedemption + FundList2[m].distribution + "\r\n";
                    }
                }
                else
                {
                    for (int m = 0; m < FundList2.Count; m++)
                    {
                        h2 += FundList2[m].date + '\t' + FundList2[m].netValue + '\t' + FundList2[m].accumulativeNetValue
                            + '\t' + FundList2[m].dailyGrowthRate + '\t' + FundList2[m].stateOfPurse + '\t'
                            + FundList2[m].stateOfRedemption + FundList2[m].distribution + "\r\n";
                    }     
                }


                sr.Write(h2);
                sr.Close();
            }
        }
    }
}
