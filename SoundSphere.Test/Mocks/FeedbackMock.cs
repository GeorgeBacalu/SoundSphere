using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Mocks
{
    public class FeedbackMock
    {
        private FeedbackMock() { }

        public static readonly List<Feedback> _feedbacks = [new()
        {
            Id = Guid.Parse("83061e8c-3403-441a-8be5-867ed1f4a86b"),
            User = _users[0],
            Type = FeedbackType.Issue,
            Message = "Notification sounds are not working on some devices.",
            CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3a02f5b9-00f1-4f75-8df8-89dbd1a7aeec"),
            User = _users[0],
            Type = FeedbackType.Issue,
            Message = "Buttons on the settings page do not respond on first tap.",
            CreatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7d44b1e2-0715-4b8b-8f22-15c123b9179b"),
            User = _users[0],
            Type = FeedbackType.Issue,
            Message = "Settings page crashes when toggling airplane mode.",
            CreatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9e592479-4c9f-444f-a1c3-f888f38ab008"),
            User = _users[0],
            Type = FeedbackType.Optimization,
            Message = "Improve page load time for the dashboard.",
            CreatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7b6706a4-6df8-4391-9936-d0fb330cdd47"),
            User = _users[0],
            Type = FeedbackType.Optimization,
            Message = "Reduce app startup time by optimizing splash screen.",
            CreatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e6e4625f-f172-4d9a-9a68-dbe02865f4c8"),
            User = _users[0],
            Type = FeedbackType.Optimization,
            Message = "Minimize app size by reducing unused assets.",
            CreatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("819ece95-0f7e-4ad6-aa95-4dcafff97345"),
            User = _users[0],
            Type = FeedbackType.Improvement,
            Message = "Add more analytics features for users.",
            CreatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e8d2d9e1-207d-4f64-8e8a-4e65e82762b4"),
            User = _users[0],
            Type = FeedbackType.Improvement,
            Message = "Add option to sync data across multiple devices.",
            CreatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2b76a5c9-f6b6-4b38-8c50-b198c7a19f31"),
            User = _users[0],
            Type = FeedbackType.Improvement,
            Message = "Allow users to reorder items in their dashboard.",
            CreatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2abf1f0c-6201-4eeb-96df-810a7fcdde47"),
            User = _users[1],
            Type = FeedbackType.Issue,
            Message = "Application crashes when uploading large files.",
            CreatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ce0fd348-c370-4874-8563-3c70332fe41d"),
            User = _users[1],
            Type = FeedbackType.Issue,
            Message = "Keyboard covers text input on some screens.",
            CreatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f3f4096a-2a44-4050-85b2-df2d1e6a6c42"),
            User = _users[1],
            Type = FeedbackType.Issue,
            Message = "Push notifications not working on some devices.",
            CreatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bf823996-d2ce-4616-a6b2-f7347f05c6aa"),
            User = _users[1],
            Type = FeedbackType.Optimization,
            Message = "Optimize search functionality for better results.",
            CreatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c8c6b44d-88e7-40d8-bf82-1f7d9fc8f64d"),
            User = _users[1],
            Type = FeedbackType.Optimization,
            Message = "Enhance image loading speed on slower connections.",
            CreatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("62764098-1b4d-404b-a394-7ff5761e1e42"),
            User = _users[1],
            Type = FeedbackType.Optimization,
            Message = "Improve network performance under low connectivity.",
            CreatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("777cfbdd-5330-44ab-848d-e2d61b46dcf7"),
            User = _users[1],
            Type = FeedbackType.Improvement,
            Message = "Allow users to customize their profile layout.",
            CreatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("235b24a2-58e2-4e36-a346-71df77ed8c7c"),
            User = _users[1],
            Type = FeedbackType.Improvement,
            Message = "Provide visual cues for successful actions.",
            CreatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b2a36ba2-67b3-46eb-95cb-2e9a76b0849c"),
            User = _users[1],
            Type = FeedbackType.Improvement,
            Message = "Add biometric authentication for quick access.",
            CreatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            DeletedAt = new DateTime(2024, 6, 18, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("fc1ae2d2-1234-4b5b-9a1e-39df1b636f38"),
            User = _users[2],
            Type = FeedbackType.Issue,
            Message = "The report generation feature fails to load.",
            CreatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3b1453c7-e683-4671-9f72-159b779b4fe3"),
            User = _users[2],
            Type = FeedbackType.Issue,
            Message = "Inconsistent behavior when switching tabs rapidly.",
            CreatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("945cbf8a-7043-4ef3-9b80-2b2fb7f7e521"),
            User = _users[2],
            Type = FeedbackType.Issue,
            Message = "Video playback stutters on slower devices.",
            CreatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("48c8b307-d4b5-4c58-a3ad-1a2affea3b36"),
            User = _users[2],
            Type = FeedbackType.Optimization,
            Message = "Compress images to improve page load time.",
            CreatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("634d6820-eedb-49f8-b201-0c21dcecae1c"),
            User = _users[2],
            Type = FeedbackType.Optimization,
            Message = "Reduce memory leaks during extended app usage.",
            CreatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e5e6fa2d-03ab-4d43-b2f3-8ecaa4c52fb2"),
            User = _users[2],
            Type = FeedbackType.Optimization,
            Message = "Optimize background task management.",
            CreatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("31f51cb2-a487-457b-9ad3-507e48a5f2b1"),
            User = _users[2],
            Type = FeedbackType.Improvement,
            Message = "Update the search functionality to include filters.",
            CreatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("99fc3420-8f9c-4a3b-bda3-5b8ec7b2080b"),
            User = _users[2],
            Type = FeedbackType.Improvement,
            Message = "Add gesture controls for easier navigation.",
            CreatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ea90e1ae-bb66-470e-a607-2a896de8d2c6"),
            User = _users[2],
            Type = FeedbackType.Improvement,
            Message = "Add a dark mode schedule option.",
            CreatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1e790ccd-7777-4bd7-a721-779f3b718b04"),
            User = _users[3],
            Type = FeedbackType.Issue,
            Message = "Multi-language support is inconsistent.",
            CreatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a6e8540f-41bc-4d8f-a847-1eaa0e68f7e0"),
            User = _users[3],
            Type = FeedbackType.Issue,
            Message = "Error 404 displayed on profile page occasionally.",
            CreatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d5c93570-870e-4e4c-83d8-1446d6e0a86f"),
            User = _users[3],
            Type = FeedbackType.Issue,
            Message = "Search bar does not yield results for partial terms.",
            CreatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7acaaed6-1bbb-4591-948c-ff5b6fbdad89"),
            User = _users[3],
            Type = FeedbackType.Optimization,
            Message = "Implement lazy loading for faster initial load.",
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0d64a9ff-018d-405d-a0e7-df1a29c3d60e"),
            User = _users[3],
            Type = FeedbackType.Optimization,
            Message = "Optimize animation rendering for smoother experience.",
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e26531d5-4e3c-4c3c-a835-e13fbb1b916a"),
            User = _users[3],
            Type = FeedbackType.Optimization,
            Message = "Improve battery efficiency when using GPS.",
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("871103f4-b5c9-4788-9f41-265c72c41b9f"),
            User = _users[3],
            Type = FeedbackType.Improvement,
            Message = "Add more detailed logs for debugging purposes.",
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fa60c824-0642-4c80-bfa6-1c9b4e42ecba"),
            User = _users[3],
            Type = FeedbackType.Improvement,
            Message = "Add custom notification sounds.",
            CreatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a74892a7-2ac3-42c5-b7ae-71a41b199760"),
            User = _users[3],
            Type = FeedbackType.Improvement,
            Message = "Expand support for accessibility features.",
            CreatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            DeletedAt = new DateTime(2024, 7, 6, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("9dd5b2dc-3c7b-42b6-b6f4-241541087b5b"),
            User = _users[4],
            Type = FeedbackType.Issue,
            Message = "App crashes when submitting a form.",
            CreatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2cc1f4b8-0e11-489f-8c92-dad0a62311a5"),
            User = _users[4],
            Type = FeedbackType.Issue,
            Message = "Graphical glitches on high-resolution displays.",
            CreatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e283b4de-8ac9-49b3-b124-d15b8a933238"),
            User = _users[4],
            Type = FeedbackType.Issue,
            Message = "Images are not loading on the main feed.",
            CreatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d901d2a3-3a96-496d-8f80-33d43a3b30da"),
            User = _users[4],
            Type = FeedbackType.Optimization,
            Message = "Reduce the memory usage on mobile devices.",
            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("110d9b91-823e-43c2-96ba-21ab7baf4600"),
            User = _users[4],
            Type = FeedbackType.Optimization,
            Message = "Speed up data synchronization between devices.",
            CreatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ce4edbc6-2c6a-4a8c-b376-d5b282616c14"),
            User = _users[4],
            Type = FeedbackType.Optimization,
            Message = "Optimize UI for better performance on older devices.",
            CreatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bc5b2a5c-3333-47b4-bdac-4b504f1f7473"),
            User = _users[4],
            Type = FeedbackType.Improvement,
            Message = "Provide customizable dashboard widgets.",
            CreatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("745495cc-6c02-49bb-b7a2-99e87048f0d5"),
            User = _users[4],
            Type = FeedbackType.Improvement,
            Message = "Enable offline mode for key functionalities.",
            CreatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fc8fca0b-63de-439e-aac1-144b58a562c1"),
            User = _users[4],
            Type = FeedbackType.Improvement,
            Message = "Provide a tutorial for new users.",
            CreatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("25d06c66-b0cd-44a6-8ede-5a6003d58bcb"),
            User = _users[5],
            Type = FeedbackType.Issue,
            Message = "Error message appears when uploading an image.",
            CreatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4cce8a04-c268-497e-bb7b-f6559be27ed4"),
            User = _users[5],
            Type = FeedbackType.Issue,
            Message = "Text formatting is lost when copying and pasting.",
            CreatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f9d9251d-81f6-4c8e-8c33-015cd36dd46e"),
            User = _users[5],
            Type = FeedbackType.Issue,
            Message = "Notifications are duplicated after the latest update.",
            CreatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("405b71fc-cd19-442b-b70b-c6fc1ef28eac"),
            User = _users[5],
            Type = FeedbackType.Optimization,
            Message = "Improve server response time by optimizing queries.",
            CreatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7f0aee91-6909-48f4-8fb1-8d9b2050659f"),
            User = _users[5],
            Type = FeedbackType.Optimization,
            Message = "Optimize database indexing for faster queries.",
            CreatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d498c0f4-4e8f-4c70-b0d0-b13fa57aaeb7"),
            User = _users[5],
            Type = FeedbackType.Optimization,
            Message = "Reduce data usage when streaming content.",
            CreatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c20369a1-8080-4f0b-9a3d-07af2fde560b"),
            User = _users[5],
            Type = FeedbackType.Improvement,
            Message = "Add dark mode feature for better user experience.",
            CreatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b4b960af-4a1d-45b2-8fa3-79802e0286b9"),
            User = _users[5],
            Type = FeedbackType.Improvement,
            Message = "Add quick access to frequently used features.",
            CreatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d5b1f4c4-187d-4741-80c6-4b232f3afba0"),
            User = _users[5],
            Type = FeedbackType.Improvement,
            Message = "Enable custom themes for personalization.",
            CreatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            DeletedAt = new DateTime(2024, 7, 24, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("5fef891b-22a5-41d1-9d4a-c78db84d6ad8"),
            User = _users[6],
            Type = FeedbackType.Issue,
            Message = "Live chat feature is not functioning.",
            CreatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dd69b0bb-f54e-4c06-823d-bf8ac2aee2fc"),
            User = _users[6],
            Type = FeedbackType.Issue,
            Message = "Profile pictures do not load on some devices.",
            CreatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4abf7e21-c820-496b-a1a5-b76d8be17bde"),
            User = _users[6],
            Type = FeedbackType.Issue,
            Message = "App crashes when multiple tabs are open.",
            CreatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7812b64a-4a6b-47e3-a31d-1810062a9138"),
            User = _users[6],
            Type = FeedbackType.Optimization,
            Message = "Optimize the database to improve load balancing.",
            CreatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5738cb45-25b9-4b08-8901-d29fa97cf9a5"),
            User = _users[6],
            Type = FeedbackType.Optimization,
            Message = "Decrease the frequency of background updates.",
            CreatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("10e6f07d-c94f-4314-b33d-3831d8b12a62"),
            User = _users[6],
            Type = FeedbackType.Optimization,
            Message = "Improve load times for large datasets.",
            CreatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3b70954d-fb63-4c5b-a6e5-7d6fd92844b0"),
            User = _users[6],
            Type = FeedbackType.Improvement,
            Message = "Enhance mobile app usability for tablet users.",
            CreatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("07a42f43-3c88-4d64-9348-3f845256d252"),
            User = _users[6],
            Type = FeedbackType.Improvement,
            Message = "Support multi-language input on all platforms.",
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8c2091be-2db7-44d2-b907-54235f33fcdf"),
            User = _users[6],
            Type = FeedbackType.Improvement,
            Message = "Integrate third-party app integrations.",
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6505b389-45c5-4a8d-9735-d56709e4cdd3"),
            User = _users[7],
            Type = FeedbackType.Issue,
            Message = "Search results do not update on back navigation.",
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cb649063-87b8-4824-b34d-104189e79b5c"),
            User = _users[7],
            Type = FeedbackType.Issue,
            Message = "App crashes when switching between light and dark mode rapidly.",
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("09e0a06d-bc43-4a1b-9b78-e82da01a90d6"),
            User = _users[7],
            Type = FeedbackType.Issue,
            Message = "Unable to download attachments from messages.",
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6aa6a324-7bfc-4d6d-bf0a-0c8f018c89b9"),
            User = _users[7],
            Type = FeedbackType.Optimization,
            Message = "Implement client-side caching to reduce server calls.",
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("25f77135-205a-4a5b-a1ae-5baba229d3bc"),
            User = _users[7],
            Type = FeedbackType.Optimization,
            Message = "Optimize server response time during peak usage hours.",
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c2f4875b-0e10-4b27-b7a0-1e42cdd9cf9b"),
            User = _users[7],
            Type = FeedbackType.Optimization,
            Message = "Optimize video buffering on slower networks.",
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6a1ff3d4-0988-43e0-802a-f6bb6e7a6f1c"),
            User = _users[7],
            Type = FeedbackType.Improvement,
            Message = "Add the ability to schedule recurring tasks.",
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("15e01f89-27ff-4085-ae8f-d9c7c75e10c8"),
            User = _users[7],
            Type = FeedbackType.Improvement,
            Message = "Introduce auto-save feature for draft messages.",
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b6846481-3ed1-4b5d-a77a-69f3b0e75b55"),
            User = _users[7],
            Type = FeedbackType.Improvement,
            Message = "Enable advanced search options for filtering results.",
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 11, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("c597c164-e5a4-45ec-a536-1ab321db7f17"),
            User = _users[8],
            Type = FeedbackType.Issue,
            Message = "The app sometimes logs out users unexpectedly.",
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8fcb4209-8cbf-4e31-b508-9a646f1fd0e1"),
            User = _users[8],
            Type = FeedbackType.Issue,
            Message = "Voice messages do not play when the screen is locked.",
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("43f4818c-8a5b-43d0-b587-44a3e61ed178"),
            User = _users[8],
            Type = FeedbackType.Issue,
            Message = "App freezes when trying to upload large files.",
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8d6e9d9b-3795-46d7-8bc2-3a6b0ed107b5"),
            User = _users[8],
            Type = FeedbackType.Optimization,
            Message = "Optimize the push notification delivery system.",
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("240fef6f-53e4-44a0-bfe3-88f2db702409"),
            User = _users[8],
            Type = FeedbackType.Optimization,
            Message = "Improve GPS accuracy for location-based features.",
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b0eae189-5a1c-49fc-82cb-d2c169d58ab8"),
            User = _users[8],
            Type = FeedbackType.Optimization,
            Message = "Enhance app speed when switching between tabs.",
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b6c678e4-448b-4907-bd3a-2c9d693ce785"),
            User = _users[8],
            Type = FeedbackType.Improvement,
            Message = "Allow users to set custom themes.",
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("187ccfcf-1916-4623-8e72-462f340a3e4c"),
            User = _users[8],
            Type = FeedbackType.Improvement,
            Message = "Add two-factor authentication for account security.",
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3b7f5e15-cbb7-43c2-bbcf-16334ec74b5a"),
            User = _users[8],
            Type = FeedbackType.Improvement,
            Message = "Add option to save app settings to cloud.",
            CreatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("62374d6a-46f2-4d68-a4f2-f5bc0af004d5"),
            User = _users[9],
            Type = FeedbackType.Issue,
            Message = "The feedback form does not submit when the network is slow.",
            CreatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e926d99d-2c7f-4b8c-b979-0d154a2052f1"),
            User = _users[9],
            Type = FeedbackType.Issue,
            Message = "Notifications are delayed by several minutes.",
            CreatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a6a8b9a4-f1df-43b4-9ebf-740a93be3c8d"),
            User = _users[9],
            Type = FeedbackType.Issue,
            Message = "Audio cuts out intermittently during calls.",
            CreatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d5e3fd9b-d0cf-4f91-8de8-8596e4bb4e14"),
            User = _users[9],
            Type = FeedbackType.Optimization,
            Message = "Reduce app size to improve download speed.",
            CreatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1bc304f1-8032-4f0e-aaba-d6e91e4ab2d5"),
            User = _users[9],
            Type = FeedbackType.Optimization,
            Message = "Optimize app for lower battery consumption.",
            CreatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f8c9d6ef-bac4-4017-9d77-3129f6fc5b35"),
            User = _users[9],
            Type = FeedbackType.Optimization,
            Message = "Decrease app memory usage when idle.",
            CreatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4d676f92-1d8e-4f77-a8cb-9ad5c8bdc10d"),
            User = _users[9],
            Type = FeedbackType.Improvement,
            Message = "Enable biometric login for added security.",
            CreatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fe551d90-1d0c-4e17-b68b-e06ff8ab09cb"),
            User = _users[9],
            Type = FeedbackType.Improvement,
            Message = "Add customizable shortcuts for quick actions.",
            CreatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("edf26e9b-7bb8-4c2c-a6c8-f0c0df1460e9"),
            User = _users[9],
            Type = FeedbackType.Improvement,
            Message = "Add support for more file formats in attachments.",
            CreatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 29, 12, 0, 0)
        }];

        public static readonly Feedback _newFeedback = new()
        {
            User = _users[0],
            Type = FeedbackType.Improvement,
            Message = "Increase file storage limits for users on all plans."
        };

        public static readonly List<FeedbackDto> _feedbackDtos = _feedbacks.Select(ToDto).ToList();

        public static readonly FeedbackDto _newFeedbackDto = ToDto(_newFeedback);

        public static readonly List<Feedback> _feedbacksPagination = _feedbacks.Where(feedback => feedback.DeletedAt == null).Take(10).ToList();

        public static readonly List<FeedbackDto> _feedbackDtosPagination = _feedbacksPagination.Select(ToDto).ToList();

        public static readonly FeedbackPaginationRequest _feedbackPayload = new(SortCriteria: null, SearchCriteria: null, Type: null, Message: null, UserName: null, DateRange: null);

        public static FeedbackDto ToDto(Feedback feedback) => new()
        {
            Id = feedback.Id,
            UserId = feedback.User.Id,
            Type = feedback.Type,
            Message = feedback.Message,
            CreatedAt = feedback.CreatedAt,
            UpdatedAt = feedback.UpdatedAt,
            DeletedAt = feedback.DeletedAt
        };
    }
}