USE [SmartSession]
GO

EXEC dbo.sp_InsertGoal 'Test Goal 1'

DECLARE @GoalId INT = (SELECT TOP 1 Id FROM dbo.Goal WHERE Title = 'Test Goal 1')

EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 1', 'P'