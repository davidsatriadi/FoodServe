USE [FoodServe]
GO

/****** Object:  Table [dbo].[FS_User]    Script Date: 21/02/2024 11:22:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FS_User](
	[user_id] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DoB] [date] NULL,
	[Role] [varchar](20) NOT NULL,
	[Password] [varchar](50) NULL,
	[ActiveDate] [date] NULL,
	[isActive] [char](1) NOT NULL,
 CONSTRAINT [PK_FS_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FS_User]  WITH CHECK ADD  CONSTRAINT [FK_FS_User_FS_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[FS_Role] ([role_id])
GO

ALTER TABLE [dbo].[FS_User] CHECK CONSTRAINT [FK_FS_User_FS_Role]
GO


