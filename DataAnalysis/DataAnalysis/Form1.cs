using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAnalysis
{
    public partial class Form1 : Form
    {
        static int num;  //存储岗位数
        string[,] data;  //存储岗位信息，每行存储一条信息，每列分别存储岗位名、公司、公司类型、地点、职责、岗位要求
        Filehandler myhandler = new Filehandler();
        Segment mysegment = new Segment();
        GetKeywords getmykeyword = new GetKeywords();

        public Form1()
        {
            InitializeComponent();

            data = myhandler.getInfo();   //读取文件，将工作信息分项存入二维数组
            num = data.GetLength(0);
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            ArrayList list4 = new ArrayList();
            ArrayList list6 = new ArrayList();
            for (int i = 0; i < data.GetLength(0); i++)  //将二维数组的各项信息分项存入ArrayList中
            {
                list1.Add(data[i, 0]);  //岗位
                list2.Add(data[i, 1]);  //公司
                list3.Add(data[i, 2]);  //公司类型
                list4.Add(data[i, 3]);  //工作地点
                list6.Add(data[i, 5].ToLower());  //岗位要求
            }
            showpage1(list1);  //岗位分布
            showpage2(list2);  //公司分布
            showpage3(list3);  //行业分布
            showpage4(list4);  //地点分布
            showpage5(list6);  //职业技能 
            showpage6(list6);  //综合素质
            showpage7();  //总体分析
        }


        //Page1 岗位分布
        private void showpage1(ArrayList list1)
        {
            //关键词字符串，用于筛选需要的关键词
            string str1 = "游戏算法前端测试数据挖掘数据分析后端系统运维客户端网页服务端架构师产品助理数据库服务器端移动iosandroidweb运营";
            string[,] jobstr = mysegment.segment(list1);  //获取关键词及其权重
            int j = 0;
            for (int i = 0; i < 100; i++)
            {
                if (j < 10)
                {
                    if (str1.Contains(jobstr[i, 0]) && !jobstr[i, 0].Equals("数据"))
                    {
                        chart1.Series[0].Points.AddXY(jobstr[i, 0], jobstr[i, 1]);  //将关键词添加到图表中
                        j++;
                    }
                }
            }
        }


        //page2 公司分布
        private void showpage2(ArrayList list2)
        {
            string[,] companystr = getmykeyword.getkey(list2);  //获取关键词及其频率
            for (int i = 0; i < 15; i++)
                chart2.Series[0].Points.AddXY(companystr[i, 0], companystr[i, 1]);  //将关键词添加到图表中

            //树控件
            for (int i = 0; i < companystr.GetLength(0); i++)
            {
                TreeNode fn1 = treeView1.Nodes.Add(companystr[i, 0] + "(" + companystr[i, 1] + ")");  //添加父节点
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (data[j, 1].Equals(companystr[i, 0]))
                    {
                        TreeNode n1 = new TreeNode(data[j, 0]);  //创建子节点
                        fn1.Nodes.Add(n1);  //子节点添加到父节点
                    }
                }
            }
        }


        //page3 行业分布
        private void showpage3(ArrayList list3)
        {

            string[,] comtypestr = getmykeyword.getkey(list3);  //获取关键词及其频率
            for (int i = 10; i >= 0; i--)
                chart3.Series[0].Points.AddXY(comtypestr[i, 0], comtypestr[i, 1]);  //将关键词添加到图表中

            //树控件
            for (int i = 0; i < comtypestr.GetLength(0); i++)
            {
                TreeNode fn1 = treeView2.Nodes.Add(comtypestr[i, 0] + "(" + comtypestr[i, 1] + ")");  //添加父节点
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (data[j, 2].Equals(comtypestr[i, 0]))
                    {
                        TreeNode n1 = new TreeNode(data[j, 0]);  //创建子节点
                        fn1.Nodes.Add(n1);  //子节点添加到父节点
                    }
                }
            }
        }


        //page4 工作地点分布
        private void showpage4(ArrayList list4)
        {
            string[,] locationstr = getmykeyword.getkey(list4);  //获取关键词及其频率
            for (int i = 0; i < 5; i++)
                chart4.Series[0].Points.AddXY(locationstr[i, 0], locationstr[i, 1]);  //将关键词添加到图表中
            int othernum = list4.Count;
            for (int i = 0; i < 5; i++) othernum = othernum - Convert.ToInt32(locationstr[i, 1]);
            chart4.Series[0].Points.AddXY("其它", othernum);

            //树控件
            for (int i = 0; i < locationstr.GetLength(0); i++)
            {
                TreeNode fn1 = treeView3.Nodes.Add(locationstr[i, 0] + "(" + locationstr[i, 1] + ")");  //添加父节点
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (data[j, 3].Equals(locationstr[i, 0]))
                    {
                        TreeNode n1 = new TreeNode(data[j, 0]);  //创建子节点
                        fn1.Nodes.Add(n1);  //子节点添加到父节点
                    }
                }
            }
        }


        //page5 职业技能
        private void showpage5(ArrayList list6)
        {
            string[,] dutystr = mysegment.segment(list6);  //获取关键词及其权重
            //关键词字符串，用于筛选需要的关键词
            string str = "javascriptjavapythoncc++linuxcsssqlwebiosandroidhtmlphp多线程数据库数据结构设计";
            int j = 0;
            for (int i = 0; i < 100; i++)
            {
                if (j < 10)
                {
                    if (str.Contains(dutystr[i, 0].ToLower()))
                    {
                        chart5.Series[0].Points.AddXY(dutystr[i, 0], dutystr[i, 1]);  //将关键词添加到图表中
                        j++;
                    }
                }
            }
        }


        //page6 综合素质
        private void showpage6(ArrayList list6)
        {
            string[,] requirestr = mysegment.segment(list6);  //获取关键词及其权重
            //关键词字符串，用于筛选需要的关键词
            string str = "认真负责沟通交流学习习惯经验合作热爱逻辑思维责任心解决问题协作理解";
            int j = 0;
            for (int i = 0; i < 100; i++)
            {
                if (j < 10)
                {
                    if (str.Contains(requirestr[i, 0]))
                    {
                        chart6.Series[0].Points.AddXY(requirestr[i, 0], requirestr[i, 1]);  //将关键词添加到图表中
                        j++;
                    }
                }
            }
        }


        //Page7 总体分析
        private void showpage7()
        {
            richTextBox1.Select(2, 2);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.Select(5, 2);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.Select(19, 4);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox2.Select(2, 4);
            richTextBox2.SelectionColor = Color.Red;
            richTextBox2.Select(7, 6);
            richTextBox2.SelectionColor = Color.Red;
            richTextBox3.Select(15, 3);
            richTextBox3.SelectionColor = Color.Red;
            richTextBox3.Select(20, 3);
            richTextBox3.SelectionColor = Color.Red;
            richTextBox4.Select(9, 4);
            richTextBox4.SelectionColor = Color.Red;
            richTextBox4.Select(14, 4);
            richTextBox4.SelectionColor = Color.Red;
            richTextBox4.Select(31, 7);
            richTextBox4.SelectionColor = Color.Red;
        }


        /*
        * 双击TreeView子节点事件
        */
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;  //获取点击的节点
            if (tn.Nodes.Count == 0)  //如果点击的节点是子节点
            {
                string[] temp = e.Node.Text.Split('\t');  //将节点的text分词
                int number = Convert.ToInt32(temp[1]) - 1;  //获取节点对应的岗位的唯一ID
                Form2 form2 = new Form2();  //初始化一个新窗体
                form2.Text = temp[2];  //将窗体标题设为岗位名称
                TextBox text = new TextBox();  //创建TextBox
                text.Height = 350;
                text.Width = 400;
                text.Multiline = true;  //允许多行显示
                text.ReadOnly = true;  //设为只读
                text.Text = "岗位：" + temp[2] + "\r\n公司：" + data[number, 1] + "\r\n公司类型：" + data[number, 2] + "\r\n工作地：" + data[number, 3] + "\r\n \r\n" + data[number, 4] + "\r\n \r\n" + data[number, 5];
                form2.Controls.Add(text);  //显示岗位的具体信息
                form2.Show();

            }
        }
    }
}
