
    /* ************************************************************************************************
	CALL sp_GetPracticeRoutineExerciseRecordersByRoutineId ((SELECT id FROM PracticeRoutine WHERE Title = 'Monday Routine'))
    ************************************************************************************************ */

	SET @_practiceRoutineId = (SELECT id FROM PracticeRoutine WHERE Title = 'Monday Routine');
		
	DROP TEMPORARY TABLE IF EXISTS RandomRoutineSlotExercises;
	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;
	
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
			-- https://www.alvinpoh.com/how-to-randomly-select-from-a-record-based-on-weight-php-mysql/
            -- Random, but weighted.
			ORDER BY LOG(RAND()) / TSE1.FrequencyWeighting DESC LIMIT 1
            
		) AS ExerciseId
	FROM 
		PracticeRoutine PR 
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
		INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
		INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
		INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
	WHERE
		PR.Id = @_practiceRoutineId 
	GROUP BY
		PR.Id,
		T.Id;

	CREATE TEMPORARY TABLE LastMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS LastRecordedSpeed,
		EA.ManualProgress AS LastRecordedManualProgress,
        (
			select 
				sum(ea1.seconds) 
			from
				exerciseactivity ea1
				inner join exercise e1 on e1.id = ea1.exerciseid
			where
				e1.id = e.id
        ) TotalSeconds
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN TimeSlotExercise TSE ON TSE.ExerciseId = E.Id
		INNER JOIN TimeSlot T ON T.Id = TSE.TimeSlotId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.TimeSlotId = T.Id
		INNER JOIN PracticeRoutine PR ON PR.Id = PRT.PracticeRoutineId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MAX(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
				INNER JOIN TimeSlotExercise TSEx ON TSEx.ExerciseId = Ex.Id
				INNER JOIN TimeSlot Tx ON Tx.Id = TSEx.TimeSlotId
				INNER JOIN PracticeRoutineTimeSlot PRTx ON PRTx.TimeSlotId = Tx.Id
				INNER JOIN PracticeRoutine PRx ON PRx.Id = PRTx.PracticeRoutineId
			WHERE
				PRx.Id = @_practiceRoutineId 
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		PR.Id = @_practiceRoutineId 
	GROUP BY
		E.Id, 
		EA.MetronomeSpeed, 
		EA.ManualProgress
	;
	
	CREATE TEMPORARY TABLE FirstMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS InitialRecordedSpeed
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN TimeSlotExercise TSE ON TSE.ExerciseId = E.Id
		INNER JOIN TimeSlot T ON T.Id = TSE.TimeSlotId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.TimeSlotId = T.Id
		INNER JOIN PracticeRoutine PR ON PR.Id = PRT.PracticeRoutineId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MIN(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
				INNER JOIN TimeSlotExercise TSEx ON TSEx.ExerciseId = Ex.Id
				INNER JOIN TimeSlot Tx ON Tx.Id = TSEx.TimeSlotId
				INNER JOIN PracticeRoutineTimeSlot PRTx ON PRTx.TimeSlotId = Tx.Id
				INNER JOIN PracticeRoutine PRx ON PRx.Id = PRTx.PracticeRoutineId
			WHERE
				PRx.Id = @_practiceRoutineId 
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		PR.Id = @_practiceRoutineId ;
		
	SELECT 
		PR.Id AS PracticeRoutineId,
		E.Id AS ExerciseId,
		
		T.Title AS TimeSlotTitle,
		E.Title AS ExerciseTitle,

		E.ManualProgressWeighting,
		E.SpeedProgressWeighting,
		E.PracticeTimeProgressWeighting,
			
		IFNULL(FM.InitialRecordedSpeed, 0) AS InitialRecordedSpeed,
		IFNULL(LM.LastRecordedSpeed, 0) AS LastRecordedSpeed,
		IFNULL(LM.LastRecordedManualProgress, 0) AS LastRecordedManualProgress,
		IFNULL(LM.TotalSeconds, 0) AS TotalPracticeTime,
		IFNULL(T.AssignedPracticeTime, 0) AS AssignedPracticeTime,
		E.TargetMetronomeSpeed AS TargetMetronomeSpeed,
		E.TargetPracticeTime AS TargetPracticeTime,
		PR.DateCreated,
		PR.DateModified
		
	FROM
		RandomRoutineSlotExercises TMP
		INNER JOIN PracticeRoutine PR ON PR.Id = TMP.PracticeRoutineId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
		INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
		INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
		INNER JOIN Exercise E ON E.Id = TSE.ExerciseId AND E.Id = TMP.ExerciseId
		LEFT JOIN LastMetronomeSpeeds LM ON LM.ExerciseId = E.Id
		LEFT JOIN FirstMetronomespeeds FM ON FM.ExerciseId = E.Id
	;
	
	DROP TEMPORARY TABLE IF EXISTS RandomRoutineSlotExercises;
	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;
    
    
    
/*
	Change Weighting for a time slot exercise
    ===============================================================================================================
			-- -- UPDATE TimeSlotExercise SET FrequencyWeighting = 4 WHERE ExerciseId = 104 AND TimeSlotId = 19;
			SELECT 
				TSE1.*,
				PR.Title, T.Title
			FROM 
				Exercise E1
				INNER JOIN TimeSlotExercise TSE1 ON TSE1.ExerciseId = E1.Id
                INNER JOIN TimeSlot T ON T.Id = TSE1.TimeSlotId
                INNER JOIN PracticeRoutineTimeslot PRTS ON PRTS.TimeSlotId = T.Id
                INNER JOIN PracticeRoutine PR ON PR.Id = PRTS.PracticeRoutineId
			WHERE
				TSE1.TimeSlotId = T.Id
                AND T.Id = 19;
*/
                
                
			
                
