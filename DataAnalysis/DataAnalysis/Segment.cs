using JiebaNet.Analyser;
using System;
using System.Collections;
using System.Text;

namespace DataAnalysis
{
    class Segment
    {
        /*
         * 此方法用于获取关键词
         *  @param list
         *  @return 存储关键词及其权重的二维数组
         */
        public string[,] segment(ArrayList arr)
        {
            ArrayList list = new ArrayList();
            string[,] one = new string[150, 2];  //存储关键词及其权重
            StringBuilder mystr = new StringBuilder();  //StringBuilder
            for (int i = 0; i < arr.Count; i++) mystr.Append(arr[i]);  //将每一项的信息放到一起，便于分词

             var extractor = new TfidfExtractor();  //结巴分词
            // 提取关键词（取前150个）
            var keywords = extractor.ExtractTagsWithWeight(mystr + "", 150);
            int j = 0;
            foreach (var keyword in keywords)
            {
                list.Add(keyword.Word);
                int weight = Convert.ToInt32(keyword.Weight * 1000);
                one[j, 0] = keyword.Word;
                one[j, 1] = weight + "";
                j++;
            }
            return one;
        }
    }
}
