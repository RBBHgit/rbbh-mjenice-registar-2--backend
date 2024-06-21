namespace Raiffeisen.RegistarMjenica.Web.Extensions;

public static class BoolExtensions
{
    public static string GetCustomTextNull(this bool? value, string? trueText, string? falseText, string? nullText)
    {
        return value switch
        {
            null => nullText,
            true => trueText,
            false => falseText
        };
    }

    public static string GetCustomText(this bool value, string? trueText, string? falseText)
    {
        return value switch
        {
            true => trueText,
            false => falseText
        };
    }
}