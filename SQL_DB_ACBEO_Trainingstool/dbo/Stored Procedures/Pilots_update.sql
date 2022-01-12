CREATE PROCEDURE [dbo].[Pilots_update]
	@Pilot_ID INT,
	@LicensNr VARCHAR(50),
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Disactivated BIT
AS
BEGIN
	UPDATE TablePilots 	
	SET		LicensNr = @LicensNr, 
			FirstName = @FirstName,
			LastName = @LastName,
			Disactivated = @Disactivated

	WHERE   Pilot_ID = @Pilot_ID
END