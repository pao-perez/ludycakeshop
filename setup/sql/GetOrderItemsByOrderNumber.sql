/*
	Get Order Items By OrderNumber
*/
CREATE PROCEDURE GetOrderItemsByOrderNumber(@OrderNumber INT = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	
	IF @OrderNumber IS NULL
		RAISERROR('OrderNumber is required parameter: @OrderNumber',16,1)
	ELSE
		BEGIN
			SELECT OrderNumber,ProductID,ItemQuantity,ItemTotal
			FROM OrderItem
			WHERE OrderItem.OrderNumber = @OrderNumber;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrderItemsByOrderNumber - SELECT error: OrderItems table.',16,1)
		END

RETURN @ReturnCode


GRANT EXECUTE ON GetOrderItemsByOrderNumber TO aspnetcore

DROP PROCEDURE GetOrderItemsByOrderNumber
