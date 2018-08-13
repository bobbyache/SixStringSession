
-- USE SmartSession
--GO

/* ===================================================================================================
	Name:				sp_InsertSession
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Create a newly completed session.
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_InsertSession 'DevUser', 1002, 2016, 1
	C:\Work\Files\New SPs\sp_InsertSession.sql
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

PRINT N'Creating [dbo].[sp_InsertSession] procedure'
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_InsertSession')
   DROP PROC sp_InsertSession;
GO

CREATE PROCEDURE dbo.sp_InsertSession
(
	@StartTime DATETIME,
	@EndTime DATETIME,
	@Notes NVARCHAR(500) = NULL,
	@SessionId INT OUTPUT 
)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Session]
			   (
			   [StartTime]
			   ,[EndTime]
			   ,[Notes])
	--OUTPUT Inserted.ID
	VALUES
		(

		@StartTime,
		@EndTime,
		@Notes)

	SET @SessionId = SCOPE_IDENTITY()
	RETURN @SessionId
END

GO