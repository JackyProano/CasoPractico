USE [CreditoAutomotriz]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[cl_id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[cl_identificacion] [varchar](10) NOT NULL,
	[cl_nombres] [varchar](100) NOT NULL,
	[cl_apellidos] [varchar](100) NOT NULL,
	[cl_edad] [int] NOT NULL,
	[cl_fecha_nacimiento] [date] NOT NULL,
	[cl_direccion] [varchar](150) NOT NULL,
	[cl_telefono] [varchar](10) NOT NULL,
	[cl_estado_civil] [varchar](1) NOT NULL,
	[cl_identificacion_conyuge] [varchar](10) NULL,
	[cl_nombre_conyuge] [varchar](150) NULL,
	[cl_es_sujeto_credito] [bit] NOT NULL,
 CONSTRAINT [PK_cl_cliente] PRIMARY KEY CLUSTERED 
(
	[cl_id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[marca]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[marca](
	[ma_id_marca] [int] IDENTITY(1,1) NOT NULL,
	[ma_descripcion_marca] [varchar](100) NOT NULL,
 CONSTRAINT [PK_au_marca] PRIMARY KEY CLUSTERED 
(
	[ma_id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[patio]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patio](
	[pa_id_patio] [int] IDENTITY(1,1) NOT NULL,
	[pa_nombre] [varchar](50) NOT NULL,
	[pa_direccion] [varchar](50) NOT NULL,
	[pa_telefono] [varchar](10) NOT NULL,
	[pa_numero_punto_venta] [int] NOT NULL,
 CONSTRAINT [PK_patio] PRIMARY KEY CLUSTERED 
(
	[pa_id_patio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON	) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ejecutivo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ejecutivo](
	[ej_id_ejecutivo] [int] IDENTITY(1,1) NOT NULL,
	[ej_identificacion] [varchar](10) NOT NULL,
	[ej_nombres] [varchar](50) NOT NULL,
	[ej_apellidos] [varchar](50) NOT NULL,
	[ej_direccion] [varchar](50) NOT NULL,
	[ej_telefono_convencional] [varchar](10) NOT NULL,
	[ej_celular] [varchar](10) NOT NULL,
	[ej_id_patio] [int] NOT NULL,
	[ej_edad] [int] NOT NULL,
 CONSTRAINT [PK_ejecutivo] PRIMARY KEY CLUSTERED 
(
	[ej_id_ejecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ejecutivo]  WITH CHECK ADD  CONSTRAINT [FK_patio_ejecutivo] FOREIGN KEY([ej_id_patio])
REFERENCES [dbo].[patio] ([pa_id_patio])
GO
ALTER TABLE [dbo].[ejecutivo] CHECK CONSTRAINT [FK_patio_ejecutivo]
GO
/****** Object:  Table [dbo].[vehiculo]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehiculo](
	[ve_id_vehiculo] [int] IDENTITY(1,1) NOT NULL,
	[ve_placa] [varchar](10) NOT NULL,
	[ve_modelo] [int] NOT NULL,
	[ve_id_marca] [int] NOT NULL,
	[ve_tipo] [varchar](50) NOT NULL,
	[ve_cilindraje] [varchar](50) NOT NULL,
	[ve_avaluo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_vehiculo] PRIMARY KEY CLUSTERED 
(
	[ve_id_vehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_marca_vehiculo] FOREIGN KEY([ve_id_marca])
REFERENCES [dbo].[marca] ([ma_id_marca])
GO
ALTER TABLE [dbo].[vehiculo] CHECK CONSTRAINT [FK_marca_vehiculo]
GO
/****** Object:  Table [dbo].[asignacion_cliente]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asignacion_cliente](
	[as_id_asignacion] [int] IDENTITY(1,1) NOT NULL,
	[as_id_cliente] [int] NOT NULL,
	[as_id_patio] [int] NOT NULL,
	[as_fecha_asignacion] [date] NOT NULL,
 CONSTRAINT [PK_Asignacion_cliente] PRIMARY KEY CLUSTERED 
(
	[as_id_asignacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[asignacion_cliente]  WITH CHECK ADD  CONSTRAINT [FK_asignacion_cliente_cliente] FOREIGN KEY([as_id_cliente])
REFERENCES [dbo].[cliente] ([cl_id_cliente])
GO
ALTER TABLE [dbo].[asignacion_cliente] CHECK CONSTRAINT [FK_asignacion_cliente_cliente]
GO

ALTER TABLE [dbo].[asignacion_cliente]  WITH CHECK ADD  CONSTRAINT [FK_asignacion_cliente_patio] FOREIGN KEY([as_id_patio])
REFERENCES [dbo].[patio] ([pa_id_patio])
GO
ALTER TABLE [dbo].[asignacion_cliente] CHECK CONSTRAINT [FK_asignacion_cliente_patio]
GO
/****** Object:  Table [dbo].[solicitud_credito]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[solicitud_credito](
	[so_id_solicitud] [int] IDENTITY(1,1) NOT NULL,
	[so_fecha_elaboracion] [date] NOT NULL,
	[so_id_cliente] [int] NOT NULL,
	[so_id_patio] [int] NOT NULL,
	[so_id_vehiculo] [int] NOT NULL,
	[so_meses_plazo] [int] NOT NULL,
	[so_cuotas] [int] NOT NULL,
	[so_entrada] [money] NOT NULL,
	[so_id_ejecutivo] [int] NOT NULL,
	[so_observacion] [varchar](100) NULL,
	[so_estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_solicitud_credito] PRIMARY KEY CLUSTERED 
(
	[so_id_solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[solicitud_credito]  WITH CHECK ADD  CONSTRAINT [FK_cliente_solicitud_credito] FOREIGN KEY([so_id_cliente])
REFERENCES [dbo].[cliente] ([cl_id_cliente])
GO
ALTER TABLE [dbo].[solicitud_credito] CHECK CONSTRAINT [FK_cliente_solicitud_credito]
GO
ALTER TABLE [dbo].[solicitud_credito]  WITH CHECK ADD  CONSTRAINT [FK_ejecutivo_solicitud_credito] FOREIGN KEY([so_id_ejecutivo])
REFERENCES [dbo].[ejecutivo] ([ej_id_ejecutivo])
GO
ALTER TABLE [dbo].[solicitud_credito] CHECK CONSTRAINT [FK_ejecutivo_solicitud_credito]
GO
ALTER TABLE [dbo].[solicitud_credito]  WITH CHECK ADD  CONSTRAINT [FK_patio_solicitud_credito] FOREIGN KEY([so_id_patio])
REFERENCES [dbo].[patio] ([pa_id_patio])
GO
ALTER TABLE [dbo].[solicitud_credito] CHECK CONSTRAINT [FK_patio_solicitud_credito]
GO
ALTER TABLE [dbo].[solicitud_credito]  WITH CHECK ADD  CONSTRAINT [FK_vehiculo_solicitud_credito] FOREIGN KEY([so_id_vehiculo])
REFERENCES [dbo].[vehiculo] ([ve_id_vehiculo])
GO
ALTER TABLE [dbo].[solicitud_credito] CHECK CONSTRAINT [FK_vehiculo_solicitud_credito]
GO