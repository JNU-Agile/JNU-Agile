using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetNetValueOfFund
{
    class NetValueOfFund
    {
        public String date;
        //float netValue;
        public String netValue;
        //float accumulativeNetValue;
        public String earningRecently;
        public String accumulativeNetValue;
        //float dailyGrowthRate;
        public String dailyGrowthRate;
        public String stateOfPurse;
        public String stateOfRedemption;
        //float distribution;
        public String earningPer7;
        public String earningPer10000;
        public String distribution;

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