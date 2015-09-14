using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ORM_Principle
{
    public class ProgramStep1
    {
        static void Main(string[] args)
        {
            // step 1. load data from data source and binding with Reflection.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand("SELECT * FROM Customers", db);
            List<Customer> customers = new List<Customer>();

            db.Open();
            SqlDataReader reader = dbcmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);

            while (reader.Read())
            {
                Customer customer = new Customer();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo property = customer.GetType().GetProperty(reader.GetName(i));
                    property.SetValue(customer, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                }

                customers.Add(customer);
            }

            reader.Close();
            db.Close();

            foreach (Customer customer in customers)
            {
                Console.WriteLine("id: {0}, name: {1}, address: {2}, phone: {3}",
                    customer.CustomerID, customer.CompanyName, customer.Address, customer.Phone);
            }

            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
