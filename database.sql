USE [OnlineMarketDB]
GO
/****** Object:  User [IIS APPPOOL\onlinemarket]    Script Date: 03/20/2017 15:00:44 ******/
CREATE USER [IIS APPPOOL\onlinemarket] FOR LOGIN [IIS APPPOOL\onlinemarket]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([Id], [Title]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([Id], [Title]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([Id], [Title]) VALUES (3, N'Banned user')
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[Resources]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Resources] ON
INSERT [dbo].[Resources] ([Id], [Title], [Price]) VALUES (1, N'Wood', 13.4)
INSERT [dbo].[Resources] ([Id], [Title], [Price]) VALUES (2, N'Iron', 30.5)
INSERT [dbo].[Resources] ([Id], [Title], [Price]) VALUES (3, N'Oil', 28)
SET IDENTITY_INSERT [dbo].[Resources] OFF
/****** Object:  Table [dbo].[DealTypes]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DealTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.DealTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DealTypes] ON
INSERT [dbo].[DealTypes] ([Id], [Type]) VALUES (1, N'Purchase')
INSERT [dbo].[DealTypes] ([Id], [Type]) VALUES (2, N'Sale')
SET IDENTITY_INSERT [dbo].[DealTypes] OFF
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201703201050217_InitialCreate', N'OnlineMarket.DataAccessLayer.OnlineMarketContext', 0x1F8B0800000000000400ED5C4B6FE43612BE07D8FF20E8B41B4C2C7BE69218DD093CED9985B1E3F1C4ED19E436A025765B0845F54A946323C82FCB213F69FFC292A21E7C4A54AB5F760C5F6C3E3E16AB8AC52AAACAFFFBF3AFC94F0F09F2EE6196C7299EFA2747C7BE07719846315E4EFD822CBEFBDEFFE9C77F7C337917250FDE977ADC1B368ECEC4F9D4BF2364751A0479780713901F257198A579BA2047619A04204A83D7C7C73F04272701A4103EC5F2BCC97581499CC0F20FFAE72CC5215C9102A0CB348228AFDA69CFBC44F53E8204E62B10C2A97F85518CE125C87E85E4E81C1070168630CF3F804798F9DE198A01A5690ED1C2F700C6290184527CFA39877392A578395FD106806E1E57908E5B0094C36A27A7ED70D74D1DBF669B0ADA89355458E4244D06029EBCA9B814A8D3D7E2B5DF7091F2F11DE5377964BB2E7939F5CF2140BEA72E743A43191BD4CDE6A3122D86F9114379E5758D7DD5280DD52DF6F3CA9B158814199C6258908CCDFF54DCA238FC0F7CBC497F85788A0B8444E229F9B44F6AA04D9FB2740533F2780D17D5962E22DF0BE479813AB19926CCE11BBEC0E4CD6BDFFB481707B70836BA2130674ED20CFE1B62980102A34F8010986186014BEE6AAB2B6B511DCCFAD7EBC6B886795A64211C8BC304C7E68FC5F9B900D5DEC7A09C2529350835C6794AF5010EDF12154A03417FBFA106A61FE423B88F97A5782D1CF2BD6B88CA01F95DBCE2A6A5D4FBAFED88F7599A5CA7A89AD5747C9D97B2A244A5A6DE1B902D2171A7A896BD9DA276844251DD61A6A8E91D4A11D3683B35BC57A184359AA9287B4C144C82D67AF5DA342E904DD83536FEC5B639DA36CE76BE1ABD68A9F7E07B97E0E103C44B7237F5E9AF5413E20718D52D15059F714C9D0D3A8964055C53ECADD68F157B8DF4227657B1C7046D5AEEFA2A9FB23884836E0767DDE1366AACDE3094179D71D499770988D1F67506E4F96F69166D7DA1394064EB8B5CE434445AC4592231EF6D4A6F4F80077B4A15D68C2EBF75CAD9053FD6C97C0B10C0032D40A7239522B31355BA20BCB7755B9A46CD6D697B46B92D7CC1D177174579B1417BBCB706DD39B5A7916FE6F269E05E34E00906E36B06D1EB058A92BA182346F308A3F5330CDB580C29E3ABC1A4DEDB43E0E0F0F22CCFD3302EA912E2CB36EA97F7F70E475EE71300373575704CAD0D3D78F18A1E35BAF4D4FF5663980DB0DE8202C8033019F4C457CFE9153E870812E89D85FC597206F21044BA6E519E44720B3DDA3063670B20EA3FE4D458C498E87620C661BC02A88B746592A3F9604435F06ACF395C41CC4E7E970C5CD615DFC5F4F59B651466F5F1661208CAE4A063CDA9EC5409FD648ED431ED0C0B80ADA138481D5349DF958EA932705957BC36F6A263AD9B6B530783CFDBAA0237C5EEBAA5BBC9A25E95CEEFA1E99446F20EF449E3B9932E55E1D5FE6C55A90D9D6645BE9C47DA28E91AEF51CAFDEB9146F2AEEC92C87397356B77786FF6C8E09276D9932EFF54560A21D21A66B23A7CDB27712F76EF614706CD2EA72773532A31889BCAD86CDE8634F26959413BED3BD7C203B68B3C0AA573089D01B38A00F13987F5C107627832A26456AF4679F534A5EA11C39E4322DCDC54FDDAC057BA923525D427974B59007828DA03221C020DA4B5A93D204C3A26007E0CFA28A0EA615CBDF4481D56EEDA82D2AFA0091A20B355C82910C618930E5485EC7B806876D0485FD3E9BE270705A2D201D566C89B73DD782B73CBC6CD6E8143543C7CE3DA7D2F40D8853A7CE3C2E7067DD39620AD274C1328AD0E47C766F5C04CDC283F1E9B912E3F9016C9EAD76A4F0C315CA2D27DD9C7A33525697A54358BB5CFD71DE2ED2A9BB1AAE710F7768BEAAE3EE8F631A847373A9CAE0D3066535A53BF3037777BD3370978826BD530092C99B0934BB05AC5782964C6562DDE9CA7C5CEBE9B0FCF124D384610E68664D186DA662592666009955EF6393982EFE32C27EC6BD32D605F2A6651A20D337932964BB65E51745674A1D5576E3D9AFDAE3B4DDA87B3D2C7D17DBF0AE53DDD62C2BCC7F2FB926269F4691E4B51060864864F59B3141509B63BB1F6D9B5D72922D83C513B8A183D89485D51951D4D7C1917D1BA5ECCED68ED272F11AB6D7547AA7347459CBA6DC0EECAE451695F658B8E3009144DD122034D33B5A04A5673E743C01DAE8D1E04932BE97818CC53B77320B84B2CCE3705197B144F7BBD6C4C3C36DFC0413CF6A95B120F4FA790E4C39BDC31AA243F11A36A3A1821F37B7F630236F9360EC2354FDB8E60AB743311A06A1A20D826134F926DD3EA8EC453ED4414DEE28EA0E6D1499C51FADC51A57C3A1152EA187071571F75A44BDBF2A1C78ED264CD89304DE3C11C2A1E676ECE6A1A2266178B699CB67F6BB9474BB7852BAD13D6D1F475CCFFFB38E3C3DDE7AD2A9216E0AA439AD59B40570968275570D95FFFA9459B7C88EF5116DDC7118B34E78F398149A97D47F3FFA2198A218B06EA019700C70B98139EE0E8D360F87BA570F4708A38833C8F902138572B396559ED204333661CEDCDC11C9808292765964B689F3A2E70041FA6FEEFE59453EFE297AF7CD62BEF2AA3C23DF58EBD3F3690C9E9BA783B7314017A3DA62B01EDCC5104A839A8E5F2A3CA38172805C331C42ACE88FE4EFAAB3887D7073E8FD322D6DCE17B90857720FB67021EFE25226DA2AEEE99B04BCCF91FC02F1D49AA477351F361E568CF83DD52A5D238762BA55CA3C0C472AD5140E692ACDB78B8C93394638DA24C2EB972BEC8CA59A3EE10A54E6BA327C390AEFA544FC6BA8668CD02A3E7C1B4BFAF6B38DC33DB46458BE86CECAEE844A6618DECE1B5B3762D1F46B692ADFB9C2A53DADC875D27C9EE47573ABED2EC4F570E226FB6BBC284A73DEDB21044CEF258A3A2653DFD30BF476F25CDF539548C38C9E619D80D6779EDCA663CA50A9043B86494A4B2F573FC0FFEDAE9FB6A73A0F7CF80BA8D7D199CFDEAD0AE4CD070FDD97BD5859ED2A88A502F89B09653F00F54533FBA4DA98C79D466CC5BB6565A74165AD8E0CDE9F9B63A8CCE320CD312F6CC6053AD84B548C3046DCEAF35D56F58CB378C141B33DA475676D8E8DF67F187A83272BA5E5FA2BC36EB90CA3B54559553DDF6B1B5D1051CA20EB779287D19EA5A32FA2194693811B633A96CA918637D15349A95FEEA84C329B31826DBCD6F774011859E4B421D04E13F8E530F258F972D04FBFFE31886926BD08CB9C08BB4F650148AEA21CA83ED25242062994F1989172024B49BA54095FF63EC0B4005641FC76E617481AF0AB22A08DD324C6E9194B6C33C9DAEF5CB4A1199E6C9D5AAFC0F4A9BD8022533665FDFAFF0DB22465143F77BC363B30582B950D5FB3F932561DF01968F0DD2C7143B0255EC6B3CBF1B98AC1005CBAFF01CDCC37568A33AF8012E41F858A704D941FA0521B37D721E83650692BCC268E7D33FA90E47C9C38FFF07687245A9785F0000, N'6.1.3-40302')
/****** Object:  Table [dbo].[Users]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Salt] [nvarchar](max) NULL,
	[IsConfirmEmail] [bit] NOT NULL,
	[ConfirmCode] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
	[Balance] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[Users] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [Email], [Password], [Salt], [IsConfirmEmail], [ConfirmCode], [RoleId], [Balance]) VALUES (1, N'sergidka@gmail.com', N'cdTDUYmieQqXaL1wmKhI81Sfqz5bKPAwiz2sEcbjULs=', N'OdwbKZkPKODn+q2EVkzHOg==', 1, N'1c70da5d-0368-409a-a9b9-e98d65c5e30b', 1, 10000)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[UserResources]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserResources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserResources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ResourceId] ON [dbo].[UserResources] 
(
	[ResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserResources] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserResources] ON
INSERT [dbo].[UserResources] ([Id], [UserId], [ResourceId], [Quantity]) VALUES (1, 1, 1, 0)
INSERT [dbo].[UserResources] ([Id], [UserId], [ResourceId], [Quantity]) VALUES (2, 1, 2, 0)
INSERT [dbo].[UserResources] ([Id], [UserId], [ResourceId], [Quantity]) VALUES (3, 1, 3, 0)
SET IDENTITY_INSERT [dbo].[UserResources] OFF
/****** Object:  Table [dbo].[Deals]    Script Date: 03/20/2017 15:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[DealTypeId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Deals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_DealTypeId] ON [dbo].[Deals] 
(
	[DealTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ResourceId] ON [dbo].[Deals] 
(
	[ResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Deals] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_dbo.Users_dbo.Roles_RoleId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Roles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.UserResources_dbo.Resources_ResourceId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[UserResources]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserResources_dbo.Resources_ResourceId] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserResources] CHECK CONSTRAINT [FK_dbo.UserResources_dbo.Resources_ResourceId]
GO
/****** Object:  ForeignKey [FK_dbo.UserResources_dbo.Users_UserId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[UserResources]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserResources_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserResources] CHECK CONSTRAINT [FK_dbo.UserResources_dbo.Users_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.Deals_dbo.DealTypes_DealTypeId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[Deals]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Deals_dbo.DealTypes_DealTypeId] FOREIGN KEY([DealTypeId])
REFERENCES [dbo].[DealTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deals] CHECK CONSTRAINT [FK_dbo.Deals_dbo.DealTypes_DealTypeId]
GO
/****** Object:  ForeignKey [FK_dbo.Deals_dbo.Resources_ResourceId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[Deals]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Deals_dbo.Resources_ResourceId] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deals] CHECK CONSTRAINT [FK_dbo.Deals_dbo.Resources_ResourceId]
GO
/****** Object:  ForeignKey [FK_dbo.Deals_dbo.Users_UserId]    Script Date: 03/20/2017 15:00:45 ******/
ALTER TABLE [dbo].[Deals]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Deals_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deals] CHECK CONSTRAINT [FK_dbo.Deals_dbo.Users_UserId]
GO
