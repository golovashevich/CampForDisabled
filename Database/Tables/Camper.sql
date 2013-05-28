IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Camper]') AND type in (N'U'))
	DROP TABLE [dbo].[Camper]

CREATE TABLE [dbo].[Camper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Foto] [image] NULL,
	[CityId] [int] NOT NULL CONSTRAINT [DF_Camper_CityId]  DEFAULT ((1)),
	[District] [nvarchar](60) NOT NULL,
	[Street] [nvarchar](max) NOT NULL,
	[HomeNumber] [nvarchar](10) NULL,
	[AppartmentNumber] [nvarchar](10) NULL,
	[HomePhone] [nvarchar](70) NULL,
	[AddressNote] [nvarchar](max) NULL,
	[Contacts] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Skype] [nvarchar](40) NULL, 
	[DisabilityGrade] [int] NULL,
	[MedicalNote] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Camper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET DATEFORMAT ymd;
GO

SET IDENTITY_INSERT Camper ON

INSERT INTO [dbo].[Camper] 
	([Id]
	,[FirstName]
	,[LastName]
	,[DateOfBirth]
	,[CityId]
	,[District]
	,[Street]
	,[HomeNumber]
	,[AppartmentNumber]
	,[HomePhone]
	,[AddressNote]
	,[Contacts]
	,[Email]
	,[Skype]
	,[DisabilityGrade]
	,[MedicalNote]
	,[Comments])
VALUES 
	(1
	,N'FirstName1'
	,N'LastName1'
	,'1980-01-01'
	,1
	,N'District1'
	,N'Street'
	,N'25A'
	,N'1'
	,N'+380670000001'
	,N'Folow the pointers to city park than turn left'
	,N'+380500000001, ask Betty (my mother)'
	,N'mail@mail.com'
	,N'my_skype'
	,2
	,N'No medical notes'
	,N'Likes ice-cream'),
	
	(2
	,N'FirstName2'
	,N'LastName2'
	,'1981-05-05'
	,1
	,N'District2'
	,N'Second avenue'
	,N'185'
	,N'123'
	,N'+380670000002'
	,N'Center of city'
	,N'+380500000002, my cell-phone'
	,N'mail2@mail.com'
	,N'my_skype2'
	,2
	,N'No medical notes'
	,N''),
	
	(3
	,N'FirstName3'
	,N'LastName3'
	,'1979-12-06'
	,1
	,N'District2'
	,N'Time Sq.'
	,N'2'
	,N'31'
	,N'+380670000003'
	,N'Center of city'
	,N'+380500000003, my cell-phone'
	,N'mail2@mail.com'
	,N'my_skype3'
	,2
	,N'No medical notes'
	,N''),
	
	(4
	,N'FirstName4'
	,N'LastName4'
	,'1965-07-16'
	,1
	,N'District2'
	,N'Time Sq.'
	,N'2b'
	,N'3'
	,N'+380670000004'
	,N'Center of city'
	,N'+380500000004, my cell-phone'
	,N'mail4@mail.com'
	,N'my_skype4'
	,2
	,N'No medical notes'
	,N''),
	
	(5
	,N'FirstName5'
	,N'LastName5'
	,'1997-10-25'
	,1
	,N'District3'
	,N'Elisabet St.'
	,N'26'
	,N''
	,N'+380670000005'
	,N'Near Target supermarket'
	,N'+380500000005, my cell-phone'
	,N'mail5@mail.com'
	,N'my_skype5'
	,1
	,N'Grass pollen allergy'
	,N'')

SET IDENTITY_INSERT Camper OFF
