-- https://code.msdn.microsoft.com/How-to-using-Entity-1464feea
-- dotnet ef dbcontext scaffold "Server=127.0.0.1; Port=3306; Database=Qlyniq; Uid=nberic; Pwd=nbericpass" Pomelo.EntityFrameworkCore.MySql -o Models --data-annotations
-- dotnet aspnet-codegenerator controller -name MoviesController -m Movie -dc MvcMovieContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries


CREATE DATABASE IF NOT EXISTS `Qlyniq`;
USE `Qlyniq`;

-- Create tables

CREATE TABLE IF NOT EXISTS `Patients`(
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `SocialSecurityNumber` VARCHAR(13) NOT NULL,
    `FirstName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NOT NULL,
    `BirthDate` DATE,
    `Gender` ENUM('Male', 'Female') NOT NULL,
    `HealthCareProvider` VARCHAR(100),
    CHECK (`SocialSecurityNumber` REGEXP '^[0-9]{13}$')
);

CREATE TABLE IF NOT EXISTS `Employees` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `OfficeId` INT UNSIGNED NOT NULL,
    `SocialSecurityNumber` VARCHAR(13) NOT NULL,
    `FirstName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NOT NULL,
    `BirthDate` DATE NOT NULL,
    `Gender` ENUM('Male', 'Female') NOT NULL,
    `IsMedicalWorker` BOOLEAN DEFAULT FALSE,
    `MedicalTitle` VARCHAR(50),
    CHECK (`SocialSecurityNumber` REGEXP '^[0-9]{13}$')
);

CREATE TABLE IF NOT EXISTS `Offices` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(100) NOT NULL,
    `DeanId` INT UNSIGNED NOT NULL,
    `Budget` DECIMAL NOT NULL DEFAULT 0.0
);

CREATE TABLE IF NOT EXISTS `Deans` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `OfficeId` INT UNSIGNED NOT NULL,
    `EmployeeId` INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS `Files` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `PatientId` INT UNSIGNED NOT NULL,
    `CreatorId` INT UNSIGNED NOT NULL,
	`CreationDate` DATETIME NOT NULL,
    `Note` VARCHAR(1000) NOT NULL
);

CREATE TABLE IF NOT EXISTS `Appointments` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `PatientFirstName` VARCHAR(255),
    `PatientLastName` VARCHAR(255),
    `PatientId` INT UNSIGNED,
    `DoctorId` INT UNSIGNED NOT NULL,
    `StartingTime` DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS `Diagnosis` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Code` VARCHAR(4) NOT NULL,
    `MedicalTerm` VARCHAR(100) NOT NULL,
    CHECK (`Code` REGEXP '^[A-Z]{1}[0-9]{3}$')
);

CREATE TABLE IF NOT EXISTS `Examinations` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `PatientId` INT UNSIGNED NOT NULL,
    `DoctorId` INT UNSIGNED NOT NULL,
    `StartingTime` DATETIME NOT NULL,
    `FileId` INT UNSIGNED NOT NULL,
    `DiagnosisId` INT UNSIGNED,
    `Therapy` VARCHAR(500) NOT NULL DEFAULT 'Nihil',
    `IsEmergency` BOOLEAN NOT NULL DEFAULT FALSE,
    `LabReportId` INT UNSIGNED
);

CREATE TABLE IF NOT EXISTS `LabReports` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `RecipientId` INT UNSIGNED NOT NULL,
    `PatientId` INT UNSIGNED NOT NULL,
    `AcceptedTime` DATETIME NOT NULL DEFAULT NOW(),
    `SampledTime` DATETIME NOT NULL DEFAULT NOW(),
    `Glucose` FLOAT NOT NULL,
    `Urea` FLOAT NOT NULL,
    `Creatine` FLOAT NOT NULL,
    `Cholesterol` FLOAT NOT NULL,
    `Helicobacter` BOOL NOT NULL DEFAULT FALSE
);


-- Add foreign keys

ALTER TABLE `Employees`
ADD CONSTRAINT FK_Employees_OfficeId
	FOREIGN KEY (`OfficeId`) REFERENCES `Offices`(`Id`);

ALTER TABLE `Offices`
ADD CONSTRAINT FK_Offices_DeanId 
	FOREIGN KEY (`DeanId`) REFERENCES `Deans`(`Id`);

ALTER TABLE `Deans`
ADD CONSTRAINT FK_Deans_OfficeId 
	FOREIGN KEY (`OfficeId`) REFERENCES `Offices`(`Id`),
ADD CONSTRAINT FK_Deans_EmployeeId
	FOREIGN KEY (`EmployeeId`) REFERENCES `Employees`(`Id`);

ALTER TABLE `Files`
ADD CONSTRAINT FK_Files_PatientId
	FOREIGN KEY (`PatientId`) REFERENCES `Patients`(`Id`),
ADD CONSTRAINT FK_Files_CreatorId
	FOREIGN KEY (`CreatorId`) REFERENCES `Employees`(`Id`);
    
ALTER TABLE `Appointments`
ADD CONSTRAINT FK_Appointments_PatientId
	FOREIGN KEY (`PatientId`) REFERENCES `Patients`(`Id`),
ADD CONSTRAINT FK_Appointments_DoctorId
	FOREIGN KEY (`DoctorId`) REFERENCES `Employees`(`Id`);
    
ALTER TABLE `Examinations`
ADD CONSTRAINT FK_Examinations_PatientId
	FOREIGN KEY (`PatientId`) REFERENCES `Patients`(`Id`),
ADD CONSTRAINT FK_Examinations_DoctorId
	FOREIGN KEY (`DoctorId`) REFERENCES `Employees`(`Id`),
ADD CONSTRAINT FK_Examinations_FileId
	FOREIGN KEY (`FileId`) REFERENCES `Files`(`Id`),
ADD CONSTRAINT FK_Examinations_DiagnosisId
	FOREIGN KEY (`DiagnosisId`) REFERENCES `Diagnosis`(`Id`),
ADD CONSTRAINT FK_Examinations_LabReportId
	FOREIGN KEY (`LabReportId`) REFERENCES `LabReports`(`Id`);

ALTER TABLE `LabReports`
ADD CONSTRAINT FK_LabResults_RecipientId
	FOREIGN KEY (`RecipientId`) REFERENCES `Employees`(`Id`),
ADD CONSTRAINT FK_LabResults_PatientId
	FOREIGN KEY (`PatientId`) REFERENCES `Patients`(`Id`);