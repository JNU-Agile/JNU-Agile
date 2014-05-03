using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public class StrProc
    {
	//将从网页抓取的数据转化为json
        public string StringProcess(string str)
        {
            string strDeleH = str.Replace("{\"resultcode\":\"200\",\"reason\":\"SUCCESSED!\",\"result\":", "");//去头
            string strDeleHDeleT = strDeleH.Replace(",\"error_code\":0}", "");//去尾
            string strDeleHDeleTDeleN = Regex.Replace(strDeleHDeleT, "\"[0-9]{1,4}\":", "");//匹配去嵌套的数字
            string strDeleHDeleTDeleNDeleA = strDeleHDeleTDeleN.Replace("[{{", "[{");//去除去数字后留下的多余的字符
            string TargetStr = strDeleHDeleTDeleNDeleA.Replace("}}]", "}]");
            return TargetStr;
        }
    }
}
