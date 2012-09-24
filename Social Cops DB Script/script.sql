USE [master]
GO
/****** Object:  Database [socialcops]    Script Date: 24-Sep-12 18:16:50 ******/
CREATE DATABASE [socialcops]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'socialcops', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\socialcops.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'socialcops_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\socialcops_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [socialcops] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [socialcops].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [socialcops] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [socialcops] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [socialcops] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [socialcops] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [socialcops] SET ARITHABORT OFF 
GO
ALTER DATABASE [socialcops] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [socialcops] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [socialcops] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [socialcops] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [socialcops] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [socialcops] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [socialcops] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [socialcops] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [socialcops] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [socialcops] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [socialcops] SET  DISABLE_BROKER 
GO
ALTER DATABASE [socialcops] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [socialcops] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [socialcops] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [socialcops] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [socialcops] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [socialcops] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [socialcops] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [socialcops] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [socialcops] SET  MULTI_USER 
GO
ALTER DATABASE [socialcops] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [socialcops] SET DB_CHAINING OFF 
GO
ALTER DATABASE [socialcops] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [socialcops] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [socialcops]
GO
/****** Object:  Table [dbo].[Authorities]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authorities](
	[authId] [varchar](10) NOT NULL,
	[authName] [varchar](500) NOT NULL,
	[authAddress] [varchar](1000) NULL,
	[email] [varchar](500) NULL,
	[phone] [varchar](20) NULL,
	[numPending] [int] NULL,
	[numResolved] [int] NULL,
	[latitude] [float] NULL,
	[longitude] [float] NULL,
	[website] [varchar](200) NULL,
	[profilePic] [varchar](200) NULL,
	[flag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[authId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comments](
	[userId] [varchar](10) NOT NULL,
	[complaintId] [varchar](10) NOT NULL,
	[comment] [varchar](2000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[complaintId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Complaints]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Complaints](
	[complaintId] [varchar](10) NOT NULL,
	[userId] [varchar](10) NOT NULL,
	[title] [varchar](100) NOT NULL,
	[details] [varchar](1000) NULL,
	[numLikes] [int] NULL,
	[numComments] [int] NULL,
	[picture] [varchar](200) NULL,
	[complaintDate] [date] NULL,
	[location] [varchar](1000) NULL,
	[latitude] [float] NULL,
	[longitude] [float] NULL,
	[category] [varchar](50) NOT NULL,
	[complaintStatus] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[complaintId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Jurisdiction]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Jurisdiction](
	[authId] [varchar](10) NOT NULL,
	[complaintId] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[authId] ASC,
	[complaintId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Likes]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Likes](
	[userId] [varchar](10) NOT NULL,
	[complaintId] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[complaintId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subscriptions]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subscriptions](
	[subscriberId] [varchar](10) NOT NULL,
	[subscribedToId] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[subscriberId] ASC,
	[subscribedToId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [varchar](10) NOT NULL,
	[userName] [varchar](100) NOT NULL,
	[email] [varchar](100) NULL,
	[pwd] [varchar](200) NULL,
	[phone] [varchar](20) NULL,
	[userAddress] [varchar](200) NULL,
	[points] [float] NULL,
	[userRank] [varchar](50) NULL,
	[profilePic] [varchar](200) NULL,
	[webURI] [varchar](200) NULL,
	[phoneURI] [varchar](200) NULL,
	[numComplaints] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([complaintId])
REFERENCES [dbo].[Complaints] ([complaintId])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Complaints]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Jurisdiction]  WITH CHECK ADD FOREIGN KEY([authId])
REFERENCES [dbo].[Authorities] ([authId])
GO
ALTER TABLE [dbo].[Jurisdiction]  WITH CHECK ADD FOREIGN KEY([complaintId])
REFERENCES [dbo].[Complaints] ([complaintId])
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD FOREIGN KEY([complaintId])
REFERENCES [dbo].[Complaints] ([complaintId])
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD FOREIGN KEY([subscriberId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD FOREIGN KEY([subscribedToId])
REFERENCES [dbo].[Users] ([userId])
GO
/****** Object:  Trigger [dbo].[updateComments]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger 
[dbo].[updateComments] ON [dbo].[Comments]
For INSERT, UPDATE, DELETE
AS

Declare @check_complaintId varchar(10)

Select @check_complaintId = (Select complaintId from inserted)

Update Complaints set numComments = (Select Count(*) from Comments where complaintId = @check_complaintId) where complaintId = @check_complaintId
GO
/****** Object:  Trigger [dbo].[updateComplaints]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger 
[dbo].[updateComplaints] ON [dbo].[Complaints]
For INSERT, UPDATE, DELETE
AS

Declare @check_userId varchar(10)

Select @check_userId = (Select userId from inserted)

Update Users set numComplaints = (Select Count(*) from Complaints where userId = @check_userId) where userId = @check_userId
GO
/****** Object:  Trigger [dbo].[updateStatusCount]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger 
[dbo].[updateStatusCount] ON [dbo].[Complaints]
For UPDATE, DELETE
AS

Declare @check_complaintId varchar(10)

Select @check_complaintId = (Select complaintId from inserted)

Update Authorities set numResolved = (Select Count(*) from Complaints where complaintId = @check_complaintId AND complaintStatus = 'resolved') where authId IN (Select authId from Jurisdiction where complaintId = @check_complaintId)
Update AUthorities set numPending = (Select Count(*) from Complaints where complaintId = @check_complaintId And complaintStatus = 'pending') where authId IN (Select authId from Jurisdiction where complaintId = @check_complaintId)
GO
/****** Object:  Trigger [dbo].[updateAuthorities]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger 
[dbo].[updateAuthorities] ON [dbo].[Jurisdiction]
For INSERT, UPDATE, DELETE
AS

Declare @check_complaintId varchar(10)
Declare @check_authId varchar(10)

Select @check_complaintId = (Select complaintId from inserted)
Select @check_authId = (Select authId from inserted)

Update Authorities set numResolved = (Select Count(*) from Complaints where complaintId = @check_complaintId AND complaintStatus = 'resolved') where authId = @check_authId
Update AUthorities set numPending = (Select Count(*) from Complaints where complaintId = @check_complaintId And complaintStatus = 'pending') where authId = @check_authId
GO
/****** Object:  Trigger [dbo].[updatelikes]    Script Date: 24-Sep-12 18:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Trigger 
[dbo].[updatelikes] ON [dbo].[Likes]
For INSERT, UPDATE, DELETE
AS

Declare @check_complaintId varchar(10)

Select @check_complaintId = (Select complaintId from inserted)

Update Complaints set numLikes = (Select Count(*) from Likes where complaintId = @check_complaintId) where complaintId = @check_complaintId
GO
USE [master]
GO
ALTER DATABASE [socialcops] SET  READ_WRITE 
GO
