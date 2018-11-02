
DROP DATABASE IF EXISTS `smartsession`;

CREATE DATABASE `smartsession` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;

USE `smartsession`;

CREATE TABLE `exercise` (
  `ID` int(11) NOT NULL,
  `Title` varchar(255) NOT NULL,
  `DifficultyRating` int(11) DEFAULT NULL,
  `PracticalityRating` int(11) DEFAULT NULL,
  `PercentCompleteCalculationType` int(11) NOT NULL,
  `InitialMetronomeSpeed` int(11) DEFAULT NULL,
  `TargetMetronomeSpeed` int(11) DEFAULT NULL,
  `TargetPracticeTime` int(11) DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `smartsession`.`exerciseactivity` (
  `Id` INT NOT NULL,
  `StartTime` DATETIME NOT NULL COMMENT 'Time is not calculated from StartTime to EndTime, these metrics are just there in order to give context and attach to a diary.',
  `EndTime` DATETIME NOT NULL COMMENT 'Time is not calculated from StartTime to EndTime, these metrics are just there in order to give context and attach to a diary.',
  `Seconds` INT NOT NULL,
  `StartSpeed` INT NULL,
  `Speed` INT NOT NULL,
  `DateCreated` DATETIME NOT NULL COMMENT 'A generic exercise activity.',
  `DateModified` DATETIME NULL,
  PRIMARY KEY (`Id`));

ALTER TABLE `smartsession`.`exerciseactivity` 
ADD COLUMN `ExerciseId` INT NOT NULL AFTER `Id`;
ALTER TABLE `smartsession`.`exerciseactivity` 
ADD CONSTRAINT `fk_exercise`
  FOREIGN KEY (`Id`)
  REFERENCES `smartsession`.`exercise` (`ID`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

