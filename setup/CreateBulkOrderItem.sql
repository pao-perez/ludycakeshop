/*
	Create Bulk Order Item
*/
CREATE PROCEDURE CreateBulkOrderItem(
							@BulkOrderID VARCHAR(36) = NULL,
							@ProductID VARCHAR(36) = NULL,
							@ItemQuantity INT = NULL,
							@ItemTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @BulkOrderID IS NULL
		RAISERROR('CreateBulkOrderItem - required parameter: @BulkOrderID',16,1)
	ELSE IF @ProductID IS NULL
		RAISERROR('CreateBulkOrderItem - required parameter: @ProductID',16,1)
	ELSE IF @ItemQuantity IS NULL
		RAISERROR('CreateBulkOrderItem - required parameter: @ItemQuantity',16,1)
	ELSE IF @ItemTotal IS NULL
		RAISERROR('CreateBulkOrderItem - required parameter: @ItemTotal',16,1)

		BEGIN
			INSERT INTO BulkOrderItem(BulkOrderID,ProductID,ItemQuantity,ItemTotal)
			VALUES (@BulkOrderID,@ProductID,@ItemQuantity,@ItemTotal)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateBulkOrderItem - INSERT ERROR : BulkOrderItem Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON CreateBulkOrderItem TO aspnetcore

DROP PROCEDURE CreateBulkOrderItem
