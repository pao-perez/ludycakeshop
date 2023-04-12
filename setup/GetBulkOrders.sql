/*
	Get Bulk Orders
*/
CREATE PROCEDURE GetBulkOrders
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	BEGIN
		SELECT 
			BulkOrderID,
			BulkOrderNumber,
			InvoiceNumber,
			BulkOrderDate,
			BulkOrderStatus,
			GST,
			SubTotal,
			SaleTotal,
			CompanyName,
			CompanyAddress,
			CompanyEmail,
			CompanyContactNumber,
			CompanyContactPerson,
			Note
		FROM BulkOrders;
			
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetBulkOrders - SELECT error: BulkOrders table.',16,1)
	END

RETURN @ReturnCode

GRANT EXECUTE ON GetBulkOrders TO aspnetcore

DROP PROCEDURE GetBulkOrders
