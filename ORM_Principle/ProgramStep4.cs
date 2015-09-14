using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ORM_Principle.TypeConverters;

namespace ORM_Principle
{
  public  class ProgramStep4
    {
        static void Main(string[] args)
        {
            // step 4. handling data type convert.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand(@"SELECT EmployeeID, LastName, FirstName, Title, HomePhone FROM Employees", db);
            List<Employee> employees = new List<Employee>(); 

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
                    string dataSourceColumnName = null;

                    // get attribute.
                    DataSourceColumnAttribute[] attributes = 
                        property.GetCustomAttributes(typeof(DataSourceColumnAttribute), true) as DataSourceColumnAttribute[];

                    if (attributes != null && attributes.Length > 0)
                        dataSourceColumnName = attributes[0].Name;
                    else
                        dataSourceColumnName = property.Name;

                    // get column index, if not exist, set -1 to ignore.
                    try
                    {
                        ordinal = reader.GetOrdinal(dataSourceColumnName);
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

