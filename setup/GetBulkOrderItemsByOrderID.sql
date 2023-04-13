/*
	Get Bulk Order Items By OrderID
*/
CREATE PROCEDURE GetBulkOrderItemsByOrderID(@OrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	
	IF @OrderID IS NULL
		RAISERROR('GetBulkOrderItemsByOrderID - required parameter: @OrderID',16,1)
	ELSE
		BEGIN
			SELECT OrderID,ProductID,ItemQuantity,ItemTotal
			FROM BulkOrderItem
			WHERE OrderID = @OrderID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetBulkOrderItemsByOrderID - SELECT error: BulkOrderItem table.',16,1)
		END

RETURN @ReturnCode


GRANT EXECUTE ON GetBulkOrderItemsByOrderID TO aspnetcore

DROP PROCEDURE GetBulkOrderItemsByOrderID
