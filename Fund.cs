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
            return null;
        }
    }
}
