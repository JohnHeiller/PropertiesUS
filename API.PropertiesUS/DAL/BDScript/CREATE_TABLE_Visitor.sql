
CREATE TABLE [dbo].[Visitor](
	[IdVisitor] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[VisitDate] [datetime2](7) NOT NULL,
	[IdProperty] [bigint] NOT NULL,
 CONSTRAINT [PK_Visitor] PRIMARY KEY CLUSTERED 
(
	[IdVisitor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Visitor]  WITH CHECK ADD  CONSTRAINT [FK_Visitor_Property_IdProperty] FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Property] ([IdProperty])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Visitor] CHECK CONSTRAINT [FK_Visitor_Property_IdProperty]
GO
