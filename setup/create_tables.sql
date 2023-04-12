use ludycakeshop


CREATE TABLE Category
(
	CategoryID VARCHAR(36) NOT NULL CONSTRAINT PK_Category_CategoryID PRIMARY KEY,
	CategoryName VARCHAR(30) NOT NULL,
	CategoryDescription VARCHAR(255) NULL, 
	CategoryImage Image NULL
)

CREATE TABLE Product
(
	ProductID VARCHAR(36) NOT NULL CONSTRAINT PK_Product_ProductID PRIMARY KEY,
	ProductName VARCHAR(25) NOT NULL,
	ProductDescription VARCHAR(200) NULL,
	QuantityAvailable INT NOT NULL,
	UnitPrice MONEY NOT NULL,
	Discontinued BIT NOT NULL CONSTRAINT DK_Product_Discontinued DEFAULT(0),
	QuantityPerUnit VARCHAR(30) NULL,
	CategoryID VARCHAR(36) NOT NULL CONSTRAINT FK_Product_CategoryID REFERENCES Category(CategoryID),
	ProductImage IMAGE NULL
)

CREATE TABLE Orders
(
	OrderID VARCHAR(36) NOT NULL CONSTRAINT PK_Orders_OrderID PRIMARY KEY,
	OrderNumber INT NOT NULL CONSTRAINT DF_Orders_OrderNumber DEFAULT (FLOOR(RAND() * (1000000-100 + 1)) + 100),
	InvoiceNumber INT NULL,
	OrderDate DATETIME NOT NULL CONSTRAINT DK_Orders_OrderDate DEFAULT(GETDATE()),
	OrderStatus VARCHAR(20) NOT NULL CONSTRAINT CHK_Orders_OrderStatus CHECK (OrderStatus IN ('Submitted', 'Completed', 'For-Pickup', 'Preparing')),
	GST MONEY NOT NULL,
	SubTotal MONEY NOT NULL,
	SaleTotal MONEY NOT NULL,
	CustomerName VARCHAR(40) NOT NULL,
	CustomerAddress VARCHAR(50) NULL,
	CustomerEmail VARCHAR(30) NULL,
	CustomerContactNumber VARCHAR(20) NOT NULL,
	Note VARCHAR(255) NULL,
)

CREATE TABLE OrderItem
(	
	OrderID VARCHAR(36) NOT NULL CONSTRAINT FK_OrderItem_OrderID REFERENCES Orders(OrderID),
	ProductID VARCHAR(36) NOT NULL CONSTRAINT FK_OrderItem_ProductID REFERENCES Product(ProductID),
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
	('23165650-6f81-4c7e-b25f-5b77646ec7b2','Cakes')

 INSERT INTO Category
	(CategoryID,CategoryName)
 VALUES
	('34565650-6f81-4c7e-b25f-5b77646ec7a2','Breads')


INSERT INTO Product
	(ProductID,ProductName,QuantityAvailable,UnitPrice,CategoryID)
	VALUES
	('45265650-3f63-4c7e-b25f-5b77646eb3a2','Pandesal',40,4.56,'34565650-6f81-4c7e-b25f-5b77646ec7a2')
INSERT INTO Product
	(ProductID,ProductName,QuantityAvailable,UnitPrice,CategoryID)
	VALUES
	('23455450-5d63-5d7f-a25f-3b77646eb3c5','Carrot Cake',2,11,'23165650-6f81-4c7e-b25f-5b77646ec7b2')

