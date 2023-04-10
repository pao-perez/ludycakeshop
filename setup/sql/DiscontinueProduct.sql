/*
	Discontinue Product
*/
CREATE PROCEDURE DiscontinueProduct(
							@ProductID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1

	IF @ProductID IS NULL
		RAISERROR('DiscontinueProduct - required parameter: @ProductID',16,1)
	ELSE
		BEGIN
			UPDATE Product
			SET Discontinued = 1
			WHERE ProductID = @ProductID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('DiscontinueProduct - UPDATE ERROR : Product Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON DiscontinueProduct TO aspnetcore

DROP PROCEDURE DiscontinueProduct
