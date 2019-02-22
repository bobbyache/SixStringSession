

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Yellow Exercise', 80, 50, NULL, 50, 0, '2015-02-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise', 100, 50, NULL, 50, 0, '2015-05-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Blue Exercise', 120, 50, NULL, 50, 0, '2015-12-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Orange Exercise', NULL, 50, 30000, 50, 0, '2016-02-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise 1', NULL, 50, 40000, 50, 0, '2019-02-01 01:00:00', '2019-04-01 01:00:00');

INSERT INTO Exercise (Title,  TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise 2', NULL, 50, 50000, 50, 0, '2019-02-01 01:00:00', '2019-06-01 01:00:00');

	
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Monday Routine', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Tuesday Routine', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Wednesday Routine', '2017-02-01 01:00:00');
	

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, DifficultyRating, PracticalityRating, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Yellow Exercise'),
            100,
            3,
            3,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, DifficultyRating, PracticalityRating, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Green Exercise'),
            120,
            4,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, DifficultyRating, PracticalityRating, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Orange Exercise'),
            120,
            4,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, DifficultyRating, PracticalityRating, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Wednesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Orange Exercise'),
            120,
            4,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, DifficultyRating, PracticalityRating, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Wednesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Blue Exercise'),
            120,
            4,
            4,
            '2015-02-01 01:20:00',
            NULL
        );