CREATE TABLE [dbo].[CartItem]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [productId] INT NOT NULL
)

GO

ALTER TABLE [dbo].[CartItem]
ADD CONSTRAINT FK_Product_CartItem
FOREIGN KEY (productId) REFERENCES Product(id)
