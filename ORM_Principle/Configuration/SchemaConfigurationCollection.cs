using System.Collections;
using System.Configuration;

namespace ORM_Principle.Configuration
{
    public class SchemaConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SchemaConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SchemaConfiguration).Name;
        }

        public SchemaConfiguration GetConfigurationFromName(string Name)
        {
            IEnumerator navigator = this.GetEnumerator();

            while (navigator.MoveNext())
            {
                // found, return immediately.
                if ((navigator.Current as SchemaConfiguration).Name == Name)
                    return navigator.Current as SchemaConfiguration;
            }

            // not found.
            return null;
        }
    }
}
