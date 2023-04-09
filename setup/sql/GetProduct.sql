/*
	Get Product
*/
CREATE PROCEDURE GetProduct(@ProductID INT = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ProductID IS NULL
		RAISERROR('GetProduct - Required parameter: @ProductID',16,1)
	ELSE
		BEGIN
			SELECT ProductID,ProductName,ProductDescription,QuantityAvailable,UnitPrice,Discontinued,QuantityPerUnit,CategoryID,ProductImageID
			FROM Product
			WHERE ProductID = @ProductID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetProduct - SELECT error: Product table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetProduct TO aspnetcore

DROP PROCEDURE GetProduct
