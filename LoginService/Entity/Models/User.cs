﻿namespace LoginService.Entity.Models
{
    public class User
    {
        public int Id { get; set; } 


        public string UserName { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int RoleType { get; set; }
    }
}
