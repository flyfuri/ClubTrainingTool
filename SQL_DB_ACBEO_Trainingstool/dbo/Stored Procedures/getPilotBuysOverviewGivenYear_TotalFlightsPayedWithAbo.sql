CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_TotalFlightsPayedWithAbo]
	@YEAR INT
AS
	SELECT TableAboFlight.Abo_Year, TableAboFlight.Abo_NrInYear, TableAboFlight.FlightNrOnAbo, TableAboFlight.DateFlightPayedWith
	FROM TableAboFlight
	WHERE YEAR(TableAboFlight.DateFlightPayedWith) = @YEAR

	ORDER BY TableAboFlight.DateFlightPayedWith ASC
RETURN 0