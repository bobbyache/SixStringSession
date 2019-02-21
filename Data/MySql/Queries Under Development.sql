	SELECT * FROM smartsession_tests.exercise;
	SELECT * FROM smartsession_tests.exerciseactivity
	SELECT * FROM smartsession_tests.PracticeRoutineTimeslot;
	SELECT * FROM smartsession_tests.timeslotexercise;

	
    /* ************************************************************************************************
	Get back random time slot records in a practice routine.
    ************************************************************************************************ */


		DROP TEMPORARY TABLE IF EXISTS RandomRoutineSlotExercises;
		
		CREATE TEMPORARY TABLE RandomRoutineSlotExercises
		SELECT 
			PR.Id AS PracticeRoutineId, 
            T.Id As TimeSlotId, 
            (
				SELECT Id 
                FROM 
					Exercise E1
                    INNER JOIN TimeSlotExercise TSE1 ON TSE1.ExerciseId = E1.Id
                WHERE
					TSE1.TimeSlotId = T.Id
                ORDER BY RAND() LIMIT 1
            ) AS ExerciseId
		FROM 
			PracticeRoutine PR 
			INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
			INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
			INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
			INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
		WHERE
			PR.Title = 'Monday Routine'
		GROUP BY
			PR.Id,
            T.Id;


		SELECT 
			PR.Title,
            T.Title,
			E.Title
        FROM
			RandomRoutineSlotExercises TMP
			INNER JOIN PracticeRoutine PR ON PR.Id = TMP.PracticeRoutineId
			INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
			INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
			INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
			INNER JOIN Exercise E ON E.Id = TSE.ExerciseId AND E.Id = TMP.ExerciseId
		;
        
		DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
        

	


    /* ************************************************************************************************
    
    PracticeRoutineExercise - needs to be modified in the domain to supply some readonly information.
    The object will need to become full blown, and exposes its own eventing model too to the 
    PracticeRoutine object that will house it.
    
	CALL sp_GetPracticeRoutineExercisesByPracticeRoutine (1)
    ************************************************************************************************ */
    
	DROP PROCEDURE IF EXISTS `sp_GetPracticeRoutineExercisesByPracticeRoutine`;
	CREATE PROCEDURE `sp_GetPracticeRoutineExercisesByPracticeRoutine`(
		in _practiceRoutineId int
		)
	BEGIN

		DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
		
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
					PR.Id = _practiceRoutineId
				GROUP BY
					E.Id
			) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
		GROUP BY
			E.Id, 
			EA.MetronomeSpeed, 
			EA.ManualProgress
		;
		
		SELECT 
			E.Id AS ExerciseId,
			PRE.PracticeRoutineId,
			E.Title,
			LM.LastRecordedSpeed,
			LM.LastRecordedManualProgress,
			LM.TotalSeconds AS TotalPracticeTime,
			PRE.AssignedPracticeTime,
			PRE.DifficultyRating, 
			PRE.PracticalityRating,
			E.TargetMetronomeSpeed, 
			E.TargetPracticeTime AS TargetPracticeTime,
			PRE.DateCreated,
			PRE.DateModified
		FROM
			Exercise E
			INNER JOIN PracticeRoutineExercise PRE ON PRE.ExerciseId = E.Id
			INNER JOIN PracticeRoutine PR ON PR.Id = PRE.PracticeRoutineId
			LEFT JOIN LastMetronomeSpeeds LM ON LM.ExerciseId = E.Id
		WHERE
			PR.Id = _practiceRoutineId
			
		;
		
		DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;

	END;
	COMMIT;
