using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace FundHelper
{
    public class Fund
    {
        private String code = String.Empty;
        public String Code
        {
            get { return code; }
            set { code = value; }
        }
        private String abbr = String.Empty;
        public String Abbr
        {
            get { return abbr; }
            set { abbr = value; }
        }
        private String name = String.Empty;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String type = String.Empty;
        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        private Double netValueToday;

        public Double NetValueToday
        {
            get { return netValueToday; }
            set { netValueToday = value; }
        }
        private Double totalNetValueToday;

        public Double TotalNetValueToday
        {
            get { return totalNetValueToday; }
            set { totalNetValueToday = value; }
        }
        private Double netValueInsToday;

        public Double NetValueInsToday
        {
            get { return netValueInsToday; }
            set { netValueInsToday = value; }
        }
        private Double netValueInsRateToday;

        public Double NetValueInsRateToday
        {
            get { return netValueInsRateToday; }
            set { netValueInsRateToday = value; }
        }


        private List<NetValueOfFund> netValue = new List<NetValueOfFund>();

        public List<NetValueOfFund> NetValue
        {
            get { return netValue; }
            set { netValue = value; }
        }

    }
}
