	
    /* ************************************************************************************************
    
    PracticeRoutineExercise - needs to be modified in the domain to supply some readonly information.
    The object will need to become full blown, and exposes its own eventing model too to the 
    PracticeRoutine object that will house it.
    
    ************************************************************************************************ */
    
    CREATE TEMPORARY TABLE LastMetronomeSpeeds
    
    SELECT DISTINCT
		E.Id AS ExerciseId, 
        EA.MetronomeSpeed AS LastRecordedSpeed,
        EA.ManualProgress AS LastRecordedManualProgress,
        SUM(EA.Seconds) AS TotalSeconds
    FROM
		ExerciseActivity EA
        INNER JOIN Exercise E ON E.Id = EA.ExerciseId
        INNER JOIN PracticeRoutineExercise PRE ON PRE.ExerciseId = E.Id
        INNER JOIN PracticeRoutine PR ON PR.Id = PRE.PracticeRoutineId
		INNER JOIN
        (
			SELECT
				E.Id, 
				MAX(EA.Id) AS LastId
			FROM
				ExerciseActivity EA
				INNER JOIN Exercise E ON E.Id = EA.ExerciseId
				INNER JOIN PracticeRoutineExercise PRE ON PRE.ExerciseId = E.Id
				INNER JOIN PracticeRoutine PR ON PR.Id = PRE.PracticeRoutineId
			WHERE
				PR.Title = 'Wednesday Routine'
			GROUP BY
				E.Id
        ) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	GROUP BY
		E.Id, 
        EA.MetronomeSpeed, 
        EA.ManualProgress
	;
    
    SELECT 
		LM.ExerciseId,
        PRE.PracticeRoutineId,
        E.Title,
        LM.LastRecordedSpeed,
        LM.LastRecordedManualProgress,
        LM.TotalSeconds AS TotalPracticedSeconds,
		PRE.AssignedPracticeTime AS AssignedPracticeTimeSeconds,
        PRE.DifficultyRating, 
        PRE.PracticalityRating,
        E.TargetMetronomeSpeed, 
        E.TargetPracticeTime,
        PRE.DateCreated,
        PRE.DateModified
    FROM
		Exercise E
		INNER JOIN PracticeRoutineExercise PRE ON PRE.ExerciseId = E.Id
		INNER JOIN PracticeRoutine PR ON PR.Id = PRE.PracticeRoutineId
        INNER JOIN LastMetronomeSpeeds LM ON LM.ExerciseId = E.Id
	WHERE
		PR.Title = 'Wednesday Routine'
        
	;

        
	-- SELECT * FROM LastMetronomeSpeeds;
    
    DROP TEMPORARY TABLE LastMetronomeSpeeds;
