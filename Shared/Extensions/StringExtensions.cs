namespace Shared.Extensions;

public static class StringExtensions
{
    public static string RemoveInputDto(this string str)
    {
        return str.Replace("UsersInputDto.", "");
    }
}
