using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimpleSearch.GraphCreatorProcessor
{
    public abstract class CreatorBase
    {

        //http://nuget.org/packages/Neo4jClient
        protected static GraphClient client;
        protected static string connectionString;

        public abstract void Create();

        public CreatorBase()
        {
             connectionString = ConfigurationManager.ConnectionStrings["Client"].ConnectionString;
        }

    }
}
