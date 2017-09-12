
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2017 10:19:37
-- Generated from EDMX file: C:\Gitrepos\WebAPI\DataService\rebModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [rebtel];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsersSubscriptions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subscriptions] DROP CONSTRAINT [FK_UsersSubscriptions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Subscriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subscriptions];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Subscriptions'
CREATE TABLE [dbo].[Subscriptions] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [Price] decimal(18,2)  NOT NULL,
    [PriceIncVatAmount] decimal(18,2)  NOT NULL,
    [CallMinutes] int  NOT NULL,
    [UrlFriendly] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [PK_Subscriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [FK_UsersSubscriptions]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersSubscriptions'
CREATE INDEX [IX_FK_UsersSubscriptions]
ON [dbo].[Subscriptions]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------