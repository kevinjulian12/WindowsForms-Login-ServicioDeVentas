IF NOT EXISTS(SELECT OBJECT_ID='BETA')
CREATE DATABASE BETA
GO
USE BETA 
GO
IF NOT EXISTS(SELECT OBJECT_ID='Users')
create table Users(
UserID int identity(1,1) primary key,
LoginName nvarchar (100) unique not null,
Password nvarchar (100) not null,
FirstName nvarchar(100) not null,
LastName nvarchar(100) not null,
Position nvarchar (100) null,
Email nvarchar(150)not null,
DateoOfBirth date NULL,
GENDER varchar(1) NULL,
)
go
--insert into Users values ('admin','admin','Jackson','Collins','Administrator','Support@SystemAll.biz')
--insert into Users values ('Ben','abc123456','Benjamin','Thompson','Receptionist','BenThompson@MyCompany.com')                                                         
--insert into Users values ('Kevin','ABC123','Kevin','Gaitano','Accounting','kevinjuliangaitano@gmail.com' )
--go

IF NOT EXISTS(SELECT OBJECT_ID='Clientes')
create table Clientes
(
ID int identity (1,1) primary key,
Nombre varchar (100),
Apellido varchar (100),
Direccion varchar (100),
Telefono float,
Localidad varchar (100),
)
go

create or alter procedure MostrarClientes
as
select *from Clientes
go
IF NOT EXISTS(SELECT OBJECT_ID='Productos')
create table Productos 
(
Id int identity (1,1) primary key,
Producto nvarchar (100),
Descripcion nvarchar (100),
Marca nvarchar (100),
Costo float,
Precio float,
Stock int
)
go

--insert into Productos values ('Gaseosa','3 litros','marcacola',0,7.5,24),('Chocolate','Tableta 100 gramos','iberica',0,12.5,36)
---PROCEDIMIENTOS ALMACENADOS 
--------------------------MOSTRAR
go

create or alter procedure MostrarProductos
as
select *from Productos
go
--------------------------INSERTAR 
create or alter procedure InsetarProductos
@nombre nvarchar (100),
@descrip nvarchar (100),
@marca nvarchar (100),
@costo float,
@precio float,
@stock int
as
insert into Productos values (@nombre,@descrip,@marca,@costo,@precio,@stock)
go
------------------------ELIMINAR
create or alter procedure EliminarProducto
@idpro int
as
delete from Productos where Id=@idpro
go
------------------EDITAR
create or alter procedure EditarProductos
@nombre nvarchar (100),
@descrip nvarchar (100),
@marca nvarchar (100),
@costo float,
@precio float,
@stock int,
@id int
as
update Productos set Producto=@nombre, Descripcion=@descrip, Marca=@marca,Costo=@costo, Precio=@precio, Stock=@stock where Id=@id
go
IF NOT EXISTS(SELECT OBJECT_ID='Provedor')
create table Proveedor 
(
Id int identity (1,1) primary key,
Nombre nvarchar (100),
Razon_Social nvarchar (100),
Direccion nvarchar (100),
Telefono int,
Referencia nvarchar (100),
Email nvarchar (100)
)
go
create or alter procedure MostrarProveedor
as
select *from Proveedor
go
IF NOT EXISTS(SELECT OBJECT_ID='Ventas')
CREATE TABLE [dbo].[ventas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCliente] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[Total] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
IF NOT EXISTS(SELECT OBJECT_ID='ventasitems')
CREATE TABLE [dbo].[ventasitems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDVenta] [int] NOT NULL,
	[IDProducto] [int] NOT NULL,
	[PrecioUnitario] [float] NULL,
	[Cantidad] [float] NULL,
	[PrecioTotal] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF NOT EXISTS(SELECT OBJECT_ID='Compras')
CREATE TABLE [dbo].[Compras](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDProveedor] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[Total] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
IF NOT EXISTS(SELECT OBJECT_ID='comprasitems')
CREATE TABLE [dbo].[comprasitems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCompra] [int] NOT NULL,
	[IDProducto] [int] NOT NULL,
	[PrecioCompra] [float] NULL,
	[PrecioVenta] [float] NULL,
	[Cantidad] [float] NULL,
	[SubTotal] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
create or alter procedure RestarStock
@stock int,
@id int
as
Update Productos set Stock=Stock-@stock where Id=@id
go
--select Nombre,Marca,Descripcion,PrecioUnitario,Cantidad,SubTotal from ventasitems join productos on IDProducto=productos.ID where IDVenta=1
--
--
--
--select *from ventasitems
--join productos on IDProducto=productos.ID
--join ventas on IDVenta = ventas.ID
--join Clientes on IDCliente = Clientes.ID 
-- where Producto='gaseosa' 
--
-- select ve.ID,COUNT(*) as CantidadID,avg(ventasitems.SubTotal) as promedio from ventas ve
-- join ventasitems on ve.ID=ventasitems.IDVenta
-- join Clientes on ve.IDCliente=Clientes.ID
-- where ventasitems.IDProducto=1 AND ventasitems.IDProducto=2
-- group by ve.ID
-- 
-- 
-- select* from ventas ve
-- join ventasitems on ve.ID=ventasitems.IDVenta
--

 Select* from Productos