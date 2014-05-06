using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using System.Globalization;
using Irony.Interpreter.Ast;
using System.Configuration;
using System.Data.SqlClient;


namespace ClientSimpleSearch.Business
{

    //http://www.sqlservercentral.com/articles/Full-Text+Search+(2008)/64248/
    //http://tilt.carr.no/Post/1/full-text-searching-with-lucene-net



    public class SearchHelper
    {
        private IClientDocumentRepository clientDocumentRepository;
        private SearchGrammar _grammar;


        private Parser _compiler;
        private string error;

        public SearchHelper()
            : this(new ClientDocumentRepository())
        {

        }
        public SearchHelper(IClientDocumentRepository clientDocumentRepository)
        {
            this.clientDocumentRepository = clientDocumentRepository;
            _grammar = new SearchGrammar();
            _compiler = new Irony.Parsing.Parser(_grammar);
            var errors = _compiler.Language.ParserData.Language.Errors;
            if (errors.Count > 0)
            {
                throw new ApplicationException("SearchGrammar contains errors. Investigate using GrammarExplorer." + errors.ToString());
            }
        }

        //public int CountClient(string query, string searchType)
        //{
        //    //this function should just be a scalor.

        //    var queryPredicate = CreatePredicate(query);
        //    StringBuilder sb = new StringBuilder(200);
        //    sb.Append("SELECT Id, '' as PayloadXML, '' as PayloadJSON ");
        //    sb.Append("FROM ClientDocuments ");
        //    sb.AppendFormat("WHERE CONTAINS({0}, '{1}' );", Columns(searchType), queryPredicate);

        //    List<int> list = null;
        //    //using (var context = new ClientLocateDbContext())
        //    //{
        //    //    list = context.ClientDocuments.SqlQuery(sb.ToString()).Select(c => c.Id).Distinct().ToList();
        //    //}
        //    return list.Count;

        //}

        //private string CreatePredicate(string query)
        //{
        //    var queryWithDate = AddDateToQuery(query);

        //    AstNode root = _compiler.Language.ParserData(queryWithDate);
        //    if (!CheckParseErrors())
        //    {
        //        throw new ApplicationException(error);
        //    }

        //    var queryPredicate = SearchGrammar.ConvertQuery(root, SearchGrammar.TermType.Inflectional);
        //    return queryPredicate;
        //}
        //public int CountClient(string name, string phone, string address, string policy)
        //{
        //    StringBuilder sb = new StringBuilder(200);
        //    sb.Append("SELECT Id, '' as PayloadXML, '' as PayloadJSON ");
        //    sb.Append("FROM ClientDocuments ");
        //    sb.AppendFormat("WHERE {0}", BuildPredicate(name, phone, address, policy));

        //    List<int> list = null;
        //    //using (var context = new ClientLocateDbContext())
        //    //{
        //    //    list = context.ClientDocuments.SqlQuery(sb.ToString()).Select(c => c.Id).Distinct().ToList();
        //    //}
        //    return list.Count;
        //}

        public string CreateQuery(string queryTerms, string searchType)
        {
            queryTerms = AddDateToQuery(queryTerms);

            ParseTree root = _compiler.Parse(queryTerms);
            if (!CheckParseErrors())
            {
                throw new ApplicationException(error);
            }

            var queryPredicate = SearchGrammar.ConvertQuery(root.Root);

            // var queryPredicate = SearchGrammar.ConvertQuery(root, SearchGrammar.TermType.Inflectional);
            StringBuilder sb = new StringBuilder(200);
            sb.Append("SELECT top 50 Id, '' as PayloadXML, PayloadJSON ");
            sb.Append("FROM ClientDocuments ");
            sb.AppendFormat("WHERE CONTAINS({0}, '{1}' );", Columns(searchType), queryPredicate);

            return sb.ToString();
        }

        public List<string> ExecuteQuery(string query)
        {
            List<string> list = null;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientLocate"].ConnectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }

            return list;
        }


        //public List<string> LocateClient(string name, string phone, string address, string policy)
        //{

        //    StringBuilder sb = new StringBuilder(200);
        //    sb.Append("SELECT top 50 Id, '' as PayloadXML, PayloadJSON ");
        //    sb.Append("FROM ClientDocuments ");
        //    sb.AppendFormat("WHERE {0}", BuildPredicate(name, phone, address, policy));

