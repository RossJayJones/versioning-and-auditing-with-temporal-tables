CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Line] NVARCHAR(255) NOT NULL, 
    [Suburb] NVARCHAR(255) NOT NULL, 
    [City] NVARCHAR(255) NOT NULL, 
    [Province] NVARCHAR(255) NOT NULL, 
    [Code] NVARCHAR(255) NOT NULL, 
    [CustomerId] INT NOT NULL, 
    CONSTRAINT [FK_Address_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]),
)
