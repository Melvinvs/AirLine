using LoginService.Entity.Models;

namespace LoginService.Entity
{
    public interface IdataRepository
    {
        User Register(User user);

        User GetUser(string userName);
    }
}
