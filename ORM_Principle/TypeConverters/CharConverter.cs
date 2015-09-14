using System;

namespace ORM_Principle.TypeConverters
{
    public class CharConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return (char)0x0;

            return System.Convert.ToChar(ValueToConvert);
        }
    }
}
