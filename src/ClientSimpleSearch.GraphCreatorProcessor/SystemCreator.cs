using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimpleSearch.GraphCreatorProcessor
{
   public class SystemCreator :CreatorBase
    {
       private const string query = "dbo.GetSystems";

       public override void Create()
       {
           var json = CreateSystems();
           Debug.WriteLine(json);

           RunAsync(json).Wait();
       }

       private string CreateSystems()
       {
           var connection = new SqlConnection(connectionString);

           var actionBuilder = new StringBuilder();
           actionBuilder.Append("{\"query\" : \"CREATE (n:System { props } )\",");
           actionBuilder.Append("\"params\" : {");
           actionBuilder.Append("\"props\" : [ ");

           SqlCommand command = new SqlCommand(query, connection);
           connection.Open();
           SqlDataReader reader = command.ExecuteReader();
           // http://stackoverflow.com/questions/19494846/neo4jclient-doubts-about-crud-api/19506992#19506992

           int i = 0;
           if (reader.HasRows)
           {
               while (reader.Read())
               {
                   //Begin Adding Clients 
                   actionBuilder.Append("{");
                   actionBuilder.AppendFormat("\"name\" : \"{0}\"", reader.GetString(0));
                   actionBuilder.Append("},");

                   Console.WriteLine(i);
                   i++;

               }
           }
           else
           {
               Console.WriteLine("No rows found.");
           }
           reader.Close();

           //Get rid of the last comma. 
           actionBuilder.Remove(actionBuilder.Length - 1, 1);
           //End loop
           actionBuilder.Append("]");
           actionBuilder.Append("}");
           actionBuilder.Append("}");

           return actionBuilder.ToString();
       }

       static async Task RunAsync(string jsonContent)
       {
           using (var client = new HttpClient())
           {
               client.BaseAddress = new Uri("http://localhost:7474/");
               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               var sc = new StringContent(jsonContent);

               HttpResponseMessage response = await client.PostAsync("db/data/cypher/", sc);

               if (response.IsSuccessStatusCode)
               {
                   Console.WriteLine("Success");
               }
               else
               {
                   Console.WriteLine(response.Content.ReadAsStringAsync().Result);
               }

           }

       }

    }
}
