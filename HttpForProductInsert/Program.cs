using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpForProductInsert
{
    public class Program
    { 
        static async Task Main(string[] args)
        {
           

             using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"
            var result = await client.GetAsync("https://fakestoreapi.com/products");
           // var result = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
             var userJson = result.Content.ReadAsStringAsync().Result;
            List<Product> users = JsonConvert.DeserializeObject<List<Product>>(userJson);


            using (SqlConnection conn = new SqlConnection("server=UT-LTP-076;Database=EcommerceDB;Trusted_Connection=True;")) 
            using (SqlCommand comm = new SqlCommand())
             

            for (int i = 0; i <= users.Count; i++)
            {
                     
                    if (i<1)
                    {
                        comm.Connection = conn;
                        conn.Open();
                    }

                    comm.CommandText = "INSERT INTO Product (title,price,CatagoryId,description,image) VALUES (@title,@price,@category,@description,@image)";
                    //SqlParameter id = comm.Parameters.AddWithValue("@id", users[i].id);
                    SqlParameter title = comm.Parameters.AddWithValue("@title", users[i].title);
                    SqlParameter price = comm.Parameters.AddWithValue("@price", users[i].price);
                    if(users[i].category== "electronics")
                    {
                        SqlParameter category = comm.Parameters.AddWithValue("@category", 1);
                    }
                    else if(users[i].category == "jewelery")
                    {
                        SqlParameter category = comm.Parameters.AddWithValue("@category", 2);
                    }
                    else if (users[i].category == "men's clothing")
                    {
                        SqlParameter category = comm.Parameters.AddWithValue("@category", 3);
                    }
                    else if (users[i].category == "women's clothing")
                    {
                        SqlParameter category = comm.Parameters.AddWithValue("@category", 4);
                    } 
                    SqlParameter description = comm.Parameters.AddWithValue("@description", users[i].description);
                    SqlParameter image = comm.Parameters.AddWithValue("@image", users[i].image);

               

                    comm.ExecuteNonQuery();

                    comm.Parameters.Clear();
                }
            Console.WriteLine(result.StatusCode);
        }
    } 
    public class Product
    {
        public Int64 id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }

        

    }
}
