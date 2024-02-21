USE [FoodServe]
GO

/****** Object:  Table [dbo].[FS_Order_Detail]    Script Date: 21/02/2024 11:21:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FS_Order_Detail](
	[order_id] [varchar](20) NOT NULL,
	[detail_id] [varchar](20) NOT NULL,
	[food_id] [varchar](20) NOT NULL,
	[qty] [numeric](3, 0) NOT NULL,
 CONSTRAINT [PK_FS_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FS_Order_Detail]  WITH CHECK ADD  CONSTRAINT [FK_FS_Order_Detail_FS_Food] FOREIGN KEY([food_id])
REFERENCES [dbo].[FS_Food] ([food_id])
GO

ALTER TABLE [dbo].[FS_Order_Detail] CHECK CONSTRAINT [FK_FS_Order_Detail_FS_Food]
GO

ALTER TABLE [dbo].[FS_Order_Detail]  WITH CHECK ADD  CONSTRAINT [FK_FS_Order_Detail_FS_Food_Order] FOREIGN KEY([order_id])
REFERENCES [dbo].[FS_Food_Order] ([order_id])
GO

ALTER TABLE [dbo].[FS_Order_Detail] CHECK CONSTRAINT [FK_FS_Order_Detail_FS_Food_Order]
GO


