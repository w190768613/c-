using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;


namespace webspider
{
    class getHtml
    {

        /**
        *通过每条兼职信息的jobid和companyid,确定每条信息对应页面的地址*
        * */
        public void getAddress(ArrayList addresses)
        {
            string[] city = { "北京", "上海", "杭州", "成都", "深圳"};
            string preAddress = "https://www.nowcoder.com/recommend-intern/list?token=&page=1&address=";
            string preAddress2 = "https://www.nowcoder.com/recommend-intern/list?token=&page=";
            string postAddress = "&address=";
            string jobaddress1 = "https://www.nowcoder.com/recommend-intern/";
            string jobaddress2 = "?jobId=";
            string address, jsonText, dataText;
            int totalpage;  //存储某个城市对应的兼职信息的页数

            //遍历city数组，获取每个城市的兼职信息
            for (int i = 0; i < 5; i++)
            {
                address = preAddress + city[i];  //这里的address对应某城市信息列表的第一页的json页面
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                jsonText = reader.ReadToEnd();  //获取页面的json
                JObject jo = JObject.Parse(jsonText); //解析json
                dataText = jo["data"].ToString();
                JObject jo2 = JObject.Parse(dataText);
                totalpage = Convert.ToInt32(jo2["totalPage"].ToString());//获取此城市兼职信息的页数

                //依次访问每个城市的兼职信息列表对应的json页面
                for (int j = 1; j < totalpage + 1; j++)
                {
                    JArray ja = (JArray)JsonConvert.DeserializeObject(jo2["jobList"].ToString());
                    //对json中的每条兼职信息，存储其id 和companyid
                    for (int k = 0; k < ja.Count; k++)
                        addresses.Add(jobaddress1 + ja[k]["internCompanyId"].ToString() + jobaddress2 + ja[k]["id"].ToString());

                    //访问下个列表的json页面
                    address = preAddress2 + ((j + 1).ToString()) + postAddress + city[i];
                    request = (HttpWebRequest)WebRequest.Create(address);
                    response = (HttpWebResponse)request.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    jsonText = reader.ReadToEnd();
                    jo = JObject.Parse(jsonText);
                    dataText = jo["data"].ToString();
                    jo2 = JObject.Parse(dataText);
                }
            }
        }


        /**
         *返回每个地址对应的兼职信息*
         * */
        public string getJobInfo(object address)
        {
            HtmlWeb web = new HtmlWeb();

            //通过 HtmlAgilityPack解析html
            HtmlAgilityPack.HtmlDocument doc = web.Load(address + "");
            HtmlNode rootnode = doc.DocumentNode;  //获取根节点
            HtmlNode node = rootnode.SelectSingleNode("//*[@class=\"nk-content \"]");
            HtmlNode h2 = node.SelectSingleNode("//h2");
            HtmlNode dl1 = node.SelectSingleNode("//dl[@class=\"job-duty\"]");
            HtmlNode dl2 = node.SelectSingleNode("//dl[not(@class)]");
            HtmlNode node2 = rootnode.SelectSingleNode("//*[@class=\"nk-bar\"]");
            HtmlNode comname = node2.SelectSingleNode(".//h3[@class=\"teacher-name\"]");
            HtmlNode company = node2.SelectSingleNode(".//div[@class=\"rec-info\"]");
            HtmlNode comtype = company.SelectSingleNode(".//p[@class=\"com-type\"]");
            HtmlNode location = company.SelectSingleNode(".//p[@class=\"com-lbs\"]");
            HtmlNode comDetail = node2.SelectSingleNode(".//div[@class=\"com-detail\"]");
            string jobtext = h2.InnerText +"\n"+"&&&"+comname.InnerText+ "\n" + "&&&" + comtype.InnerText + "\n" + "&&&" + location.InnerText + "\n" + "&&&" + dl1.InnerText + "\n" + "&&&" + dl2.InnerText + "\n";
            return jobtext;
        }
    }
}


