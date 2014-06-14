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
using System.Windows.Forms.DataVisualization.Charting;

namespace FundHelper
{
    public partial class Main : Form
    {
        private static Search search = new Search();
        private static int RowIndex;
        private static WebService webService = new WebService();
        private static List<Fund> FundList = new List<Fund>();


        //酒店排序方法：当日净值高到低
        public class Sort1 : IComparer<Fund>
        {
            public int Compare(Fund x, Fund y)
            {
                return (y.NetValueToday.CompareTo(x.NetValueToday));
            }
        }
        //酒店排序方法：累计净值高到低
        public class Sort2 : IComparer<Fund>
        {
            public int Compare(Fund x, Fund y)
            {
                return (y.TotalNetValueToday.CompareTo(x.TotalNetValueToday));
            }
        }
        //酒店排序方法：日增长值高到低
        public class Sort3 : IComparer<Fund>
        {
            public int Compare(Fund x, Fund y)
            {
                return (y.NetValueInsToday.CompareTo(x.NetValueInsToday));
            }
        }
        //酒店排序方法：日增长率高到低
        public class Sort4 : IComparer<Fund>
        {
            public int Compare(Fund x, Fund y)
            {
                return (y.NetValueInsRateToday.CompareTo(x.NetValueInsRateToday));
            }
        }
        public Main()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            FundList = webService.GetAllFund();
            InitDataGridView1();
        }

        private void InitDataGridView1()
        {
            var q = from f in FundList
                    select f.Type;
            comboBox2.DataSource = q.Distinct().ToList();
            dataGridView1.DataSource = FundList;
            /*
            dataGridView1.Columns[0].HeaderText = "基金代码";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "简称";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderText = "全称";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderText = "类型";
            dataGridView1.Columns[3].Width = 100;
             */
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
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
            button5.Enabled = false;
            button4.Enabled = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            chart1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = true;
            chart1.Visible = true;
            dataGridView2.Visible = false;
            chart1.Series.Clear();
            this.ToLineChart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button4.Enabled = true;
            dataGridView2.Visible = true;
            chart1.Series.Clear();
            chart1.Visible = false;
        }

        private void ToLineChart()
        {
            Fund fund = FundList[RowIndex];
            Dictionary<string, string> value = new Dictionary<string, string>();
            ArrayList al = new ArrayList();

            chart1.ChartAreas[0].AxisX.Title = "日期";
            chart1.ChartAreas[0].AxisY.Title = "净值";

            //chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            //chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2;
            //chart1.Titles[0].Name = fund.Name + "基金净值变化情况";

            //无数据情况
            if (fund.NetValue.Count == 0)
            {
                return;
            }
            if (chart1.Series.Count < 2)
            {
                chart1.Series.Add("Series2");
                chart1.Series.Add("Series3");
                chart1.Series["Series3"].ChartType = SeriesChartType.Line;
                chart1.Series["Series2"].ChartType = SeriesChartType.Line;
            }

            if (fund.NetValue[0].NetValue != string.Empty
                && fund.NetValue[0].AccumulativeNetValue != string.Empty)
            {
                foreach (NetValueOfFund netValue in fund.NetValue)
                {
                    value.Add(netValue.Date, netValue.NetValue);
                    al.Add(netValue.AccumulativeNetValue);
                }
                chart1.Series[0].LegendText = "单位净值（元）";
                chart1.Series[1].LegendText = "累计净值（元）";
            }
            if (fund.NetValue[0].EarningPer10000 != string.Empty)
            {
                foreach (NetValueOfFund netValue in fund.NetValue)
                {
                    value.Add(netValue.Date, netValue.EarningPer10000);
                }
                chart1.Series[0].LegendText = "每万份收益（元）";
                //chart1.Legends[0].Position
                chart1.Series.Remove(chart1.Series[1]);
            }

            if (al.Count != 0)
            {
                chart1.Series["Series3"].Points.DataBindXY(value.Keys, al);
                chart1.Series[1].ToolTip = "日期：#VALX\n净值：#VAL";
            }
            chart1.Series["Series2"].Points.DataBindXY(value.Keys, value.Values);
            chart1.Series[0].ToolTip = "日期：#VALX\n净值：#VAL";
            chart1.Visible = true;
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            int _currentPointX = e.X;
            int _currentPointY = e.Y;

            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(_currentPointX, _currentPointY), true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(_currentPointX, _currentPointY), true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = -1;
            if (comboBox1.SelectedIndex == 0)
                result = search.SearchByCode(FundList, textBox1.Text);
            else
                result = search.SearchByName(FundList, textBox1.Text);
            if (result < 0)
                MessageBox.Show("搜索失败");
            else
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[result].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = result;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            for (int i = 0; i < FundList.Count; i++)
            {
                if (FundList[i].Type == comboBox2.SelectedItem.ToString())
                {
                    dataGridView1.Rows[i].Selected = true;
                }
            }
        }

        //选择排名依据
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            FundList = webService.GetAllFund();
            InitDataGridView1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                FundList.Sort(new Sort1());
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                FundList.Sort(new Sort2());
            }
            else if (comboBox3.SelectedIndex == 2)
            {
                FundList.Sort(new Sort3());
            }
            else if (comboBox3.SelectedIndex == 3)
            {
                FundList.Sort(new Sort4());
            }
            dataGridView1.DataSource = FundList;
        }


    }
}
