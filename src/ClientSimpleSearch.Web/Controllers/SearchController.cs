using ClientSimpleSearch.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace ClientSimpleSearch.Web.Controllers
{
    public class SearchController : ApiController
    {
        public HttpResponseMessage Get(string query, string searchType)
        {
            var facade = new SearchFacade();
            List<string> list =  facade.CreateAndExecuteQuery(query, searchType);
            var count = facade.GetTotalCount(query, searchType);
            List<string> words = facade.GetWords(query, searchType);
            return CreateResponse(list, count, words);

        }

        private HttpResponseMessage CreateResponse(List<string> list, int totalPossible, List<string> listOfWords)
        {
            var response = new HttpResponseMessage();
            StringBuilder sb = new StringBuilder();

            sb.Append("{\"people\":[" + String.Join(",", list.ToArray()) + "]");
            sb.Append(",\"totalPossible\":" + totalPossible.ToString()); ;
            sb.Append(",\"listofwords\":[\"" + String.Join("\",\"", listOfWords.ToArray()) + "\"]"); ;
            sb.Append("}");

            response.StatusCode = HttpStatusCode.OK;
            response.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            response.Content = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
