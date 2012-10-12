CREATE TABLE [dbo].[Users](
	[userId] int Primary Key Identity,
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
	[date] [datetime] NOT NULL
);

CREATE TABLE Authorities(
	authId int  Primary key Identity,
	authName varchar(500) NOT NULL,
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
	[date] [datetime] NOT NULL,
);

CREATE TABLE [dbo].[Complaints](
	[complaintId] int Primary Key Identity,
	[userId] int NOT NULL,
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
	[date] [datetime] NOT NULL,
	[isAnonymous] [int] NOT NULL Default 0,
	Foreign Key(userId) references Users(userId)
);

CREATE TABLE Comments(
	[userId] [int] NOT NULL,
	[complaintId] [int] NOT NULL,
	[comment] [varchar](2000) NOT NULL,
	[date] [datetime] NOT NULL,

	Foreign key(userId) references Users(userId),
	Foreign Key(complaintId) references Complaints(complaintId),
);

CREATE TABLE [dbo].[Jurisdiction](
	[authId] int NOT NULL,
	[complaintId] int NOT NULL,
	[date] [datetime] NOT NULL,

	Foreign Key(authId) references Authorities(authId),
	Foreign Key(complaintId) references Complaints(complaintId)
);

CREATE TABLE [dbo].[Likes](
	[userId] int NOT NULL,
	[complaintId] int NOT NULL,
	[date] [datetime] NOT NULL,

	Foreign Key(userId) References Users(userId),
	Foreign Key(complaintId) References Complaints(complaintId)
);

CREATE TABLE [dbo].[Subscriptions](
	[subscriberId] int NOT NULL,
	[subscribedToId] int NOT NULL,
	[date] [datetime] NOT NULL,

	Foreign Key(subscriberId) references Users(userId),
	Foreign Key(subscribedToId) references Users(userId)
);

Create Table Spam(
	userId int NOT NULL,
	complaintId int NOT NULL,
	date datetime NOT NULL,

	Foreign Key (userId) references Users(userId),
	Foreign Key(complaintId) references Complaints(complaintId)
);
