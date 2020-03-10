DROP DATABASE IF EXISTS CheckPoint2
GO
CREATE DATABASE CheckPoint2
GO

USE CheckPoint2
GO

CREATE TABLE [Event](event_id INT PRIMARY KEY IDENTITY(1,1),
                    event_name VARCHAR(50) NOT NULL,
                    event_date DATE NOT NULL,
					event_description VARCHAR(max))

CREATE TABLE Prof(prof_id INT PRIMARY KEY IDENTITY(1,1),
					[name] VARCHAR(60),
					superiour_id INT NOT NULL FOREIGN KEY REFERENCES Prof(prof_id))


CREATE TABLE Cursus(cursus_id INT PRIMARY KEY IDENTITY(1,1),
                    [name] VARCHAR(60),
					startDate DATE NOT NULL,
					endDate DATE NOT NULL)

CREATE TABLE Student(student_id INT PRIMARY KEY IDENTITY(1,1),
                      [name] VARCHAR(60),
					  FK_prof_id INT NOT NULL FOREIGN KEY REFERENCES Prof(prof_id),
					  FK_cursus_id INT NOT NULL FOREIGN KEY REFERENCES Cursus(cursus_id)
					  )

CREATE TABLE Agenda(FK_prof_id INT FOREIGN KEY REFERENCES Prof(prof_id),
					FK_student_id INT FOREIGN KEY REFERENCES Student(student_id),
					FK_event_id INT NOT NULL FOREIGN KEY REFERENCES [Event](event_id)
					)
Alter Table Agenda
Add Constraint PK_Agenda
Primary Key Clustered (FK_prof_id, FK_student_id, FK_event_id)
GO

INSERT INTO [Event](event_name, event_date, event_description) VALUES ('Startup Day', '2020-02-25', 'Come over and take part in the main start-up event dedicated to the future of transportation, aviation and tourism!'), ('Freshers defloration', '2020-09-02', 'College student freshers wanted urgently audition start new hindi Tv serial & movies WhatsApp'),
												   ('Mad bitches night', '2020-03-09', 'You cannot be out here on stage—these bitches paid mad money, and this is ladies night'), ('Coding with Billy', '2020-03-01', 'Come code with Billy. Or with Corentin... If you got spare 5-6 hours'),
												   ('Great special event', '2020-04-02', 'This whole event is just very fucking special you know')
GO

INSERT INTO Prof([name], superiour_id) VALUES ('Mestre Bimba', 1), ('Joshua', 1), ('Michael', 1), ('John', 1),
											   ('Huba', 1), ('Buba', 2), ('Sucka', 1), ('Fucka', 1), ('Licka', 2)
GO

INSERT INTO Cursus([name], startDate, endDate) VALUES ('Computer Science', '2019-09-25', '2020-06-25'), ('History of Arts', '2020-02-01', '2020-08-25'), 
													  ('Biotechnology', '2019-11-20', '2020-11-25'), ('Ass-eating', '2019-09-25', '2021-06-25')
GO


--Fill the Student table
Declare @number_student INT = 0
Declare @name_suffix INT = 1

WHILE(@number_student <= 150)
BEGIN
   Declare @name VARCHAR(20) = 'Student ' + CONVERT(VARCHAR, @name_suffix)
   INSERT INTO Student([name], FK_prof_id, FK_cursus_id) VALUES (@name, FLOOR(RAND()*(9-1+1))+1, FLOOR(RAND()*(4-1+1))+1)
   SET @name_suffix = @name_suffix + 1
   SELECT @number_student = (SELECT COUNT(student_id) FROM Student)             
END
GO

--Fill the Agenda table
Declare @counter INT = 0

WHILE(@counter <= 300)
BEGIN
   IF @counter % 5 = 0
   BEGIN
	   INSERT INTO Agenda(FK_prof_id, FK_student_id, FK_event_id) VALUES (FLOOR(RAND()*(9-1+1))+1, null, FLOOR(RAND()*(5-1+1))+1)
	   SET @counter = @counter + 1
   END
   ELSE
   BEGIN
	   INSERT INTO Agenda(FK_prof_id, FK_student_id, FK_event_id) VALUES (null, FLOOR(RAND()*(151-1+1))+1, FLOOR(RAND()*(5-1+1))+1)
	   SET @counter = @counter + 1
   END
END
GO

--Procedures événements auxquels une personne assiste sur une période donnée
DROP PROCEDURE IF EXISTS sp_eventsStudent
GO
CREATE PROCEDURE sp_eventsStudent
	@studentName VARCHAR(50)
	AS
	BEGIN
		SELECT event_name, event_date, event_description FROM [Event]
		INNER JOIN Agenda ON Agenda.FK_event_id = [Event].event_id
		INNER JOIN Student ON Student.student_id = Agenda.FK_student_id
		WHERE Student.[name] = @studentName
	END
GO
--EXECUTE sp_eventsStudent 'Student 20'
DROP PROCEDURE IF EXISTS sp_eventsProf
GO
CREATE PROCEDURE sp_eventsProf
	@profName VARCHAR(50)
	AS
	BEGIN
		SELECT event_name, event_date, event_description FROM [Event]
		INNER JOIN Agenda ON Agenda.FK_event_id = [Event].event_id
		INNER JOIN Prof ON Prof.prof_id = Agenda.FK_prof_id
		WHERE Prof.[name] = @profName
	END
GO
--EXECUTE sp_eventsProf 'Mestre Bimba'
