/*
	Get Products
*/
CREATE PROCEDURE GetProducts
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1


	BEGIN
		SELECT ProductID,ProductName,ProductDescription,QuantityAvailable,UnitPrice,Discontinued,QuantityPerUnit,CategoryID,ProductImage
		FROM Product
		WHERE Discontinued=0

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetProducts - SELECT error: Product table.',16,1)
	END

RETURN @ReturnCode


GRANT EXECUTE ON GetProducts TO aspnetcore

DROP PROCEDURE GetProducts
