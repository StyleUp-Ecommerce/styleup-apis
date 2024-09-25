using CleanBase.Core.Domain.Exceptions;

namespace Core.Helpers
{
    public static class EnumExtension
    {
        public static List<TEnum> ParseEnumList<TEnum>(this string data) where TEnum : struct
        {
            try
            {
                return !string.IsNullOrEmpty(data)
                    ? data.Split(',')
                        .Select(c => Enum.Parse<TEnum>(c.Trim().ToUpper()))
                        .ToList()
                    : new List<TEnum>();
            }
            catch(Exception ex)
            {
                throw new DomainException(ex.Message,"ERROR_VALUE",null,400,null);
            }
        }

        public static string SerializeEnumList<TEnum>(this List<TEnum> enumList) where TEnum : struct
        {
            return enumList != null && enumList.Any()
                ? string.Join(",", enumList.Select(e => e.ToString().ToUpper()))
                : string.Empty;
        }

        public static string ToFriendlyString(this Enum enumValue)
        {
            return enumValue.ToString();
        }

        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, out var result))
            {
                return result;
            }
            throw new ArgumentException($"Unable to convert '{value}' to enum type {typeof(T).Name}");
        }
    }
}
