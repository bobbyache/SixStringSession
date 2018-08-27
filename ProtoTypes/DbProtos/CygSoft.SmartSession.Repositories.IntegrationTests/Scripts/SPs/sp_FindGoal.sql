/* ===================================================================================================
	Name:				sp_FindGoal
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Find a goal
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_FindGoal ...
=================================================================================================== */

CREATE PROCEDURE dbo.sp_FindGoal
(
	@Title NVARCHAR(150) = NULL,
	@GoalTaskType NCHAR(1) = NULL,
	@CreatedFrom DATETIME = NULL,
	@CreatedTo DATETIME = NULL,
	@TargetCompletionDateFrom DATETIME = NULL,
	@TargetCompletionDateTo DATETIME = NULL,
	@Status NCHAR(3) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		Id,
		Title, 
		CreateDate,
		TargetCompletionDate,
		[Status]
	FROM Goal
	WHERE
		 (@Title IS NULL OR Title LIKE '%' + @Title + '%')
		 AND (@CreatedFrom IS NULL OR CreateDate >= @CreatedFrom)
		 AND (@CreatedTo IS NULL OR CreateDate <= @CreatedTo)
		 AND (@TargetCompletionDateFrom IS NULL OR TargetCompletionDate >= @TargetCompletionDateFrom)
		 AND (@TargetCompletionDateTo IS NULL OR TargetCompletionDate <= @TargetCompletionDateTo)
		 AND (@Status IS NULL OR [Status] = @Status)
	
END


