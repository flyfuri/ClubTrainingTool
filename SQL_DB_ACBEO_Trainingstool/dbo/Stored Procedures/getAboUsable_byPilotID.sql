CREATE PROCEDURE [dbo].[getAboUsable_byPilotID]
	@PilotID int,
	@actYear int
AS
	SELECT * 
	FROM TableAbos
	WHERE PilotID = @PilotID
	AND (YEAR(DateBought) = @actYear -1
	OR YEAR(DateBought) = @actYear)
	/*AND (YEAR(DateBought) = YEAR(GETDATE()) -1
	OR YEAR(DateBought) = YEAR(GETDATE()))*/
	AND (DateFlight1 < '1800-01-01'
	OR DateFlight2 < '1800-01-01'
	OR DateFlight3 < '1800-01-01'
	OR DateFlight4 < '1800-01-01'
	OR DateFlight5 < '1800-01-01'
	OR DateFlight6 < '1800-01-01'
	OR DateFlight7 < '1800-01-01'
	OR DateFlight8 < '1800-01-01'
	OR DateFlight9 < '1800-01-01'
	OR DateFlight10 < '1800-01-01')
RETURN 0