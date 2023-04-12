/*
	Get Bulk Order Items By BulkOrderID
*/
CREATE PROCEDURE GetBulkOrderItemsByBulkOrderID(@BulkOrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
	
	IF @BulkOrderID IS NULL
		RAISERROR('GetBulkOrderItemsByBulkOrderID - required parameter: @BulkOrderID',16,1)
	ELSE
		BEGIN
			SELECT BulkOrderID,ProductID,ItemQuantity,ItemTotal
			FROM BulkOrderItem
			WHERE BulkOrderID = @BulkOrderID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetBulkOrderItemsByBulkOrderID - SELECT error: BulkOrderItem table.',16,1)
		END

RETURN @ReturnCode


GRANT EXECUTE ON GetBulkOrderItemsByBulkOrderID TO aspnetcore

DROP PROCEDURE GetBulkOrderItemsByBulkOrderID
