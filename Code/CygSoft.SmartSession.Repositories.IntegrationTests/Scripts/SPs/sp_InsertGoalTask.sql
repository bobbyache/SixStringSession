

/* ===================================================================================================
	Name:				sp_InsertGoalTask
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Insert a goal task
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_InsertGoalTask 'DevUser', 1002, 2016, 1
	C:\Work\Files\New SPs\sp_InsertGoalTask.sql
=================================================================================================== */

CREATE PROCEDURE dbo.sp_InsertGoalTask
(
	@GoalId INT,
	@Title NVARCHAR(150),
	@GoalTaskType NCHAR(1),
	@DesiredSpeed INT = NULL,
	@CreateDate DATETIME = NULL,
	@Description NVARCHAR(500) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;
	
INSERT INTO [dbo].[GoalTask]
           (
           [GoalId]
           ,[Title]
           ,[Description]
           ,[CreateDate]
           ,[GoalTaskType]
           ,[DesiredSpeed])
     VALUES
           (
				@GoalId,
				@Title,
				@Description,
				@CreateDate,
				@GoalTaskType,
				@DesiredSpeed
		   )
END


