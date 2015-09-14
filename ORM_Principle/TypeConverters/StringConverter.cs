using System;

namespace ORM_Principle.TypeConverters
{
    public class StringConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return string.Empty;

            return ValueToConvert.ToString();
        }
    }
}
