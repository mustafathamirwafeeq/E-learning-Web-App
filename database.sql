
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[IsAttendanceAuthorized] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[CVPath] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[FilePath] [varchar](516) NOT NULL,
	[PubDate] [datetime] NOT NULL,
	[Order] [int] NOT NULL,
	[CourseClassId] [int] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentIds] [nvarchar](max) NOT NULL,
	[AttendanceDate] [datetime] NOT NULL,
	[isHoliday] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[isAllPresent] [bit] NOT NULL,
	[CourseClassId] [int] NOT NULL,
	[IsInstructorPresent] [bit] NOT NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CMSPage]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CMSPage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Body] [varchar](max) NULL,
 CONSTRAINT [PK_CMSPage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Description] [varchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseClass]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainingBaseId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[InstructorId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[StudentIds] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[ClassDates] [nvarchar](max) NULL,
 CONSTRAINT [PK_CourseClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Evaluation]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,
	[Grade] [nvarchar](max) NOT NULL,
	[ExamDate] [datetime] NOT NULL,
	[EvaluationTypeId] [int] NOT NULL,
	[IsStudentEvaluation] [bit] NOT NULL,
	[Efforts] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[A1] [bit] NULL,
	[A2] [bit] NULL,
	[B1] [bit] NULL,
	[B2] [bit] NULL,
	[C1] [bit] NULL,
	[C2] [bit] NULL,
 CONSTRAINT [PK_Evaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EvaluationTypeSet]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvaluationTypeSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[A1] [nvarchar](max) NOT NULL,
	[A2] [nvarchar](max) NOT NULL,
	[B1] [nvarchar](max) NOT NULL,
	[B2] [nvarchar](max) NOT NULL,
	[C1] [nvarchar](max) NOT NULL,
	[C2] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EvaluationTypeSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Exam]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](256) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[ExamTypeId] [int] NOT NULL,
	[MaxGrade] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CourseClassId] [int] NOT NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExamQuestion]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_ExamQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExamType]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExamType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](128) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ExamType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](516) NULL,
	[PubDate] [datetime] NULL,
	[Details] [varchar](max) NULL,
	[Audience] [int] NOT NULL,
	[ClassId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [varchar](516) NOT NULL,
	[Option1] [varchar](516) NOT NULL,
	[Option2] [varchar](516) NOT NULL,
	[Option3] [varchar](516) NULL,
	[Option4] [varchar](516) NULL,
	[CorrectAnswer] [int] NOT NULL,
	[AnswerData] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TrainingBase]    Script Date: 5/26/2018 5:45:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingBase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[ContactDetail] [nvarchar](max) NOT NULL,
	[Latitude] [nvarchar](max) NULL,
	[Longitude] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_TrainingBase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6eeef584-f10d-47f0-9cac-6efa52e874f2', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'89ac587d-07a2-4bc9-8dde-207d01856ae5', N'Management')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f87f3cc1-5134-4d1a-b460-7db30b41b74d', N'Moderator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'38911630-ccf7-400c-8e78-473f7737b893', N'Student')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7fee43fa-189d-4c93-8fba-541bd7258d5f', N'Visitor')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserRoles] ON 

GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (1, N'38911630-ccf7-400c-8e78-473f7737b893', N'2aed2c41-df94-4ef9-8f2e-1eeaaf381a94')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (2, N'38911630-ccf7-400c-8e78-473f7737b893', N'81a1700a-dfc8-4054-a5db-355d84247634')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (3, N'38911630-ccf7-400c-8e78-473f7737b893', N'd562f3ef-cd11-40a2-b280-22e658157e42')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (4, N'38911630-ccf7-400c-8e78-473f7737b893', N'40be7a1d-63c4-458a-9fe5-0d14a89aa14f')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (5, N'38911630-ccf7-400c-8e78-473f7737b893', N'50e84c2b-195c-4c58-9163-21a3149ef62a')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (6, N'f87f3cc1-5134-4d1a-b460-7db30b41b74d', N'5afc4503-ddf3-41ce-9c34-ee82311e72e7')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (7, N'f87f3cc1-5134-4d1a-b460-7db30b41b74d', N'ac158d8d-7fab-43e6-997c-0183e40b85e1')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (8, N'f87f3cc1-5134-4d1a-b460-7db30b41b74d', N'962870c6-d548-436f-9187-61050a2a3c94')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (9, N'6eeef584-f10d-47f0-9cac-6efa52e874f2', N'8826368c-4d2f-411f-a3ca-2d3f325f0bd4')
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [RoleId], [UserId]) VALUES (10, N'89ac587d-07a2-4bc9-8dde-207d01856ae5', N'e3032f67-93ce-4133-842d-22da193ea5a3')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 

GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'2aed2c41-df94-4ef9-8f2e-1eeaaf381a94', N'Student 1', NULL, NULL, N'/Images/Profiles/download.png', N'student1@email.com', 1, N'AONsqErkffln3gHdPJOeR/HWs8ZBvjnbpWFF4PEi8Alw9ypFIOZU/OtoijmkbnIOxg==', N'67992583-152c-4317-bfd0-bab6d7d75da4', NULL, 0, 0, NULL, 0, 0, N'student1', 1, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'40be7a1d-63c4-458a-9fe5-0d14a89aa14f', N'Student 4', NULL, NULL, N'/Images/Profiles/download.png', N'student4@email.com', 1, N'ACXEH5zCMmEwrsS5mtIped8QyFfumP261al6jE6cXX4Up8XNDBG3H49aJzT7FVYqeA==', N'bea8fd99-3dac-4ccc-be45-2bcc4a738dc5', NULL, 0, 0, NULL, 0, 0, N'student4', 4, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'50e84c2b-195c-4c58-9163-21a3149ef62a', N'Student 5', NULL, NULL, N'/Images/Profiles/download.png', N'student5@email.com', 1, N'ABV74OWh9LbtrnLt6V4lD5luzwHF13vF+yMueryvqkOT1bl68iUoKknlpKjaVM/imw==', N'1e50980e-8cc6-43a5-bce6-3f31118674b3', NULL, 0, 0, NULL, 0, 0, N'student5', 5, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'5afc4503-ddf3-41ce-9c34-ee82311e72e7', N'Instructor 1', NULL, NULL, N'/Images/Profiles/download.png', N'instructor1@email.com', 1, N'AKjHmTSA9jyW01Yc6xzqCT39rzZ4DCDWRq8tUhK+zUPHeOAKIleiWgtCf685k2LsSg==', N'48d173d8-0164-4712-942a-bee59063b2b8', NULL, 0, 0, NULL, 0, 0, N'instructor1', 6, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'81a1700a-dfc8-4054-a5db-355d84247634', N'Student 2', NULL, NULL, N'/Images/Profiles/download.png', N'student2@email.com', 1, N'ANw+Rk5mf4F5RlXM5I3O1KJVUrBS6rROAT/tbO3ECcSGelBlirGLF/RJUvzkYDKiLg==', N'ab73cd2f-be24-4f42-b109-0b948f976944', NULL, 0, 0, NULL, 0, 0, N'student2', 2, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'8826368c-4d2f-411f-a3ca-2d3f325f0bd4', N'admin', NULL, NULL, N'/Images/Profiles/download.png', N'admin@email.com', 1, N'AHhslsGKDVrgDJF5UskgiKBHzZcaHSzS1C32D42LQaq7SJNDu+G0/UTmN+fd4eLUNw==', N'6b8f1263-83ad-4548-8ce8-497212ede09b', NULL, 0, 0, NULL, 0, 0, N'admin', 9, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'962870c6-d548-436f-9187-61050a2a3c94', N'Instructor 3', NULL, NULL, N'/Images/Profiles/download.png', N'instructor3@email.com', 1, N'AGQ7ZUF0i67g0CbWRDQjGChccmF8Ac60LLAm2GDHXPK4sk64T0UIrSxkjEDR0aDIgA==', N'92bbc235-6145-4f50-8782-69f2d2d63ffa', NULL, 0, 0, NULL, 0, 0, N'instructor3', 8, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'ac158d8d-7fab-43e6-997c-0183e40b85e1', N'Instructor 2', NULL, NULL, N'/Images/Profiles/download.png', N'instructor2@email.com', 1, N'ABJdoylB3UD3BxHsIutgFuHvMX3l2jRPeI6VeZ7+SEvQnX0CoJHjXt0e1kCypq84gA==', N'dc4ec4bc-6b17-4837-8d37-173ea86e19b9', NULL, 0, 0, NULL, 0, 0, N'instructor2', 7, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'd562f3ef-cd11-40a2-b280-22e658157e42', N'Student 3', NULL, NULL, N'/Images/Profiles/download.png', N'student3@email.com', 1, N'AGhbRKHtCoHYziNUeyLGhHsj1tYEOj+ZB3vX1JdQfiH8aMGANfnWKfKx8ai7MmRY8g==', N'23b22b85-70c1-4f3e-b729-acbd4cd002a5', NULL, 0, 0, NULL, 0, 0, N'student3', 3, 0, 1, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [DateOfBirth], [Address], [Photo], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserId], [IsAttendanceAuthorized], [Status], [CVPath]) VALUES (N'e3032f67-93ce-4133-842d-22da193ea5a3', N'manager', NULL, NULL, NULL, N'manager1@email.com', 1, N'AJqiwtvlaowJ0zMpce0AJcfiNazebDlUv7vrOI43ZQHVd2GZ6/lKgIewhNL6ZrLu5w==', N'623f73c7-0eaa-4411-a43e-d84f5aae4de2', NULL, 0, 0, NULL, 0, 0, N'manager1', 10, 0, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Attachment] ON 