        //    List<string> list = null;
        //    //using (var context = new ClientLocateDbContext())
        //    //{
        //    //    list = context.ClientDocuments.SqlQuery(sb.ToString()).Select(c => c.PayloadJson).Distinct().ToList();
        //    //}
        //    return list;

        //}
        //private string BuildPredicate(string name, string phone, string address, string policy)
        //{
        //    List<string> predicate = new List<string>();
        //    if (name != string.Empty)
        //        predicate.Add(CreatePredicate("Name", name));
        //    if (phone != string.Empty)
        //        predicate.Add(CreatePredicate("Phone", phone));
        //    if (address != string.Empty)
        //        predicate.Add(CreatePredicate("Address", address));
        //    if (policy != string.Empty)
        //        predicate.Add(CreatePredicate("Policy", policy));

        //    return String.Join(" or ", predicate.ToArray());
        //}
        //private string CreatePredicate(string column, string query)
        //{
        //    AstNode root = _compiler.Parse(query);

        //    if (!CheckParseErrors())
        //    {
        //        throw new ApplicationException(error);
        //    }
        //    var queryPredicate = SearchGrammar.ConvertQuery(root, SearchGrammar.TermType.Inflectional);
        //    return string.Format("CONTAINS([{0}], '{1}' )", column, queryPredicate);

        //}
  
        private string Columns(string searchType)
        {
            if (searchType.Contains("All")) return "PayloadXml";

            List<string> ftscolumns = new List<string>();
            foreach (var column in searchType.Split(','))
            {
                switch (column)
                {
                    case "Name":
                        ftscolumns.Add("Name");
                        break;
                    case "Address":
                        ftscolumns.Add("Address");
                        break;
                    case "Phone":
                        ftscolumns.Add("Phone");
                        break;
                    case "Policy":
                        ftscolumns.Add("Policy");
                        break;
                    default:
                        ftscolumns.Add("PayloadXml");
                        break;
                }
            }
            return String.Format("({0})", String.Join(",", ftscolumns.ToArray()));
        }
        private string AddDateToQuery(string query)
        {
            DateTime testDate;
            var peices = query.Split(' ');
            string newQuery = string.Empty;

            foreach (var item in peices.ToList())
            {
                if (DateTime.TryParse(item, out testDate))
                {
                    newQuery += "\"" + testDate.ToString("yyyy-MM-ddTHH:mm:ss") + "\"";
                }
                else
                    newQuery += item + " ";
            }
            return newQuery;
        }
        private bool CheckParseErrors()
        {

            //if (_compiler.Context.Errors.Count == 0) return true;
            //error = "Errors: \r\n";
            //foreach (SyntaxError err in _compiler.Context.Errors)
            //    error += err.ToString() + "\r\n";
            //return false;

            if (_compiler.Language.Errors.Count == 0) return true;
            error = "Errors: \r\n";
            foreach (var err in _compiler.Language.Errors)
                error += err.ToString() + "\r\n";
            return false;
        }

        internal List<string> GetWordList(string name)
        {
            return Thesaurus(name); ;
        }
        public List<string> GetWordList(string query, string searchType)
        {
            DateTime testDate;
            var querySplit = query.Split(' ').ToList();
            querySplit.Remove("or");
            querySplit.Remove("and");
            var result = new List<string>();

            foreach (var item in querySplit)
            {
                if (item.Contains('~'))
                {
                    result.AddRange(Thesaurus(item.Replace("~", "").Replace("*", "")));
                }
                else if (item.Contains('*'))
                {
                    result.Add(item.Replace("*", ""));
                }
                else if (DateTime.TryParse(item, out testDate))
                {
                    result.Add(testDate.ToString("MM/dd/yyyy"));
                }
                else
                    result.Add(item);
            }
            return result;
        }

        private List<string> Thesaurus(string word)
        {
            if (word.Contains('~'))
            {
                word = word.Replace("~", "");
            }

            var t = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientSearch"].ConnectionString))
            {
                var command = new SqlCommand("SELECT display_term FROM sys.dm_fts_parser('FORMSOF (THESAURUS, " + word + ")', 1033, 0, 0)", connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    t.Add(reader.GetString(0));
                }

                reader.Close();
            }
            return t;
        }
    }
}