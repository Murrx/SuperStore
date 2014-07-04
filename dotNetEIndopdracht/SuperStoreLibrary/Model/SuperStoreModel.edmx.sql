
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/04/2014 13:35:29
-- Generated from EDMX file: C:\Users\Robin Altena\Source\Repos\SuperStore\dotNetEIndopdracht\SuperStoreLibrary\Model\SuperStoreModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_PurchaseProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_PurchaseProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stock] DROP CONSTRAINT [FK_ProductStock];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerPurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_CustomerPurchase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Stock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stock];
GO
IF OBJECT_ID(N'[dbo].[Purchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purchases];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [price] float  NOT NULL
);
GO

-- Creating table 'Stock'
CREATE TABLE [dbo].[Stock] (
    [productId] int  NOT NULL,
    [amount] int  NOT NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [id] int IDENTITY(1,1) NOT NULL,
    [amount] int  NOT NULL,
    [date] datetime  NOT NULL,
    [product_id] int  NOT NULL,
    [customer_id] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [saldo] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [productId] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [PK_Stock]
    PRIMARY KEY CLUSTERED ([productId] ASC);
GO

-- Creating primary key on [id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [product_id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_PurchaseProduct]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[Products]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseProduct'
CREATE INDEX [IX_FK_PurchaseProduct]
ON [dbo].[Purchases]
    ([product_id]);
GO

-- Creating foreign key on [productId] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [FK_ProductStock]
    FOREIGN KEY ([productId])
    REFERENCES [dbo].[Products]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [customer_id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_CustomerPurchase]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[Customers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerPurchase'
CREATE INDEX [IX_FK_CustomerPurchase]
ON [dbo].[Purchases]
    ([customer_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------