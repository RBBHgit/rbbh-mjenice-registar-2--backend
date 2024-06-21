using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DataAccess.Enums;

namespace DataAccess.Common.Extensions;

public static class EnumExtensions
{
    public static string ReasonToBackColor(this ExemptionReason value)
    {
        return value switch
        {
            ExemptionReason.Blocked => "background-color:#AD1C42",
            ExemptionReason.ReturnedToClient => "background-color:#40826d",
            ExemptionReason.TransferredToAnotherContract => "background-color:#f8de7e",
            ExemptionReason.ReturnedToDataImport => "background-color:#4682B4",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static string GetDisplayName(this ExemptionReason value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var displayAttribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? value.ToString();
    }
}