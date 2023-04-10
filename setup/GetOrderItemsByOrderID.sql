/*
	Get Order Items By OrderID
*/
CREATE PROCEDURE GetOrderItemsByOrderID(@OrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	
	IF @OrderID IS NULL
		RAISERROR('OrderID is required parameter: @OrderID',16,1)
	ELSE
		BEGIN
			SELECT OrderID,ProductID,ItemQuantity,ItemTotal
			FROM OrderItem
			WHERE OrderItem.OrderID = @OrderID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrderItemsByOrderID - SELECT error: OrderItems table.',16,1)
		END

RETURN @ReturnCode


GRANT EXECUTE ON GetOrderItemsByOrderID TO aspnetcore

DROP PROCEDURE GetOrderItemsByOrderID
