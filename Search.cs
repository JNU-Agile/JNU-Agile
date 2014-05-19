using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundHelper
{
    class Search
    {
        private static int compar(Fund f1, Fund f2)
        {
            if (f1.Name == f2.Name)
                return 0;
            else if (f1.Name.CompareTo(f2.Name) < 0)
                return -1;
            else if (f1.Name.CompareTo(f2.Name) > 0)
                return 1;
            return -2;
        }

        //两个函数返回的都是基金的在列表中的index

        public int SearchByCode(List<Fund> Fundlist,String code){
            int start = 0;
            int end = Fundlist.Count - 1;
            int mid = 0;
            //折半查找
            while(start <= end){
                mid = (start + end)/2;
                Console.WriteLine(mid + " " + start + " " + end);
                if(Fundlist[mid].Code.Equals(code))
                    return mid;
                else if(code.CompareTo(Fundlist[mid].Code) < 0)
                    end = mid - 1;
                else if(code.CompareTo(Fundlist[mid].Code) > 0)
                    start = mid + 1;
            }
            return -2;
        }

        public int SearchByName(List<Fund> Fundlist, String name)
        {
            //建议在抓取之后就按基金名称排序
            List<Fund> fundlist = new List<Fund>(Fundlist.ToArray());
            //按基金名称排序
            fundlist.Sort(compar);
            int start = 0;
            int end = fundlist.Count - 1;
            int mid = 0;
            //折半查找
            while (start <= end)
            {
                mid = (start + end) / 2;
                Console.WriteLine(mid + " " + start + " " + end);
                if (fundlist[mid].Name.Equals(name))
                    break;
                else if (name.CompareTo(fundlist[mid].Name) < 0)
                    end = mid - 1;
                else if (name.CompareTo(fundlist[mid].Name) > 0)
                    start = mid + 1;
            }
            //原先的Fundlist该基金的位置
            int i = SearchByCode(Fundlist, fundlist[mid].Code);
            return i;
        }
    }
}
