using System;

namespace ORM_Principle.TypeConverters
{
    public class EnumConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            throw new NotImplementedException();
        }

        public object Convert(Type EnumType, object ValueToConvert)
        {
            if (!EnumType.IsEnum)
                throw new InvalidOperationException("ERROR_TYPE_IS_NOT_ENUMERATION");

            return System.Convert.ChangeType(Enum.Parse(EnumType, ValueToConvert.ToString()), EnumType);
        }
    }
}
