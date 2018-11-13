
/*
	README !!!
	Always want a copy of this, because your test database will often be ahead of your
	production database.
	Go to the Data\MySql folder to update/find the production or release scripts.
*/



DROP DATABASE IF EXISTS `smartsession_tests`;

CREATE DATABASE `smartsession_tests` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;

USE `smartsession_tests`;

CREATE TABLE `exercise` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `DifficultyRating` int(11) NOT NULL,
  `PracticalityRating` int(11)  NOT NULL,
  `PercentageCompleteCalculationType` int(11) NOT NULL,
  `InitialMetronomeSpeed` int(11) DEFAULT NULL,
  `TargetMetronomeSpeed` int(11) DEFAULT NULL,
  `TargetPracticeTime` int(11) DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `smartsession_tests`.`exerciseactivity` (
  `Id` INT NOT NULL,
  `StartTime` DATETIME NOT NULL COMMENT 'Time is not calculated from StartTime to EndTime, these metrics are just there in order to give context and attach to a diary.',
  `EndTime` DATETIME NOT NULL COMMENT 'Time is not calculated from StartTime to EndTime, these metrics are just there in order to give context and attach to a diary.',
  `Seconds` INT NOT NULL,
  `MetronomeSpeed` INT NOT NULL,
  `DateCreated` DATETIME NOT NULL COMMENT 'A generic exercise activity.',
  `DateModified` DATETIME NOT NULL,
  PRIMARY KEY (`Id`));

ALTER TABLE `smartsession_tests`.`exerciseactivity` 
ADD COLUMN `ExerciseId` INT NOT NULL AFTER `Id`;
ALTER TABLE `smartsession_tests`.`exerciseactivity` 
ADD CONSTRAINT `fk_exercise`
  FOREIGN KEY (`Id`)
  REFERENCES `smartsession_tests`.`exercise` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

