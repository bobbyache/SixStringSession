USE smartsession_tests;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sp_InsertExercise`$$
CREATE PROCEDURE `sp_InsertExercise`
(
	in title varchar(255), 
    in difficulty_rating int(11),
    in practicality_rating int(11),
    in percentage_complete_calculation_type int(11),
    in initial_metronome_speed int(11),
    in target_metronome_speed int(11),
    in target_practice_time int(11)
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
        difficulty_rating,
        practicality_rating,
        percentage_complete_calculation_type,
        initial_metronome_speed,
        target_metronome_speed,
        target_practice_time,
		NOW(), 
        NULL
	);
	SELECT LAST_INSERT_ID()$$
	COMMIT;
END $$

DELIMITER ;
COMMIT;