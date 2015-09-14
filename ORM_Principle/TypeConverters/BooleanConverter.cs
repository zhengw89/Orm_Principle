﻿using System;

namespace ORM_Principle.TypeConverters
{
    public class BooleanConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return false;

            if (string.IsNullOrEmpty(ValueToConvert.ToString()))
                return false;
            else if (ValueToConvert.ToString() == "0")
                return false;
            else if (ValueToConvert.ToString() == "1")
                return true;
            else
                return System.Convert.ToBoolean(ValueToConvert);
        }
    }
}
