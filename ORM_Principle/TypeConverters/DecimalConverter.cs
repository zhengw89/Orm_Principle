using System;

namespace ORM_Principle.TypeConverters
{
    public class DecimalConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0.0m;

            return System.Convert.ToDecimal(ValueToConvert);
        }
    }
}
