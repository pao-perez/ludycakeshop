/*
	Delete Category
*/
CREATE PROCEDURE DeleteCategory(@CategoryID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @CategoryID IS NULL
		RAISERROR('DeleteCategory - Required parameter: @CategoryID',16,1)
	ELSE
		BEGIN
			DELETE
			FROM Category
			WHERE CategoryID = @CategoryID;

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('DeleteCategory - DELETE error: Category table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON DeleteCategory TO aspnetcore

DROP PROCEDURE DeleteCategory