/*
	Update Category
*/
CREATE PROCEDURE UpdateCategory(@CategoryID VARCHAR(36) = NULL,
							@CategoryName VARCHAR(25) = NULL,
							@CategoryDescription VARCHAR(200) = NULL,
							@CategoryImage IMAGE = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @CategoryID IS NULL
		RAISERROR('UpdateCategory - required parameter: @CategoryID',16,1)
	ELSE IF @CategoryName IS NULL
		RAISERROR('UpdateCategory - required parameter: @CategoryName',16,1)
	ELSE
		BEGIN
			UPDATE Category
			SET CategoryName = @CategoryName,
				CategoryDescription = @CategoryDescription,
				CategoryImage = @CategoryImage
			WHERE CategoryID = @CategoryID

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateCategory - INSERT ERROR : Category Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON UpdateCategory TO aspnetcore
