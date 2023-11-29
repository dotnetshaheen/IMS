using System.Reflection;

namespace Domain.Extensions
{
    public static class EnumExtension
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttributes<TAttribute>()
                        .OfType<TAttribute>()
                        .FirstOrDefault();
        }
    }
}
