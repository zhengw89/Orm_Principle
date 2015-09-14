using System.Collections.Generic;
using System.Data;

namespace ORM_Principle.Contracts
{
    public interface IDBProvider
    {
        void Open();
        void Close();
        void ExecuteCmd(string Statement, Dictionary<string, object> Parameters);
        IDataReader ExecuteQuery(string Statement, Dictionary<string, object> Parameters);
    }
}
