CREATE TABLE [dbo].[AddressHistory]
(
	[Id] INT NOT NULL, 
    [Line] NVARCHAR(255) NOT NULL, 
    [Suburb] NVARCHAR(255) NOT NULL, 
    [City] NVARCHAR(255) NOT NULL, 
    [Province] NVARCHAR(255) NOT NULL, 
    [Code] NVARCHAR(255) NOT NULL, 
    [CustomerId] INT NOT NULL,
	[SysStartTime] DATETIME2 NOT NULL,
	[SysEndTime] DATETIME2 NOT NULL
)
GO
CREATE CLUSTERED COLUMNSTORE INDEX IX_AddressHistory ON [dbo].[AddressHistory];
GO
CREATE NONCLUSTERED INDEX IX_AddressHistory_ID_PERIOD_COLUMNS ON [dbo].[AddressHistory] ([SysEndTime], [SysStartTime], [Id]);
GO