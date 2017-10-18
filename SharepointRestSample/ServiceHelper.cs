using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
namespace SharepointRestSample
{
    public class ServiceHelper
    {
        //public external url
        private static string StartBaseServiceUrl { get; set; } = "";
        //image url start base external url
        private static string BaseUrl { get; set; } = "";

        private System.Net.NetworkCredential credentials
        {
            get
            {
                return new System.Net.NetworkCredential("username", "password", "domain");
            }
        }


        public
            ServiceHelper(bool value = false)
        {
            if (value)
                GetDatas();
        }

        private void GetDatas()
        {
            var jsonStr = GetResponseJson();

            if (!string.IsNullOrEmpty(jsonStr))
            {
                var instance = DeserializeObject<ResponseModel.RootObject>(jsonStr);

                foreach (var item in instance.d.results)
                {
                    Console.WriteLine(item.ID + Environment.NewLine);
                    Console.WriteLine(item.Created + Environment.NewLine);
                    Console.WriteLine(item.EndDate + Environment.NewLine);
                    Console.WriteLine(item.BeginDate + Environment.NewLine);
                    Console.WriteLine(item.RedirectUrl + Environment.NewLine);
                    Console.WriteLine(item.VisibleOnDashboard + Environment.NewLine);
                    Console.WriteLine(item.ImageUrl + Environment.NewLine);
                }
                Console.ReadKey();
            }
            else
                Console.WriteLine("json is null or empty.");
        }

        private string GetResponseJson()
        {
            HttpWebRequest endPRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/getByTitle('listname')/items", StartBaseServiceUrl));

            endPRequest.Method = "GET";
            endPRequest.Accept = "application/json;odata=verbose";

            endPRequest.Credentials = credentials;

            HttpWebResponse respons = (HttpWebResponse)endPRequest.GetResponse();

            try
            {
                WebResponse wResp = endPRequest.GetResponse();

                Stream stream = wResp.GetResponseStream();

                StreamReader sReader = new StreamReader(stream);

                string responseJson = sReader.ReadToEnd();

                sReader.Close();

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                Console.ReadKey();

                return string.Empty;
            }
        }

        public string GetImageUrl(int id)
        {
            HttpWebRequest endPRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/getByTitle('listname')/items?$filter=ID eq {1}&$select=FileRef/FileRef", StartBaseServiceUrl, id));

            endPRequest.Method = "GET";
            endPRequest.Accept = "application/json;odata=verbose";

            endPRequest.Credentials = credentials;

            HttpWebResponse respons = (HttpWebResponse)endPRequest.GetResponse();

            try
            {
                WebResponse wResp = endPRequest.GetResponse();

                Stream stream = wResp.GetResponseStream();

                StreamReader sReader = new StreamReader(stream);

                string responseJson = sReader.ReadToEnd();

                var instance = DeserializeObject<ImageUrlResponseModel.RootObject>(responseJson);

                sReader.Close();

                return BaseUrl + instance.d.results.FirstOrDefault().FileRef;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                Console.ReadKey();

                return string.Empty;
            }
        }


        private T DeserializeObject<T>(string pXmlizedString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(pXmlizedString);
            }
            catch
            { }
            return default(T);
        }
    }
}
