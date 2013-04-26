IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Camper]') AND type in (N'U'))
	DROP TABLE [dbo].[Camper]

CREATE TABLE [dbo].[Camper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Foto] [image] NULL,
	[PostIndex] [int] NULL,
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

INSERT INTO [dbo].[Camper] (
	[Id],
	[FirstName],
	[LastName],
	[DateOfBirth],
	[CityId],
	[District],
	[Street],
	[HomeNumber],
	[AppartmentNumber],
	[Contacts])
VALUES (
	1,
	'Виталик',
	'Марченко',
	'1968-04-01',
	1,
	'Рогань',
	'Танковая ул.',
	'2',
	'16',
	'телефон...')

INSERT INTO [dbo].[Camper] (
	[Id],
	[FirstName],
	[LastName],
	[DateOfBirth],
	[CityId],
	[District],
	[Street],
	[HomeNumber],
	[AppartmentNumber],
	[Contacts])
VALUES (
	2,
	'Михаил',
	'Вергелес',
	'1955-01-10',
	1,
	'Алексеевка',
	'Победы пр.',
	'10',
	'100',
	'телефон...')

INSERT INTO [dbo].[Camper] (
	[Id],
	[FirstName],
	[LastName],
	[DateOfBirth],
	[CityId],
	[District],
	[Street],
	[HomeNumber],
	[AppartmentNumber],
	[Contacts])
VALUES (
	3,
	'Мойша Ицыкович',
	'Купитман',
	'1960-04-01',
	2,
	'Слободской',
	'Севашёвская ул.',
	'18',
	'',
	'123-15-18, спросить Беню Ицыковича (брат)')
	

INSERT INTO [dbo].[Camper] (
	[Id],
	[FirstName],
	[LastName],
	[DateOfBirth],
	[CityId],
	[District],
	[Street],
	[HomeNumber],
	[AppartmentNumber],
	[Contacts],
	[Comments])
VALUES (
	4,
	'Беня Арнольдович',
	'Зухенвей',
	'1900-01-01',
	1,
	'Моськалёвка',
	'Рабкоровский пер.',
	'1-б',
	'16',
	'123-45-67, спросить Мойшу Абрамовича Цуккермана (сосед)',
	'Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. Очень длинное примечание. ')
	
SET IDENTITY_INSERT Camper OFF
