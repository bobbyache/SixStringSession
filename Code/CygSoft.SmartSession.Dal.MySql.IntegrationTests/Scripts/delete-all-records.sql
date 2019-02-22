USE `smartsession_tests`;

DELETE FROM TimeSlotExercise WHERE ExerciseId > -1;
DELETE FROM PracticeRoutineTimeslot WHERE PracticeRoutineId > -1;
DELETE FROM TimeSlot WHERE Id > -1;
DELETE FROM PracticeRoutineExercise WHERE ExerciseId > -1;
DELETE FROM PracticeRoutine WHERE Id > -1;
DELETE FROM PracticeRoutine WHERE Id > -1;
DELETE FROM ExerciseActivity WHERE Id > -1;
DELETE FROM Exercise WHERE Id > -1;

