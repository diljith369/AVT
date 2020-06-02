USE [avt]
GO

ALTER TABLE [dbo].[userdetails] DROP CONSTRAINT [DF__userdetai__isadm__023D5A04]
GO

ALTER TABLE [dbo].[userdetails] DROP CONSTRAINT [DF__userdetai__email__014935CB]
GO

/****** Object:  Table [dbo].[userdetails]    Script Date: 31/05/2020 11:16:57 pm ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[userdetails]') AND type in (N'U'))
DROP TABLE [dbo].[userdetails]
GO

/****** Object:  Table [dbo].[userdetails]    Script Date: 31/05/2020 11:16:57 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[userdetails](
	[id] [int] IDENTITY(1,1) primary key,
	[username] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[email] [varchar](100) NULL,
	[isadmin] [int] NULL,
)
GO

ALTER TABLE [dbo].[userdetails] ADD  DEFAULT (NULL) FOR [email]
GO

ALTER TABLE [dbo].[userdetails] ADD  DEFAULT ((0)) FOR [isadmin]
GO


