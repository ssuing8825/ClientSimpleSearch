using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientSimpleSearch.Business;

namespace ClientSimpleSearch.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateQuery()
        {

            SearchHelper helper = new SearchHelper();
            Console.WriteLine(helper.CreateQuery("Fred", "Name"));
        }

        [TestMethod]
        public void ExecuteQuery()
        {

            SearchHelper helper = new SearchHelper();
            Console.WriteLine(helper.ExecuteQuery("SELECT top 1 LastName FROM [People]"));

        }

        [TestMethod]
        public void Connect()
        {
            SearchHelper helper = new SearchHelper();
            helper.Connect();
        }

        [TestMethod]
        public void ExecuteFTSQuery()
        {

            SearchHelper helper = new SearchHelper();
            var query = helper.CreateQuery("Marc", "Name");
            Console.WriteLine(helper.ExecuteQuery(query)[0]);
        }

    }
}
