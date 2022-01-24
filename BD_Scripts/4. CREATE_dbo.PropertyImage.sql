USE [PropertiesUS]
GO

CREATE TABLE [dbo].[PropertyImage](
	[IdPropertyImage] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProperty] [bigint] NOT NULL,
	[File] [nvarchar](max) NOT NULL,
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties image record identifier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyImage', @level2type=N'COLUMN',@level2name=N'IdPropertyImage'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties record identifier FK' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyImage', @level2type=N'COLUMN',@level2name=N'IdProperty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Base64 value of properties image file' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyImage', @level2type=N'COLUMN',@level2name=N'File'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties image record enabled indicator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyImage', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
