use ludycakeshop

CREATE TABLE Category
(
	CategoryID INT NOT NULL CONSTRAINT PK_Category_CategoryID PRIMARY KEY,
	CategoryName VARCHAR(30) NOT NULL,
	Description VARCHAR(255) NULL, 
	CategoryImage Image Null
)


CREATE TABLE Product
(
	ProductID INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_Product_ProductID PRIMARY KEY,
	ProductName VARCHAR(25) NOT NULL,
	ProductDescription VARCHAR(200) NULL,
	QuantityAvailable INT NOT NULL,
	UnitPrice MONEY NOT NULL,
	Discontinued BIT NOT NULL CONSTRAINT DK_Product_Discontinued DEFAULT(0),
	QuantityPerUnit VARCHAR(30) NULL,
	CategoryID INT NOT NULL CONSTRAINT FK_Product_CategoryID REFERENCES Category(CategoryID),
	ProductImageID INT NULL CONSTRAINT FK_Product_ProductImageID REFERENCES ProductImage(ProductImageID),
)


CREATE TABLE ProductImage
(
	ProductImageID INT NOT NULL CONSTRAINT PK_ProductImage_ProductImageID PRIMARY KEY,
	ProductImage IMAGE NOT NULL,
	DefaultImage IMAGE NULL
)


CREATE TABLE Orders
(
	OrderID VARCHAR(36) NOT NULL CONSTRAINT PK_Orders_OrderID PRIMARY KEY,
	OrderNumber INT NOT NULL CONSTRAINT DF_Orders_OrderNumber DEFAULT (FLOOR(RAND() * (1000000-100 + 1)) + 100),
	InvoiceNumber INT NULL,
	OrderDate DATETIME NOT NULL CONSTRAINT DK_Orders_OrderDate DEFAULT(GETDATE()),
	OrderStatus VARCHAR(20) NOT NULL CONSTRAINT DK_Orders_OrderStatus DEFAULT 'Submitted',
	GST MONEY NOT NULL,
	SubTotal MONEY NOT NULL,
	SaleTotal MONEY NOT NULL,
	CustomerName VARCHAR(40) NOT NULL,
	CustomerAddress VARCHAR(50) NULL,
	CustomerEmail VARCHAR(30) NULL,
	CustomerContactNumber VARCHAR(20) NOT NULL,
	Note VARCHAR(255) NULL,
)
--Add types of Order status


CREATE TABLE OrderItem
(	
	OrderID VARCHAR(36) NOT NULL CONSTRAINT FK_OrderItem_OrderID REFERENCES Orders(OrderID),
	ProductID INT NOT NULL CONSTRAINT FK_OrderItem_ProductID REFERENCES Product(ProductID),
	ItemQuantity INT NOT NULL,
	ItemTotal MONEY NOT NULL,
	CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ProductID)
)


CREATE TABLE UserAccount
(
	UserAccountID INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_UserAccount_UserAccountID PRIMARY KEY,
	Username VARCHAR(20) NOT NULL,
	Password VARCHAR(60) NOT NULL,
)

 INSERT INTO Category
	(CategoryID,CategoryName)
 VALUES
	(1,'Cakes')

 INSERT INTO Category
	(CategoryID,CategoryName)
 VALUES
	(2,'Breads')


INSERT INTO Product
	(ProductName,QuantityAvailable,UnitPrice,CategoryID)
	VALUES
	('Pandesal',40,4.56,2)
INSERT INTO Product
	(ProductName,QuantityAvailable,UnitPrice,CategoryID)
	VALUES
	('Carrot Cake',2,11,1)

