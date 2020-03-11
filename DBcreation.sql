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

CREATE TABLE Cursus(cursus_id INT PRIMARY KEY IDENTITY(1,1),
					[name] VARCHAR(60),
					startDate DATE NOT NULL,
					endDate DATE NOT NULL)

CREATE TABLE Person(person_id INT PRIMARY KEY IDENTITY(1,1),
					[name] VARCHAR(60),
					superiour_id INT NOT NULL FOREIGN KEY REFERENCES Person(person_id),
					FK_cursus_id INT FOREIGN KEY REFERENCES Cursus(cursus_id))


CREATE TABLE Agenda(agenda_id INT PRIMARY KEY IDENTITY(1,1),
                    FK_person_id INT FOREIGN KEY REFERENCES Person(person_id),
					FK_event_id INT NOT NULL FOREIGN KEY REFERENCES [Event](event_id)
					)
GO

INSERT INTO [Event](event_name, event_date, event_description) VALUES ('Startup Day', '2020-02-25', 'Come over and take part in the main start-up event dedicated to the future of transportation, aviation and tourism!'), ('Freshers defloration', '2020-09-02', 'College student freshers wanted urgently audition start new hindi Tv serial & movies WhatsApp'),
												   ('Mad bitches night', '2020-03-09', 'You cannot be out here on stage—these bitches paid mad money, and this is ladies night'), ('Coding with Billy', '2020-03-01', 'Come code with Billy. Or with Corentin... If you got spare 5-6 hours'),
												   ('Great special event', '2020-04-02', 'This whole event is just very fucking special you know'), ('Caramba Day', '2020-05-12', 'Come feel the Caramba vibe today, Hohohoh!!!!'), ('Black swans exodus', '2019-04-15', 'Black swans are going to leave the premises today'), ('Groundhog Day', '2019-02-02', 'Come witness the little furry bastard see its shadow. Or not'),
												   ('Annual carnival', '2019-10-15', 'Get drunk, get laid, just fucking relax pal')
GO

INSERT INTO Person([name], superiour_id) VALUES ('Mestre Bimba', 1), ('Joshua', 1), ('Michael', 1), ('John', 1),
											   ('Huba', 1), ('Buba', 2), ('Sucka', 1), ('Fucka', 1), ('Licka', 2)
GO

INSERT INTO Cursus([name], startDate, endDate) VALUES ('Computer Science', '2019-09-25', '2020-06-25'), ('History of Arts', '2020-02-01', '2020-08-25'), 
													  ('Biotechnology', '2019-11-20', '2020-11-25'), ('Ass-eating', '2019-09-25', '2021-06-25')
GO


--Fill the Person table with Students
Declare @number_student INT = 0
Declare @name_suffix INT = 1

WHILE(@number_student <= 150)
BEGIN
   Declare @name VARCHAR(20) = 'Student ' + CONVERT(VARCHAR, @name_suffix)
   INSERT INTO Person([name], superiour_id, FK_cursus_id) VALUES (@name, FLOOR(RAND()*(9-1+1))+1, FLOOR(RAND()*(4-1+1))+1)
   SET @name_suffix = @name_suffix + 1
   SET @number_student = @number_student + 1               
END
GO

--Fill the Agenda table
Declare @counter INT = 0

WHILE(@counter <= 600)
BEGIN
	   INSERT INTO Agenda(FK_person_id, FK_event_id) VALUES (FLOOR(RAND()*(160-1+1))+1, FLOOR(RAND()*(9-1+1))+1)
	   SELECT @counter = (SELECT COUNT(agenda_id) FROM Agenda)
END
GO

--Procedure qui retourne tous les événements auxquels une personne assiste
DROP PROCEDURE IF EXISTS sp_eventsPerson
GO
CREATE PROCEDURE sp_eventsPerson
	@personName VARCHAR(50)
	AS
	BEGIN
		SELECT event_name, event_date, event_description FROM [Event]
		INNER JOIN Agenda ON Agenda.FK_event_id = [Event].event_id
		INNER JOIN Person ON Person.person_id = Agenda.FK_person_id
		WHERE Person.[name] = @personName
	END
GO
--EXECUTE sp_eventsPerson 'Mestre Bimba'


--Procedure getting the number of subordinates of a Person by Name
DROP PROCEDURE IF EXISTS sp_getNumberSubordinates
GO
CREATE PROCEDURE sp_getNumberSubordinates
	@personName VARCHAR(50)
	AS
	BEGIN
		SELECT sp.[name], COUNT(sub.person_id) AS Number_subordinates FROM Person AS sp
		INNER JOIN Person AS sub ON sub.superiour_id = sp.person_id
		WHERE sp.[name] = @personName
		GROUP BY sp.[name]
	END
GO
--EXECUTE sp_getNumberSubordinates 'Licka'

--The list of subordinates of a Person by Name
DROP PROCEDURE IF EXISTS sp_getSubordinatesByName
GO
CREATE PROCEDURE sp_getSubordinatesByName
	@personName VARCHAR(50)
	AS
	BEGIN
		SELECT sp.[name] AS Chief, sub.[name] AS Subordinate_name FROM Person AS sp
		INNER JOIN Person AS sub ON sub.superiour_id = sp.person_id
		WHERE sp.[name] = @personName
	END
GO
