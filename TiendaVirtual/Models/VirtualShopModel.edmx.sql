
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/16/2021 13:07:43
-- Generated from EDMX file: C:\Users\Hector Muñoz Mérida\source\repos\TiendaVirtual\TiendaVirtual\Models\VirtualShopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VirtualShop];
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

-- Creating table 'Pedidos'
CREATE TABLE [dbo].[Pedidos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Factura_Id] int  NOT NULL
);
GO

-- Creating table 'Facturas'
CREATE TABLE [dbo].[Facturas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_Cliente] int  NOT NULL,
    [Precio] float  NOT NULL
);
GO

-- Creating table 'LineasPedido'
CREATE TABLE [dbo].[LineasPedido] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Pedido_Id] int  NOT NULL,
    [Producto_Id] int  NOT NULL
);
GO

-- Creating table 'Productos'
CREATE TABLE [dbo].[Productos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Imagen] nvarchar(max)  NOT NULL,
    [Cantidad] int  NOT NULL,
    [Precio] float  NOT NULL,
    [Stock_Id] int  NULL,
    [Categoria_Id] int  NOT NULL
);
GO

-- Creating table 'Categorias'
CREATE TABLE [dbo].[Categorias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Stocks'
CREATE TABLE [dbo].[Stocks] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Pedidos'
ALTER TABLE [dbo].[Pedidos]
ADD CONSTRAINT [PK_Pedidos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [PK_Facturas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LineasPedido'
ALTER TABLE [dbo].[LineasPedido]
ADD CONSTRAINT [PK_LineasPedido]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [PK_Productos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categorias'
ALTER TABLE [dbo].[Categorias]
ADD CONSTRAINT [PK_Categorias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [PK_Stocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Factura_Id] in table 'Pedidos'
ALTER TABLE [dbo].[Pedidos]
ADD CONSTRAINT [FK_PedidoFactura]
    FOREIGN KEY ([Factura_Id])
    REFERENCES [dbo].[Facturas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoFactura'
CREATE INDEX [IX_FK_PedidoFactura]
ON [dbo].[Pedidos]
    ([Factura_Id]);
GO

-- Creating foreign key on [Pedido_Id] in table 'LineasPedido'
ALTER TABLE [dbo].[LineasPedido]
ADD CONSTRAINT [FK_PedidoLineaPedido]
    FOREIGN KEY ([Pedido_Id])
    REFERENCES [dbo].[Pedidos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoLineaPedido'
CREATE INDEX [IX_FK_PedidoLineaPedido]
ON [dbo].[LineasPedido]
    ([Pedido_Id]);
GO

-- Creating foreign key on [Producto_Id] in table 'LineasPedido'
ALTER TABLE [dbo].[LineasPedido]
ADD CONSTRAINT [FK_LineaPedidoProducto]
    FOREIGN KEY ([Producto_Id])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineaPedidoProducto'
CREATE INDEX [IX_FK_LineaPedidoProducto]
ON [dbo].[LineasPedido]
    ([Producto_Id]);
GO

-- Creating foreign key on [Stock_Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [FK_ProductoStock]
    FOREIGN KEY ([Stock_Id])
    REFERENCES [dbo].[Stocks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoStock'
CREATE INDEX [IX_FK_ProductoStock]
ON [dbo].[Productos]
    ([Stock_Id]);
GO

-- Creating foreign key on [Categoria_Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [FK_ProductoCategoria]
    FOREIGN KEY ([Categoria_Id])
    REFERENCES [dbo].[Categorias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoCategoria'
CREATE INDEX [IX_FK_ProductoCategoria]
ON [dbo].[Productos]
    ([Categoria_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------