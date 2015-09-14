using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class EntitySetConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("entities", IsRequired = false, IsKey = false)]
        [ConfigurationCollection(typeof(EntityConfigurationCollection), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "entity")]
        public EntityConfigurationCollection EntityConfigurations { get { return base["entities"] as EntityConfigurationCollection; } }        
    }
}
