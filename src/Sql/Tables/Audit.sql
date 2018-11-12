CREATE TABLE [dbo].[Audit]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Messages] NVARCHAR(MAX) NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [CK_Audit_Messages_Json] CHECK (ISJSON([Messages]) = 1),
    CONSTRAINT [FK_Audit_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
