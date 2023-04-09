/*
	Create Order
*/
CREATE PROCEDURE CreateOrder(@InvoiceNumber BIGINT = NULL,
							@CustomerName VARCHAR(40) = NULL,
							@CustomerAddress VARCHAR(50) = NULL,
							@CustomerEmail VARCHAR(30) = NULL,
							@CustomerContactNumber VARCHAR(20) =NULL,
							@Note VARCHAR(255) = NULL,
							@OrderStatus VARCHAR(20) = NULL,
							@GST MONEY = NULL,
							@SubTotal MONEY = NULL,
							@SaleTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @CustomerName IS NULL
		RAISERROR('CustomerName is required parameter: @CustomerName',16,1)
	ELSE IF @CustomerContactNumber IS NULL
		RAISERROR('CustomerContactNumber is required parameter: @CustomerContactNumber',16,1)
	ELSE IF @OrderStatus IS NULL
		RAISERROR('OrderStatus is required parameter: @OrderStatus',16,1)
	ELSE IF @GST IS NULL
		RAISERROR('GST is required parameter: @GST',16,1)
	ELSE IF @SubTotal IS NULL
		RAISERROR('SubTotal is required parameter: @SubTotal',16,1)
	ELSE IF @SaleTotal IS NULL
		RAISERROR('SaleTotal is required parameter: @SaleTotal',16,1)
	ELSE
		BEGIN
			INSERT INTO Orders(CustomerName,CustomerAddress,CustomerEmail,CustomerContactNumber,OrderStatus,GST,SubTotal,SaleTotal,InvoiceNumber,Note)
			VALUES (@CustomerName,@CustomerAddress,@CustomerEmail,@CustomerContactNumber,@OrderStatus,@GST,@SubTotal,@SaleTotal,@InvoiceNumber,@Note)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateOrder - INSERT ERROR : Orders Table',16,1)
		END


GRANT EXECUTE ON CreateOrder TO aspnetcore

DROP PROCEDURE CreateOrder
