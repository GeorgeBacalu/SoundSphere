namespace SoundSphere.Database
{
    public class Constants
    {
        private Constants() { }

        public static readonly string AlbumNotFound = "Album with id {0} not found";
        public static readonly string ArtistNotFound = "Artist with id {0} not found";
        public static readonly string FeedbackNotFound = "Feedback with id {0} not found";
        public static readonly string NotificationNotFound = "Notification with id {0} not found";
        public static readonly string PlaylistNotFound = "Playlist with id {0} not found";
        public static readonly string SongNotFound = "Song with id {0} not found";
        public static readonly string UserNotFound = "User with id {0} not found";

        public static readonly string UserAlreadyExists = "User with this name, email, or phone already exists";
        public static readonly string UserEmailNotFound = "User with email {0} not found";
        public static readonly string InvalidPassword = "Invalid password";

        public static readonly Guid ValidAlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb");
        public static readonly Guid ValidArtistId = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88");
        public static readonly Guid ValidFeedbackId = Guid.Parse("83061e8c-3403-441a-8be5-867ed1f4a86b");
        public static readonly Guid ValidNotificationId = Guid.Parse("7e221fa3-2c22-4573-bf21-cd1d6696b576");
        public static readonly Guid ValidPlaylistId = Guid.Parse("239d050b-b59c-47e0-9e1a-ab5faf6f903e");
        public static readonly Guid ValidSongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8");
        public static readonly Guid ValidUserId = Guid.Parse("0a9e546f-38b4-4dbf-a482-24a82169890e");
        public static readonly Guid ValidUserId2 = Guid.Parse("7eb88892-549b-4cae-90be-c52088354643");
        public static readonly Guid InvalidId = Guid.Empty;

        public static readonly string ApiAlbum = "/api/Album";
        public static readonly string ApiArtist = "/api/Artist";
        public static readonly string ApiFeedback = "/api/Feedback";
        public static readonly string ApiNotification = "/api/Notification";
        public static readonly string ApiPlaylist = "/api/Playlist";
        public static readonly string ApiSong = "/api/Song";
        public static readonly string ApiUser = "/api/User";
    }
}