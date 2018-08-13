
/* ===================================================================================================
	Name:				sp_GetTasksByGoal
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Select tasks for a specific goal
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_GetTasksByGoal 1
=================================================================================================== */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_GetTasksByGoal')
   DROP PROC sp_GetTasksByGoal;
GO

CREATE PROCEDURE dbo.sp_GetTasksByGoal
(
	@GoalId INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [Id]
		  ,[GoalId]
		  ,[Title]
		  ,[Description]
		  ,[CreateDate]
		  ,[GoalTaskType]
		  ,[DesiredSpeed]
	  FROM 
	[dbo].[GoalTask]
	WHERE
		GoalId = @GoalId

END

GO


