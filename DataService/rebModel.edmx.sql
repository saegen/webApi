
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/10/2018 11:53:26
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

IF OBJECT_ID(N'[dbo].[FK_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user_subscription] DROP CONSTRAINT [FK_user];
GO
IF OBJECT_ID(N'[dbo].[FK_user_subscription_Subscriptions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user_subscription] DROP CONSTRAINT [FK_user_subscription_Subscriptions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Subscriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subscriptions];
GO
IF OBJECT_ID(N'[dbo].[user_subscription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[user_subscription];
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

-- Creating table 'user_subscription'
CREATE TABLE [dbo].[user_subscription] (
    [Users_Id] int  NOT NULL,
    [Subscriptions_Id] uniqueidentifier  NOT NULL
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

-- Creating primary key on [Users_Id], [Subscriptions_Id] in table 'user_subscription'
ALTER TABLE [dbo].[user_subscription]
ADD CONSTRAINT [PK_user_subscription]
    PRIMARY KEY CLUSTERED ([Users_Id], [Subscriptions_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Users_Id] in table 'user_subscription'
ALTER TABLE [dbo].[user_subscription]
ADD CONSTRAINT [FK_user_subscription_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Subscriptions_Id] in table 'user_subscription'
ALTER TABLE [dbo].[user_subscription]
ADD CONSTRAINT [FK_user_subscription_Subscription]
    FOREIGN KEY ([Subscriptions_Id])
    REFERENCES [dbo].[Subscriptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_subscription_Subscription'
CREATE INDEX [IX_FK_user_subscription_Subscription]
ON [dbo].[user_subscription]
    ([Subscriptions_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------