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
    }
}
