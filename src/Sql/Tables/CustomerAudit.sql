CREATE TABLE [dbo].[CustomerAudit]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Messages] NVARCHAR(MAX) NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Timestamp] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [CK_CustomerAudit_Messages_Json] CHECK (ISJSON([Messages]) = 1),
    CONSTRAINT [FK_CustomerAudit_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
