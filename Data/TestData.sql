USE SmartSession_EF

BEGIN TRANSACTION

DELETE FROM dbo.Keywords
DELETE FROM dbo.FileAttachments
DELETE FROM dbo.Exercises


INSERT INTO dbo.Keywords
VALUES
('Vibrato'),
('Bending'),
('Bends'),
('Blues'),
('12 Bar'),
('Technique')

INSERT INTO dbo.Exercises (Title, Scribed, DifficultyRating, OptimalDuration, PracticalityRating, Notes, DateCreated, DateModified) VALUES ('12 Bar Blues Exercise', 1, 2, 60, 2, 'Exercise your 12 bar blues and get jiggy with it.', GETDATE(), GETDATE())
INSERT INTO dbo.Exercises (Title, Scribed, DifficultyRating, OptimalDuration, PracticalityRating, Notes, DateCreated, DateModified) VALUES ('Vibrato Exercise', 1, 2, 120, 2, 'Work on your vibrato. Pull chicks.', GETDATE(), GETDATE())
INSERT INTO dbo.Exercises (Title, Scribed, DifficultyRating, OptimalDuration, PracticalityRating, Notes, DateCreated, DateModified) VALUES ('String Bending Exercise', 1, 3, 578, 3, 'Bend it like Clapton. Look cool and hip.', GETDATE(), GETDATE())

INSERT INTO [dbo].[ExerciseKeyword]
VALUES
(
	(SELECT Id FROM Exercises WHERE Title = '12 Bar Blues Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Blues')
),
(
	(SELECT Id FROM Exercises WHERE Title = '12 Bar Blues Exercise'),
	(SELECT Id FROM Keywords WHERE Word = '12 Bar')
),

(
	(SELECT Id FROM Exercises WHERE Title = 'Vibrato Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Vibrato')
),
(
	(SELECT Id FROM Exercises WHERE Title = 'Vibrato Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Technique')
),

(
	(SELECT Id FROM Exercises WHERE Title = 'String Bending Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Bending')
),
(
	(SELECT Id FROM Exercises WHERE Title = 'String Bending Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Bends')
),
(
	(SELECT Id FROM Exercises WHERE Title = 'String Bending Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Technique')
)

INSERT INTO dbo.FileAttachments (FileTitle, Extension, Notes, DateCreated, DateModified) VALUES ('12_Bar_Blues_Exercise', '.gp', 'Exercise file', GETDATE(), GETDATE())

INSERT INTO [dbo].[FileAttachmentKeyword]
VALUES
(
	(SELECT Id FROM FileAttachments WHERE FileTitle = '12_Bar_Blues_Exercise'),
	(SELECT Id FROM Keywords WHERE Word = 'Blues')
)

COMMIT TRANSACTION