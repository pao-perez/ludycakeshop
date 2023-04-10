/*
	Update Order Item
*/
CREATE PROCEDURE UpdateOrderItem(
							@OrderID VARCHAR(36) = NULL,
							@ProductID INT = NULL,
							@ItemQuantity INT = NULL,
							@ItemTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @OrderID IS NULL
		RAISERROR('UpdateOrderItem - required parameter: @OrderID',16,1)
	ELSE IF @ProductID IS NULL
		RAISERROR('UpdateOrderItem - required parameter: @ProductID',16,1)
	ELSE IF @ItemQuantity IS NULL
		RAISERROR('UpdateOrderItem - required parameter: @ItemQuantity',16,1)
	ELSE IF @ItemTotal IS NULL
		RAISERROR('UpdateOrderItem - required parameter: @ItemTotal',16,1)

		BEGIN
			INSERT INTO OrderItem(OrderID,ProductID,ItemQuantity,ItemTotal)
			VALUES (@OrderID,@ProductID,@ItemQuantity,@ItemTotal)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateOrderItem - UPDATE ERROR : OrderItem Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON UpdateOrderItem TO aspnetcore

DROP PROCEDURE UpdateOrderItem
