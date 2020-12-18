create table Clientes
(
ID int identity (1,1) primary key,
Nombre varchar (100),
Apellido varchar (100),
Direccion varchar (100),
Telefono float,
Localidad varchar (100),
)
create proc MostrarClientes
as
select *from Clientes
go
