CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_TotalAboFlights_0_unused]
	@YEAR INT
AS
	SELECT TableAboFlight.Abo_Year, TableAboFlight.Abo_NrInYear, TableAboFlight.FlightNrOnAbo, TableAboFlight.DateFlightPayedWith
	FROM TableAboFlight
	WHERE (TableAboFlight.Abo_Year = @YEAR OR TableAboFlight.Abo_Year = @YEAR -1)
	and (TableAboFlight.DateFlightPayedWith < '1800-01-01' ) 
	and TableAboFlight.Abo_NrInYear = 0

	ORDER BY TableAboFlight.DateFlightPayedWith ASC
RETURN 0