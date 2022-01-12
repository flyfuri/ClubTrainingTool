CREATE PROCEDURE [dbo].[Pilots_add]
	@LicensNr VARCHAR(50),
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Disactivated BIT
AS
BEGIN
	INSERT INTO TablePilots (LicensNr, FirstName, LastName, Disactivated) 
	VALUES (@LicensNr, @FirstName, @LastName, @Disactivated)
END