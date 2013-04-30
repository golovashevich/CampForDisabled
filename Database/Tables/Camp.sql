﻿IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Camp]') AND type in (N'U'))
	DROP TABLE [dbo].[Camp]

CREATE TABLE [dbo].[Camp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Theme] [nvarchar](100) NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[History] [nvarchar](max) NULL,
 CONSTRAINT [PK_Camp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET DATEFORMAT ymd;

SET IDENTITY_INSERT Camp ON

INSERT INTO [dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (1
		   ,2004
           ,N'First Camp'
           ,N'Greeting'
           ,'2004-06-01'
           ,'2004-06-12'
           ,N''
           ,N'')

INSERT INTO [dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (2
		   ,2005
           ,N'Second Camp'
           ,N'Dasibled people problems'
           ,'2005-06-04'
           ,'2005-06-18'
           ,N''
           ,N'')

INSERT INTO [dbo].[Camp]
           ([Id]
		   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (3
		   ,2006
           ,N'Third Camp'
           ,N'Placement for disabled'
           ,'2006-06-06'
           ,'2006-06-19'
           ,N''
           ,N'')

INSERT INTO [dbo].[Camp]
           ([Id]
	 	   ,[Year]
           ,[Name]
           ,[Theme]
           ,[BeginDate]
           ,[EndDate]
           ,[Description]
           ,[History])
     VALUES
           (4
		   ,2007
           ,N'4th Camp'
           ,N'Medical helping'
           ,'2007-06-02'
           ,'2007-06-15'
           ,N''
           ,N'')

SET IDENTITY_INSERT Camp OFF

