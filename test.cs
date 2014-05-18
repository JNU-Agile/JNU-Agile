using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FundHelper
{
    public partial class test : Form
    {
        private Fund fund;
        public test(Fund fund)
        {
            InitializeComponent();
            this.fund = fund;
            dataGridView1.DataSource = fund.NetValue;
        }
    }
}
