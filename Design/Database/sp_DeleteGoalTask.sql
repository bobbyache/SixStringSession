
/* ===================================================================================================
	Name:				sp_DeleteGoalTask
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Delete a goal task
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_DeleteGoalTask 
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_DeleteGoalTask')
   DROP PROC sp_DeleteGoalTask;
GO

CREATE PROCEDURE dbo.sp_DeleteGoalTask
(
	@Id INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM GoalTask WHERE Id = @Id
END

