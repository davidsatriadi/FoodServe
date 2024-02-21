USE [FoodServe]
GO

/****** Object:  Table [dbo].[FS_Food_Order]    Script Date: 21/02/2024 11:21:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FS_Food_Order](
	[order_id] [varchar](20) NOT NULL,
	[user_id] [varchar](20) NOT NULL,
	[order_date] [datetime] NOT NULL,
	[is_close] [char](1) NOT NULL,
	[qty_total] [numeric](3, 0) NULL,
	[price_total] [numeric](18, 0) NULL,
 CONSTRAINT [PK_FS_Food_Order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FS_Food_Order]  WITH CHECK ADD  CONSTRAINT [FK_FS_Food_Order_FS_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[FS_User] ([user_id])
GO

ALTER TABLE [dbo].[FS_Food_Order] CHECK CONSTRAINT [FK_FS_Food_Order_FS_User]
GO


