/*
	Create Bulk Order
*/
CREATE PROCEDURE CreateBulkOrder(@OrderID VARCHAR(36) = NULL,
							@BulkOrderStatus VARCHAR(20) = NULL,
							@InvoiceNumber BIGINT = NULL,
							@CompanyName VARCHAR(40) = NULL,
							@CompanyAddress VARCHAR(50) = NULL,
							@CompanyEmail VARCHAR(30) = NULL,
							@CompanyContactNumber VARCHAR(20) =NULL,
							@CompanyContactPerson VARCHAR(40) =NULL,
							@Note VARCHAR(255) = NULL,
							@GST MONEY = NULL,
							@SubTotal MONEY = NULL,
							@SaleTotal MONEY = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @OrderID IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @OrderID',16,1)
	ELSE IF @CompanyName IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @CompanyName',16,1)
	ELSE IF @CompanyContactNumber IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @CompanyContactNumber',16,1)
	ELSE IF @CompanyContactPerson IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @CompanyContactPerson',16,1)
	ELSE IF @BulkOrderStatus IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @BulkOrderStatus',16,1)
	ELSE IF @GST IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @GST',16,1)
	ELSE IF @SubTotal IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @SubTotal',16,1)
	ELSE IF @SaleTotal IS NULL
		RAISERROR('CreateBulkOrder - required parameter: @SaleTotal',16,1)
	ELSE
		BEGIN
			INSERT INTO BulkOrders(OrderID,CompanyName,CompanyAddress,CompanyEmail,CompanyContactNumber,CompanyContactPerson,BulkOrderStatus,GST,SubTotal,SaleTotal,InvoiceNumber,Note)
			VALUES (@OrderID,@CompanyName,@CompanyAddress,@CompanyEmail,@CompanyContactNumber,@CompanyContactPerson,@BulkOrderStatus,@GST,@SubTotal,@SaleTotal,@InvoiceNumber,@Note)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateBulkOrder - INSERT ERROR : BulkOrders Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON CreateBulkOrder TO aspnetcore

DROP PROCEDURE CreateBulkOrder
