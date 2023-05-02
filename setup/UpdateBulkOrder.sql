/*
	Update Bulk Order
*/
CREATE PROCEDURE UpdateBulkOrder(@BulkOrderID VARCHAR(36) = NULL,
							@BulkOrderStatus VARCHAR(20) = NULL,
							@InvoiceNumber BIGINT = NULL,
							@CompanyName VARCHAR(40) = NULL,
							@CompanyAddress VARCHAR(50) = NULL,
							@CompanyEmail VARCHAR(30) = NULL,
							@CompanyContactNumber VARCHAR(20) =NULL,
							@CompanyContactPerson VARCHAR(40) = NULL,
							@Note VARCHAR(255) = NULL,
							@GST MONEY = NULL,
							@SubTotal MONEY = NULL,
							@SaleTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @BulkOrderID IS NULL
		RAISERROR('UpdateBulkOrder - required parameter: @BulkOrderID',16,1)
	ELSE
		BEGIN
			UPDATE BulkOrders
			SET InvoiceNumber = @InvoiceNumber,
				BulkOrderStatus = @BulkOrderStatus,
				CompanyName = @CompanyName,
				CompanyAddress = @CompanyAddress,
				CompanyEmail = @CompanyEmail,
				CompanyContactNumber = @CompanyContactNumber,
				CompanyContactPerson = @CompanyContactPerson,
				Note = @Note,
				GST = @GST,
				SubTotal = @SubTotal,
				SaleTotal = @SaleTotal
			WHERE BulkOrderID = @BulkOrderID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateBulkOrder - UPDATE ERROR : BulkOrders Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON UpdateBulkOrder TO aspnetcore
