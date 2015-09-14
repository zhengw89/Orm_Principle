using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ORM_Principle.Configuration;
using ORM_Principle.TypeConverters;

namespace ORM_Principle
{
    public class ProgramStep4_2
    {
        static void Main(string[] args)
        {
            // step 4-2. data mapping with interface implementation.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand(@"SELECT EmployeeID, LastName, FirstName, Title, HomePhone FROM Employees", db);
            List<Employee> employees = new List<Employee>();

            // for method 2, prepare configuration.
            EntitySetConfiguration entitySetConfiguration =
                ConfigurationManager.GetSection("entitySetConfiguration") as EntitySetConfiguration;
            EntityConfiguration employeeMapConfiguration =
                entitySetConfiguration.EntityConfigurations.GetConfigurationFromType(typeof(Employee).FullName);

            db.Open();
            SqlDataReader reader = dbcmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Employee employee = new Employee();

                PropertyInfo[] properties = employee.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    int ordinal = -1;
                    Type propType = property.PropertyType;

                    // method 1, implement a interface to map entity and schema.
                    //Contracts.IDataSourceColumnMapper dsMapper = employee as Contracts.IDataSourceColumnMapper;

                    // method 2, provision map information from configuration.
                    EntitySchemaMap schemaMap = employeeMapConfiguration.EntitySchemaMaps.GetConfigurationFromPropertyName(property.Name);

                    // get column index, if not exist, set -1 to ignore.
                    try
                    {
                        // ordinal = reader.GetOrdinal(dsMapper.GetDataSourceColumn(property.Name));
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
                            property.SetValue(employee,
                                Convert.ChangeType(typeConverter.Convert(reader.GetValue(ordinal)), propType), null);
                        }
                        else
                        {
                            EnumConverter converter = typeConverter as EnumConverter;
                            property.SetValue(employee,
                                Convert.ChangeType(converter.Convert(propType, reader.GetValue(ordinal)), propType), null);
                        }
                    }
                }

                employees.Add(employee);
            }

            reader.Close();
            db.Close();

            foreach (Employee employee in employees)
            {
                Console.WriteLine("id: {0}, name: {1}, title: {2}, phone: {3}",
                    employee.EmployeeID, employee.FirstName + ' ' + employee.LastName, employee.Title, employee.Phone);
            }

            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}

