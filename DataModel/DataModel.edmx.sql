
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/08/2015 03:16:37
-- Generated from EDMX file: D:\Google Drive\Visual Studio\comp1690\StockManagement\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [comp1690];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_CustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stocks] DROP CONSTRAINT [FK_ProductStock];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_ProductOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStockTransferItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StocksTransferItems] DROP CONSTRAINT [FK_ProductStockTransferItem];
GO
IF OBJECT_ID(N'[dbo].[FK_WarehouseStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stocks] DROP CONSTRAINT [FK_WarehouseStock];
GO
IF OBJECT_ID(N'[dbo].[FK_StockTransferStockTransferItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StocksTransferItems] DROP CONSTRAINT [FK_StockTransferStockTransferItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StockTransferWarehouse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StocksTransfers] DROP CONSTRAINT [FK_StockTransferWarehouse];
GO
IF OBJECT_ID(N'[dbo].[FK_StockTransferDestination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StocksTransfers] DROP CONSTRAINT [FK_StockTransferDestination];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Stocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stocks];
GO
IF OBJECT_ID(N'[dbo].[Warehouses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Warehouses];
GO
IF OBJECT_ID(N'[dbo].[StocksTransferItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StocksTransferItems];
GO
IF OBJECT_ID(N'[dbo].[OrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderItems];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[StocksTransfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StocksTransfers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Stocks'
CREATE TABLE [dbo].[Stocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Product_Id] int  NOT NULL,
    [Warehouse_Id] int  NOT NULL
);
GO

-- Creating table 'Warehouses'
CREATE TABLE [dbo].[Warehouses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StocksTransferItems'
CREATE TABLE [dbo].[StocksTransferItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Product_Id] int  NOT NULL,
    [StockTransfer_Id] int  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Order_Id] int  NOT NULL,
    [Product_Id] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Gender] bit  NOT NULL,
    [DOB] datetime  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [AddressLine1] nvarchar(max)  NOT NULL,
    [AddressLine2] nvarchar(max)  NOT NULL,
    [Postcode] nvarchar(max)  NOT NULL,
    [Town] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(10,4)  NOT NULL,
    [Weight] decimal(7,2)  NOT NULL,
    [BoxItemsAmount] smallint  NOT NULL,
    [StockTotal] int  NOT NULL
);
GO

-- Creating table 'StocksTransfers'
CREATE TABLE [dbo].[StocksTransfers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Distance] decimal(8,2)  NOT NULL,
    [Departure_Id] int  NOT NULL,
    [Destination_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [PK_Stocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Warehouses'
ALTER TABLE [dbo].[Warehouses]
ADD CONSTRAINT [PK_Warehouses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StocksTransferItems'
ALTER TABLE [dbo].[StocksTransferItems]
ADD CONSTRAINT [PK_StocksTransferItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StocksTransfers'
ALTER TABLE [dbo].[StocksTransfers]
ADD CONSTRAINT [PK_StocksTransfers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_CustomerOrder]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrder'
CREATE INDEX [IX_FK_CustomerOrder]
ON [dbo].[Orders]
    ([Customer_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderOrderItem]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'
CREATE INDEX [IX_FK_OrderOrderItem]
ON [dbo].[OrderItems]
    ([Order_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [FK_ProductStock]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStock'
CREATE INDEX [IX_FK_ProductStock]
ON [dbo].[Stocks]
    ([Product_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_ProductOrderItem]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrderItem'
CREATE INDEX [IX_FK_ProductOrderItem]
ON [dbo].[OrderItems]
    ([Product_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'StocksTransferItems'
ALTER TABLE [dbo].[StocksTransferItems]
ADD CONSTRAINT [FK_ProductStockTransferItem]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStockTransferItem'
CREATE INDEX [IX_FK_ProductStockTransferItem]
ON [dbo].[StocksTransferItems]
    ([Product_Id]);
GO

-- Creating foreign key on [Warehouse_Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [FK_WarehouseStock]
    FOREIGN KEY ([Warehouse_Id])
    REFERENCES [dbo].[Warehouses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarehouseStock'
CREATE INDEX [IX_FK_WarehouseStock]
ON [dbo].[Stocks]
    ([Warehouse_Id]);
GO

-- Creating foreign key on [StockTransfer_Id] in table 'StocksTransferItems'
ALTER TABLE [dbo].[StocksTransferItems]
ADD CONSTRAINT [FK_StockTransferStockTransferItem]
    FOREIGN KEY ([StockTransfer_Id])
    REFERENCES [dbo].[StocksTransfers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockTransferStockTransferItem'
CREATE INDEX [IX_FK_StockTransferStockTransferItem]
ON [dbo].[StocksTransferItems]
    ([StockTransfer_Id]);
GO

-- Creating foreign key on [Departure_Id] in table 'StocksTransfers'
ALTER TABLE [dbo].[StocksTransfers]
ADD CONSTRAINT [FK_StockTransferWarehouse]
    FOREIGN KEY ([Departure_Id])
    REFERENCES [dbo].[Warehouses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockTransferWarehouse'
CREATE INDEX [IX_FK_StockTransferWarehouse]
ON [dbo].[StocksTransfers]
    ([Departure_Id]);
GO

-- Creating foreign key on [Destination_Id] in table 'StocksTransfers'
ALTER TABLE [dbo].[StocksTransfers]
ADD CONSTRAINT [FK_StockTransferDestination]
    FOREIGN KEY ([Destination_Id])
    REFERENCES [dbo].[Warehouses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockTransferDestination'
CREATE INDEX [IX_FK_StockTransferDestination]
ON [dbo].[StocksTransfers]
    ([Destination_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------