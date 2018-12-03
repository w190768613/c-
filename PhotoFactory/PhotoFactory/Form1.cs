using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoFactory
{
    public partial class Form1 : Form
    {
        private string filepath;  //存储打开的图片的路径
        private Bitmap origImg;  //存储原始图片
        private Bitmap curImg;  //存储当前图片

        Function myfunction = new Function();

        public Form1()
        {
            InitializeComponent();
        }

        //"打开"按钮
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //OpenFileDialog
            ofd.Title = "请选择一幅图像";  //设置文件对话框标题
            ofd.Filter = "图片文件(*.png, *.jpg, *.jpeg, *.bmp) | *.png; *.jpg; *.jpeg; *.bmp";  //筛选文件类型
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filepath = ofd.FileName;
                FileStream fs = File.OpenRead(filepath);
                Image img = Image.FromStream(fs);
                int width = img.Width;  //获取图片长宽
                int height = img.Height;

                //设置picturebox的大小，让图片以原长宽比例显示
                pictureBox1.Height = pictureBox1.Width * height / width;  //先保持picturebox宽度不变，改变高度
                pictureBox2.Height = pictureBox2.Width * height / width;
                if (pictureBox2.Height > 400)  //如果高度太大，
                {
                    pictureBox2.Height = 400;  //让高度等于400
                    pictureBox2.Width = pictureBox2.Height * width / height;  //按比例改变宽度
                    pictureBox1.Height = 400;  //让高度等于400
                    pictureBox1.Width = pictureBox2.Height * width / height;  //按比例改变宽度
                }

                fs.Close();
                origImg = new Bitmap(img);  //初始化 origImg
                curImg = new Bitmap(img);   //初始化 curImg
                pictureBox1.Image = origImg;  //设置piturebox的图像
                pictureBox2.Image = origImg;
            }
        }


        //"保存"按钮
        private void button2_Click(object sender, EventArgs e)
        {
            curImg.Save(filepath);  //将图片保存到原位置
        }


        //"另存为"按钮
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();  //SaveFileDialog
            sfd.Filter = "图片文件(*.png, *.jpg, *.jpeg, *.bmp) | *.png; *.jpg; *.jpeg; *.bmp";     //设置文件类型   
            if (sfd.ShowDialog() == DialogResult.OK)  //选定位置后
            {
                curImg.Save(sfd.FileName);  //保存图片
                MessageBox.Show("保存成功！");  //弹出消息框提示另存成功
            }
        }


        //左旋
        private void button4_Click(object sender, EventArgs e)
        {
            curImg = myfunction.LRotate(curImg);  //调用Rotate方法
            int h = pictureBox2.Height;  //改变pictureBox2的大小,以适应图片
            pictureBox2.Height = pictureBox2.Width;
            pictureBox2.Width = h;
            pictureBox2.Image = curImg;
           
        }


        //右旋
        private void button5_Click(object sender, EventArgs e)
        {
            curImg = myfunction.RRotate(curImg);   //调用Rotate方法
            int h = pictureBox2.Height;  //改变pictureBox2的大小,以适应图片
            pictureBox2.Height = pictureBox2.Width;
            pictureBox2.Width = h;
            pictureBox2.Image = curImg;
        }


        //左右翻转
        private void button6_Click(object sender, EventArgs e)
        {
            curImg = myfunction.LRReverse(curImg);  //调用LRReverse方法实现左右翻转
            pictureBox2.Image = curImg;
        }


        //上下翻转
        private void button7_Click(object sender, EventArgs e)
        {
            curImg = myfunction.UDReverse(curImg);  //调用UDReverse方法实现上下翻转
            pictureBox2.Image = curImg;
        }


        //灰度化
        private void button8_Click(object sender, EventArgs e)
        {
            curImg = myfunction.ToGray(curImg);  //调用ToGray方法实现灰度化
            pictureBox2.Image = curImg;
        }


        //雾化
        private void button10_Click(object sender, EventArgs e)
        {
            curImg = myfunction.Atomization(curImg);  //调用Atomization方法实现雾化
            pictureBox2.Image = curImg;
        }


        //取消
        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = origImg;  //设置pictureBox2的图像为origImg
            Rectangle rect = new Rectangle(0, 0, origImg.Width, origImg.Height);
            curImg = origImg.Clone(rect, origImg.PixelFormat);  //让curImg与origImg相同
            textBox1.Text = "";  //所有textBox归零
            textBox2.Text = "";
            textBox3.Text = "";
            trackBar1.Value = 0;  //所有trackBar归零
            trackBar2.Value = 0;
            trackBar3.Value = 0;
        }


        //颜色改变-R
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();  //textBox中显示对应的trackBar的数值
            curImg = myfunction.ColorChange(curImg, trackBar1.Value, 0, 0);  //调用ColorChange改变颜色
            pictureBox2.Image = curImg;
        }
        //textbox获得焦点时的按键事件
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))  //如果按下enter键
            {
                trackBar1.Value = Convert.ToInt32(textBox1.Text);  //设置trackBar的值为对应的textBox的输入
                curImg = myfunction.ColorChange(curImg, trackBar1.Value, 0, 0);
                pictureBox2.Image = curImg;
            }
        }


        //颜色改变-G
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();  //textBox中显示对应的trackBar的数值
            curImg = myfunction.ColorChange(curImg, 0, trackBar2.Value, 0);  //调用ColorChange改变颜色
            pictureBox2.Image = curImg;
        }
        //textbox获得焦点时的按键事件
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))  //如果按下enter键
            {
                trackBar2.Value = Convert.ToInt32(textBox2.Text);  //设置trackBar的值为对应的textBox的输入
                curImg = myfunction.ColorChange(curImg, 0, trackBar2.Value, 0);
                pictureBox2.Image = curImg;
            }
        }


        //颜色改变-B
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();  //textBox中显示对应的trackBar的数值
            curImg = myfunction.ColorChange(curImg, 0, 0, trackBar3.Value);  //调用ColorChange改变颜色
            pictureBox2.Image = curImg;
        }
        //textbox获得焦点时的按键事件
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))  //如果按下enter键
            {
                trackBar3.Value = Convert.ToInt32(textBox3.Text);  //设置trackBar的值为对应的textBox的输入
                curImg = myfunction.ColorChange(curImg, 0, 0, trackBar3.Value);
                pictureBox2.Image = curImg;
            }
        }
    }
}