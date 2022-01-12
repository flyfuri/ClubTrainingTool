CREATE PROCEDURE [dbo].[Pilots_delete]
	@Pilot_ID INT
AS
BEGIN
	DELETE FROM TablePilots 	
	WHERE   Pilot_ID = @Pilot_ID
END