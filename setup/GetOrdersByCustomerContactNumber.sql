/*
	Get Orders By Customer Contact Number
*/
CREATE PROCEDURE GetOrdersByCustomerContactNumber(@CustomerContactNumber VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @CustomerContactNumber IS NULL
		RAISERROR('GetOrdersByCustomerContactNumber - Required parameter: @CustomerContactNumber',16,1)
	ELSE
		BEGIN
			SELECT OrderID,OrderNumber,InvoiceNumber,OrderDate,OrderStatus,GST,SubTotal,SaleTotal,CustomerName,CustomerAddress,CustomerEmail,CustomerContactNumber,Note
			FROM Orders
			WHERE CustomerContactNumber = @CustomerContactNumber

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrdersByCustomerContactNumber - SELECT error: Orders table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetOrdersByCustomerContactNumber TO aspnetcore
