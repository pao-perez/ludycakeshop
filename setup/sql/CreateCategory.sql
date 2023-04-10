/*
	Create Category
*/
CREATE PROCEDURE CreateCategory(@CategoryID VARCHAR(36) = NULL,
							@CategoryName VARCHAR(25) = NULL,
							@CategoryDescription VARCHAR(200) = NULL,
							@CategoryImage IMAGE = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = -1
	
	IF @CategoryID IS NULL
		RAISERROR('CreateCategory - required parameter: @CategoryID',16,1)
	ELSE IF @CategoryName IS NULL
		RAISERROR('CreateCategory - required parameter: @CategoryName',16,1)
	ELSE IF @CategoryDescription IS NULL
		RAISERROR('CreateCategory - required parameter: @CategoryDescription',16,1)
	ELSE
		BEGIN
			INSERT INTO Category(CategoryID,CategoryName,CategoryDescription,CategoryImage)
			VALUES (@CategoryID,@CategoryName,@CategoryDescription,@CategoryImage)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CreateCategory - INSERT ERROR : Category Table',16,1)
		END

	Return @ReturnCode

GRANT EXECUTE ON CreateCategory TO aspnetcore

DROP PROCEDURE CreateCategory
