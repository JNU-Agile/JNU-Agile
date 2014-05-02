using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private float m_newnet;
        public float Newnet
        {
            get { return m_newnet; }
            set { m_newnet = value; }
        }

        private float m_totalnet;
        public float Totalnet
        {
            get { return m_totalnet; }
            set { m_totalnet = value; }
        }

        private float m_dayincrease;
        public float Dayincrease
        {
            get { return m_dayincrease; }
            set { m_dayincrease = value; }
        }

        private float m_daygrowrate;
        public float Daygrowrate
        {
            get { return m_daygrowrate; }
            set { m_daygrowrate = value; }
        }

        private float m_weekgrowrate;
        public float Weekgrowrate
        {
            get { return m_weekgrowrate; }
            set { m_weekgrowrate = value; }
        }
        private float m_annualincome;
        public float Annualincome
        {
            get { return m_annualincome; }
            set { m_annualincome = value; }
        }

        //因为再本来的json中有日期，我就先加上了。
        private DateTime m_date;
        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }
    }


    //将json转化为对象
    public class JsonTranslate
    {
        public List<Found> main (string url, Encoding code)
        {
            WebService web = new WebService();
            string json = web.GetWebContent(url, code);
            List<Found> todydata = JsonConvert.DeserializeObject<List<Found>>(json);
            return todydata;
         }
     }
}
