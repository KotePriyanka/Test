using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace  Nimble.Automation.Accelerators
{
    public class TestManagementToolUpdate
    {
        public static void UpdateResultsInJira(string ExecutionId, string Status)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://172.16.28.151:8080/rest/zapi/latest/execution/" + ExecutionId + "/quickExecute");

            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add("Authorization:Basic YWRtaW46YWRtaW4=");
            httpWebRequest.ContentType = "application/json";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json;
                    if (Status.ToUpper() == "PASS")
                    {
                        json = "{" + "\"status\": 1 " + "}";
                    }
                    else
                    {
                        json = "{" + "\"status\": 2 " + "}";
                    }
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();                 
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                    //Console.WriteLine(result);
                    //Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Upadtion failed. Error Message : " + e.Message);
                Console.ReadLine();
            }

        }

    }
}
