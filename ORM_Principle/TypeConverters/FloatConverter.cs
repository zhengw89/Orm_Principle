using System;

namespace ORM_Principle.TypeConverters
{
    public class FloatConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0.0f;

            return System.Convert.ToSingle(ValueToConvert);
        }
    }
}
