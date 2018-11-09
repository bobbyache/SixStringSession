USE smartsession_tests;

DROP PROCEDURE IF EXISTS `sp_InsertExercise`;
CREATE PROCEDURE `sp_InsertExercise`
(
	in title varchar(255), 
    in difficultyRating int(11),
    in practicalityRating int(11),
    in percentageCompleteCalculationType int(11),
    in initialMetronomeSpeed int(11),
    in targetMetronomeSpeed int(11),
    in targetPracticeTime int(11)
)
BEGIN
	INSERT INTO Exercise
    (
		Title, 
        DifficultyRating, 
        PracticalityRating, 
        PercentageCompleteCalculationType, 
        InitialMetronomeSpeed, 
        TargetMetronomeSpeed, 
        TargetPracticeTime, 
        DateCreated, 
        DateModified
	) 
	VALUES 
    (
		title,
        difficultyRating,
        practicalityRating,
        percentageCompleteCalculationType,
        initialMetronomeSpeed,
        targetMetronomeSpeed,
        targetPracticeTime,
		NOW(), 
        NULL
	);
	SELECT LAST_INSERT_ID();
END;


DROP PROCEDURE IF EXISTS `sp_GetExerciseById`;
CREATE PROCEDURE `sp_GetExerciseById`(IN _id int)
BEGIN
	SELECT
		Id,
		Title, 
        DifficultyRating, 
        PracticalityRating, 
        PercentageCompleteCalculationType, 
        InitialMetronomeSpeed, 
        TargetMetronomeSpeed, 
        TargetPracticeTime, 
        DateCreated, 
        DateModified
	FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_DeleteExercise`;
CREATE PROCEDURE `sp_DeleteExercise`(in _id int)
BEGIN
	DELETE FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_UpdateExercise`;
CREATE PROCEDURE `sp_UpdateExercise`(
	in _id int, 
	in _title varchar(255), 
	in _difficultyRating int,
	in _practicalityRating int,
	in _targetPracticeTime int,
	in _targetMetronomeSpeed int,
	in _initialMetronomeSpeed int
	)
BEGIN
	UPDATE Exercise SET 
		Title = _title,
		DifficultyRating = _difficultyRating,
		PracticalityRating = _practicalityRating,
		TargetPracticeTime = _targetPracticeTime,
		TargetMetronomeSpeed = _targetMetronomeSpeed,
		InitialMetronomeSpeed = _initialMetronomeSpeed,
		DateModified = NOW()
	WHERE Id = _id;
END;

COMMIT;