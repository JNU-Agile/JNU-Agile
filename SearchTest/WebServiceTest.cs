using FundHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace SearchTest
{
    
    
    /// <summary>
    ///这是 WebServiceTest 的测试类，旨在
    ///包含所有 WebServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class WebServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///GetWebContent 的测试
        ///</summary>
        [TestMethod()]
        public void GetWebContentTest()
        {
            WebService target = new WebService(); // TODO: 初始化为适当的值
            string Url = "http://fund.eastmoney.com/js/fundcode_search.js?v=20130718.js"; // TODO: 初始化为适当的值
            Encoding encode = Encoding.UTF8; // TODO: 初始化为适当的值
            string HtmlTextPart = "﻿var r = [[\"000001\",\"HXCZ\",\"华夏成长\",\"混合型\"],"; // TODO: 初始化为适当的值

            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
            Byte[] pageData = MyWebClient.DownloadData(Url); //从指定网站下载数据
            string HtmlText = encode.GetString(pageData);

            bool expected = true;
            bool actual = HtmlText.Contains(HtmlTextPart);

            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetAllFund 的测试
        ///</summary>
        [TestMethod()]
        public void GetAllFundTest()
        {
            WebService target = new WebService(); // TODO: 初始化为适当的值
            //Assert.Inconclusive("验证此测试方法的正确性。");

            string HtmlText = target.GetWebContent("http://fund.eastmoney.com/js/fundcode_search.js?v=20130718.js", Encoding.UTF8);
            HtmlText = HtmlText.Replace(",", "");
            HtmlText = HtmlText.Replace("][", "");
            HtmlText = HtmlText.Replace("\"\"", "\"");
            //对处理过后的数据进行匹配
            Regex regex = new Regex("(?<=\").+?(?=\")", RegexOptions.None);
            MatchCollection MC = regex.Matches(HtmlText);
            List<String> AllFund = new List<String>();
            //将匹配后的数据存在一个字符串数组中
            foreach (Match ma in MC)
            {
                AllFund.Add(ma.Value);
            }

            //按四个一组读，并将读后的数据存入基金类列表中
            List<Fund> Fundlist = new List<Fund>();
            for (int n = 0; n < AllFund.Count; n = n + 4)
            {
                Fund found = new Fund();
                found.Code = AllFund[n];
                found.Abbr = AllFund[n + 1];
                found.Name = AllFund[n + 2];
                found.Type = AllFund[n + 3];
                Fundlist.Add(found);
            }

            bool expected = true;
            bool actual = Fundlist.Count > 0;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///getNetValue 的测试
        ///</summary>
        [TestMethod()]
        public void getNetValueTest()
        {
            WebService target = new WebService(); // TODO: 初始化为适当的值
            string HtmlText = target.GetWebContent("http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code=000547&page=1&per=10000%22", Encoding.Default);

            //获取总记录数
            int head = HtmlText.IndexOf("records:") + 8;
            int foot = HtmlText.IndexOf(",", head);
            int count = Convert.ToInt32(HtmlText.Substring(head, foot - head));
            //return count;
            int flag = 1;
            String h2 = String.Empty;
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

            int expected = FundList2.Count;
            int actual;
            int value = 0;
            foreach (NetValueOfFund nvof in FundList2)
            {
                if (nvof.NetValue != null)
                    value++;
                else
                    value--;
            }
            actual = value;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///WebService 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void WebServiceConstructorTest()
        {
            WebService target = new WebService();
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
