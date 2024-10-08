﻿namespace Core.IdentityServer.Constants.Authorization
{
    public static class ApiScope
    {
        public const string Read = $"{ApiResource.Template}_read";
        public const string Write = $"{ApiResource.Template}_write";
        public const string Update = $"{ApiResource.Template}_update";
        public const string Delete = $"{ApiResource.Template}_delete";
        public const string Test = $"{ApiResource.Template}_test";

        public const string UpdateProfilePassword = $"{ApiResource.Template}_update_profile_password";

    }
}