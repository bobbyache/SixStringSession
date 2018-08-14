
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

CREATE PROCEDURE dbo.sp_DeleteGoalTask
(
	@Id INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM GoalTask WHERE Id = @Id
END

