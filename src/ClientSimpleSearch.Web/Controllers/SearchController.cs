using ClientSimpleSearch.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientSimpleSearch.Web.Controllers
{
    public class SearchController : ApiController
    {
        public string Get(string query, string searchType)
        {
            var searchHelper = new SearchHelper();
            return searchHelper.CreateQuery(query, searchType);
            
            //var list = searchHelper.LocateClient(query, searchType);

            return "value";
        }
    }
}
