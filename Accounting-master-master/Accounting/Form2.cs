using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting
{
    
    public partial class Form2 : Form
    {
        string[] IncomeCategory = new string[] { "薪資", "獎金" };
        string[] ExpenseCategory = new string[7] { "飲食", "服裝", "住宿", "交通", "育兒", "娛樂", "其他" };

        string[] pay = new string[] {"正職薪水","兼職薪水" };
        string[] award = new string[] {"發票獎","樂透" };
        string[] food= new string[5] {"早餐","午餐","晚餐","宵夜","飲料"};
        string[] cloth = new string[] {"上衣","褲子","內衣","內褲" };
        string[] house = new string[] {"房租","水電費","瓦斯費"};
        string[] traffic = new string[] {"油費","車票","機票","船票"};
        string[] baby = new string[] { "尿布","奶粉" };
        string[] entertainment = new string[] { "唱歌", "看電影" };
        public Form2()
        {
           InitializeComponent();
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            if(this.comboBox1.Text=="")
            {
                MessageBox.Show("請選擇類別");
            }
            else if(this.comboBox2.Text==""&&(this.textBox3.Text=="輸入項目"||this.textBox3.Text==""))
            {
                MessageBox.Show("請輸入項目");
            }
            else if(this.textBox2.Text=="")
            {
                MessageBox.Show("請輸入花費金額");
            }
            else
                this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            this.radioButton1.Checked = true;
            this.comboBox1.Items.AddRange(ExpenseCategory);
            this.textBox3.Text = "輸入項目";
        }
           
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();
            switch (this.comboBox1.SelectedItem.ToString())
            {               
                case "飲食":                    
                    this.comboBox2.Items.AddRange(food);
                    break;
                case "服裝":
                    this.comboBox2.Items.AddRange(cloth);
                    break;
                case "住宿":
                    this.comboBox2.Items.AddRange(house);
                    break;
                case "交通":
                    this.comboBox2.Items.AddRange(traffic);
                    break;
                case "育兒":
                    this.comboBox2.Items.AddRange(baby);
                    break;
                case "娛樂":
                    this.comboBox2.Items.AddRange(entertainment);
                    break;
                case "其他":
                    //this.comboBox2.Items.AddRange("");
                    break;


                default:
                    break;
            }
        }

        

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton2.Checked==true)
            {
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.AddRange(IncomeCategory);
                
            }
            else
            {
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.AddRange(ExpenseCategory);
                
            }
            this.comboBox1.SelectedIndex=0;

        }
        public string GetCategory()
        {
            return this.comboBox1.Text.ToString();
        }
        public string GetIncome()
        {
            if (this.radioButton1.Checked == true)
                return "支出";
            if (this.radioButton2.Checked == true)
                return "收入";
            return "";
        }
        public string GetItem()
        {
            if (this.textBox3.Text != "" && this.textBox3.Text != "輸入項目")
                return this.textBox3.Text.ToString();
            else
            {
                return this.comboBox2.Text.ToString();
            }

        }
        public string GetCost()
        {
            return this.textBox2.Text.ToString();
        }
        public string GetRemarks()
        {
            return this.textBox1.Text.ToString();
        }
    }
}
