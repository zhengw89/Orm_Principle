using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class SchemaSetConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("schemas", IsRequired = true)]
        [ConfigurationCollection(typeof(SchemaConfigurationCollection), AddItemName = "schema", CollectionType = ConfigurationElementCollectionType.BasicMap)]
        public SchemaConfigurationCollection Schemas { get { return base["schemas"] as SchemaConfigurationCollection; } }
    }
}
