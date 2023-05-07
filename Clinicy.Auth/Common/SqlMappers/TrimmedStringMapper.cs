using System.Data;
using Dapper;

namespace Clinicy.Auth.Common.SqlMappers;

public class TrimmedStringMapper : SqlMapper.TypeHandler<string>
{
    public override string Parse(object value)
    {
        var result = (value as string)?.Trim();
        return result!;
    }

    public override void SetValue(IDbDataParameter parameter, string value)
    {
        parameter.Value = value;
    }
}