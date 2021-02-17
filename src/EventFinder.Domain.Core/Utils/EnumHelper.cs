using System;
using System.Reflection;

namespace EventFinder.Domain.Core.Utils
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo oFieldInfo = value.GetType().GetTypeInfo().GetField(value.ToString());
            StringValueAttribute[] attributes = (StringValueAttribute[])oFieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : value.ToString();

        }
    }
}
