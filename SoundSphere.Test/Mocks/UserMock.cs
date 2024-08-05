using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;

namespace SoundSphere.Test.Mocks
{
    public class UserMock
    {
        private UserMock() { }

        public static List<User> GetUsers() => [GetUser1(), GetUser2()];

        public static List<UserDto> GetUserDtos() => GetUsers().Select(ToDto).ToList();

        public static User GetUser1() => new()
        {
            Id = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Name = "user_name1",
            Email = "user_email1@email.com",
            Password = "#User1_password!",
            Phone = "+40700000000",
            Address = "user_address1",
            Birthday = new DateOnly(2000, 1, 1),
            ImageUrl = "https://user-image1.jpg",
            Role = Role.Admin,
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        };

        public static User GetUser2() => new()
        {
            Id = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Name = "user_name2",
            Email = "user_email2@email.com",
            Password = "#User2_password!",
            Phone = "+40700000001",
            Address = "user_address2",
            Birthday = new DateOnly(2000, 1, 2),
            ImageUrl = "https://user-image2.jpg",
            Role = Role.Moderator,
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        };

        public static User GetNewUser() => new()
        {
            Name = "new_user_name",
            Email = "new_user_email@email.com",
            Phone = "+40700000002",
            Address = "new_user_address",
            Birthday = new DateOnly(2000, 1, 3),
            ImageUrl = "https://new-user-image.jpg",
            Role = Role.Listener
        };

        public static UserDto GetUserDto1() => ToDto(GetUser1());

        public static UserDto GetUserDto2() => ToDto(GetUser2());

        public static UserDto GetNewUserDto() => ToDto(GetNewUser());

        public static UserDto ToDto(User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            Birthday = user.Birthday,
            ImageUrl = user.ImageUrl,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            DeletedAt = user.DeletedAt
        };
    }
}