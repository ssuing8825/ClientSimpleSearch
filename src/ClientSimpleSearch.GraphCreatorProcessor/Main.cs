using Neo4jClient;
using Neo4jClient.Cypher;
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
    public class GrapheCreatorProcessor
    {
        protected static GraphClient client;

        static void Main(string[] args)
        {
            try
            {
                Connect();
                ClearDatabase();

                var systemCreator = new SystemCreator();
                systemCreator.Create();

                var c = new IndividualClientCreator();
                c.Create();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Connect()
        {
            client = new GraphClient(new Uri("http://localhost.:7474/db/data"));
            client.Connect();
        }

        public static void ClearDatabase()
        {
            var query = client
               .Cypher
               .Start(new { n = All.Nodes })
               .Match("n-[r]-()")
               .Delete("n, r");

            query.ExecuteWithoutResults();

            query = client
            .Cypher
            .Start(new { n = All.Nodes })
            .Delete("n");

            query.ExecuteWithoutResults();

        }
    }
}
