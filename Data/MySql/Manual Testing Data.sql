USE `smartsession_tests`;

DELETE FROM TimeSlotExercise WHERE ExerciseId > -1;
DELETE FROM PracticeRoutineTimeslot WHERE PracticeRoutineId > -1;
DELETE FROM TimeSlot WHERE Id > -1;
DELETE FROM PracticeRoutineExercise WHERE ExerciseId > -1;
DELETE FROM PracticeRoutine WHERE Id > -1;
DELETE FROM PracticeRoutine WHERE Id > -1;
DELETE FROM ExerciseActivity WHERE Id > -1;
DELETE FROM Exercise WHERE Id > -1;



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

	
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Strumming Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Strumming Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Strumming Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Strumming Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Lead Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Lead Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Lead Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Lead Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Scales Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Scales Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Scales Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Scales Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Theory Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Theory Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Theory Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Theory Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Song Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Song Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Song Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Song Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Ear Exercise 1', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Ear Exercise 2', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Ear Exercise 3', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Ear Exercise 4', 80, 50, 600, 50, 0, '2017-02-01 01:00:00', NULL);
	
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Monday Routine', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Tuesday Routine', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Wednesday Routine', '2017-02-01 01:00:00');

INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Strumming Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Lead Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Scales Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Theory Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Song Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Monday - Ear Training Practice", 5, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Strumming Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Lead Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Scales Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Theory Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Song Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Tuesday - Ear Training Practice", 5, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Strumming Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Lead Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Scales Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Theory Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Song Practice", 5, '2017-02-01 01:20:00', NULL);
INSERT INTO TimeSlot (Title, AssignedPracticeTime, DateCreated, DateModified) VALUES ("Wednesday - Ear Training Practice", 5, '2017-02-01 01:20:00', NULL);



INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Strumming Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Lead Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Scales Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Theory Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Song Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Monday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Monday - Ear Training Practice"), '2017-02-01 01:20:00', NULL);



INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Strumming Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Lead Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Scales Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Theory Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Song Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Tuesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Tuesday - Ear Training Practice"), '2017-02-01 01:20:00', NULL);



INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Strumming Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Lead Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Scales Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Theory Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Song Practice"), '2017-02-01 01:20:00', NULL);

INSERT INTO PracticeRoutineTimeslot (PracticeRoutineId, TimeSlotId, DateCreated, DateModified)
VALUES ((SELECT Id FROM PracticeRoutine WHERE Title = "Wednesday Routine"), (SELECT Id FROM TimeSlot WHERE Title = "Wednesday - Ear Training Practice"), '2017-02-01 01:20:00', NULL);





INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 4'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Monday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 4'), 2, '2017-02-01 01:20:00', NULL);









INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 4'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Tuesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 4'), 2, '2017-02-01 01:20:00', NULL);






INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Strumming Practice'), (SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 4'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Lead Practice'), (SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Scales Practice'), (SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Theory Practice'), (SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Song Practice'), (SELECT Id FROM Exercise WHERE Title = 'Song Exercise 4'), 2, '2017-02-01 01:20:00', NULL);


INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 1'), 2, '2017-02-01 01:20:00', NULL);
	
INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 2'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 3'), 2, '2017-02-01 01:20:00', NULL);

INSERT INTO TimeSlotExercise (TimeSlotId, ExerciseId, FrequencyWeighting, DateCreated, DateModified)
VALUES ((SELECT Id FROM TimeSlot WHERE Title = 'Wednesday - Ear Training Practice'), (SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 4'), 2, '2017-02-01 01:20:00', NULL);



INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, FrequencyWeighting, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Yellow Exercise'),
            100,
            3,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, FrequencyWeighting, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Green Exercise'),
            120,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, FrequencyWeighting, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Tuesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Orange Exercise'),
            120,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, FrequencyWeighting, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Wednesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Orange Exercise'),
            120,
            4,
            '2015-02-01 01:20:00',
            NULL
        );

INSERT INTO PracticeRoutineExercise (PracticeRoutineId, ExerciseId, AssignedPracticeTime, FrequencyWeighting, DateCreated, DateModified)
	VALUES 
		(
			(SELECT Id FROM PracticeRoutine WHERE Title = 'Wednesday Routine'),
            (SELECT Id FROM Exercise WHERE Title = 'Blue Exercise'),
            120,
            4,
            '2015-02-01 01:20:00',
            NULL
        );
		
		


-- One activity for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Strumming Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
-- Two activities for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 1'), 60, 40, 20, '2019-03-16', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 2'), 60, 40, 20, '2019-03-16', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 3'), 60, 40, 20, '2019-03-16', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Lead Exercise 4'), 60, 40, 20, '2019-03-16', null);
        
-- One activity for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Scales Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
-- One activity for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Theory Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
-- One activity for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Song Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Song Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Song Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Song Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
-- One activity for every exercise.

INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 1'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 2'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 3'), 60, 35, 10, '2019-03-15', null);
    
INSERT INTO ExerciseActivity (ExerciseId, Seconds, MetronomeSpeed, ManualProgress, DateCreated, DateModified)
	VALUES ((SELECT Id FROM Exercise WHERE Title = 'Ear Exercise 4'), 60, 35, 10, '2019-03-15', null);
    
	