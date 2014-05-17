using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using System.Threading;

namespace FundHelper
{
    public partial class Main : Form
    {
        //无边框窗口拖动代码
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void main_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private static int RowIndex;
        private static WebService webService = new WebService();
        private static List<Fund> FundList = new List<Fund>();
        private void GetNetValue()
        {
            FundList[RowIndex].NetValue = webService.getNetValue(FundList[RowIndex].Code.ToString(),progressBar1);

            //for test
            String result = "test\n";
            for (int i = 0; i < 3; i++)
                result += FundList[RowIndex].NetValue[i].date + ": " + FundList[RowIndex].NetValue[i].netValue + "\n";
            MessageBox.Show(result);

        }
        public Main()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            FundList = webService.GetAllFund();
            dataGridView1.DataSource = FundList;
            dataGridView1.Columns[0].HeaderText = "基金代码";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].HeaderText = "简称";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].HeaderText = "全称";
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].HeaderText = "类型";
            dataGridView1.Columns[3].Width = 200;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex=e.RowIndex;
            Thread t = new Thread(GetNetValue);
            t.Start();
        }
    }
}
