USE [PropertiesUS]
GO

CREATE TABLE [dbo].[Owner](
	[IdOwner] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](150) NULL,
	[Phone] [nvarchar](15) NULL,
	[Photo] [nvarchar](max) NULL,
	[Birthday] [datetime2](7) NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[IdOwner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Owners record identifier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'IdOwner'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Owners full name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Owners residence address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Owners phone number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Base64 value of owners photo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'Photo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Owners date of birth' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Owner', @level2type=N'COLUMN',@level2name=N'Birthday'
GO

USE [PropertiesUS]
GO

INSERT INTO [dbo].[Owner]
           ([Name],[Address],[Phone],[Photo],[Birthday])
     VALUES
           ('Default User','Street 1 # 10-10','3501590000',NULL,'1900-01-01')
GO
