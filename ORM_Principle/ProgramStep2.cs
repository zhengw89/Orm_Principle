using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ORM_Principle.TypeConverters;

namespace ORM_Principle
{
    public class ProgramStep2
    {
        static void Main(string[] args)
        {
            // step 2. handling data type convert.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand(
                @"SELECT o.CustomerID, SUM(od.Quantity) AS Qty, SUM(od.Quantity * od.UnitPrice * od.Discount) AS Amount 
                  FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID 
                  GROUP BY o.CustomerID", db);
            List<CustomerAmount> customerAmounts = new List<CustomerAmount>();

            db.Open();
            SqlDataReader reader = dbcmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);

            while (reader.Read())
            {
                CustomerAmount customerAmount = new CustomerAmount();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // object defaultValue = null;

                    //if (propType == typeof(int) || propType == typeof(long) || propType == typeof(short))
                    //    defaultValue = 0;
                    //else if (propType == typeof(float) || propType == typeof(double) || propType == typeof(decimal))
                    //    defaultValue = 0.0;
                    //else if (propType == typeof(bool))
                    //    defaultValue = false;
                    //else if (propType == typeof(char))
                    //    defaultValue = (char)0;
                    //else
                    //    defaultValue = string.Empty;

                    PropertyInfo property = customerAmount.GetType().GetProperty(reader.GetName(i));
                    Type propType = property.PropertyType;
                    ITypeConverter typeConverter = TypeConverterFactory.GetConvertType(propType);

                    property.SetValue(customerAmount,
                        Convert.ChangeType(typeConverter.Convert(reader.GetValue(i)), propType), null);
                }

                customerAmounts.Add(customerAmount);
            }

            reader.Close();
            db.Close();

            foreach (CustomerAmount customer in customerAmounts)
            {
                Console.WriteLine("id: {0}, qty: {1:###,##0}, amount: {2:$###,###,##0}", customer.CustomerID, customer.Qty, customer.Amount);
            }

            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
