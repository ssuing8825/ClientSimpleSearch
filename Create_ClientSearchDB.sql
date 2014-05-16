USE [master]
GO

/****** Object:  Database [ClientSearch]    Script Date: 5/16/2014 6:50:35 AM ******/
CREATE DATABASE [ClientSearch]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClientSearch', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\ClientSearch.mdf' , SIZE = 720448KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ClientSearch_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\ClientSearch_log.LDF' , SIZE = 3200KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [ClientSearch] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClientSearch].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ClientSearch] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ClientSearch] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ClientSearch] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ClientSearch] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ClientSearch] SET ARITHABORT OFF 
GO

ALTER DATABASE [ClientSearch] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ClientSearch] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [ClientSearch] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ClientSearch] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ClientSearch] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ClientSearch] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ClientSearch] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ClientSearch] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ClientSearch] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ClientSearch] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ClientSearch] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ClientSearch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ClientSearch] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ClientSearch] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ClientSearch] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ClientSearch] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ClientSearch] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ClientSearch] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ClientSearch] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ClientSearch] SET  MULTI_USER 
GO

ALTER DATABASE [ClientSearch] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ClientSearch] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ClientSearch] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ClientSearch] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ClientSearch] SET  READ_WRITE 
GO


