﻿using System;

namespace ORM_Principle.TypeConverters
{
    public class LongConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0;

            return System.Convert.ToInt64(ValueToConvert);
        }
    }
}
