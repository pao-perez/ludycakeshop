/*
	Get Orders
*/
CREATE PROCEDURE GetCategories
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1


	BEGIN
		SELECT CategoryID,CategoryName,CategoryDescription,CategoryImage
		FROM Category;

		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetCategories - SELECT error: Category table.',16,1)
	END

RETURN @ReturnCode


GRANT EXECUTE ON GetCategories TO aspnetcore

DROP PROCEDURE GetCategories
