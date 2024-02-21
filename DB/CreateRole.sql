USE [FoodServe]
GO

/****** Object:  Table [dbo].[FS_Role]    Script Date: 21/02/2024 11:21:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FS_Role](
	[role_id] [varchar](20) NOT NULL,
	[role_name] [varchar](50) NOT NULL,
	[ActiveDate] [date] NULL,
	[isActive] [varchar](20) NULL,
 CONSTRAINT [PK_FS_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


