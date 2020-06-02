USE [avt]
GO

/****** Object:  Table [dbo].[timetable]    Script Date: 2/06/2020 7:23:45 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[timetable](
	[staffname] [varchar](100) NULL,
	[email] [varchar](100) NOT NULL,
	[Monday] [varchar](100) NULL,
	[Tuesday] [varchar](100) NULL,
	[Wednesday] [varchar](100) NULL,
	[Thursday] [varchar](100) NULL,
	[Friday] [varchar](100) NULL
) ON [PRIMARY]
GO


