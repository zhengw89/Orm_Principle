using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ORM_Principle.TypeConverters;

namespace ORM_Principle
{
    public class ProgramStep3
    {
        static void Main(string[] args)
        {
            // step 2. handling data type convert.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand(@"SELECT ProductID, ProductName, ReorderLevel FROM Products", db);
            List<Product> products = new List<Product>();

            db.Open();
            SqlDataReader reader = dbcmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Product product = new Product();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo property = product.GetType().GetProperty(reader.GetName(i));
                    Type propType = property.PropertyType;

                    ITypeConverter typeConverter = TypeConverterFactory.GetConvertType(propType);

                    if (!propType.IsEnum)
                    {
                        property.SetValue(product,
                            Convert.ChangeType(typeConverter.Convert(reader.GetValue(i)), propType), null);
                    }
                    else
                    {
                        EnumConverter converter = typeConverter as EnumConverter;
                        property.SetValue(product,
                            Convert.ChangeType(converter.Convert(propType, reader.GetValue(i)), propType), null);
                    }
                }

                products.Add(product);
            }

            reader.Close();
            db.Close();

            foreach (Product product in products)
            {
                Console.WriteLine("id: {0}, name: {1}, level: {2}",
                    product.ProductID, product.ProductName, Enum.GetName(typeof(ReorderLevel), product.ReorderLevel));
            }

            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
