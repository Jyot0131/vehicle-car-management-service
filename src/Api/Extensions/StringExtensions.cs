using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        if (value == null) return true;

        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}