/*
	Get Category
*/
CREATE PROCEDURE GetCategory(@CategoryID VARCHAR(36) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @CategoryID IS NULL
		RAISERROR('GetCategory - Required parameter: @CategoryID',16,1)
	ELSE
		BEGIN
			SELECT CategoryID,CategoryName,CategoryDescription,CategoryImage
			FROM Category
			WHERE CategoryID = @CategoryID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetCategory - SELECT error: Category table.',16,1)
		END

	RETURN @ReturnCode


GRANT EXECUTE ON GetCategory TO aspnetcore
