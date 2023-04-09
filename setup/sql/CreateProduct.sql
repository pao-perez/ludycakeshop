/*
	Create Product
*/
CREATE PROCEDURE CreateProduct(@ProductName VARCHAR(25) = NULL,
							@ProductDescription VARCHAR(200) = NULL,
							@QuantityAvailable INT = NULL,
							@UnitPrice MONEY =NULL,
							@Discontinued BIT = NULL,
							@QuantityPerUnit VARCHAR(30) = NULL,
							@CategoryID INT = NULL,
							@ProductImageID INT = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @ProductName IS NULL
		RAISERROR('ProductName is required parameter: @ProductName',16,1)
	ELSE IF @QuantityAvailable IS NULL
		RAISERROR('QuantityAvailable is required parameter: @QuantityAvailable',16,1)
	ELSE IF @UnitPrice IS NULL
		RAISERROR('UnitPrice is required parameter: @UnitPrice',16,1)
	ELSE IF @Discontinued IS NULL
		RAISERROR('Discontinued is required parameter: @Discontinued',16,1)
	ELSE IF @CategoryID IS NULL
		RAISERROR('CategoryID is required parameter: @CategoryID',16,1)
	ELSE IF @ProductImageID IS NULL
		RAISERROR('ProductImageID is required parameter: @ProductImageID',16,1)
	ELSE
		BEGIN
			INSERT INTO Product(ProductName,ProductDescription,QuantityAvailable,UnitPrice,Discontinued,QuantityPerUnit,CategoryID,ProductImageID)
			VALUES (@ProductName,@ProductDescription,@QuantityAvailable,@UnitPrice,@Discontinued,@QuantityPerUnit,@CategoryID,@ProductImageID)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateProduct - INSERT ERROR : Product Table',16,1)
		END


GRANT EXECUTE ON CreateProduct TO aspnetcore

DROP PROCEDURE CreateProduct
