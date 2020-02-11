CREATE TABLE [dbo].[Product]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(255) NULL, 
    [category] VARCHAR(255) NULL, 
    [price] VARCHAR(255) NULL, 
    [oldPrice] VARCHAR(255) NULL, 
    [stock] INT NULL
)
