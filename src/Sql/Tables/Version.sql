CREATE TABLE [dbo].[Version]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Message] NVARCHAR(MAX) NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_Version_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
