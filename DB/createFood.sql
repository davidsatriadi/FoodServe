USE [FoodServe]
GO

/****** Object:  Table [dbo].[FS_Food]    Script Date: 21/02/2024 11:21:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FS_Food](
	[food_id] [varchar](20) NOT NULL,
	[food_name] [varchar](50) NOT NULL,
	[food_price] [numeric](18, 0) NOT NULL,
	[active_date] [date] NOT NULL,
	[is_active] [char](1) NOT NULL,
 CONSTRAINT [PK_FS_Food] PRIMARY KEY CLUSTERED 
(
	[food_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


