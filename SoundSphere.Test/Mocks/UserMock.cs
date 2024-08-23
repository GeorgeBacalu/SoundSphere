using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Test.Mocks
{
    public class UserMock
    {
        private UserMock() { }

        public static readonly List<User> _users = [new()
        {
            Id = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Name = "John Doe",
            Email = "john.doe@email.com",
            PasswordSalt = "8N2aeuESLAW/U7ugfotVkA==",
            PasswordHash = "2BQB4efoBi4Z5VldE8ErF0qQ47NvO9+0hF0al7PmJ24=",
            Phone = "+40721543701",
            Address = "123 Main St, Boston, USA",
            Birthday = new DateOnly(1980, 2, 15),
            ImageUrl = "https://john_doe.jpg",
            Role = Role.Admin,
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Name = "Jane Smith",
            Email = "jane.smith@email.com",
            PasswordSalt = "YQg1izpe9i4sytzAGgHI7Q==",
            PasswordHash = "3JaeXTimekdGh/REM6606mLuKrYRoiR41ZU/G6lB+QY=",
            Phone = "+40756321802",
            Address = "456 Oak St, London, UK",
            Birthday = new DateOnly(1982, 7, 10),
            ImageUrl = "https://jane_smith.jpg",
            Role = Role.Admin,
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Name = "Michael Johnson",
            Email = "michael.johnson@email.com",
            PasswordSalt = "bFfX/bLhotMNHzTFJm6mOg==",
            PasswordHash = "hwITjH+aBIuqnoBtW2QYYaL07ZYfdhzFtO3f5FMLps4=",
            Phone = "+40789712303",
            Address = "789 Pine St, Madrid, Spain",
            Birthday = new DateOnly(1990, 11, 20),
            ImageUrl = "https://michael_johnson.jpg",
            Role = Role.Admin,
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Name = "Laura Brown",
            Email = "laura.brown@email.com",
            PasswordSalt = "sqNvyZ7e7GkjoEqU9YfMAw==",
            PasswordHash = "3s4NRoTE5JNNU3oBn1Nu7/TvBCb7oEJ4rG6NDRoeYyo=",
            Phone = "+40734289604",
            Address = "333 Elm St, Paris, France",
            Birthday = new DateOnly(1985, 8, 25),
            ImageUrl = "https://laura_brown.jpg",
            Role = Role.Moderator,
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Name = "Robert Davis",
            Email = "robert.davis@email.com",
            PasswordSalt = "Nsh49T0WsVz+bOVqLNnzdA==",
            PasswordHash = "5iTYDGwqxY+oxmeR/tv0LYx+9nu8nxWUoPEyjTXKsPA=",
            Phone = "+40754321805",
            Address = "555 Oak St, Berlin, Germany",
            Birthday = new DateOnly(1988, 5, 12),
            ImageUrl = "https://robert_davis.jpg",
            Role = Role.Moderator,
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Name = "Emily Wilson",
            Email = "emily.wilson@email.com",
            PasswordSalt = "jyjr+pUA1grf5+b2JYWLQw==",
            PasswordHash = "gG3FRFHv+UsSpIZt721JS8sJ5xyzleeSBL7+r5GiUsA=",
            Phone = "+40789012606",
            Address = "777 Pine St, Sydney, Australia",
            Birthday = new DateOnly(1995, 9, 8),
            ImageUrl = "https://emily_wilson.jpg",
            Role = Role.Moderator,
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Name = "Michaela Taylor",
            Email = "michaela.taylor@email.com",
            PasswordSalt = "Mq6REd3vYBoqGTaoyJ0mHA==",
            PasswordHash = "bA1qhE2oW/YKJ+/wIP72g6RddIqtXz20bjsnwLu0/AY=",
            Phone = "+40723145607",
            Address = "999 Elm St, Rome, Italy",
            Birthday = new DateOnly(1983, 12, 7),
            ImageUrl = "https://michaela_taylor.jpg",
            Role = Role.Listener,
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Name = "David Anderson",
            Email = "david.anderson@email.com",
            PasswordSalt = "I+vzT0Q0h9v6BokkhHH40g==",
            PasswordHash = "+reqhXnGiShHUC5sxP02v23kKaYjedIRfZAYg9/3Mkw=",
            Phone = "+40787654308",
            Address = "111 Oak St, Moscow, Russia",
            Birthday = new DateOnly(1992, 4, 23),
            ImageUrl = "https://david_anderson.jpg",
            Role = Role.Listener,
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Name = "Sophia Garcia",
            Email = "sophia.garcia@email.com",
            PasswordSalt = "8auwB8NbccCHKdS1S+9sUg==",
            PasswordHash = "vBz8hkle8HsLLhVuCFuQaaYJw4ds0CmxCJBvacBp/pU=",
            Phone = "+40754321809",
            Address = "333 Pine St, Athens, Greece",
            Birthday = new DateOnly(1998, 7, 30),
            ImageUrl = "https://sophia_garcia.jpg",
            Role = Role.Listener,
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Name = "Joseph Wilson",
            Email = "joseph.wilson@email.com",
            PasswordSalt = "MHN0/ddi3b/HJXWTGm2RNw==",
            PasswordHash = "+BiUsbLF9nCTfJJyoZCMQz8yVuD1FNfmWQZ7w8K8ucU=",
            Phone = "+40789012610",
            Address = "555 Elm St, Madrid, Spain",
            Birthday = new DateOnly(1991, 3, 14),
            ImageUrl = "https://joseph_wilson.jpg",
            Role = Role.Listener,
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }];

        public static readonly User _newUser = new()
        {
            Name = "Olivia Martinez",
            Email = "olivia.martinez@email.com",
            PasswordSalt = "zhbTGpnXdSQedOoZX3E8Vw==",
            PasswordHash = "HevTSA0PtWByNjcE4cuWAHj8Xo15o0DqPrKDtQ/iOro=",
            Phone = "+40723145611",
            Address = "777 Oak St, Tokyo, Japan",
            Birthday = new DateOnly(1999, 10, 17),
            ImageUrl = "https://olivia_martinez.jpg",
            Role = Role.Listener
        };

        public static readonly List<UserDto> _userDtos = _users.Select(ToDto).ToList();

        public static readonly UserDto _newUserDto = ToDto(_newUser);

        public static readonly List<User> _usersPagination = _users.Where(user => user.DeletedAt == null).Take(10).ToList();

        public static readonly List<UserDto> _userDtosPagination = _usersPagination.Select(ToDto).ToList();

        public static readonly UserPaginationRequest _userPayload = new(SortCriteria: null, SeachCriteria: null, Name: null, Email: null, DateRange: null, Role: null);

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