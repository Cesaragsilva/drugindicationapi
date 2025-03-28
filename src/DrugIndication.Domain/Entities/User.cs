using DrugIndication.Domain.Constants;
using DrugIndication.Domain.Helpers;

namespace DrugIndication.Domain.Entities
{
    public class User
    {
        public User()
        {
            
        }

        public User(string email, string password)
        {
            Id = Guid.NewGuid().ToString();

            Email = email;
            Password = HashHelper.Hash(password);
        }

        public string? Id { get; set; } = null;
        public string Email { get; set; } = null!;
        public string Password { get; private set; } = null!;
        public string Role { get; set; } = Roles.User;

    }
}
