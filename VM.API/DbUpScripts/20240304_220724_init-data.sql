
SET IDENTITY_INSERT [dbo].[FeesHead] ON 
GO
INSERT [dbo].[FeesHead] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (1, N'Basic user fee', CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesHead] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (2, N'Seller''s special fee', CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesHead] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (3, N'Association based ', CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesHead] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (4, N'Fixed storage fee ', CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[FeesHead] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleType] ON 
GO
INSERT [dbo].[VehicleType] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (1, N'Luxury', CAST(N'2024-03-02T14:54:27.803' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[VehicleType] ([Id], [Name], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (2, N'Common', CAST(N'2024-03-02T14:54:27.803' AS DateTime), 1, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[VehicleType] OFF
GO
SET IDENTITY_INSERT [dbo].[FeesStructure] ON 
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (1, 1, 1, N'Percentage', 10, 25, 200, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (2, 2, 1, N'Percentage', 10, 10, 50, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (3, 1, 2, N'Percentage', 4, NULL, NULL, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (6, 2, 2, N'Percentage', 2, NULL, NULL, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (7, NULL, 3, N'Fix', 5, 1, 500, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (8, NULL, 3, N'Fix', 10, 501, 1000, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (9, NULL, 3, N'Fix', 15, 1001, 3000, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (10, NULL, 3, N'Fix', 20, 3001, NULL, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[FeesStructure] ([Id], [TypeId], [FeesHeadId], [IsFixOrPercentage], [Value], [MinValue], [MaxValue], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsDeleted]) VALUES (11, NULL, 4, N'Fix', 100, NULL, NULL, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 1, CAST(N'2024-03-04T16:13:36.110' AS DateTime), 1, 0)
GO
SET IDENTITY_INSERT [dbo].[FeesStructure] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Email], [EmailVerifiedAt], [Password], [Salt], [CreatedAt], [CreatedBy], [IsDeleted]) VALUES (1, N'payal@7span.com', CAST(N'2024-03-04T21:55:45.470' AS DateTime), N'AQAAAAEACSfAAAAAEIjauGVJWfC5kIaa6f/E3K20wOWlMXit991rWDSHANLVn5733q4iqE2VnrS433ZSVA==', N'be6c12d79b270f776dced85b35c32307', CAST(N'2024-03-01T00:00:00.000' AS DateTime), 0, 0)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
