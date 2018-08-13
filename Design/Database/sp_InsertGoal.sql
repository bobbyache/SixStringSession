
/* ===================================================================================================
	Name:				sp_InsertGoal
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Insert a goal
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_InsertGoal 'DevUser'
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_InsertGoal')
   DROP PROC sp_InsertGoal;
GO

CREATE PROCEDURE dbo.sp_InsertGoal
(
	@Title NVARCHAR(150),
	@CreateDate DATETIME = NULL,
	@Status NCHAR(3) = 'CRE', -- Created 
	@TargetCompletionDate DATETIME = NULL

)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @InsertDate DATETIME = GETDATE()
	IF @CreateDate IS NULL SET @CreateDate = @InsertDate

	INSERT INTO [dbo].[Goal]
			   (
			   [Title]
			   ,[CreateDate]
			   ,[TargetCompletionDate]
			   ,[Status]
			   )
		VALUES
		(
			@Title,
			@CreateDate,
			@TargetCompletionDate,
			@Status
		)
	
END

GO