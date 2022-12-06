USE [Contacts]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 05/12/2022 15:47:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[SocialNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[BirthDate] [datetime2](7) NOT NULL,
	[Gender] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


