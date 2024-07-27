namespace Auth.Domain.Dtos;

public class CreateRoleDto
{
    public string Name { get; set; } = string.Empty;
    public IList<string> ActionNames;
}


