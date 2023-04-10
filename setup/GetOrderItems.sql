/*
	Get Order Items
*/
CREATE PROCEDURE GetOrderItems
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT 
			OrderID,
			ProductID,
			ItemQuantity,
			ItemTotal
		FROM OrderItem;
			
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetOrderItems - SELECT error: OrderItem table.',16,1)
	END

RETURN @ReturnCode

GRANT EXECUTE ON GetOrderItems TO aspnetcore

DROP PROCEDURE GetOrderItems
