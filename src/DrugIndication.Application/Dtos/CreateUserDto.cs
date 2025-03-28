using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Dtos
{
    public class CreateUserDto : UserDto
    {
        public string Role { get; set; }

        public User ToEntity()
        {
            return new(Email, Password)
            {
                Role = Role
            };
        }
    }
}