GO
INSERT [dbo].[Attachment] ([Id], [Title], [FilePath], [PubDate], [Order], [CourseClassId]) VALUES (1, N'product 1', N'/CourseAttachments/ad screen.PNG', CAST(N'2018-05-24 20:56:00.000' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Attachment] OFF
GO
SET IDENTITY_INSERT [dbo].[CMSPage] ON 

GO
INSERT [dbo].[CMSPage] ([Id], [Title], [Body]) VALUES (1, N'Home/Welcome', N'Welcome text')
GO
INSERT [dbo].[CMSPage] ([Id], [Title], [Body]) VALUES (2, N'About Us', N'Aboutus text')
GO
INSERT [dbo].[CMSPage] ([Id], [Title], [Body]) VALUES (3, N'Contact Us', N'Contactus text')
GO
SET IDENTITY_INSERT [dbo].[CMSPage] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

GO
INSERT [dbo].[Course] ([Id], [Title], [Description], [IsPublished], [IsDeleted]) VALUES (1, N'course1', N'', 1, 0)
GO
INSERT [dbo].[Course] ([Id], [Title], [Description], [IsPublished], [IsDeleted]) VALUES (2, N'course2', N'', 1, 0)
GO
INSERT [dbo].[Course] ([Id], [Title], [Description], [IsPublished], [IsDeleted]) VALUES (3, N'course3', N'', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseClass] ON 

GO
INSERT [dbo].[CourseClass] ([Id], [TrainingBaseId], [CourseId], [InstructorId], [Status], [AddedDate], [StartDate], [EndDate], [StartTime], [EndTime], [StudentIds], [Title], [ClassDates]) VALUES (1, 1, 1, 6, 1, CAST(N'2018-05-25 20:11:58.000' AS DateTime), CAST(N'2018-05-23 00:00:00.000' AS DateTime), CAST(N'2018-05-30 00:00:00.000' AS DateTime), CAST(N'20:11:00' AS Time), CAST(N'22:11:00' AS Time), N'1,2', N'class 1', N'5/23/2018 12:00:00 AM,5/24/2018 12:00:00 AM,5/27/2018 12:00:00 AM,5/28/2018 12:00:00 AM,5/29/2018 12:00:00 AM,5/30/2018 12:00:00 AM')
GO
SET IDENTITY_INSERT [dbo].[CourseClass] OFF
GO
SET IDENTITY_INSERT [dbo].[Evaluation] ON 

GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (1, 1, 1, N'0', CAST(N'2018-05-25 22:34:12.467' AS DateTime), 1, 1, 3, N'comments', 1, 0, 0, 1, 0, 0)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (2, 2, 1, N'0', CAST(N'2018-05-25 22:32:10.963' AS DateTime), 1, 1, 3, N'ttes comments', 1, 1, 1, 0, 0, 0)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (3, 1, 1, N'0', CAST(N'2018-05-25 22:34:12.487' AS DateTime), 2, 1, 3, N'comments', 1, 1, 0, 0, 0, 0)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (4, 2, 1, N'0', CAST(N'2018-05-25 22:32:10.997' AS DateTime), 2, 1, 3, N'ttes comments', 0, 0, 0, 0, 1, 1)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (5, 1, 1, N'20', CAST(N'2018-05-25 22:33:56.153' AS DateTime), 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (6, 1, 1, N'25', CAST(N'2018-05-25 22:33:56.177' AS DateTime), 2, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (7, 2, 1, N'100', CAST(N'2018-05-25 22:33:57.913' AS DateTime), 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Evaluation] ([Id], [StudentId], [ExamId], [Grade], [ExamDate], [EvaluationTypeId], [IsStudentEvaluation], [Efforts], [Comments], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (8, 2, 1, N'100', CAST(N'2018-05-25 22:33:57.933' AS DateTime), 2, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Evaluation] OFF
GO
SET IDENTITY_INSERT [dbo].[EvaluationTypeSet] ON 

GO
INSERT [dbo].[EvaluationTypeSet] ([Id], [Name], [Description], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (1, N'English', N'English', N'20', N'20', N'20', N'20', N'20', N'20')
GO
INSERT [dbo].[EvaluationTypeSet] ([Id], [Name], [Description], [A1], [A2], [B1], [B2], [C1], [C2]) VALUES (2, N'Math', N'Math', N'50', N'50', N'50', N'50', N'50', N'50')
GO
SET IDENTITY_INSERT [dbo].[EvaluationTypeSet] OFF
GO
SET IDENTITY_INSERT [dbo].[Exam] ON 

GO
INSERT [dbo].[Exam] ([Id], [Title], [CreationDate], [IsPublished], [ExamTypeId], [MaxGrade], [UserId], [Duration], [IsDeleted], [CourseClassId]) VALUES (1, N'Quiz 1', CAST(N'2018-05-25 22:24:31.000' AS DateTime), 1, 1, N'N/A', 6, 0, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Exam] OFF
GO
SET IDENTITY_INSERT [dbo].[ExamType] ON 

GO
INSERT [dbo].[ExamType] ([Id], [Type], [IsDeleted]) VALUES (1, N'Quiz', 0)
GO
SET IDENTITY_INSERT [dbo].[ExamType] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

GO
INSERT [dbo].[News] ([Id], [Title], [PubDate], [Details], [Audience], [ClassId], [CreatedBy]) VALUES (1, N'test', CAST(N'2018-05-25 20:05:00.000' AS DateTime), N'test', 1, NULL, 9)
GO
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[TrainingBase] ON 

GO
INSERT [dbo].[TrainingBase] ([Id], [Name], [Address], [ContactDetail], [Latitude], [Longitude], [Description], [CreateDate]) VALUES (1, N'Base1', N'test address', N'contact person', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TrainingBase] ([Id], [Name], [Address], [ContactDetail], [Latitude], [Longitude], [Description], [CreateDate]) VALUES (2, N'Base2', N'test address', N'contact person', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TrainingBase] ([Id], [Name], [Address], [ContactDetail], [Latitude], [Longitude], [Description], [CreateDate]) VALUES (3, N'Base3', N'test address', N'contact person', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TrainingBase] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_AspNetUserRoles_AspNetRoles]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_AspNetUserRoles_AspNetRoles] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_AspNetUserRoles_AspNetUsers]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_AspNetUserRoles_AspNetUsers] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_AttachmentCourseClass]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_AttachmentCourseClass] ON [dbo].[Attachment]
(
	[CourseClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_CourseClassAttendance]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_CourseClassAttendance] ON [dbo].[Attendance]
