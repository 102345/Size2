USE [ContaDigitalSize]
GO

INSERT INTO [dbo].[User]
           ([Nome]
           ,[Login]
           ,[Senha])
     VALUES
           ('TESTE'
           ,'TESTE'
           ,'TESTE')
GO

INSERT INTO [dbo].[User]
           ([Nome]
           ,[Login]
           ,[Senha])
     VALUES
           ('TESTE2'
           ,'TESTE2'
           ,'TESTE2')
GO


INSERT INTO [dbo].[Conta]
           ([IdUsuario]
           ,[Agencia]
           ,[ContaCorrente]
           ,[NroDocumento]
           ,[Saldo]
           ,[TipoConta])
     VALUES
           (1
           ,'1234'
           ,'12345'
           ,'111.111.111-11'
           ,100
           ,'FISICA')
GO

INSERT INTO [dbo].[Conta]
           ([IdUsuario]
           ,[Agencia]
           ,[ContaCorrente]
           ,[NroDocumento]
           ,[Saldo]
           ,[TipoConta])
     VALUES
           (2
           ,'5678'
           ,'56789'
           ,'222.222.222-22'
           ,100
           ,'FISICA')
GO


INSERT INTO [dbo].[MovimentoConta]
           ([IdUsuario]
           ,[TipoMovimento]
           ,[DataMovimento]
           ,[Valor])
     VALUES
           (1
           ,'C'
           ,getdate()
           ,100)
GO

INSERT INTO [dbo].[MovimentoConta]
           ([IdUsuario]
           ,[TipoMovimento]
           ,[DataMovimento]
           ,[Valor])
     VALUES
           (2
           ,'C'
           ,getdate()
           ,100)
GO
