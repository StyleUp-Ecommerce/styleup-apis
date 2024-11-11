namespace Core.Identity.Constants.Authorization;

public static class ApiPolicy
{
    public const string ReadAccess = "read_access";
    public const string WriteAccess = "write_access";
    public const string UpdateAccess = "update_access";
    public const string DeleteAccess = "delete_access";  
    
    public const string AdminReadAccess = "admin_read_access";
    public const string AdminWriteAccess = "admin_write_access";
    public const string AdminUpdateAccess = "admin_update_access";
    public const string AdminDeleteAccess = "admin_delete_access";
    public const string UpdateProfilePasswordAccess = "update_profile_password_access";
    public const string Test = "admin_test";
}
