USE [VisitorRegistration]
GO

/****** Object:  Table [dbo].[Files]    Script Date: 24.08.2017 21:26:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Files](
	[ID] [nvarchar](38) NOT NULL,
	[FileUrl] [nvarchar](250) NULL,
	[VisitorID] [int] NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Visitor] FOREIGN KEY([VisitorID])
REFERENCES [dbo].[Visitor] ([ID])
GO

ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Visitor]
GO


