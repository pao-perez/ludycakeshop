/*
	Get Order
*/
CREATE PROCEDURE GetOrder(@OrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @OrderID IS NULL
		RAISERROR('GetOrder - Required parameter: @OrderID',16,1)
	ELSE
		BEGIN
			SELECT OrderID,OrderNumber,InvoiceNumber,OrderDate,OrderStatus,GST,SubTotal,SaleTotal,CustomerName,CustomerAddress,CustomerEmail,CustomerContactNumber,Note
			FROM Orders
			WHERE OrderID = @OrderID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetOrder - SELECT error: Orders table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetOrder TO aspnetcore

DROP PROCEDURE GetOrder
