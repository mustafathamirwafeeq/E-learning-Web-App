
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2015 14:44:11
-- Generated from EDMX file: C:\Users\Agile pk\Dropbox\AgilePK\Muhammad Ali Rajak\Outsource Projects\GreenLightFeatures\GreenLightFeatures\Models\AppModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GreenLightFeatures];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseClassAttendance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendance] DROP CONSTRAINT [FK_CourseClassAttendance];
GO
IF OBJECT_ID(N'[dbo].[FK_Questions_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Question] DROP CONSTRAINT [FK_Questions_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCourses_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseClass] DROP CONSTRAINT [FK_UserCourses_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCourseTrainingBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseClass] DROP CONSTRAINT [FK_UserCourseTrainingBase];
GO
IF OBJECT_ID(N'[dbo].[FK_Grade_ToTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evaluation] DROP CONSTRAINT [FK_Grade_ToTest];
GO
IF OBJECT_ID(N'[dbo].[FK_Test_ToTestType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exam] DROP CONSTRAINT [FK_Test_ToTestType];
GO
IF OBJECT_ID(N'[dbo].[FK_TestQuestions_ToTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExamQuestion] DROP CONSTRAINT [FK_TestQuestions_ToTest];
GO
IF OBJECT_ID(N'[dbo].[FK_TestQuestions_ToQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExamQuestion] DROP CONSTRAINT [FK_TestQuestions_ToQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentCourseClass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attachment] DROP CONSTRAINT [FK_AttachmentCourseClass];
GO
IF OBJECT_ID(N'[dbo].[FK_ExamCourseClass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exam] DROP CONSTRAINT [FK_ExamCourseClass];
GO
IF OBJECT_ID(N'[dbo].[FK_EvaluationEvaluationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evaluation] DROP CONSTRAINT [FK_EvaluationEvaluationType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachment];
GO
IF OBJECT_ID(N'[dbo].[Attendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendance];
GO
IF OBJECT_ID(N'[dbo].[CMSPage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CMSPage];
GO
IF OBJECT_ID(N'[dbo].[Course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Course];
GO
IF OBJECT_ID(N'[dbo].[CourseClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseClass];
GO
IF OBJECT_ID(N'[dbo].[Evaluation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evaluation];
GO
IF OBJECT_ID(N'[dbo].[Exam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Exam];
GO
IF OBJECT_ID(N'[dbo].[ExamQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExamQuestion];
GO
IF OBJECT_ID(N'[dbo].[ExamType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExamType];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Question];
GO
IF OBJECT_ID(N'[dbo].[TrainingBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingBase];
GO
IF OBJECT_ID(N'[dbo].[EvaluationTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EvaluationTypeSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [FullName] nvarchar(max)  NULL,
    [DateOfBirth] datetime  NULL,
    [Address] nvarchar(max)  NULL,
    [Photo] nvarchar(max)  NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [UserId] int IDENTITY(1,1) NOT NULL,
    [IsAttendanceAuthorized] bit  NOT NULL,
    [Status] bit  NOT NULL,
    [CVPath] nvarchar(max)  NULL
);
GO

-- Creating table 'Attachment'
CREATE TABLE [dbo].[Attachment] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [FilePath] varchar(516)  NOT NULL,
    [PubDate] datetime  NOT NULL,
    [Order] int  NOT NULL,
    [CourseClassId] int  NOT NULL
);
GO

-- Creating table 'Attendance'
CREATE TABLE [dbo].[Attendance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StudentIds] nvarchar(max)  NOT NULL,
    [AttendanceDate] datetime  NOT NULL,
    [isHoliday] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [isAllPresent] bit  NOT NULL,
    [CourseClassId] int  NOT NULL,
    [IsInstructorPresent] bit  NOT NULL
);
GO

-- Creating table 'CMSPage'
CREATE TABLE [dbo].[CMSPage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Body] varchar(max)  NULL
);
GO

-- Creating table 'Course'
CREATE TABLE [dbo].[Course] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Description] varchar(max)  NULL,
    [IsPublished] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'CourseClass'
CREATE TABLE [dbo].[CourseClass] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TrainingBaseId] int  NOT NULL,
    [CourseId] int  NOT NULL,
    [InstructorId] int  NOT NULL,
    [Status] bit  NOT NULL,
    [AddedDate] datetime  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [StartTime] time  NOT NULL,
    [EndTime] time  NOT NULL,
    [StudentIds] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [ClassDates] nvarchar(max)  NULL
);
GO

-- Creating table 'Evaluation'
CREATE TABLE [dbo].[Evaluation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StudentId] int  NOT NULL,
    [ExamId] int  NOT NULL,
    [Grade] nvarchar(max)  NOT NULL,
    [ExamDate] datetime  NOT NULL,
    [EvaluationTypeId] int  NOT NULL,
    [IsStudentEvaluation] bit  NOT NULL,
    [Efforts] int  NULL,
    [Comments] nvarchar(max)  NULL,
    [A1] bit  NULL,
    [A2] bit  NULL,
    [B1] bit  NULL,
    [B2] bit  NULL,
    [C1] bit  NULL,
    [C2] bit  NULL
);
GO

-- Creating table 'Exam'
CREATE TABLE [dbo].[Exam] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(256)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [IsPublished] bit  NOT NULL,
    [ExamTypeId] int  NOT NULL,
    [MaxGrade] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [Duration] int  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CourseClassId] int  NOT NULL
);
GO

-- Creating table 'ExamQuestion'
CREATE TABLE [dbo].[ExamQuestion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ExamId] int  NOT NULL,
    [QuestionId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'ExamType'
CREATE TABLE [dbo].[ExamType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(128)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(516)  NULL,
    [PubDate] datetime  NULL,
    [Details] varchar(max)  NULL,
    [Audience] int  NOT NULL,
    [ClassId] int  NULL,
    [CreatedBy] int  NOT NULL
);
GO

-- Creating table 'Question'
CREATE TABLE [dbo].[Question] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionText] varchar(516)  NOT NULL,
    [Option1] varchar(516)  NOT NULL,
    [Option2] varchar(516)  NOT NULL,
    [Option3] varchar(516)  NULL,
    [Option4] varchar(516)  NULL,
    [CorrectAnswer] int  NOT NULL,
    [AnswerData] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [CourseId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'TrainingBase'
CREATE TABLE [dbo].[TrainingBase] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [ContactDetail] nvarchar(max)  NOT NULL,
    [Latitude] nvarchar(max)  NULL,
    [Longitude] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'EvaluationTypeSet'
CREATE TABLE [dbo].[EvaluationTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [A1] nvarchar(max)  NOT NULL,
    [A2] nvarchar(max)  NOT NULL,
    [B1] nvarchar(max)  NOT NULL,
    [B2] nvarchar(max)  NOT NULL,
    [C1] nvarchar(max)  NOT NULL,
    [C2] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attachment'
ALTER TABLE [dbo].[Attachment]
ADD CONSTRAINT [PK_Attachment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attendance'
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT [PK_Attendance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CMSPage'
ALTER TABLE [dbo].[CMSPage]
ADD CONSTRAINT [PK_CMSPage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Course'
ALTER TABLE [dbo].[Course]
ADD CONSTRAINT [PK_Course]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseClass'
ALTER TABLE [dbo].[CourseClass]
ADD CONSTRAINT [PK_CourseClass]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Evaluation'
ALTER TABLE [dbo].[Evaluation]
ADD CONSTRAINT [PK_Evaluation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Exam'
ALTER TABLE [dbo].[Exam]
ADD CONSTRAINT [PK_Exam]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExamQuestion'
ALTER TABLE [dbo].[ExamQuestion]
ADD CONSTRAINT [PK_ExamQuestion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExamType'
ALTER TABLE [dbo].[ExamType]
ADD CONSTRAINT [PK_ExamType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Question'
ALTER TABLE [dbo].[Question]
ADD CONSTRAINT [PK_Question]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainingBase'
ALTER TABLE [dbo].[TrainingBase]
ADD CONSTRAINT [PK_TrainingBase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EvaluationTypeSet'
ALTER TABLE [dbo].[EvaluationTypeSet]
ADD CONSTRAINT [PK_EvaluationTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetRoles'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetRoles]
ON [dbo].[AspNetUserRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([UserId]);
GO

-- Creating foreign key on [CourseClassId] in table 'Attendance'
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT [FK_CourseClassAttendance]
    FOREIGN KEY ([CourseClassId])
    REFERENCES [dbo].[CourseClass]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseClassAttendance'
CREATE INDEX [IX_FK_CourseClassAttendance]
ON [dbo].[Attendance]
    ([CourseClassId]);
GO

-- Creating foreign key on [CourseId] in table 'Question'
ALTER TABLE [dbo].[Question]
ADD CONSTRAINT [FK_Questions_Courses]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Course]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Questions_Courses'
CREATE INDEX [IX_FK_Questions_Courses]
ON [dbo].[Question]
    ([CourseId]);
GO

-- Creating foreign key on [CourseId] in table 'CourseClass'
ALTER TABLE [dbo].[CourseClass]
ADD CONSTRAINT [FK_UserCourses_Courses]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Course]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCourses_Courses'
CREATE INDEX [IX_FK_UserCourses_Courses]
ON [dbo].[CourseClass]
    ([CourseId]);
GO

-- Creating foreign key on [TrainingBaseId] in table 'CourseClass'
ALTER TABLE [dbo].[CourseClass]
ADD CONSTRAINT [FK_UserCourseTrainingBase]
    FOREIGN KEY ([TrainingBaseId])
    REFERENCES [dbo].[TrainingBase]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCourseTrainingBase'
CREATE INDEX [IX_FK_UserCourseTrainingBase]
ON [dbo].[CourseClass]
    ([TrainingBaseId]);
GO

-- Creating foreign key on [ExamId] in table 'Evaluation'
ALTER TABLE [dbo].[Evaluation]
ADD CONSTRAINT [FK_Grade_ToTest]
    FOREIGN KEY ([ExamId])
    REFERENCES [dbo].[Exam]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Grade_ToTest'
CREATE INDEX [IX_FK_Grade_ToTest]
ON [dbo].[Evaluation]
    ([ExamId]);
GO

-- Creating foreign key on [ExamTypeId] in table 'Exam'
ALTER TABLE [dbo].[Exam]
ADD CONSTRAINT [FK_Test_ToTestType]
    FOREIGN KEY ([ExamTypeId])
    REFERENCES [dbo].[ExamType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Test_ToTestType'
CREATE INDEX [IX_FK_Test_ToTestType]
ON [dbo].[Exam]
    ([ExamTypeId]);
GO

-- Creating foreign key on [ExamId] in table 'ExamQuestion'
ALTER TABLE [dbo].[ExamQuestion]
ADD CONSTRAINT [FK_TestQuestions_ToTest]
    FOREIGN KEY ([ExamId])
    REFERENCES [dbo].[Exam]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestions_ToTest'
CREATE INDEX [IX_FK_TestQuestions_ToTest]
ON [dbo].[ExamQuestion]
    ([ExamId]);
GO

-- Creating foreign key on [QuestionId] in table 'ExamQuestion'
ALTER TABLE [dbo].[ExamQuestion]
ADD CONSTRAINT [FK_TestQuestions_ToQuestion]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Question]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestions_ToQuestion'
CREATE INDEX [IX_FK_TestQuestions_ToQuestion]
ON [dbo].[ExamQuestion]
    ([QuestionId]);
GO

-- Creating foreign key on [CourseClassId] in table 'Attachment'
ALTER TABLE [dbo].[Attachment]
ADD CONSTRAINT [FK_AttachmentCourseClass]
    FOREIGN KEY ([CourseClassId])
    REFERENCES [dbo].[CourseClass]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttachmentCourseClass'
CREATE INDEX [IX_FK_AttachmentCourseClass]
ON [dbo].[Attachment]
    ([CourseClassId]);
GO

-- Creating foreign key on [CourseClassId] in table 'Exam'
ALTER TABLE [dbo].[Exam]
ADD CONSTRAINT [FK_ExamCourseClass]
    FOREIGN KEY ([CourseClassId])
    REFERENCES [dbo].[CourseClass]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExamCourseClass'
CREATE INDEX [IX_FK_ExamCourseClass]
ON [dbo].[Exam]
    ([CourseClassId]);
GO

-- Creating foreign key on [EvaluationTypeId] in table 'Evaluation'
ALTER TABLE [dbo].[Evaluation]
ADD CONSTRAINT [FK_EvaluationEvaluationType]
    FOREIGN KEY ([EvaluationTypeId])
    REFERENCES [dbo].[EvaluationTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EvaluationEvaluationType'
CREATE INDEX [IX_FK_EvaluationEvaluationType]
ON [dbo].[Evaluation]
    ([EvaluationTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------