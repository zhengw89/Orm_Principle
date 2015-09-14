using System.Collections;
using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class EntitySchemaMapCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EntitySchemaMap();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as EntitySchemaMap).EntityPropertyName;
        }
        
        public EntitySchemaMap GetConfigurationFromPropertyName(string PropertyName)
        {
            IEnumerator navigator = this.GetEnumerator();

            while (navigator.MoveNext())
            {
                // found, return immediately.
                if ((navigator.Current as EntitySchemaMap).EntityPropertyName == PropertyName)
                    return navigator.Current as EntitySchemaMap;
            }

            // not found.
            return null;
        }

        public EntitySchemaMap GetConfigurationFromSchemaName(string SchemaName)
        {
            IEnumerator navigator = this.GetEnumerator();

            while (navigator.MoveNext())
            {
                // found, return immediately.
                if ((navigator.Current as EntitySchemaMap).EntitySchemaName == SchemaName)
                    return navigator.Current as EntitySchemaMap;
            }

            // not found.
            return null;
        }
    }
}
