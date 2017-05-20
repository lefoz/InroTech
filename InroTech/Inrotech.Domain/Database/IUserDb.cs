namespace Inrotech.Domain.UserDb
{
    public interface IUserDb
    {
        bool GetUser(string name, string pass);
    }
}