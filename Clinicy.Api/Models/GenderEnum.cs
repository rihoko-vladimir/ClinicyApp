namespace Clinicy.WebApi.Models;

public enum GenderEnum
{
    Male = 'M',
    Female = 'F'
}

public static class GenderExtensions
{
    public static GenderEnum ParseCharToGender(char? gender)
    {
        return gender switch
        {
            'M' => GenderEnum.Male,
            'F' => GenderEnum.Female,
            _ => GenderEnum.Male
        };
    }
}