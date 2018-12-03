using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
delegate void SetTextCallback(string text);

namespace webspider
{
    public partial class Form1 : Form
    {
        ArrayList addresses = new ArrayList();
        volatile StringBuilder text = new StringBuilder();

        //初始化窗体，并获取每条兼职信息的address
        public Form1()
        {
            InitializeComponent();
            getHtml getmyhtml = new getHtml();  //为缩短读取时间，
            getmyhtml.getAddress(addresses);   //初始化窗体时即获取每条信息的address
            //gettime();
        }

        private void gettime()
        {
            string address = "https://www.nowcoder.com/recommend";
            HtmlWeb web = new HtmlWeb();

            //通过 HtmlAgilityPack解析html
            HtmlAgilityPack.HtmlDocument doc = web.Load(address + "");
            HtmlNode rootnode = doc.DocumentNode;  //获取根节点
            HtmlNode node = rootnode.SelectSingleNode("//body/div[1]/div[3]/div[5]/div[3]/ul");
           var node2 = rootnode.SelectNodes("//*[@class=\"jobs-list js-intern-jobs-list \"]");
          
            if (node != null)
            {
                Console.WriteLine("OK");
                Console.WriteLine(node.InnerHtml);
            }
            else Console.WriteLine("FAIL");
        }


        //“启动爬虫”按钮，获取信息并写入文件"text.txt"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "正在爬取信息，请耐心等待";
            getHtml getmyhtml = new getHtml();
            string filePath = "text.txt";
            FileStream file = new FileStream(filePath, FileMode.Create);
            StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding("utf-8"));
            Double d = Convert.ToDouble(addresses.Count);
            for (int i = 0; i < addresses.Count; i++)
            {
                writer.Write("JOBID" + "\t"+(i + 1) +"\t"+ getmyhtml.getJobInfo(addresses[i]));
                double num = i / d;
                string result = num.ToString("0%");  //化为百分数
                textBox1.Text = "正在爬取信息，请耐心等待 " + "\n" + "已完成" + result;
            }
            writer.Close();
            file.Close();
            MessageBox.Show("爬取成功!兼职信息已写入文件'text.txt'");  //弹出消息框，提示爬取完成
        }


        //“展示信息”按钮（展示通过爬虫存入文件的兼职信息）
        private void button2_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream("text.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            textBox1.Clear();
            textBox1.Text = reader.ReadToEnd();
        }


        //“边爬边展示”按钮
        private void button3_Click(object sender, EventArgs e)
        {
            getHtml getmyhtml = new getHtml();
            textBox1.Text = "正在爬取信息，请耐心等待";
            Thread download = new Thread(new ThreadStart(show)); //开启新线程爬取信息
            download.Priority = ThreadPriority.BelowNormal;
            download.Start();
        }

        //新线程，爬取信息的同时，在窗体展示信息
        private void show()
        {

            getHtml getmyhtml = new getHtml();
            string filePath = "text2.txt";
            FileStream file = new FileStream(filePath, FileMode.Create);
            StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding("utf-8"));
            for (int i = 0; i < addresses.Count; i++)
            {
                string singletext = "JOB" + (i + 1) + getmyhtml.getJobInfo(addresses[i]);
                writer.Write(singletext);
                text.Append(singletext);
                if (i % 50 == 49) this.SetText(text + "");
            }
            writer.Close();
            file.Close();
            MessageBox.Show("爬取成功!兼职信息已写入文件'text2.txt'");
        }

        //多线程的windows窗体控制
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {

                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
