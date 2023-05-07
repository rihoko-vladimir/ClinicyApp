using Dapper;

namespace Clinicy.Auth.Models.Constants;

public record PreparedRequest(string Query, DynamicParameters DynamicParameters);