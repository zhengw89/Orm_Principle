using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ORM_Principle
{
    public class ProgramStep0
    {
        static void Main(string[] args)
        {
            // step 0. load data from data source and binding with hard-coding.
            SqlConnection db = new SqlConnection("initial catalog=Northwind; integrated security=SSPI");
            SqlCommand dbcmd = new SqlCommand("SELECT * FROM Customers", db);
            List<Customer> customers = new List<Customer>();

            db.Open();
            SqlDataReader reader = dbcmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);

            while (reader.Read())
            {
                Customer customer = new Customer();

                if (!reader.IsDBNull(reader.GetOrdinal("CustomerID")))
                    customer.CustomerID = reader.GetValue(reader.GetOrdinal("CustomerID")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("CompanyName")))
                    customer.CompanyName = reader.GetValue(reader.GetOrdinal("CompanyName")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("ContactName")))
                    customer.ContactName = reader.GetValue(reader.GetOrdinal("ContactName")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("ContactTitle")))
                    customer.ContactTitle = reader.GetValue(reader.GetOrdinal("ContactTitle")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("Address")))
                    customer.Address = reader.GetValue(reader.GetOrdinal("Address")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("Region")))
                    customer.Region = reader.GetValue(reader.GetOrdinal("Region")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("City")))
                    customer.City = reader.GetValue(reader.GetOrdinal("City")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("PostalCode")))
                    customer.PostalCode = reader.GetValue(reader.GetOrdinal("PostalCode")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("Country")))
                    customer.Country = reader.GetValue(reader.GetOrdinal("Country")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("Phone")))
                    customer.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                if (!reader.IsDBNull(reader.GetOrdinal("Fax")))
                    customer.Fax = reader.GetValue(reader.GetOrdinal("Fax")).ToString();

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
