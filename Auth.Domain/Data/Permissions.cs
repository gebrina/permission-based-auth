namespace Auth.Domain.Data;

public enum Permissions
{
    ADMIN, // manages everything
    EDITOR, // edits, deletes something he/she created and view everything
    VIEWER  // can only view datas created by editor 
}