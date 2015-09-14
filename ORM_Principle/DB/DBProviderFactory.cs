using System;
using System.Configuration;
using ORM_Principle.Configuration;
using ORM_Principle.Contracts;

namespace ORM_Principle.DB
{
    public class DBProviderFactory
    {
        public static IDBProvider CreateDBProvider(string ConfigurationName)
        {
            SchemaSetConfiguration schemaSetConfiguration =
                ConfigurationManager.GetSection("schemaSetConfiguration") as SchemaSetConfiguration;
            SchemaConfiguration schemaConfiguration = 
                schemaSetConfiguration.Schemas.GetConfigurationFromName(ConfigurationName);

            IDBProvider provider = 
                Activator.CreateInstance(schemaConfiguration.GetProviderType(), schemaConfiguration) as IDBProvider;

            return provider;
        }
    }
}
