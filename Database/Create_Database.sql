USE [master]
GO

/****** Object:  Database [ContaDigitalSize]    Script Date: 24/06/2019 15:20:56 ******/
CREATE DATABASE [ContaDigitalSize]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContaDigitalSize', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\ContaDigitalSize.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ContaDigitalSize_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\ContaDigitalSize_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [ContaDigitalSize] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContaDigitalSize].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ContaDigitalSize] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET ARITHABORT OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ContaDigitalSize] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ContaDigitalSize] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ContaDigitalSize] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ContaDigitalSize] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ContaDigitalSize] SET  MULTI_USER 
GO

ALTER DATABASE [ContaDigitalSize] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ContaDigitalSize] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ContaDigitalSize] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ContaDigitalSize] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ContaDigitalSize] SET  READ_WRITE 
GO


