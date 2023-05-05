using Dapper;

namespace Clinicy.WebApi.Models.Constants;

public record PreparedRequest(string Query, DynamicParameters DynamicParameters);