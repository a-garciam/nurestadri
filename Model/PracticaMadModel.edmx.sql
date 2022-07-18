
SET QUOTED_IDENTIFIER OFF;
GO
USE [PracticaMaD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ImageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_ImageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_ImageCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_ImageComment];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_CommentUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_CommentImage];
GO
IF OBJECT_ID(N'[dbo].[FK_LikeUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Like] DROP CONSTRAINT [FK_LikeUser];
GO
IF OBJECT_ID(N'[dbo].[FK_LikeImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Like] DROP CONSTRAINT [FK_LikeImage];
GO
IF OBJECT_ID(N'[dbo].[FK_FollowerUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follower] DROP CONSTRAINT [FK_FollowerUser];
GO
IF OBJECT_ID(N'[dbo].[FK_FollowedUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follower] DROP CONSTRAINT [FK_FollowedUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[Like]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Like];
GO
IF OBJECT_ID(N'[dbo].[Follower]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follower];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [usrId] bigint IDENTITY(1,1) NOT NULL,
    [loginName] nvarchar(30)  NOT NULL,
    [enPassword] nvarchar(100)  NOT NULL,
    [firstName] nvarchar(30)  NOT NULL,
    [lastName] nvarchar(30)  NOT NULL,
    [email] nvarchar(30)  NOT NULL,
    [country] nvarchar(30)  NOT NULL,
    [language] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [imageId] bigint IDENTITY(1,1) NOT NULL,
    [likes] int  NOT NULL,
    [creationDate] datetime2 NOT NULL,
    [title] nvarchar(50)  NOT NULL,
    [description] nvarchar(500)  NOT NULL,
    [aperture] nvarchar(10)  NOT NULL,
    [balance] nvarchar(10)  NOT NULL,
    [exposure] nvarchar(10)  NOT NULL,
    [imagePath] varchar(100)  NOT NULL,
    [usrId] bigint  NOT NULL,
    [categoryId] bigint  NOT NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [categoryId] bigint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Comment'
CREATE TABLE [dbo].[Comment] (
    [commentId] bigint IDENTITY(1,1) NOT NULL,
    [creationDate] datetime2 NOT NULL,
    [text] nvarchar(200)  NOT NULL,
    [imageId] bigint  NOT NULL,
    [usrId] bigint  NOT NULL
);
GO

-- Creating table 'Like'
CREATE TABLE [dbo].[Like] (
    [userLikesId] bigint  NOT NULL,
    [imageLikesId] bigint  NOT NULL
);
GO

-- Creating table 'Follower'
CREATE TABLE [dbo].[Follower] (
    [followedId] bigint  NOT NULL,
    [followerId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [usrId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([usrId] ASC);
GO

-- Creating primary key on [imageId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [PK_Image]
    PRIMARY KEY CLUSTERED ([imageId] ASC);
GO

-- Creating primary key on [categoryId] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [commentId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [PK_Comment]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [UserLikes_usrId], [ImageLikes_imageId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [PK_Like]
    PRIMARY KEY CLUSTERED ([userLikesId], [imageLikesId] ASC);
GO

-- Creating primary key on [Followed_usrId], [Follower_usrId] in table 'Follower'
ALTER TABLE [dbo].[Follower]
ADD CONSTRAINT [PK_Follower]
    PRIMARY KEY CLUSTERED ([followedId], [followerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [usrId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [FK_ImageUser]
    FOREIGN KEY ([usrId])
    REFERENCES [dbo].[User]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageUser'
CREATE INDEX [IX_FK_ImageUser]
ON [dbo].[Image]
    ([usrId]);
GO

-- Creating foreign key on [categoryId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [FK_ImageCategory]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Category]
        ([categoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageCategory'
CREATE INDEX [IX_FK_ImageCategory]
ON [dbo].[Image]
    ([categoryId]);
GO

-- Creating foreign key on [imageId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_CommentImage]
    FOREIGN KEY ([imageId])
    REFERENCES [dbo].[Image]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageComment'
CREATE INDEX [IX_FK_CommentImage]
ON [dbo].[Comment]
    ([imageId]);
GO

-- Creating foreign key on [usrId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_CommentUser]
    FOREIGN KEY ([usrId])
    REFERENCES [dbo].[User]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUser'
CREATE INDEX [IX_FK_CommentUser]
ON [dbo].[Comment]
    ([usrId]);
GO

-- Creating foreign key on [UserLikes_usrId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [FK_LikeUser]
    FOREIGN KEY ([userLikesId])
    REFERENCES [dbo].[User]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ImageLikes_imageId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [FK_LikeImage]
    FOREIGN KEY ([imageLikesId])
    REFERENCES [dbo].[Image]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Like_Image'
CREATE INDEX [IX_FK_LikeImage]
ON [dbo].[Like]
    ([imageLikesId]);
GO

-- Creating foreign key on [Followed_usrId] in table 'Follower'
ALTER TABLE [dbo].[Follower]
ADD CONSTRAINT [FK_FollowedUser]
    FOREIGN KEY ([followedId])
    REFERENCES [dbo].[User]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Follower_usrId] in table 'Follower'
ALTER TABLE [dbo].[Follower]
ADD CONSTRAINT [FK_FollowerUser]
    FOREIGN KEY ([followerId])
    REFERENCES [dbo].[User]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Follower_User1'
CREATE INDEX [IX_FK_FollowerUser]
ON [dbo].[Follower]
    ([followerId]);
GO


--------------- DATA INSERT -----------------
SET IDENTITY_INSERT [User] ON
INSERT INTO [User] ([usrId], [loginName], [enPassword], [firstName], [lastName], [email], [country], [language])
    VALUES (1, 'admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'Admin', 'lastName', 'admin@udc.es', 'es', 'ES');
INSERT INTO [User] ([usrId], [loginName], [enPassword], [firstName], [lastName], [email], [country], [language])
    VALUES (2, 'user1', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'User1', 'lastName', 'user@udc.es', 'es', 'ES');
INSERT INTO [User] ([usrId], [loginName], [enPassword], [firstName], [lastName], [email], [country], [language])
    VALUES (3, 'user2', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'User2', 'lastName', 'user@udc.es', 'es', 'ES');
INSERT INTO [User] ([usrId], [loginName], [enPassword], [firstName], [lastName], [email], [country], [language])
    VALUES (4, 'user3', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'User3', 'lastName', 'user@udc.es', 'es', 'ES');
SET IDENTITY_INSERT [User] OFF


INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (1, 2);
INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (1, 3);
INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (1, 4);
INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (2, 1);
INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (3, 1);
INSERT INTO [Follower] ([followedId], [followerId])
    VALUES (4, 1);

SET IDENTITY_INSERT [Category] ON
INSERT INTO [Category] ([categoryId],[name])
    VALUES (1, 'Colores');
SET IDENTITY_INSERT [Category] OFF

SET IDENTITY_INSERT [Category] ON
INSERT INTO [Category] ([categoryId],[name])
    VALUES (2, 'Naturaleza');
SET IDENTITY_INSERT [Category] OFF

SET IDENTITY_INSERT [Image] ON
INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (1, 0, 'Arbore', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\arbore.jpg', 1, 2, "06-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (2, 0, 'Azul', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\azul.jpg', 2, 1, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (3, 0, 'Buratos', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\buratos.jpg', 3, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (4, 0, 'Can', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\can.jpg', 1, 2, "07-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (5, 0, 'Flor', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\flor.jpg', 1, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (6, 0, 'Folla', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\folla.jpg', 1, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (7, 0, 'Pombas', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\pombas.jpg', 1, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (8, 0, 'Praia', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\praia.jpg', 1, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (9, 0, 'Tulipans', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\tulipans.jpg', 1, 2, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (10, 0, 'Verde', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\verde.jpg', 1, 1, "04-10-2021");

INSERT INTO [Image] ([imageId],[likes],[title],[description],[aperture],[balance],[exposure],[imagePath],[usrId],[categoryId],[creationDate])
    VALUES (11, 0, 'Vermello', 'jpg', 'f/8.9', '2000', '1/80', '\images\1\vermello.jpg', 1, 1, "04-10-2021");
SET IDENTITY_INSERT [Image] OFF

SET IDENTITY_INSERT [Comment] ON
INSERT INTO [Comment] ([commentId],[creationDate],[imageId],[usrId],[text])
    VALUES(1,"04-10-2021",1,1,"nuevo comentario <3");
INSERT INTO [Comment] ([commentId],[creationDate],[imageId],[usrId],[text])
    VALUES(2,"05-10-2021",1,1,"otro comentario <3");
INSERT INTO [Comment] ([commentId],[creationDate],[imageId],[usrId],[text])
    VALUES(3,"06-10-2021",1,1,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempo");
SET IDENTITY_INSERT [Comment] OFF

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------