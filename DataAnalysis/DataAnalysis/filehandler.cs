using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace DataAnalysis
{
    class Filehandler
    {
        /*
         * 读取文件*
         * return 工作信息*
         */
        public string[,] getInfo()
        {
            FileStream file = new FileStream("text.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string datastring = reader.ReadToEnd();  //读取信息到字符串中
            string[] temp = Regex.Split(datastring, "JOB");  //分割字符串，将信息分条存入数组temp中
            string[,] data = new string[temp.Count() - 1, 6];  //二维数组，行数即信息条数，每列存放信息的不同项
            for (int i = 1; i < temp.Count(); i++)  //遍历temp数组
            {
                //分割数组的每一项，得到每份兼职的 岗位、公司、公司类型、地点、职责、要求；
                string[] tempone = Regex.Split(temp[i], "&&&");
                for (int j = 0; j < tempone.Count(); j++) data[i - 1, j] = tempone[j];
            }
            return data;  //返回存储信息的二维数组
        }
    }
}
