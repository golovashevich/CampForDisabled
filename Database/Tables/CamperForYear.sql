IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CamperForYear]') AND type in (N'U'))
	DROP TABLE [dbo].[CamperForYear]

CREATE TABLE [dbo].[CamperForYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampId] [int] NOT NULL,
	[CamperId] [int] NOT NULL,
	CONSTRAINT [PK_CamperForYear] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT CamperForYear ON

INSERT INTO [dbo].[CamperForYear]
	([Id], [CampId], [CamperId]) 
VALUES
	(1, 1, 4),
	(13, 1, 4),
	(2, 1, 2),
	(3, 1, 3),
	(4, 2, 1),
	(5, 2, 3),
	(6, 2, 4),
	(7, 3, 1),
	(8, 3, 2),
	(9, 3, 4),
	(10, 4, 1),
	(11, 4, 2),
	(12, 4, 3);

SET IDENTITY_INSERT CamperForYear OFF
