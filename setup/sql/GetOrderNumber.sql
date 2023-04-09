/*
	Get Order Number
*/
CREATE PROCEDURE GetOrderNumber
AS
	DECLARE @OrderNumber INT
	SET @OrderNumber = 0

		BEGIN
			SET @OrderNumber = (SELECT @@IDENTITY FROM Orders)

			IF @@ERROR <> 0
				RAISERROR('GetOrderNumber - SELECT error: Orders table.',16,1)
		END

	RETURN @OrderNumber


GRANT EXECUTE ON GetOrderNumber TO aspnetcore

DROP PROCEDURE GetOrderNumber
