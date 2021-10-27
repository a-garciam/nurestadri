
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/20/2021 17:40:35
-- Generated from EDMX file: C:\Users\Nuria\source\mad\nurestadri\PracticaMaD\Model\PracticaMadModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PracticaMaD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [usrId] int IDENTITY(1,1) NOT NULL,
    [loginName] nvarchar(max)  NOT NULL,
    [enPassword] nvarchar(max)  NOT NULL,
    [firstName] nvarchar(max)  NOT NULL,
    [lastName] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [country] nvarchar(max)  NOT NULL,
    [Follower_usrId] int  NOT NULL
);
GO

-- Creating table 'ImageSet'
CREATE TABLE [dbo].[ImageSet] (
    [imageId] int IDENTITY(1,1) NOT NULL,
    [likes] nvarchar(max)  NOT NULL,
    [usrId] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [aperture] nvarchar(max)  NOT NULL,
    [balance] nvarchar(max)  NOT NULL,
    [exposure] nvarchar(max)  NOT NULL,
    [categoryId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [categoryId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [commentId] int IDENTITY(1,1) NOT NULL,
    [userId] nvarchar(max)  NOT NULL,
    [text] nvarchar(max)  NOT NULL,
    [imageId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LikeSet'
CREATE TABLE [dbo].[LikeSet] (
    [likeId] int IDENTITY(1,1) NOT NULL,
    [userId] nvarchar(max)  NOT NULL,
    [imageId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [usrId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([usrId] ASC);
GO

-- Creating primary key on [imageId] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [PK_ImageSet]
    PRIMARY KEY CLUSTERED ([imageId] ASC);
GO

-- Creating primary key on [categoryId] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [commentId] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [likeId] in table 'LikeSet'
ALTER TABLE [dbo].[LikeSet]
ADD CONSTRAINT [PK_LikeSet]
    PRIMARY KEY CLUSTERED ([likeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_usrId] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_UserPost]
    FOREIGN KEY ([usrId])
    REFERENCES [dbo].[UserSet]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPost'
CREATE INDEX [IX_FK_UserPost]
ON [dbo].[ImageSet]
    ([usrId]);
GO

-- Creating foreign key on [Category_categoryId] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_ImageCategory]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[CategorySet]
        ([categoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageCategory'
CREATE INDEX [IX_FK_ImageCategory]
ON [dbo].[ImageSet]
    ([categoryId]);
GO

-- Creating foreign key on [Image_imageId] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_ImageComment]
    FOREIGN KEY ([imageId])
    REFERENCES [dbo].[ImageSet]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageComment'
CREATE INDEX [IX_FK_ImageComment]
ON [dbo].[CommentSet]
    ([imageId]);
GO

-- Creating foreign key on [User_usrId] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([usrId])
    REFERENCES [dbo].[UserSet]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[CommentSet]
    ([usrId]);
GO

-- Creating foreign key on [Image_imageId] in table 'LikeSet'
ALTER TABLE [dbo].[LikeSet]
ADD CONSTRAINT [FK_ImageLike]
    FOREIGN KEY ([imageId])
    REFERENCES [dbo].[ImageSet]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageLike'
CREATE INDEX [IX_FK_ImageLike]
ON [dbo].[LikeSet]
    ([imageId]);
GO

-- Creating foreign key on [User_usrId] in table 'LikeSet'
ALTER TABLE [dbo].[LikeSet]
ADD CONSTRAINT [FK_UserLike]
    FOREIGN KEY ([usrId])
    REFERENCES [dbo].[UserSet]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLike'
CREATE INDEX [IX_FK_UserLike]
ON [dbo].[LikeSet]
    ([usrId]);
GO

-- Creating foreign key on [Follower_usrId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_Follower]
    FOREIGN KEY ([Follower_usrId])
    REFERENCES [dbo].[UserSet]
        ([usrId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Follower'
CREATE INDEX [IX_FK_Follower]
ON [dbo].[UserSet]
    ([Follower_usrId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------