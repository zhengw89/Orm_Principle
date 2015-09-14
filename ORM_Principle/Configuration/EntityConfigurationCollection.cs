using System.Collections;
using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class EntityConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as EntityConfiguration).EntityType;
        }
        
        public EntityConfiguration GetConfigurationFromType(string Type)
        {
            IEnumerator navigator = this.GetEnumerator();

            while (navigator.MoveNext())
            {
                // found, return immediately.
                if ((navigator.Current as EntityConfiguration).EntityType == Type)
                    return navigator.Current as EntityConfiguration;
            }

            // not found.
            return null;
        }
    }
}
