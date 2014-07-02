
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/02/2014 14:57:31
-- Generated from EDMX file: C:\Users\Robin Altena\documents\visual studio 2013\Projects\dotNetEIndopdracht\SuperStoreLibrary\Model\SuperStoreModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [localDb];
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

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Stock'
CREATE TABLE [dbo].[Stock] (
    [ProductId] int  NOT NULL,
    [Amount] int  NOT NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Product_Id] int  NOT NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductId] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [PK_Stock]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_PurchaseProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseProduct'
CREATE INDEX [IX_FK_PurchaseProduct]
ON [dbo].[Purchases]
    ([Product_Id]);
GO

-- Creating foreign key on [ProductId] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [FK_ProductStock]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Customer_Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_CustomerPurchase]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerPurchase'
CREATE INDEX [IX_FK_CustomerPurchase]
ON [dbo].[Purchases]
    ([Customer_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------