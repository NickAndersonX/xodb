USE [XODB]
GO
DROP INDEX ISurvey ON dbo.X_Survey
GO
UPDATE X_PrivateData set Value='2' where UniqueID='XODBSchemaVersion'
GO