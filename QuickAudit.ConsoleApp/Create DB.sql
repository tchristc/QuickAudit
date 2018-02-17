USE [QuickAudit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AuditEntry] (
    [AuditEntryId] [int] NOT NULL IDENTITY,
    [EntityTypeName] [varchar](255) NOT NULL,
    [State] [int] NOT NULL,
    [StateName] [varchar](255) NOT NULL,
    [CreatedBy] [varchar](255) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_AuditEntry] PRIMARY KEY ([AuditEntryId])
)
GO

CREATE TABLE [dbo].[AuditEntryProperty] (
    [AuditEntryPropertyId] [int] NOT NULL IDENTITY,
    [AuditEntryId] [int] NOT NULL,
    --[RelationName] [nvarchar](255),
    [PropertyName] [nvarchar](255),
    [OldValue] [nvarchar](max),
    [NewValue] [nvarchar](max),
    CONSTRAINT [PK_AuditEntryProperty] PRIMARY KEY ([AuditEntryPropertyId])
)
GO

CREATE INDEX [IX_AuditEntryProperty_AuditEntryID] ON [dbo].[AuditEntryProperty]([AuditEntryId])
GO

ALTER TABLE [dbo].[AuditEntryProperty] 
ADD CONSTRAINT [FK_AuditEntryProperty_AuditEntry_AuditEntryId] 
FOREIGN KEY ([AuditEntryId])
REFERENCES [dbo].[AuditEntry] ([AuditEntryId])
ON DELETE CASCADE
GO