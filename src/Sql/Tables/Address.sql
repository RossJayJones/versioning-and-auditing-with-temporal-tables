﻿CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Line] NVARCHAR(255) NOT NULL, 
    [Suburb] NVARCHAR(255) NOT NULL, 
    [City] NVARCHAR(255) NOT NULL, 
    [Province] NVARCHAR(255) NOT NULL, 
    [Code] NVARCHAR(255) NOT NULL, 
    [CustomerId] INT NOT NULL,
	[SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [FK_Address_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]),
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[AddressHistory]))