USE smartsession_tests;

-- ----------------------------------------------------------
-- Insert test exercises.
-- CALL sp_InsertExercise('My Title', 1, 1, 0, 50, 60, 300);
-- ----------------------------------------------------------
INSERT INTO Exercise (Title, DifficultyRating, PracticalityRating, TargetMetronomeSpeed, TargetPracticeTime, PercentageCompleteCalculationType, DateCreated, DateModified) 
	VALUES ('Test Title 1', 1, 3, 80, 3600, 1, '2015-10-30 01:02:03', '2015-11-20 13:50:59');

INSERT INTO Exercise (Title, DifficultyRating, PracticalityRating, TargetMetronomeSpeed, TargetPracticeTime, PercentageCompleteCalculationType, DateCreated, DateModified) 
	VALUES ('Test Title 2', 1, 3, 80, 3600, 0, '2015-10-30 01:02:03', '2015-11-20 13:50:59');

