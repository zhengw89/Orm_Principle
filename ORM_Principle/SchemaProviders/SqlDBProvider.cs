using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ORM_Principle.Contracts;

namespace ORM_Principle.SchemaProviders
{
    public class SqlDBProvider : DataProvider
    {
        private SqlConnection _db = null;

        public SqlDBProvider(ISchemaConfiguration SchemaConfiguration)
        {
            this._db = new SqlConnection(SchemaConfiguration.GetConnectionString());
        }

        public override void Open()
        {
            this._db.Open();
        }

        public override void Close()
        {
            this._db.Close();
        }

        public override void ExecuteCmd(string Statement, Dictionary<string, object> Parameters)
        {
            SqlCommand cmd = new SqlCommand(Statement, this._db);

            cmd.CommandText = Statement;
            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (KeyValuePair<string, object> param in Parameters)
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            if (this._db.State == ConnectionState.Closed)
                this._db.Open();

            cmd.ExecuteNonQuery();
            this._db.Close();
        }

        public override IDataReader ExecuteQuery(string Statement, Dictionary<string, object> Parameters)
        {
            SqlCommand cmd = new SqlCommand(Statement, this._db);
            SqlDataReader reader = null;

            cmd.CommandText = Statement;
            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (KeyValuePair<string, object> param in Parameters)
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            if (this._db.State == ConnectionState.Closed)
                this._db.Open();

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
            return reader;
        }
    }
}
