IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Driver')
	DROP TABLE [Driver];
GO

CREATE TABLE [dbo].[Driver](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar] (MAX) NOT NULL,
	[LastName] [nvarchar] (MAX) NOT NULL,
	[Contacts] [nvarchar] (MAX) NOT NULL,
	[SitPlacesNum] [int] NULL,
	[WheelchairsNum] [int] NULL,
	[Comments] [nvarchar] (MAX) NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT Driver ON

INSERT INTO [dbo].[Driver] 
	([Id]
	,[FirstName]
	,[LastName]
	,[Contacts]
	,[SitPlacesNum]
	,[WheelchairsNum]
	,[Comments])
VALUES
	(1
	,N'Driver1'
	,N'Busman'
	,N'+380 67 000-00-00'
	,18
	,8
	,N'Volkswagen LT'),

	(2 
	,N'Driver2'
	,N'Vanman'
	,N'+380 67 000-00-01'
	,12
	,7
	,N'Vito TDI'),

	(3
	,N'Driver3'
	,N'Carman'
	,N'+380 67 000-00-03'
	,5
	,1
	,N'VAZ 2108')

SET IDENTITY_INSERT Driver OFF
