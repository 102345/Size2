USE [ContaDigitalSize]
GO

/****** Object:  Table [dbo].[User]    Script Date: 24/06/2019 15:22:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](75) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Senha] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[User] ([IdUsuario])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO


/****** Object:  Table [dbo].[Conta]    Script Date: 24/06/2019 15:22:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Conta](
	[IdConta] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Agencia] [varchar](20) NOT NULL,
	[ContaCorrente] [varchar](40) NOT NULL,
	[NroDocumento] [varchar](25) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
	[TipoConta] [varchar](40) NULL,
 CONSTRAINT [PK_Conta] PRIMARY KEY CLUSTERED 
(
	[IdConta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Conta]  WITH CHECK ADD  CONSTRAINT [FK_Conta_User] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[User] ([IdUsuario])
GO

ALTER TABLE [dbo].[Conta] CHECK CONSTRAINT [FK_Conta_User]
GO

/****** Object:  Table [dbo].[MovimentoConta]    Script Date: 24/06/2019 15:23:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MovimentoConta](
	[IdMovimento] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[TipoMovimento] [varchar](25) NOT NULL,
	[DataMovimento] [datetime] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_MovimentoConta] PRIMARY KEY CLUSTERED 
(
	[IdMovimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MovimentoConta]  WITH CHECK ADD  CONSTRAINT [FK_MovimentoConta_User] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[User] ([IdUsuario])
GO

ALTER TABLE [dbo].[MovimentoConta] CHECK CONSTRAINT [FK_MovimentoConta_User]
GO








