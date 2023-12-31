USE [Examen]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[examen_Areas]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[examen_Areas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_examen_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[examen_Empleados]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[examen_Empleados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
	[ApellidoPaterno] [nvarchar](30) NOT NULL,
	[ApellidoMaterno] [nvarchar](30) NOT NULL,
	[Telefono] [nvarchar](13) NOT NULL,
	[Sexo] [nvarchar](1) NOT NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
	[AreaId] [int] NOT NULL,
 CONSTRAINT [PK_examen_Empleados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230828184955_tables', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230828191022_SPArea', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230829022854_SPId', N'6.0.0')
GO
SET IDENTITY_INSERT [dbo].[examen_Areas] ON 

INSERT [dbo].[examen_Areas] ([Id], [Nombre]) VALUES (1, N'Sistemas API')
INSERT [dbo].[examen_Areas] ([Id], [Nombre]) VALUES (2, N'RH')
INSERT [dbo].[examen_Areas] ([Id], [Nombre]) VALUES (3, N'Ventas')
SET IDENTITY_INSERT [dbo].[examen_Areas] OFF
GO
SET IDENTITY_INSERT [dbo].[examen_Empleados] ON 

INSERT [dbo].[examen_Empleados] ([Id], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [Telefono], [Sexo], [FechaNacimiento], [AreaId]) VALUES (1, N'Angel', N'Lopez', N'Lopez', N'7442081807', N'M', CAST(N'2023-08-28T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[examen_Empleados] OFF
GO
ALTER TABLE [dbo].[examen_Empleados]  WITH CHECK ADD  CONSTRAINT [FK_examen_Empleados_examen_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[examen_Areas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[examen_Empleados] CHECK CONSTRAINT [FK_examen_Empleados_examen_Areas_AreaId]
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_actualizacion_Area]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_actualizacion_Area]
                @nombre nvarchar(30),
                @id int
                AS
                BEGIN
                        UPDATE  examen_Areas SET Nombre=@nombre WHERE Id=@id
                END
            
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_actualizacion_Empleado]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_actualizacion_Empleado]
                @nombre nvarchar(30),
                @apellidoPat nvarchar(30),
                @apellidoMat nvarchar(30),
                @telefono nvarchar(13),
                @sexo nvarchar(1),
                @fechaNac datetime2(7),
                @area int,
                @id int
                AS
                BEGIN
                        UPDATE  examen_Empleados SET Nombre=@nombre,ApellidoPaterno=@apellidoPat,ApellidoMaterno=@apellidoMat,Telefono=@telefono,Sexo=@sexo,FechaNacimiento=@fechaNac,AreaId=@area
                        WHERE Id=@id
                END
            
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_inserccion_Area]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_inserccion_Area]
                @nombre nvarchar(30),
                @id int OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                        INSERT INTO examen_Areas(Nombre)
                        VALUES(@nombre)
                    SELECT @id =  SCOPE_IDENTITY()
                END
            
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_inserccion_Empleado]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_inserccion_Empleado]
                @nombre nvarchar(30),
                @apellidoPat nvarchar(30),
                @apellidoMat nvarchar(30),
                @telefono nvarchar(13),
                @sexo nvarchar(1),
                @fechaNac datetime2(7),
                @area int,
                @id int OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                        INSERT INTO examen_Empleados(Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Sexo,FechaNacimiento,AreaId)
                        VALUES(@nombre, @apellidoPat,@apellidoMat,@telefono, @sexo,@fechaNac,@area)
                    SELECT @id =  SCOPE_IDENTITY()
                END
            
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_ListaAreas]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_ListaAreas]
                AS
                BEGIN
                SELECT Id,Nombre FROM examen_Areas;
                END
            
GO
/****** Object:  StoredProcedure [dbo].[examen_sp_ListaEmpleados]    Script Date: 28/08/2023 11:39:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[examen_sp_ListaEmpleados]
                AS
                BEGIN
                SELECT * FROM examen_Empleados;
                END
            
GO
