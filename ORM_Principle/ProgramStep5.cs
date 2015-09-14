using System;
using ORM_Principle.DB;

namespace ORM_Principle
{
    public class ProgramStep5
    {
        static void Main(string[] args)
        {
            DataContext db = new DataContext("sql");
            EmployeeCollection employees = db.GetEntities<EmployeeCollection, Employee>();

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
