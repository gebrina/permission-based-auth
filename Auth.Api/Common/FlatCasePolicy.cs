using System.Text.Json;

namespace Auth.Api.Common;

public class FlatCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));
        return name.ToLower();
    }
}