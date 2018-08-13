USE [SmartSession]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_Goal_GoalStatus') AND parent_object_id = OBJECT_ID(N'[dbo].Goal'))
	ALTER TABLE [dbo].Goal DROP CONSTRAINT FK_Goal_GoalStatus

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_GoalTask_Goal') AND parent_object_id = OBJECT_ID(N'[dbo].GoalTask'))
	ALTER TABLE [dbo].GoalTask DROP CONSTRAINT FK_GoalTask_Goal

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_SessionTask_GoalTask') AND parent_object_id = OBJECT_ID(N'[dbo].SessionTask'))
	ALTER TABLE [dbo].SessionTask DROP CONSTRAINT FK_SessionTask_GoalTask

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_SessionTask_Session') AND parent_object_id = OBJECT_ID(N'[dbo].SessionTask'))
	ALTER TABLE [dbo].SessionTask DROP CONSTRAINT FK_SessionTask_Session


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.GoalStatus') AND type in (N'U'))
	DROP TABLE [dbo].[GoalStatus]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.SessionTask') AND type in (N'U'))
	DROP TABLE [dbo].[SessionTask]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.GoalTask') AND type in (N'U'))
	DROP TABLE [dbo].[GoalTask]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Goal') AND type in (N'U'))
	DROP TABLE [dbo].[Goal]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Session') AND type in (N'U'))
	DROP TABLE [dbo].[Session]

CREATE TABLE [dbo].[SessionTask](
	[SessionId] [int] NOT NULL,
	[GoalTaskId] [int] NOT NULL,
	[StartSpeed] [int] NOT NULL,
	[AttainedSpeed] [int] NOT NULL,
	[TimePracticed] [int] NOT NULL,
	[PercentComplete] [int] NOT NULL
 CONSTRAINT [PK_SessionTask] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC,
	[GoalTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Session](
	[Id] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Notes] [nchar](10) NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[GoalTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoalId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[Description] [nvarchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[GoalTaskType] [nchar](1) NULL,
	[DesiredSpeed] [int] NULL,
 CONSTRAINT [PK_GoalTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Goal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[TargetCompletionDate] [datetime] NULL,
	[Status] [nchar](3) NOT NULL,
 CONSTRAINT [PK_Goal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[GoalStatus](
	[Id] [nchar](3) NOT NULL,
	[Status] [nchar](50) NOT NULL,
 CONSTRAINT [PK_GoalStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

------------------------------------------------------------------------------------------------------------------------------------
-- Add Constraints
------------------------------------------------------------------------------------------------------------------------------------


ALTER TABLE [dbo].[Goal]  WITH CHECK ADD  CONSTRAINT [FK_Goal_GoalStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[GoalStatus] ([Id])
GO
ALTER TABLE [dbo].[Goal] CHECK CONSTRAINT [FK_Goal_GoalStatus]
GO

ALTER TABLE [dbo].[GoalTask]  WITH CHECK ADD  CONSTRAINT [FK_GoalTask_Goal] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal] ([Id])
GO
ALTER TABLE [dbo].[GoalTask] CHECK CONSTRAINT [FK_GoalTask_Goal]
GO

ALTER TABLE [dbo].[SessionTask]  WITH CHECK ADD  CONSTRAINT [FK_SessionTask_GoalTask] FOREIGN KEY([GoalTaskId])
REFERENCES [dbo].[GoalTask] ([Id])
GO

ALTER TABLE [dbo].[SessionTask] CHECK CONSTRAINT [FK_SessionTask_GoalTask]
GO

ALTER TABLE [dbo].[SessionTask]  WITH CHECK ADD  CONSTRAINT [FK_SessionTask_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO

ALTER TABLE [dbo].[SessionTask] CHECK CONSTRAINT [FK_SessionTask_Session]
GO
