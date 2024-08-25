using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Mocks
{
    public class NotificationMock
    {
        private NotificationMock() { }

        public static readonly List<Notification> _notifications = [new() 
        {
            Id = Guid.Parse("7e221fa3-2c22-4573-bf21-cd1d6696b576"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[1],
            Receiver = _users[0],
            Type = NotificationType.Music,
            Message = "Discover the top hits this week!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("36624bfa-f9db-4e81-91e4-c54177e2e817"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[2],
            Receiver = _users[0],
            Type = NotificationType.Music,
            Message = "Explore new albums that just dropped.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d35f8020-9f64-4544-9ed7-f24b97de5ade"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[3],
            Receiver = _users[0],
            Type = NotificationType.Music,
            Message = "Check out the latest music video trending now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1d23fa22-3455-407b-9371-c42d56001de7"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[4],
            Receiver = _users[0],
            Type = NotificationType.Social,
            Message = "Your friend just updated their status!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("51965dce-b06b-40da-84b9-4d56a0e304ce"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[5],
            Receiver = _users[0],
            Type = NotificationType.Social,
            Message = "Join the conversation in a popular thread.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4ede744f-a688-4106-bb9d-a82e25fe067c"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[6],
            Receiver = _users[0],
            Type = NotificationType.Social,
            Message = "You were mentioned in a post by a friend.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a88fda83-356b-46b2-a1fd-041ef5b98270"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[7],
            Receiver = _users[0],
            Type = NotificationType.Account,
            Message = "Complete your profile to unlock new features.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4078255e-d40c-467b-9404-2638772c89a1"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[8],
            Receiver = _users[0],
            Type = NotificationType.Account,
            Message = "Your account settings have been updated successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("635e834e-fc43-4698-8823-191a2c888267"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[9],
            Receiver = _users[0],
            Type = NotificationType.Account,
            Message = "Check out the new features added to your account.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f6c5a67-369c-402b-88a7-0c0535e67411"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[1],
            Receiver = _users[0],
            Type = NotificationType.System,
            Message = "Your subscription has been renewed successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("49b2bef0-cfe1-4290-b703-0927fde4c027"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[2],
            Receiver = _users[0],
            Type = NotificationType.System,
            Message = "Enjoy the new features with the latest update.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f9b5d15a-d589-4349-b2ac-f267bfe308a3"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[3],
            Receiver = _users[0],
            Type = NotificationType.System,
            Message = "We’ve made improvements to your experience. Check it out now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2ba6fa59-3c3b-4599-8a2c-bf34b6aac71b"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[2],
            Receiver = _users[1],
            Type = NotificationType.Music,
            Message = "Live concert alert: Your favorite band is performing next week! Don't miss out.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("df18e464-fe9e-4b4f-b67f-e5f51d093246"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[3],
            Receiver = _users[1],
            Type = NotificationType.Music,
            Message = "Your favorite artist just released a new album. Check it out!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9e16b405-e7fc-4676-9d1d-922ba9fe3eae"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[4],
            Receiver = _users[1],
            Type = NotificationType.Music,
            Message = "New playlist recommendation based on your recent listens!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("49f1caba-5d7e-4f3b-9217-49fefa6c4f3b"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[5],
            Receiver = _users[1],
            Type = NotificationType.Social,
            Message = "Your post has gone viral! Over 100 shares!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("de8664c7-a45b-4e4f-b108-405fc0e50313"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[6],
            Receiver = _users[1],
            Type = NotificationType.Social,
            Message = "Congratulations! Your latest post has received over 100 likes.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cd387c97-361b-4942-b6fe-3cd4149397c3"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[7],
            Receiver = _users[1],
            Type = NotificationType.Social,
            Message = "Your post has been featured in the top trending list!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("17de3d40-3f7a-45f1-a25e-2538c09e4d22"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[8],
            Receiver = _users[1],
            Type = NotificationType.Account,
            Message = "Your profile has been updated successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("eac935b5-c8cf-470d-8709-3a97167e288d"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[9],
            Receiver = _users[1],
            Type = NotificationType.Account,
            Message = "Your account settings have been changed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("19a9cfc5-3907-45ab-b82b-c48314181b07"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[0],
            Receiver = _users[1],
            Type = NotificationType.Account,
            Message = "Your password has been updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6ec9550d-073c-4a17-8d1a-20a54dfd15f1"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[2],
            Receiver = _users[1],
            Type = NotificationType.System,
            Message = "Scheduled system maintenance will take place on Sunday.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("14cb4d71-ad8e-4780-95a4-32d78a57522e"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[3],
            Receiver = _users[1],
            Type = NotificationType.System,
            Message = "We will be performing maintenance this Sunday. Plan accordingly.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e6d24a5c-6f29-42fc-90ad-61ad94664bd1"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            Sender = _users[4],
            Receiver = _users[1],
            Type = NotificationType.System,
            Message = "Reminder: Scheduled maintenance this Sunday.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            DeletedAt = new DateTime(2024, 6, 24, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("5f14e45d-e702-47ed-bb98-dd1f5d556f47"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[3],
            Receiver = _users[2],
            Type = NotificationType.Music,
            Message = "We've updated your playlist based on recent activity.'",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ee11ad32-9d3f-4f45-9a56-5698e0549437"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[4],
            Receiver = _users[2],
            Type = NotificationType.Music,
            Message = "New songs added to your playlist based on your latest listens.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("35b4d624-fdbe-4d9f-b5cc-c9c3d0639bc2"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[5],
            Receiver = _users[2],
            Type = NotificationType.Music,
            Message = "Check out the updated playlist curated for you!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("67b832f0-4883-4df4-9a9d-9b3f0f4efc8e"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[6],
            Receiver = _users[2],
            Type = NotificationType.Social,
            Message = "You have new comments to respond to.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0f583946-c3a2-4619-81f2-d7718b921bd7"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[7],
            Receiver = _users[2],
            Type = NotificationType.Social,
            Message = "You have new likes on your post.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d4517e13-4a7d-4482-961a-c047765cf2dd"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[8],
            Receiver = _users[2],
            Type = NotificationType.Social,
            Message = "You have new followers.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1dc0d6c1-33a2-49f4-87a2-a2df00df00a1"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[9],
            Receiver = _users[2],
            Type = NotificationType.Account,
            Message = "Feature update: New tools available in your account management.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a50cebf0-6fc6-4769-9851-ffdf24551180"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[0],
            Receiver = _users[2],
            Type = NotificationType.Account,
            Message = "Feature update: Enhanced security features added.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3e981aa2-dc28-45af-aa4a-2efecf5a7c3f"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[1],
            Receiver = _users[2],
            Type = NotificationType.Account,
            Message = "Feature update: Improved user interface.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("22d0d3b1-33a3-49f5-87a3-a2df01df01a1"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[3],
            Receiver = _users[2],
            Type = NotificationType.System,
            Message = "Security notice: Please update your password.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("343b7f36-192a-471d-97d8-c422fb22a386"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[4],
            Receiver = _users[2],
            Type = NotificationType.System,
            Message = "Security notice: Unusual login activity detected.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("de4748ab-44c3-40c3-bec9-ba65d66710ab"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            Sender = _users[5],
            Receiver = _users[2],
            Type = NotificationType.System,
            Message = "Security notice: Two-factor authentication enabled.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("33d0d4c2-34a4-4af6-88a4-a3ef02df02a2"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[4],
            Receiver = _users[3],
            Type = NotificationType.Music,
            Message = "Check out the newest albums released this week!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("eb88d004-5c92-4f46-a8b9-8e57da8bc8da"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[5],
            Receiver = _users[3],
            Type = NotificationType.Music,
            Message = "Discover the latest albums of the week!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2230c2a9-be8f-4dbd-a6b2-14da2237395e"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[6],
            Receiver = _users[3],
            Type = NotificationType.Music,
            Message = "New music releases this week you might like!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("44e0d5d3-35a5-4bf7-99a5-a4ff03df03a3"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[7],
            Receiver = _users[3],
            Type = NotificationType.Social,
            Message = "You have been tagged in 10 new photos.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ce6ed6cb-53b2-4f38-aa90-a116c1a5d83c"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[8],
            Receiver = _users[3],
            Type = NotificationType.Social,
            Message = "New comments in posts you follow.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dc3e389c-a5e9-44fb-b865-d360f4996a8c"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[9],
            Receiver = _users[3],
            Type = NotificationType.Social,
            Message = "You have new mentions in your posts.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("55f0e6e4-36a6-4cf8-aaa6-b50004df04a4"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[0],
            Receiver = _users[3],
            Type = NotificationType.Account,
            Message = "Password change confirmation.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e3ee8c5d-1bc0-48ae-b7cd-f82842524725"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[1],
            Receiver = _users[3],
            Type = NotificationType.Account,
            Message = "Your password has been successfully changed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b6643a26-bbef-441b-9b68-9e7a07332340"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[2],
            Receiver = _users[3],
            Type = NotificationType.Account,
            Message = "Password change was successful.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fe62d1cb-797b-47b8-b61d-2fb4bc55ce20"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[4],
            Receiver = _users[3],
            Type = NotificationType.System,
            Message = "Update: Your requested features are now live!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("68659872-cdde-45b9-a8b8-9702a249ab8b"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[5],
            Receiver = _users[3],
            Type = NotificationType.System,
            Message = "New features have been deployed as per your request.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("932a0069-0ea8-4338-b2da-3e1bf0fd907b"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            Sender = _users[6],
            Receiver = _users[3],
            Type = NotificationType.System,
            Message = "Your requested features are now available.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            DeletedAt = new DateTime(2024, 7, 18, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("c8afa171-ee36-424e-a67d-de6d465861f7"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[5],
            Receiver = _users[4],
            Type = NotificationType.Music,
            Message = "Playlist update: New tracks added to your favorite genre.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c07373b0-58d4-4da6-8f5a-d5cb195b9a3f"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[6],
            Receiver = _users[4],
            Type = NotificationType.Music,
            Message = "New tracks have been added to your playlist.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9fcdae03-c4e9-42c8-b2a3-3dca104eb4b0"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[7],
            Receiver = _users[4],
            Type = NotificationType.Music,
            Message = "Your playlist has been updated with new tracks.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("984df627-a5b3-4541-9987-b54e9c1576c0"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[8],
            Receiver = _users[4],
            Type = NotificationType.Social,
            Message = "Thank you for your feedback on social features! We have implemented your suggestions.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d21accdb-62ae-4fea-86b7-9db506aa21da"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[9],
            Receiver = _users[4],
            Type = NotificationType.Social,
            Message = "Your suggestions for social features have been implemented.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("72e5be4e-bd17-49fd-9581-713954704f56"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[0],
            Receiver = _users[4],
            Type = NotificationType.Social,
            Message = "We have implemented your feedback on social features.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b4c57e93-4f26-4868-bfd6-f511fe63d7e3"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[1],
            Receiver = _users[4],
            Type = NotificationType.Account,
            Message = "Account notification: You've earned a new loyalty badge.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b5a62816-699c-4219-8cb0-8ccbb09fee68"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[2],
            Receiver = _users[4],
            Type = NotificationType.Account,
            Message = "Congratulations! You have earned a new loyalty badge.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("25e0ac5c-a0d7-48f9-b3a2-ec340f7a9e02"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[3],
            Receiver = _users[4],
            Type = NotificationType.Account,
            Message = "You have earned a new loyalty badge.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("376659d1-fa63-48b3-956b-f178e7750a04"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[5],
            Receiver = _users[4],
            Type = NotificationType.System,
            Message = "Your app interface has been updated for a better experience.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3ff4bb54-57a8-4ca6-9a30-eb827e0b62c8"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[6],
            Receiver = _users[4],
            Type = NotificationType.System,
            Message = "We have updated your app interface for a better experience.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5d7c239c-d531-4ced-860c-f0e43d5442a3"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            Sender = _users[7],
            Receiver = _users[4],
            Type = NotificationType.System,
            Message = "Enjoy the new app interface update.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("31a74b33-5530-4fd9-9530-c5b31adddba9"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[6],
            Receiver = _users[5],
            Type = NotificationType.Music,
            Message = "A live concert is happening near you. Don't miss out!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2e5a8563-0b6d-443f-92c8-d32abfe56c52"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[7],
            Receiver = _users[5],
            Type = NotificationType.Music,
            Message = "Your favorite band just announced a tour. Tickets are on sale now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8b258db8-5603-487f-ba6e-aa8ee2484bdd"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[8],
            Receiver = _users[5],
            Type = NotificationType.Music,
            Message = "Check out our playlist of trending songs this week!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("938732f5-3bff-426d-a838-657a577c87b1"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[9],
            Receiver = _users[5],
            Type = NotificationType.Social,
            Message = "A friend just shared a photo with you. Take a look!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("74112870-a28d-4ac3-881d-1f18d20a903b"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[0],
            Receiver = _users[5],
            Type = NotificationType.Social,
            Message = "You have a new message from a friend. Open it now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f5fb713d-2899-4869-883a-ae4600550259"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[1],
            Receiver = _users[5],
            Type = NotificationType.Social,
            Message = "Someone tagged you in a post. Check it out!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b7954386-cacc-476c-bfad-01267d262f69"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[2],
            Receiver = _users[5],
            Type = NotificationType.Account,
            Message = "You've successfully logged into your account from a new device.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2a206d3a-c25c-408e-aa7f-c57ef7508a81"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[3],
            Receiver = _users[5],
            Type = NotificationType.Account,
            Message = "Password successfully changed. Keep your account secure.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("712decd3-5fb7-4796-8b65-dfd8174b719a"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[4],
            Receiver = _users[5],
            Type = NotificationType.Account,
            Message = "A new device was added to your account. Review your security settings.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f562d55f-281b-4b48-92fa-e2476b3abf74"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[6],
            Receiver = _users[5],
            Type = NotificationType.System,
            Message = "System maintenance scheduled for tonight. Expect brief downtime.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2798dee8-0dbd-4ff6-ab7a-2f89870f3a74"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[7],
            Receiver = _users[5],
            Type = NotificationType.System,
            Message = "New features added in the latest update. Explore them now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("50b132fa-f8d1-49af-abf2-8fccc66e11f0"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            Sender = _users[8],
            Receiver = _users[5],
            Type = NotificationType.System,
            Message = "We've improved our performance in the latest app update.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 11, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("25dbcfe4-b6aa-44c0-aa2d-e95cca4df9ff"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[7],
            Receiver = _users[6],
            Type = NotificationType.Music,
            Message = "New tracks added to your favorite playlist.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("10d744ac-589f-4db7-8135-a51e8601134f"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[8],
            Receiver = _users[6],
            Type = NotificationType.Music,
            Message = "Your playlist has been updated with new songs.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3b9c29fe-db09-4e97-b1c6-f345e7817ec0"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[9],
            Receiver = _users[6],
            Type = NotificationType.Music,
            Message = "Check out the latest additions to your playlist.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0f96131e-74c2-4acb-82a3-5564c1049086"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[0],
            Receiver = _users[6],
            Type = NotificationType.Social,
            Message = "You have been mentioned in a new comment.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c18c2552-ab4a-4054-bbb1-6c5aa0e90c16"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[1],
            Receiver = _users[6],
            Type = NotificationType.Social,
            Message = "Someone mentioned you in a comment.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1035d3ad-03a1-4e6d-9363-4d57abf0e094"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[2],
            Receiver = _users[6],
            Type = NotificationType.Social,
            Message = "You were tagged in a new comment.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2ea232f6-b229-4846-b028-ecdaa214ef7f"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[3],
            Receiver = _users[6],
            Type = NotificationType.Account,
            Message = "Profile update confirmed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("45b1ddd6-078d-4ee8-87ba-ee3cdd57790a"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[4],
            Receiver = _users[6],
            Type = NotificationType.Account,
            Message = "Your profile has been successfully updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("04316896-5f53-441d-9db4-231492a4d57b"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[5],
            Receiver = _users[6],
            Type = NotificationType.Account,
            Message = "Your profile changes have been saved.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dc41888e-78f4-4fe4-a437-3fd27401d9ea"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[7],
            Receiver = _users[6],
            Type = NotificationType.System,
            Message = "System security patch applied successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("01b3b8e8-84ca-46d5-9a67-f454bcfaff83"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[8],
            Receiver = _users[6],
            Type = NotificationType.System,
            Message = "Security patch has been successfully applied.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("710f2fb0-0d33-4172-b472-08b4e2eec71b"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            Sender = _users[9],
            Receiver = _users[6],
            Type = NotificationType.System,
            Message = "System update completed successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("707e8678-89f3-4670-9fd0-aba89da589ff"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[8],
            Receiver = _users[7],
            Type = NotificationType.Music,
            Message = "New recommendations based on your listening habits.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dd064a28-3e8c-41b2-9d46-57c63d73e276"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[9],
            Receiver = _users[7],
            Type = NotificationType.Music,
            Message = "Check out these new albums based on your recent plays.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("511feb97-9244-452e-af5d-49d2b7eabcb4"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[0],
            Receiver = _users[7],
            Type = NotificationType.Music,
            Message = "Discover new artists similar to your favorites.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ec4f53d0-5554-4ad9-a761-8f2811add21f"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[1],
            Receiver = _users[7],
            Type = NotificationType.Social,
            Message = "Your post has been featured in a trending topic.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fa0e6fa9-21f6-4490-9c84-db57d9051850"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[2],
            Receiver = _users[7],
            Type = NotificationType.Social,
            Message = "Your recent post is gaining popularity!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("eb4ccd1f-905f-4d56-bddc-62663bf52c66"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[3],
            Receiver = _users[7],
            Type = NotificationType.Social,
            Message = "You have new comments on your post.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a492110f-a4ba-4f6e-9565-d0e902b91f36"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[4],
            Receiver = _users[7],
            Type = NotificationType.Account,
            Message = "Your account has been successfully upgraded.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("53482f1c-f686-459b-8d96-67504a61064b"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[5],
            Receiver = _users[7],
            Type = NotificationType.Account,
            Message = "Your subscription plan has been updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c2a38821-b5db-4cef-98b6-beead97e9d44"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[6],
            Receiver = _users[7],
            Type = NotificationType.Account,
            Message = "Your account settings have been updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2b76e979-7def-44c8-9b3c-f6ac0253b86b"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[8],
            Receiver = _users[7],
            Type = NotificationType.System,
            Message = "New system enhancements are available now.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bc9680b6-e3e8-42de-af81-96a8bd3cdd5f"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[9],
            Receiver = _users[7],
            Type = NotificationType.System,
            Message = "System update completed successfully.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c87d903d-8b4b-4533-b162-e58bbf61ae87"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            Sender = _users[0],
            Receiver = _users[7],
            Type = NotificationType.System,
            Message = "Scheduled maintenance has been completed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 4, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("0d1d4015-a33d-4f2c-b947-39cf988b4b1b"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[9],
            Receiver = _users[8],
            Type = NotificationType.Music,
            Message = "A new album by your favorite artist is now available!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5a638c5c-006b-4ea4-b197-e89c7846c649"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[0],
            Receiver = _users[8],
            Type = NotificationType.Music,
            Message = "Check out the latest release from your favorite artist!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3650bfe2-6f60-4de9-aa05-6e1a91e62aa9"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[1],
            Receiver = _users[8],
            Type = NotificationType.Music,
            Message = "New music alert: Your favorite artist has just dropped a new album!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c9b81833-485b-4d0e-91b9-dbc65a299c55"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[2],
            Receiver = _users[8],
            Type = NotificationType.Social,
            Message = "Your friend just started a live stream. Join in now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e89248ed-5b5f-46bb-85c7-410d490814d7"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[3],
            Receiver = _users[8],
            Type = NotificationType.Social,
            Message = "A friend just sent you a virtual gift. Check it out!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("66774f24-fe4b-4efb-9a0b-6fb8fa5326c4"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[4],
            Receiver = _users[8],
            Type = NotificationType.Social,
            Message = "Your friend just created a new event. RSVP now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f245bed0-ab5b-4470-8c0c-373ef2a10c3b"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[5],
            Receiver = _users[8],
            Type = NotificationType.Account,
            Message = "Your account balance has been updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("32d9d85a-ca9f-4443-b5dc-835ffd19ae3f"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[6],
            Receiver = _users[8],
            Type = NotificationType.Account,
            Message = "You've earned rewards points. Redeem them now!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("98a0cf0e-c65f-401e-a87c-e79a8b3b147e"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[7],
            Receiver = _users[8],
            Type = NotificationType.Account,
            Message = "You've unlocked a new achievement. View it in your profile!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0960f07f-ce1f-485b-83f2-55f33a7bbd92"),
            SenderId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[9],
            Receiver = _users[8],
            Type = NotificationType.System,
            Message = "A backup of your data was successfully completed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5788b299-fdaa-44d9-ac97-ea079ee745f3"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[0],
            Receiver = _users[8],
            Type = NotificationType.System,
            Message = "Your settings have been successfully updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7c42e285-6c3f-4109-8e8d-82ad0ae3ae51"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            Sender = _users[1],
            Receiver = _users[8],
            Type = NotificationType.System,
            Message = "You've successfully synced your device. Everything is up to date.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4ad89c50-e58c-4d4c-aadf-1e80519a84d7"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[0],
            Receiver = _users[9],
            Type = NotificationType.Music,
            Message = "Special offer: 20% off on all albums this week!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c259ff30-a12e-47b7-b7c8-2312cae9810a"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[1],
            Receiver = _users[9],
            Type = NotificationType.Music,
            Message = "New album release: Check out the latest hits!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("91e35ae6-8b4e-4abd-87ce-a30ccc4b93ce"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[2],
            Receiver = _users[9],
            Type = NotificationType.Music,
            Message = "Exclusive: Early access to upcoming concerts!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("35f49831-5024-47ab-bec1-f7e19f6919bc"),
            SenderId = Guid.Parse("cb83d524-2016-4ce3-965b-223af9e7ba99"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[3],
            Receiver = _users[9],
            Type = NotificationType.Social,
            Message = "You have received a new message from a friend.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("251fd063-84db-4427-b09d-1a82822ced54"),
            SenderId = Guid.Parse("9c6a5f06-b1f0-4507-b2f8-955156a8bed6"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[4],
            Receiver = _users[9],
            Type = NotificationType.Social,
            Message = "Your friend has commented on your post.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dbc4a2a3-75ab-49a5-98ee-cc12c9f09fa4"),
            SenderId = Guid.Parse("ff3dd044-ebfb-4e1d-9050-b6dcfe684a1a"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[5],
            Receiver = _users[9],
            Type = NotificationType.Social,
            Message = "You have a new follower!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1952adee-ae48-4c4a-bdf5-1838f17a329a"),
            SenderId = Guid.Parse("f2a1edb1-332f-4c2f-af59-6b5508eafbec"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[6],
            Receiver = _users[9],
            Type = NotificationType.Account,
            Message = "Your email address has been verified.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("761ce11f-fd7e-4b2a-adc1-ea138a33f1eb"),
            SenderId = Guid.Parse("14a3d4c4-fb21-4153-9096-d96bda62ee59"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[7],
            Receiver = _users[9],
            Type = NotificationType.Account,
            Message = "Your password has been successfully changed.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("aa568bd5-24e0-42f8-aaaa-bb82da89eca8"),
            SenderId = Guid.Parse("978d41ee-1f38-4c84-8826-8e62bc5ba109"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[8],
            Receiver = _users[9],
            Type = NotificationType.Account,
            Message = "Your profile has been updated.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b0a154df-217e-407a-af1a-167daec30b0c"),
            SenderId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[0],
            Receiver = _users[9],
            Type = NotificationType.System,
            Message = "Critical security update applied. Please review changes.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7c88fd57-893c-4df4-839c-01cfa355dab3"),
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[1],
            Receiver = _users[9],
            Type = NotificationType.System,
            Message = "System maintenance scheduled for this weekend.",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3861af67-bf42-477d-869f-407d6c0bd717"),
            SenderId = Guid.Parse("36712aa4-77f0-4510-8425-cf53dad54840"),
            ReceiverId = Guid.Parse("da189940-d09e-4587-9665-8efdf10684dd"),
            Sender = _users[2],
            Receiver = _users[9],
            Type = NotificationType.System,
            Message = "New feature released: Check out the latest updates!",
            IsRead = false,
            CreatedAt = new DateTime(2024, 9, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 28, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 28, 12, 0, 0)
        }];

        public static readonly Notification _newNotification = new()
        {
            SenderId = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643"),
            ReceiverId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e"),
            Sender = _users[1],
            Receiver = _users[0],
            Type = NotificationType.System,
            Message = "The latest update includes several performance enhancements.",
            IsRead = false
        };

        public static readonly List<NotificationDto> _notificationDtos = _notifications.Select(ToDto).ToList();

        public static readonly NotificationDto _newNotificationDto = ToDto(_newNotification);

        public static readonly List<Notification> _notificationsPagination = _notifications.Where(notification => notification.DeletedAt == null).Take(10).ToList();

        public static readonly List<NotificationDto> _notificationDtosPagination = _notificationsPagination.Select(ToDto).ToList();

        public static readonly NotificationPaginationRequest _notificationPayload = new(SortCriteria: null, SearchCriteria: null, Type: null, Message: null, SenderName: null, IsRead: null, DateRange: null);

        private static NotificationDto ToDto(Notification notification) => new()
        {
            Id = notification.Id,
            SenderId = notification.SenderId,
            ReceiverId = notification.ReceiverId,
            Type = notification.Type,
            Message = notification.Message,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt,
            UpdatedAt = notification.UpdatedAt,
            DeletedAt = notification.DeletedAt
        };
    }
}