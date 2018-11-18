using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace practice1
{
   public class Program
    {
        private static User_dataEntities db;

        static void Main(string[] args) 
        {
            //https://jsonplaceholder.typicode.com/users
            
           
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("/users");
            List<User> user = new List<User>();
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            var users = JArray.FromObject(JsonConvert.DeserializeObject(content));
            Console.WriteLine(users[0]["address"]["city"]);
            

            foreach(var person in users)
            {
                User tempuser = new User
                {
                    id = person["id"].ToString(),
                    name = person["name"].ToString(),
                    username = person["username"].ToString(),
                    email = person["email"].ToString(),
                    city = person["address"]["city"].ToString(),
                    phone = person["phone"].ToString(),
                    website = person["website"].ToString()

                };
                user.Add(tempuser);
            }

            using (SqlConnection connection = new SqlConnection(@"data source=SEAN-PC\SQL;initial catalog=User_data;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))

            {
                User_dataEntities db = new User_dataEntities();
                connection.Open();
                foreach (var person in user)
                {
                    db.Users.Add(person);
                }

                db.SaveChanges();
                connection.Close();

            }
        
        }
    }
}           

           