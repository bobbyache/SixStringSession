
EXEC dbo.sp_InsertGoal 'Test Goal 1'
EXEC dbo.sp_InsertGoal 'Test Goal 2'
EXEC dbo.sp_InsertGoal 'Test Goal 3'

DECLARE @GoalId INT = (SELECT TOP 1 Id FROM dbo.Goal WHERE Title = 'Test Goal 1')

EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 1', 'P'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 2', 'P'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 3', 'M'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 6', 'P'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 7', 'M'

SET @GoalId = (SELECT TOP 1 Id FROM dbo.Goal WHERE Title = 'Test Goal 2')

EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 4', 'P'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 5', 'D'

SET @GoalId = (SELECT TOP 1 Id FROM dbo.Goal WHERE Title = 'Test Goal 2')

EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 8', 'P', 80, '2018-03-01'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 9', 'P', 80,  '2018-04-01'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 10', 'M', 80,  '2018-05-01'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 11', 'P', 80,  '2018-06-01'
EXEC dbo.sp_InsertGoalTask @GoalId, 'Test Goal Task 12', 'M', 80,  '2018-07-01'

