using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebService web = new WebService();
            Encoding encode = Encoding.UTF8;
            String jsonText = web.GetWebContent("http://web.juhe.cn:8080/fund/netdata/all?key=927c4fed88698967319b811467f9b4c3", encode);
            String result = String.Empty;
            JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            String FILE_NAME = "MyFile.txt";
            StreamWriter sr;
            try
            {
                sr = File.CreateText(FILE_NAME);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }
            while (reader.Read())
            {
                result += reader.Value;
                result += " ";
            }
            sr.Write(result);
            sr.Close();
        }
    }
}
