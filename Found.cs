using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    class Found
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

        public List<Found> GetAllFound(string Htmltext)
        {
            Regex regex = new Regex("(?<=\")\\w+(?=\")",RegexOptions.None);
            MatchCollection MC = regex.Matches("Htmltext");
            List<String> AllFound = new List<string>();
            foreach (Match ma in MC)
            {
                AllFound.Add(ma.Value);
            }
            List<Found> Foundlist =new List<Found>();
            try
            {
                for (int n = 0; n < AllFound.Count; n = n + 4)
                {
                    Found found = new Found();
                    found.Code = AllFound[n];
                    found.Abbr = AllFound[n + 1];
                    found.Name = AllFound[n + 2];
                    found.Type = AllFound[n + 3];
                    Foundlist.Add(found);
                }
                return Foundlist;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return null;
            }
        }
    }
}
