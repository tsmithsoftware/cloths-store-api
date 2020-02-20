CREATE TABLE [dbo].[CartItem]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [productId] INT NOT NULL, 
    [userId] INT NOT NULL, 
    CONSTRAINT [FK_User_CartItem] FOREIGN KEY (userId) REFERENCES dbo.[User](id)
)

GO

ALTER TABLE [dbo].[CartItem]
ADD CONSTRAINT FK_Product_CartItem
FOREIGN KEY (productId) REFERENCES Product(id)
