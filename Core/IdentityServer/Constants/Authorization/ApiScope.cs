namespace Core.IdentityServer.Constants.Authorization
{
    public static class ApiScope
    {
        public const string Read = $"{ApiResource.User}_read";
        public const string Write = $"{ApiResource.User}_write";
        public const string Update = $"{ApiResource.User}_update";
        public const string Delete = $"{ApiResource.User}_delete";
        public const string Test = $"{ApiResource.User}_test";

        public const string AdminRead = $"{ApiResource.Admin}_read";
        public const string AdminWrite = $"{ApiResource.Admin}_write";
        public const string AdminUpdate = $"{ApiResource.Admin}_update";
        public const string AdminDelete = $"{ApiResource.Admin}_delete";

        public const string UpdateProfilePassword = $"{ApiResource.User}_update_profile_password";

    }
}