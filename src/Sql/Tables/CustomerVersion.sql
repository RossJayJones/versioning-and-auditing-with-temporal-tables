CREATE TABLE [dbo].[CustomerVersion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Message] NVARCHAR(MAX) NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Timestamp] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [FK_CustomerVersion_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
