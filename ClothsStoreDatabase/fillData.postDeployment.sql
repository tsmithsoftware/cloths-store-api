/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF (EXISTS(SELECT * FROM [dbo].CartItem))  
BEGIN  
    DELETE FROM [dbo].CartItem  
END  

IF (EXISTS(SELECT * FROM [dbo].Product))
BEGIN
    DELETE FROM [dbo].Product
END

IF (EXISTS(SELECT * FROM [dbo].[User]))
BEGIN
    DELETE FROM [dbo].[User]
END

BEGIN
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Shoes', 'Clothing', '£25', '£30', 15)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Shirt', 'Clothing', '£25', '£30', 40)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Jacket', 'Clothing', '£25', '£30', 30)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Trousers', 'Clothing', '£25', '£30', 12)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Laptop', 'Electronics', '£25', '£30', 10)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Phone', 'Electronics', '£25', '£30', 23)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Carpet', 'Furnishings', '£25', '£30', 13)
INSERT INTO [dbo].Product(name,category,price, oldPrice, stock) VALUES ('Chair', 'Furnishings', '£25', '£30', 5)
END

--909!
BEGIN
INSERT INTO [dbo].[User](Username, HashedPassword) VALUES ('admin','fUfIDYIiuZmO5RURMyPCPryTbfaudbfXiqKC+3bSAvg=')
END
