/*
	Get Bulk Order
*/
CREATE PROCEDURE GetBulkOrder(@BulkOrderID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @BulkOrderID IS NULL
		RAISERROR('GetBulkOrder - Required parameter: @BulkOrderID',16,1)
	ELSE
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
			FROM BulkOrders
			WHERE BulkOrderID = @BulkOrderID;
			
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetBulkOrder - SELECT error: BulkOrders table.',16,1)
		END

RETURN @ReturnCode

GRANT EXECUTE ON GetBulkOrder TO aspnetcore

DROP PROCEDURE GetBulkOrder
