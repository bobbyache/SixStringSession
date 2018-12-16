USE smartsession_tests;

/* ***********************************************************************************************************************
Exercise
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_InsertExercise`;
CREATE PROCEDURE `sp_InsertExercise`
(
	in _title varchar(255), 
    in _difficultyRating int(11),
    in _practicalityRating int(11),
    in _targetMetronomeSpeed int(11),
    in _speedProgressWeighting int(11),
    in _targetPracticeTime int(11),
    in _practiceTimeProgressWeighting int(11),
    in _manualProgressWeighting int(11)
)
BEGIN
	INSERT INTO Exercise
    (
		Title, 
        DifficultyRating, 
        PracticalityRating, 
        TargetMetronomeSpeed, 
        SpeedProgressWeighting,
        TargetPracticeTime,
        PracticeTimeProgressWeighting,
        ManualProgressWeighting,
        DateCreated, 
        DateModified
	) 
	VALUES 
    (
		_title,
        _difficultyRating,
        _practicalityRating,
        _targetMetronomeSpeed,
        _speedProgressWeighting,
        _targetPracticeTime,
        _practiceTimeProgressWeighting,
        _manualProgressWeighting,
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
        TargetMetronomeSpeed, 
        SpeedProgressWeighting,
        TargetPracticeTime, 
        PracticeTimeProgressWeighting,
        ManualProgressWeighting,
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
    in _practiceTimeProgressWeighting int,
	in _targetMetronomeSpeed int,
    in _speedProgressWeighting int,
    in _manualProgressWeighting int
	)
BEGIN
	UPDATE Exercise SET 
		Title = _title,
		DifficultyRating = _difficultyRating,
		PracticalityRating = _practicalityRating,
		TargetPracticeTime = _targetPracticeTime,
        PracticeTimeProgressWeighting = _practiceTimeProgressWeighting,
		TargetMetronomeSpeed = _targetMetronomeSpeed,
        SpeedProgressWeighting = _speedProgressWeighting,
        ManualProgressWeighting = _manualProgressWeighting,
		DateModified = NOW()
	WHERE Id = _id;
END;


DROP PROCEDURE IF EXISTS `sp_FindExercises`;
CREATE PROCEDURE `sp_FindExercises`(
	in _title varchar(255),
	-- in _percentCompleteCalculationType int,
	in _fromDateCreated datetime,
	in _toDateCreated datetime,
	in _fromDateModified datetime,
	in _toDateModified datetime
	)
BEGIN
	SELECT * 
	FROM Exercise
	WHERE
		(_title IS NULL OR Title LIKE CONCAT('%', _title, '%'))
		-- AND
		-- (_percentCompleteCalculationType IS NULL OR PercentCompleteCalculationType = _percentCompleteCalculationType)
		AND
		(_fromDateCreated IS NULL OR DateCreated >= _fromDateCreated)
		AND
		(_toDateCreated IS NULL OR DateCreated <= _toDateCreated)
		AND
		(_fromDateModified IS NULL OR DateModified >= _fromDateModified)
		AND
		(_toDateModified IS NULL OR DateModified <= _toDateModified)
		;
END;

/* ***********************************************************************************************************************
ExerciseActivity
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_InsertExerciseActivity`;
CREATE PROCEDURE `sp_InsertExerciseActivity`
(
	in _exerciseId int(11),
	in _startTime datetime,
	in _endTime datetime,
	in _seconds int(11),
	in _metronomeSpeed int(11),
    in _manualProgress int(11)
)
BEGIN
	INSERT INTO ExerciseActivity
    (
		ExerciseId,
		StartTime,
		EndTime,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated
	) 
	VALUES 
    (
		_exerciseId,
		_startTime,
		_endTime,
		_seconds,
		_metronomeSpeed,
        _manualProgress,
		NOW()
	);
	SELECT LAST_INSERT_ID();
END;


DROP PROCEDURE IF EXISTS `sp_GetExerciseActivityById`;
CREATE PROCEDURE `sp_GetExerciseActivityById`(IN _id int)
BEGIN
	SELECT
		Id,
		ExerciseId,
		StartTime,
		EndTime,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated,
		DateModified
	FROM ExerciseActivity WHERE Id = _id;
END;


DROP PROCEDURE IF EXISTS `sp_DeleteExerciseActivity`;
CREATE PROCEDURE `sp_DeleteExerciseActivity`(in _id int)
BEGIN
	DELETE FROM ExerciseActivity WHERE Id = _id;
END;


DROP PROCEDURE IF EXISTS `sp_UpdateExerciseActivity`;
CREATE PROCEDURE `sp_UpdateExerciseActivity`(
	_id int,
	_startTime datetime,
	_endTime datetime,
	_seconds int,
	_metronomeSpeed int,
    _manualProgress int
	)
BEGIN
	UPDATE ExerciseActivity SET 
		StartTime = _startTime,
		EndTime = _endTime,
		Seconds = _seconds,
		MetronomeSpeed = _metronomeSpeed,
        ManualProgress = _manualProgress,
		DateModified = NOW()
	WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_GetExerciseActivitiesByExercise`;
CREATE PROCEDURE `sp_GetExerciseActivitiesByExercise`(
	in _exerciseId int
	)
BEGIN
	SELECT  
		Id,
		ExerciseId,
		StartTime,
		EndTime,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated,
		DateModified
	FROM ExerciseActivity
	WHERE
		ExerciseId = _exerciseId;
END;
COMMIT;




DROP PROCEDURE IF EXISTS `sp_InsertPracticeRoutine`;
CREATE PROCEDURE `sp_InsertPracticeRoutine`
(
	in _title varchar(255)
)
BEGIN
	INSERT INTO Exercise
    (
		Title, 
        DateModified
	) 
	VALUES 
    (
		_title,
		NOW(), 
        NULL
	);
	SELECT LAST_INSERT_ID();
END;

DROP PROCEDURE IF EXISTS `sp_GetPracticeRoutineById`;
CREATE PROCEDURE `sp_GetPracticeRoutineById`(IN _id int)
BEGIN
	SELECT
		Id,
		Title, 
        DateCreated, 
        DateModified
	FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_FindPracticeRoutines`;
CREATE PROCEDURE `sp_FindPracticeRoutines`(
	in _title varchar(255),
	in _fromDateCreated datetime,
	in _toDateCreated datetime,
	in _fromDateModified datetime,
	in _toDateModified datetime
	)
BEGIN
	SELECT * 
	FROM Exercise
	WHERE
		(_title IS NULL OR Title LIKE CONCAT('%', _title, '%'))
		-- AND
		-- (_percentCompleteCalculationType IS NULL OR PercentCompleteCalculationType = _percentCompleteCalculationType)
		AND
		(_fromDateCreated IS NULL OR DateCreated >= _fromDateCreated)
		AND
		(_toDateCreated IS NULL OR DateCreated <= _toDateCreated)
		AND
		(_fromDateModified IS NULL OR DateModified >= _fromDateModified)
		AND
		(_toDateModified IS NULL OR DateModified <= _toDateModified)
		;
END;

DROP PROCEDURE IF EXISTS `sp_DeletePracticeRoutine`;
CREATE PROCEDURE `sp_DeletePracticeRoutine`(in _id int)
BEGIN
	DELETE FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_UpdatePracticeRoutine`;
CREATE PROCEDURE `sp_UpdatePracticeRoutine`(
	in _id int, 
	in _title varchar(255)
	)
BEGIN
	UPDATE Exercise SET 
		Title = _title,
		DateModified = NOW()
	WHERE Id = _id;
END;
