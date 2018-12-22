USE `smartsession_tests`;

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Yellow Exercise', 80, 50, NULL, 50, 0, '2015-02-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise', 100, 50, NULL, 50, 0, '2015-05-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Blue Exercise', 120, 50, NULL, 50, 0, '2015-12-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Orange Exercise', NULL, 50, 30000, 50, 0, '2016-02-01 01:00:00', NULL);

INSERT INTO Exercise (Title, TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise 1', NULL, 50, 40000, 50, 0, '2017-02-01 01:00:00', '2017-04-01 01:00:00');

INSERT INTO Exercise (Title,  TargetMetronomeSpeed, SpeedProgressWeighting, TargetPracticeTime, PracticeTimeProgressWeighting, ManualProgressWeighting, DateCreated, DateModified) 
	VALUES ('Green Exercise 2', NULL, 50, 50000, 50, 0, '2017-02-01 01:00:00', '2017-06-01 01:00:00');

INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Monday', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Tuesday', '2017-02-01 01:00:00');
    
INSERT INTO PracticeRoutine (Title, DateCreated)
	VALUES ('Wednesday', '2017-02-01 01:00:00');