IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
	DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--hash for "123" is "202cb962ac59075b964b07152d234b70"

SET IDENTITY_INSERT [dbo].[User] ON

INSERT INTO [dbo].[User] ([Id], [Name], [PasswordHash])
VALUES 
(1, 'Greek', '202cb962ac59075b964b07152d234b70'),
(2, 'BigMax', '202cb962ac59075b964b07152d234b70'),
(3, 'Jane', '202cb962ac59075b964b07152d234b70'),
(4, 'Oksana', '202cb962ac59075b964b07152d234b70'),
(5, 'Editor', '202cb962ac59075b964b07152d234b70')

SET IDENTITY_INSERT Driver OFF
