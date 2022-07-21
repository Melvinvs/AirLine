using LoginService.Entity.Models;

namespace LoginService.Entity
{
    public class DataRepository : IdataRepository
    {
        private readonly LoginDbContext db;

        public DataRepository(LoginDbContext db)
        {
            this.db = db;   
        }

        public User Register(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return db.User.FirstOrDefault(u => u.UserName == user.UserName);
        }

        public User GetUser(string userName)
        {
            return db.User.Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}
