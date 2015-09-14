using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using ORM_Principle.Configuration;
using ORM_Principle.Contracts;
using ORM_Principle.TypeConverters;

namespace ORM_Principle.DB
{
    public class DataContext
    {
        private IDBProvider _provider = null;
        private EntitySetConfiguration _entityConfiguration = null;

        private DataContext()
        {
            this._entityConfiguration = ConfigurationManager.GetSection("entitySetConfiguration") as EntitySetConfiguration;
        }

        public DataContext(string SchemaConfigurationName)
            : this()
        {
            this._provider = DBProviderFactory.CreateDBProvider(SchemaConfigurationName) as IDBProvider;
        }

        public T GetEntity<T>()
            where T : class
        {
            T entity = Activator.CreateInstance(typeof(T)) as T;
            EntityConfiguration entityConfiguration =
                this._entityConfiguration.EntityConfigurations.GetConfigurationFromType(typeof(T).FullName);

            IDataReader reader = this._provider.ExecuteQuery("SELECT * FROM " + entityConfiguration.SchemaName, null);

            while (reader.Read())
            {
                MethodInfo bindingMethod = this.GetType().GetMethod("DoObjectBinding", BindingFlags.Instance | BindingFlags.NonPublic);
                bindingMethod = bindingMethod.MakeGenericMethod(typeof(T));
                bindingMethod.Invoke(this, new object[] { reader, entity, entityConfiguration });
            }

            reader.Close();

            return entity;
        }

        public TCollection GetEntities<TCollection, T>()
            where TCollection : ICollection<T>
            where T : class
        {
            TCollection entityCollection = (TCollection)Activator.CreateInstance(typeof(TCollection));
            EntityConfiguration entityConfiguration =
                this._entityConfiguration.EntityConfigurations.GetConfigurationFromType(typeof(T).FullName);

            IDataReader reader = this._provider.ExecuteQuery("SELECT * FROM " + entityConfiguration.SchemaName, null);

            while (reader.Read())
            {
                T entity = Activator.CreateInstance(typeof(T)) as T;
                
                MethodInfo bindingMethod = this.GetType().GetMethod("DoObjectBinding", BindingFlags.Instance | BindingFlags.NonPublic);
                bindingMethod = bindingMethod.MakeGenericMethod(typeof(T));
                bindingMethod.Invoke(this, new object[] { reader, entity, entityConfiguration });

                entityCollection.Add(entity);
            }

            reader.Close();

            return entityCollection;
        }

        private void DoObjectBinding<T>(ref IDataReader reader, ref T entity, EntityConfiguration entityConfiguration)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                int ordinal = -1;
                Type propType = property.PropertyType;

                // get attribute.
                EntitySchemaMap schemaMap = entityConfiguration.EntitySchemaMaps.GetConfigurationFromPropertyName(property.Name);

                // get column index, if not exist, set -1 to ignore.
                try
                {
                    ordinal = reader.GetOrdinal(schemaMap.EntitySchemaName);
                }
                catch (Exception)
                {
                    ordinal = -1;
                }

                // set value.
                if (ordinal >= 0)
                {
                    ITypeConverter typeConverter = TypeConverterFactory.GetConvertType(propType);

                    if (!propType.IsEnum)
                    {
                        property.SetValue(entity,
                            Convert.ChangeType(typeConverter.Convert(reader.GetValue(ordinal)), propType), null);
                    }
                    else
                    {
                        EnumConverter converter = typeConverter as EnumConverter;
                        property.SetValue(entity,
                            Convert.ChangeType(converter.Convert(propType, reader.GetValue(ordinal)), propType), null);
                    }
                }
            }
        }
    }
}
