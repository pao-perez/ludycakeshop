/*
	Get Bulk Order
*/
CREATE PROCEDURE GetBulkOrder(@OrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @OrderID IS NULL
		RAISERROR('GetBulkOrder - Required parameter: @OrderID',16,1)
	ELSE
		BEGIN
			SELECT 
				OrderID,
				OrderNumber,
				InvoiceNumber,
				OrderDate,
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
			FROM BulkOrders
			WHERE OrderID = @OrderID;
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetBulkOrder - SELECT error: BulkOrders table.',16,1)
		END

RETURN @ReturnCode

GRANT EXECUTE ON GetBulkOrder TO aspnetcore
