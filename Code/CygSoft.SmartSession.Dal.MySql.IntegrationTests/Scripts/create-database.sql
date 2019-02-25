
DROP DATABASE IF EXISTS `smartsession_tests`;
CREATE DATABASE `smartsession_tests` /* !40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `smartsession_tests`;

/* ********************************************************************************************************************************
TEST DATABASE
-----------------------------------------------------------------------------------------------------------------------------------
	This database is the first database in the line of attack.
	When you're happy with all tests on this database then you can run this script against the QA database (smartsession_qa). 

	Go to the Data\MySql folder to update/find the production or release scripts.
******************************************************************************************************************************** */

CREATE TABLE IF NOT EXISTS `exercise` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `TargetMetronomeSpeed` int(11) DEFAULT NULL,
  `SpeedProgressWeighting` int(11) NOT NULL,
  `TargetPracticeTime` int(11) DEFAULT NULL,
  `PracticeTimeProgressWeighting` int(11) NOT NULL,
  `ManualProgressWeighting` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `smartsession_tests`.`exerciseactivity` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `ExerciseId` INT NOT NULL,
  `Seconds` INT NOT NULL,
  `MetronomeSpeed` INT NOT NULL,
  `ManualProgress` INT NOT NULL,
  `DateCreated` DATETIME NOT NULL COMMENT 'A generic exercise activity.',
  `DateModified` DATETIME NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_exerciseactivity_exercise` FOREIGN KEY (`ExerciseId`) REFERENCES `exercise` (`Id`)
);

CREATE TABLE IF NOT EXISTS `practiceroutine` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `timeslot` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `AssignedPracticeTime` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `practiceroutinetimeslot` (
  `PracticeRoutineId` int(11) NOT NULL,
  `TimeSlotId` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`PracticeRoutineId`, `TimeSlotId`),
  CONSTRAINT `fk_exerciseroutine_timeslot` FOREIGN KEY (`TimeSlotId`) REFERENCES `timeslot` (`Id`)
);

CREATE TABLE IF NOT EXISTS `timeslotexercise` (
  `TimeSlotId` int(11) NOT NULL,
  `ExerciseId` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`TimeSlotId`, `ExerciseId`),
  CONSTRAINT `fk_timeslotexercise_exercise` FOREIGN KEY (`ExerciseId`) REFERENCES `exercise` (`Id`),
  CONSTRAINT `fk_timeslotexercise_timeslot` FOREIGN KEY (`TimeSlotId`) REFERENCES `timeslot` (`Id`)
);

CREATE TABLE IF NOT EXISTS `practiceroutineexercise` (
  `PracticeRoutineId` int(11) NOT NULL,
  `ExerciseId` int(11) NOT NULL,
  `AssignedPracticeTime` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`PracticeRoutineId`, `ExerciseId`),
  CONSTRAINT `fk_exerciseroutine_exercise` FOREIGN KEY (`ExerciseId`) REFERENCES `exercise` (`Id`),
  CONSTRAINT `fk_exerciseroutine_practiceroutine` FOREIGN KEY (`PracticeRoutineId`) REFERENCES `practiceroutine` (`Id`)
);