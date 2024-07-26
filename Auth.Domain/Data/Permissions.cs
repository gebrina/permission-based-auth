namespace Auth.Domain.Data;

public enum Permissions
{
    ADMIN, // manages everything
    EDITOR, // edits & delete something he/she created
    VIEWER  // can only view datas created by editor 
}