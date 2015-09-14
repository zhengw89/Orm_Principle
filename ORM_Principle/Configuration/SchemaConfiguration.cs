using System;
using System.Configuration;
using ORM_Principle.Contracts;

namespace ORM_Principle.Configuration
{
    public class SchemaConfiguration : ConfigurationElement, ISchemaConfiguration
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name { get { return base["name"].ToString(); } }
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public string Type { get { return base["type"].ToString(); } }
        [ConfigurationProperty("connectionstring", IsRequired = true, IsKey = true)]
        public string ConnectionString { get { return base["connectionstring"].ToString(); } }

        public string GetConnectionString()
        {
            return this.ConnectionString;
        }

        public Type GetProviderType()
        {
            return System.Type.GetType(this.Type);
        }
    }
}
