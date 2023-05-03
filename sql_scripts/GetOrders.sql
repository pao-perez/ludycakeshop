/*
	Get Orders
*/
CREATE PROCEDURE GetOrders
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT 
			OrderID,
			OrderNumber,
			InvoiceNumber,
			OrderDate,
			OrderStatus,
			GST,
			SubTotal,
			SaleTotal,
			Orders.CustomerName,
			Orders.CustomerAddress,
			Orders.CustomerEmail,
			Orders.CustomerContactNumber,
			Orders.Note
		FROM Orders;
			
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetOrders - SELECT error: Orders table.',16,1)
	END

RETURN @ReturnCode

GRANT EXECUTE ON GetOrders TO aspnetcore
