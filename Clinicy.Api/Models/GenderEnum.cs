namespace Clinicy.WebApi.Models;

public enum GenderEnum
{
    Male = 'M',
    Female = 'F',
    Unknown = 'U',
    None = 0
}

public static class GenderExtensions
{
    public static GenderEnum ParseCharToGender(char? gender)
    {
        return gender switch
        {
            'M' => GenderEnum.Male,
            'F' => GenderEnum.Female,
            _ => GenderEnum.Unknown
        };
    }
}