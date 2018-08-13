/* ===================================================================================================
	Name:				sp_FindGoalTask
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Find a goal task
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_FindGoalTask ...
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_FindGoalTask')
   DROP PROC sp_FindGoalTask;
GO

CREATE PROCEDURE dbo.sp_FindGoalTask
(
	@Title NVARCHAR(150) = NULL,
	@GoalTaskType NCHAR(1) = NULL,
	@CreatedFrom DATETIME = NULL,
	@CreatedTo DATETIME = NULL
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		Id,
		GoalId,
		Title, 
		[Description],
		CreateDate,
		GoalTaskType,
		DesiredSpeed 
	FROM GoalTask
	WHERE
		 (@Title IS NULL OR Title LIKE '%' + @Title + '%')
		 AND (@GoalTaskType IS NULL OR GoalTaskType = @GoalTaskType)
		 AND (@CreatedFrom IS NULL OR CreateDate >= @CreatedFrom)
		 AND (@CreatedTo IS NULL OR CreateDate <= @CreatedTo)
	
END

GO

