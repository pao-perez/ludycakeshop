/*
	Add Order Item
*/
CREATE PROCEDURE CreateOrderItem(
							@ProductID INT = NULL,
							@ItemQuantity INT = NULL,
							@ItemTotal MONEY = NULL,
							@CustomerContactNumber VARCHAR = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @ProductID IS NULL
		RAISERROR('CreateOrderItem - required parameter: @ProductID',16,1)
	ELSE IF @ItemQuantity IS NULL
		RAISERROR('CreateOrderItem - required parameter: @ItemQuantity',16,1)
	ELSE IF @ItemTotal IS NULL
		RAISERROR('CreateOrderItem - required parameter: @ItemTotal',16,1)
	ELSE IF @CustomerContactNumber IS NULL
		RAISERROR('CreateOrderItem - required parameter: @ItemTotal',16,1)

		BEGIN
			INSERT INTO OrderItem(ProductID,ItemQuantity,ItemTotal,OrderNumber)
			VALUES (@ProductID,@ItemQuantity,@ItemTotal,(SELECT TOP 1 OrderNumber FROM Orders WHERE CustomerContactNumber=@CustomerContactNumber ORDER BY OrderDate DESC))

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateOrderItem - INSERT ERROR : OrderItem Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON CreateOrderItem TO aspnetcore

DROP PROCEDURE CreateOrderItem
