using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace WindowsFormsApplication1
{
    //基金类
    public class Found
    {
        private string m_code;
        public string Code
        {
            get { return m_code; }
            set { m_code = value; }

        }

        private string m_name;
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }

        }

        private string m_newnet;
        public string Newnet
        {
            get { return m_newnet; }
            set { m_newnet = value; }
        }

        private string m_totalnet;
        public string Totalnet
        {
            get { return m_totalnet; }
            set { m_totalnet = value; }
        }

        private string m_dayincrease;
        public string Dayincrease
        {
            get { return m_dayincrease; }
            set { m_dayincrease = value; }
        }

        private string m_daygrowrate;
        public string Daygrowrate
        {
            get { return m_daygrowrate; }
            set { m_daygrowrate = value; }
        }

        private string m_weekgrowrate;
        public string Weekgrowrate
        {
            get { return m_weekgrowrate; }
            set { m_weekgrowrate = value; }
        }
        private string m_annualincome;
        public string Annualincome
        {
            get { return m_annualincome; }
            set { m_annualincome = value; }
        }

        private string m_time;
        public string Time
        {
            get { return m_time; }
            set { m_time = value; }
        }
    }


    
    //将json转化为对象
    public class JsonTranslate
    {
        public List<Found> JsonToObject (string url, Encoding code)
        {
            WebService web = new WebService();
            string str = web.GetWebContent(url, code);
            StrProc strproc = new StrProc();//将原始的字符串转化为json
            string json = strproc.StringProcess(str);
            List<Found> todydata = new List<Found>();
            todydata =JsonConvert.DeserializeObject<List<Found>>(json);//将json转化为对象
            return todydata;
         }
     }
    
}
