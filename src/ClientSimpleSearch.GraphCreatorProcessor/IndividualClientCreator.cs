using ClientSimpleSearch.GraphCreatorProcessor.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimpleSearch.GraphCreatorProcessor
{
    public class IndividualClientCreator : CreatorBase
    {
        private const string query = "dbo.GetClients";


        public override void Create()
        {
            var json = CreateClients();
            Debug.WriteLine(json);

            RunAsync(json).Wait();
        }


        private static string CreateClients()
        {

            var connection = new SqlConnection(connectionString);

            var actionBuilder = new StringBuilder();
            actionBuilder.Append("{\"query\" : \"CREATE (n:Client { props } )\",");
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
                    actionBuilder.AppendFormat("\"name\" : \"{0}\",", reader.GetString(0) + " " + reader.GetString(1));
                    actionBuilder.AppendFormat("\"clientid\" : \"{0}\",", reader.GetInt32(2));
                    actionBuilder.AppendFormat("\"clientkey\" : \"{0}\"", reader.GetString(3));
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

        private static string BuildJson()
        {
          
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Client"].ConnectionString);
            String actionTemplate = "{{\"method\" : \"POST\",\"to\" : \"/node\",\"body\" : {{\"Name\" : \"{0}\",\"ClientId\" : \"{1}\",\"ClientKey\" : \"{2}\" }}}}";


            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            // http://stackoverflow.com/questions/19494846/neo4jclient-doubts-about-crud-api/19506992#19506992

            int i = 0;
            var sb = new StringBuilder();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    sb.AppendFormat(actionTemplate, reader.GetString(0) + " " + reader.GetString(1), reader.GetInt32(2), reader.GetString(3));

                    if (reader.HasRows)
                    {
                        sb.Append(",");
                    }
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
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            sb.Insert(0, "[");

            return sb.ToString();

        }

        static async Task RunAsync(string jsonContent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7474/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var sc = new StringContent(jsonContent);

            //    HttpResponseMessage response = await client.PostAsync("db/data/batch/", sc);
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
        private static void CreateClientsUsingTypeProvider()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Client"].ConnectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //http://stackoverflow.com/questions/19494846/neo4jclient-doubts-about-crud-api/19506992#19506992

                int i = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Debug.Write(reader.GetSqlInt32(2));
                        Console.WriteLine(i);
                        var newClient = new Client { ClientId = (Int32)reader.GetSqlInt32(2), Name = reader.GetSqlString(0).ToString() + " " + reader.GetSqlString(1).ToString(), ClientKey = reader.GetSqlString(3).ToString() };
                        client.Cypher
                            .Create("(client:Client {newClient})")
                            .WithParam("newClient", newClient)
                            .ExecuteWithoutResults();
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }

        }


    }
}