(
	[CourseClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_UserCourses_Courses]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserCourses_Courses] ON [dbo].[CourseClass]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_UserCourseTrainingBase]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserCourseTrainingBase] ON [dbo].[CourseClass]
(
	[TrainingBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_EvaluationEvaluationType]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EvaluationEvaluationType] ON [dbo].[Evaluation]
(
	[EvaluationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Grade_ToTest]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Grade_ToTest] ON [dbo].[Evaluation]
(
	[ExamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_ExamCourseClass]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_ExamCourseClass] ON [dbo].[Exam]
(
	[CourseClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Test_ToTestType]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Test_ToTestType] ON [dbo].[Exam]
(
	[ExamTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TestQuestions_ToQuestion]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_TestQuestions_ToQuestion] ON [dbo].[ExamQuestion]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TestQuestions_ToTest]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_TestQuestions_ToTest] ON [dbo].[ExamQuestion]
(
	[ExamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Questions_Courses]    Script Date: 5/26/2018 5:45:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Questions_Courses] ON [dbo].[Question]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_AttachmentCourseClass] FOREIGN KEY([CourseClassId])
REFERENCES [dbo].[CourseClass] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_AttachmentCourseClass]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_CourseClassAttendance] FOREIGN KEY([CourseClassId])
REFERENCES [dbo].[CourseClass] ([Id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_CourseClassAttendance]
GO
ALTER TABLE [dbo].[CourseClass]  WITH CHECK ADD  CONSTRAINT [FK_UserCourses_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[CourseClass] CHECK CONSTRAINT [FK_UserCourses_Courses]
GO
ALTER TABLE [dbo].[CourseClass]  WITH CHECK ADD  CONSTRAINT [FK_UserCourseTrainingBase] FOREIGN KEY([TrainingBaseId])
REFERENCES [dbo].[TrainingBase] ([Id])
GO
ALTER TABLE [dbo].[CourseClass] CHECK CONSTRAINT [FK_UserCourseTrainingBase]
GO
ALTER TABLE [dbo].[Evaluation]  WITH CHECK ADD  CONSTRAINT [FK_EvaluationEvaluationType] FOREIGN KEY([EvaluationTypeId])
REFERENCES [dbo].[EvaluationTypeSet] ([Id])
GO
ALTER TABLE [dbo].[Evaluation] CHECK CONSTRAINT [FK_EvaluationEvaluationType]
GO
ALTER TABLE [dbo].[Evaluation]  WITH CHECK ADD  CONSTRAINT [FK_Grade_ToTest] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exam] ([Id])
GO
ALTER TABLE [dbo].[Evaluation] CHECK CONSTRAINT [FK_Grade_ToTest]
GO
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_ExamCourseClass] FOREIGN KEY([CourseClassId])
REFERENCES [dbo].[CourseClass] ([Id])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_ExamCourseClass]
GO
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Test_ToTestType] FOREIGN KEY([ExamTypeId])
REFERENCES [dbo].[ExamType] ([Id])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Test_ToTestType]
GO
ALTER TABLE [dbo].[ExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestions_ToQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
GO
ALTER TABLE [dbo].[ExamQuestion] CHECK CONSTRAINT [FK_TestQuestions_ToQuestion]
GO
ALTER TABLE [dbo].[ExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestions_ToTest] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exam] ([Id])
GO
ALTER TABLE [dbo].[ExamQuestion] CHECK CONSTRAINT [FK_TestQuestions_ToTest]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Questions_Courses]
GO
