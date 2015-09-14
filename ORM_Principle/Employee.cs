using System;
using ORM_Principle.Contracts;

namespace ORM_Principle
{
    public class Employee : IDataSourceColumnMapper
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        // [DataSourceColumn("HomePhone")] // this line is for phrase 4.
        public string Phone { get; set; }

        public string GetDataSourceColumn(string PropertyName)
        {
            switch (PropertyName)
            {
                case "EmployeeID":
                    return "EmployeeID";

                case "FirstName":
                    return "FirstName";

                case "LastName":
                    return "LastName";

                case "Title":
                    return "Title";

                case "Phone":
                    return "HomePhone";

                default:
                    throw new NotSupportedException("ERROR_PROPERTY_CANT_MAP_DATA_SOURCE_COLUMN");
            }
        }
    }
}
