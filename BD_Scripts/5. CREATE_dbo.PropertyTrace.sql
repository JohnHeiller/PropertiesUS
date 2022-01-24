USE [PropertiesUS]
GO

CREATE TABLE [dbo].[PropertyTrace](
	[IdPropertyTrace] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProperty] [bigint] NOT NULL,
	[DateSale] [datetime2](7) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[Tax] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PropertyTrace] PRIMARY KEY CLUSTERED 
(
	[IdPropertyTrace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PropertyTrace]  WITH CHECK ADD  CONSTRAINT [FK_PropertyTrace_Property_IdProperty] FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Property] ([IdProperty])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PropertyTrace] CHECK CONSTRAINT [FK_PropertyTrace_Property_IdProperty]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties trace record identifier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'IdPropertyTrace'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties record identifier FK' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'IdProperty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties sale date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'DateSale'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Properties buyer name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sale value of the property' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'Value'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tax value for the sale of the property' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropertyTrace', @level2type=N'COLUMN',@level2name=N'Tax'
GO
