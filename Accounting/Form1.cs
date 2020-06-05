using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Accounting
{
    public partial class Form1 : Form
    {
        int year, month;
        string Year, Month, excelid, path;
        DateTime dt = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            year = dt.Year;
            month = dt.Month;
            if (month < 10) Month = "0" + month.ToString();
            Year = year.ToString();
            excelid = Year + Month;
            path = @"D:\" + excelid + ".txt";
            ShowTableWithGridview();
            datetime();

            if(File.Exists(path))
            {
                string[] data = File.ReadAllLines(path);
                foreach (string s in data)
                {
                    string[] datas = s.Split('+');
                    DataGridViewRowCollection rows = dataGridView1.Rows;
                    rows.Add(datas[0], datas[1], datas[2], datas[3], datas[4], datas[5]);                 
                }
            }                       
        }

        private void ShowTableWithGridview()
        {
            this.dataGridView1.ColumnCount = 6;
            this.dataGridView1.ColumnHeadersVisible = true;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Columns[0].Name = "日期";
            this.dataGridView1.Columns[1].Name = "收支";
            this.dataGridView1.Columns[2].Name = "類別";
            this.dataGridView1.Columns[3].Name = "項目";
            this.dataGridView1.Columns[4].Name = "金額";
            this.dataGridView1.Columns[5].Name = "備註";
        }

        private void datetime()
        {
            int year = dt.Year;
            int month = dt.Month;
            for(int i=year;i>year-6;i--)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
            for(int i=month;i>0;i--)
            {
                comboBox2.Items.Add(i);
            }
            if(month != 12)
            {
                for(int i=12;i>month;i--)
                {
                    comboBox2.Items.Add(i);
                }
            }
            comboBox2.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog(this);
            DataGridViewRowCollection rows = dataGridView1.Rows;
            rows.Add(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString(), f.GetIncome(), f.GetCategory(), f.GetItem(), f.GetCost(), f.GetRemarks());

            if (File.Exists(path) == true)
            {
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "+" + f.GetIncome() + "+" + f.GetCategory() + "+" + f.GetItem() + "+" + f.GetCost() + "+" +f.GetRemarks());
                sw.Flush();
                sw.Close();
            }
            else
            {
                StreamWriter sw = File.CreateText(path);
                sw.WriteLine(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "+" + f.GetIncome() + "+" + f.GetCategory() + "+" + f.GetItem() + "+" + f.GetCost() + "+" + f.GetRemarks());
                sw.Flush();
                sw.Close();
            }
        }
    }
}