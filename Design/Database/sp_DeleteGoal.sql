
/* ===================================================================================================
	Name:				sp_DeleteGoal
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Delete a goal and all it's tasks.
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_DeleteGoal ...
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_DeleteGoal')
   DROP PROC sp_DeleteGoal;
GO

CREATE PROCEDURE dbo.sp_DeleteGoal
(
	@Id INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM GoalTask WHERE GoalId = @Id
	DELETE FROM Goal
	WHERE 
		Id = @Id
END
GO

