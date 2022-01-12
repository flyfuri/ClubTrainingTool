CREATE PROCEDURE [dbo].[Pilots_getByID]
	@Pilot_ID int
AS
	SELECT * from tablePilots where Pilot_ID = @Pilot_ID;
RETURN 0