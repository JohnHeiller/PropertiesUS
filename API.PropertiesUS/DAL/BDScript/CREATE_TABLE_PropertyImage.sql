
CREATE TABLE [dbo].[PropertyImage](
	[IdPropertyImage] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProperty] [bigint] NOT NULL,
	[File] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_PropertyImage] PRIMARY KEY CLUSTERED 
(
	[IdPropertyImage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PropertyImage]  WITH CHECK ADD  CONSTRAINT [FK_PropertyImage_Property_IdProperty] FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Property] ([IdProperty])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PropertyImage] CHECK CONSTRAINT [FK_PropertyImage_Property_IdProperty]
GO
