using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class EntityConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public string EntityType { get { return base["type"].ToString(); } }
        [ConfigurationProperty("schema", IsRequired = true)]
        public string SchemaName { get { return base["schema"].ToString(); } }
        [ConfigurationProperty("maps", IsRequired = true)]
        [ConfigurationCollection(typeof(EntitySchemaMapCollection), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "map")]
        public EntitySchemaMapCollection EntitySchemaMaps { get { return base["maps"] as EntitySchemaMapCollection; } }
    }
}
