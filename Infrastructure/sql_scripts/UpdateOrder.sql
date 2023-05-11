/*
	Update Order
*/
CREATE PROCEDURE UpdateOrder(@OrderID VARCHAR(36) = NULL,
							@OrderStatus VARCHAR(20) = NULL,
							@InvoiceNumber BIGINT = NULL,
							@CustomerName VARCHAR(40) = NULL,
							@CustomerAddress VARCHAR(50) = NULL,
							@CustomerEmail VARCHAR(30) = NULL,
							@CustomerContactNumber VARCHAR(20) =NULL,
							@Note VARCHAR(255) = NULL,
							@GST MONEY = NULL,
							@SubTotal MONEY = NULL,
							@SaleTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @OrderID IS NULL
		RAISERROR('OrderID is required parameter: @OrderID',16,1)
	ELSE
		BEGIN
			UPDATE Orders
			SET InvoiceNumber = @InvoiceNumber,
				OrderStatus = @OrderStatus,
				CustomerName = @CustomerName,
				CustomerAddress = @CustomerAddress,
				CustomerEmail = @CustomerEmail,
				CustomerContactNumber = @CustomerContactNumber,
				Note = @Note,
				GST = @GST,
				SubTotal = @SubTotal,
				SaleTotal = @SaleTotal
			WHERE OrderID = @OrderID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateOrder - UPDATE ERROR : Orders Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON UpdateOrder TO aspnetcore
