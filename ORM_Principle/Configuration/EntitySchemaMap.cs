using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class EntitySchemaMap : ConfigurationElement
    {
        [ConfigurationProperty("propertyName", IsKey = true, IsRequired = true)]
        public string EntityPropertyName { get { return base["propertyName"].ToString(); } }
        [ConfigurationProperty("schemaName", IsRequired = true)]
        public string EntitySchemaName { get { return base["schemaName"].ToString(); } }
    }
}
