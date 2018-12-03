using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis
{
    class GetKeywords
    {
        /*
         * 对于公司、公司类型、工作地点三项，
         * 不分词，直接统计出现次数
         * @param 存储某项信息的arraylist
         * @return 存储关键词及其出现次数、按次数从大到小排序的二维数组
         *  （一维存储关键词，二维存储出现次数）     
         */
        internal string[,] getkey(ArrayList arr)
        {
            Hashtable ht = new Hashtable();  //暂时存储关键词及其出现次数
            for (int i = 0; i < arr.Count; i++)
            {
                while (ht.Contains(arr[i]) == false)
                {
                    int times = 0;   //times存储关键词的出现次数
                    for (int j = 0; j < arr.Count; j++)
                        if (arr[i].Equals(arr[j])) times++;
                    ht.Add(arr[i], times);   //得到每个词的出现次数和词本身放入hashmap
                }
            }
            return sortkey(ht);  //调用sortkey方法，将关键词按出现次数排序
        }


        /*
         * 将关键词按出现次数排序
         * @param 存储关键词及其出现次数的hashtable
         * @return 存储关键词及其出现次数、按次数从大到小排序的二维数组
         */
        private string[,] sortkey(Hashtable ht)
        {
            string[] arrKey = new string[ht.Count];  //暂存 Hashtable 的键
            int[] arrValue = new int[ht.Count];  //暂存 Hashtable 的值
            ht.Keys.CopyTo(arrKey, 0);
            ht.Values.CopyTo(arrValue, 0);
            Array.Sort(arrValue, arrKey);  //按 HashTable 的值升序排序
            string[,] one = new string[arrKey.Count(), 2];
            for (int i = 0; i < arrKey.Count(); i++)
            {
                one[i, 0] = arrKey[arrKey.Count() - i - 1];  //将关键词和出现次数存入数组中
                one[i, 1] = arrValue[arrKey.Count() - i - 1] + "";
            }
            return one;
        }
    }
}
