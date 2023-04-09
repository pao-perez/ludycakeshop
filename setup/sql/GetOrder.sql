/*
	Get Order
*/
CREATE PROCEDURE GetOrder(@OrderNumber INT = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @OrderNumber IS NULL
		RAISERROR('GetOrder - Required parameter: @OrderNumber',16,1)
	ELSE
		BEGIN
			SELECT OrderNumber,InvoiceNumber,OrderDate,OrderStatus,GST,SubTotal,SaleTotal,CustomerName,CustomerAddress,CustomerEmail,CustomerContactNumber,Note
			FROM Orders
			WHERE OrderNumber = @OrderNumber

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrder - SELECT error: Orders table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetOrder TO aspnetcore

DROP PROCEDURE GetOrder
