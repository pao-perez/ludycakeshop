/*
	Get User Account
*/
CREATE PROCEDURE GetUserAccount(@Username VARCHAR(30) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @Username IS NULL
		RAISERROR('GetUserAccount - Required parameter: @Username',16,1)
	ELSE
		BEGIN
			SELECT 
				Username,
				Password,
				UserAccountID
			FROM UserAccount
			WHERE Username = @Username

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetUserAccount - SELECT error: UserAccount table.',16,1)
		END

	RETURN @ReturnCode

GRANT EXECUTE ON GetUserAccount TO aspnetcore

DROP PROCEDURE GetUserAccount
