USE Steam

CREATE TABLE Versiuni
(cod_versiune INT PRIMARY KEY IDENTITY,
versiune INT);

INSERT INTO Versiuni (versiune)
values(0);



--modifica tipul unei coloane din int in big int
GO
CREATE PROCEDURE Versi1
AS
BEGIN
ALTER TABLE Jocuri 
ALTER COLUMN Pret FLOAT
PRINT 'Am modificat tipul coloanei jocuri din int in float';
END
 
 
--modifica tipul unei coloane din big int in int
GO
CREATE PROCEDURE UndoVersi1
AS
BEGIN
ALTER TABLE Jocuri
ALTER COLUMN Pret INT
PRINT 'Am modificat tipul coloanei jocuri din float in int';
END

--adauga o constrangere de valoare implicita pentru un camp
GO
CREATE PROCEDURE Versi2
AS
BEGIN
ALTER TABLE Categorii
ADD CONSTRAINT df_denumire DEFAULT  'Denumire' FOR Denumire
PRINT 'Am adaugat constrangere default la campul Data_Imprumutului in tabelul Imprumut_Biblioteca';
END

--sterge o constrangere de valoare implicita pentru un camp
GO
CREATE PROCEDURE UndoVersi2
AS
BEGIN
ALTER TABLE Categorii
DROP CONSTRAINT df_denumire 
PRINT 'Am sters constrangerea default';
END

--creeaza o tabela
GO
CREATE PROCEDURE Versi3
AS
BEGIN
CREATE TABLE Sedii(
Sedid INT PRIMARY KEY IDENTITY,
Locatie VARCHAR(50)
)
PRINT 'Am adaugat tabelul Sedii';
END

--sterge o tabela
GO
CREATE PROCEDURE UndoVersi3
AS
BEGIN
DROP TABLE Sedii
PRINT 'Am sters tabelul Sedii';
END

--adauga un camp nou
GO
CREATE PROCEDURE Versi4
AS 
BEGIN
ALTER TABLE Sedii
ADD Adresa VARCHAR(50)
PRINT 'Am adaugat la tabelul Sedii campul adresa';
END

--sterge un camp
GO
CREATE PROCEDURE UndoVersi4
AS 
BEGIN
ALTER TABLE Sedii
DROP COLUMN Adresa
PRINT 'Am sters campul Adresa din tableul Sedii';
END

--creeaza o constrangere de cheie straina--
GO 
CREATE PROCEDURE Versi5
AS
BEGIN
ALTER TABLE Sedii
ADD Aid INT
ALTER TABLE Sedii
ADD CONSTRAINT fk_Sedii FOREIGN KEY(Aid) REFERENCES Angajati(Aid)
PRINT 'Am adaugat foreign key';
END

--sterge o constrangere de cheie straina--
GO 
CREATE PROCEDURE UndoVersi5
AS
BEGIN
ALTER TABLE Sedii
DROP CONSTRAINT fk_Sedii
ALTER TABLE Sedii
DROP COLUMN Aid

PRINT 'Am sters constrangerea de foreign key din tabelul Angajati';
END

--procedura principala bazata pe versiune
GO
ALTER PROCEDURE modificaVersiune (@nouaVersiune INT)
AS
BEGIN
DECLARE @vecheaVersiune INT
DECLARE @vers VARCHAR(10)
SELECT TOP 1 @vecheaVersiune=versiune
FROM Versiuni
IF @nouaVersiune<0 OR @nouaVersiune>5
BEGIN
	PRINT 'Versiunea trebuie sa fie intre 1 si 5!'
END
ELSE
BEGIN
	IF @vecheaVersiune < @nouaVersiune
	BEGIN
		SET @vecheaVersiune = @vecheaVersiune+1
		WHILE @vecheaVersiune <=@nouaVersiune
		BEGIN
			SET @vers = 'Versi' + CONVERT(VARCHAR(10),@vecheaVersiune)
			EXEC @vers
			PRINT 'S-a executat '+@vers
			SET @vecheaVersiune=@vecheaVersiune+1
			
		END
		UPDATE Versiuni
		SET versiune=@nouaVersiune
	END
	ELSE
	BEGIN
		WHILE @vecheaVersiune > @nouaVersiune
		BEGIN
			SET @vers= 'UndoVersi' + CONVERT(VARCHAR(10),@vecheaVersiune)
			EXEC @vers
			PRINT 'Executata procedura '+@vers
			SET @vecheaVersiune=@vecheaVersiune-1
			
		END
		UPDATE Versiuni
		SET versiune=@nouaVersiune
	END
END
END;
--executabile
EXEC modificaVersiune 0;
EXEC modificaVersiune 1;
EXEC modificaVersiune 2;
EXEC modificaVersiune 3;
EXEC modificaVersiune 4;
EXEC modificaVersiune 5;
select * from Versiuni
