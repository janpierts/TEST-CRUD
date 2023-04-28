USE [master]
GO 
CREATE DATABASE [employees]
GO
USE [employees]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TABLE [dbo].[Usuarios](
	[Usuarios_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[pass] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Usuarios_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TABLE [dbo].[empleado](
	[empleado_id] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](250) NOT NULL,
	[apellidos] [varchar](250) NOT NULL,
	[f_nacimiento] [date] NOT NULL,
    [f_ingreso] [date] NOT NULL,
    [afp] [varchar](500) NOT NULL,
	[sueldo] [decimal] NOT NULL,
	[FK_Usuarios] [int] FOREIGN KEY REFERENCES Usuarios(Usuarios_id),
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[empleado_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TABLE [dbo].[Roles](
	[Roles_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[description] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Roles_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TABLE [dbo].[empleado_has_Roles](
	[empleado_id] [int],
	[Roles_id] [int],
	CONSTRAINT empleado_has_Roles_id PRIMARY KEY ([empleado_id], [Roles_id]),
	CONSTRAINT FK_empleado FOREIGN KEY ([empleado_id]) REFERENCES empleado ([empleado_id]),
	CONSTRAINT FK_Roles FOREIGN KEY ([Roles_id]) REFERENCES Roles (Roles_id));
GO

create procedure sp_ListarPersonal
as
begin
	select empleado.empleado_id as PID,empleado.nombres as NP,empleado.apellidos as AP,empleado.f_nacimiento as FN,empleado.f_ingreso as FI,empleado.afp as AFP,empleado.sueldo as sueldo,Roles.name as RN from empleado
	join empleado_has_Roles on empleado.empleado_id=empleado_has_Roles.empleado_id
	join Roles on empleado_has_Roles.Roles_id=Roles.Roles_id
	where Roles.name != 'Cliente' and Roles.name != 'Invitado'
end
go

create procedure sp_ListarRoles
as
begin
	select * from Roles
end
go

create procedure sp_GuardarPersonal(
@NP varchar(250),
@AP varchar(250),
@F_N date,
@F_I date,
@afp varchar(500),
@sueldo decimal,
@R_id int
)
as
begin
declare @P_id int
	begin
		insert into empleado(nombres,apellidos,f_nacimiento,f_ingreso,afp,sueldo) 
		values(@NP,@AP,@F_N,@F_I,@afp,@sueldo)
	end
	begin
		select @P_id = empleado_id from empleado
	end
	begin
		insert into empleado_has_Roles(empleado_id,Roles_id)
		values(@P_id,@R_id)
	end
end
go
create procedure sp_ObtenerIdPersonal(
@P_ID int
)
as
begin
	select empleado.empleado_id as PID,empleado.nombres as NP,empleado.apellidos as AP,empleado.f_nacimiento as FN,empleado.f_ingreso as FI,empleado.afp as AFP,empleado.sueldo as sueldo,Roles.name as RN from empleado
	join empleado_has_Roles on empleado.empleado_id=empleado_has_Roles.empleado_id
	join Roles on empleado_has_Roles.Roles_id=Roles.Roles_id
	where empleado.empleado_id = @P_ID
end
go

create procedure sp_EditarPersonal(
@NP varchar(250),
@AP varchar(250),
@F_N date,
@F_I date,
@afp varchar(500),
@sueldo decimal,
@R_id int
)
as
begin
	update empleado set nombres = @NP,apellidos = @AP,f_nacimiento = @F_N,f_ingreso = @F_I,afp = @afp,sueldo = @sueldo
	where empleado_id = @P_id
end
begin
	update empleado_has_Roles set Roles_id = @R_id where empleado_id = @P_Id
end
go

create procedure sp_EliminarPersonal(@P_Id int)
as
begin
	delete from empleado_has_Roles where empleado_id=@P_Id
end
begin
	delete from empleado where empleado_id=@P_Id
end
go



insert into Roles(name,description) 
values ('Cliente','Puede generar carrito de compras')
go
insert into Roles(name,description) 
values ('Invitado','Puede generar carrito de compras')
go
insert into Roles(name,description) 
values ('Gerente','Puede gestionar el negocio usando el sistema')
go
insert into Roles(name,description) 
values ('Recepcionista','Puede gestionar el pedido del cliente con el sistema')
go
insert into Roles(name,description) 
values ('Despachador','Puede validar el pedido del cliente con el sistema')
go
insert into Roles(name,description) 
values ('Delivery','Puede validar y hacer entrega del pedido del cliente usando el sistema')
go
