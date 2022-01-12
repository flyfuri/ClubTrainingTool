CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_TotalAboFlights0fromJobs]
	@YEAR INT
AS
	SELECT TableAboFlight.Abo_Year, TableAboFlight.Abo_NrInYear, TableAboFlight.FlightNrOnAbo, TableAboFlight.DateFlightPayedWith, TableAboFlight.Comment
	FROM TableAboFlight
	WHERE TableAboFlight.Abo_Year = @YEAR 
	and TableAboFlight.Comment like 'Flug aus überzähligen Diensten'
	and TableAboFlight.Abo_NrInYear = 0

	ORDER BY TableAboFlight.DateFlightPayedWith ASC
RETURN 0