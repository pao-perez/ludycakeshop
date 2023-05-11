/*
	Get Bulk Order Items
*/
CREATE PROCEDURE GetBulkOrderItems
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT 
			BulkOrderID,
			ProductID,
			ItemQuantity,
			ItemTotal
		FROM BulkOrderItem;
			
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetBulkOrderItems - SELECT error: BulkOrderItem table.',16,1)
	END

RETURN @ReturnCode

GRANT EXECUTE ON GetBulkOrderItems TO aspnetcore
