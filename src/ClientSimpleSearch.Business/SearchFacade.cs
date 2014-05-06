using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimpleSearch.Business
{
    public class SearchFacade
    {
        private SearchHelper searchHelper;

        public SearchFacade()
        {
            searchHelper = new SearchHelper();
        }
        public string CreateQuery(string queryTerms, string searchType)
        {
            return searchHelper.CreateQuery(queryTerms, searchType);
        }
        public List<string> CreateAndExecuteQuery(string queryTerms, string searchType)
        {
            var query = searchHelper.CreateQuery(queryTerms, searchType);
            return searchHelper.ExecuteQuery(query);
        }
    }
}
