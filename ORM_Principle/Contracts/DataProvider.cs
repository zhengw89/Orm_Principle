using System.Collections.Generic;
using System.Data;

namespace ORM_Principle.Contracts
{
    public abstract class DataProvider : IDBProvider
    {
        public abstract void Open();
        public abstract void Close();
        public abstract void ExecuteCmd(string Statement, Dictionary<string, object> Parameters);
        public abstract IDataReader ExecuteQuery(string Statement, Dictionary<string, object> Parameters);
    }
}
