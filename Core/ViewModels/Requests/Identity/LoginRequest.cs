﻿namespace Core.ViewModels.Requests.Identity
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRememmber { get; set; }
    }
}
