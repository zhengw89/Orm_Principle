using System;

namespace ORM_Principle
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataSourceColumnAttribute : Attribute
    {
        public string Name { get; private set; }

        public DataSourceColumnAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}
