CREATE PROCEDURE [dbo].[Backup_DB_Data_withTimestamp]
	@nameDB VARCHAR(MAX) = '', -- DB NAME TO CREATE BACKUP
	@pathToSave VARCHAR(MAX) = '' -- DB NAME TO CREATE BACKUP
AS
	BEGIN

		--DECLARE @path VARCHAR(256) -- path of backup files
		DECLARE @fileName VARCHAR(256) -- filename for backup
		DECLARE @fileDate VARCHAR(20) -- used for file name
		DECLARE @fileTimeH VARCHAR(2) -- used for file name		
		DECLARE @fileTimeM VARCHAR(2) -- used for file name
		DECLARE @fileTimeS VARCHAR(2) -- used for file name

		--SET @path = '.\Bckups\'

		-- specify filename format
		SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112)
		SELECT @fileTimeH = RIGHT('00' + CAST(DATEPART(HOUR,GETDATE()) AS VARCHAR),2)
		SELECT @fileTimeM = RIGHT('00' + CAST(DATEPART(MINUTE,GETDATE()) AS VARCHAR),2)
		SELECT @fileTimeS = RIGHT('00' + CAST(DATEPART(SECOND,GETDATE()) AS VARCHAR),2)

			BEGIN
			SET @fileName = @pathToSave + @nameDB + '_' + @fileDate + '_' +  @fileTimeH +  @fileTimeM +  @fileTimeS  + '.BAK'
			BACKUP DATABASE @nameDB TO DISK = @fileName
			END
	END
RETURN 0
