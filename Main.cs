﻿using System;
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

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            label4.Text = "基金名";
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex=e.RowIndex;
            FundList[RowIndex].NetValue = webService.getNetValue(FundList[RowIndex].Code.ToString(), progressBar1);
            label4.Text = FundList[RowIndex].Name;
            if (FundList[RowIndex].Type == "货币型")
            {
                dataGridView2.DataSource = FundList[RowIndex].NetValue;
                dataGridView2.Columns[0].HeaderText = "净值日期";
                dataGridView2.Columns[0].Width = 140;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].Visible = true;
                dataGridView2.Columns[3].HeaderText = "每万元收益（元）";
                dataGridView2.Columns[3].Width = 140;
                dataGridView2.Columns[4].Visible = true;
                dataGridView2.Columns[4].HeaderText = "7日年化收益率（%）";
                dataGridView2.Columns[4].Width = 150;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[7].HeaderText = "申购状态";
                dataGridView2.Columns[7].Width = 140;
                dataGridView2.Columns[8].HeaderText = "赎回状态";
                dataGridView2.Columns[8].Width = 140;
                dataGridView2.Columns[9].HeaderText = "分红送配";
                dataGridView2.Columns[9].Width = 140;
            }
            else if (FundList[RowIndex].Type == "理财型")
            {
                dataGridView2.DataSource = FundList[RowIndex].NetValue;
                dataGridView2.Columns[0].HeaderText = "净值日期";
                dataGridView2.Columns[0].Width = 100;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].Visible = true;
                dataGridView2.Columns[3].HeaderText = "每万元收益（元）";
                dataGridView2.Columns[3].Width = 128;
                dataGridView2.Columns[4].Visible = true;
                dataGridView2.Columns[4].HeaderText = "7日年化收益率（%）";
                dataGridView2.Columns[4].Width = 148;
                dataGridView2.Columns[5].Visible = true;
                dataGridView2.Columns[5].HeaderText = "最近运作期年化收益率";
                dataGridView2.Columns[5].Width = 153;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[7].HeaderText = "申购状态";
                dataGridView2.Columns[7].Width = 100;
                dataGridView2.Columns[8].HeaderText = "赎回状态";
                dataGridView2.Columns[8].Width = 90;
                dataGridView2.Columns[9].HeaderText = "分红送配";
            }
            else
            {
                dataGridView2.DataSource = FundList[RowIndex].NetValue;
                dataGridView2.Columns[0].HeaderText = "净值日期";
                dataGridView2.Columns[0].Width = 115;
                dataGridView2.Columns[1].Visible = true;
                dataGridView2.Columns[1].HeaderText = "净值";
                dataGridView2.Columns[1].Width = 115;
                dataGridView2.Columns[2].Visible = true;
                dataGridView2.Columns[2].HeaderText = "累计净值";
                dataGridView2.Columns[2].Width = 115;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = true;
                dataGridView2.Columns[6].HeaderText = "日增长率";
                dataGridView2.Columns[6].Width = 115;
                dataGridView2.Columns[7].HeaderText = "申购状态";
                dataGridView2.Columns[7].Width = 115;
                dataGridView2.Columns[8].HeaderText = "赎回状态";
                dataGridView2.Columns[8].Width = 115;
                dataGridView2.Columns[9].HeaderText = "分红送配";
            }
            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
        }

    }
}
