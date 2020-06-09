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
        DateTime dt = DateTime.Now;
        DataSet ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            datetime();
            
            
            if(File.Exists("new記帳.xml"))//讀檔判斷式
            {
                ds.ReadXml("new記帳.xml");
                if(ds.Tables.Count==0)//如果new記帳.xml為空,則讀記帳.xml
                    ds.ReadXml("記帳.xml");
            }                
            else
                ds.ReadXml("記帳.xml");//如果new記帳.xml不存在
            dataGridView1.DataSource = ds.Tables["Ledger"];
            DataColumn dc = ds.Tables["Ledger"].Columns["筆數"];//"筆數"索引值
            ds.Tables["Ledger"].Constraints.Add("Ledger_Set", dc, true);

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataRow dr;
            dr = ds.Tables["Ledger"].Rows.Find("");
            if (dr != null)
                ds.Tables["Ledger"].Rows.Remove(dr);

        }        

        //負責顯示日期
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
        
        //"新增"案鈕
        private void Add_btn_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog(this);

            DataRow tempRow = ds.Tables["Ledger"].NewRow();
            tempRow["筆數"] = dataGridView1.Rows.Count;
            tempRow["月"] = DateTime.Now.Month.ToString();
            tempRow["日"] = DateTime.Now.Day.ToString();
            tempRow["收支"] = f.GetIncome();
            tempRow["類別"] = f.GetCategory();
            tempRow["項目"] = f.GetItem();
            tempRow["金額"] = f.GetCost();
            tempRow["備註"] = f.GetRemarks();
            ds.Tables["Ledger"].Rows.Add(tempRow);
            ds.WriteXml("new記帳.xml");//存檔
        }


        private void Delete_btn_Click(object sender, EventArgs e)
        {            
            if (dataGridView1.Rows.Count >1)//count<=1會錯誤
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            else
                MessageBox.Show("資料已全部清空");

            ds.WriteXml("new記帳.xml");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {

            }
        }
    }
}