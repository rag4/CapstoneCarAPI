using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDelershipMVC.Models
{
    public class CarDAL
    {
        public string GetData()
        {
            string url = $"https://localhost:44304/api/car/";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string JSON = rd.ReadToEnd();
            return JSON;
        }

        public string GetDataExt(string keyword, string query)
        {
            string url = $"https://localhost:44304/api/car/{keyword}/{query}";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string JSON = rd.ReadToEnd();
            return JSON;
        }

        public List<Cars> GetCars()
        {
            string json = GetData();
            List<Cars> c = JsonConvert.DeserializeObject<List<Cars>>(json);
            return c;
        }

        public List<Cars> GetCarsExt(string keyword, string query)
        {
            string json = GetDataExt(keyword, query);
            List<Cars> c = JsonConvert.DeserializeObject<List<Cars>>(json);
            return c;
        }

        
    }
}
