
--pentru usurinta am pus totul aici

use Steam
select * from Jocuri1
-- pentru dirty reads
BEGIN TRANSACTION
UPDATE Jocuri1 SET DescriereJ='descriere frumix1' WHERE Jid = 2
WAITFOR DELAY '00:00:10'
ROLLBACK TRANSACTION

--Problema
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN

select * from Jocuri1

BEGIN TRANSACTION
UPDATE Jocuri1 SET DescriereJ='descriere frumix1' WHERE Jid = 2
WAITFOR DELAY '00:00:10'
ROLLBACK TRANSACTION

--Rezolvarea
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN



--pentru non-repeatable reads
select * from Jocuri1

INSERT INTO Jocuri1(NumeJ,DescriereJ,Pret) VALUES ('joculet1','desc1',50)
BEGIN TRAN
WAITFOR DELAY '00:00:10'
UPDATE Jocuri1 SET DescriereJ='descriere frumi 2' WHERE NumeJ ='joculet1'
COMMIT TRAN

--Problema
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN

select * from Jocuri1
delete from Jocuri1 where NumeJ = 'joculet1'
select * from Jocuri1

INSERT INTO Jocuri1(NumeJ,DescriereJ,Pret) VALUES ('joculet1','desc1',50)
BEGIN TRAN
WAITFOR DELAY '00:00:10'
UPDATE Jocuri1 SET DescriereJ='descriere frumi 2' WHERE NumeJ ='joculet1'
COMMIT TRAN

--Rezolvarea
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN

SELECT * FROM Jocuri1


--pentru phantom reads
select * from Jocuri1

BEGIN TRAN
WAITFOR DELAY '00:00:10'
INSERT INTO Jocuri1(NumeJ,DescriereJ,Pret) VALUES ('jocccc','cel mai joc',34)
COMMIT TRAN

--Problema
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN

select * from Jocuri1
delete from Jocuri1 where NumeJ='jocccc'
select * from Jocuri1

BEGIN TRAN
WAITFOR DELAY '00:00:10'
INSERT INTO Jocuri1(NumeJ,DescriereJ,Pret) VALUES ('jocccc','cel mai joc',34)
COMMIT TRAN

--Rezolvarea
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
SELECT * FROM Jocuri1
WAITFOR DELAY '00:00:15'
SELECT * FROM Jocuri1
COMMIT TRAN

select * from Jocuri1


--pentru deadlock
insert into Jocuri1 values ('joc deadlock', 'haida cu deadlock ul',25)
insert into ComenziPtRollback2 values ('2012-05-12',700)
select * from Jocuri1
select * from ComenziPtRollback2

-- prima tranzactie
begin tran
update Jocuri1 set NumeJ='deadlock Jocuri1 Tran 1' where Jid=3
-- lock - Jocuri1
waitfor delay '00:00:10'
update ComenziPtRollback2 set Suma=200 where Cid=2
commit tran

-- a doua tranzactie
begin tran
update ComenziPtRollback2 set Suma=200 where Cid=2
-- lock - ComenziPtRollback2
waitfor delay '00:00:10'
update Jocuri1 set NumeJ='deadlock Jocuri1 Tran 1' where Jid=3
commit tran

select * from Jocuri1
select * from ComenziPtRollback2

--setam prioritati
-- prima tranzactie 
begin tran
update Jocuri1 set NumeJ='deadlock Jocuri1 Tran 1' where Jid=3
-- lock - Jocuri1
waitfor delay '00:00:10'
update ComenziPtRollback2 set Suma=200 where Cid=2
commit tran
select* from Jocuri1
select* from ComenziPtRollback2
-- a doua tranzactie 
--SET DEADLOCK_PRIORITY HIGH
SET DEADLOCK_PRIORITY LOW
begin tran
update ComenziPtRollback2 set Suma=200 where Cid=2
-- lock - ComenziPtRollback2
waitfor delay '00:00:10'
update Jocuri1 set NumeJ='deadlock Jocuri1 Tran 1' where Jid=3
commit tran
select* from Jocuri1
select* from ComenziPtRollback2