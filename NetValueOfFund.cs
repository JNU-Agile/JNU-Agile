using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace FundHelper
{
    public class NetValueOfFund
    {
        private String date;

        public String Date
        {
            get { return date; }
            set { date = value; }
        }
        //float netValue;
        private String netValue;

        public String NetValue
        {
            get { return netValue; }
            set { netValue = value; }
        }
        private String accumulativeNetValue;

        public String AccumulativeNetValue
        {
            get { return accumulativeNetValue; }
            set { accumulativeNetValue = value; }
        }
        private String earningPer10000;

        public String EarningPer10000
        {
            get { return earningPer10000; }
            set { earningPer10000 = value; }
        }
        private String earningPer7;

        public String EarningPer7
        {
            get { return earningPer7; }
            set { earningPer7 = value; }
        }
        private String earningRecently;

        public String EarningRecently
        {
            get { return earningRecently; }
            set { earningRecently = value; }
        }
        //float dailyGrowthRate;
        private String dailyGrowthRate;

        public String DailyGrowthRate
        {
            get { return dailyGrowthRate; }
            set { dailyGrowthRate = value; }
        }
        //float accumulativeNetValue;
        
        private String stateOfPurse;

        public String StateOfPurse
        {
            get { return stateOfPurse; }
            set { stateOfPurse = value; }
        }
        private String stateOfRedemption;

        public String StateOfRedemption
        {
            get { return stateOfRedemption; }
            set { stateOfRedemption = value; }
        }
        //float distribution;
        
        private String distribution;

        public String Distribution
        {
            get { return distribution; }
            set { distribution = value; }
        }
        public NetValueOfFund()
        {

        }
        public NetValueOfFund(String date, String netValue, String earningPer10000, String earningPer7, String earningRecently, String accumulativeNetValue, String dailyGrowthRate, String stateOfPurse, String stateOfRedemption, String distribution)
        {
            this.date = date;
            this.netValue = netValue;
            this.accumulativeNetValue = accumulativeNetValue;
            this.earningPer10000 = earningPer10000;
            this.earningPer7 = earningPer7;
            this.earningRecently = earningRecently;
            this.dailyGrowthRate = dailyGrowthRate;
            this.stateOfPurse = stateOfPurse;
            this.stateOfRedemption = stateOfRedemption;
            this.distribution = distribution;
        }
    }

}