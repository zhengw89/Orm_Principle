namespace ORM_Principle
{
    public enum ReorderLevel
    {
        None    = 0,
        Lowest  = 5,
        Lower   = 10,
        Medium  = 15,
        More    = 20,
        High    = 25,
        Often   = 30
    }

    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public ReorderLevel ReorderLevel { get; set; }
    }
}
