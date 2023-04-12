/*
	Get Orders By Customer Email
*/
CREATE PROCEDURE GetOrdersByCustomerEmail(@CustomerEmail VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @CustomerEmail IS NULL
		RAISERROR('GetOrdersByCustomerEmail - Required parameter: @CustomerEmail',16,1)
	ELSE
		BEGIN
			SELECT OrderID,OrderNumber,InvoiceNumber,OrderDate,OrderStatus,GST,SubTotal,SaleTotal,CustomerName,CustomerAddress,CustomerEmail,CustomerContactNumber,Note
			FROM Orders
			WHERE CustomerEmail = @CustomerEmail

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrdersByCustomerEmail - SELECT error: Orders table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetOrdersByCustomerEmail TO aspnetcore

DROP PROCEDURE GetOrdersByCustomerEmail
