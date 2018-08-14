
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


