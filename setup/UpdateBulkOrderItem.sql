/*
	Update Bulk Order Item
*/
CREATE PROCEDURE UpdateBulkOrderItem(
							@BulkOrderID VARCHAR(36) = NULL,
							@ProductID VARCHAR(36) = NULL,
							@ItemQuantity INT = NULL,
							@ItemTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @BulkOrderID IS NULL
		RAISERROR('UpdateBulkOrderItem - required parameter: @BulkOrderID',16,1)
	ELSE IF @ProductID IS NULL
		RAISERROR('UpdateBulkOrderItem - required parameter: @ProductID',16,1)
	ELSE IF @ItemQuantity IS NULL
		RAISERROR('UpdateBulkOrderItem - required parameter: @ItemQuantity',16,1)
	ELSE IF @ItemTotal IS NULL
		RAISERROR('UpdateBulkOrderItem - required parameter: @ItemTotal',16,1)

		BEGIN
			UPDATE BulkOrderItem
			SET ItemQuantity = @ItemQuantity,
				ItemTotal = @ItemTotal
			WHERE BulkOrderID = @BulkOrderID
			AND ProductID = @ProductID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateBulkOrderItem - UPDATE ERROR : BulkOrderItem Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON UpdateBulkOrderItem TO aspnetcore
