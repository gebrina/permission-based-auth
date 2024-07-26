namespace Auth.Domain.Dtos;

public class RoleDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IList<string> Permissions {get;set;} = [];
}