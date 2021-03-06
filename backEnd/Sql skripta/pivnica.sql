USE [Pivnica]
GO
SET IDENTITY_INSERT [dbo].[Pivnice] ON 

INSERT [dbo].[Pivnice] ([ID], [Naziv], [BrojStolova]) VALUES (1, N'Murphy''s pub', 5)
INSERT [dbo].[Pivnice] ([ID], [Naziv], [BrojStolova]) VALUES (4, N'Irish pub', 5)
SET IDENTITY_INSERT [dbo].[Pivnice] OFF
GO
SET IDENTITY_INSERT [dbo].[Stolovi] ON 


INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (1002, 9, 1)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (2004, 14, 4)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3003, 18, 1)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3004, 19, 1)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3005, 20, 1)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3006, 21, 1)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3007, 2, 4)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3008, 3, 4)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3009, 4, 4)
INSERT [dbo].[Stolovi] ([ID], [BrojStola], [PivnicaID]) VALUES (3010, 5, 4)
SET IDENTITY_INSERT [dbo].[Stolovi] OFF
GO
SET IDENTITY_INSERT [dbo].[Meni] ON 

INSERT [dbo].[Meni] ([ID], [OpisPromocije], [PivnicaID], [MinKolicinaHrane], [MinKolicinaPica], [Popust]) VALUES (4, N'Za narucenih 5 ili vise piva popust 20%', 1, 5, 5, 20)
INSERT [dbo].[Meni] ([ID], [OpisPromocije], [PivnicaID], [MinKolicinaHrane], [MinKolicinaPica], [Popust]) VALUES (8, N'Za narucenih 5 i vise piva popust od 20%.', 4, 5, 0, 20)
SET IDENTITY_INSERT [dbo].[Meni] OFF
GO
