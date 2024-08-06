--CREATE TABLE [__EFMigrationsHistory] (
--    [MigrationId] nvarchar(150) NOT NULL,
--    [ProductVersion] nvarchar(32) NOT NULL,
--    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId]));

CREATE TABLE [dbo].[Albums](
    [Id] [uniqueidentifier] NOT NULL,
    [Title] [nvarchar](max) NOT NULL,
    [ImageUrl] [nvarchar](max) NOT NULL,
    [ReleaseDate] [date] NOT NULL,
    [CreatedAt] [datetime2](7) NOT NULL,
    [UpdatedAt] [datetime2](7) NOT NULL,
    [DeletedAt] [datetime2](7) NULL,
    CONSTRAINT [PK_Albums] PRIMARY KEY ([Id]));

CREATE TABLE [AlbumPairs] (
    [AlbumId] uniqueidentifier NOT NULL,
    [SimilarAlbumId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AlbumPairs] PRIMARY KEY ([AlbumId], [SimilarAlbumId]),
    CONSTRAINT [FK_AlbumPairs_Albums_AlbumId] FOREIGN KEY ([AlbumId]) REFERENCES [Albums] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AlbumPairs_Albums_SimilarAlbumId] FOREIGN KEY ([SimilarAlbumId]) REFERENCES [Albums] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [Artists] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Artists] PRIMARY KEY ([Id]));

CREATE TABLE [ArtistPairs] (
    [ArtistId] uniqueidentifier NOT NULL,
    [SimilarArtistId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ArtistPairs] PRIMARY KEY ([ArtistId], [SimilarArtistId]),
    CONSTRAINT [FK_ArtistPairs_Artists_ArtistId] FOREIGN KEY ([ArtistId]) REFERENCES [Artists] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ArtistPairs_Artists_SimilarArtistId] FOREIGN KEY ([SimilarArtistId]) REFERENCES [Artists] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [Songs] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [Genre] nvarchar(max) NOT NULL,
    [ReleaseDate] date NOT NULL,
    [DurationSeconds] int NOT NULL,
    [AlbumId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Songs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Songs_Albums_AlbumId] FOREIGN KEY ([AlbumId]) REFERENCES [Albums] ([Id]) ON DELETE CASCADE);

CREATE TABLE [SongPairs] (
    [SongId] uniqueidentifier NOT NULL,
    [SimilarSongId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SongPairs] PRIMARY KEY ([SongId], [SimilarSongId]),
    CONSTRAINT [FK_SongPairs_Songs_SimilarSongId] FOREIGN KEY ([SimilarSongId]) REFERENCES [Songs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SongPairs_Songs_SongId] FOREIGN KEY ([SongId]) REFERENCES [Songs] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [ArtistSong] (
    [ArtistsId] uniqueidentifier NOT NULL,
    [SongsId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ArtistSong] PRIMARY KEY ([ArtistsId], [SongsId]),
    CONSTRAINT [FK_ArtistSong_Artists_ArtistsId] FOREIGN KEY ([ArtistsId]) REFERENCES [Artists] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArtistSong_Songs_SongsId] FOREIGN KEY ([SongsId]) REFERENCES [Songs] ([Id]) ON DELETE CASCADE);

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Phone] nvarchar(450) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Birthday] date NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]));

CREATE TABLE [UsersArtists] (
    [UserId] uniqueidentifier NOT NULL,
    [ArtistId] uniqueidentifier NOT NULL,
    [IsFollowing] bit NOT NULL,
    CONSTRAINT [PK_UsersArtists] PRIMARY KEY ([UserId], [ArtistId]),
    CONSTRAINT [FK_UsersArtists_Artists_ArtistId] FOREIGN KEY ([ArtistId]) REFERENCES [Artists] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UsersArtists_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [UsersSongs] (
    [UserId] uniqueidentifier NOT NULL,
    [SongId] uniqueidentifier NOT NULL,
    [PlayCount] int NOT NULL,
    CONSTRAINT [PK_UsersSongs] PRIMARY KEY ([UserId], [SongId]),
    CONSTRAINT [FK_UsersSongs_Songs_SongId] FOREIGN KEY ([SongId]) REFERENCES [Songs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UsersSongs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [Feedbacks] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Feedbacks_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE);

CREATE TABLE [Notifications] (
    [Id] uniqueidentifier NOT NULL,
    [SenderId] uniqueidentifier NOT NULL,
    [ReceiverId] uniqueidentifier NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [IsRead] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_Users_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Notifications_Users_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION);

CREATE TABLE [Playlists] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Playlists] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Playlists_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE);

CREATE TABLE [PlaylistSong] (
    [PlaylistsId] uniqueidentifier NOT NULL,
    [SongsId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PlaylistSong] PRIMARY KEY ([PlaylistsId], [SongsId]),
    CONSTRAINT [FK_PlaylistSong_Playlists_PlaylistsId] FOREIGN KEY ([PlaylistsId]) REFERENCES [Playlists] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlaylistSong_Songs_SongsId] FOREIGN KEY ([SongsId]) REFERENCES [Songs] ([Id]) ON DELETE CASCADE);

CREATE INDEX [IX_AlbumPairs_SimilarAlbumId] ON [AlbumPairs] ([SimilarAlbumId]);
CREATE INDEX [IX_ArtistPairs_SimilarArtistId] ON [ArtistPairs] ([SimilarArtistId]);
CREATE INDEX [IX_SongPairs_SimilarSongId] ON [SongPairs] ([SimilarSongId]);
CREATE INDEX [IX_ArtistSong_SongsId] ON [ArtistSong] ([SongsId]);
CREATE INDEX [IX_Feedbacks_UserId] ON [Feedbacks] ([UserId]);
CREATE INDEX [IX_Notifications_SenderId] ON [Notifications] ([SenderId]);
CREATE INDEX [IX_Notifications_ReceiverId] ON [Notifications] ([ReceiverId]);
CREATE INDEX [IX_Playlists_UserId] ON [Playlists] ([UserId]);
CREATE INDEX [IX_PlaylistSong_SongsId] ON [PlaylistSong] ([SongsId]);
CREATE INDEX [IX_Songs_AlbumId] ON [Songs] ([AlbumId]);
CREATE INDEX [IX_UsersArtists_ArtistId] ON [UsersArtists] ([ArtistId]);
CREATE INDEX [IX_UsersSongs_SongId] ON [UsersSongs] ([SongId]);
CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
CREATE UNIQUE INDEX [IX_Users_Name] ON [Users] ([Name]);
CREATE UNIQUE INDEX [IX_Users_Phone] ON [Users] ([Phone]);