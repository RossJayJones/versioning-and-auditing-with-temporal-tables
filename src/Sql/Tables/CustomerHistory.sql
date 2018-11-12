CREATE TABLE [dbo].[CustomerHistory]
(
	[Id] INT NOT NULL, 
    [Name] NVARCHAR(150) NOT NULL,
	[SysStartTime] DATETIME2 NOT NULL,
	[SysEndTime] DATETIME2 NOT NULL
)
GO
CREATE CLUSTERED COLUMNSTORE INDEX IX_CustomerHistory ON [dbo].[CustomerHistory];
GO
CREATE NONCLUSTERED INDEX IX_CustomerHistory_ID_PERIOD_COLUMNS ON [dbo].[CustomerHistory] ([SysEndTime], [SysStartTime], [Id]);
GO
