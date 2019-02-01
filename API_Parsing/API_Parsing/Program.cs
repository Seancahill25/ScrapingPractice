using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API_Parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("/posts");

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            var posts = JArray.FromObject(JsonConvert.DeserializeObject(content));


            Console.WriteLine(posts[0]["title"]);
        }       
    }
}
