using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Raiffeisen.RegistarMjenicaBackend.Helpers;


namespace Raiffeisen.RegistarMjenicaBackend.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDisplayName<TEnum>(this TEnum value) where TEnum : Enum
    {
        var memberInfo = value.GetType().GetMember(value.ToString())[0];
        var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? value.ToString();
    }

    public static List<EnumDisplay<TEnum>> SetDropdown<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new EnumDisplay<TEnum>
            {
                DisplayName = e.GetEnumDisplayName(),
                Value = e
            })
            .ToList();
    }
}