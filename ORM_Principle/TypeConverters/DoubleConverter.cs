using System;

namespace ORM_Principle.TypeConverters
{
    public class DoubleConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0.0d;

            return System.Convert.ToDouble(ValueToConvert);
        }
    }
}
