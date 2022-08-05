namespace LoginService.Models
{
    public class UserModel
    {
        public int id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int RoleType { get; set; }

        public string refreshToken { get; set; }

        public DateTime TokenCreated { get; set; }

        public DateTime Expire { get; set; }
    }
}
