
CREATE TABLE [dbo].[Property](
	[IdProperty] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](150) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CodeInternal] [nvarchar](40) NULL,
	[Year] [nvarchar](4) NULL,
	[IdOwner] [bigint] NOT NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[IdProperty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Property] ADD  DEFAULT ((0.0)) FOR [Price]
GO

ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_Owner_IdOwner] FOREIGN KEY([IdOwner])
REFERENCES [dbo].[Owner] ([IdOwner])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_Owner_IdOwner]
GO
