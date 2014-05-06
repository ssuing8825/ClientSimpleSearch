using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimpleSearch.Business
{
    public interface IClientDocumentRepository
    {
        ClientDocument Find(int id);
    }
}
